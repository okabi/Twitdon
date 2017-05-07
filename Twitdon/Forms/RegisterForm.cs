using log4net;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitdon.Interfaces;
using Twitdon.Models;
using Twitdon.Properties;

namespace Twitdon
{
    /// <summary>
    /// アカウント情報を登録するフォームです。
    /// </summary>
    public partial class RegisterForm : Form
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region プロパティ

        /// <summary>
        /// 登録するアカウントが Twitter の場合 true を返します。
        /// </summary>
        public bool IsTwitter
        {
            get
            {
                return radioButtonTwitter.Checked;
            }
            set
            {
                radioButtonTwitter.Checked = value;
                IsMastodon = !value;
            }
        }

        /// <summary>
        /// 登録するアカウントが Mastodon の場合 true を返します。
        /// </summary>
        public bool IsMastodon
        {
            get
            {
                return radioButtonMastodon.Checked;
            }
            set
            {
                radioButtonMastodon.Checked = value;
                IsTwitter = !value;
            }
        }

        /// <summary>
        /// 登録する Mastodon インスタンスのドメインです。
        /// </summary>
        public string MastodonDomain
        {
            get
            {
                return textBoxMastodonDomain.Text;
            }
            set
            {
                textBoxMastodonDomain.Text = value;
            }
        }

        /// <summary>
        /// 登録する Mastodon インスタンスのメールアドレスです。
        /// </summary>
        public string MastodonEMail
        {
            get
            {
                return textBoxMastodonEMail.Text;
            }
            set
            {
                textBoxMastodonEMail.Text = value;
            }
        }

        /// <summary>
        /// 登録する Mastodon インスタンスのパスワードです。
        /// </summary>
        public string MastodonPassword
        {
            get
            {
                return textBoxMastodonPassword.Text;
            }
            set
            {
                textBoxMastodonPassword.Text = value;
            }
        }

        /// <summary>
        /// 設定されたクライアント。null なら未設定か設定に失敗しています。
        /// </summary>
        public IClient Client { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public RegisterForm()
        {
            InitializeComponent();
            ChangePanel();
        }

        #endregion

        #region private メソッド

        /// <summary>
        /// 登録サービスの選択状況に応じてパネルを切り替えます。
        /// </summary>
        private void ChangePanel()
        {
            panelTwitter.Visible = radioButtonTwitter.Checked;
            panelMastodon.Visible = radioButtonMastodon.Checked;
        }

        /// <summary>
        /// フォームの内容から Mastodon アクセストークンを取得し、クライアントを作成して Close します。
        /// </summary>
        private async Task RegisterMastodon()
        {
            // 既に登録されている内容ならエラー
            if (Settings.Default.MastodonDomains.Contains(MastodonDomain) && Settings.Default.MastodonEMails.Contains(MastodonEMail))
            {
                logger.ErrorFormat($"既に登録されているアカウントです。");
                Utilities.ShowError($"既に登録されているアカウントです。");
                return;
            }

            // アカウントに接続
            var client = new TwitdonMastodonClient(MastodonDomain, MastodonEMail, MastodonPassword);
            var result = await client.CreateClient(true);
            if (result == null)
            {
                return;
            }

            // アカウント情報を保存して終了
            Settings.Default.MastodonDomains.Add(MastodonDomain);
            Settings.Default.MastodonEMails.Add(MastodonEMail);
            Settings.Default.MastodonPasswords.Add(MastodonPassword);
            Client = client;
            Invoke((MethodInvoker)(() => Close()));
        }

        #endregion

        #region イベントハンドラ

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            MastodonDomain = Settings.Default.MastodonDomain;
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.MastodonDomain = MastodonDomain;
            Settings.Default.Save();
        }

        private void radioButtonTwitter_CheckedChanged(object sender, EventArgs e)
        {
            ChangePanel();
        }

        private async void buttonRegister_Click(object sender, EventArgs e)
        {
            await RegisterMastodon();
        }

        #endregion
    }
}
