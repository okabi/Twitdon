using System.Windows.Forms;

namespace Twitdon
{
    /// <summary>
    /// 便利関数群です。
    /// </summary>
    static class Utilities
    {
        /// <summary>
        /// メッセージボックスでエラーを表示します。
        /// </summary>
        /// <param name="message">表示するエラー内容。</param>
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
