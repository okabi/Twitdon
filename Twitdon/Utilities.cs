using System.Threading;
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

        /// <summary>
        /// 非同期タスク中でプログレスフォームを更新します。また、キャンセルが押されたかを返します。
        /// </summary>
        /// <param name="form">親フォーム。</param>
        /// <param name="progressForm">進捗状況を表すフォーム。</param>
        /// <param name="token">処理キャンセルを監視するトークン。</param>
        /// <param name="progress">プログレスバーの表示(0~100)。</param>
        /// <param name="message">プログレスフォームに表示する現在の作業メッセージ。</param>
        /// <returns>キャンセルが押されたか。</returns>
        public static bool UpdateProgressForm(Form form, ProgressForm progressForm, CancellationToken token, int progress, string message)
        {
            if (token.IsCancellationRequested)
            {
                // 中断命令が出された
                return true;
            }
            form.Invoke(
                (MethodInvoker)(() =>
                {
                    progressForm.Progress = progress;
                    progressForm.Message = message;
                }));
            return false;
        }

    }
}
