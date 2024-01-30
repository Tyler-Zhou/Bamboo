using System;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 批量账单快速搜索界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BatchCustomerBillFastSearchPart : BaseSearchPart
    {
        #region Service
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 财务服务
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region Property
        List<Guid> companyIDs = null;
        /// <summary>
        /// 公司IDs,默认为当前用户所在公司( OrganizationType.Company)
        /// </summary>
        Guid[] _CompanyIDs
        {
            get
            {
                if (companyIDs != null)
                {
                    return companyIDs.ToArray();
                }
                else
                {
                    companyIDs = new List<Guid>();

                    List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                    if (userCompanyList.Count == 0)
                    {
                        throw new Exception(LocalData.IsEnglish ? "You have no rights to query data of any company. Please contat administrator." : "您没有权限查询任何操作口岸的数据，请联系管理员！");
                    }

                    foreach (var item in userCompanyList)
                    {
                        companyIDs.Add(item.ID);
                    }
                    return companyIDs.ToArray();
                }
            }
        }
        #endregion

        #region Delegate
        public override event SearchResultHandler OnSearched;
        #endregion

        #region Init
        /// <summary>
        /// 批量账单快速搜索界面
        /// </summary>
        public BatchCustomerBillFastSearchPart()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender,arg)=>
            {
                UnRegisterEvent();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region Controls Event
        /// <summary>
        /// 输入框按键事件
        /// </summary>
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) btnSearch.PerformClick();
        }
        /// <summary>
        /// 查询
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            txtNo.KeyDown += TextBox_KeyDown;
            stxtCustomer.KeyDown += TextBox_KeyDown;
            KeyDown += TextBox_KeyDown;
            llabMore.Click += delegate { OnClickMore(); };
            btnSearch.Click += btnSearch_Click;
        }

        
        /// <summary>
        /// 移除事件
        /// </summary>
        void UnRegisterEvent()
        {
            txtNo.KeyDown -= TextBox_KeyDown;
            stxtCustomer.KeyDown -= TextBox_KeyDown;
            KeyDown -= TextBox_KeyDown;
            btnSearch.Click -= btnSearch_Click;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            int i = 0;
            if (string.IsNullOrEmpty(txtNo.Text.Trim()) && string.IsNullOrEmpty(stxtCustomer.Text.Trim()))
            {
                i = 100;
            }
            List<BillList> list = FinanceService.GetBillList(_CompanyIDs
                ,txtNo.Text.Trim()
                ,stxtCustomer.Text.Trim()
                ,BillState.Approved
                ,BillType.AR
                ,true
                ,null
                ,null
                , i);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count + " data." : "总共查询到 " + list.Count + " 条数据.");
            return list;

        }
        /// <summary>
        /// 点击更多查找
        /// </summary>
        protected void OnClickMore()
        {
            Workitem.Commands[BatchBillCommandConstants.Command_ShowSearch].Execute();
        }
        #endregion
    }
}
