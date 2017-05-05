using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twitdon
{
    /// <summary>
    /// 非同期処理の進捗状況を表示するフォームです。
    /// </summary>
    public partial class ProgressForm : Form
    {
        #region フィールド

        /// <summary>タスク中断トークンのソース。</summary>
        CancellationTokenSource tokenSource;

        /// <summary>タスク中断トークン。</summary>
        CancellationToken token;

        /// <summary>タスクが正常終了したか。</summary>
        bool __finished;

        #endregion

        #region プロパティ

        /// <summary>
        /// 非同期に実行するタスク。
        /// ProgressForm で進捗状況を表示するフォームを指定する。
        /// CancellationToken を監視することで実行がキャンセルされたか分かる。
        /// </summary>
        public Action<ProgressForm, CancellationToken> AsyncTask { get; set; }
        
        /// <summary>
        /// プログレスバーの値。
        /// </summary>
        public int Progress
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                progressBar.Value = value;
                labelPercentage.Text = value.ToString() + @"%";
            }
        }

        /// <summary>
        /// 現在の作業を表示するメッセージ。
        /// </summary>
        public string Message
        {
            get
            {
                return labelText.Text;
            }
            set
            {
                labelText.Text = value;
            }
        }

        /// <summary>
        /// タスクが正常終了したかどうか。
        /// </summary>
        public bool Finished
        {
            get
            {
                return __finished;
            }
            set
            {
                __finished = value;
                if (value)
                {
                    Close();
                }
            }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            Finished = false;
        }

        #endregion

        #region private メソッド

        /// <summary>
        /// 非同期タスクを実行します。
        /// このメソッドが実行される前に、AsyncTask を定義してください。
        /// </summary>
        private async void Run()
        {
            if (AsyncTask == null)
            {
                MessageBox.Show("AsyncTask が指定されていません。\nバグなので報告をお願いします。", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            await Task.Run(() => AsyncTask(this, token), token);
            Close();
        }

        #endregion

        #region イベントハンドラ

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            Run();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            Close();
        }

        #endregion
    }
}
