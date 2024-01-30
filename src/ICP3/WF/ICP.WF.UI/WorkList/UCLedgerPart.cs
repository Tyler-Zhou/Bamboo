using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraGrid.Columns;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.UI
{
    public partial class UCLedgerPart : UserControl
    {
        public UCLedgerPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (organizationFinder != null)
                {
                    organizationFinder.Dispose();
                    organizationFinder = null;
                }
                if (userFinder != null)
                {
                    userFinder.Dispose();
                    userFinder = null;
                }
                if (glFinder != null)
                {
                    glFinder.Dispose();
                    glFinder = null;
                }
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region 属性
        private IDisposable organizationFinder, userFinder, glFinder, customerFinder;

        public List<KeyValuePair<int, Guid?>> OrgCurrencyList = new List<KeyValuePair<int, Guid?>>();

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<WFLedgerList> DataList
        {
            get
            {
                return bsDetail.DataSource as List<WFLedgerList>;
            }
        }
        /// <summary>
        /// 当前行数据
        /// </summary>
        public WFLedgerList CurrentRow
        {
            get
            {
                return bsDetail.Current as WFLedgerList;

            }
        }
        /// <summary>
        /// 默认公司的解决方案
        /// </summary>
        public Guid SolutionID
        {
            get;
            set;
        }
        /// <summary>
        /// 默认公司的本位币
        /// </summary>
        public Guid StandardCurrencyID
        {
            get;
            set;
        }

        /// <summary>
        /// 是否需要从服务器中获取数据
        /// </summary>
        public bool IsGetServerData
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示附单据数面板
        /// </summary>
        public bool IsShowReceiptQty
        {
            get;
            set;
        }
        /// <summary>
        /// 表单数据
        /// </summary>
        public WFLedgerMaster FormData
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人部门ID
        /// </summary>
        public Guid ApplyDeptID { get; set; }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public Guid ApplyUserID { get; set; }

        List<SolutionExchangeRateList> rateList;
        public DateTime startDate;
        #endregion

        #region 窗体加载
        private void SetBaseData()
        {
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID);
            if (configureInfo != null)
            {
                SolutionID = configureInfo.SolutionID;
                StandardCurrencyID = configureInfo.StandardCurrencyID;
            }
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                this.pnlReceiptQty.Visible = IsShowReceiptQty;
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            #region 会计科目
            glFinder = DataFindClientService.RegisterGridColumnFinder(colGLCode,
                        ICP.FAM.ServiceInterface.FAMFinderConstants.GLCodeFinder,
                        new string[] { "GLID", "GLFullName", "CurrencyID" },
                        new string[] { "ID", "GLCodeName", "ForeignCurrencyID" },
                        GetSolutionIDSearchCondition);


            #endregion

            #region 客户
            customerFinder = DataFindClientService.RegisterGridColumnFinder(colCustomer
                  , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                  , "CustomerID"
                  , "CustomerName"
                  , "ID"
                  , LocalData.IsEnglish ? "EName" : "CName"
                  , GetCustomerCodeApplyStateCondition);
            #endregion

            #region 部门
            organizationFinder = DataFindClientService.RegisterGridColumnFinder(colDep
                                  , SystemFinderConstants.OrganizationFinder
                                  , "DeptID"
                                  , "DeptName"
                                  , "ID"
                                  , LocalData.IsEnglish ? "EShortName" : "CShortName");
            #endregion

            #region 个人
            userFinder = DataFindClientService.RegisterGridColumnFinder(colUser
                                 , SystemFinderConstants.UserFinder
                                 , "UserID"
                                 , "UserName"
                                 , "ID"
                                 , LocalData.IsEnglish ? "EName" : "CName");
            #endregion

            if (CompanyID != Guid.Empty)
            {
                rateList = ConfigureService.GetCompanyExchangeRateList(CompanyID, true);
            }
            SetBaseData();
        }
        /// <summary>
        /// 客户审核状态
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetCustomerCodeApplyStateCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);

            return conditions;
        }
        public Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 解决方案ID
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetSolutionIDSearchCondition()
        {
            List<Guid> idList = new List<Guid>();
            if (CompanyID != null && CompanyID != Guid.Empty)
            {
                idList.Add(CompanyID);
            }
            else
            {
                idList.Add(LocalData.UserInfo.DefaultCompanyID);
            }
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", SolutionID, false);
            conditions.AddWithValue("OnlyLeaf", true, false);
            conditions.AddWithValue("CompanyIDs", idList, false);
            return conditions;
        }

        #endregion

        #region 绑定数据
        public void BindDataSource(WFLedgerMaster master)
        {
            FormData = master;
            if (master.Opinion == "0")
            {
                this.rgpOpinion.SelectedIndex = 0;
            }
            else
            {
                this.rgpOpinion.SelectedIndex = 1;
            }
            if (IsShowReceiptQty)
            {
                this.numReceiptQty.Value = DataTypeHelper.GetDecimal(master.ReceiptQty, 0);
            }
            this.txtRemark.Text = master.Remark;
            this.dtFeeDate.DateTime = DataTypeHelper.GetDateTime(master.FeeDate, DateTime.Now);

            if (IsGetServerData)
            {
                foreach (WFLedgerList item in master.LedgerList)
                {
                    if (!DataTypeHelper.GuidIsNullOrEmpty(item.GLID))
                    {
                        SolutionGLCodeList glCode = ConfigureService.GetSolutionGLCodeInfoNew(item.GLID, LocalData.IsEnglish);
                        item.GLFullName = "(" + glCode.Code + ")" + glCode.FullName;

                        if (glCode.IsDepartmentCheck && DataTypeHelper.GetGuid(item.DeptID, Guid.Empty) == Guid.Empty)
                        {
                            item.DeptID = ApplyDeptID;
                        }
                        if (glCode.IsPersonalCheck && DataTypeHelper.GetGuid(item.UserID, Guid.Empty) == Guid.Empty)
                        {
                            item.UserID = ApplyUserID;
                        }
                        if (DataTypeHelper.GuidIsNullOrEmpty(item.CurrencyID))
                        {
                            if (DataTypeHelper.GuidIsNullOrEmpty(glCode.ForeignCurrencyID))
                            {
                                item.CurrencyID = StandardCurrencyID;
                            }
                            else
                            {
                                item.CurrencyID = glCode.ForeignCurrencyID;
                            }
                        }
                    }
                    if (!DataTypeHelper.GuidIsNullOrEmpty(item.CustomerID))
                    {
                        CustomerInfo customer = CustomerService.GetCustomerInfo(item.CustomerID.Value);
                        item.CustomerName = LocalData.IsEnglish ? customer.EName : customer.CName;
                    }
                    if (!DataTypeHelper.GuidIsNullOrEmpty(item.DeptID))
                    {
                        OrganizationInfo orgInfo = OrganizationService.GetOrganizationInfo(item.DeptID.Value);
                        item.DeptName = LocalData.IsEnglish ? orgInfo.EShortName : orgInfo.CShortName;
                    }
                    if (!DataTypeHelper.GuidIsNullOrEmpty(item.UserID))
                    {
                        UserInfo user = UserService.GetUserInfo(item.UserID.Value);
                        item.UserName = LocalData.IsEnglish ? user.EName : user.CName;
                    }
                }
            }

            this.bsDetail.DataSource = master.LedgerList;
            this.bsDetail.ResetBindings(false);
            TotalInfo();
        }
        #endregion

        #region Grid事件
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WFLedgerList item = new WFLedgerList();
            item.CRAmt = 0.0m;
            item.DRAmt = 0.0m;
            item.OrgAmt = 0.0m;

            bsDetail.Add(item);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to deleted the selected Data " : "确认删除选中的数据?",
                                             LocalData.IsEnglish ? "Tip" : "提示",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bsDetail.RemoveCurrent();
                TotalInfo();
            }
        }
        #endregion

        #region 输入=号时 自动计算
        private void gvDetails_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //输入的字符中包含了=号时
            if (CurrentRow != null && e.Value != null && e.Value.ToString().Contains("="))
            {
                decimal totalDRAmt = 0.0m, totalCRAmt = 0.0m;
                for (int i = 0; i < this.gvDetails.RowCount; i++)
                {
                    //不包含当前行的数据
                    if (i != e.RowHandle)
                    {
                        WFLedgerList item = gvDetails.GetRow(i) as WFLedgerList;
                        if (item != null)
                        {
                            totalDRAmt += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                            totalCRAmt += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                        }
                    }
                }

                if (e.Column == this.colDRAmt)
                {

                    //借方
                    CurrentRow.DRAmt = totalCRAmt - totalDRAmt;
                    this.gvDetails.SetFocusedValue(CurrentRow.DRAmt);
                }
                else if (e.Column == this.colCRAmt)
                {
                    //贷方
                    CurrentRow.CRAmt = totalDRAmt - totalCRAmt;
                    this.gvDetails.SetFocusedValue(CurrentRow.CRAmt);
                }
                bsDetail.ResetBindings(false);
            }

        }

        #endregion

        #region 计算总的借方、贷方
        private void gvDetails_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            if (rateList == null)
                return;

            if (e.Column == this.colCRAmt ||
             e.Column == this.colDRAmt)
            {
                TotalInfo();
            }

            if (e.Column == this.colGLCode || e.Column == this.colOrgAmt)
            {
                if (CurrentRow.OrgAmt != null && CurrentRow.GLID != Guid.Empty)
                {
                    SolutionGLCodeList currGLCode = ConfigureService.GetSolutionGLCodeInfoNew(CurrentRow.GLID, LocalData.IsEnglish);
                    if (currGLCode != null)
                    {
                        if (CurrentRow.CurrencyID == StandardCurrencyID)
                        {
                            if (currGLCode.GLCodeProperty == GLCodeProperty.Credit && CurrentRow.OrgAmt != 0)
                            {
                                CurrentRow.CRAmt = CurrentRow.OrgAmt;
                                CurrentRow.DRAmt = 0.0m;
                            }
                            else if (currGLCode.GLCodeProperty == GLCodeProperty.Debit && CurrentRow.OrgAmt != 0)
                            {
                                CurrentRow.DRAmt = CurrentRow.OrgAmt;
                                CurrentRow.CRAmt = 0.0m;
                            }
                        }
                        else
                        {
                            var rates = rateList.FindAll(r => r.SourceCurrencyID == CurrentRow.CurrencyID && r.TargetCurrencyID == StandardCurrencyID && r.IsValid == true && r.FromDate <= startDate && r.ToDate >= startDate).OrderByDescending(r => r.CreateDate);
                            if (rates == null || rates.Count() <= 0)
                            {
                                return;
                            }
                            SolutionExchangeRateList rate = rates.First() as SolutionExchangeRateList;
                            if (rate.Rate != 0)
                            {
                                if (currGLCode.GLCodeProperty == GLCodeProperty.Credit && CurrentRow.OrgAmt != 0)
                                {
                                    CurrentRow.CRAmt = (decimal?)decimal.Round((decimal)(CurrentRow.OrgAmt * rate.Rate), 2, MidpointRounding.AwayFromZero);
                                    CurrentRow.DRAmt = 0.0m;
                                }
                                else if (currGLCode.GLCodeProperty == GLCodeProperty.Debit && CurrentRow.OrgAmt != 0)
                                {
                                    CurrentRow.DRAmt = (decimal?)decimal.Round((decimal)(CurrentRow.OrgAmt * rate.Rate), 2, MidpointRounding.AwayFromZero);
                                        CurrentRow.CRAmt = 0.0m;
                                }
                            }
                        }
                    }
                }
                TotalInfo();
            }

        }
        private void TotalInfo()
        {
            if (DataList == null || DataList.Count == 0)
            {
                this.barTotalDR.Caption = "借方:0.00";
                this.barTotalCR.Caption = "贷方:0.00";
                return;
            }
            decimal totalDR = DataTypeHelper.GetDecimal((from d in DataList select d.DRAmt).Sum(), 0);
            decimal totalCR = DataTypeHelper.GetDecimal((from d in DataList select d.CRAmt).Sum(), 0);

            this.barTotalDR.Caption = "借方:" + totalDR.ToString("F2");
            this.barTotalCR.Caption = "贷方:" + totalCR.ToString("F2");
        }
        #endregion

        #endregion

        #region 保存与验证
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData(Guid FormConfigID)
        {
            List<string> errorList = new List<string>();
            string message = string.Empty;
            string tempString = LocalData.IsEnglish ? "{0}must input on line {1}" : "第{0}行的{1}必须输入";
            decimal totalDRAmt = 0.0m, totalCRAmt = 0.0m;

            if (DataList == null || DataList.Count == 0)
            {
                message = LocalData.IsEnglish ? "Please input detail data" : "请录入明细数据";
                errorList.Add(message);
            }

            for (int i = 0; i < this.gvDetails.RowCount; i++)
            {
                WFLedgerList item = this.gvDetails.GetRow(i) as WFLedgerList;
                if (item != null)
                {
                    if (DataTypeHelper.GuidIsNullOrEmpty(item.GLID))
                    {
                        if (LocalData.IsEnglish)
                        {
                            message = string.Format(tempString, "GLCode", (i + 1).ToString());
                        }
                        else
                        {
                            message = string.Format(tempString, (i + 1).ToString(), "会计科目");
                        }
                        errorList.Add(message);
                    }
                    if (string.IsNullOrEmpty(item.Remark))
                    {
                        if (LocalData.IsEnglish)
                        {
                            message = string.Format(tempString, "Remark", (i + 1).ToString());
                        }
                        else
                        {
                            message = string.Format(tempString, (i + 1).ToString(), "摘要");
                        }
                        errorList.Add(message);
                    }
                    if (DataTypeHelper.GuidIsNullOrEmpty(item.CustomerID))
                    {
                        SolutionGLCodeList glcode = ConfigureService.GetSolutionGLCodeInfoNew(item.GLID, LocalData.IsEnglish);
                        if (glcode != null && glcode.IsCustomerCheck)
                        {
                            message = string.Format(tempString, (i + 1).ToString(), "客户");
                            errorList.Add(message);
                        }
                    }
                    if (DataTypeHelper.GetDecimal(item.DRAmt, 0) == 0 &&
                       DataTypeHelper.GetDecimal(item.CRAmt, 0) == 0)
                    {
                        if (LocalData.IsEnglish)
                        {
                            message = string.Format("DRAmt or CRAmt Enter at least one on line{0}", (i + 1).ToString());
                        }
                        else
                        {
                            message = string.Format("第{0}行的借方或贷方至少输入一个", (i + 1).ToString());
                        }
                        errorList.Add(message);
                    }
                    totalDRAmt += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                    totalCRAmt += DataTypeHelper.GetDecimal(item.CRAmt, 0);
                }
            }
            if (FormConfigID == Utility.NewCashierAuditorFormID && totalDRAmt != totalCRAmt)
            {
                //出纳支付时才去验证借贷是否平衡
                message = LocalData.IsEnglish ? "DRAmt does not equal CRAmt" : "借方与贷方不平衡";
                errorList.Add(message);
            }
            if (errorList.Count > 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(errorList.ToCustomString("错误:"));

                return false;
            }

            return ValidateCRDRData();
        }
         /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateCRDRData()
        {
            decimal totalDRAmt = 0.0m, totalCRAmt = 0.0m;
            for (int i = 0; i < this.gvDetails.RowCount; i++)
            {
                WFLedgerList item = this.gvDetails.GetRow(i) as WFLedgerList;
                totalDRAmt += DataTypeHelper.GetDecimal(item.DRAmt, 0);
                totalCRAmt += DataTypeHelper.GetDecimal(item.CRAmt, 0);
            }
            //if (totalDRAmt != totalCRAmt && totalCRAmt > 0 && totalDRAmt>0)
            //{
            //    string message = LocalData.IsEnglish ? "DRAmt does not equal CRAmt" : "借方与贷方不平衡";
            //    DevExpress.XtraEditors.XtraMessageBox.Show(message);
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        public DataSet GetData(DataSet ds, Guid WorkFlowConfigID, Guid FormConfigID)
        {
            if (ds == null)
            {
                return null;
            }

            this.gvDetails.Focus();
            this.gcMain.Focus();

            if (FormConfigID == Utility.NewCashierAuditorFormID)
            {
                // 出纳支付表单中的数据
                GetCashierAuditorFormData(ds);
            }
            else if (FormConfigID == Utility.NewAccountAuditorFormID)
            {
                // 财务审核表单中的数据
                GetAccountAuditorFormData(ds);
            }

            return ds;
        }
        /// <summary>
        /// 获得出纳支付界面中的数据
        /// </summary>
        private void GetCashierAuditorFormData(DataSet ds)
        {
            ds.Tables[0].Rows[0][CashierAuditorFormDataColumns.Opinion] = this.rgpOpinion.SelectedIndex.ToString();
            ds.Tables[0].Rows[0][CashierAuditorFormDataColumns.MasterRemark] = this.txtRemark.Text;
            ds.Tables[0].Rows[0][CashierAuditorFormDataColumns.FeeDate] = this.dtFeeDate.DateTime;

            ds.Tables[1].Rows.Clear();
            int index = 0;

            foreach (WFLedgerList item in DataList)
            {
                DataRow dr = ds.Tables[1].NewRow();
                dr[CashierAuditorFormDataColumns.GLID] = item.GLID;
                dr[CashierAuditorFormDataColumns.GLFullName] = item.GLFullName;
                dr[CashierAuditorFormDataColumns.DetailRemark] = item.Remark;
                dr[CashierAuditorFormDataColumns.OrgAmt] = DataTypeHelper.GetDecimal(item.OrgAmt, 0);
                dr[CashierAuditorFormDataColumns.DRAmt] = DataTypeHelper.GetDecimal(item.DRAmt, 0);
                dr[CashierAuditorFormDataColumns.CRAmt] = DataTypeHelper.GetDecimal(item.CRAmt, 0);
                dr[CashierAuditorFormDataColumns.CustomerID] = item.CustomerID;
                dr[CashierAuditorFormDataColumns.CustomerName] = item.CustomerName;
                dr[CashierAuditorFormDataColumns.DeptID] = item.DeptID;
                dr[CashierAuditorFormDataColumns.DeptName] = item.DeptName;
                dr[CashierAuditorFormDataColumns.UserID] = item.UserID;
                dr[CashierAuditorFormDataColumns.UserName] = item.UserName;

                if (DataTypeHelper.GuidIsNullOrEmpty(item.CurrencyID))
                {
                    if (DataTypeHelper.GuidIsNullOrEmpty(StandardCurrencyID))
                    {
                        StandardCurrencyID = Guid.Empty;
                    }
                    dr[CashierAuditorFormDataColumns.CurrencyID] = StandardCurrencyID;
                    OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, StandardCurrencyID));
                }
                else
                {
                    dr[CashierAuditorFormDataColumns.CurrencyID] = item.CurrencyID;
                    OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, StandardCurrencyID));
                }

                index++;
                ds.Tables[1].Rows.Add(dr);
            }
        }

        /// <summary>
        /// 获得出纳支付界面中的数据
        /// </summary>
        private void GetAccountAuditorFormData(DataSet ds)
        {
            ds.Tables[0].Rows[0][AccountAuditorFormDataColumns.Opinion] = this.rgpOpinion.SelectedIndex.ToString();
            ds.Tables[0].Rows[0][AccountAuditorFormDataColumns.MasterRemark] = this.txtRemark.Text;
            ds.Tables[0].Rows[0][AccountAuditorFormDataColumns.ReceiptQty] = DataTypeHelper.GetInt(this.numReceiptQty.Value, 0);
            //ds.Tables[0].Rows[0][AccountAuditorFormDataColumns.FeeDate] = this.dtFeeDate.DateTime;

            ds.Tables[1].Rows.Clear();
            int index = 0;
            foreach (WFLedgerList item in DataList)
            {
                DataRow dr = ds.Tables[1].NewRow();
                dr[AccountAuditorFormDataColumns.GLID] = item.GLID;
                dr[AccountAuditorFormDataColumns.GLFullName] = item.GLFullName;
                dr[AccountAuditorFormDataColumns.DetailRemark] = item.Remark;
                dr[AccountAuditorFormDataColumns.OrgAmt] = DataTypeHelper.GetDecimal(item.OrgAmt, 0);
                dr[AccountAuditorFormDataColumns.DRAmt] = DataTypeHelper.GetDecimal(item.DRAmt, 0);
                dr[AccountAuditorFormDataColumns.CRAmt] = DataTypeHelper.GetDecimal(item.CRAmt, 0);
                dr[AccountAuditorFormDataColumns.CustomerID] = item.CustomerID;
                dr[AccountAuditorFormDataColumns.CustomerName] = item.CustomerName;
                dr[AccountAuditorFormDataColumns.DeptID] = item.DeptID;
                dr[AccountAuditorFormDataColumns.DeptName] = item.DeptName;
                dr[AccountAuditorFormDataColumns.UserID] = item.UserID;
                dr[AccountAuditorFormDataColumns.UserName] = item.UserName;

                if (DataTypeHelper.GuidIsNullOrEmpty(item.CurrencyID))
                {
                    if (DataTypeHelper.GuidIsNullOrEmpty(StandardCurrencyID))
                    {
                        StandardCurrencyID = Guid.Empty;
                    }
                    dr[AccountAuditorFormDataColumns.CurrencyID] = StandardCurrencyID;
                    OrgCurrencyList.Add(new KeyValuePair<int,Guid?>(index,StandardCurrencyID));
                }
                else
                {
                    dr[AccountAuditorFormDataColumns.CurrencyID] = item.CurrencyID;
                    OrgCurrencyList.Add(new KeyValuePair<int, Guid?>(index, item.CurrencyID));
                }
                index++;
                ds.Tables[1].Rows.Add(dr);
            }
        }
        #endregion




    }
}
