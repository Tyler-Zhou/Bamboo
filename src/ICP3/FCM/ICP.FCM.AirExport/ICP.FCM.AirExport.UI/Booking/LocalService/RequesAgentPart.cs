using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    public partial class RequesAgentPart : XtraUserControl
    {  
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public RequesAgentPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            Disposed += delegate {
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };

        }

        void SetCnText()
        {
            radioGroup1.Properties.Items[0].Description = "普通代理";
            radioGroup1.Properties.Items[1].Description = "第三方代理";
            radioGroup1.Properties.Items[2].Description = "对收款有特殊要求的代理";
            labRemark.Text = "备注";
            btnOK.Text = "确定(&O)";
            btnCancel.Text = "取消(&C)";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
        }
    }
}
