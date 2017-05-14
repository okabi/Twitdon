using log4net;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitdon.Interfaces;
using Twitdon.Models;
using Twitdon.Properties;

namespace Twitdon
{
    public partial class MainForm : Form
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// タイムラインのリストです。
        /// </summary>
        private List<TimeLineFrame> timelines;

        /// <summary>
        /// クライアントのリストです。
        /// </summary>
        private List<IClient> clients;

        /// <summary>
        /// ユーザがテキストボックスに何も入力していない場合 true になります。
        /// </summary>
        private bool textboxBlank;

        #endregion

        #region コンストラクタ

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region private メソッド

        /// <summary>
        /// 初期化します。
        /// </summary>
        private void Initialize()
        {
            timelines = new List<TimeLineFrame>();
            Settings.Default.MastodonDomains = Settings.Default.MastodonDomains ?? new StringCollection();
            Settings.Default.MastodonEMails = Settings.Default.MastodonEMails ?? new StringCollection();
            Settings.Default.MastodonPasswords = Settings.Default.MastodonPasswords ?? new StringCollection();
            Settings.Default.TwitterAccessTokens = Settings.Default.TwitterAccessTokens ?? new StringCollection();
            Settings.Default.TwitterAccessTokenSecrets = Settings.Default.TwitterAccessTokenSecrets ?? new StringCollection();
            ActiveControl = buttonPost;
        }

        /// <summary>
        /// 登録されている情報から全てのクライアントを作成します。
        /// </summary>
        private async Task CreateAllClients()
        {
            clients = new List<IClient>();
            for (int i = 0; i < Settings.Default.TwitterAccessTokens.Count; i++)
            {
                var client = new TwitdonTwitterClient(i);
                // TODO: どのアカウントにも接続できなかった時の処理(インターネット接続など)
                // TODO: アカウントに接続できなかった時の個別処理
                if ((await client.CreateClient(false)) != null)
                {
                    clients.Add(client);
                }
            }
            for (int i = 0; i < Settings.Default.MastodonDomains.Count; i++)
            {
                var client = new TwitdonMastodonClient(Settings.Default.MastodonDomains[i], Settings.Default.MastodonEMails[i], Settings.Default.MastodonPasswords[i], i);
                // TODO: どのアカウントにも接続できなかった時の処理(インターネット接続など)
                // TODO: アカウントに接続できなかった時の個別処理
                if ((await client.CreateClient(false)) != null)
                {
                    clients.Add(client);
                }
            }
        }

        /// <summary>
        /// クライアントの取得状況に応じてメニューを更新します。
        /// このメソッドを呼ぶ前に CreateAllClients を実行してください。
        /// </summary>
        private void UpdateMenuStrip()
        {
            timelinesToolStripMenuItem.DropDownItems.Clear();
            deleteToolStripMenuItem.DropDownItems.Clear();
            foreach (var client in clients)
            {
                var item = new ToolStripMenuItem(client.AccountName);
                if (client is TwitdonTwitterClient)
                {
                    item.DropDownItems.Add(new ToolStripMenuItem("ホーム"));
                    item.DropDownItems.Add(new ToolStripMenuItem("通知"));
                    item.DropDownItems.Add(new ToolStripMenuItem("リスト"));
                }
                else if (client is TwitdonMastodonClient)
                {
                    item.DropDownItems.Add(new ToolStripMenuItem("ホーム"));
                    item.DropDownItems.Add(new ToolStripMenuItem("通知"));
                    item.DropDownItems.Add(new ToolStripMenuItem("連合"));
                }
                timelinesToolStripMenuItem.DropDownItems.Add(item);
                deleteToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(client.AccountName, null, accountDelete_Click));
            }
        }

        /// <summary>
        /// 指定したタイムラインをコントロールに追加します。
        /// </summary>
        /// <param name="timeline">追加するタイムライン。</param>
        private async Task AddTimeLine(ITimeLine timeline)
        {
            Utilities.BeginUpdate(this);
            // 枠の大きさを調整
            if (timelines.Count > 0)
            {
                tableLayoutPanel.ColumnCount++;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
                for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
                {
                    tableLayoutPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanel.ColumnCount);
                }
            }

            // タイムラインの追加
            var tlf = new TimeLineFrame(timeline);
            await tlf.StartStreaming();
            timelines.Add(tlf);
            tlf.Size = Size;
            tlf.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            tableLayoutPanel.Controls.Add(tlf, timelines.Count - 1, 0);
            Utilities.EndUpdate(this);
        }

        /// <summary>
        /// 指定したタイムラインをコントロールから削除します。
        /// </summary>
        /// <param name="indexList">削除するタイムラインのインデックスリスト。</param>
        private void RemoveTimeLine(List<int> indexList)
        {
            Utilities.BeginUpdate(this);
            // コントロールから削除
            for (int i = 0; i < indexList.Count; i++)
            {
                timelines[indexList[i]].StopStreaming();
                timelines[indexList[i]].Dispose();
                tableLayoutPanel.ColumnStyles.RemoveAt(indexList[i]);
                timelines.RemoveAt(indexList[i]);
                for (int j = i + 1; j < indexList.Count; j++)
                {
                    indexList[j]--;
                }
            }
            tableLayoutPanel.ColumnCount -= indexList.Count;

            // 枠の大きさを調整
            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanel.ColumnCount);
            }
            Utilities.EndUpdate(this);
        }

        /// <summary>
        /// ステータスを投稿します。
        /// </summary>
        private async Task Post()
        {
            if (textboxBlank || textBoxPost.TextLength == 0)
            {
                return;
            }
            try
            {
                ActiveControl = buttonPost;
                await clients[0].PostStatus(textBoxPost.Text);
                textboxBlank = true;
                textBoxPost.Text = "今なにしてる？";
                textBoxPost.BackColor = Color.Silver;
                textBoxPost.ForeColor = Color.Gray;
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"エラーが発生しました。インターネット接続を確認してください。{e.Message}");
                Utilities.ShowError($"エラーが発生しました。\nインターネット接続を確認してください。");
            }
        }

        #endregion

        #region イベントハンドラ

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Initialize();

            // アカウント未登録時は登録フォームを表示する
            if (Settings.Default.MastodonDomains.Count == 0 && Settings.Default.TwitterAccessTokens.Count == 0)
            {
                using (var f = new RegisterForm())
                {
                    f.ShowDialog(this);
                }
            }

            // 登録されたアカウントが無ければ終了
            if (Settings.Default.MastodonDomains.Count == 0 && Settings.Default.TwitterAccessTokens.Count == 0)
            {
                Application.Exit();
                return;
            }

            try
            {
                // 全クライアントを作成
                await CreateAllClients();

                // メニューを更新
                UpdateMenuStrip();

                foreach (var client in clients)
                {
                    // (テスト用)ユーザーのアイコン画像を取得する
                    textboxBlank = true;
                    pictureBoxUser.ImageLocation = client.Icon;
                    if (client is TwitdonMastodonClient)
                    {
                        // (テスト用)ホーム・Public タイムラインを追加。
                        var c = client as TwitdonMastodonClient;
                        await AddTimeLine(new TimeLineMastodon(c, c.UserStreaming, Define.MastodonTimeLineType.Home));
                        await AddTimeLine(new TimeLineMastodon(c, c.PublicStreaming, Define.MastodonTimeLineType.Public));
                    }
                    else if(client is TwitdonTwitterClient)
                    {
                        var c = client as TwitdonTwitterClient;
                        await AddTimeLine(new TimeLineTwitter(c, Define.TwitterTimeLineType.Home));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat($"エラーが発生しました。インターネット接続を確認してください。{ex.Message}");
                Utilities.ShowError($"エラーが発生しました。\nインターネット接続を確認してください。");
                Application.Exit();
                return;
            }
        }

        private async void 登録RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IClient client;
            using (var f = new RegisterForm())
            {
                f.ShowDialog(this);
                client = f.Client;
                if (client == null)
                {
                    return;
                }
            }

            try
            {
                if (client is TwitdonMastodonClient)
                {
                    // (テスト用)ホーム・Public タイムラインを追加。
                    var c = client as TwitdonMastodonClient;
                    clients.Add(c);
                    await AddTimeLine(new TimeLineMastodon(c, c.UserStreaming, Define.MastodonTimeLineType.Home));
                    await AddTimeLine(new TimeLineMastodon(c, c.PublicStreaming, Define.MastodonTimeLineType.Public));
                }
                else if (client is TwitdonTwitterClient)
                {
                    var c = client as TwitdonTwitterClient;
                    clients.Add(c);
                    await AddTimeLine(new TimeLineTwitter(c, Define.TwitterTimeLineType.Home));
                }

                // (テスト用)ユーザーのアイコン画像を取得する
                textboxBlank = true;
                pictureBoxUser.ImageLocation = client.Icon;

                // メニューを更新
                UpdateMenuStrip();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat($"エラーが発生しました。インターネット接続を確認してください。{ex.Message}");
                Utilities.ShowError($"エラーが発生しました。\nインターネット接続を確認してください。");
                Application.Exit();
                return;
            }
        }

        private void accountDelete_Click(object sender, EventArgs e)
        {
            // 削除するクライアントのインデックスを探す
            var accountName = (sender as ToolStripMenuItem).Text;
            var index = clients.FindIndex(x => x.AccountName == accountName);

            // 保存していた情報を削除
            if (clients[index] is TwitdonTwitterClient)
            {
                for (int i = index + 1; i < clients.Count; i++)
                {
                    if (clients[i] is TwitdonTwitterClient)
                    {
                        clients[i].Index--;
                    }
                }
                Settings.Default.TwitterAccessTokens.RemoveAt(clients[index].Index);
                Settings.Default.TwitterAccessTokenSecrets.RemoveAt(clients[index].Index);
            }
            else if (clients[index] is TwitdonMastodonClient)
            {
                for (int i = index + 1; i < clients.Count; i++)
                {
                    if (clients[i] is TwitdonMastodonClient)
                    {
                        clients[i].Index--;
                    }
                }
                Settings.Default.MastodonDomains.RemoveAt(clients[index].Index);
                Settings.Default.MastodonEMails.RemoveAt(clients[index].Index);
                Settings.Default.MastodonPasswords.RemoveAt(clients[index].Index);
            }
            Settings.Default.Save();

            // タイムライン・クライアントから削除
            RemoveTimeLine(timelines
                .Select((x, i) => new { Index = i, Name = x.AccountName })
                .Where(x => x.Name == accountName)
                .Select(x => x.Index)
                .ToList());
            clients.RemoveAt(index);

            UpdateMenuStrip();
        }

        private void textBoxPost_Enter(object sender, EventArgs e)
        {
            textboxBlank = false;
            textBoxPost.Text = "";
            textBoxPost.BackColor = Color.White;
            textBoxPost.ForeColor = Color.Black;
        }

        private void textBoxPost_Leave(object sender, EventArgs e)
        {
            if (textBoxPost.TextLength == 0)
            {
                textboxBlank = true;
                textBoxPost.Text = "今なにしてる？";
            }
            textBoxPost.BackColor = Color.Silver;
            textBoxPost.ForeColor = Color.Gray;
        }

        private async void textBoxPost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                await Post();
            }
        }

        private async void buttonPost_Click(object sender, EventArgs e)
        {
            await Post();
        }

        #endregion
    }
}
