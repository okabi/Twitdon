using Mastonet.Entities;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Mastodon のトゥートを表すクラスです。
    /// </summary>
    class TwitdonMastodonStatus : IStatus
    {
        #region フィールド

        /// <summary>
        /// トゥートの本体。
        /// </summary>
        private readonly Status status;

        #endregion

        #region プロパティ

        /// <summary>
        /// @user などのユーザ名です。リプライに使われます。
        /// </summary>
        public string ScreenName
        {
            get { return status.Account.UserName; }
        }

        /// <summary>
        /// @user@pawoo.net などのユーザ名です。ステータス表示に使われます。
        /// </summary>
        public string AccountName
        {
            get { return status.Account.AccountName; }
        }

        /// <summary>
        /// 識別に用いられないユーザ表示名です。
        /// </summary>
        public string UserName
        {
            get { return status.Account.DisplayName; }
        }

        /// <summary>
        /// トゥートの文章です。
        /// </summary>
        public string Content
        {
            get { return status.Content; }
        }

        /// <summary>
        /// アイコンの URL です。
        /// </summary>
        public string Icon
        {
            get { return status.Account.AvatarUrl; }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// トゥートを作成します。
        /// </summary>
        /// <param name="status">トゥート。</param>
        public TwitdonMastodonStatus(Status status)
        {
            this.status = status;
        }

        #endregion
    }
}