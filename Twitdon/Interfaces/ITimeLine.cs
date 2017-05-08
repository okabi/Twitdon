using System.Windows.Forms;

namespace Twitdon.Interfaces
{
    /// <summary>
    /// タイムラインのモデルを表すインターフェースです。
    /// </summary>
    public interface ITimeLine
    {
        #region プロパティ

        /// <summary>
        /// タイムラインを表示するパネルです。
        /// </summary>
        Panel Panel { get; set; }

        /// <summary>
        /// タイムラインに保存されているステータスです。
        /// </summary>
        /// <param name="i">古いものから順になっているインデックス。</param>
        /// <returns>ステータスのコントロール。</returns>
        TimeLineStatus this[int i] { get; }

        /// <summary>
        /// タイムラインに保存されているステータスの数です。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// タイムライン枠に表示するタイムライン名です。
        /// </summary>
        string TimeLineName { get; }

        #endregion

        #region public メソッド

        /// <summary>
        /// タイムラインにステータスコントロールを追加します。
        /// </summary>
        /// <param name="status">追加するステータス。</param>
        void AddStatus(IStatus status);

        /// <summary>
        /// タイムライン追加待ちのステータスがあれば追加してコントロールを更新します。
        /// </summary>
        void Update();

        /// <summary>
        /// ストリーミングを開始します。
        /// </summary>
        void Start();

        /// <summary>
        /// ストリーミングを終了します。
        /// </summary>
        void Stop();

        #endregion
    }
}
