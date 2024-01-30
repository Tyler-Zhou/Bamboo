using System.Windows.Forms;

namespace ICP.FCM.OceanExport.UI.BL.HBL
{
    public partial class ImoFlag : Form
    {
        public string VesselName { get; set; }
     
        public ImoFlag()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://shipnumber.com/");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState < WebBrowserReadyState.Complete) return;
          
        }
    }
}
