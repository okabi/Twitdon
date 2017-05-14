using log4net;
using Mastonet;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Mastodon タイムラインを表すクラスです。
    /// </summary>
    class TimeLineMastodon : ITimeLine
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// クライアントの本体。
        /// </summary>
        private TwitdonMastodonClient client;

        /// <summary>
        /// ステータスのコントロールリストの本体。
        /// </summary>
        private List<TimeLineStatus> statuses;

        /// <summary>
        /// タイムラインストリーミング。
        /// </summary>
        private readonly TimelineStreaming streaming;

        /// <summary>
        /// タイムライン追加待ちのステータス。
        /// </summary>
        private Queue<TwitdonMastodonStatus> fetchedStatuses;

        /// <summary>
        /// タイムラインの種類。
        /// </summary>
        private Define.MastodonTimeLineType type;

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

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="client">Mastodon クライアント。</param>
        /// <param name="type">タイムラインの種類。</param>
        public TimeLineMastodon(TwitdonMastodonClient client, Define.MastodonTimeLineType type)
        {
            this.client = client;
            streaming = type == Define.MastodonTimeLineType.Home ? client.UserStreaming : client.PublicStreaming;
            this.type = type;
            var name = $"{Utilities.MastodonTimeLineTypeToString(type)}  ";
            TimeLineName = $"{name}{client.AccountName}";
            statuses = new List<TimeLineStatus>(Define.StatusesCapacity);
            fetchedStatuses = new Queue<TwitdonMastodonStatus>();
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// 最新のタイムラインで初期化します。
        /// </summary>
        public async Task Initialize()
        {
            var response = type == Define.MastodonTimeLineType.Home ? 
                await client.GetHomeTimeline(limit: 50) : await client.GetPublicTimeline(limit: 50);
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
            fetchedStatuses.Enqueue(status as TwitdonMastodonStatus);
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
                Panel.Controls[0].Dispose();
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
                streaming.Start();
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
            streaming.Stop();
        }

        /// <summary>
        /// ストリーミングで新しいステータスを得た時の動作を設定します。
        /// </summary>
        /// <param name="function">設定する動作。</param>
        public void SetOnUpdate(EventHandler<StreamUpdateEventArgs> function)
        {
            streaming.OnUpdate += function;
        }

        /// <summary>
        /// ストリーミングで新しい通知を得た時の動作を設定します。
        /// </summary>
        /// <param name="function">設定する動作。</param>
        public void SetOnNotification(EventHandler<StreamNotificationEventArgs> function)
        {
            streaming.OnNotification += function;
        }

        /// <summary>
        /// ストリーミングで新しい削除通知を得た時の動作を設定します。
        /// </summary>
        /// <param name="function">設定する動作。</param>
        public void SetOnDelete(EventHandler<StreamDeleteEventArgs> function)
        {
            streaming.OnDelete += function;
        }

        #endregion
    }
}
