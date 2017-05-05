using log4net;
using Mastonet;
using Mastonet.Entities;
using System;
using System.Reflection;
using System.Threading;
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
        /// 登録された Twitter / Mastodon のクライアントです。
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
        /// 非同期タスク中でプログレスフォームを更新します。また、キャンセルが押されたかを返します。
        /// </summary>
        /// <param name="form">進捗状況を表すフォーム。</param>
        /// <param name="token">処理キャンセルを監視するトークン。</param>
        /// <param name="progress">プログレスバーの表示(0~100)。</param>
        /// <param name="message">プログレスフォームに表示する現在の作業メッセージ。</param>
        /// <returns>キャンセルが押されたか。</returns>
        private bool UpdateProgressForm(ProgressForm form, CancellationToken token, int progress, string message)
        {
            if (token.IsCancellationRequested)
            {
                // 中断命令が出された
                return true;
            }
            Invoke(
                (MethodInvoker)(() =>
                {
                    form.Progress = progress;
                    form.Message = message;
                }));
            return false;
        }

        private void RegisterTwitter(ProgressForm form, CancellationToken token) { }

        /// <summary>
        /// フォームの内容から Mastodon アクセストークンを取得し、クライアントを作成します。
        /// </summary>
        /// <param name="form">進捗状況を表すフォーム。</param>
        /// <param name="token">処理キャンセルを監視するトークン。</param>
        private async void RegisterMastodon(ProgressForm form, CancellationToken token)
        {
            // Mastodon Instance へのアプリケーションの登録
            if (UpdateProgressForm(form, token, 0, $"{MastodonDomain}: アプリケーション登録中です..."))
            {
                return;
            }
            AuthenticationClient authClient;
            AppRegistration appRegistration;
            try
            {
                authClient = new AuthenticationClient(MastodonDomain);
                appRegistration = await authClient.CreateApp(Assembly.GetExecutingAssembly().GetName().Name, Scope.Read | Scope.Write | Scope.Follow);
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{MastodonDomain}: サーバ接続に失敗 - {e.Message}");
                Utilities.ShowError($"{MastodonDomain} に接続できません。\nドメイン名を確認してください。");
                return;
            }

            // アクセストークンの取得
            if (UpdateProgressForm(form, token, 50, $"{MastodonDomain}: アクセストークン取得中です..."))
            {
                return;
            }
            Auth auth;
            try
            {
                auth = await authClient.ConnectWithPassword(MastodonEMail, MastodonPassword);
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{MastodonDomain}: アカウントへの接続に失敗 - {e.Message}");
                Utilities.ShowError("アカウントに接続できません。\nメールアドレス・パスワードを確認してください。");
                return;
            }

            // クライアントを作成
            Client = new TwitdonMastodonClient(appRegistration, auth);
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

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            using (var f = new ProgressForm())
            {
                f.AsyncTask = RegisterTwitter;
                if (IsMastodon)
                {
                    f.AsyncTask = RegisterMastodon;
                }
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
            }
        }

        #endregion
    }
}
