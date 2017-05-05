using System.Windows.Forms;

namespace Twitdon
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region イベントハンドラ

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // アカウント未登録時は登録フォームを表示する
            /*
            using (var f = new RegisterForm())
            {
                f.ShowDialog(this);
            }
            */
            Controls.Add(new TimeLineFrame());
            Controls.Add(new TimeLineFrame());
        }

        #endregion
    }
}
