using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Service;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.UI;

namespace ICP.FAM.UI
{
    public partial class FeeYearMonthBudgetPart : XtraForm
    {
        public FeeYearMonthBudgetPart()
        {
            InitializeComponent();
        }
        
        #region 服务 
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

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

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 加载

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                BindDataList(true);
                TotalInfo();

                FAMUtility.ShowGridRowNo(gvMain);
            }
        }

        private void InitControls()
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, true);
            //年
            cmbYear.Properties.Items.Clear();
            for (int n = 3; n >= 0; n--)
            {
                int yearh = DateTime.Now.Year - n;
                cmbYear.Properties.Items.Add(new ImageComboBoxItem(yearh, yearh));
            }
            cmbYear.Properties.Items.Add(new ImageComboBoxItem(DateTime.Now.Year + 1, DateTime.Now.Year+1));

            cmbCompany.EditValue = LocalData.UserInfo.DefaultCompanyID;
            cmbYear.EditValue = DateTime.Now.Year;

            //类型
            cmbType.Properties.Items.Add(new ImageComboBoxItem("管理费用",1));
            cmbType.Properties.Items.Add(new ImageComboBoxItem("财务费用", 2));
            cmbType.SelectedIndex = 0;

        }
        private void TotalInfo()
        {
            decimal amount = (from d in DataList where d.ChildCount==0 select d.Amount).Sum();
            txtAmount.Text = amount.ToString("N");
        }
        #endregion

        #region 属性
        public Guid CompanyID
        {
            get;
            set;
        }
        public int Year
        {
            get;
            set;
        }

        public FeeMonthBudgetType Type
        {
            get
            {
                return (FeeMonthBudgetType)DataTypeHelper.GetInt(cmbType.SelectedIndex+1);
            }
        }

        private Guid solutionID;
        public Guid SolutionID
        {
            get
            {
                if (FAMUtility.GuidIsNullOrEmpty(solutionID))
                {
                   ConfigureInfo info= ConfigureService.GetCompanyConfigureInfo(CompanyID,LocalData.IsEnglish);
                   if (info != null)
                   {
                       solutionID = info.SolutionID;
                   }                
                }
                return solutionID;
            }
        }

        List<FeeYearMonthBudgetList> DataList
        {
            get
            {
                return bsList.DataSource as List<FeeYearMonthBudgetList>;
            }
        }

        List<FeeYearMonthBudgetList> ChargeDataList
        {
            get
            {
                List<FeeYearMonthBudgetList> list = new List<FeeYearMonthBudgetList>();
                if (DataList == null)
                {
                    return list;
                }
                list = (from d in DataList where d.IsDirty select d).ToList();

                return list;
            }
        }
        public bool IsDirty
        { 
            get
            {
                bool isDirty=false;
                if (ChargeDataList.Count > 0)
                {
                    isDirty = true;
                }
                return isDirty;
            }
           
        }

        #endregion

        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataList(true);
        }
        private void BindDataList(bool isInitData)
        {
            CompanyID = DataTypeHelper.GetGuid(cmbCompany.EditValue,LocalData.UserInfo.DefaultCompanyID);
            Year = DataTypeHelper.GetInt(cmbYear.EditValue,DateTime.Now.Year);

            IsUpdateDate = false;
            List<FeeYearMonthBudgetList> list = FinanceService.GetFeeMonthBudgetList(CompanyID, Year,Type);
            bsList.DataSource = list;
            bsList.ResetBindings(false);

            if (list.Count == 0 && isInitData)
            { 
                InitDataList();
            }

            TotalInfo();

            IsUpdateDate = true;
        }
        /// <summary>
        /// 初始化数据列表
        /// </summary>
        private void InitDataList()
        {
            int theradID = LoadingServce.ShowLoadingForm("正在初始化数据...");

            try
            {
                List<SolutionGLCodeList> list = new List<SolutionGLCodeList>();
                if (Type == FeeMonthBudgetType.Cost)
                {
                    list = ConfigureService.GetFeeGLCodeList(CompanyID, LocalData.IsEnglish);
                }
                else
                {
                    list = ConfigureService.GetSolutionGLCodeListNew(SolutionID, new Guid[] { CompanyID }, "6603", string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
                }

                List<Guid> idList = new List<Guid>();
                List<Guid> glIdList = new List<Guid>();
                List<Decimal> amountList = new List<Decimal>();
                List<string> remarkList = new List<string>();
                List<DateTime?> updateDateList = new List<DateTime?>();

                foreach (SolutionGLCodeList item in list)
                {
                    if (item.Code == "6602" || item.Code == "6603"||
                        item.Code == "660299" || item.Code == "660399")
                    {
                        continue;
                    }
                    idList.Add(Guid.NewGuid());
                    glIdList.Add(item.ID);
                    amountList.Add(0);
                    remarkList.Add(string.Empty);
                    updateDateList.Add(null);
                }

                SaveData(idList,
                        glIdList,
                        amountList,
                        remarkList,
                        updateDateList);


                BindDataList(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("数据初始化出错,请联系系统管理员"+Environment.NewLine+ex.Message);
                return;
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }

         
        }


        #endregion

        #region 保存
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsDirty)
            {
                return;
            }

            List<Guid> idList = new List<Guid>();
            List<Guid> glIdList = new List<Guid>();
            List<Decimal> amountList = new List<Decimal>();
            List<string> remarkList = new List<string>();
            List<DateTime?> updateDateList = new List<DateTime?>();

            foreach (FeeYearMonthBudgetList item in DataList)
            {
                idList.Add(item.ID);
                glIdList.Add(item.GLID);
                amountList.Add(item.Amount);
                remarkList.Add(item.Remark);
                updateDateList.Add(item.UpdateDate);
            }


            SaveData(idList,
                    glIdList,
                    amountList,
                    remarkList,
                    updateDateList);


        }
        private void SaveData(List<Guid> idList,
                                List<Guid> glIdList,
                                List<Decimal> amountList,
                                List<string> remarkList ,
                                List<DateTime?> updateDateList)
        {
            try
            {
                ManyResult result= FinanceService.SaveFeeMonthBudgets(idList.ToArray(),
                                                   glIdList.ToArray(),
                                                   amountList.ToArray(),
                                                   remarkList.ToArray(),
                                                   updateDateList.ToArray(),
                                                   CompanyID,
                                                   Year,
                                                   Type,
                                                   LocalData.UserInfo.LoginID);

                //重新加载数据
                BindDataList(false);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),"保存成功!");

            }catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
     
        }


        #endregion

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
        #endregion

        #region 列表事件
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
             FeeYearMonthBudgetList item = gvMain.GetRow(e.RowHandle) as FeeYearMonthBudgetList;
             if (item != null)
             {
                 if (item.ChildCount>0||item.GLCode.Length==6)
                 {
                     e.Appearance.Font = new Font("宋体", 9, FontStyle.Bold);
                 }
                 
             }
        }
        /// <summary>
        /// 不是子节点的行，不允许更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            FeeYearMonthBudgetList item = gvMain.GetRow(e.FocusedRowHandle) as FeeYearMonthBudgetList;
            if (item != null)
            {
                if (item.ChildCount == 0)
                {
                    gvMain.OptionsBehavior.Editable = true;
                }
                else
                {
                    gvMain.OptionsBehavior.Editable = false;
                }
            }
        }

        bool IsUpdateDate = true;
        /// <summary>
        /// 输入的值发生改变时，更新上级的金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (!IsUpdateDate)
            {
                return;
            }
            if (e.Column == colAmount)
            {
                FeeYearMonthBudgetList item = gvMain.GetRow(e.RowHandle) as FeeYearMonthBudgetList;
                if (item!=null)
                {
                    UpdateAmount(item.GLID);

                    TotalInfo();
                    bsList.ResetBindings(false);
                }
            }
        }
        private void UpdateAmount(Guid id)
        {
            //当前行
            FeeYearMonthBudgetList currentRow = DataList.Find(delegate(FeeYearMonthBudgetList item) { return item.GLID == id; });
            if (currentRow == null)
            {
                return;
            }
            //上级行
            FeeYearMonthBudgetList parentRow = DataList.Find(delegate(FeeYearMonthBudgetList item) { return item.GLID == currentRow.ParentID; });
            if (parentRow == null)
            {
                return;
            }

            decimal amount = (from d in DataList where d.ParentID == parentRow.GLID select d.Amount).Sum();
            parentRow.Amount = amount;

            UpdateAmount(parentRow.GLID);
        }

        #endregion

        #region 导出
        private void barImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = cmbCompany.Text + cmbYear.Text + "年费用预算表.xls" ;
            dialog.Filter = "Excel Files(*.xls)|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gvMain.ExportToXls(dialog.FileName,true);
            }
        }
        #endregion



    }
}
