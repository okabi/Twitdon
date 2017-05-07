using System.Drawing;
using System.Windows.Forms;
using Twitdon.Interfaces;
using Twitdon.Models;

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
            panel.MouseWheel += Panel_MouseWheel; ;

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
                    AddStatus(new TwitdonMastodonStatus(e.Status));
                });
                tl.Start();
            }
        }

        #endregion

        #region private メソッド

        /// <summary>
        /// タイムラインにステータスを追加して反映します。
        /// </summary>
        /// <param name="status">追加するステータス。</param>
        private void AddStatus(IStatus status)
        {
            // ステータスを追加
            timeline.AddStatus(status);

            // 各コントロールの描画位置を調整
            UpdateUI();
        }

        /// <summary>
        /// タイムラインの表示を調整します。
        /// </summary>
        private void UpdateUI()
        {
            int y = 0;
            for (int i = timeline.Count - 1; i >= 0; i--)
            {
                timeline[i].Location = new Point(0, y);
                timeline[i].UpdateUI();
                y += timeline[i].Size.Height;
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

        #endregion

    }
}
