using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using Twitdon.Interfaces;

namespace Twitdon
{
    /// <summary>
    /// タイムライン中のステータスを表すユーザーコントロールです。
    /// </summary>
    public partial class TimeLineStatus : UserControl
    {
        #region プロパティ

        /// <summary>
        /// 紐付けられているステータスです。
        /// </summary>
        public IStatus Status { get; set; }

        #endregion

        #region コンストラクタ

        public TimeLineStatus(IStatus status)
        {
            InitializeComponent();
            DoubleBuffered = true;

            Status = status;
            UpdateUI();
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// status の内容に合わせて表示を更新します。
        /// </summary>
        public void UpdateUI()
        {
            // 情報の紐付け
            labelUser.Text = $"{Status.UserName} @{Status.AccountName}";
            labelContent.Text = Regex.Replace(Status.Content, "<(\".*? \"|'.*?'|[^'\"])*?>", "");
            pictureBoxIcon.ImageLocation = Status.Icon;

            // 枠の高さの調整
            labelContent.MaximumSize = new Size(Size.Width - 60, 0);
            var pictureHeight = pictureBoxIcon.Location.Y + pictureBoxIcon.Size.Height + pictureBoxIcon.Margin.Bottom;
            var contentHeight = labelContent.Location.Y + labelContent.Size.Height + labelContent.Margin.Bottom;
            Size = new Size(Size.Width, pictureHeight > contentHeight ? pictureHeight : contentHeight);
        }

        #endregion
    }
}
