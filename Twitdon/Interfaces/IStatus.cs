namespace Twitdon.Interfaces
{
    /// <summary>
    /// ツイート/トゥートを定義するインターフェースです。
    /// </summary>
    public interface IStatus
    {
        #region プロパティ

        /// <summary>
        /// @user などのユーザ名です。リプライに使われます。
        /// </summary>
        string ScreenName { get; }

        /// <summary>
        /// @user@pawoo.net などのユーザ名です。ステータス表示に使われます。
        /// </summary>
        string AccountName { get; }

        /// <summary>
        /// 識別に用いられないユーザ表示名です。
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// アイコンの URL です。
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// ツイート/トゥートの文章です。
        /// </summary>
        string Content { get; }

        #endregion
    }
}
