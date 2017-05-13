using log4net;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.Drawing;
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
            for (int i = 0; i < Settings.Default.MastodonDomains.Count; i++)
            {
                var client = new TwitdonMastodonClient(Settings.Default.MastodonDomains[i], Settings.Default.MastodonEMails[i], Settings.Default.MastodonPasswords[i]);
                // TODO: どのアカウントにも接続できなかった時の処理(インターネット接続など)
                // TODO: アカウントに接続できなかった時の個別処理
                if ((await client.CreateClient(false)) != null)
                {
                    clients.Add(client);
                }
            }
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
        }

        /// <summary>
        /// 指定したタイムラインをコントロールに追加します。
        /// </summary>
        /// <param name="timeline">追加するタイムライン。</param>
        private void AddTimeLine(ITimeLine timeline)
        {
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
            timelines.Add(tlf);
            tlf.Size = Size;
            tlf.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            tableLayoutPanel.Controls.Add(tlf, timelines.Count - 1, 0);
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
                // テスト用
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

        private async void MainForm_Load(object sender, System.EventArgs e)
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

                foreach (var client in clients)
                {
                    // (テスト用)ユーザーのアイコン画像を取得する
                    textboxBlank = true;
                    pictureBoxUser.ImageLocation = client.Icon;
                    if (client is TwitdonMastodonClient)
                    {
                        // (テスト用)ホーム・Public タイムラインを追加。
                        var c = client as TwitdonMastodonClient;
                        AddTimeLine(new TimeLineMastodon(c, c.UserStreaming, ""));
                        AddTimeLine(new TimeLineMastodon(c, c.PublicStreaming, "Public  "));
                    }
                    else if(client is TwitdonTwitterClient)
                    {
                        var c = client as TwitdonTwitterClient;
                        AddTimeLine(new TimeLineTwitter(c, ""));
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

        private void 登録RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Initialize();

            IClient client;
            using (var f = new RegisterForm())
            {
                f.ShowDialog(this);
                client = f.Client;
            }

            // クライアントを作成
            if (client != null)
            {
                clients.Add(client);
            }
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
