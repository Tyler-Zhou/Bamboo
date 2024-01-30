using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Business.Common.UI.Common;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FastAddBooking : BaseEditPart
    {
        #region Service
        /// <summary>
        /// workitem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// FCM Client Service
        /// </summary>
        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }
        
        /// <summary>
        /// 客户端搜索服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 地点服务
        /// </summary>
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
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
        /// <summary>
        /// 海运出口服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        /// <summary>
        /// 公共方法服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 公共客户端服务
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 海运出口客户端服务
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        /// <summary>
        /// 系统管理服务
        /// </summary>
        private ISystemService _systemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }
        /// <summary>
        /// 询价服务
        /// </summary>
        public IClientInquireRateService ClientInquireRateService
        {
            get { return ServiceClient.GetClientService<IClientInquireRateService>(); }
        }
        /// <summary>
        /// 报表服务
        /// </summary>
        public IOEReportDataService OEReportDataSrvice
        {
            get
            {
                return ServiceClient.GetService<IOEReportDataService>();
            }
        }

        ///<summary>
        /// EDI发送服务接口
        ///</summary>
        public IEDIClientService EdiClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }
        #endregion

        #region 联系人&文档
        /// <summary>
        /// 联系人&文档
        /// </summary>
        private UCContactAndDocumentPart bookingOtherPart;
        /// <summary>
        /// 联系人&文档
        /// </summary>
        public UCContactAndDocumentPart UCBookingOtherPart
        {
            get
            {
                if (bookingOtherPart != null)
                {
                    return bookingOtherPart;
                }
                else
                {
                    bookingOtherPart = Workitem.Items.AddNew<UCContactAndDocumentPart>();
                    return bookingOtherPart;
                }
            }
        }
        #endregion

        /// <summary>
        /// 订舱详细信息
        /// </summary>
        OceanBookingInfo _oceanBookingInfo = null;

        /// <summary>
        /// 订舱详细信息
        /// </summary>
        List<OceanBookingInfo> _oceanBookingList = new List<OceanBookingInfo>();

        /// <summary>
        /// 操作内容
        /// </summary>
        BusinessOperationContext OperationContext = new BusinessOperationContext();

        /// <summary>
        /// 客户类型
        /// </summary>
        CustomerType customerType = CustomerType.Unknown;

        /// <summary>
        /// 公司对应客户列表
        /// </summary>
        List<CustomerList> companyCustomerList = null;

        /// <summary>
        /// 重量单位
        /// </summary>
        List<DataDictionaryList> _weightUnits;

        StringBuilder _oldstring = new StringBuilder();
        /// <summary>
        /// 发送修改记录邮件-订舱单修改的数据组合字符串
        /// </summary>
        StringBuilder _updatestring = new StringBuilder();


        public FastAddBooking()
        {
            InitializeComponent();
        }

        private void FastAddBooking_Load(object sender, EventArgs e)
        {
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
            OperationContext.OperationType = OperationType.OceanExport;
            barAdd_ItemClick(null, null);
            UCBookingOtherPart.BindData(OperationContext);
            SearchRegister();
            InitControls();
            InitalComboxes();
            SetLazyLoaders();

            stxtCustomer.Select();
            stxtCustomer.Focus();
        }

        /// <summary>
        /// 设置中文字符
        /// </summary>
        private void SetCnText()
        {
            if (LocalData.IsEnglish) return;

            labCarrier.Text = "船公司";
            labCompany.Text = "操作口岸";
            labCustomer.Text = "客户";
            labMeasurement.Text = "体积";
            labPaymentTerm.Text = "付款方式";
            labDelivery.Text = "交货地";
            labPlaceOfReceipt.Text = "收货地";
            labPOD.Text = "卸货港";
            labQuantity.Text = "数量";
            labSales.Text = "揽货人";
            labSalesDepartment.Text = "揽货部门";
            labSalesType.Text = "揽货类型";
            labTradeTerm.Text = "贸易条款";
            labTransportClause.Text = "运输条款";
            labType.Text = "业务类型";
            labWeight.Text = "重量";
            labShipper.Text = "船名航次";
            labPOL2.Text = "装货港";
            lblCommodity.Text = "品名";
            labContractNo.Text = "合约";
            labContainer.Text = "箱量";

            barAdd.Caption = "新增";
            barCopy.Caption = "复制";
            barCopy5.Caption = "复制5份";
            barDelete.Caption = "删除";
            barSave.Caption = "保存所有";
            barSendEDI.Caption = "保存并发送EDI";

            colCarrierName.Caption = "船公司";
            colContainerDescription.Caption = "箱量";
            colCustomerName.Caption = "客户";
            colNo.Caption = "业务号";
            colPlaceOfDeliveryName.Caption = "交货地";
            colPODName.Caption = "卸货港";
            colPOLName.Caption = "装货港";
            colVesselVoyage.Caption = "船名航次";
        }

        void SearchRegister()
        {

            OEUtility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtDelivery,
                stxtReceipt ,
                stxtPOD,
                stxtPOL,
            });

            //客户
            OEUtility.SetEnterToExecuteOnec(stxtCustomer, delegate
            {
                List<CustomerCarrierObjects> contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _oceanBookingInfo.CustomerID);

                stxtCustomer.CustomerID = _oceanBookingInfo.CustomerID;
                stxtCustomer.SetOperationContext(OperationContext);
                stxtCustomer.CompanyID = _oceanBookingInfo.CompanyID;
                stxtCustomer.ContactStage = ContactStage.SO;
                stxtCustomer.ContactType = ContactType.Customer;
                stxtCustomer.ContactList = contactList;

                stxtCustomer.EditValueChanging += stxtCustomer_EditValueChanging;
                stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
                stxtCustomer.SelectChanged += stxtCustomer_SelectChanged;
            });
            stxtCustomer.OnOk += stxtCustomer_OnOk;
            stxtCustomer.OnRefresh += stxtCustomer_OnRefresh;

            mcmbSales.SelectedRow += mcmbSales_SelectedRow;
            trsSalesDep.Enter += trsSalesDep_Enter;
            txtContractNo.Click += txtContractNo_Click;
            cmbSalesType.SelectedIndexChanged += cmbSalesType_SelectedIndexChanged;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;

            PortFinderBridge pfbPOL = new PortFinderBridge(stxtPOL, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(stxtPOD, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbDelivery = new PortFinderBridge(stxtDelivery, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbReceipt = new PortFinderBridge(stxtReceipt, DataFindClientService, LocalData.IsEnglish);
        }

        #region Customer 客户
        void stxtCustomer_EditValueChanging(object sender, ChangingEventArgs e)
        {
            stxtCustomer.EditValueChanging -= stxtCustomer_EditValueChanging;
            RemoveContactList(_oceanBookingInfo.CustomerID, "CustomerID");
            stxtCustomer.EditValueChanging += stxtCustomer_EditValueChanging;
        }
        void stxtCustomer_SelectChanged(object sender, CommonEventArgs<OceanOrderList> e)
        {
            if (e.Data == null)
            {
                return;
            }
            OceanOrderList currentOrderList = e.Data;
            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "是否覆盖当前页面数据?" : "是否覆盖当前页面数据?"
                              , LocalData.IsEnglish ? "Tip" : "提示"
                              , MessageBoxButtons.YesNo
                              );
            if (result == DialogResult.Yes)
            {
                ImportOrder(currentOrderList);
            }
        }

        private void ImportOrder(OceanOrderList currentOrderList)
        {
            OceanBookingInfo order = OceanExportService.GetOceanBookingInfo(currentOrderList.ID);
            if (order == null) return;
            order.ID = Guid.Empty;
            order.No = string.Empty;
            order.State = OEOrderState.NewOrder;
            order.OceanShippingOrderID = Guid.Empty;
            order.OceanShippingOrderNo = string.Empty;
            order.SalesID = LocalData.UserInfo.LoginID;
            order.SalesName = LocalData.UserInfo.LoginName;
            order.UpdateDate = null;
            order.CreateByID = LocalData.UserInfo.LoginID;
            order.CreateDate = DateTime.Now;
            //order.ETA = null;
            //order.ETD = null;

            #region 设置默认值, modifyBy pearl for bug1821:从最近10票业务导入，不需要导入:数量，重量，体积

            order.Quantity = 0;
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            order.QuantityUnitID = normalDictionary.ID;
            order.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            order.Weight = 0.0m;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            order.WeightUnitID = normalDictionary.ID;
            order.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            order.Measurement = 0.0m;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            order.MeasurementUnitID = normalDictionary.ID;
            order.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            _oceanBookingInfo = order;
            InitControls();
            bsBookingInfo.DataSource = _oceanBookingInfo;
            bsBookingInfo.ResetBindings(false);
            EndEdit();
            Invalidate();

            _oceanBookingInfo.VesselVoyage = stxtVoyage.EditText;
            _oceanBookingList[gvBLList.FocusedRowHandle] = _oceanBookingInfo;

            numQuantity.Focus();
        }

        void stxtCustomer_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New || EditMode == EditMode.Copy)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _oceanBookingInfo.CompanyID, _oceanBookingInfo.CustomerID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_oceanBookingInfo.ID, OperationType.OceanExport, ContactType.Customer, ContactStage.Unknown, _oceanBookingInfo.CustomerID);
            }

            UCBookingOtherPart.RemoveContactList(_oceanBookingInfo.CustomerID, ContactType.Customer);
            UCBookingOtherPart.InsertContactList(temp);
            SetContactList(_oceanBookingInfo.CustomerID, temp);
        }

        void stxtCustomer_OnOk(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> currentList = stxtCustomer.ContactList;
            UCBookingOtherPart.RemoveContactList(_oceanBookingInfo.CustomerID, ContactType.Customer);

            if (currentList.Count > 0)
            {
                UCBookingOtherPart.InsertContactList(currentList);
            }
            SetContactList(_oceanBookingInfo.CustomerID, currentList);
        }

        void stxtCustomer_EditValueChanged(object sender, EventArgs e)
        {
            stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
            customerType = stxtCustomer.CustomerType;
            Guid customerId = stxtCustomer.CustomerID;
            _oceanBookingInfo.CustomerID = customerId;
            _oceanBookingInfo.CustomerName = stxtCustomer.CustomerName;
            cmbTradeTerm.ShowSelectedValue(stxtCustomer.TradeTermID, stxtCustomer.TradeTermName);

            AddLastestContact(_oceanBookingInfo.CustomerID, stxtCustomer, ContactType.Customer);
            stxtCustomer.CustomerID = customerId;
            CustomerDescription _customerDescription = new CustomerDescription();
            ICPCommUIHelper.SetCustomerDesByID(customerId, _customerDescription);
            stxtCustomer.CustomerDescription = _customerDescription;
            if (customerType == CustomerType.DirectClient)
            {
                Guid companyid = cmbCompany.EditValue == null ? LocalData.UserInfo.DefaultCompanyID : (Guid)cmbCompany.EditValue;
                List<OceanOrderList> orderList = OceanExportService.GetRecentlyOceanOrderList(companyid, customerId, 1);
                if (orderList != null && orderList.Count > 0)
                {
                    ImportOrder(orderList[0]);
                }
            }

            stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
            SetSalesTypeByCustomerAndCompany();
        }

        /// <summary>
        /// 判断当前引用的客户是否被其他栏位引用，否则移除相关客户的业务联系人
        /// </summary>
        /// <param name="changeID"></param>
        /// <param name="sourcePropertyName"></param>
        private void RemoveContactList(Guid changeID, string sourcePropertyName)
        {
            if (changeID == null || changeID == Guid.Empty)
                return;
            List<string> relativePropertyNames = new List<string> { "CustomerID", "BookingCustomerID", "AgentID", "AgentOfCarrierID" };
            relativePropertyNames.Remove(sourcePropertyName);
            List<PropertyInfo> properties = typeof(OceanBookingInfo).GetProperties().Where(p => relativePropertyNames.Contains(p.Name)).ToList();
            if (properties == null || properties.Count <= 0)
            {
                return;
            }
            if (!properties.Exists(p => p.GetValue(_oceanBookingInfo, null) != null && (Guid)p.GetValue(_oceanBookingInfo, null) == changeID))
            {
                UCBookingOtherPart.RemoveContactList(changeID, null);
            }
        }

        private void SetContactList(Guid customerID, List<CustomerCarrierObjects> contactList)
        {
            if (_oceanBookingInfo.CustomerID == customerID)
            {
                stxtCustomer.ContactList = contactList;
            }
        }

        /// <summary>
        /// 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// </summary>
        /// <param name="customerID"></param>
        private void AddLastestContact(Guid customerID, object customerControl, ContactType contactType)
        {
            if (customerID == Guid.Empty)
            {
                return;
            }
            List<CustomerCarrierObjects> contactList = new List<CustomerCarrierObjects>();
            if (!UCBookingOtherPart.CurrentContactList.Exists(item => item.CustomerID == customerID))
            {
                contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _oceanBookingInfo.CompanyID, customerID, contactType, ContactStage.Unknown);
                if (contactList == null || contactList.Count <= 0)
                    return;
                for (int i = 0; i < contactList.Count; i++)
                {
                    contactList[i].Id = Guid.Empty;

                }
                List<CustomerCarrierObjects> currentContactList = UCBookingOtherPart.CurrentContactList;
                if (currentContactList == null || currentContactList.Count <= 0)
                {
                    UCBookingOtherPart.InsertContactList(contactList);
                }
                else
                {
                    List<string> nameList = (from item in currentContactList select item.Name).ToList();
                    List<string> emailList = (from item in currentContactList select item.Mail).ToList();

                    contactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
                    UCBookingOtherPart.InsertContactList(contactList);
                }
            }
            else
            {
                contactList = UCBookingOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID);
            }
            SetContactList(customerID, contactList);
        }
        #endregion

        /// <summary>
        /// 当前用户所在的操作口岸和揽货人所在的部门
        /// </summary>
        void trsSalesDep_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userOrganizationTreeLists = new List<OrganizationList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserCompanyList(_oceanBookingInfo.SalesID.Value, null);
            }

            List<OrganizationList> saleOrgrnazitionTreeList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            foreach (OrganizationList dept in saleOrgrnazitionTreeList)
            {
                if (userOrganizationTreeLists.FindAll(o => o.ID == dept.ID).Count == 0)
                {
                    userOrganizationTreeLists.Add(dept);
                }
            }

            trsSalesDep.SetSource<OrganizationList>(userOrganizationTreeLists, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
        }

        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            SetSalesDepartment();
        }


        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<UserOrganizationTreeList> userOrganizationTreeLists = new List<UserOrganizationTreeList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.SalesID) == false)
            {
                userOrganizationTreeLists = UserService.GetUserOrganizationTreeList(_oceanBookingInfo.SalesID.Value);
                UserOrganizationTreeList orginazation = userOrganizationTreeLists.Find(o => o.IsDefault);
                if (orginazation != null)
                {
                    trsSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _oceanBookingInfo.SalesDepartmentID = orginazation.ID;
                    _oceanBookingInfo.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    trsSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _oceanBookingInfo.SalesDepartmentID = Guid.Empty;
                    _oceanBookingInfo.SalesDepartmentName = string.Empty;
                }
            }
        }

        /// <summary>
        /// 加载控件
        /// </summary>
        private void InitControls()
        {
            //业务类型
            cmbType.ShowSelectedValue(_oceanBookingInfo.OEOperationType,
                EnumHelper.GetDescription<FCMOperationType>(_oceanBookingInfo.OEOperationType, LocalData.IsEnglish, true));
            //操作口岸
            cmbCompany.ShowSelectedValue(_oceanBookingInfo.CompanyID, _oceanBookingInfo.CompanyName);
            //贸易条款
            cmbTradeTerm.ShowSelectedValue(_oceanBookingInfo.TradeTermID, _oceanBookingInfo.TradeTermName);
            //包装
            cmbQuantityUnit.ShowSelectedValue(_oceanBookingInfo.QuantityUnitID, _oceanBookingInfo.QuantityUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.QuantityUnitID)
                 && cmbMeasurementUnit.EditValue != null && cmbQuantityUnit.EditValue != DBNull.Value)
            {
                _oceanBookingInfo.QuantityUnitID = (Guid)cmbQuantityUnit.EditValue;
            }
            //重量
            cmbWeightUnit.ShowSelectedValue(_oceanBookingInfo.WeightUnitID, _oceanBookingInfo.WeightUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.WeightUnitID)
                 && cmbMeasurementUnit.EditValue != null && cmbWeightUnit.EditValue != DBNull.Value)
            {
                _oceanBookingInfo.WeightUnitID = (Guid)cmbWeightUnit.EditValue;
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(_oceanBookingInfo.MeasurementUnitID, _oceanBookingInfo.MeasurementUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null && cmbMeasurementUnit.EditValue != DBNull.Value)
            {
                _oceanBookingInfo.MeasurementUnitID = (Guid)cmbMeasurementUnit.EditValue;
            }
            mcmbSales.ShowSelectedValue(_oceanBookingInfo.SalesID, _oceanBookingInfo.SalesName);
            //揽货类型
            cmbSalesType.ShowSelectedValue(_oceanBookingInfo.SalesTypeID, _oceanBookingInfo.SalesTypeName);
            // 付款方式
            cmbPaymentTerm.ShowSelectedValue(_oceanBookingInfo.PaymentTermID, _oceanBookingInfo.PaymentTermName);
            //揽货部门
            trsSalesDep.ShowSelectedValue(_oceanBookingInfo.SalesDepartmentID, _oceanBookingInfo.SalesDepartmentName);

            //运输条款
            cmbTransportClause.ShowSelectedValue(_oceanBookingInfo.TransportClauseID, _oceanBookingInfo.TransportClauseName);

            //船公司
            mcmbCarrier.ShowSelectedValue(_oceanBookingInfo.CarrierID, _oceanBookingInfo.CarrierName);

            //箱需求
            if (_oceanBookingInfo.ContainerDescription != null)
            {
                containerDemandControl1.Text = _oceanBookingInfo.ContainerDescription.ToString();
            }
            //大船
            stxtVoyage.ShowSelectedValue(_oceanBookingInfo.VoyageID, _oceanBookingInfo.VoyageName);
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        void InitalComboxes()
        {
            _weightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            //包装
            OEUtility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            });

            //重量
            OEUtility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            {
                _weightUnits = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            });

            //体积
            OEUtility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            });
        }

        void SetLazyLoaders()
        {
            //操作口岸列表   
            OEUtility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);

                if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.CompanyID))
                {
                    _oceanBookingInfo.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });

            //业务类型
            OEUtility.SetEnterToExecuteOnec(cmbType, delegate
            {
                ICPCommUIHelper.SetComboxByEnum<FCMOperationType>(cmbType, true);
            });

            //贸易条款
            OEUtility.SetEnterToExecuteOnec(cmbTradeTerm, delegate
            {
                ICPCommUIHelper.SetCmbDataDictionary(cmbTradeTerm, DataDictionaryType.TradeTerm);
            });

            //揽货方式
            OEUtility.SetEnterToExecuteOnec(cmbSalesType, delegate
            {
                List<DataDictionaryList> salesTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbSalesType, DataDictionaryType.SalesType);
            });

            //3个付款方式的下拉列表
            OEUtility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, DataBindType.EName, true);
            });

            //船公司
            OEUtility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            });

            //运输条款
            OEUtility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
            });

            OEUtility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                //ICPCommUIHelper.SetMcmbUsersByCompanys(this.mcmbSales);
                List<UserList> userList = UserService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表", "电商顾问", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理", "分公司副总经理" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbSales.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

        }

        /// <summary>
        /// 公司改变
        /// </summary>
        private void CompanyChanged()
        {
            SetSalesTypeByCustomerAndCompany();
            stxtCustomer.CompanyID = _oceanBookingInfo.CompanyID;
        }


        /// <summary>
        /// 根据公司和客户设置揽货方式
        /// </summary>
        private void SetSalesTypeByCustomerAndCompany()
        {
            if (_oceanBookingInfo.CompanyID != Guid.Empty && _oceanBookingInfo.CustomerID != Guid.Empty)
            {
                DataDictionaryInfo salesType = OceanExportService.GetSalesType(_oceanBookingInfo.CustomerID, _oceanBookingInfo.CompanyID);
                if (salesType != null)
                {
                    _oceanBookingInfo.SalesTypeID = salesType.ID;
                    _oceanBookingInfo.SalesTypeName = LocalData.IsEnglish ? salesType.EName : salesType.CName;

                    cmbSalesType.ShowSelectedValue(_oceanBookingInfo.SalesTypeID, _oceanBookingInfo.SalesTypeName);
                }
            }
        }

        void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetContainerDemandByBookingType();
            //chkIsTruck.Enabled = (_oceanBookingInfo.OEOperationType == FCMOperationType.FCL
            //    || _oceanBookingInfo.OEOperationType == FCMOperationType.LCL);

            //if (!chkIsTruck.Enabled)
            //{
            //    chkIsTruck.Checked = _oceanBookingInfo.IsTruck = false;
            //}
        }

        void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalesType.EditValue == null
                || cmbSalesType.EditValue == DBNull.Value)
            {
                return;
            }
            _oceanBookingInfo.SalesTypeName = cmbSalesType.Text;
            _oceanBookingInfo.SalesTypeID = (Guid)cmbSalesType.EditValue;
        }

        /// <summary>
        /// 选择运价合约
        /// </summary>
        private void txtContractNo_Click(object sender, EventArgs e)
        {
            if (!ClientOceanExportService.IsNeedAccept(_oceanBookingInfo.ID)) return;
            ClientOceanExportService.SelectContract(_oceanBookingInfo, SelectType.Contract, AfterSelectContract);
        }

        private void AfterSelectContract(object[] parameters)
        {
            FreightList selectedContract = parameters[0] as FreightList;
            if (selectedContract != null)
            {
                _oceanBookingInfo.BeginEdit();
                _oceanBookingInfo.ContractID = selectedContract.ID;
                txtContractNo.Text = _oceanBookingInfo.ContractNo = selectedContract.FreightNo;
                //labContranct.Text = selectedContract.ContractName + Environment.NewLine + selectedContract.ItemCode;
            }
        }

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            bsBookingInfo.EndEdit();
            //_oceanBookingInfo.EndEdit();

            OceanBookingInfo newData = new OceanBookingInfo();
            newData.OEOperationType = FCMOperationType.FCL;
            newData.BookingDate = newData.CreateDate = DateTime.Now;
            //确认日期为今天
            newData.SODate = DateTime.Now;
            newData.BookingMode = FCMBookingMode.Fax;
            newData.State = OEOrderState.NewOrder;
            newData.AgentID = Guid.Empty;
            newData.BookingerID = LocalData.UserInfo.LoginID;
            newData.BookingerName = LocalData.UserInfo.LoginName;
            newData.IsContract = true;
            newData.IsValid = true;


            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = normalDictionary.EName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            newData.WeightUnitID = normalDictionary.ID;
            newData.WeightUnitName = normalDictionary.EName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = normalDictionary.EName;
            #endregion

            newData.HBLReleaseType = newData.MBLReleaseType = FCMReleaseType.Unknown;

            // TODO: 这种Guard型的逻辑要在最开始的时候完成
            OEUtility.EnsureDefaultCompanyExists(UserService);

            newData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            newData.CompanyName = LocalData.UserInfo.DefaultCompanyName;

            newData.QuotedPricePartInfo = new QuotedPricePartInfo();
            newData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            newData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
            newData.OEOperationType = FCMOperationType.FCL;

            _oceanBookingList.Insert(0, newData);
            gcBLList.DataSource = _oceanBookingList;
            gvBLList.RefreshData();

            _oceanBookingInfo = newData;

            //刷新数据源会触发客户改变导致当前对象数据污染 先移除事件
            stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
            stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
            cmbSalesType.SelectedIndexChanged -= cmbSalesType_SelectedIndexChanged;
            cmbSalesType.SelectedIndexChanged -= cmbSalesType_SelectedIndexChanged;

            bsBookingInfo.DataSource = _oceanBookingInfo;
            bsBookingInfo.ResetBindings(false);
            InitControls();
            stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
            cmbSalesType.SelectedIndexChanged += cmbSalesType_SelectedIndexChanged;
            gvBLList.FocusedRowHandle = 0;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void gvBLList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
                stxtCustomer.EditValueChanged -= stxtCustomer_EditValueChanged;
                cmbSalesType.SelectedIndexChanged -= cmbSalesType_SelectedIndexChanged;
                cmbSalesType.SelectedIndexChanged -= cmbSalesType_SelectedIndexChanged;
                _oceanBookingInfo = _oceanBookingList[e.FocusedRowHandle];
                bsBookingInfo.DataSource = _oceanBookingInfo;
                bsBookingInfo.ResetBindings(false);
                InitControls();
                gvBLList.RefreshData();
                stxtCustomer.EditValueChanged += stxtCustomer_EditValueChanged;
                cmbSalesType.SelectedIndexChanged += cmbSalesType_SelectedIndexChanged;
            }
        }

        /// <summary>
        /// 复制当前业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            bsBookingInfo.EndEdit();
            OceanBookingInfo newData = new OceanBookingInfo();
            newData = CopyDate(_oceanBookingInfo);

            _oceanBookingList.Add(newData);
            gcBLList.DataSource = _oceanBookingList;
            gvBLList.RefreshData();
        }

        /// <summary>
        /// 复制5份当前数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCopy5_ItemClick(object sender, ItemClickEventArgs e)
        {
            bsBookingInfo.EndEdit();
            for (int i = 0; i < 5; i++)
            {
                OceanBookingInfo newData = new OceanBookingInfo();
                newData = CopyDate(_oceanBookingInfo);

                _oceanBookingList.Add(newData);
            }
            gcBLList.DataSource = _oceanBookingList;
            gvBLList.RefreshData();
        }

        /// <summary>
        /// COPY 业务类
        /// </summary>
        /// <param name="oriData"></param>
        /// <returns></returns>
        private OceanBookingInfo CopyDate(OceanBookingInfo oriData)
        {
            OceanBookingInfo newData = new OceanBookingInfo();
            newData.BookingByID = oriData.BookingByID;
            newData.BookingByName = oriData.BookingByName;
            newData.BookingDate = oriData.BookingDate;
            newData.BookingerID = oriData.BookingerID;
            newData.BookingerName = oriData.BookingerName;
            newData.BookingMode = oriData.BookingMode;
            newData.BookingPartyID = oriData.BookingPartyID;
            newData.BookingPartyName = oriData.BookingPartyName;
            newData.BookingCustomerID = oriData.BookingCustomerID;
            newData.BookingCustomerDescription = oriData.BookingCustomerDescription;
            newData.BookingConsigneeID = oriData.BookingConsigneeID;
            newData.BookingConsigneeName = oriData.BookingConsigneeName;
            newData.BookingConsigneedescription = oriData.BookingConsigneedescription;
            newData.BookingNotifyPartyID = oriData.BookingNotifyPartyID;
            newData.BookingNotifyPartyname = oriData.BookingNotifyPartyname;
            newData.BookingNotifyPartydescription = oriData.BookingNotifyPartydescription;
            newData.BookingShipperID = oriData.BookingShipperID;
            newData.BookingShipperName = oriData.BookingShipperName;
            newData.BookingShipperdescription = oriData.BookingShipperdescription;

            newData.CarrierID = oriData.CarrierID;
            newData.CarrierName = oriData.CarrierName;
            newData.AgentOfCarrierID = oriData.AgentOfCarrierID;
            newData.AgentDescription = oriData.AgentDescription;
            newData.Commodity = oriData.Commodity;
            newData.CompanyID = oriData.CompanyID;
            newData.CompanyName = oriData.CompanyName;
            newData.ConsigneeDescription = oriData.ConsigneeDescription;
            newData.ConsigneeID = oriData.ConsigneeID;
            newData.ConsigneeName = oriData.ConsigneeName;
            newData.ContainerDescription = oriData.ContainerDescription;
            newData.ContractID = oriData.ContractID;
            newData.ContractName = oriData.ContractName;
            newData.ContractNo = oriData.ContractNo;
            newData.CreateByID = oriData.CreateByID;
            newData.CreateByName = oriData.CreateByName;
            newData.CustomerID = oriData.CustomerID;
            newData.CustomerName = oriData.CustomerName;
            newData.IsContract = oriData.IsContract;
            newData.Measurement = oriData.Measurement;
            newData.MeasurementUnitID = oriData.MeasurementUnitID;
            newData.MeasurementUnitName = oriData.MeasurementUnitName;
            newData.NotifyPartydescription = oriData.NotifyPartydescription;
            newData.NotifyPartyID = oriData.NotifyPartyID;
            newData.NotifyPartyname = oriData.NotifyPartyname;
            newData.OEOperationType = oriData.OEOperationType;
            newData.HBLReleaseType = oriData.HBLReleaseType;
            newData.MBLReleaseType = oriData.MBLReleaseType;
            newData.OperationDate = oriData.OperationDate;
            newData.PaymentTermID = oriData.PaymentTermID;
            newData.PaymentTermName = oriData.PaymentTermName;
            newData.PlaceOfDeliveryID = oriData.PlaceOfDeliveryID;
            newData.PlaceOfDeliveryName = oriData.PlaceOfDeliveryName;
            newData.PlaceOfReceiptID = oriData.PlaceOfReceiptID;
            newData.PlaceOfReceiptName = oriData.PlaceOfReceiptName;
            newData.FinalDestinationID = oriData.FinalDestinationID;

            newData.PODID = oriData.PODID;
            newData.PODName = oriData.PODName;
            newData.POLID = oriData.POLID;
            newData.POLName = oriData.POLName;
            newData.ETA = oriData.ETA;
            newData.ETD = oriData.ETD;
            newData.Quantity = oriData.Quantity;
            newData.QuantityUnitID = oriData.QuantityUnitID;
            newData.QuantityUnitName = oriData.QuantityUnitName;
            newData.SalesDepartmentID = oriData.SalesDepartmentID;
            newData.SalesDepartmentName = oriData.SalesDepartmentName;
            newData.SalesID = oriData.SalesID;
            newData.SalesName = oriData.SalesName;
            newData.SalesTypeID = oriData.SalesTypeID;
            newData.SalesTypeName = oriData.SalesTypeName;
            newData.ShipperDescription = oriData.ShipperDescription;
            newData.ShipperID = oriData.ShipperID;
            newData.ShipperName = oriData.ShipperName;
            newData.SODate = oriData.SODate;
            newData.State = oriData.State;
            newData.TradeTermID = oriData.TradeTermID;
            newData.TradeTermName = oriData.TradeTermName;
            newData.TransportClauseID = oriData.TransportClauseID;
            newData.TransportClauseName = oriData.TransportClauseName;
            newData.VesselVoyage = oriData.VesselVoyage;
            newData.VoyageID = oriData.VoyageID;
            newData.VoyageName = oriData.VoyageName;
            newData.Weight = oriData.Weight;
            newData.WeightUnitID = oriData.WeightUnitID;
            newData.WeightUnitName = oriData.WeightUnitName;

            return newData;
        }

        private void barSendEDI_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_oceanBookingList == null || _oceanBookingList.Count < 1)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please add current operation." : "请新增发送EDI的业务.");
                return;
            }

            barSave_ItemClick(sender, e);

            if (_oceanBookingList.Count > 10)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items be less than or equal to 10." : "选择的项应小于等于10！");
                return;
            }
            int i = (from d in _oceanBookingList group d by d.CarrierID into g select g.Key).Count();
            if (i > 1)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different Carrier." : "选择的项中存在不同船东！");
                return;
            }
            List<Guid> inttraList = new List<Guid>();
            inttraList.Add(new Guid("979B3DA5-2FE2-4895-8F43-DE5610D20599"));//CMA
            inttraList.Add(new Guid("E2A5B70E-9D7A-47B2-9902-082D8A317548"));//UASC
            inttraList.Add(new Guid("FDCA28E3-7673-4803-B3C2-71E7E66B7650"));//APL
            inttraList.Add(new Guid("68797EA6-F0BB-4035-947B-84A731E21245"));//HPL
            inttraList.Add(new Guid("BF072F15-BEE9-4C33-8448-70931FC06FA9"));//MSC


            List<EDIConfigureList> ediList = ConfigureService.GetEDIConfigureList(null, null, true, 0);

            List<EDIConfigureList> findEdiList = (from d in ediList where d.EDIMode == EDIMode.Booking && d.CarrierID == _oceanBookingList[0].CarrierID select d).ToList();
            string key = string.Empty;

            if (findEdiList.Count > 0)
            {
                key = findEdiList[0].Code;
            }
            else
            {
                if (inttraList.Contains((Guid)_oceanBookingList[0].CarrierID))
                {
                    key = "InttraSo";
                }
                else
                {
                    string message = "目前EDI订舱只支持下列船东:";
                    List<String> carrierList = (from d in ediList where d.EDIMode == EDIMode.Booking group d by d.CarrierName into g select g.Key).ToList();
                    message = message + Environment.NewLine + carrierList.Aggregate((a, b) => (a + Environment.NewLine + b));
                    MessageBoxService.ShowInfo(message);
                    return;
                }
            }

            try
            {
                List<Guid> ids = _oceanBookingList.Select<OceanBookingInfo, Guid>(book => book.ID).ToList();
                List<string> nos = _oceanBookingList.Select<OceanBookingInfo, string>(book => book.No).ToList();
                Guid customerID = Guid.Empty;
                foreach (var item in _oceanBookingList)
                {
                    if (ids.Contains(item.ID) == false)
                    {
                        ids.Add(item.ID);
                    }
                    if (nos.Contains(item.No) == false)
                    {
                        nos.Add(item.No);
                    }

                    OceanBookingInfo bookinfo = OceanExportService.GetOceanBookingInfo(item.ID);
                    if (bookinfo.BookingPartyID == null || bookinfo.BookingPartyID == Guid.Empty)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "There is no booking party in the selected item" : "选择的项中存在没有订舱人的项！");
                        return;
                    }

                    if (customerID != Guid.Empty && customerID != bookinfo.BookingPartyID)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish == true ? "Selected items exist different companys." : "选择的项中存在不同的操作口岸！");
                        return;
                    }
                }

                EDISendOption sendItem = new EDISendOption();
                sendItem.ServiceKey = key;
                sendItem.EdiMode = EDIMode.Booking;
                sendItem.CompanyID = _oceanBookingList[0].CompanyID;
                sendItem.Subject = string.Empty;
                sendItem.IDs = ids.ToArray();
                sendItem.FIDs = ids.ToArray();
                sendItem.NOs = nos.ToArray();
                sendItem.OperationType = OperationType.OceanExport;
                sendItem.Subject = "INTTRA电子订舱(";
                foreach (string s in nos)
                    sendItem.Subject += s + ",";
                sendItem.Subject = sendItem.Subject.TrimEnd(',') + ")";
                sendItem.CarrierID = _oceanBookingList[0].CarrierID == null ? Guid.Empty : (Guid)_oceanBookingList[0].CarrierID;
                bool isSucc = EdiClientService.SendEDI(sendItem);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
            }
        }

        /// <summary>
        /// 保存所有业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Bookins Saveing......" : "订舱信息保存中.....");
            string message = string.Empty;

            try
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Bookins Saveing......" : "订舱信息保存中.....");

                foreach (OceanBookingInfo savedata in _oceanBookingList.FindAll(r => r.IsDirty || r.IsNew))
                {
                    OperationLogID = Guid.NewGuid();

                    //操作时间记录
                    StopwatchSaveData = Stopwatch.StartNew();
                    StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now, BaseFormID,
                        "FASTSAVE-BOOKING", string.Format("快速保存Booking;Booking ID[{0}]", savedata.ID));
                    if (ValidateData(savedata) == false)
                    {
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                        return;
                    }
                    BookingSaveRequest originalBooking = BuildOceanBooking(savedata);

                    Stopwatch StopwatchSaveBooking = Stopwatch.StartNew();

                    //保存服务
                    SingleResult result = OceanExportService.SaveOceanBookingInfo(originalBooking);
                    savedata.ID = result.GetValue<Guid>("ID");
                    savedata.No = result.GetValue<string>("No");
                    savedata.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                    string.Format("快速保存订舱单[{0}ms]", StopwatchSaveBooking.ElapsedMilliseconds));

                    string saveState = string.Format("{0}快速保存订舱单成功", ((EditMode == EditMode.New || EditMode == EditMode.Copy) ? ("[" + savedata.ID + "]") : ""));
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty, string.Empty, saveState);
                    savedata.IsDirty = false;
                }

                gvBLList.RefreshData();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty,
                    string.Empty, string.Format("保存失败 SessionId[{0}]", LocalData.SessionId));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message + "Save_Method");
            }

        }

        private BookingSaveRequest BuildOceanBooking(OceanBookingInfo currentData)
        {
            EndEdit();

            if (currentData.IsDirty == true || currentData.IsNew)
            {

                if (currentData.BookingShipperID == null)
                {
                    try
                    {
                        //发货人默认选择的公司对应客户
                        ConfigureInfo config = ConfigureService.GetCompanyConfigureInfo((Guid)cmbCompany.EditValue);
                        CustomerInfo customer = CustomerService.GetCustomerInfo(config.CustomerID);
                        currentData.BookingShipperID = config.CustomerID;
                        currentData.BookingShipperdescription = new CustomerDescription();
                        currentData.BookingShipperdescription.Address = customer.EAddress;
                        currentData.BookingShipperdescription.Fax = customer.Fax;
                        currentData.BookingShipperdescription.Name = customer.EName;
                        currentData.BookingShipperdescription.Tel = customer.Tel1;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (currentData.BookingShipperID == null)
                {
                    try
                    {
                        //收货人默认洛杉矶公司
                        ConfigureInfo config = ConfigureService.GetCompanyConfigureInfo(new Guid("51D907E2-A9C2-4EA2-9759-BB6EAFC398BE"));
                        CustomerInfo customer = CustomerService.GetCustomerInfo(config.CustomerID);
                        currentData.BookingConsigneeID = config.CustomerID;
                        currentData.BookingConsigneedescription = new CustomerDescription();
                        currentData.BookingConsigneedescription.Address = customer.EAddress;
                        currentData.BookingConsigneedescription.Fax = customer.Fax;
                        currentData.BookingConsigneedescription.Name = customer.EName;
                        currentData.BookingConsigneedescription.Tel = customer.Tel1;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                BookingSaveRequest saveRequest = new BookingSaveRequest();
                saveRequest.id = currentData.ID;
                saveRequest.customerRefNo = string.Empty;
                saveRequest.customerID = currentData.CustomerID;
                saveRequest.tradeTermID = currentData.TradeTermID;
                saveRequest.oeOperationType = currentData.OEOperationType;
                saveRequest.companyID = currentData.CompanyID;
                saveRequest.bookingerID = currentData.BookingerID;
                saveRequest.filerID = currentData.FilerId;
                saveRequest.bookingById = currentData.BookingByID;
                saveRequest.overSeasFilerId = currentData.OverSeasFilerID;
                saveRequest.salesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.salesID = currentData.SalesID;
                saveRequest.salesTypeID = currentData.SalesTypeID;
                saveRequest.bookingMode = currentData.BookingMode;
                saveRequest.bookingDate = currentData.BookingDate;
                saveRequest.bookingCustomerID = currentData.BookingCustomerID == Guid.Empty ? currentData.CustomerID : currentData.BookingCustomerID;
                saveRequest.bookingCustomerDescription = currentData.BookingCustomerDescription;
                saveRequest.shipperID = currentData.ShipperID;
                saveRequest.shipperDescription = currentData.ShipperDescription;
                saveRequest.consigneeID = currentData.ConsigneeID;
                saveRequest.consigneeDescription = currentData.ConsigneeDescription;
                saveRequest.placeOfReceiptID = currentData.PlaceOfReceiptID;
                saveRequest.polID = currentData.POLID;
                saveRequest.podID = currentData.PODID;
                saveRequest.placeOfDeliveryID = currentData.PlaceOfDeliveryID;
                saveRequest.agentID = currentData.AgentID;
                saveRequest.agentDescription = currentData.AgentDescription;
                saveRequest.carrierID = currentData.CarrierID;
                saveRequest.agentOfCarrierID = currentData.AgentOfCarrierID;
                saveRequest.isContract = currentData.IsContract;
                saveRequest.freightRateID = currentData.ContractID;
                saveRequest.oceanShippingOrderID = currentData.OceanShippingOrderID;
                saveRequest.oceanShippingOrderNo = currentData.OceanShippingOrderNo;
                saveRequest.soDate = currentData.SODate;
                saveRequest.estimatedDeliveryDate = currentData.EstimatedDeliveryDate;
                saveRequest.actualDeliveryDate = currentData.DeliveryDate;
                saveRequest.expectedShipDate = currentData.ExpectedShipDate;
                saveRequest.expectedArriveDate = currentData.ExpectedArriveDate;
                saveRequest.closingDate = currentData.ClosingDate;
                saveRequest.paymentTermID = currentData.PaymentTermID;
                saveRequest.transportClauseID = currentData.TransportClauseID;
                saveRequest.shippingLineID = currentData.ShippingLineID;
                saveRequest.preVoyageID = currentData.PreVoyageID;
                saveRequest.voyageID = currentData.VoyageID;
                saveRequest.commodity = currentData.Commodity;
                saveRequest.quantity = currentData.Quantity;
                saveRequest.quantityUnitID = currentData.QuantityUnitID;
                saveRequest.weight = currentData.Weight;
                saveRequest.weightUnitID = currentData.WeightUnitID;
                saveRequest.measurement = currentData.Measurement;
                saveRequest.measurementUnitID = currentData.MeasurementUnitID;
                saveRequest.cargoDescription = currentData.CargoDescription;
                saveRequest.containerDescription = currentData.ContainerDescription;
                saveRequest.mblPaymentTermID = currentData.PaymentTermID;
                saveRequest.hblPaymentTermID = currentData.HBLPaymentTermID;
                saveRequest.isTruck = currentData.IsTruck;
                saveRequest.isCustoms = currentData.IsCustoms;
                saveRequest.isCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.isQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.isWarehouse = currentData.IsWareHouse;
                saveRequest.isOnlyMBL = currentData.IsOnlyMBL;
                saveRequest.mblReleaseType = currentData.MBLReleaseType == null ? FCMReleaseType.Unknown : currentData.MBLReleaseType;
                saveRequest.hblReleaseType = currentData.HBLReleaseType == null ? FCMReleaseType.Unknown : currentData.HBLReleaseType;
                saveRequest.mblRequirements = currentData.MBLRequirements;
                saveRequest.hblRequirements = currentData.HBLRequirements;
                saveRequest.remark = currentData.Remark;
                saveRequest.finalDestinationID = currentData.FinalDestinationID;
                saveRequest.returnLocationID = currentData.ReturnLocationID;
                saveRequest.warehouseID = currentData.WarehouseID;
                saveRequest.closingWarehousedate = currentData.ClosingWarehousedate;
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.oceanShippingOrderUpdateDate = currentData.OceanShippingOrderUpdateDate;
                saveRequest.oceanOrderUpdateDate = currentData.UpdateDate;
                saveRequest.ClosingDate = currentData.ClosingDate;
                saveRequest.CYClosingDate = currentData.CYClosingDate;
                saveRequest.DOCClosingDate = currentData.DOCClosingDate;
                saveRequest.VGMCutOffDate = currentData.VGMCutOffDate;
                saveRequest.ETA = currentData.ETA;
                saveRequest.ETD = currentData.ETD;
                saveRequest.GateInDate = currentData.GateInDate;
                saveRequest.PreETD = currentData.PORETD;
                saveRequest.pickupEarliestDate = currentData.PickupEarliestDate;
                saveRequest.IsThirdPlacePay = currentData.IsThirdPlacePay;
                saveRequest.CollectbyAgentID = currentData.CollectbyAgentID;
                saveRequest.CollectbyAgentName = currentData.CollectbyAgentName;

                saveRequest.bookingCustomerID = currentData.BookingCustomerID;
                saveRequest.bookingPartyID = currentData.BookingPartyID;
                saveRequest.bookingShipperID = currentData.BookingShipperID;
                saveRequest.bookingShipperdescription = currentData.BookingShipperdescription;
                saveRequest.bookingConsigneeID = currentData.BookingConsigneeID;
                saveRequest.bookingConsigneedescription = currentData.BookingConsigneedescription;
                saveRequest.bookingNotifyPartyID = currentData.BookingNotifyPartyID;
                saveRequest.bookingNotifyPartydescription = currentData.BookingNotifyPartydescription;
                saveRequest.marks = currentData.Marks;
                saveRequest.pickupRequirement = currentData.PickupRequirement;
                saveRequest.bookingExplanation = currentData.BookingExplanation;
                saveRequest.isInsurance = currentData.IsInsurance;
                saveRequest.isFumigation = currentData.IsFumigation;
                saveRequest.isWoodPacking = currentData.IsWoodPacking;
                saveRequest.isCarrierSendAMS = currentData.IsCarrierSendAMS;
                saveRequest.mBLTransportClauseID = currentData.TransportClauseID;
                saveRequest.AMSClosingDate = currentData.AMSClosingDate;
                saveRequest.IsThirdPlacePayOrder = currentData.IsThirdPlacePayOrder;
                saveRequest.CollectbyAgentOrderID = currentData.CollectbyAgentOrderID;
                saveRequest.NotifyPartyID = currentData.NotifyPartyID;
                saveRequest.ScacCode = currentData.ScacCode;
                saveRequest.RailCutOff = currentData.RailCutOff;
                saveRequest.BookingRefNo = currentData.BookingRefNo;
                saveRequest.OkToSub = currentData.OkToSub;
                saveRequest.CusClearanceNo = currentData.CusClearanceNo;

                saveRequest.InquirePriceOceanId = currentData.InquirePricePartInfo == null ? Guid.Empty : currentData.InquirePricePartInfo.InquirePriceID;
                saveRequest.InquirePriceOceanConfirmedBy = currentData.InquirePricePartInfo == null ? Guid.Empty : currentData.InquirePricePartInfo.ConfirmedByID;

                if (!saveRequest.isContract)
                {
                    saveRequest.freightRateID = currentData.ContractID = null;
                }

                if (currentData.IsNew)
                {
                    ///新增的订舱单，不需要产生账单
                    saveRequest.IsCreateBill = false;
                }
                else
                {
                    saveRequest.IsCreateBill = currentData.IsCreateBill;
                }

                saveRequest.AddInvolvedObject(currentData);

                return saveRequest;
            }
            else
            {
                return null;
            }
        }

        private bool ValidateData(OceanBookingInfo currentData)
        {
            EndEdit();

            bool isScrr = true;
            isScrr = currentData.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       //if (!ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.SalesTypeID)
                       //    && _oceanBookingInfo.SalesTypeID.ToString().Equals("e34bdcaa-4253-41c0-b14b-e38111cf2fc8")
                       //    && ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.OverSeasFilerID))
                       //{
                       //    e.SetErrorInfo("OverSeasFilerID", LocalData.IsEnglish ? "Nomination Cargo，G.C.S. Must Input." : "指定货，总客服必须输入.");
                       //    isScrr = false;
                       //}

                       //如果选择合约的判断条件
                       if (_oceanBookingInfo.IsContract)
                       {
                           if (ArgumentHelper.GuidIsNullOrEmpty(_oceanBookingInfo.ContractID))
                           {
                               e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "Contract Must Input" : "合约必须输入.");
                               isScrr = false;
                           }
                       }

                       if (_oceanBookingInfo.POLID != Guid.Empty && _oceanBookingInfo.POLID == _oceanBookingInfo.PODID)
                       {
                           isScrr = false;
                           e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                       }

                       if (_oceanBookingInfo.ContainerDescription != null)
                       {
                           if (_oceanBookingInfo.ContainerDescription.ToString() != containerDemandControl1.Text)
                           {
                               _oceanBookingInfo.IsDirty = true;
                           }
                       }
                       //果选择整箱业务类型，箱需求必输；箱需求逻辑,点击对应的箱型n次,则显示n*箱型
                       if (_oceanBookingInfo.OEOperationType == FCMOperationType.FCL)
                       {
                           if (containerDemandControl1.Text.Trim().Length == 0)
                           {
                               e.SetErrorInfo("ContainerDescription", LocalData.IsEnglish ? "FCL business container needs to be input." : "整箱业务必须输入箱需求.");
                               isScrr = false;
                           }

                       }
                       //把箱需求转换成对象
                       _oceanBookingInfo.ContainerDescription = new ContainerDescription(containerDemandControl1.Text.Trim());
                   }
               );
            return isScrr;
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            _oceanBookingList.Remove(_oceanBookingInfo);
            _oceanBookingInfo = _oceanBookingList[0];
            gcBLList.DataSource = _oceanBookingList;
            gvBLList.RefreshData();
            bsBookingInfo.DataSource = _oceanBookingInfo;
            bsBookingInfo.ResetBindings(false);

            gvBLList.FocusedRowHandle = 0;
        }

        private void stxtVoyage_EditValueChanged(object sender, EventArgs e)
        {
            _oceanBookingInfo.VesselVoyage = stxtVoyage.EditText;
        }

    }
}
