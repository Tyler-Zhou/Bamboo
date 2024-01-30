using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.UI.Configure.Solution
{
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class SolutionExchangeRateListPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region init

        SolutionList solutionList = new SolutionList();

        public SolutionExchangeRateListPart()
        {
            InitializeComponent();

            if (LocalData.IsEnglish == false) this.SetCnText();
            this.Disposed += delegate
            {
                this.solutionList = null;
                this.DataChanged = null;
                this.dxErrorProvider1.DataSource = null;
                this.gridView1.InitNewRow -= this.gridView1_InitNewRow;
                this.gridView1.RowCellStyle -= this.gridView1_RowCellStyle;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;

                this.gridControl1.DataSource = null;
                this.bindingSource1.PositionChanged -= this.bindingSource1_PositionChanged;
                this.bindingSource1.Dispose();
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void InitCurrency(SolutionList solution)
        {
            cmbCruuency.Properties.Items.Clear();
            if (solution == null || solution.ID == Guid.Empty) return;

            List<SolutionCurrencyList> currencys = ConfigureService.GetSolutionCurrencyList(solution.ID, true);
            foreach (var item in currencys)
            {
                cmbCruuency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (item.CurrencyName, item.CurrencyID));
            }
        }

        private void SetCnText()
        {
            colSourceCurrencyID.Caption = "源币种";
            colTargetCurrency.Caption = "目标币种";
            colToDate.Caption = "结束日期";
            colFromDate.Caption = "开始日期";
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
            colRate .Caption = "汇率";
            gridView1.NewItemRowText = "点击这里以新增一行.";

            barSave.Caption = "保存(&S)";
            barDisuse.Caption = "作废(&S)";
        }

        public void BindingData(object data)
        {
            SolutionList newSolutionList = data as SolutionList;
            InitCurrency(newSolutionList);
            if (newSolutionList == null || newSolutionList.ID == Guid.Empty)
            {
                this.Enabled = false;
            }
            else if (newSolutionList.ID != (solutionList == null ? Guid.Empty : solutionList.ID))
            {
                solutionList = newSolutionList;
                List<SolutionExchangeRateList> list = new List<SolutionExchangeRateList>();
                list = ConfigureService.GetSolutionExchangeRateList(solutionList.ID, null);

                if (list == null)
                {
                    list = new List<SolutionExchangeRateList>();
                    this.Enabled = false;
                }
                else
                {
                    list = (from d in list where d.ExchangeType == ExchangeType.Bill select d).ToList();
                    this.bindingSource1.DataSource = list;
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }

            this.RefreshToolBars();
        }

        #endregion

        #region IDataContentPart 成员
        public bool AutoWidth { get; set; }
        public object Current { get { return null; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return this.bindingSource1.DataSource; }
            set { BindingData(value); }
        }


        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion

        #region event

        #region Save

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            this.EndEdit();
            if (this.ValidateData() == false) return;

            List<SolutionExchangeRateList> currentList = bindingSource1.DataSource as List<SolutionExchangeRateList>;

            List<Guid?> ids = new List<Guid?>();
            List<Guid> sourceCurrencyIDs = new List<Guid>() ,targetCurrencyIDs = new List<Guid>();;
            List<DateTime?> versions = new List<DateTime?>();
            List<DateTime> fromDates = new List<DateTime>(),toDates = new List<DateTime>();
            List<decimal> rates = new List<decimal>();

            for (int i = 0; i < currentList.Count; i++)
            {
                ids.Add(currentList[i].ID);
                sourceCurrencyIDs.Add(currentList[i].SourceCurrencyID );
                targetCurrencyIDs.Add(currentList[i].TargetCurrencyID);
                versions.Add(currentList[i].UpdateDate);

                fromDates.Add(currentList[i].FromDate);
                toDates.Add(currentList[i].ToDate);

                rates.Add(currentList[i].Rate);
            }
            try
            {
                ManyResultData result = ConfigureService.SaveSolutionExchangeRateInfo(solutionList.ID
                                                                                ,ExchangeType.Bill
                                                                                ,ids.ToArray()
                                                                                ,sourceCurrencyIDs.ToArray()
                                                                                ,targetCurrencyIDs.ToArray()
                                                                                ,fromDates .ToArray()
                                                                                ,toDates .ToArray()
                                                                                ,rates .ToArray()
                                                                                ,LocalData.UserInfo.LoginID
                                                                                ,versions.ToArray());
                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private bool ValidateData()
        {
            List<SolutionExchangeRateList> currentList = bindingSource1.DataSource as List<SolutionExchangeRateList>;
            if (solutionList == null || solutionList.ID == Guid.Empty
                || currentList == null || currentList.Count == 0) return false;

            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].Validate(delegate(ValidateEventArgs e)
                {
                    if (currentList[i].Rate == 0)
                    {
                        e.SetErrorInfo("Rate", LocalData.IsEnglish ? "Rate can't be 0." : "汇率不能为0.");
                    }

                    if (currentList[i].ToDate < currentList[i].FromDate)
                    {
                        e.SetErrorInfo("ToDate", LocalData.IsEnglish ? "FromDatecan't biger ToDate. " : "结束日期不能小于开始日期.");
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

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var obj = gridView1.GetRow(e.RowHandle);
            if (solutionList == null) throw new ApplicationException("InitNewItem error at SolutionCurrencyUIProxyLogic");

            SolutionExchangeRateList newData;
            if (obj == null)
                newData = new SolutionExchangeRateList();
            else
                newData = obj as SolutionExchangeRateList;

            newData.IsValid = true;
            newData.SolutionID = solutionList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.FromDate =  new DateTime(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Month, 1);
            newData.ToDate = new DateTime(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(1).Year, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(1).Month , 1);

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            SolutionExchangeRateList data = this.gridView1.GetRow(e.RowHandle) as SolutionExchangeRateList;
            if (data == null) return;
            if (data.IsValid==false)
            {
                CommonUtility.SetRowDisuseStyle(e.Appearance);
            }
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        private void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            this.RefreshToolBars();
        }

        private void barDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SolutionExchangeRateList currentRow = bindingSource1.Current as SolutionExchangeRateList;
            if (currentRow == null) return;

            if (currentRow.IsNew)
            {
                bindingSource1.RemoveCurrent();
                return;
            }

            SingleResultData result = ConfigureService.ChangeSolutionExchangeRateState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
            currentRow.IsValid = !currentRow.IsValid;
            currentRow.UpdateDate = result.UpdateDate;
            this.RefreshToolBars();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");
           
        }

        void RefreshToolBars()
        {
            SolutionExchangeRateList currentRow = bindingSource1.Current as SolutionExchangeRateList;
            if (currentRow == null)
            {
                this.barDisuse.Enabled = false;
            }
            else
            {
                this.barDisuse.Enabled = true;

                if (currentRow.ID == Guid.Empty)
                {
                    barDisuse.Caption = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                }
                else
                {
                    if (currentRow.IsValid)
                        barDisuse.Caption = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    else
                        barDisuse.Caption = LocalData.IsEnglish ? "Available(&D)" : "激活(&D)";
                }
            }

            if (this.bindingSource1.Count > 0)
            {
                this.barSave.Enabled = true;
            }
            else
            {
                this.barSave.Enabled = false;
            }

        }

        #endregion

       
    }
}


