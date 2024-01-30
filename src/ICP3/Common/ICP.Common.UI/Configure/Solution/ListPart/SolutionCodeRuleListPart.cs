using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
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
    public partial class SolutionCodeRuleListPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
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

        private string _descriptionTemp = string.Empty;
        SolutionList solutionList = new SolutionList();

        public SolutionCodeRuleListPart()
        {
            InitializeComponent();

            if (LocalData.IsEnglish == false) this.SetCnText();
            this.Disposed += delegate {
                this.DataChanged = null;
                this.dxErrorProvider1.DataSource = null;
                this.gridView1.InitNewRow -= this.gridView1_InitNewRow;
                this.gridView1.RowCellStyle -= this.gridView1_RowCellStyle;
                this.repositoryItemMemoExEdit1.Closed -= this.repositoryItemMemoExEdit1_Closed;
                this.repositoryItemMemoExEdit1.Popup -= this.repositoryItemMemoExEdit1_Popup;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
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
            List<ConfigureKeyList> configureKeyList = ConfigureService.GetConfigureKeyListForType(ConfigureType.SystemNo);
            foreach (var item in configureKeyList)
            {
                cmbConfigureKey.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (item.Code, item.ID));

            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<CodeYearFormart>> customerTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<CodeYearFormart>(LocalData.IsEnglish);
            foreach (var item in customerTypes)
            {
                cmbCodeYear.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        private void SetCnText()
        {
            colCodeMonth.Caption = "月";
            colIsIncludeCompanyCode.Caption = "包括公司代码";
            colCodePrefix.Caption = "前缀";
            colCodeSNLength.Caption = "序列号长度";
            colCodeYear.Caption = "年";
            colConfigureKeyID.Caption = "配置键";
            colDescription.Caption = "描述";
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
                List<SolutionCodeRuleList> list = new List<SolutionCodeRuleList>();
                list = ConfigureService.GetSolutionCodeRuleList(solutionList.ID, null);

                if (list == null) this.Enabled = false;
                else
                {
                    this.bindingSource1.DataSource = list;
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
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

            List<SolutionCodeRuleList> currentList = bindingSource1.DataSource as List<SolutionCodeRuleList>;

            List<Guid?> ids = new List<Guid?>();
            List<Guid> configureKeyIDs = new List<Guid>();
            List<DateTime?> versions = new List<DateTime?>();
            List<string> descriptions = new List<string>(),codePrefixs = new List<string>();
            List<bool> isIncludeCompanyCodes = new List<bool>(),includeCodeMonths = new List<bool>();
            List<short> codeSNLengths = new List<short>();
            List<CodeYearFormart> codeYears = new List<CodeYearFormart>();



            for (int i = 0; i < currentList.Count; i++)
            {
                ids.Add(currentList[i].ID);
                configureKeyIDs.Add(currentList[i].ConfigureKeyID);
                versions.Add(currentList[i].UpdateDate);

                descriptions.Add(currentList[i].Description);
                codePrefixs.Add(currentList[i].CodePrefix);

                isIncludeCompanyCodes.Add(currentList[i].IsIncludeCompanyCode);
                includeCodeMonths.Add(currentList[i].CodeMonth);

                codeSNLengths.Add(currentList[i].CodeSNLength);
                codeYears.Add(currentList[i].CodeYear);
            }
            try
            {
                ManyResultData result = ConfigureService.SaveSolutionCodeRuleInfo(solutionList.ID
                                                                                ,ids.ToArray()
                                                                                ,configureKeyIDs.ToArray()
                                                                                ,descriptions.ToArray()
                                                                                ,isIncludeCompanyCodes .ToArray()
                                                                                ,codePrefixs .ToArray()
                                                                                ,codeYears .ToArray()
                                                                                ,includeCodeMonths .ToArray()
                                                                                ,codeSNLengths .ToArray()
                                                                                ,LocalData.UserInfo.LoginID
                                                                                ,versions.ToArray());
                for (int i = 0; i < currentList.Count; i++)
                {

                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }

                bindingSource1_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private bool ValidateData()
        {
            List<SolutionCodeRuleList> currentList = bindingSource1.DataSource as List<SolutionCodeRuleList>;
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
            SolutionCodeRuleList currentRow = bindingSource1.Current as SolutionCodeRuleList;
            if (currentRow == null || currentRow.IsNew)
            {
                this.barDisuse.Enabled = false;
            }
            else
            {
                this.barDisuse.Enabled = true;

                if (currentRow.IsValid)
                    barDisuse.Caption = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                else
                    barDisuse.Caption = LocalData.IsEnglish ? "Available(&D)" : "激活(&D)";
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var obj = gridView1.GetRow(e.RowHandle);
            if (solutionList == null) throw new ApplicationException("InitNewItem error at SolutionCurrencyUIProxyLogic");

            SolutionCodeRuleList newData;
            if (obj == null)
                newData = new SolutionCodeRuleList();
            else
                newData = obj as SolutionCodeRuleList;
            newData.IsValid = true;
            newData.CodeSNLength = 4;
            newData.SolutionID = solutionList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            SolutionCodeRuleList data = this.gridView1.GetRow(e.RowHandle) as SolutionCodeRuleList;
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

        private void barDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SolutionCodeRuleList currentRow = bindingSource1.Current as SolutionCodeRuleList;
            if (currentRow == null || currentRow.ID ==Guid.Empty) return;
            SingleResultData result = ConfigureService.ChangeSolutionCodeRuleState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
            currentRow.IsValid = !currentRow.IsValid;
            currentRow.UpdateDate = result.UpdateDate;
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");
            if (currentRow.IsValid)
                barDisuse.Caption = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
            else
                barDisuse.Caption = LocalData.IsEnglish ? "Available(&D)" : "激活(&D)";
        }

        void repositoryItemMemoExEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Immediate)
            {
                (sender as MemoExEdit).Text = _descriptionTemp;
            }
        }

        void repositoryItemMemoExEdit1_Popup(object sender, System.EventArgs e)
        {
            _descriptionTemp = (sender as MemoExEdit).Text;
        }

        #endregion     
    }
}

