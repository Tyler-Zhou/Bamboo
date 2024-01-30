using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.CommonUI;
using ICP.Message.ServiceInterface;
namespace ICP.OA.UI.FaxManage
{  
    /// <summary>
    /// 传真预览界面
    /// </summary>
    public partial class frmFaxPreview : DevExpress.XtraEditors.XtraForm
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }
  
        #endregion

        #region 本地属性
        private ICP.Message.ServiceInterface.Message _dataSource;
        UCFaxPreview ucPreview;
        #endregion

        #region 初始化

        public frmFaxPreview()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Disposed += delegate
            {
                this._dataSource = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(ucPreview);
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
                if (this.ucPreview != null)
                {
                    this.ucPreview.Dispose();
                    this.ucPreview = null;
                }
            };
            if (!LocalData.IsDesignMode && LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            this.Text = "查看传真";
            barTransfer.Caption = "转发(&T)";
            barClose.Caption = "关闭(&C)";
            barDelete.Caption = "删除(&D)";

            barRevert.Caption = "回复(&R)";
            barRevertAll.Caption = "全部回复(&A)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            HideBarItems();


        }

        private void HideBarItems()
        {
            this.barRevert.Visibility = this.barRevertAll.Visibility = this.barTransfer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        private void EnsurePreviewControlExists()
        {
            if (!LocalData.IsDesignMode)
            {
                ucPreview = Workitem.Items.AddNew<UCFaxPreview>();
                ucPreview.ReadOnly = true;
                this.pnlContent.Controls.Add(ucPreview);
                this.ucPreview.Dock = DockStyle.Fill;
            }
        }
        #endregion

        #region 接口


        public void BindData(ICP.Message.ServiceInterface.Message entry)
        {

            if (entry == null) entry = new ICP.Message.ServiceInterface.Message();
            _dataSource = entry;
            EnsurePreviewControlExists();
            SetTitle();
            this.ucPreview.BindData(_dataSource);
        }

        private void SetTitle()
        {

            string title;
            bool isEnglish = LocalData.IsEnglish;
            if (_dataSource.Type == MessageType.Fax)
            {
                title = isEnglish ? "Read Fax" : "查看传真";
            }
            else
            {
                title = isEnglish ? "Read EDI" : "查看EDI";
                title = string.Format("{0}{1}{2}", title, "-", _dataSource.UserProperties.EDILogRelation.TypeDescription);
            }
            this.Text = title;

        }
        private void InitSource(ICP.Message.ServiceInterface.Message entry)
        {
            //ICP.Message.ServiceInterface.Message message = ClientMessageService.Get(entry.Id);
            BindData(entry);
        }
        #endregion





        #region bar
        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                ClientMessageService.Remove(new Guid[] { _dataSource.Id }, new DateTime?[] { _dataSource.UpdateDate });

            }
            catch (Exception ex)
            {
                ErrorInfoData errorData = new ErrorInfoData(this, ex);
                XtraMessageBox.Show(errorData.Message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private ICP.Message.ServiceInterface.Message InitMessage(string sendTo, string subject)
        {
            ICP.Message.ServiceInterface.Message entry = Utility.InitMessage();
            entry.UserProperties = _dataSource.UserProperties;
            entry.SendFrom = LocalData.UserInfo.EmailAddress;
            entry.SendTo = sendTo;
            entry.Subject = subject;
            entry.Body = _dataSource.Body;
            entry.BodyFormat = _dataSource.BodyFormat;
            entry.HasAttachment = _dataSource.HasAttachment;
            entry.Attachments = _dataSource.Attachments;
            return entry;
        }


        private void barRevert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sendTo = _dataSource.SendFrom;
            string subject = (LocalData.IsEnglish ? "Re:" : "回复:") + _dataSource.Subject;
            ICP.Message.ServiceInterface.Message message = InitMessage(sendTo, subject);
            this.Close();
            ClientMessageService.Reply(_dataSource.Id, _dataSource.UpdateDate, message);
        }

        private void barTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sendTo = _dataSource.SendFrom;
            string subject = (LocalData.IsEnglish ? "Fw:" : "转发:") + _dataSource.Subject;
            ICP.Message.ServiceInterface.Message message = InitMessage(sendTo, subject);
            this.Close();
            ClientMessageService.Forward(_dataSource.Id, _dataSource.UpdateDate, message);
        }
        private void barRevertAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sendTo = string.Format("{0};{1}", _dataSource.SendFrom, _dataSource.SendTo);
            string subject = (LocalData.IsEnglish ? "Re:" : "回复:") + _dataSource.Subject;
            ICP.Message.ServiceInterface.Message message = InitMessage(sendTo, subject);
            this.Close();
            ClientMessageService.Reply(_dataSource.Id, _dataSource.UpdateDate, message);
        }
        #endregion

        private void frmFaxPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}