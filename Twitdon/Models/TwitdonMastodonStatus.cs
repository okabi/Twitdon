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
        /// トゥートの文章です。
        /// </summary>
        public string Content
        {
            get { return status.Content; }
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