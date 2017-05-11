using log4net;
using Mastonet;
using Mastonet.Entities;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Mastodon クライアントです。
    /// </summary>
    class TwitdonMastodonClient : IClient
    {
        #region フィールド

        /// <summary>
        /// ロガーオブジェクト。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// インスタンスのドメイン。
        /// </summary>
        private readonly string instance;

        /// <summary>
        /// ユーザのメールアドレス。
        /// </summary>
        private readonly string email;

        /// <summary>
        /// ユーザのパスワード。
        /// </summary>
        private readonly string password;

        /// <summary>
        /// クライアントの本体。
        /// </summary>
        private MastodonClient client;

        #endregion

        #region プロパティ

        /// <summary>
        /// 紐付けられているユーザーの @user@pawoo.net のようなアカウント名。
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// 紐付けられているユーザーのアイコンの URL。
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// ユーザーストリーミング。
        /// </summary>
        public TimelineStreaming UserStreaming
        {
            get { return client.GetUserStreaming(); }
        }

        /// <summary>
        /// 連合ストリーミング。
        /// </summary>
        public TimelineStreaming PublicStreaming
        {
            get { return client.GetPublicStreaming(); }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// Mastodon のトゥート用クライアント情報を登録します。この後 CreateClient を呼んでください。
        /// </summary>
        /// <param name="instance">インスタンスのドメイン。</param>
        /// <param name="email">ユーザのメールアドレス。</param>
        /// <param name="password">ユーザのパスワード。</param>
        public TwitdonMastodonClient(string instance, string email, string password)
        {
            this.instance = instance;
            this.email = email;
            this.password = password;
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// 登録された情報でクライアントを作成して返します。エラー発生時は null を返します。
        /// </summary>
        /// <param name="showError">エラー発生時にメッセージボックスを出力するか。</param>
        /// <returns>作成したクライアント。エラー発生時は null を返します。</returns>
        public async Task<MastodonClient> CreateClient(bool showError)
        {
            // Mastodon Instance へのアプリケーションの登録
            AuthenticationClient authClient;
            AppRegistration appRegistration;
            try
            {
                authClient = new AuthenticationClient(instance);
                appRegistration = await authClient.CreateApp(Assembly.GetExecutingAssembly().GetName().Name, Scope.Read | Scope.Write | Scope.Follow);
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{instance}: サーバ接続に失敗 - {e.Message}");
                if (showError)
                {
                    Utilities.ShowError($"{instance} に接続できません。\nドメイン名を確認してください。");
                }
                return null;
            }

            // アクセストークンの取得
            Auth auth;
            try
            {
                auth = await authClient.ConnectWithPassword(email, password);
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{instance}: アカウントへの接続に失敗 - {e.Message}");
                if (showError)
                {
                    Utilities.ShowError("アカウントに接続できません。\nメールアドレス・パスワードを確認してください。");
                }
                return null;
            }

            // クライアントを作成
            client = new MastodonClient(appRegistration, auth);
            var user = await client.GetCurrentUser();
            AccountName = $"{user.UserName}@{instance}";
            Icon = user.AvatarUrl;
            return client;
        }

        /// <summary>
        /// トゥートします。
        /// </summary>
        /// <param name="status">トゥートする内容。</param>
        /// <returns>トゥート内容。</returns>
        public async Task<IStatus> PostStatus(string status)
        {
            try
            {
                return new TwitdonMastodonStatus(await client.PostStatus(status, Visibility.Public));
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{client.Instance}: トゥートに失敗 - {status} - {e.Message}");
                Utilities.ShowError("トゥートに失敗しました。");
            }
            return null;
        }

        /// <summary>
        /// ツイート/トゥートをお気に入り登録します。
        /// </summary>
        /// <param name="statusId">お気に入り登録するトゥートのID。</param>
        /// <returns>トゥート内容。</returns>
        public async Task<IStatus> Favourite(int statusId)
        {
            try
            {
                return new TwitdonMastodonStatus(await client.Favourite(statusId));
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{client.Instance}: お気に入り登録に失敗 - {e.Message}");
                Utilities.ShowError("お気に入り登録に失敗しました。");
            }
            return null;
        }

        /// <summary>
        /// ツイート/トゥートをお気に入り解除します。
        /// </summary>
        /// <param name="statusId">お気に入り登録するトゥートのID。</param>
        /// <returns>トゥート内容。</returns>
        public async Task<IStatus> Unfavourite(int statusId)
        {
            try
            {
                return new TwitdonMastodonStatus(await client.Unfavourite(statusId));
            }
            catch (Exception e)
            {
                logger.ErrorFormat($"{client.Instance}: お気に入り解除に失敗 - {e.Message}");
                Utilities.ShowError("お気に入り解除に失敗しました。");
            }
            return null;
        }

        #endregion
    }
}
