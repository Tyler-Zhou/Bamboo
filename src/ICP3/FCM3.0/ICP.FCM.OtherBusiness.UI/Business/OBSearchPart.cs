using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.Common;
namespace ICP.FCM.OtherBusiness.UI.Business
{
    [ToolboxItem(false)]
    public partial class OBSearchPart : BaseSearchPart
    {
        protected virtual bool OperationNoEnabled
        {
            get { return true; }
        }

        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IOrganizationService organizationService { get; set; }

        [ServiceDependency]
        public ICP.FCM.OtherBusiness.ServiceInterface.IOtherBusinessService OBService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ExportUIHelper ExportUIHelper { get; set; }


        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }



        #endregion

        public AddType addType
        {
            get;
            set;
        }

        public OBSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); }; ;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {

                InitControls();
                SetKeyDownToSearch();
            }
        }


        private void SetCompany()
        {
            List<OrganizationList> userCompanyList = new List<OrganizationList>();
            //取消默认揽货部门
            bool flag = false;
            if (addType == AddType.OtherBusiness)
            {
                //业务
                userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                flag = true;
            }
            else
            {
                //订单
                userCompanyList = organizationService.GetOfficeList();
            }

            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                if (item.ID == LocalData.UserInfo.DefaultCompanyID)
                {
                    if (!flag)
                    {
                        chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                      CheckState.Unchecked, true);
                    }
                    else
                    {
                        chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                      CheckState.Checked, true);
                    }

                }
                else
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                       CheckState.Unchecked, true);
                }
            }

            chkcmbCompany.Properties.EndUpdate();

        }


        #region 初始化
        private void InitControls()
        {
            txtOperationNo.Enabled = OperationNoEnabled;

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OBOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OBOrderState>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in orderStates)
            {
                if (item.Value == OBOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.SelectedIndex = 0;

            SetCompany();

            //揽货人
            Utility.SetEnterToExecuteOnec(this.mcmbSales, delegate
            {
                ExportUIHelper.SetMcmbUsers(mcmbSales, true, true);
            });

            //海外部客服
            Utility.SetEnterToExecuteOnec(mcmbOverseasFiler, delegate
            {
                List<Guid> companyIDs = treeCheckBox1.EditValue;
                List<UserList> userList = userService.GetUnderlingUserList(companyIDs.ToArray(), new string[] { "海外拓展", "客服" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbOverseasFiler.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            this.mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);

            if (addType == AddType.OtherBusinessOrder)
            {

                if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices))
                {
                    ICPCommUIHelperService.BindDepartmentByAll(treeCheckBox1);
                }
                else
                {
                    ICPCommUIHelperService.BindCompanyByUser(treeCheckBox1, CheckState.Checked);
                    this.mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
                }

            }
            else
            {
                //业务的，要绑定全部的部门
                ICPCommUIHelperService.BindDepartmentByAll(treeCheckBox1);

            }

            //日期类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndexChanged += new EventHandler(cmbDateSearchType_SelectedIndexChanged);

            SetControlsEnterToSearch();

        }
        #endregion
        #region 设置CN
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
        #endregion
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            this.Workitem.State["SalesId"] = this.SalesID;
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) this.btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) this.btnClear.PerformClick();
        }
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
                    };
                }
            }
        }
        #region 查询
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            DateSearchType dateSearchType = DateSearchType.All;
            OBOrderState? orderState = OBOrderState.Unknown;

            if (cmbState.EditValue != null && cmbState.EditValue != System.DBNull.Value)
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
                List<OtherBusinessList> list = OBService.GetOtherBusinessList(CompanyIDs.ToArray(),
                                                                             salesDepIDs.ToArray(),
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
                                                                            int.Parse(numMax.Value.ToString()),
                                                                            LocalData.IsEnglish);
                return list;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return null;
            }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }
        #endregion
        private void btnClear_Click(object sender, EventArgs e)
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
                else if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

            cmbState.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndex = 0;
        }
        /// <summary>
        /// 日期搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDateSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateSearchType.EditValue.ToString() == DateSearchType.All.ToString())
                fromToDateMonthControl1.Enabled = false;
            else
                fromToDateMonthControl1.Enabled = true;
        }
        //揽货人
        public Guid? SalesID
        {
            get
            {
                if (mcmbSales.EditValue == null) return null;
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        //海外部客服
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

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            this.txtOperationNo.Text = string.Empty;
            this.stxtCustomer.Text = string.Empty;
            this.stxtPOL.Text = string.Empty;
            this.stxtPOD.Text = string.Empty;
            this.stxtDestination.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "RefNo":
                        txtOperationNo.Text = value;
                        break;
                    case "CustomerName":
                        this.stxtCustomer.Text = value;
                        break;
                    case "POLName":
                        this.stxtPOL.Text = value;
                        break;
                    case "PODName":
                        this.stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        this.stxtDestination.Text = value;
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 虚拟页面(其他业务--订单管理）
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBOrderSearchPart : OBSearchPart
    {
        protected override bool OperationNoEnabled
        {
            get
            {
                return false;
            }
        }

    }
}
