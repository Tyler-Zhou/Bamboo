using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CompanyConfigureSearchPart : BaseSearchPart
    {
        #region 服务注入
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        public CompanyConfigureSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.OnConditionChanged = null;
                this.bsChargingGroup.DataSource = null;
                this.bsChargingGroup.Dispose();

                this.cmbSolution.OnFirstEnter -= this.OncmbSolutionFirstEnter;
                this.cmbCompany.OnFirstEnter -= this.OncmbCompanyFirstEnter;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
        }
        private void OncmbSolutionFirstEnter(object sender, EventArgs e)
        {
            List<SolutionList> solutionList = ConfigureService.GetSolutionList(string.Empty, true, 0);
            this.cmbSolution.Properties.BeginUpdate();
            cmbSolution.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "" : "", (Guid?)null));
            foreach (var item in solutionList)
            {
                cmbSolution.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbSolution.Properties.EndUpdate();
        }
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            List<OrganizationList> companyList = OrganizationService.GetOfficeList();
            this.cmbCompany.Properties.BeginUpdate();
            cmbCompany.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "" : "", (Guid?)null));
            foreach (var item in companyList)
            {
                cmbCompany.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                   (LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }
            this.cmbCompany.Properties.EndUpdate();
        }
        private void InitControls()
        {
            this.cmbSolution.OnFirstEnter += this.OncmbSolutionFirstEnter;
            this.cmbCompany.OnFirstEnter += this.OncmbCompanyFirstEnter;
        }


        private void SetCnText()
        {
            labCompany.Text = "公司";
            labSolution.Text = "解决方案";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";

            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";

            nbarBase.Caption = "基本信息";

            btnClear.Text = "清空(&L)";
            btnSearch.Text = "查询(&R)";
        }


        #region ISearchPart 接口

        /// <summary>
        /// 查询界面条件改变后触发该事件
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override event ConditionChangedHandler OnConditionChanged;

        /// <summary>
        /// 查询完成后,触发该事件
        /// <remarks>
        /// 查询完成后,一定要触发该事件
        /// </remarks>
        /// </summary>
        public override event SearchResultHandler OnSearched;

        public override object GetData()
        {
            try
            {
                Guid? companyID = (Guid?)cmbCompany.EditValue;
                Guid? solutionID = (Guid?)cmbSolution.EditValue;

                List<ConfigureList> codeList = ConfigureService.GetConfigureListByList(
                    companyID,
                    solutionID,
                    lwchkIsValid.Checked,
                    int.Parse(numMax.Value.ToString()));

                return codeList;
            }
            catch (Exception ex)
            {
                throw ex;//UNDONE: 错误提示处理
            }
        }

        //public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;
        //public virtual void InitialValues(string property, object value)
        //{
        //}

        /// <summary>
        /// 从外界向查询面版初始化值
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="values">初始化值</param>
        public override void Init(IDictionary<string, object> values)
        {
        }

        /// <summary>
        /// 触发工具栏按钮的查询事件(列入下拉工具栏按钮,文本框按钮)
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override void RaiseSearched()
        {
            if (this.OnSearched != null)
            {
                object datas = this.GetData();
                this.OnSearched(this, datas);
            }
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.RaiseSearched();
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


            lwchkIsValid.Checked = true;
            cmbCompany.EditValue = null;
            cmbCompany.Text = string.Empty;
            cmbSolution.Text = string.Empty;
            cmbSolution.EditValue = null;
        }

        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommpanyConfigureConstants.CMD_Refresh)]
        public void CMD_Refresh(object s, EventArgs e)
        {
            this.RaiseSearched();
        }   
    }
}
