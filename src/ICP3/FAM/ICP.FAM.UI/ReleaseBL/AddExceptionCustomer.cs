using System;
using System.Collections.Generic;
using DevExpress.XtraTab;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI.CustomerManager;
using ICP.Common.UI;
using ICP.Common.ServiceInterface;

namespace ICP.FAM.UI.ReleaseBL
{
    public partial class AddExceptionCustomer : BaseEditPart
    {
        #region 初始化
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        /// <summary>
        /// 通用服务
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        /// <summary>
        /// 客户服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        Guid? _nullNodeofctrLocationID = null;
        #endregion

        public AddExceptionCustomer()
        {
            InitializeComponent();

            Disposed += delegate
            {
                cmbType.OnFirstEnter -= OncmbTypeFirstEnter;
                cmbApplyState.OnFirstEnter -= OncmbApplyStateFirstEnter;
                ctrLocation.OnFirstTimeEnter -= OnctrLocationFirstEnter;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }

            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void OnctrLocationFirstEnter(object sender, EventArgs e)
        {
            List<CountryProvinceList> datas = Controller.GetCountryProvinceList(
                 string.Empty,
                 string.Empty,
                 null,
                 true,
                 0);

            CountryProvinceList nullItem = new CountryProvinceList();
            nullItem.ID = Guid.NewGuid();
            _nullNodeofctrLocationID = nullItem.ID;
            nullItem.ParentID = null;
            nullItem.ParentName = string.Empty;
            nullItem.Type = CountryProvinceType.Country;
            nullItem.CName = string.Empty;
            nullItem.EName = string.Empty;
            datas.Insert(0, nullItem);

            ctrLocation.AllowMultSelect = false;
            ctrLocation.RootValue = Guid.Empty;
            ctrLocation.ParentMember = "ParentID";
            ctrLocation.ValueMember = "ID";
            ctrLocation.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            ctrLocation.DataSource = datas;

            ctrLocation.InitSelectedNode(null);
        }

        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<CustomerType>> customerTypes = EnumHelper.GetEnumValues<CustomerType>(LocalData.IsEnglish);

            cmbType.Properties.BeginUpdate();

            cmbType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in customerTypes)
            {
                if (item.Value == CustomerType.Unknown)
                {
                    continue;
                }

                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "AgentOfCarrier" : "承运人", (short)100, 0));

            cmbType.Properties.EndUpdate();
        }
        private void OncmbApplyStateFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<CustomerCodeApplyState>> customerCodeApplyState = EnumHelper.GetEnumValues<CustomerCodeApplyState>(LocalData.IsEnglish);

            cmbApplyState.Properties.BeginUpdate();

            cmbApplyState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in customerCodeApplyState)
            {
                if (item.Value == CustomerCodeApplyState.All)
                {
                    continue;
                }

                cmbApplyState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbApplyState.Properties.EndUpdate();
        }
        private void InitControls()
        {
            //地点控件初始化
            ctrLocation.OnFirstTimeEnter += OnctrLocationFirstEnter;
            FAMUtility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
            });
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            #region 区域控件初始化
            List<EnumHelper.ListItem<CodeApplicantArea>> areas = EnumHelper.GetEnumValues<CodeApplicantArea>(LocalData.IsEnglish);
            cmbArea.Properties.BeginUpdate();
            cmbArea.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", null));
            foreach (var item in areas)
            {
                cmbArea.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbArea.Properties.EndUpdate();
            if (LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_checkCustomer))
            {
                if (LocalData.UserInfo.DefaultCompanyID == new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718"))
                {
                    //越南公司
                    cmbArea.SelectedIndex = 3;
                }
                else
                {
                    ConfigureInfo configureInfo = Controller.ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                    {
                        //远东区解决方案，属于远东区物流板块
                        cmbArea.SelectedIndex = 1;
                    }
                    else
                    {
                        //非远东区解决方案，属于北美区物流板块
                        cmbArea.SelectedIndex = 2;
                    }
                }
            }

            #endregion

            //类型控件初始化
            cmbType.OnFirstEnter += OncmbTypeFirstEnter;


            //申请开始,结束时间初始化
            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date.AddMonths(-1);
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Date;

            //状态控件初始化
            // Utility.SetEnterToExecuteOnec(cmbCustomerState, delegate
            //{
            cmbCustomerState.Properties.BeginUpdate();

            List<EnumHelper.ListItem<CustomerStateType>> customerStateTypes = EnumHelper.GetEnumValues<CustomerStateType>(LocalData.IsEnglish);
            //cmbCustomerState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in customerStateTypes)
            {
                cmbCustomerState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbCustomerState.Properties.EndUpdate();
            cmbCustomerState.SelectedIndex = 0;
            //});

            //申请状态初始化
            cmbApplyState.OnFirstEnter += OncmbApplyStateFirstEnter;

        }

        /// <summary>
        /// 客户管理控制器
        /// </summary>
        public CustomerManagerController Controller
        {
            get
            {
                return ClientHelper.Get<CustomerManagerController, CustomerManagerController>();
            }
        }


        public object GetData()
        {
            //区域
            Guid? areaID = null;
            if (cmbArea.EditValue != null)
            {
                if ((CodeApplicantArea)cmbArea.EditValue == CodeApplicantArea.FarEast)
                {
                    areaID = new Guid("FA56E82F-2352-E111-A359-0026551CA87B");
                }
                else if ((CodeApplicantArea)cmbArea.EditValue == CodeApplicantArea.NorthAmerica)
                {
                    areaID = new Guid("B22FE9E1-B33A-4E40-88C5-528EED76B314");
                }
                else if ((CodeApplicantArea)cmbArea.EditValue == CodeApplicantArea.Vietnam)
                {
                    areaID = new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718");
                }
            }

            //状态
            CustomerStateType? customerState = CustomerStateType.Valid;
            if (cmbCustomerState.EditValue != null)
            {
                customerState = (CustomerStateType)cmbCustomerState.EditValue;
            }

            //类型
            CustomerType? customerType = null;
            bool? isAgentOfCarrier = null;
            if (cmbType.EditValue != null && cmbType.EditValue.ToString().Length > 0)
            {
                if (cmbType.EditValue.GetType() == typeof(short))
                {
                    isAgentOfCarrier = true;
                }
                else
                {
                    customerType = (CustomerType)cmbType.EditValue;
                }
            }

            Guid companyid = Guid.Empty;
            if (cmbCompany.EditValue != null && cmbCompany.EditValue.ToString().Length > 0)
            {
                companyid = (Guid)cmbCompany.EditValue;
            }


            //申请审核开始，结束时间
            DateTime? from = null, to = null;
            if (chkEnabled.Checked)
            {
                from = dteFrom.DateTime.Date;
                to = FAMUtility.GetEndDate(dteTo.DateTime);
            }

            //审核状态
            CustomerCodeApplyState? codeApplyState = null;
            if (cmbApplyState.EditValue != null)
            {
                codeApplyState = (CustomerCodeApplyState)cmbApplyState.EditValue;
            }

            Guid? countryId = (Guid?)ctrLocation.GetSelectedValues("ParentID");
            Guid? provinceId = null;
            if (countryId != null)
            {
                if (ctrLocation.SelectedValue != null)
                {
                    provinceId = (Guid)ctrLocation.SelectedValue;
                }
            }
            else
            {
                if (ctrLocation.SelectedValue != null && (Guid)ctrLocation.SelectedValue != _nullNodeofctrLocationID)
                {
                    countryId = (Guid)ctrLocation.SelectedValue;
                }
            }


            List<CustomerList> list = null;
            if (oriTab.SelectedTabPageIndex == 0)
            {
                list = Controller.GetCustomerList(txtCode.Text.Trim(),
                                                                          txtName.Text.Trim(),
                                                                          txtAddress.Text.Trim(),
                                                                          txtTel.Text.Trim(),
                                                                          txtFax.Text.Trim(),
                                                                          txtEmail.Text.Trim(),
                                                                          countryId,
                                                                          provinceId,
                                                                          customerState,
                                                                          customerType,
                                                                          isAgentOfCarrier,
                                                                          codeApplyState,
                                                                          areaID,
                                                                          from,
                                                                          to,
                                                                          int.Parse(numMax.Value.ToString()));
            }
            else
            {
                list = CustomerService.GetExCustomerListByList(txtCode.Text.Trim(),
                                                                          txtName.Text.Trim(),
                                                                          txtAddress.Text.Trim(),
                                                                          txtTel.Text.Trim(),
                                                                          txtFax.Text.Trim(),
                                                                          txtEmail.Text.Trim(),
                                                                          countryId,
                                                                          provinceId,
                                                                          customerState,
                                                                          customerType, companyid, int.Parse(numMax.Value.ToString()));
            }
            return list;
        }

        private void oriTab_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "xtraTabSearch")
            {
                labCom.Enabled = true;
                cmbCompany.Enabled = true;
                navBarGroupControlContainer1.Enabled = true;
            }
            else
            {
                labCom.Enabled = false;
                cmbCompany.Enabled = false;
                navBarGroupControlContainer1.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (oriTab.SelectedTabPageIndex == 0)
            {
                mainGridList.DataSource = GetData();
                mainGridView.RefreshData();
            }
            else
            {
                oriGridList.DataSource = GetData();
                oriGridView.RefreshData();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Guid companyid = Guid.Empty;
            if (cmbCompany.EditValue != null && cmbCompany.EditValue.ToString().Length > 0)
            {
                companyid = (Guid)cmbCompany.EditValue;
            }
            int[] selectrows = mainGridView.GetSelectedRows();
            if (selectrows == null || selectrows.Length < 1)
            {
                return;
            }

            List<Guid> customerids = new List<Guid>();
            List<CustomerList> customerinfos = mainGridView.DataSource as List<CustomerList>;
            List<CustomerList> customers = new List<CustomerList>();
            foreach (int sel in selectrows)
            {
                customerids.Add(customerinfos[sel].ID);
                customers.Add(customerinfos[sel]);
            }


            try
            {
                CustomerService.SaveExCustomerInfo(companyid, customerids.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set exception customer Successfully" : "设置例外客户成功");

                oriTab.SelectedTabPageIndex = 1;
                oriGridList.DataSource = customers;
                oriGridView.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Set exception customer Failed" : "设置例外客户失败") + ex.Message);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (oriTab.SelectedTabPageIndex == 0)
            {
                oriTab.SelectedTabPageIndex = 1;
            }
            int[] selectrows = oriGridView.GetSelectedRows();
            if (selectrows == null || selectrows.Length < 1)
            {
                return;
            }

            Guid companyid = Guid.Empty;
            if (cmbCompany.EditValue != null && cmbCompany.EditValue.ToString().Length > 0)
            {
                companyid = (Guid)cmbCompany.EditValue;
            }

            List<Guid> customerids = new List<Guid>();
            List<CustomerList> customerinfos = oriGridList.DataSource as List<CustomerList>;
            List<CustomerList> customers = new List<CustomerList>();

            foreach (int sel in selectrows)
            {
                customerids.Add(customerinfos[sel].ID);
                customers.Add(customerinfos[sel]);
            }

            try
            {
                CustomerService.CancelExCustomer(customerids.ToArray(), companyid, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete exception customer Successfully" : "删除例外客户成功");
                customerinfos.RemoveAll(delegate(CustomerList cus)
                {
                    if (customers.Find(r => r.ID == cus.ID) != null)
                    {
                        return true;
                    }
                    return false;
                });
                oriGridList.DataSource = customerinfos;
                oriGridView.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Delete exception customer Failed" : "删除例外客户失败") + ex.Message);
            }
        }

    }
}
