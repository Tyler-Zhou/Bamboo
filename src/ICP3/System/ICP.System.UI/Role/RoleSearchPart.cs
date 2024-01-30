using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI.Role
{
    [ToolboxItem(false)]
    public partial class RoleSearchPart :ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        protected virtual bool? IsValid { get { return null; } }

        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        #endregion

        #region Init

        public RoleSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

            if (this.IsFinder)
            {
                this.lwchkIsValid.Checked = IsValid;
                this.lwchkIsValid.Enabled = false;
            }
            this.Disposed += delegate
            {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtName }, this.OnKeyDownHandle);
                this.OnSearched = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labIsValid.Text = "状态";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
            this.btnSearch.Text = "查询(&R)";
            this.btnClear.Text = "清空(&L)";
            nbarBase.Caption = "基本信息";
        }

        protected virtual bool IsFinder { get { return false; } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsFinder == false && OnSearched != null)
                OnSearched(this, GetData());

            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtName }, this.btnSearch,this.OnKeyDownHandle);
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch.PerformClick();
            }

        }
        #endregion

        #region

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
        }
        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            List<RoleList> list = RoleService.GetRoleList(txtName.Text.Trim(),
                                          lwchkIsValid.Checked,
                                          int.Parse(numMax.Value.ToString()));
            return list;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }

    public class RoleFinderSearchPart : RoleSearchPart
    {
        protected override bool IsFinder { get { return true; } }
        protected override bool? IsValid { get { return true; } }
    }
}
