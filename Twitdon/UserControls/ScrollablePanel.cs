using System.Windows.Forms;

namespace Twitdon
{
    /// <summary>
    /// タイムラインに置く、描画時のちらつきを防止したパネルです。
    /// </summary>
    public partial class ScrollablePanel : Panel
    {
        public ScrollablePanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.Opaque, true);
        }
    }
}
