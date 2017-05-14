namespace Twitdon
{
    /// <summary>
    /// 定数情報をまとめたクラスです。
    /// </summary>
    public static class Define
    {
        /// <summary>
        /// 1つのタイムラインに保持されるステータスの最大数です。
        /// </summary>
        public const int StatusesCapacity = 100;

        /// <summary>
        /// Mastodon タイムラインの種類です。
        /// </summary>
        public enum MastodonTimeLineType
        {
            /// <summary>ホームタイムライン。</summary>
            Home,
            /// <summary>連合タイムライン。</summary>
            Public,
        }

        /// <summary>
        /// Twitter タイムラインの種類です。
        /// </summary>
        public enum TwitterTimeLineType
        {
            /// <summary>ホームタイムライン。</summary>
            Home,
        }
    }
}
