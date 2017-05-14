using log4net;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitdon.Interfaces;
using Twitdon.Models;
using Timer = System.Timers;

namespace Twitdon
{
    /// <summary>
    /// タイムラインを表すユーザーコントロールです。
    /// </summary>
    public partial class TimeLineFrame : UserControl
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// タイムラインの本体です。
        /// </summary>
        private readonly ITimeLine timeline;

        /// <summary>
        /// タイムラインが描画されている親フォーム。
        /// </summary>
        private readonly MainForm owner;

        #endregion

        #region プロパティ

        /// <summary>
        /// タイムラインに紐付けられている user@pawoo.net のようなアカウント名です。
        /// </summary>
        public string AccountName
        {
            get { return timeline.AccountName; }
        }

        /// <summary>
        /// Public user@pawoo.net のような一意のタイムライン名です。
        /// </summary>
        public string TimeLineName
        {
            get { return labelTimeLineName.Text; }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="owner">タイムラインが描画されている親フォーム。</param>
        /// <param name="timeline">このインスタンスに紐付けるタイムライン。</param>
        public TimeLineFrame(MainForm owner, ITimeLine timeline)
        {
            InitializeComponent();
            this.owner = owner;
            panel.MouseWheel += Panel_MouseWheel;

            // タイムラインにこの枠を紐付ける
            this.timeline = timeline;
            timeline.Panel = panel;
            labelTimeLineName.Text = timeline.TimeLineName;

            // ストリーミングイベントの設定、ストリーミングの開始
            if (timeline is TimeLineMastodon)
            {
                var tl = timeline as TimeLineMastodon;
                tl.SetOnUpdate((_, e) =>
                {
                    tl.AddStatus(new TwitdonMastodonStatus(e.Status));
                });
            }
            else if (timeline is TimeLineTwitter)
            {
                var tl = timeline as TimeLineTwitter;
                tl.OnGetStatusMessage.Subscribe(x => tl.AddStatus(new TwitdonTwitterStatus(x.Status)));
            }

            // タイマーイベントの追加
            var timer = new Timer.Timer();
            timer.Elapsed += new Timer.ElapsedEventHandler(Timer_Update);
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// ストリーミングを開始します。
        /// </summary>
        public async Task StartStreaming()
        {
            await timeline.Initialize();
            timeline.Start();
        }

        /// <summary>
        /// ストリーミングを停止します。
        /// </summary>
        public void StopStreaming()
        {
            timeline.Stop();
        }

        #endregion

        #region private メソッド

        /// <summary>
        /// タイムラインの表示を調整します。
        /// </summary>
        private void UpdateUI()
        {
            try
            {
                Invoke(
                    (MethodInvoker)(() =>
                    {
                        Utilities.BeginUpdate(this);
                        timeline.Update();
                        int y = 0;
                        for (int i = timeline.Count - 1; i >= 0; i--)
                        {
                            timeline[i].Location = new Point(0, y);
                            timeline[i].UpdateUI();
                            y += timeline[i].Size.Height;
                        }
                        Utilities.EndUpdate(this);
                    }));
            }
            catch (Exception)
            {
                
            }
        }

        #endregion

        #region イベントハンドラ

        private void panel_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateUI();
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            UpdateUI();
        }

        private void Timer_Update(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            owner.RemoveTimeLine(timeline.TimeLineName);
        }

        #endregion
    }
}
