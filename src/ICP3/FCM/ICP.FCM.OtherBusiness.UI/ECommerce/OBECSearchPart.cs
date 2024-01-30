using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Common;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 电商物流-查询面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBECSearchPart : BaseSearchPart
    {

        #region Fields & Property
        /// <summary>
        /// 揽货人
        /// </summary>
        public Guid? SalesID
        {
            get
            {
                if (mcmbSales.EditValue == null) return null;
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        /// <summary>
        /// 海外部客服
        /// </summary>
        public Guid? OverseasFilerID
        {
            get
            {
                if (mcmbOverseasFiler.EditValue == null) return null;
                else if ((new Guid(mcmbOverseasFiler.EditValue.ToString())) == Guid.Empty)
                {
                    return null;
                }

                return new Guid(mcmbOverseasFiler.EditValue.ToString());
            }
        }

        /// <summary>
        /// 操作口岸
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                return (from CheckedListBoxItem item in chkcmbCompany.Properties.Items where item.CheckState == CheckState.Checked select new Guid(item.Value.ToString())).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual bool OperationNoEnabled
        {
            get { return true; }
        }
        #endregion

        #region 服务注入
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public OtherUIHelper ExportUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }



        #endregion

        #region 构造函数
        /// <summary>
        /// 电商物流-查询面板
        /// </summary>
        public OBECSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;
            Disposed += delegate
            {
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region Override
        /// <summary>
        /// 
        /// </summary>
        public override event SearchResultHandler OnSearched;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            txtOperationNo.Text = string.Empty;
            stxtCustomer.Text = string.Empty;
            stxtPOL.Text = string.Empty;
            stxtPOD.Text = string.Empty;
            stxtDestination.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "RefNo":
                        txtOperationNo.Text = value;
                        break;
                    case "CustomerName":
                        stxtCustomer.Text = value;
                        break;
                    case "POLName":
                        stxtPOL.Text = value;
                        break;
                    case "PODName":
                        stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        stxtDestination.Text = value;
                        break;
                }
            }
        }
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

        public override object GetData()
        {
            if (CompanyIDs.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");
                return null;
            }
            DateTime? dtFrom = null, dtTo = null;
            DateSearchType dateSearchType = DateSearchType.All;
            OBOrderState? orderState = OBOrderState.Unknown;

            if (cmbState.EditValue != null && cmbState.EditValue != DBNull.Value)
            {
                orderState = (OBOrderState)cmbState.EditValue;

            }

            if (nbarDate.Expanded && cmbDateSearchType.EditValue != null && cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString())
            {
                dateSearchType = (DateSearchType)cmbDateSearchType.EditValue;
                dtFrom = fromToDateMonthControl1.From;
                dtTo = fromToDateMonthControl1.To;
            }

            List<Guid> salesDepIDs = treeCheckBox1.EditValue;

            if (treeCheckBox1.EditValue.Count == 0)
            {
                salesDepIDs = treeCheckBox1.GetAllAvailableValues();
            }



            try
            {
                List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessList(CompanyIDs.ToArray(),
                                                                             salesDepIDs.ToArray(),
                                                                            new[] { 5, 6 ,7 },
                                                                            txtOperationNo.Text.Trim(),
                                                                            txtMBL.Text.Trim(),
                                                                            txtHBL.Text.Trim(),
                                                                            txtContainerNo.Text.Trim(),
                                                                            txtConsign.Text.Trim(),
                                                                            txtShipper.Text.Trim(),
                                                                            txtNotify.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtDestination.Text.Trim(),
                                                                            txtVessel.Text.Trim(),
                                                                            txtVoyage.Text.Trim(),
                                                                            lwchkIsValid.Checked,
                                                                            orderState,
                                                                            SalesID,
                                                                            OverseasFilerID,
                                                                            dateSearchType,
                                                                            fromToDateMonthControl1.From,
                                                                            fromToDateMonthControl1.To,
                                                                            int.Parse(numMax.Value.ToString()));
                return list;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        #endregion

        #region Event
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            Workitem.State["SalesId"] = SalesID;
        }
        /// <summary>
        /// 日期搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDateSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fromToDateMonthControl1.Enabled = cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString();
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }

        /// <summary>
        /// 清空查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is MultiSearchCommonBox)
                {
                    MultiSearchCommonBox msc = (MultiSearchCommonBox)item;
                    msc.ShowSelectedValue(null, string.Empty);
                }
                else if (item is TextEdit
                    && (item is SpinEdit) == false
                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

            cmbState.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndex = 0;
        }

        
        #endregion

        #region 初始化
        private void InitControls()
        {
            txtOperationNo.Enabled = OperationNoEnabled;

            List<EnumHelper.ListItem<OBOrderState>> orderStates = EnumHelper.GetEnumValues<OBOrderState>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));
            foreach (var item in orderStates)
            {
                if (item.Value == OBOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.SelectedIndex = 0;

            SetCompany();

            //揽货人
            Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                ExportUIHelper.SetMcmbUsers(mcmbSales, true, true);
            });

            //海外部客服
            Utility.SetEnterToExecuteOnec(mcmbOverseasFiler, delegate
            {
                List<Guid> companyIDs = treeCheckBox1.EditValue;
                List<UserList> userList = UserService.GetUnderlingUserList(companyIDs.ToArray(), new string[] { "海外拓展", "客服" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbOverseasFiler.InitSource(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            mcmbSales.SelectedRow += mcmbSales_SelectedRow;

            ICPCommUIHelper.BindDepartmentByAll(treeCheckBox1);

            //日期类型
            List<EnumHelper.ListItem<DateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndexChanged += cmbDateSearchType_SelectedIndexChanged;

            SetControlsEnterToSearch();

        }
        #endregion

        /// <summary>
        /// 设置口岸
        /// </summary>
        private void SetCompany()
        {
            List<OrganizationEntry> userCompanyList = new List<OrganizationEntry>();
            //取消默认揽货部门
            userCompanyList = OrganizationService.GetOfficeList().Select(item => new OrganizationEntry { ID = item.ID, CName = item.CShortName, EName = item.EShortName }).ToList();
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EName : item.CName,
                                                       CheckState.Unchecked, true);
            }

            chkcmbCompany.Properties.EndUpdate();

        }
        /// <summary>
        /// 设置CN
        /// </summary>
        private void SetCnText()
        {
            lblCompany.Text = "揽货部门";
            lblShipper.Text = "发货人";
            lblConsignee.Text = "收货人";
            lblHawb.Text = "HBL";
            lblMAWB.Text = "MBL";
            lblNotify.Text = "通知人";
            lblVessel.Text = "船名";
            lblVoyage.Text = "航次";
            labCustomer.Text = "客户";
            labDestination.Text = "交货地";
            labelContainer.Text = "箱号";
            labFrom.Text = "从";
            labIsValid.Text = "是否有效";
            labMax.Text = "最大记录数";
            labOperationNo.Text = "业务号";
            labPOL.Text = "装货港";
            labPOD.Text = "卸货港";
            labSales.Text = "揽货人";
            labOverseasFiler.Text = "海外部客服";
            labState.Text = "状态";
            labTo.Text = "到";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                    };
                }
            }
        }
    }
}
