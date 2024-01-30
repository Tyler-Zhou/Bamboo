using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.Organization
{
    [ToolboxItem(false)]
    public partial class OrganizationEditPart : BaseEditPart
    {
        #region 服务注入
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化

        public OrganizationEditPart()
        {
            InitializeComponent(); this.Enabled = false;
            this.Disposed += delegate {
                this.dxErrorProvider1.DataSource = null;
                this.Saved = null;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                this.treeParent.DataSource = null;
                this.treeParent.DoubleClick -= this.treeParent_DoubleClick;
                this.bsParent.DataSource = null;
                this.bsParent.Dispose();
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this._originalList = null;
                if (this.popParent != null)
                {
                    this.popParent.QueryPopUp -= this.popParent_QueryPopUp;
                }
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
            labParent.Text = "上级";
            labType.Text = "类型";
            btnClearPop.Text = "清空";

            barSave.Caption = "保存(&S)";
            colCShortName.Caption = "名称";
            colCShortName.Visible = true;
            colEShortName.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            panelScroll.Click += delegate { panelScroll.Focus(); };
            InitControls();
            base.OnLoad(e);
        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OrganizationType>> types = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OrganizationType>(LocalData.IsEnglish);
            this.cmbType.Properties.BeginUpdate();
            this.cmbType.Properties.Items.Clear();
            foreach (var type in types)
            {
                this.cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(type.Name, type.Value));
            }
            this.cmbType.Properties.EndUpdate();
        }
        private void InitControls()
        {
            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;
           
            this.popParent.QueryPopUp += new CancelEventHandler(popParent_QueryPopUp);
        }

        #endregion

        #region Property
        List<OrganizationList> _originalList = null;
        OrganizationInfo CurrentData
        {
            get { return bindingSource1.DataSource as OrganizationInfo; }
        }

        OrganizationList CurrentOrganizationList
        {
            get { return bsParent.Current as OrganizationList; }
        }

        #endregion

        #region Pop
        Guid? _initID = null;

        void popParent_QueryPopUp(object sender, CancelEventArgs e)
        {
            List<OrganizationList> data = new List<OrganizationList>();
            OrganizationInfo currentData = bindingSource1.DataSource as OrganizationInfo;
            if (currentData.ID != Guid.Empty)
            {
                if (_initID != null && _initID.Value == currentData.ID) return;

                List<Guid> needRemoveIds = GetChildIdsById(_originalList, currentData.ID);
                List<OrganizationList> needAddChilds = _originalList.FindAll(delegate(OrganizationList item) { return needRemoveIds.Contains(item.ID) == false; });
                foreach (var item in needAddChilds)
                {
                    data.Add(item);
                }
                _initID = currentData.ID;
            }
            else
            {
                _initID = null;
                foreach (var item in _originalList)
                {
                    data.Add(item);
                }
            }

            bsParent.DataSource = data;
            treeParent.ExpandAll();
        }

        /// <summary>
        /// 获取所有子项(包括自身)ID
        /// </summary>
        List<Guid> GetChildIdsById(List<OrganizationList> data, Guid currentId)
        {
            List<Guid> childIds = new List<Guid>();
            childIds.Add(currentId);

            while (true)
            {
                List<OrganizationList> childs = data.FindAll(delegate(OrganizationList item)
                { return childIds.Contains(item.ParentID == null ? Guid.Empty : item.ParentID.Value) && childIds.Contains(item.ID) == false; });

                if (childs == null || childs.Count == 0)
                    break;
                else
                {
                    foreach (OrganizationList item in childs)
                    {
                        childIds.Add(item.ID);
                    }
                }
            }
            return childIds;
        }


        private void treeParent_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentOrganizationList != null)
            {
                OrganizationInfo currentData = bindingSource1.DataSource as OrganizationInfo;
                currentData.ParentID = CurrentOrganizationList.ID;
                currentData.ParentName = LocalData.IsEnglish ? CurrentOrganizationList.EShortName : CurrentOrganizationList.CShortName;
            }
            popParent.ClosePopup();
        }

        private void btnClearPop_Click(object sender, EventArgs e)
        {
            OrganizationInfo currentData = bindingSource1.DataSource as OrganizationInfo;
            currentData.ParentID = null;
            currentData.ParentName = string.Empty;
            popParent.ClosePopup();
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            _initID = null;
            OrganizationInfo info = data as OrganizationInfo;

            #region 更新了IsValid 刷新父节点列表
            OrganizationInfo currentInfo = this.bindingSource1.DataSource as OrganizationInfo;
            if (currentInfo != null && info != null && currentInfo.ID == info.ID)
            {
                if (currentInfo.IsValid != info.IsValid)
                {
                    _originalList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);
                }
            }
            if (info != null)
            {
                this.cmbType.ShowSelectedValue(info.Type, ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OrganizationType>(info.Type, LocalData.IsEnglish));
            }
            #endregion

            if (info == null) { this.bindingSource1.DataSource = typeof(OrganizationInfo); this.Enabled = false; }
            else if (info.IsValid == false)
            {
                this.Enabled = false;
                this.bindingSource1.DataSource = info;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit();
            }
            else
            {
                if(_originalList==null)_originalList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);

                if (Utility.GuidIsNullOrEmpty(info.ParentID) == false)
                {
                    OrganizationList parent = _originalList.Find(delegate(OrganizationList item) { return item.ID == info.ParentID; });
                    if (parent != null)
                        info.ParentName = LocalData.IsEnglish ? parent.EShortName : parent.CShortName;

                }
                if (info.IsNew) { txtCode.Focus(); }

                this.bindingSource1.DataSource = info;
                this.Enabled = true;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
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
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region barItem

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            this.Validate();
            bindingSource1.EndEdit();
            OrganizationInfo currentData = bindingSource1.DataSource as OrganizationInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false) return false;
            try
            {
                ManyHierarchyResultData result = OrganizationService.SaveOrganizationInfo
                                        (currentData.IsNew ? Guid.Empty : currentData.ID
                                        ,currentData.ParentID
                                        ,currentData.Type
                                        ,currentData.Code
                                        ,currentData.CShortName
                                        , currentData.EShortName
                                        ,LocalData.UserInfo.LoginID
                                        ,currentData.UpdateDate);

                currentData.ID = result.ID;
                currentData.HierarchyCode = result.HierarchyCode;
                currentData.UpdateDate = result.UpdateDate;
                currentData.CancelEdit();
                currentData.BeginEdit();
                if (Saved != null) Saved(new object[] { currentData, result });


                OrganizationList exist = _originalList.Find(delegate(OrganizationList item) { return item.ID == currentData.ID; });
                if (exist == null)
                    _originalList.Add(currentData);

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
