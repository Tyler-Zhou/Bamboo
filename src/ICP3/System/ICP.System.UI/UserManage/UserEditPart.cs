using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class UserEditPart : ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        #region 服务注入

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region init
        public UserEditPart()
        {
            InitializeComponent();this.Enabled = false;
            this.Disposed += delegate {
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

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labEName.Text = "英文名";
            labFax.Text = "传真";
            labMobile.Text = "手机";
            labGender.Text = "性别";
            labTel.Text = "电话";

            barSave.Caption = "保存(&S)";
        }
        protected override void OnLoad(EventArgs e)
        {
            panelScroll.Click += delegate { panelScroll.Focus(); };
            InitControls();
            base.OnLoad(e);
        }

        private void InitControls()
        {
            cmbSex.Properties.Items.Clear();
            List<EnumHelper.ListItem<GenderType>> types = EnumHelper.GetEnumValues<GenderType>(LocalData.IsEnglish);
            this.cmbSex.Properties.BeginUpdate();
            foreach (var item in types)
            {
                cmbSex.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(item.Value, item.Name));
            }
            this.cmbSex.Properties.EndUpdate();
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
            if (data == null) { this.bindingSource1.DataSource = typeof(UserInfo); this.Enabled = false; }
            else if ((data as UserInfo).IsValid == false) 
            { 
                this.Enabled = false;
                this.bindingSource1.DataSource = data;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); 
            }
            else
            {
                if ((data as ICP.Framework.CommonLibrary.Common.BaseDataObject).IsNew)
                {
                    ((DevExpress.XtraBars.Docking.DockPanel)(this.Parent.Parent.Parent.Parent)).ActiveChild = (DevExpress.XtraBars.Docking.DockPanel)this.Parent.Parent.Parent;
                    txtCode.Focus();
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

        public void Init(bool isEnabled)
        {
            if (!isEnabled)
            {
                this.txtCName.Properties.ReadOnly = true;
                this.txtEName.Properties.ReadOnly = true;
                this.txtCode.Properties.ReadOnly = true;
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region Save

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }
        bool Save()
        {
            this.Validate();
            bindingSource1.EndEdit();
            UserInfo currentData = bindingSource1.DataSource as UserInfo;
            if (currentData == null || currentData.Validate() == false) return false;

            try
            {
                SingleResultData result = UserService.SaveUserInfo(currentData.ID, currentData.Code, currentData.CName,
                                        currentData.EName, currentData.Gender, currentData.Tel,
                                        currentData.Fax, currentData.Mobile,
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
    }
}
