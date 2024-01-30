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
    public partial class SolutionGLConfigListPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
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

        public SolutionGLConfigListPart()
        {
            InitializeComponent();

            if (LocalData.IsEnglish == false) this.SetCnText();
            this.Disposed += delegate {
                this.DataChanged = null;
                this.solutionList = null;
                this.gridView1.InitNewRow -= this.gridView1_InitNewRow;
                this.gridView1.RowCellStyle -= this.gridView1_RowCellStyle;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.dxErrorProvider1.DataSource = null;
                this.gridControl1.DataSource = null;
                this.bindingSource1.PositionChanged -= this.bindingSource1_PositionChanged;
                this.bindingSource1.DataSource = null;
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
            base.OnLoad(e);
        }

        private void InitControls()
        {
            cmbType.Items.Clear();
            cmbGLCode.Items.Clear();
            cmbCurrency .Items.Clear();
            cmbChargingCode.Items.Clear();
            if (solutionList == null || solutionList.ID == Guid.Empty) return;


            List<SolutionChargingCodeList> chargingCodeList = ConfigureService.GetSolutionChargingCodeListBySolutionID(solutionList.ID, true);
            foreach (var item in chargingCodeList)
            {
                cmbChargingCode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (item.ChargingCodeName, item.ChargingCodeID));
            }

            List<SolutionCurrencyList> currencyList = ConfigureService.GetSolutionCurrencyList(solutionList.ID,true);
            foreach (var item in currencyList)
            {
                cmbCurrency.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (item.CurrencyName, item.CurrencyID));
            }

            List<SolutionGLCodeList> solutionGLCodeList = ConfigureService.GetSolutionGLCodeList(solutionList.ID,true);
            foreach (var item in solutionGLCodeList)
            {
                cmbGLCode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (item.Code, item.ID));
            }

            //List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<GLConfigType>> customerTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<GLConfigType>(LocalData.IsEnglish);
            List<GLConfigType> customerTypes = ConfigureService.GetGLConfigTypes();
            foreach (var item in customerTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Index, 0));
            }
        }

        private void SetCnText()
        {
            colType.Caption = "类型";
            colChargingCodeID.Caption = "费用项目";
            colCurrencyID  .Caption = "币种";
            colDRGLCodeID.Caption = "应收会计科目";
            colCRGLCodeID.Caption = "应付会计科目";
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";

            gridView1.NewItemRowText = "点击这里以新增一行.";

            barSave.Caption = "保存(&S)";
            barDisuse.Caption = "作废(&S)";
        }

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
                List<SolutionGLConfigList> list = new List<SolutionGLConfigList>();
                list = ConfigureService.GetSolutionGLConfigList(solutionList.ID, null);

                if (list == null) this.Enabled = false;
                else
                {
                    this.bindingSource1.DataSource = list;
                    
                    this.Enabled = true; 
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }

            InitControls();
            RefreshToolBars();
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

            List<SolutionGLConfigList> currentList = bindingSource1.DataSource as List<SolutionGLConfigList>;

            List<Guid?> ids = new List<Guid?>(), chargeCodeIDs = new List<Guid?>();
            List<Guid> currencyIDs = new List<Guid>(), drGLCodeIDs = new List<Guid>(), crGLCodeIDs = new List<Guid>();
            List<DateTime?> versions = new List<DateTime?>();
            List<int> gLConfigTypes = new List<int>();

            for (int i = 0; i < currentList.Count; i++)
            {
                ids.Add(currentList[i].ID);
                chargeCodeIDs.Add(currentList[i].ChargingCodeID);
                versions.Add(currentList[i].UpdateDate);
                currencyIDs.Add(currentList[i].CurrencyID);
                drGLCodeIDs.Add(currentList[i].DRGLCodeID);
                crGLCodeIDs.Add(currentList[i].CRGLCodeID);
                gLConfigTypes.Add(currentList[i].GLConfigTypeID);
            }
            try
            {
                ManyResultData result = ConfigureService.SaveSolutionGLConfigInfo(solutionList.ID
                                                                                , ids.ToArray()
                                                                                , gLConfigTypes.ToArray()
                                                                                , chargeCodeIDs.ToArray()
                                                                                , currencyIDs.ToArray()
                                                                                , drGLCodeIDs.ToArray()
                                                                                , crGLCodeIDs.ToArray()
                                                                                , LocalData.UserInfo.LoginID
                                                                                , versions.ToArray());


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
            List<SolutionGLConfigList> currentList = bindingSource1.DataSource as List<SolutionGLConfigList>;
            if (solutionList == null || solutionList.ID == Guid.Empty
                || currentList == null || currentList.Count == 0) return false;

            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].Validate(delegate(ValidateEventArgs e)
                {
                }) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        private void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            RefreshToolBars();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var obj = gridView1.GetRow(e.RowHandle);
            if (solutionList == null) throw new ApplicationException("InitNewItem error at SolutionCurrencyUIProxyLogic");

            SolutionGLConfigList newData;
            if (obj == null)
                newData = new SolutionGLConfigList();
            else
                newData = obj as SolutionGLConfigList;
            newData.IsValid = true;
            newData.SolutionID = solutionList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            SolutionGLConfigList data = this.gridView1.GetRow(e.RowHandle) as SolutionGLConfigList;
            if (data == null) return;
            if (data.IsValid == false)
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

        private void cmbChargingCode_Enter(object sender, System.EventArgs e)
        {
            SolutionGLConfigList currentRow = bindingSource1.Current as SolutionGLConfigList;
            if (currentRow == null) return;
            if (currentRow.GLConfigTypeID != 1)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "" : "非费用项目类型，不能设置费用项目值!", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }
        }

        private void barDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SolutionGLConfigList currentRow = bindingSource1.Current as SolutionGLConfigList;
            if (currentRow == null) return;
            if (currentRow.ID == Guid.Empty)
            {
                bindingSource1.RemoveCurrent();
                return;
            }

            SingleResultData result = ConfigureService.ChangeSolutionGLConfigState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
            currentRow.IsValid = !currentRow.IsValid;
            currentRow.UpdateDate = result.UpdateDate;
            RefreshToolBars();
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");
        }

        void RefreshToolBars()
        {
            SolutionGLConfigList currentRow = bindingSource1.Current as SolutionGLConfigList;
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

