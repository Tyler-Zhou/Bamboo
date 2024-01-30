using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Service;
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
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI;
using ICP.Framework.CommonLibrary;

namespace ICP.FAM.UI
{
    public partial class ImportUFBeginBalanceListPart : BasePart
    {
        public ImportUFBeginBalanceListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (glFinder != null)
                {
                    glFinder.Dispose();
                    glFinder = null;
                }
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
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

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
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

        #region 属性
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
        BeginBalances CurrentRow
        {
            get
            {
                return bsList.Current as BeginBalances;
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
        #endregion

        #region 窗体事件
        private IDisposable glFinder;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                BindDataList();
            }
        }
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        private void BindDataList()
        {
            CompanyID = DataTypeHelper.GetGuid(cmbCompany.EditValue, LocalData.UserInfo.DefaultCompanyID);
            Year = DataTypeHelper.GetInt(cmbYear.EditValue, DateTime.Now.Year);

            List<BeginBalances> dataList = FinanceService.GetBeginBalance(CompanyID,Year);
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
            cmbBalanceDirection.Properties.Items.Clear();
            foreach (var item in glCodeProperty)
            {
                if (item.Value != GLCodeProperty.Unknown)
                {
                    cmbBalanceDirection.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbBalanceDirection.Properties.EndUpdate();

            ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);

            //年
            cmbYear.Properties.Items.Clear();
            for (int n = 3; n >= 0; n--)
            {
                int yearh = DateTime.Now.Year - n;
                cmbYear.Properties.Items.Add(new ImageComboBoxItem(yearh, yearh));
            }
            cmbYear.Properties.Items.Add(new ImageComboBoxItem(DateTime.Now.Year + 1, DateTime.Now.Year + 1));

            cmbCompany.EditValue = LocalData.UserInfo.DefaultCompanyID;
            cmbYear.EditValue = DateTime.Now.Year;

            #region 科目搜索器
            glFinder = dfService.RegisterGridColumnFinder(colGLCode
                          , FAMFinderConstants.GLCodeFinder
                          , new string[] { "GLID", "GLCode", "GLName", "GLCodeProperty" }
                          , new string[] { "ID", "Code", "FullName", "GLCodeProperty" }
                          , GetSolutionIDSearchCondition);
            #endregion

            FAMUtility.ShowGridRowNo(gvMain);
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
            idList.Add(CompanyID);

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

            List<BeginBalances> saveList = DataList.FindAll(b => b.IsDirty&&b.RowCount<=1);
            if(saveList==null||saveList.Count==0)
            {
                return;
            }
            foreach (BeginBalances item in saveList)
            {
                if (FAMUtility.GuidIsNullOrEmpty(item.GLId))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this, "科目代码必须输入");
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
            }

            SaveData(saveList);

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
            List<Guid> GLIds = new List<Guid>();
            List<Guid?> Ids = new List<Guid?>();
            List<String> Remarks = new List<string>();
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
                OrgAmts.Add(item.OrgAmt);
                Rates.Add(item.Rate);
                GLIds.Add(item.GLId);
                Ids.Add(item.Id);                      
                UpdateDateList.Add(item.UpdateDate);
            }

            request.DRAmts = DRAmts.ToArray();
            request.CRAmts = CRAmts.ToArray();
            request.Rates = Rates.ToArray();
            request.GLIDs = GLIds.ToArray();
            request.DetailIDS = Ids.ToArray();
            request.SaveBy = LocalData.UserInfo.LoginID;
            request.OrgAmts = OrgAmts.ToArray();
            request.UpdateDates = UpdateDateList.ToArray();
            request.CustomerIDs = new Guid?[GLIds.Count];
            request.DepIDs = new Guid?[GLIds.Count];
            request.UserIDs = new Guid?[GLIds.Count];
            request.CompanyID = CompanyID;
            request.Year = Year;
            request.SaveBy = LocalData.UserInfo.LoginID;
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
            item.RowCount = 0;
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

            //包含了多个的。不能删除
            int count=(from d in selecteds where d.RowCount>1 select d).Count();

            if(count>0)
            {
                XtraMessageBox.Show("选择的科目中，有包含了多条明细的，请先在详细中删除明细");
            }

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
               
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "删除成功!");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
     
        }
        #endregion

        #region 详细
        private void barDetail_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowDetailForm();
          
        }
        /// <summary>
        /// 双击查看详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            ShowDetailForm();
        }

        private void ShowDetailForm()
        {
            if (CurrentRow != null)
            {
                //if (CurrentRow.GLId == Guid.Empty)
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "There is no subject in the current line, please select the subject, save and then view details" : "当前行没有科目,请先选择科目,保存后再查看明细");
                //    return;
                //}
                //打开详细界面
                GLBeginBalanceDetailListPart detailPart = Workitem.Items.AddNew<GLBeginBalanceDetailListPart>();
                detailPart.GLID = CurrentRow.GLId;
                detailPart.CompanyID = CompanyID;
                detailPart.Year = Year;
                IWorkspace mainWorkspace = Workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = "查看详细";
                mainWorkspace.Show(detailPart, smartPartInfo);

                detailPart.Saved += delegate(object[] prams)
                {
                    EditPartSaved(prams);
                };

                if (CurrentRow.GLId == Guid.Empty)
                {
                    BindDataList();
                }
            }
        }
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (prams == null || prams.Length == 0) return;

                List<BeginBalances> list = prams[0] as List<BeginBalances>;
                if (list == null || list.Count==0)
                {
                    list=new List<BeginBalances>();
                    return ;
                }

                BeginBalances tager = DataList.Find(delegate(BeginBalances item) { return item.GLId == list[0].GLId; });
                if (tager!= null)
                {
                    CurrentRow.RowCount = list.Count();
                    CurrentRow.Balance = (from d in list select d.GLCodeProperty==GLCodeProperty.Debit?d.Balance:-d.Balance).Sum();
                    CurrentRow.Balance = (from d in list select d.GLCodeProperty == GLCodeProperty.Debit ? d.OrgAmt : -d.OrgAmt).Sum();

                }                  

            }
        }
        #endregion

        #region 判断当前行是否可以编辑
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                if (CurrentRow.RowCount > 1)
                {
                   // this.gvMain.OptionsBehavior.ReadOnly = true;
                    gvMain.OptionsBehavior.Editable = false;
                }
                else
                {
                    gvMain.OptionsBehavior.Editable = true;
                    //this.gvMain.OptionsBehavior.ReadOnly = false;
                }
            }
        }
        #endregion

        #region 导出
        private void barExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            int theradID = 0;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.FileName = LocalData.UserInfo.DefaultCompanyName+"期初余额";
                    saveFile.Filter = "xls files(*.xls)|*.xls";
                    saveFile.RestoreDirectory = true;
                    saveFile.FilterIndex = 2;
                    if (saveFile.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    string fileName = saveFile.FileName.ToString();

                    theradID = LoadingServce.ShowLoadingForm("Exporting......");

                    gvMain.ExportToXls(fileName);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),ex.Message);
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }
            }

        }
        #endregion

        #region 试算
        private void barSpreadsheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<BeginBalances> DataList = FinanceService.GetBeginBalance(CompanyID,Year);

            BeginSpreadsheetPart part = new BeginSpreadsheetPart();

            part.DataList = DataList;

            PartLoader.ShowDialog(part,"期初余额试算平衡");
        }
        #endregion

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            BindDataList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataList();
        }

    }
}
