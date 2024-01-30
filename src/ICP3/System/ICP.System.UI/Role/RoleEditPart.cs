using System;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.Sys.UI.Role
{
    [ToolboxItem(false)]
    public partial class RoleEditPart :ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        #region 服务注入
        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init

        public RoleEditPart()
        {
            InitializeComponent();this.Enabled = false;
            this.Disposed += delegate{
                this.Saved = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        protected override void OnLoad(EventArgs e)
        {
            panelScroll.Click += delegate { panelScroll.Focus(); };
            base.OnLoad(e);
           

        }

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labDescription.Text = "描述";
            labEName.Text = "英文名";

            barSave.Caption = "保存(&S)";
        }

        #endregion

        #region Save

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            this.Validate();
            bindingSource1.EndEdit();
            RoleInfo currentData = bindingSource1.DataSource as RoleInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }


            try
            {
                SingleResultData result = RoleService.SaveRoleInfo(currentData.ID,
                                                currentData.CName,
                                                currentData.EName,
                                                currentData.Description,
                                                LocalData.UserInfo.LoginID,
                                                currentData.UpdateDate);
                currentData.CancelEdit();
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.CancelEdit();
                currentData.BeginEdit();
                if (Saved != null) Saved(currentData, new object[] { result });
                


                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(RoleInfo); this.Enabled = false; }
            else if ((data as RoleInfo).IsValid == false)
            {
                this.Enabled = false;
                this.bindingSource1.DataSource = data;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit();
            }
            else
            {
                if ((data as BaseDataObject).IsNew) 
                {
                    ((DevExpress.XtraBars.Docking.DockPanel)(this.Parent.Parent.Parent.Parent)).ActiveChild = (DevExpress.XtraBars.Docking.DockPanel)this.Parent.Parent.Parent;
                    txtCName.Focus(); 
                }

                this.bindingSource1.DataSource = data;
                this.Enabled = true;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
            }
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
    }
}
