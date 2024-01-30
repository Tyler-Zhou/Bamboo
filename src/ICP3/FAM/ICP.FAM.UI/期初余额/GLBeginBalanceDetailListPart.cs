using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

namespace ICP.FAM.UI
{
    public partial class GLBeginBalanceDetailListPart : BasePart
    {
        public GLBeginBalanceDetailListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (glFinder != null)
                {
                    glFinder.Dispose();
                    glFinder = null;
                }
                if (organizationFinder != null)
                {
                    organizationFinder.Dispose();
                    organizationFinder = null;
                }
                if (userNameFinder != null)
                {
                    userNameFinder.Dispose();
                    userNameFinder = null;
                }
            };
        }

        #region 服务
        [ServiceDependency]
        private WorkItem Workitem
        {
            get;
            set;
        }

        BeginBalances CurrentRow
        {
            get
            {
                return bsList.Current as BeginBalances;
            }
        }
       
        IDataFinderFactory dataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }
        IDataFindClientService dfService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();

            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数据源
        /// </summary>
        List<BeginBalances> CurrentSource
        {
            get
            {
                List<BeginBalances> list = new List<BeginBalances>();
                list = bsList.DataSource as List<BeginBalances>;
                if (list == null)
                {
                    list = new List<BeginBalances>();
                }
                return list;
            }
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        List<BeginBalances> DataList
        {
            get
            {
                List<BeginBalances> list = new List<BeginBalances>();
                list = bsList.DataSource as List<BeginBalances>;
                if (list == null)
                {
                    list = new List<BeginBalances>();
                }
                return list;
            }
        } 
        
        /// <summary>
        /// 选择的数据
        /// </summary>
        List<BeginBalances> SelectedItem
        {
            get
            {

                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<BeginBalances> tagers = new List<BeginBalances>();
                foreach (var item in rowIndexs)
                {
                    BeginBalances dr = gvMain.GetRow(item) as BeginBalances;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
            }
        }
        public Guid GLID
        {
            get;
            set;
        }
        public Guid CompanyID
        {
            get;
            set;
        }
        public Int32 Year
        {
            get;
            set;
        }

        SolutionGLCodeList currGLCode;

        /// <summary>
        /// 
        /// </summary>
        List<SolutionExchangeRateList> rateList;

        /// <summary>
        /// 汇率
        /// </summary>
        SolutionExchangeRateList rate = new SolutionExchangeRateList();

        /// <summary>
        /// 人民币的Guid
        /// </summary>
        public Guid RMB = new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976"); //人民币的Guid

        DateTime startDate = DateTime.Parse(DateTime.Now.Year.ToString() + "-01-01");

        #endregion

        #region 私有变量
        private IDisposable customerFinder, glFinder, organizationFinder, userNameFinder;
        #endregion

        #region 窗体事件

        public event SavedHandler Saved;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                SearchRegister();
                InitControls();
                BindDataList();
            }
        }
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        private void BindDataList()
        {
            List<BeginBalances> dataList = FinanceService.GetGLBeginBalance(CompanyID,Year, GLID);
            bsList.DataSource = dataList;
            bsList.ResetBindings(false);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            List<EnumHelper.ListItem<GLCodeProperty>> glCodeProperty = EnumHelper.GetEnumValues<GLCodeProperty>(LocalData.IsEnglish);
            cmbBalanceDirection.Properties.BeginUpdate();
            foreach (var item in glCodeProperty)
            {
                if (item.Value != GLCodeProperty.Unknown)
                {
                    cmbBalanceDirection.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbBalanceDirection.Properties.EndUpdate();

            FAMUtility.ShowGridRowNo(gvMain);

            if (GLID != Guid.Empty)
            {
                currGLCode = ConfigureService.GetSolutionGLCodeInfoNew(GLID, LocalData.IsEnglish);
                rateList = ConfigureService.GetCompanyExchangeRateList(CompanyID, true);
                ConfigureInfo companyInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID, LocalData.IsEnglish);

                if (currGLCode.ForeignCurrencyID != null && currGLCode.ForeignCurrencyID != Guid.Empty)
                {
                    rate = rateList.Find(r => r.SourceCurrencyID == currGLCode.ForeignCurrencyID && r.TargetCurrencyID == companyInfo.StandardCurrencyID && r.IsValid == true && r.FromDate <= startDate && r.ToDate >= startDate);
                }
            }
        }
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region 客户搜索器
            customerFinder = dfService.RegisterGridColumnFinder(colCustomerCode
                          , CommonFinderConstants.CustoemrFinder
                          , new string[] { "CustomerId", "CustomerCode", "CustomerName" }
                          , new string[] { "ID", "Code", "CName" });


            #endregion

            #region 科目搜索器
            glFinder = dfService.RegisterGridColumnFinder(colGLCode
                          ,FAMFinderConstants.GLCodeFinder
                          , new string[] { "GLID", "GLCode", "GLName", "GLCodeProperty" }
                          , new string[] { "ID", "Code", "CName", "GLCodeProperty" }
                          , GetSolutionIDSearchCondition);
            #endregion

            #region 部门搜索器
            organizationFinder = dfService.RegisterGridColumnFinder(colDepName
                                                 , SystemFinderConstants.OrganizationFinder
                                                 , "DeptID"
                                                 , "DeptName"
                                                 , "ID"
                                                 , LocalData.IsEnglish ? "EShortName" : "CShortName");
            #endregion

            #region 用户搜索器
            userNameFinder = dfService.RegisterGridColumnFinder(colUserName
                          , SystemFinderConstants.UserFinder
                          , "PersonalID"
                          , "PersonalName"
                          , "ID"
                          , LocalData.IsEnglish ? "EName" : "CName");
            #endregion

        }
        SearchConditionCollection GetSolutionIDSearchCondition()
        {
            Guid solutionID = Guid.Empty;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            if (configureInfo != null)
            {
                solutionID = configureInfo.SolutionID;
            }
            List<Guid> idList = new List<Guid>();
            idList.Add(LocalData.UserInfo.DefaultCompanyID);

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", solutionID, false);
            conditions.AddWithValue("OnlyLeaf", true, false);
            conditions.AddWithValue("CompanyIDs", idList, false);
            return conditions;
        }

        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvMain.CloseEditor();
            bsList.EndEdit();
        
            List<BeginBalances> saveList = DataList.FindAll(b => b.IsDirty);
            if(saveList==null||saveList.Count==0)
            {
                return;
            }

            foreach(BeginBalances item in saveList)
            {

                if (FAMUtility.GuidIsNullOrEmpty(item.GLId))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),"科目代码必须输入");
                    return;
                }

                if(item.GLCodeProperty==GLCodeProperty.Debit)
                {
                    item.DRAmt=item.Balance;
                    item.CRAmt=0;
                }
                else if(item.GLCodeProperty==GLCodeProperty.Credit)
                {
                     item.CRAmt=item.Balance;
                     item.DRAmt=0;
                 } 
                else
                {
                    item.DRAmt=0;
                    item.CRAmt=0;
                }

                if (currGLCode.ForeignCurrencyID != null && currGLCode.ForeignCurrencyID != Guid.Empty && item.Rate == 0)
                {
                    item.Rate = rate.Rate;
                }
            }

            SaveData(saveList);

            if (Saved != null)
            {
                Saved(DataList);
            }

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "保存成功");
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="saveList"></param>
        private void SaveData(List<BeginBalances> saveList)
        {
            if (saveList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "共导入0条数据");
                return;
            }

            BeginBalanceSaveRequest request = new BeginBalanceSaveRequest();

            #region  拼装数据
            List<Decimal> DRAmts = new List<decimal>();
            List<Decimal> CRAmts = new List<decimal>();
            List<Decimal> OrgAmts = new List<decimal>();
            List<Decimal?> Rates = new List<decimal?>();
            List<Guid?> CustomerIds = new List<Guid?>();
            List<Guid?> DeptIDs = new List<Guid?>();
            List<Guid> GLIds = new List<Guid>();
            List<Guid?> Ids = new List<Guid?>();
            List<String> Remarks = new List<string>();
            List<Guid?> UserIDs = new List<Guid?>();
            List<DateTime?> UpdateDateList = new List<DateTime?>();

            foreach (BeginBalances item in saveList)
            {
                if (FAMUtility.GuidIsNullOrEmpty(item.GLId))
                {
                    continue;
                }
                if (FAMUtility.GuidIsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid();
                }
                DRAmts.Add(item.DRAmt);
                CRAmts.Add(item.CRAmt);
                Rates.Add(item.Rate);
                CustomerIds.Add(item.CustomerId);
                DeptIDs.Add(item.DeptID);
                GLIds.Add(item.GLId);
                Ids.Add(item.Id);
                UserIDs.Add(item.PersonalID);
                OrgAmts.Add(item.OrgAmt);
                UpdateDateList.Add(item.UpdateDate);
            }

            request.DRAmts = DRAmts.ToArray();
            request.CRAmts = CRAmts.ToArray();
            request.Rates = Rates.ToArray();
            request.CustomerIDs = CustomerIds.ToArray();
            request.GLIDs = GLIds.ToArray();
            request.DetailIDS = Ids.ToArray();
            request.UserIDs = UserIDs.ToArray();
            request.SaveBy = LocalData.UserInfo.LoginID;
            request.DepIDs = DeptIDs.ToArray();
            request.OrgAmts = OrgAmts.ToArray();
            request.UpdateDates = UpdateDateList.ToArray();
            request.CompanyID = CompanyID;
            request.Year = Year;

            #endregion

            if (request.GLIDs == null || request.GLIDs.Length == 0)
            {
                return;
            }

            //调用服务保存
            ManyResult result = FinanceService.SaveBeginBalance(request);

            //更新界面上的数据
            foreach (SingleResult item in result.Items)
            {
                Guid id = item.GetValue<Guid>("ID");
                DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");

                BeginBalances searchData = saveList.Find(p => p.Id == id);
                if (searchData != null)
                {
                    searchData.UpdateDate = updateDate;
                    searchData.IsDirty = false;
                }
            }

        }
        #endregion

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Close();
            }
        }
        #endregion

        #region 新增 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BeginBalances item = new BeginBalances();
            item.CreateBy = LocalData.UserInfo.LoginID;
            item.Remark = "期初余额";
            item.GLCodeProperty = GLCodeProperty.Debit;
            if (currGLCode != null)
            {
                item.GLCode = currGLCode.Code;
                item.GLName = currGLCode.CName;
                item.GLId = currGLCode.ID;
            }

            bsList.Insert(0, item);

            bsList.ResetBindings(false);
        }
        #endregion

        #region 删除
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvMain.CloseEditor();
            List<BeginBalances> selecteds = SelectedItem;
            if (selecteds == null || selecteds.Count == 0) return;

            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                              "确认要删除这些数据?",
                                              "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion

            #region 构建需数据库中删除的数据

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdates = new List<DateTime?>();

            foreach (var item in selecteds)
            {
                if (item.IsNew) continue;

                needRemoveIDs.Add(item.Id);
                needRemoveUpdates.Add(item.UpdateDate);
            }
            #endregion

            try
            {
                if (needRemoveIDs.Count > 0)
                {
                    FinanceService.RemoveBeginBalance(needRemoveIDs.ToArray(), needRemoveUpdates.ToArray(), LocalData.UserInfo.LoginID);
                }

                List<BeginBalances> source = CurrentSource;

                foreach (var item in selecteds)
                {
                    source.Remove(item);
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "删除成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
     
        }
        #endregion

        #region 删除
        private void barDelete_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            gvMain.CloseEditor();
            List<BeginBalances> selecteds = SelectedItem;
            if (selecteds == null || selecteds.Count == 0) return;


            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                              "确认要删除这些数据?",
                                              "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion

            #region 构建需数据库中删除的数据

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdates = new List<DateTime?>();

            foreach (var item in selecteds)
            {
                if (item.IsNew) continue;

                needRemoveIDs.Add(item.Id);
                needRemoveUpdates.Add(item.UpdateDate);
            }
            #endregion

            try
            {
                if (needRemoveIDs.Count > 0)
                {
                    FinanceService.RemoveBeginBalance(needRemoveIDs.ToArray(), needRemoveUpdates.ToArray(), LocalData.UserInfo.LoginID);

                    gvMain.DeleteSelectedRows();
                }
                if (Saved != null)
                {
                    Saved(DataList);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "删除成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }

        }
        #endregion

        #region 刷新
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            BindDataList();
        }
        #endregion

        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if(e.RowHandle < 0 )
            {
                return;
            }
            if (e.Column.FieldName.ToUpper() == "GLCODE")
            {
                if (GLID == Guid.Empty && CurrentRow.GLId != Guid.Empty)
                {
                    GLID = CurrentRow.GLId;
                    currGLCode = ConfigureService.GetSolutionGLCodeInfoNew(GLID, LocalData.IsEnglish);
                    rateList = ConfigureService.GetCompanyExchangeRateList(CompanyID, true);
                    ConfigureInfo companyInfo = ConfigureService.GetCompanyConfigureInfo(CompanyID, LocalData.IsEnglish);

                    if (currGLCode.ForeignCurrencyID != null && currGLCode.ForeignCurrencyID != Guid.Empty)
                    {
                        rate = rateList.Find(r => r.SourceCurrencyID == currGLCode.ForeignCurrencyID && r.TargetCurrencyID == companyInfo.StandardCurrencyID && r.IsValid == true && r.FromDate <= startDate && r.ToDate >= startDate);
                    }
                }
            }
            if (e.Column.FieldName == "OrgAmt" && (currGLCode.ForeignCurrencyID != null && currGLCode.ForeignCurrencyID != Guid.Empty))
            {
                BeginBalances balance = gvMain.GetFocusedRow() as BeginBalances;
                if (rate.Rate > 0)
                {
                    balance.Balance = balance.OrgAmt * rate.Rate;
                }
            }
            gvMain.RefreshData();
        }

    }
}
