using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class LedgerListSearchPart : BaseSearchPart
    {
        public LedgerListSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                RemoveKeyDownHandle();
                OnSearched = null;
                RemoveKeyDownHandle();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                SetKeyDownToSearch();
                InitControls();
                dmdDate.radioGroup1.SelectedIndex = 1;
            }
        }

        #region 响应回车事件

        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupControlContainer2.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupControlContainer2.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
                btnSearch.PerformClick();
        }

        #endregion

        #region 初始化控件

        private void InitControls()
        {
            FAMUtility.BindCheckComboBoxByCompany(chcCompany);
            FAMUtility.BindCmbBoxUser(mscCreator);
            FAMUtility.BindCmbBoxUser(mscAuditor);
            FAMUtility.BindCmbBoxUser(mscCashier);

            dmdDate.IsEngish = LocalData.IsEnglish;
            dmdDate.radioGroup1.SelectedIndex = 2;

            //类型
            List<EnumHelper.ListItem<LedgerMasterType>> masterType = EnumHelper.GetEnumValues<LedgerMasterType>(LocalData.IsEnglish);
            chcType.Properties.BeginUpdate();
            foreach (var item in masterType)
            {
                if (item.Value != LedgerMasterType.Unknown)
                {
                    chcType.Properties.Items.Add(item.Value, item.Name, CheckState.Checked, true);
                }
            }
            chcType.Properties.EndUpdate();
            //状态
            List<EnumHelper.ListItem<LedgerMasterStatus>> masterstatus = EnumHelper.GetEnumValues<LedgerMasterStatus>(LocalData.IsEnglish);
            cmbStatus.Properties.BeginUpdate();
            cmbStatus.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish?"All":"全部", 0));
            foreach (var item in masterstatus)
            {
                if (item.Value != LedgerMasterStatus.Unknown)
                {
                    cmbStatus.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbStatus.Properties.EndUpdate();


            //控件默认值
            cmbStatus.SelectedIndex = 0;            
        }

        #endregion

        #endregion

        public override event SearchResultHandler OnSearched;

        /// <summary>
        /// 总行数
        /// </summary>
        private int totalRowCount
        {
            get;
            set;
        }


        public override object GetData()
        {
            int? minVoucherSeqNo=null;
            int? maxVoucherSeqNo=null;
            Guid?  createBy = null;
            Guid?  auditorID = null;
            Guid? cashierID = null;
            LedgerMasterStatus statue = LedgerMasterStatus.Unknown;

            if(!string.IsNullOrEmpty(txtNoFrom.Text)&&txtNoFrom.Text!="0")
            {
                try
                {
                    minVoucherSeqNo=Convert.ToInt32(txtNoFrom.Text);
                }
                catch
                {
                    minVoucherSeqNo=null;
                }
            }
            if(!string.IsNullOrEmpty(txtNoTo.Text)&&txtNoTo.Text!="0")
            {
                try
                {
                    maxVoucherSeqNo = Convert.ToInt32(txtNoTo.Text);
                }
                catch
                {
                    maxVoucherSeqNo = null;
                }
            }
            if (mscCreator.EditValue != null)
            {
                createBy = new Guid(mscCreator.EditValue.ToString());
            }
            if (mscAuditor.EditValue != null)
            {
                auditorID = new Guid(mscAuditor.EditValue.ToString());
            }
            if (mscCashier.EditValue != null)
            {
                cashierID = new Guid(mscCashier.EditValue.ToString());
            }
            if (cmbStatus.SelectedIndex > 0)
            {
                statue = (LedgerMasterStatus)cmbStatus.SelectedIndex;
            }



            try
            {
                List<LedgerListInfo> list = FinanceService.GetLedgerList(CompanyIDs.ToArray(),
                                                                          minVoucherSeqNo,
                                                                          maxVoucherSeqNo,
                                                                          txtRefNo.Text,
                                                                          (LedgerSearchAmountType)rgpAmountType.SelectedIndex+1,
                                                                          DataTypeHelper.GetDecimal(numAmountMin.Value,0),
                                                                          DataTypeHelper.GetDecimal(numAmountMax.Value,0),
                                                                          txtRemark.Text,
                                                                          createBy, 
                                                                          auditorID, 
                                                                          cashierID, 
                                                                          TypeList.ToArray(), 
                                                                          statue, 
                                                                          ckbValid.Checked, 
                                                                          dmdDate.From, 
                                                                          dmdDate.To);
    
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        public List<LedgerMasterType> TypeList
        {
            get
            {
                List<LedgerMasterType> list = new List<LedgerMasterType>();
                foreach (CheckedListBoxItem item in chcType.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        list.Add((LedgerMasterType)(item.Value));
                }
                return list;
            }
        }

        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chcCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        #region 按钮事件
        #region 查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                totalRowCount = 0;

                //验证数据有效性
                if (ValidateData() == false)
                    return;
                if (OnSearched != null)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),LocalData.IsEnglish?"Searching......":"正在查询......");

                    List<LedgerListInfo> list = GetData() as List<LedgerListInfo>;

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 "
                                                    + list.Count.ToString() + " 条数据.");

                    OnSearched(this, list);
                }
            }
        }
        #endregion

        #region 验证数据

        private bool ValidateData()
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(txtNoFrom.Text) && !string.IsNullOrEmpty(txtNoTo.Text))
            {
                if (int.Parse(txtNoTo.Text) < int.Parse(txtNoFrom.Text))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),
                        LocalData.IsEnglish ? "Its error! max is not max." : "最大值小于最小值。请重新输入。");
                    flag = false;
                }
            }
            return flag;
        }

        #endregion

        #region 清空
        private void btnClare_Click(object sender, EventArgs e)
        {
            ckbValid.Checked = null;
            chcCompany.Text = string.Empty;
            txtNoFrom.Text = string.Empty;
            txtNoTo.Text = string.Empty;
            txtRefNo.Text = string.Empty;
            mscCreator.EditText = string.Empty;
            mscAuditor.EditValue = null;
            mscCashier.EditValue = null;
            cmbStatus.Text = string.Empty;
        }
        #endregion

        private void navBarSearch_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
