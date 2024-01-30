using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    public partial class RequesAgentPart : DevExpress.XtraEditors.XtraUserControl
    {
        public RequesAgentPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();

        }

        void SetCnText()
        {
            this.radioGroup1.Properties.Items[0].Description = "普通代理";
            this.radioGroup1.Properties.Items[1].Description = "第三方代理";
            this.radioGroup1.Properties.Items[2].Description = "对收款有特殊要求的代理";
            this.labRemark.Text = "备注";
            btnOK.Text = "确定(&O)";
            btnCancel.Text = "取消(&C)";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
        }
    }
}
