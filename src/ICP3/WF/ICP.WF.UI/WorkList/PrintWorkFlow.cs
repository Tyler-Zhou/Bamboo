using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.WF.ServiceInterface.Client;
using System.IO;
using ICP.WF.Controls;

namespace ICP.WF.UI
{
    public partial class PrintWorkFlow : UserControl
    {
        public PrintWorkFlow()
        {
            InitializeComponent();       
        }


 
        public LWBaseForm BaseForm
        {
            get;
            set;
        }
        public string WorkName
        {
            get;
            set;
        }

        public void ShowPrint(LWBaseForm baseForm, string name, string workInfoName)
        {
            this.Height =600;
            this.Width = 800;

            TableLayoutPanel _tableLayoutPanel = baseForm.Controls[0] as TableLayoutPanel;

            if (_tableLayoutPanel != null)
            {
                HtmlPageBuilder bp = new HtmlPageBuilder(_tableLayoutPanel, name, workInfoName, 700,"100%");
                string workflowprintdirectory = DefaultConfigMananger.Default.FlowPrintFolder.Path;
                if (!Directory.Exists(workflowprintdirectory))
                {
                    Directory.CreateDirectory(workflowprintdirectory);
                }

                DirectoryInfo di = new DirectoryInfo(workflowprintdirectory);
                FileInfo[] fs = di.GetFiles("*.html");
                foreach (FileInfo f in fs)
                {
                    if (f.CreationTime.Date < DateTime.Now.Date)
                    {
                        f.Delete();
                    }
                }

                string fl = workflowprintdirectory + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
                bp.SaveAsHtml(fl);


                web.Navigate(fl);
            }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            web.ShowPrintDialog();
        }
    }
}
