using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.UI;

namespace ICP.FAM.UI
{
    public partial class AgentBillCheckingSearch : BaseSearchPart
    {
        public AgentBillCheckingSearch()
        {
            InitializeComponent();

            Disposed += delegate {
                cmbCompany.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;
                cmbCompany.OnFirstEnter -= OnCompanyFirstTimeEnter;
                RemoteKeyDownHandle();
                OnSearched = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                }
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 初始化数据
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
            }
        }
        private void OnCompanyFirstTimeEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, true);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            mcDates.IsEngish = LocalData.IsEnglish;

            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);


            searchParamenter.DataPageinfo = new DataPageInfo();

            //公司
            cmbCompany.OnFirstEnter += OnCompanyFirstTimeEnter;

            //类型
            List<EnumHelper.ListItem<AgentBillCheckType>> checkType = EnumHelper.GetEnumValues<AgentBillCheckType>(LocalData.IsEnglish);
            foreach (var item in checkType)
            {
                cmbBillCheckType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbBillCheckType.SelectedIndex = 0;




        }
        /// <summary>
        /// 注册查询控件的热键
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoteKeyDownHandle()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnClare.PerformClick();
            }
        }
        #endregion

        #region 清空
        private void btnClare_Click(object sender, EventArgs e)
        {
            txtNo.Text = string.Empty;
            cmbCreate.EditValue = null;
            cmbCreate.EditText = null;
            cmbCompany.SelectedIndex = 0;
            cmbBillCheckType.SelectedIndex = 0;
            ckbCompleted.Checked = false;


        }
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Guid> compaynIDs = new List<Guid>();
            if (cmbCompany.EditValue == null)
            {
                compaynIDs = FAMUtility.GetCompanyIDList();
            }
            else
            {
                compaynIDs.Add(new Guid(cmbCompany.EditValue.ToString()));
            }

            FAMUtility.SetMcmbUsers(cmbCreate, compaynIDs, string.Empty, string.Empty);

        }
        #endregion

        #region 私有变量
        private ABCSearchParameter searchParamenter = new ABCSearchParameter();
        #endregion

        #region 查询

        public override event SearchResultHandler OnSearched;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchParamenter.No = txtNo.Text;

                if (FAMUtility.GuidIsNullOrEmpty(cmbCompany.EditValue))
                {
                    searchParamenter.CompanyID = FAMUtility.GetCompanyIDList().ToArray();
                }
                else
                {
                    searchParamenter.CompanyID = new Guid[] { (Guid)cmbCompany.EditValue };
                }

                searchParamenter.CheckCompanyID = null;
                searchParamenter.CheckType = AgentBillCheckType.InternalAgentBill;//暂时只有这一种类型
                searchParamenter.IsCompleted = ckbCompleted.Checked;
                searchParamenter.BeginDate = mcDates.From;
                searchParamenter.EndDate = mcDates.To;

                searchParamenter.DataPageinfo.PageSize = (int)numMaxCount.Value;
                searchParamenter.DataPageinfo.CurrentPage = 1;
                if (string.IsNullOrEmpty(searchParamenter.DataPageinfo.SortByName))
                {
                    searchParamenter.DataPageinfo.SortByName = "NO";
                    searchParamenter.DataPageinfo.SortOrderType = SortOrderType.Asc;
                }

                if (cmbCreate.EditValue != null)
                {
                    searchParamenter.CreateID = (Guid)cmbCreate.EditValue;
                }
                else
                {
                    searchParamenter.CreateID = null;
                }

                if (OnSearched != null)
                {
                    PageList list = GetData() as PageList;
                    if (list != null && list.DataPageInfo != null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString()+" data." : "总共查询到 "
                                                    + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }
                    OnSearched(this, list);
                }
            }
        }

        public override object GetData()
        {
            Guid? companyID=null;
            Guid? crateID=null;

            if(cmbCompany.EditValue!=null)
            {
                companyID=(Guid)cmbCompany.EditValue;
            }
            if(cmbCreate.EditValue!=null)
            {
                crateID=(Guid)cmbCreate.EditValue;
            }

            return FinanceService.GetAgnetBillCheckList(
                   searchParamenter.No,
                   searchParamenter.CompanyID,
                   searchParamenter.CheckType,
                   searchParamenter.IsCompleted,
                   searchParamenter.CreateID,
                   searchParamenter.BeginDate,
                   searchParamenter.EndDate,
                   searchParamenter.DataPageinfo,
                   searchParamenter.IsEnglish);
        }
        #endregion

        #region 重写
        /// <summary>
        /// 热键搜索
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="data"></param>
        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParamenter.DataPageinfo = dataPageInfo;
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }
        #endregion


    }
    /// <summary>
    /// 查询参数实体
    /// </summary>
    public class ABCSearchParameter
    { 
        public string No{get;set;}
        public Guid[] CompanyID { get; set; }
        public Guid? CheckCompanyID { get; set; }
        public AgentBillCheckType CheckType { get; set; }
        public bool IsCompleted { get; set; }
        public Guid? CreateID { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DataPageInfo DataPageinfo { get; set; }
        public bool IsEnglish { get { return LocalData.IsEnglish; } }
    }


}
