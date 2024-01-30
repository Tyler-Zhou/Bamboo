namespace ICP.FCM.OceanExport.UI.Common.Form
{
    public partial class SendDocsForm :DevExpress.XtraEditors.XtraForm
    {
        public SendDocsForm()
        {
            InitializeComponent();
        }

        private void SetCnText()
        {
            lblAgent.Text = "代理";
            ckeBelong.Text = "是否内部代理";
            rdoAgent.Text = "发送给代理";
            rdoOverseas.Text = "发送给海外客服";
            btnSubmit.Text = "提交";
            this.Text = "向代理分发文件";

        }
    }
}
