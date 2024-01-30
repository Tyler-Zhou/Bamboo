using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using System.Windows.Forms;

namespace ICP.FAM.UI.Bill
{
    /// <summary>
    /// 账单的付款申请
    /// </summary>
    public partial class BillPaymentRequest : BasePart
    {
        public BillPaymentRequest()
        {
            InitializeComponent();

            Disposed += delegate
            {
                billList = null;
                ConfigureInfo = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IWorkflowClientService WorkflowClientService
        {
            get
            {
                return ServiceClient.GetClientService<IWorkflowClientService>();
            }
        }
        #endregion

        #region 属性

        private bool _useCommon = false;
        /// <summary>
        /// 是否采用通用模式获取工作名 [获取工作名就不在往下执行]
        /// </summary>
        public bool useCommon
        {
            get { return _useCommon; }
            set { _useCommon = value; }
        }

        public DialogResult dialogResult
        {
            get;
            set;
        }
        /// <summary>
        /// 账单数据 
        /// </summary>
        public CurrencyBillList billList
        {
            get;
            set;
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate
        {
            get;
            set;
        }

        /// <summary>
        /// WorkName
        /// </summary>
        private string _WorkName;
        public string WorkName
        {
            get { return _WorkName; }
            set
            {
                txtWorkName.Text = value;
                _WorkName = value;
            }
        }
        /// <summary>
        /// 配置信息
        /// </summary>
        public ConfigureInfo ConfigureInfo { get; set; }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
            }
        }

        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("11090600003", LocalData.IsEnglish ? "Input the process name" : "请输入流程名称");
        }
        #endregion

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            FindForm().Close();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _WorkName = txtWorkName.Text.Trim();

            if (string.IsNullOrEmpty(_WorkName))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    FindForm(),
                    NativeLanguageService.GetText(this, "11090600003"));
                return;
            }
            if (useCommon)
            {
                dialogResult = DialogResult.OK;
                FindForm().Close();
                return;
            }

            if (billList == null)
            {
                return;
            }

            string formTitle = "付款申请";

            try
            {
                WorkflowClientService.StartNoticeOfPaymentWorkFlow(
                    LocalData.UserInfo.LoginID,
                    LocalData.UserInfo.DefaultDepartmentID,
                    _WorkName,
                    formTitle,
                    string.Empty,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                    LocalData.UserInfo.DefaultDepartmentID,
                    billList.BillRefNO == null ? string.Empty : billList.BillRefNO,
                    LocalData.IsEnglish ? "" : "见账号资料",
                    (billList.Amount * Rate).ToString("n"),
                    ConfigureInfo.StandardCurrency,
                    billList.CurrencyName,
                    billList.CustomerName,
                    billList.CustomerID,
                    billList.BillNO == null ? string.Empty : billList.BillNO,
                    billList.CustomerName,
                    LocalData.IsEnglish ? "" : "见账号资料",
                    string.Empty,
                    string.Empty,
                    LocalData.UserInfo.LoginName);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
            FindForm().Close();

        }

    }
}
