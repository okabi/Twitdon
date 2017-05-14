using CoreTweet.Streaming;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Twitter タイムラインを表すクラスです。
    /// </summary>
    class TimeLineTwitter : ITimeLine
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// クライアントの本体。
        /// </summary>
        private TwitdonTwitterClient client;

        /// <summary>
        /// ステータスのコントロールリストの本体。
        /// </summary>
        private List<TimeLineStatus> statuses;

        /// <summary>
        /// ホームタイムラインのストリーミング。
        /// </summary>
        private IConnectableObservable<StreamingMessage> streaming;

        /// <summary>
        /// 開始されたストリーミング。
        /// </summary>
        private IDisposable disposable;

        /// <summary>
        /// タイムライン追加待ちのステータス。
        /// </summary>
        private Queue<TwitdonTwitterStatus> fetchedStatuses;

        /// <summary>
        /// タイムラインの種類。
        /// </summary>
        private Define.TwitterTimeLineType type;

        #endregion

        #region プロパティ

        /// <summary>
        /// タイムラインを表示するパネルです。
        /// </summary>
        public Panel Panel { get; set; }

        /// <summary>
        /// タイムラインに保存されているステータスです。
        /// </summary>
        /// <param name="i">古いものから順になっているインデックス。</param>
        /// <returns>ステータスのコントロール。</returns>
        public TimeLineStatus this[int i]
        {
            get
            {
                return statuses[i];
            }
        }

        /// <summary>
        /// タイムラインに保存されているステータスコントロールの数です。
        /// </summary>
        public int Count
        {
            get
            {
                return statuses.Count;
            }
        }

        /// <summary>
        /// タイムラインに紐付けられている user@pawoo.net のようなアカウント名です。
        /// </summary>
        public string AccountName
        {
            get { return client.AccountName; }
        }

        /// <summary>
        /// タイムライン枠に表示するタイムライン名です。
        /// </summary>
        public string TimeLineName { get; private set; }

        /// <summary>
        /// Subscribe メソッドで、ストリーミングで StatusMessage を得た時の動作を設定します。
        /// </summary>
        public IObservable<StatusMessage> OnGetStatusMessage { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="client">Twitter クライアント。</param>
        /// <param name="type">タイムラインの種類。</param>
        public TimeLineTwitter(TwitdonTwitterClient client, Define.TwitterTimeLineType type)
        {
            this.client = client;
            this.type = type;
            var name = type == Define.TwitterTimeLineType.Home ? "" : "Undefined";
            TimeLineName = $"{name}{client.AccountName}";
            streaming = client.Streaming;
            OnGetStatusMessage = streaming.OfType<StatusMessage>();
            statuses = new List<TimeLineStatus>(Define.StatusesCapacity);
            fetchedStatuses = new Queue<TwitdonTwitterStatus>();
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// 最新のタイムラインで初期化します。
        /// </summary>
        public async Task Initialize()
        {
            var response = type == Define.TwitterTimeLineType.Home ?
                await client.GetHomeTimeline(limit: 50) : new List<TwitdonTwitterStatus>();
            foreach (var r in response)
            {
                AddStatus(r);
            }
        }

        /// <summary>
        /// タイムラインにステータスコントロールを追加します。
        /// 実際には、画面のちらつきを抑えるために一定時間ごとにバッファされたステータスを
        /// 一度に更新しています。
        /// </summary>
        /// <param name="status">追加するステータス。</param>
        public void AddStatus(IStatus status)
        {
            fetchedStatuses.Enqueue(status as TwitdonTwitterStatus);
        }

        /// <summary>
        /// タイムライン追加待ちのステータスがあれば追加してコントロールを更新します。
        /// </summary>
        public void Update()
        {
            // 例外処理
            if (fetchedStatuses.Count == 0)
            {
                return;
            }
            if (Panel == null)
            {
                logger.Error("タイムライン枠が未割り当てです。");
                Utilities.ShowError("タイムライン枠が未割り当てです。");
                return;
            }

            // キャパシティオーバーしている分は取得データを捨てる
            while (fetchedStatuses.Count > Define.StatusesCapacity)
            {
                fetchedStatuses.Dequeue();
            }
            int deleteNum = statuses.Count + fetchedStatuses.Count - Define.StatusesCapacity;
            for (int i = 0; i < deleteNum; i++)
            {
                Panel.Controls.RemoveAt(0);
                statuses.RemoveAt(0);
            }

            // ステータスをタイムラインに追加
            while (fetchedStatuses.Count > 0)
            {
                var status = fetchedStatuses.Dequeue();
                var st = new TimeLineStatus(status);
                st.Size = new Size(Panel.ClientSize.Width, st.ClientSize.Height);
                st.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                statuses.Add(st);
                Panel.Controls.Add(st);
            }
        }

        /// <summary>
        /// ストリーミングを開始します。
        /// このメソッドを実行する前にイベントを登録してください。
        /// </summary>
        public void Start()
        {
            try
            {
                disposable = streaming.Connect();
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"ストリーミングの取得に失敗しました。インターネット接続を確認してください。{e.Message}");
                Utilities.ShowError($"ストリーミングの取得に失敗しました。\nインターネット接続を確認してください。");
            }
        }

        /// <summary>
        /// ストリーミングを終了します。
        /// </summary>
        public void Stop()
        {
            disposable?.Dispose();
            disposable = null;
        }

        #endregion
    }
}
