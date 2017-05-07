using log4net;
using Mastonet;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
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
        /// Mastodon クライアントのリストです。
        /// </summary>
        private List<TwitdonMastodonClient> mastodonClients;

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
            if (Settings.Default.MastodonDomains == null)
            {
                Settings.Default.MastodonDomains = new StringCollection();
                Settings.Default.MastodonEMails = new StringCollection();
                Settings.Default.MastodonPasswords = new StringCollection();
            }
        }

        /// <summary>
        /// 登録されている情報から全てのクライアントを作成します。
        /// </summary>
        private async Task CreateAllClients()
        {
            mastodonClients = new List<TwitdonMastodonClient>();
            for (int i = 0; i < Settings.Default.MastodonDomains.Count; i++)
            {
                var client = new TwitdonMastodonClient(Settings.Default.MastodonDomains[i], Settings.Default.MastodonEMails[i], Settings.Default.MastodonPasswords[i]);
                // TODO: どのアカウントにも接続できなかった時の処理(インターネット接続など)
                // TODO: アカウントに接続できなかった時の個別処理
                if ((await client.CreateClient(false)) != null)
                {
                    mastodonClients.Add(client);
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
            //Controls.Add(tlf);
        }

        /// <summary>
        /// 指定した Mastodon タイムラインをコントロールに追加します。
        /// </summary>
        /// <param name="client">Mastodon クライアント。</param>
        /// <param name="streaming">ストリーミング。</param>
        /// <param name="name">タイムライン名。</param>
        private void AddTimeLineMastodon(TwitdonMastodonClient client, TimelineStreaming streaming, string name)
        {
            AddTimeLine(new TimeLineMastodon(client, streaming, name));
        }

        #endregion

        #region イベントハンドラ

        private async void MainForm_Load(object sender, System.EventArgs e)
        {
            Initialize();

            // アカウント未登録時は登録フォームを表示する
            if (Settings.Default.MastodonDomains.Count == 0)
            {
                using (var f = new RegisterForm())
                {
                    f.ShowDialog(this);
                }
            }

            // 全クライアントを作成
            await CreateAllClients();

            // (テスト用)ホーム・Public タイムラインを追加。
            foreach (var client in mastodonClients)
            {
                AddTimeLineMastodon(client, client.UserStreaming, "");
                AddTimeLineMastodon(client, client.PublicStreaming, "Public  ");
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
                mastodonClients.Add(client as TwitdonMastodonClient);
            }
        }

        #endregion
    }
}
