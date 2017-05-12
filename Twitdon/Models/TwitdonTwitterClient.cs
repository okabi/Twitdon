using CoreTweet;
using log4net;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Twitter クライアントです。
    /// </summary>
    class TwitdonTwitterClient// : IClient
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ユーザのスクリーンネームかメールアドレス。
        /// </summary>
        private readonly string screenNameOrEMail;

        /// <summary>
        /// ユーザのパスワード。
        /// </summary>
        private readonly string password;

        /// <summary>
        /// PINコード。
        /// </summary>
        private string pin;

        /// <summary>
        /// クライアントの本体。
        /// </summary>
        private Tokens client;

        #endregion

        #region プロパティ

        /// <summary>
        /// 紐付けられているユーザーの @user@Twitter のようなアカウント名。
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// 紐付けられているユーザーのアイコンの URL。
        /// </summary>
        public string Icon { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// Twitter のクライアント情報を登録します。この後 CreateClient を呼んでください。
        /// </summary>
        /// <param name="screenNameOrEMail">ユーザのスクリーンネームかメールアドレス。</param>
        /// <param name="password">ユーザのパスワード。</param>
        public TwitdonTwitterClient(string screenNameOrEMail, string password)
        {
            this.screenNameOrEMail = screenNameOrEMail;
            this.password = password;
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// 登録された情報でクライアントを作成して返します。エラー発生時は null を返します。
        /// </summary>
        /// <param name="showError">エラー発生時にメッセージボックスを出力するか。</param>
        /// <returns>作成したクライアント。エラー発生時は null を返します。</returns>
        public async Task<Tokens> CreateClient(bool showError)
        {
            // アプリケーションの登録
            OAuth.OAuthSession session;
            try
            {
                session = await OAuth.AuthorizeAsync(SecretDefine.TwitterConsumerKey, SecretDefine.TwitterConsumerSecret);
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"Twitter: サーバ接続に失敗 - {e.Message}");
                if (showError)
                {
                    Utilities.ShowError($"Twitter に接続できません。");
                }
                return null;
            }

            // アクセストークンを取得するために認証ページに裏側で遷移
            var webContext = new WebBrowser();
            try
            {
                webContext.Navigate(session.AuthorizeUri);
                while (webContext.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                var all = webContext.Document.All;
                all.GetElementsByName("session[username_or_email]")[0].InnerText = screenNameOrEMail;
                all.GetElementsByName("session[password]")[0].InnerText = password;
                webContext.DocumentCompleted += webBrowser_DocumentCompleted;
                webContext.Document.GetElementById("allow").InvokeMember("click");

                // PIN コードの取得(実際の処理は webBrowser_DocumentCompleted で行われる)
                pin = "";
                DateTime start = DateTime.Now;
                while (pin.Length == 0)
                {
                    Application.DoEvents();
                    if ((DateTime.Now - start).TotalSeconds > 5)
                    {
                        // 5秒経っても接続できなければタイムアウト
                        throw new Exception("PIN コードの取得でタイムアウトしました。");
                    }
                }

                // アクセストークンの取得
                client = await session.GetTokensAsync(pin);
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"Twitter: アカウントへの接続に失敗 - {e.Message}");
                if (showError)
                {
                    Utilities.ShowError($"アカウントに接続できません。\nユーザ名(メールアドレス)・パスワードを確認してください。");
                }
                return null;
            }

            // 各種情報の取得
            var user = await client.Users.ShowAsync(client.UserId);
            AccountName = $"{user.ScreenName}@Twitter";
            Icon = user.ProfileImageUrl;
            // テスト
            await client.Statuses.UpdateAsync(status => "てすと");

            return client;
        }

        #endregion

        #region イベントハンドラ

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var mc = Regex.Matches((sender as WebBrowser).DocumentText, @"<CODE>(.*?)</CODE>");
            foreach (Match m in mc)
            {
                pin = m.Groups[1].Value;
            }
        }

        #endregion
    }
}
