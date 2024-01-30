using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI.Job
{
    [ToolboxItem(false)]
    public partial class JobSearchPart :ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        #endregion

        #region init

        protected virtual bool? IsValid { get { return null; } }

        public JobSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            if (IsValid != null)
            {
                this.lwchkIsValid.Checked = IsValid;
                this.lwchkIsValid.Enabled = false;
            }
            this.Disposed += delegate
            {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.OnKeyDownHandle);
                this.DataReturned = null;
                this.OnSearched = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsValid==null && DataReturned != null)
                DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtCode, this.txtName }, this.btnSearch,this.OnKeyDownHandle);
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch.PerformClick();
            }

        }
        private void SetCnText()
        {

            labCode.Text = "代码";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

            nbarBase.Caption = "基本信息";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
        }

        #endregion

        #region ISearchPart 成员

        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;
        public virtual void InitialValues(string property, object value)
        {
            if (property.Contains(SearchFieldConstants.Code))
                txtCode.Text = value == null ? string.Empty : value.ToString();
            else
                txtName.Text = value == null ? string.Empty : value.ToString();
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

        #region ISearchPart<JobList> 成员

        public override object GetData()
        {
            List<JobList> data = JobService.GetJobList(txtCode.Text.Trim(),
                                               txtName.Text.Trim(),
                                               lwchkIsValid.Checked,
                                              int.Parse(numMax.Value.ToString()));
            return data;
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

    }
    public class JobFinderSearchPart : JobSearchPart
    {
        protected override bool? IsValid { get { return true; } }
    }
}
