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
        /// 登録する Twitter インスタンスのメールアドレスです。
        /// </summary>
        public string TwitterEMail
        {
            get
            {
                return textBoxTwitterEMail.Text;
            }
            set
            {
                textBoxTwitterEMail.Text = value;
            }
        }

        /// <summary>
        /// 登録する Twitter インスタンスのパスワードです。
        /// </summary>
        public string TwitterPassword
        {
            get
            {
                return textBoxTwitterPassword.Text;
            }
            set
            {
                textBoxTwitterPassword.Text = value;
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
            panelTwitter.Visible = IsTwitter;
            panelMastodon.Visible = IsMastodon;
        }

        /// <summary>
        /// コントロールの Enabled を一斉に更新します。
        /// </summary>
        /// <param name="enabled">新しく設定する Enabled の値。</param>
        private void ChangeUIEnabled(bool enabled)
        {
            panelServices.Enabled = enabled;
            panelTwitter.Enabled = enabled;
            panelMastodon.Enabled = enabled;
            buttonRegister.Visible = enabled;
            labelProgress.Visible = !enabled;
            progressBar.Visible = !enabled;
        }

        /// <summary>
        /// フォームの内容から Twitter アクセストークンを取得し、クライアントを作成します。
        /// </summary>
        private async Task RegisterTwitter()
        {
            // TODO: 既に登録されている内容か確認する

            // アカウントに接続
            var client = new TwitdonTwitterClient(TwitterEMail, TwitterPassword);
            var result = await client.CreateClient(true, this, progressBar);
            if (result == null)
            {
                return;
            }

            // アカウント情報を保存
            Settings.Default.TwitterAccessTokens.Add(result.AccessToken);
            Settings.Default.TwitterAccessTokenSecrets.Add(result.AccessTokenSecret);
            Client = client;
        }

        /// <summary>
        /// フォームの内容から Mastodon アクセストークンを取得し、クライアントを作成します。
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
            var result = await client.CreateClient(true, this, progressBar);
            if (result == null)
            {
                return;
            }

            // アカウント情報を保存
            Settings.Default.MastodonDomains.Add(MastodonDomain);
            Settings.Default.MastodonEMails.Add(MastodonEMail);
            Settings.Default.MastodonPasswords.Add(MastodonPassword);
            Client = client;
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
            progressBar.Value = 0;
            Invoke((MethodInvoker)(() => ChangeUIEnabled(false)));
            if (IsTwitter)
            {
                await RegisterTwitter();
            }
            else
            {
                await RegisterMastodon();
            }
            if (Client != null)
            {
                Invoke((MethodInvoker)(() => Close()));
                return;
            }
            Invoke((MethodInvoker)(() => ChangeUIEnabled(true)));
        }

        #endregion
    }
}
