using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
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
            // Content の HTML タグを処理
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
            var imageList = new List<PictureBox>();
            while (match.Success)
            {
                var url = match.Groups["url"].Value;
                var text = match.Groups["text"].Value;
                if (text.Contains("/media/"))
                {
                    var pb = new PictureBox();
                    pb.ImageLocation = text;
                    pb.Cursor = Cursors.Hand;
                    pb.Tag = text;
                    pb.Size = new Size(128, 128);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Click += pictureBoxIcon_Click;
                    Controls.Add(pb);
                    imageList.Add(pb);
                    content = regex.Replace(content, "", 1);
                }
                else
                {
                    indexList.Add(new KeyValuePair<int, int>(match.Index, text.Length));
                    urlList.Add(url);
                    content = regex.Replace(content, text, 1);
                }
                match = regex.Match(content);
            }
            labelContent.Text = content;
            labelContent.Links.Clear();
            for (int i = 0; i < indexList.Count; i++)
            {
                labelContent.Links.Add(indexList[i].Key, indexList[i].Value, urlList[i]);
            }

            // 情報の紐付け
            labelUser.Text = $"{Status.UserName} @{Status.AccountName}";
            pictureBoxIcon.ImageLocation = Status.Icon;
            pictureBoxIcon.Tag = Status.AccountUrl;

            // 枠の高さの調整
            labelContent.MaximumSize = new Size(Size.Width - 60, 0);
            var pictureHeight = pictureBoxIcon.Location.Y + pictureBoxIcon.Size.Height + pictureBoxIcon.Margin.Bottom;
            var contentHeight = labelContent.Location.Y + labelContent.Size.Height + labelContent.Margin.Bottom;
            foreach(var image in imageList)
            {
                image.Location = new Point(labelContent.Location.X, contentHeight);
                contentHeight += image.Height + image.Margin.Bottom;
            }
            Size = new Size(Size.Width, pictureHeight > contentHeight ? pictureHeight : contentHeight);
        }

        #endregion

        #region イベントハンドラ

        private void labelContent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
            e.Link.Visited = true;
        }

        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {
            Process.Start((sender as PictureBox).Tag as string);
        }

        #endregion
    }
}
