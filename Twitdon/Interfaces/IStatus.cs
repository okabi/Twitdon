namespace Twitdon.Interfaces
{
    /// <summary>
    /// ツイート/トゥートを定義するインターフェースです。
    /// </summary>
    public interface IStatus
    {
        #region プロパティ

        /// <summary>
        /// ツイート/トゥートの文章です。
        /// </summary>
        string Content { get; }

        #endregion
    }
}
