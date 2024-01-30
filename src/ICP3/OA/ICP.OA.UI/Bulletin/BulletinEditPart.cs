using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.OA.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary;

namespace ICP.OA.UI.Bulletin
{   
    /// <summary>
    /// 公告编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class BulletinEditPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IBulletinService BulletinService
        {
            get
            {
                return ServiceClient.GetService<IBulletinService>();
            }
        }


        public BulletinUIDataHelper BulletinUIDataHelper
        {
            get
            {
                return ClientHelper.Get<BulletinUIDataHelper, BulletinUIDataHelper>();
            }
        }

        #endregion

        #region 初始化

        public BulletinEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this._CurrentData = null;
                if (this.bindingSource1 != null)
                {
                    this.bindingSource1.DataSource = null;
                    this.bindingSource1 = null;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) InitMessage();
        }

        private void InitMessage()
        {
            this.RegisterMessage("SaveSuccessfully", LocalData.IsEnglish?"Save Successfully.":"保存成功");

            this.RegisterMessage("ValidateDepartments",LocalData.IsEnglish? "Departments Must Input.":"部门必须输入");
            this.RegisterMessage("ValidateSubject",LocalData.IsEnglish? "Subject Must Input.":"主题必须输入");
            this.RegisterMessage("ValidateContent", LocalData.IsEnglish?"Content Must Input.":"内容必须输入");
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            List<EnumHelper.ListItem<BulletinPriority>> prioritys = EnumHelper.GetEnumValues<BulletinPriority>(LocalData.IsEnglish);
            foreach (var item in prioritys)
            {
                cmbPriority.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            List<BulletinTypeData> types = BulletinUIDataHelper.BulletinTypes;
            foreach (var item in types)
            {
                cmbBulletinType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                    (LocalData.IsEnglish ? item.EName:item.CName
                    , item.ID));
            }

            List<OrganizationTreeData> organizationTreeDatas = BulletinUIDataHelper.OrganizationTreeDatas;
            List<TreeCheckControlSource> tcs = new List<TreeCheckControlSource>();
            string name = string.Empty;
            foreach (var item in organizationTreeDatas)
            {
                TreeCheckControlSource t = new TreeCheckControlSource(item.ID, item.ParentID, LocalData.IsEnglish ? item.EName : item.CName);
                tcs.Add(t);
                if (_CurrentData.Departments != null && _CurrentData.Departments.Contains(t.ID))
                {
                    if (name.Length > 0) name += ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol;

                    name += LocalData.IsEnglish ? item.EName : item.CName;
                }
                    
            }
            treeCheckDep.SetSource(tcs);
            treeCheckDep.EditText = name;
            treeCheckDep.EditValue = _CurrentData.Departments;
        }

        #endregion

        #region 

        BulletinData _CurrentData = null;

        private bool Save()
        {
            if (!this.btnSave.Visible || !this.btnSave.Enabled)
            {
                return false;
            }

            if (this.ValidateData() == false) return false;

            try
            {
                Guid id  = BulletinService.SaveBulletin(_CurrentData.ID
                                              , _CurrentData.Subject
                                              , _CurrentData.Content
                                              , _CurrentData.FromTime
                                              , _CurrentData.ToTime
                                              , _CurrentData.Departments.ToArray()
                                              , treeCheckDep.EditText
                                              , LocalData.UserInfo.LoginID
                                              , _CurrentData.Priority
                                              , _CurrentData.BulletinType);

                if (_CurrentData.ID.IsNullOrEmpty())
                {
                    _CurrentData.ID = id;
                    _CurrentData.CreateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                }

                if (this.Saved != null) this.Saved(_CurrentData);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this, NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex.Message); return false; }
        }

        private bool ValidateData()
        {
            this.EndEdit();

            if (_CurrentData.Validate(delegate(ValidateEventArgs e)
            {
                if (_CurrentData.Departments == null || _CurrentData.Departments.Count == 0)
                    e.SetErrorInfo("Departments", NativeLanguageService.GetText(this, "ValidateDepartments"));
                
                if (_CurrentData.Subject.IsNullOrEmpty())
                    e.SetErrorInfo("Subject", NativeLanguageService.GetText(this, "ValidateSubject"));

                if (_CurrentData.Content.IsNullOrEmpty())
                    e.SetErrorInfo("Content", NativeLanguageService.GetText(this, "ValidateContent"));

            }) == false) return false;

            return true;
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            _CurrentData = data as BulletinData;
            this.bindingSource1.DataSource = _CurrentData;

            if (DataTypeHelper.GetString(_CurrentData.Publisher, string.Empty).ToUpper() != LocalData.UserInfo.LoginName.ToUpper()
                   && DataTypeHelper.GetString(_CurrentData.Publisher, string.Empty).ToUpper() != LocalData.UserInfo.UserName.ToUpper()
                   && DataTypeHelper.GetString(_CurrentData.Publisher, string.Empty).ToUpper() != LocalData.UserInfo.UserEname.ToUpper())
            {
                this.btnSave.Visible = false;
                this.btnSave.Enabled = false;
            }
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            this.Validate();
            _CurrentData.Departments = treeCheckDep.EditValue;
            bindingSource1.EndEdit();

        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region btn

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion
    }
}
