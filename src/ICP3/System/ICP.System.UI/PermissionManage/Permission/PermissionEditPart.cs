using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.PermissionManage
{
    [ToolboxItem(false)]
    public partial class PermissionEditPart : ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        #region 服务注入

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始

        public PermissionEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.barClose.ItemClick -= this.barClose_ItemClick;
                this.barSave.ItemClick -= this.barSave_ItemClick;
                this.popFunction.QueryPopUp -= this.popFunction_QueryPopUp;
                this.treeFunction.DoubleClick -= this.treeFunction_DoubleClick;
                this.dxErrorProvider1.DataSource = null;
                this.bsFunction.DataSource = null;
                this.bsFunction.Dispose();
                this.Saved = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            barClose.Caption = "关闭(&C)";
            barSave.Caption = "保存(&S)";

            colCName.Caption = "中文名";
            colEName.Caption = "英文名";
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labDescription.Text = "描述";
            labEName.Text = "英文名";
            labFunction.Text = "功能项";

        }

        void popFunction_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popFunction.QueryPopUp -= new CancelEventHandler(popFunction_QueryPopUp);
            List<FunctionList> functionList = PermissionService.GetFunctionList(true);
            bsFunction.DataSource = functionList;
        }

        FunctionList CurrentFunction { get { return bsFunction.Current as FunctionList; } }

        private void treeFunction_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentFunction == null 
                || CurrentFunction.FunctionType == FunctionType.Module
                || CurrentFunction.FunctionType == FunctionType.Action) return;

            UIItemInfo editData = bindingSource1.DataSource as UIItemInfo;

            editData.FunctionID = CurrentFunction.ID;
            editData.CName = CurrentFunction.CName;
            editData.Code = CurrentFunction.Code;
            editData.EName = CurrentFunction.EName;
            editData.Description = CurrentFunction.Description;
            popFunction.Text =LocalData.IsEnglish? CurrentFunction.EName:CurrentFunction.CName;
            this.panel1.Enabled = true;

            popFunction.ClosePopup();
            
        }

        private void InitControls()
        {
            UIItemInfo currentData = bindingSource1.DataSource as UIItemInfo;
            if (currentData.FunctionType == UIConfigItemType.MenuGroup)
            {
                this.panel1.Enabled = true;
                this.labFunction.Visible = this.popFunction.Visible = false;
                this.panel1.Dock = DockStyle.Top;
            }
            else
            {
                if (currentData.FunctionID == null || currentData.FunctionID == Guid.Empty)
                {
                    this.panel1.Enabled = false;
                    this.popFunction.QueryPopUp += new CancelEventHandler(popFunction_QueryPopUp);
                }
                else
                {
                    this.panel1.Dock = DockStyle.Top;
                    this.labFunction.Visible = this.popFunction.Visible = false;
                }

            }

            if (LocalData.IsEnglish)
                colEName.Visible = true;
            else
                colCName.Visible = true;
        }

        #endregion

        #region bar

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void BindingData(object data)
        {
            this.bindingSource1.DataSource = data;
            InitControls();
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region Save

        bool Save()
        {
            this.Validate();
            bindingSource1.EndEdit();

            UIItemInfo currentData = bindingSource1.DataSource as UIItemInfo;
            if (currentData == null) return false;

            if (currentData.Validate() == false)
            {
                return false;
            }

            try
            {
                bool isNew = currentData.ID == Guid.Empty;

                SingleHierarchyResultData result = PermissionService.SaveUIConfigurationInfo(
                    currentData.ID,
                    currentData.ParentID,
                    currentData.Code,
                    currentData.CName,
                    currentData.EName,
                    currentData.Description,
                    currentData.FunctionID,
                    LocalData.UserInfo.LoginID,
                    currentData.UpdateDate);

                currentData.ID = result.ID;
                currentData.HierarchyCode = result.HierarchyCode;
                currentData.UpdateDate = result.UpdateDate;
                if (this.Saved != null) this.Saved(currentData);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                if (isNew) this.FindForm().Close();
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        #endregion
    }
}
