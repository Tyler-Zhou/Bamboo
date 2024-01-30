using System.Windows.Forms;

namespace ICP.Business.Common.UI.Communication_History
{
    public partial class AmsConfirm : Form
    {
        public AmsConfirm()
        {
            InitializeComponent();
            webAms.Navigate("https://www2.tradetech.net/forms/Home.html");
        }

        private void webAms_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webAms.ReadyState < WebBrowserReadyState.Complete) return;
        }
    }
}
