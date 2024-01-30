using System;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using System.Windows.Forms;

namespace ICP.FAM.UI
{
    public partial class GLCodeSearhcPart : BaseSearchPart
    {
        public GLCodeSearhcPart()
        {
            InitializeComponent();
            Disposed += delegate {
                OnSearched = null;
                RemovekKeyDownHandle();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #region 服务
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

        #region 变量

        //private bool onlyLeaf = false;

        #endregion

        #region 属性
        private Guid solutionID;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get { return solutionID; }
            set { solutionID = value; }
        }
        /// <summary>
        /// 公司ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupControlContainer1.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }
        private void RemovekKeyDownHandle()
        {
            foreach (Control item in navBarGroupControlContainer1.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                barSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnClear.PerformClick();
            }
        }
        public override void RaiseSearched()
        {
            barSearch.PerformClick();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //公司
            FAMUtility.BindCheckComboBoxByCompany(chkcmbCompany);

            //类型
            List<EnumHelper.ListItem<GLCodeType>> glCodeType = EnumHelper.GetEnumValues<GLCodeType>(LocalData.IsEnglish);
            foreach (var item in glCodeType)
            {
                if (item.Value == GLCodeType.Unknown)
                {
                    cmbGlCodeType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", item.Value));
                }
                else
                {
                    cmbGlCodeType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbGlCodeType.Properties.EndUpdate();
            cmbGlCodeType.SelectedIndex = 0;

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            if (configureInfo != null)
            {
                SolutionID = configureInfo.SolutionID;
            }

        }

        #endregion

        #region 查询
        public override event SearchResultHandler OnSearched;
        private void barSearch_Click(object sender, EventArgs e)
        {

           List<SolutionGLCodeList> list= ConfigureService.GetSolutionGLCodeListNew(
                                  SolutionID,
                                  CompanyIDs.ToArray(),
                                  txtCode.Text,
                                  txtName.Text,
                                  (GLCodeType)cmbGlCodeType.SelectedIndex,
                                  chbIsValid.Checked,
                                  cbtnIsDepartmentCheck.Checked,
                                  cbtnIsPersonalCheck.Checked,
                                  cbtnIsCustomerCheck.Checked,
                                  cbtnIsJournal.Checked,
                                  cbtnIsBankAccount.Checked,
                                  cbtnIsFee.Checked,
                                  LocalData.IsEnglish);

           if (OnSearched != null)
           {
               OnSearched(this,list);
           }

        }
        #endregion

        #region 清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            cmbGlCodeType.SelectedIndex = 0;
        }

        #endregion

        public override void Init(IDictionary<string, object> values)
        {
            btnClear.PerformClick();
            foreach (var item in values)
            {
                if (item.Key == "SolutionID")
                    SolutionID = new Guid(item.Value.ToString());
                if (item.Key == "GLCode")
                    txtCode.Text = item.Value.ToString();
                if (item.Key == "GLName")
                    txtName.Text = item.Value.ToString();
                if (item.Key == "IsDepartmentCheck")
                {
                    bool? isDepartmentCheck = (bool?)item.Value;
                    cbtnIsDepartmentCheck.Checked = isDepartmentCheck;
                    if (isDepartmentCheck.GetValueOrDefault())
                        cbtnIsDepartmentCheck.Enabled = false;
                }
                if (item.Key == "IsPersonalCheck")
                {
                    bool? isPersonalCheck = (bool?)item.Value;
                    cbtnIsPersonalCheck.Checked = isPersonalCheck;
                    if (isPersonalCheck.GetValueOrDefault())
                        cbtnIsPersonalCheck.Enabled = false;
                }
                if (item.Key == "IsCustomerCheck")
                {
                    bool? isCustomerCheck = (bool?)item.Value;
                    cbtnIsCustomerCheck.Checked = isCustomerCheck;
                    if (isCustomerCheck.GetValueOrDefault())
                        cbtnIsCustomerCheck.Enabled = false;
                }
                if (item.Key == "IsJournal")
                {
                    bool? isJournal = (bool?)item.Value;
                    cbtnIsJournal.Checked = isJournal;
                    if (isJournal.GetValueOrDefault())
                        cbtnIsJournal.Enabled = false;
                }
                if (item.Key == "IsBankAccount")
                {
                    bool? isBankAccount = (bool?)item.Value;
                    cbtnIsBankAccount.Checked = isBankAccount;
                    if (isBankAccount.GetValueOrDefault())
                        cbtnIsBankAccount.Enabled = false;
                }
                if (item.Key == "IsFee")
                {
                    bool? isFee = (bool?)item.Value;
                    cbtnIsFee.Checked = isFee;
                    if (isFee.GetValueOrDefault())
                        cbtnIsFee.Enabled = false;
                }
                if (item.Key == "CompanyIDs")
                { 
                    List<Guid> idsList=item.Value as  List<Guid>;
                    if (idsList != null)
                    {
                        foreach (CheckedListBoxItem boxItem in chkcmbCompany.Properties.Items)
                        {
                            if (boxItem.Value == null)
                            {
                                continue;
                            }
                            if (idsList.Contains(new Guid(boxItem.Value.ToString())))
                            {
                                boxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                boxItem.CheckState = CheckState.Unchecked;
                            }
                        }
                    }
                }
            }
        }
    }
}
