//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.IO;

//namespace ICP.WF.UI.Common
//{
//    public partial class TableLayoutPrintControl : UserControl
//    {
//        public TableLayoutPrintControl()
//        {
//            InitializeComponent();
//        }


//        TableLayoutPanel _tableLayoutPanel;
//        string _title;
//        string _key;
//        int _printWidth;
//        bool _readOnly = false;
//        public TableLayoutPrintControl(string key, TableLayoutPanel tableLayoutPanel, string title, int printWidth,bool readOnly)
//        {
//            InitializeComponent();

//            _tableLayoutPanel = tableLayoutPanel;
//            _printWidth = printWidth;
//            _title = title;
//            _key = key;
//            _readOnly = readOnly;
//        }

//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);


//            if (_tableLayoutPanel != null)
//            {
//                HtmlPageBuilder bp = new HtmlPageBuilder(_tableLayoutPanel, _title, _printWidth);
//                string workflowprintdirectory = Application.StartupPath + @"/WorkFlowPrint";
//                if (Directory.Exists(workflowprintdirectory) == false)
//                {
//                    Directory.CreateDirectory(workflowprintdirectory);
//                }

//                DirectoryInfo di = new DirectoryInfo(workflowprintdirectory);
//                FileInfo[] fs = di.GetFiles("*.html");
//                foreach (FileInfo f in fs)
//                {
//                    if (f.CreationTime.Date < DateTime.Now.Date)
//                    {
//                        f.Delete();
//                    }
//                }

//                string fl = workflowprintdirectory + @"/" + _key+".html";
//                bp.SaveAsHtml(fl);
//                this.webBrowserControl.Navigate(fl);
//            }

//            if (_readOnly)
//            {
//                barPrint.Enabled = false;
//                barPrintview.Enabled = false;
//                barPrintSet.Enabled = false;
//            }
//        }

       
//        private void barPrintview_Click(object sender, EventArgs e)
//        {
//            webBrowserControl.ShowPrintPreviewDialog();
//        }

//        private void barPrintSet_Click(object sender, EventArgs e)
//        {
//            webBrowserControl.ShowPageSetupDialog();
//        }

//        private void barPrint_Click(object sender, EventArgs e)
//        {
//            webBrowserControl.ShowPrintDialog();
//           // this.FindForm().Close();
//        }

//        private void barClose_Click(object sender, EventArgs e)
//        {
//            this.FindForm().Close();
//        }
//    }
//}
