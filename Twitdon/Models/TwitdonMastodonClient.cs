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
        /// Mastodon クライアントの本体。
        /// </summary>
        private readonly MastodonClient client;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// Mastodon のトゥート用クライアントを登録します。
        /// </summary>
        /// <param name="appRegistration">アプリの登録情報。</param>
        /// <param name="auth">ユーザの登録情報。</param>
        public TwitdonMastodonClient(AppRegistration appRegistration, Auth auth)
        {
            client = new MastodonClient(appRegistration, auth);
            client.GetPublicStreaming();
        }

        #endregion

        #region public メソッド

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
