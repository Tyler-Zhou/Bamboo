using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
namespace ICP.Sys.UI.Organization
{
    [ToolboxItem(false)]
    public partial class OrganizationSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion

        #region init
        protected virtual bool? IsValid { get { return null; } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.btnSearch,this.OnKeyDownHandle);            
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch.PerformClick();
            }

        }
        public OrganizationSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            if (IsValid != null)
            {
                this.lwchkIsValid.Checked = IsValid;
                this.lwchkIsValid.Enabled = false;
            }

            numMax.Value = 1000m;
            this.Disposed += delegate
            {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.OnKeyDownHandle);
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
            labCode.Text = "代码";
            labIsValid.Text = "状态";
            labMax.Text = "最大行数";
            labName.Text = "名称";

            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
        }

        #endregion

        #region btn

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
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false
                    
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            try
            {
                List<OrganizationList> data = OrganizationService.GetOrganizationList(txtCode.Text.Trim(),
                                                    txtName.Text.Trim(),
                                                    lwchkIsValid.Checked,
                                                   int.Parse(numMax.Value.ToString()));

                OrganizationList tager= data.Find(delegate(OrganizationList item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
                if (tager != null) data.Remove(tager);

                return data;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return null;
            }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

    }
    public class OrganizationFinderSearchPart : OrganizationSearchPart
    {
        protected override bool? IsValid { get { return true; } }
    }
}

