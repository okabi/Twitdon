using CoreTweet;
using Twitdon.Interfaces;

namespace Twitdon.Models
{
    /// <summary>
    /// Twitter のツイートを表すクラスです。
    /// </summary>
    class TwitdonTwitterStatus : IStatus
    {
        #region フィールド

        /// <summary>
        /// ツイートの本体。
        /// </summary>
        private readonly Status status;

        #endregion

        #region プロパティ

        /// <summary>
        /// @user などのユーザ名です。リプライに使われます。
        /// </summary>
        public string ScreenName
        {
            get { return status.User.ScreenName; }
        }

        /// <summary>
        /// @user@pawoo.net などのユーザ名です。ステータス表示に使われます。
        /// </summary>
        public string AccountName
        {
            get { return $"{status.User.ScreenName}@Twitter"; }
        }

        /// <summary>
        /// 識別に用いられないユーザ表示名です。
        /// </summary>
        public string UserName
        {
            get { return status.User.Name; }
        }

        /// <summary>
        /// アイコンの URL です。
        /// </summary>
        public string Icon
        {
            get { return status.User.ProfileImageUrl; }
        }

        /// <summary>
        /// ステータスを投稿したユーザの URL です。
        /// </summary>
        public string AccountUrl
        {
            get { return $"https://twitter.com/{status.User.ScreenName}"; }
        }

        /// <summary>
        /// トゥートの文章です。
        /// </summary>
        public string Content
        {
            get { return status.Text; }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// ツイートを作成します。
        /// </summary>
        /// <param name="status">ツイート。</param>
        public TwitdonTwitterStatus(Status status)
        {
            this.status = status;
        }

        #endregion
    }
}
