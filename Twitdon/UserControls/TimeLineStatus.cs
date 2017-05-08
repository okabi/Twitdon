using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Twitdon.Interfaces;
using System.Linq;

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
            var content = Status.Content;
            content = Regex.Replace(content, @"<br>", "\n");
            content = Regex.Replace(content, @"<br />", "\n");
            content = Regex.Replace(content, "</?span(\".*?\"|'.*?'|[^'\"])*?>", "");
            content = Regex.Replace(content, "</?p(\".*?\"|'.*?'|[^'\"])*?>", "");
            content = WebUtility.HtmlDecode(content); // HTMLインジェクションの可能性あり
            var regex = new Regex("<a href=\"(?<url>.*?)\".*?>(?<text>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Match(content);
            var indexList = new List<KeyValuePair<int, int>>();
            var urlList = new List<string>();
            while (match.Success)
            {
                var url = match.Groups["url"].Value;
                var text = match.Groups["text"].Value;
                indexList.Add(new KeyValuePair<int, int>(match.Index, text.Length));
                urlList.Add(url);
                content = regex.Replace(content, text, 1);
                match = regex.Match(content);
            }
            labelContent.Text = content + " ";
            labelContent.Links.Clear();
            try
            {
                for (int i = 0; i < indexList.Count; i++)
                {
                    labelContent.Links.Add(indexList[i].Key, indexList[i].Value, urlList[i]);
                }
            }
            catch
            {
                MessageBox.Show($"{content}\n\n{string.Join("\n", indexList.Select((x, _) => $"({x.Key}, {x.Value})"))}");
            }
            labelUser.Text = $"{Status.UserName} @{Status.AccountName}";
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
