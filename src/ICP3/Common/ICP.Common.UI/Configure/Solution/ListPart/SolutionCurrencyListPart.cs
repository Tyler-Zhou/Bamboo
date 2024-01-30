using System;
using System.Collections.Generic;
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
    public partial class SolutionCurrencyListPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
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

        SolutionList solutionList = new SolutionList();

        #region init 

        public SolutionCurrencyListPart()
        {
            InitializeComponent();

            if (LocalData.IsEnglish == false) this.SetCnText();
            this.Disposed += delegate
            {
                this.DataChanged = null;
                this.solutionList = null;
                this.gridView1.InitNewRow -= this.gridView1_InitNewRow;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.dxErrorProvider1.DataSource = null;
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
            InitControls();

            bindingSource1.PositionChanged += new EventHandler(bindingSource1_PositionChanged);
            base.OnLoad(e);
        }

        void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            RefreshToolBars();
        }

        private void InitControls()
        {
            List<CurrencyList> list = ConfigureService.GetCurrencyList(string.Empty,
                                                                       string.Empty,
                                                                       null,true,0);
            foreach (var item in list)
            {
                cmbCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }
        }

        private void SetCnText()
        {
            colCurrencyID.Caption = "币种";

            barDelete.Caption = "删除(&D)";
            barSave.Caption = "保存(&S)";
            gridView1.NewItemRowText = "点击这里以新增一行.";
        }

        #endregion

        public void BindingData(object data)
        {
            SolutionList newSolutionList = data as SolutionList;

            if (newSolutionList == null || newSolutionList.ID == Guid.Empty)
            {
                this.Enabled = false;
            }
            else if (newSolutionList.ID != (solutionList == null ? Guid.Empty : solutionList.ID))
            {
                solutionList = newSolutionList;
                List<SolutionCurrencyList> list = new List<SolutionCurrencyList>();
                list = ConfigureService.GetSolutionCurrencyList(solutionList.ID, null);
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();

                this.bindingSource1.DataSource = list;
                this.Enabled = true;
            }

            RefreshToolBars();
        }

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

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var obj = gridView1.GetRow(e.RowHandle);
            if (solutionList == null) throw new ApplicationException("InitNewItem error at SolutionCurrencyUIProxyLogic");

            SolutionCurrencyList newData;
            if (obj == null)
                newData = new SolutionCurrencyList();
            else
                newData = obj as SolutionCurrencyList;

            newData.SolutionID = solutionList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SolutionCurrencyList currentRow = bindingSource1.Current as SolutionCurrencyList;

            if (currentRow == null) return;
            if (CommonUtility.EnquireIsDeleteCurrentData())
            {
                if (currentRow.ID != Guid.Empty)
                    ConfigureService.RemoveSolutionCurrencyInfo(currentRow.ID, LocalData.UserInfo.LoginID, currentRow.UpdateDate);

                bindingSource1.RemoveCurrent();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
            }
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EndEdit();
            if (this.ValidateData() == false) return;

            List<SolutionCurrencyList> currentList = bindingSource1.DataSource as List<SolutionCurrencyList>;

            List<Guid> solutionCurrencyIds = new List<Guid>();
            List<Guid> currencyIds = new List<Guid>();
            List<DateTime?> versions = new List<DateTime?>();

            for (int i = 0; i < currentList.Count; i++)
            {
                solutionCurrencyIds.Add(currentList[i].ID);
                currencyIds.Add(currentList[i].CurrencyID);
                versions.Add(currentList[i].UpdateDate);
            }
            try
            {
                ManyResultData result = ConfigureService.SaveSolutionCurrencyInfo(solutionList.ID,
                                                                                solutionCurrencyIds.ToArray(),
                                                                                currencyIds.ToArray(),
                                                                                LocalData.UserInfo.LoginID,
                                                                                versions.ToArray());
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
            List<SolutionCurrencyList> currentList = bindingSource1.DataSource as List<SolutionCurrencyList>;
            if (solutionList == null || solutionList.ID ==Guid.Empty
                ||currentList == null || currentList.Count == 0) return false;

            List<Guid>  existCurrencyIds = new List<Guid>();
            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].Validate(delegate(ValidateEventArgs e)
                {
                    if (existCurrencyIds.Contains(currentList[i].CurrencyID))
                    {
                        e.SetErrorInfo("CurrencyID", LocalData.IsEnglish ? "Currency exist" : "币种重复");
                    }
                }) == false)
                {
                    return false;
                }
                existCurrencyIds.Add(currentList[i].CurrencyID);
            }

            return true;
        }

        void RefreshToolBars()
        {
            SolutionCurrencyList currentRow = bindingSource1.Current as SolutionCurrencyList;
            if (currentRow == null)
            {
                this.barDelete.Enabled = false;
            }
            else
            {
                this.barDelete.Enabled = true;

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

    }
}
