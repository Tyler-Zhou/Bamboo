using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Windows.Forms;

namespace ICP.FCM.OceanExport.UI.Common
{
    /// <summary>
    /// 复制业务
    /// </summary>
    public partial class UCCopyBusiness : BaseEditPart, IDisposable
    {
        #region Fields
        /// <summary>
        /// 业务ID
        /// </summary>
        private Guid _OperationID;
        #endregion

        #region Property
        /// <summary>
        /// 新业务ID
        /// </summary>
        public Guid NewOperationID { get; set; }
        /// <summary>
        /// 新业务号
        /// </summary>
        public string NewOperationNo { get; set; }
        #endregion

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        #endregion

        #region Init
        /// <summary>
        /// 复制业务
        /// </summary>
        public UCCopyBusiness()
        {
            InitializeComponent();
            InitControls();
            RegisterEvent();
        } 
        #endregion

        #region Base & Dispose
        void IDisposable.Dispose()
        {
            UnRegisterEvent();
            if (Workitem != null)
            {
                Workitem.Items.Remove(this);
                Workitem = null;
            }
        } 
        #endregion

        #region Windows Event
        void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SingleResult result = OceanExportService.CopyOperationInfo(_OperationID, checkEditBooking.Checked, checkEditBL.Checked, checkEditAcc.Checked, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                if (result == null)
                {
                    NewOperationID = Guid.Empty;
                    CloseForm(DialogResult.No);
                }
                else
                {
                    NewOperationID = result.GetValue<Guid>("ID");
                    NewOperationNo = result.GetValue<string>("No");
                    CloseForm(DialogResult.OK);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm(DialogResult.Cancel);
        } 
        #endregion

        #region Method
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        public void BindingData(object data)
        {
            BusinessOperationContext context = data as BusinessOperationContext;
            if (context != null)
            {
                _OperationID = context.OperationID;
            }
        }

        private void InitControls()
        {
            checkEditBooking.Checked = true;
            checkEditBooking.Enabled = false;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            btnOK.Click -= btnOK_Click;
            btnCancel.Click -= btnCancel_Click;
        }

        private void CloseForm(DialogResult dialogResult)
        {
            var findForm = FindForm();
            if (findForm != null)
            {
                findForm.DialogResult = dialogResult;
                findForm.Close();
            }
        }
        #endregion
    }
}
