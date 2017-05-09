using System;
using System.Drawing;
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
        /// タイムラインの本体です。
        /// </summary>
        private readonly ITimeLine timeline;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="timeline">このインスタンスに紐付けるタイムライン。</param>
        public TimeLineFrame(ITimeLine timeline)
        {
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, true);
            panel.MouseWheel += Panel_MouseWheel;

            // タイムラインにこの枠を紐付ける
            this.timeline = timeline;
            timeline.Panel = panel;
            labelTimeLineName.Text = timeline.TimeLineName;

            // ストリーミングイベントの設定、ストリーミングの開始
            if (timeline is TimeLineMastodon)
            {
                var tl = timeline as TimeLineMastodon;
                tl.AddOnUpdate((_, e) =>
                {
                    tl.AddStatus(new TwitdonMastodonStatus(e.Status));
                });
                tl.Start();
            }

            // タイマーイベントの追加
            var timer = new Timer.Timer();
            timer.Elapsed += new Timer.ElapsedEventHandler(Timer_Update);
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Enabled = true;

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

        #endregion

    }
}
