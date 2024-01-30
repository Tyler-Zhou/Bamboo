using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

namespace ICP.FAM.UI
{
    public partial class AdjustRateListPart : BaseListPart
    {
        public AdjustRateListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
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
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindDataSource(value);

            }
        }
        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }
        /// <summary>
        /// 当前行数据
        /// </summary>
        private SolutionExchangeRateList CurrentData
        {
            get
            {
                return bsList.Current as SolutionExchangeRateList;
            }
        }
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get;
            set;
        }

        /// <summary>
        /// 财务关帐日
        /// </summary>
        public DateTime AccountingClosingdate
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="value"></param>
        private void BindDataSource(object value)
        {
            List<SolutionExchangeRateList> list = value as List<SolutionExchangeRateList>;
            if (list == null)
            {
                list = new List<SolutionExchangeRateList>();
            }

            bsList.DataSource = list;
            bsList.ResetBindings(false);

            RefreshToolBars();
        }
        private void RefreshToolBars()
        {

            if (CurrentData == null)
            {
                barDisuse.Enabled = false;
                barCopy.Enabled = false;
            }
            else
            {
                barDisuse.Enabled = true;
                barCopy.Enabled = true;

                if (CurrentData.ID == Guid.Empty)
                {
                    barDisuse.Caption = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                }
                else
                {
                    if (CurrentData.IsValid)
                        barDisuse.Caption = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    else
                        barDisuse.Caption = LocalData.IsEnglish ? "Available(&D)" : "激活(&D)";
                }

                if (CurrentData.ToDate <= AccountingClosingdate)
                {
                    //结束日期小于会计关账日，不允许再改
                    barDisuse.Enabled = false;
                    gvMain.OptionsBehavior.ReadOnly = true;
                    gvMain.OptionsBehavior.Editable = false;
                }
                else
                {
                    barDisuse.Enabled = true;
                    gvMain.OptionsBehavior.ReadOnly = false;
                    gvMain.OptionsBehavior.Editable = true;
                }
            }

            if (bsList.Count > 0)
            {
                barSave.Enabled = true;
            }
            else
            {
                barSave.Enabled = false;
            }
        }

        #endregion

        #region 窗体事件
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            RefreshToolBars();
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
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);

            dteFromDate.DateTime = DateTime.Now.AddYears(-3);
            dteToDate.DateTime = DateTime.Now;


            #region 绑定币种
            cmbCruuency.Properties.Items.Clear();
            cmbSourceCurrencyID.Properties.Items.Clear();
            cmbTargetCurrency.Properties.Items.Clear();

            cmbSourceCurrencyID.Properties.Items.Add(new ImageComboBoxItem(null, ""));
            cmbTargetCurrency.Properties.Items.Add(new ImageComboBoxItem(null, ""));

            List<CurrencyList> currencys = ConfigureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
            foreach (var item in currencys)
            {
                cmbCruuency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                cmbSourceCurrencyID.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                cmbTargetCurrency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            #endregion

            #region 绑定列表
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (configure == null)
            {
                XtraMessageBox.Show("读取公司配置时出错,请联系系统管理员!");
                return;
            }
            SolutionID = configure.SolutionID;

            AccountingClosingdate = configure.AccountingClosingdate.Value;

            SearchDate();

            #endregion
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            SolutionExchangeRateList data = gvMain.GetRow(e.RowHandle) as SolutionExchangeRateList;
            if (data == null) return;
            if (data.IsValid == false)
            {
                e.Appearance.ForeColor = Color.Gray;
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.Options.UseFont = true;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
            }
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            SolutionExchangeRateList newData = new SolutionExchangeRateList();
            newData.ExchangeType = ExchangeType.Bill;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateDate = DateTime.Now;
            newData.IsValid = true;
            newData.CreateByName = LocalData.UserInfo.UserName;
            newData.FromDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            newData.ToDate = DateTime.Now.AddYears(10);

            bsList.Insert(0, newData);

            RefreshToolBars();
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentData == null)
            {
                return;
            }
            SolutionExchangeRateList newData = new SolutionExchangeRateList();
            newData.ExchangeType = ExchangeType.Bill;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateDate = DateTime.Now;
            newData.IsValid = true;
            newData.CreateByName = LocalData.UserInfo.UserName;
            newData.FromDate = CurrentData.FromDate;
            newData.ToDate = CurrentData.ToDate.AddYears(10);
            newData.SourceCurrency = CurrentData.SourceCurrency;
            newData.SourceCurrencyID = CurrentData.SourceCurrencyID;
            newData.TargetCurrency = CurrentData.TargetCurrency;
            newData.TargetCurrencyID = CurrentData.TargetCurrencyID;
            newData.SolutionID = SolutionID;

            bsList.Insert(0, newData);

            RefreshToolBars();
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barInvalid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentData == null)
            {
                return;
            }
            if (CurrentData.IsNew)
            {
                bsList.RemoveCurrent();
                return;
            }

            SingleResultData resultData = ConfigureService.ChangeSolutionExchangeRateState(CurrentData.ID, !CurrentData.IsValid, LocalData.UserInfo.LoginID, CurrentData.UpdateDate);
            CurrentData.IsValid = !CurrentData.IsValid;
            CurrentData.UpdateDate = resultData.UpdateDate;

            bsList.ResetBindings(false);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");

            RefreshToolBars();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveData();
            RefreshToolBars();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region 保存
        private void SaveData()
        {
            Validate();
            bsList.EndEdit();


            if (ValidateData() == false) return;

            List<SolutionExchangeRateList> currentList = bsList.DataSource as List<SolutionExchangeRateList>;

            currentList = (from d in currentList where d.IsDirty select d).ToList();

            if (currentList == null || currentList.Count == 0)
            {
                return;
            }

            List<Guid?> ids = new List<Guid?>();
            List<Guid> sourceCurrencyIDs = new List<Guid>(), targetCurrencyIDs = new List<Guid>(); ;
            List<DateTime?> versions = new List<DateTime?>();
            List<DateTime> fromDates = new List<DateTime>(), toDates = new List<DateTime>();
            List<decimal> rates = new List<decimal>();

            for (int i = 0; i < currentList.Count; i++)
            {
                ids.Add(currentList[i].ID);
                sourceCurrencyIDs.Add(currentList[i].SourceCurrencyID);
                targetCurrencyIDs.Add(currentList[i].TargetCurrencyID);
                versions.Add(currentList[i].UpdateDate);
                fromDates.Add(currentList[i].FromDate);
                toDates.Add(currentList[i].ToDate);
                rates.Add(currentList[i].Rate);
            }

            AdjustRateSaveRequest request = new AdjustRateSaveRequest();
            request.Ids = ids;
            request.SourceCurrencyIDs = sourceCurrencyIDs;
            request.TargetCurrencyIDs = targetCurrencyIDs;
            request.Versions = versions;
            request.FromDates = fromDates;
            request.ToDates = toDates;
            request.Rates = rates;
            request.SolutionID = SolutionID;
            request.SaveByID = LocalData.UserInfo.LoginID;

            try
            {
                ManyResultData result = FinanceService.SaveRateList(request);

                SearchDate();

                bsList.ResetBindings(false);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        private bool ValidateData()
        {
            List<SolutionExchangeRateList> currentList = bsList.DataSource as List<SolutionExchangeRateList>;
            if (currentList == null || currentList.Count == 0) return false;

            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].Validate(delegate(ValidateEventArgs e)
                {
                    if (currentList[i].Rate == 0)
                    {
                        e.SetErrorInfo("Rate", LocalData.IsEnglish ? "Rate can't be 0." : "汇率不能为0.");
                    }

                    if (currentList[i].SourceCurrencyID == currentList[i].TargetCurrencyID)
                    {
                        e.SetErrorInfo("TargetCurrencyID", LocalData.IsEnglish ? "TargetCurrency can't same as SourceCurrency." : "目标币种不能和源币种相同.");
                    }

                }) == false)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion



        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDate();
        }

        private void SearchDate()
        {
            Guid? SourceCurrencyID = null, TargetCurrencyID = null;
            DateTime FromDate = dteFromDate.DateTime, ToDate = dteToDate.DateTime;

            if (cmbSourceCurrencyID.SelectedIndex > 0 && cmbSourceCurrencyID.EditValue != null)
            {
                SourceCurrencyID = new Guid(cmbSourceCurrencyID.EditValue.ToString());
            }
            if (cmbTargetCurrency.SelectedIndex > 0 && cmbTargetCurrency.EditValue != null)
            {
                TargetCurrencyID = new Guid(cmbTargetCurrency.EditValue.ToString());
            }

            List<SolutionExchangeRateList> DataList = ConfigureService.GetSolutionExchangeRateList(SolutionID, true);


            List<SolutionExchangeRateList> list = (from d in DataList
                                                   where (SourceCurrencyID == null || SourceCurrencyID == d.SourceCurrencyID)
                                                       && (TargetCurrencyID == null || TargetCurrencyID == d.TargetCurrencyID)
                                                       && (d.FromDate >= FromDate || d.ToDate <= ToDate)
                                                   orderby d.FromDate descending
                                                   select d).ToList();

            DataSource = list;

        }
        #endregion

    }
}
