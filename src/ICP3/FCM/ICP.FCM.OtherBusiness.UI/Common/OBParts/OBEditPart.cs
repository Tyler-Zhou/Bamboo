using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ContainerSaveRequest = ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects.ContainerSaveRequest;
using FeeSaveRequest = ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects.FeeSaveRequest;


namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 其他业务编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBEditPart : BaseEditPart
    {
        #region 本地变量

        /// <summary>
        /// 是否是订单编辑页面
        /// </summary>
        public virtual bool IsOrderEditPart { get; set; }
        /// <summary>
        /// 是否是远东区解决方案(根据当前登录人的默认公司所属解决方案)
        /// </summary>
        bool _isFarEastSolution = false;
        int count = 0;
        List<CountryList> _countryList = null;
        /// <summary>
        /// 
        /// </summary>
        OtherBusinessInfo _currentData;
        List<OBContainerList> _con;
        CustomerFinderBridge shipperBridge;
        CustomerFinderBridge consigneeBridge;
        CustomerFinderBridge notifyBridge;
        bool isChangeSales = false;

        /// <summary>
        /// 明细列表
        /// </summary>
        public List<OBContainerList> Details
        {
            get
            {
                bsContainer.EndEdit();
                List<OBContainerList> list = bsContainer.DataSource as List<OBContainerList>;
                if (list == null)
                {
                    list = new List<OBContainerList>();
                }
                return list;
            }
            set { bsContainer.DataSource = value; }
        }

        bool _isChanged = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<OBContainerList> source = bsContainer.DataSource as List<OBContainerList>;
                    if (source != null)
                    {
                        foreach (var item in source)
                        {
                            if (item.IsDirty)
                            {
                                return true;
                            }
                        }
                    }
                }

                return _isChanged;
            }
            set
            {
                _isChanged = value;
            }
        }

        /// <summary>
        /// 添加业务类型
        /// </summary>
        public virtual AddType AddBusinessType
        {
            get;
            set;
        }
        public override event SavedHandler Saved;
        public event EventHandler DataChanged;

        #endregion

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 组织结构管理服务
        /// </summary>
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        /// <summary>
        /// 用户管理服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 国家，省份，地点信息维护
        /// </summary>
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        /// <summary>
        /// UI 辅助类
        /// </summary>
        public OtherUIHelper ExportUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        /// <summary>
        /// ICP 通用UI服务
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 其他业务服务
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 配置管理服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 显示操作帐单列表
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        /// <summary>
        /// 系统服务
        /// </summary>
        private ISystemService _systemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }

        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 海运进口服务
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        #endregion

        #region Init & Override
        /// <summary>
        /// 其他业务编辑界面
        /// </summary>
        public OBEditPart()
        {
            InitializeComponent();


            Disposed += OBBaseEditPart_Disposed;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                {
                    _isFarEastSolution = true;
                }

                InitMessage();

                SetTitle();
                RegisterRelativeEvents();
                RegisterRelativeEventsAndRunOnce();

                #region 设置水印
                Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtCustomer,
                stxtAgentOfCarrier,
                stxtShipper,
                customerPopupContainerEdit1,
                lwNotifyParty,
                btnWare,
                btnCustom
            });

                Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtDetination,
                stxtDeparture,
                stxtDes
            });

                SmartPartClosing += OtherBusinessEditPart_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);
                chkIsCustoms.CheckedChanged += delegate { SetLocalServiceEnable(); };
                chkIsWarehouse.CheckedChanged += delegate { SetLocalServiceEnable(); };
            }
                #endregion

            int count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
            barTruck.Enabled = chkIsTruck.Checked;
            chkIsTruck.CheckedChanged += delegate { if (chkIsTruck.Checked) { barTruck.Enabled = true; } else { barTruck.Enabled = false; } };

            SetPermissions();
        }
        public override object DataSource
        {
            get { return bsOtherInfo.DataSource; }
            set
            {

                BindingData(value);
            }

        }
        /// <summary>
        /// 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            Guid? voyageId = Guid.Empty;
            if (stxtVesselVoyage.EditValue != null && stxtVesselVoyage.EditValue != DBNull.Value)
            {
                voyageId = new Guid(stxtVesselVoyage.EditValue.ToString());
            }
            _currentData.VoyageID = voyageId;
            _currentData.EndEdit();
            bsOtherInfo.EndEdit();
            bsContainer.EndEdit();
            gridView1.CloseEditor();
            Validate();
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 当前用户所在的操作口岸和揽货人所在的部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsbSelectAll_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userCompanyList = new List<OrganizationList>();

            if (AddBusinessType == AddType.OtherBusinessOrder && _currentData.SalesID != null)
            {
                userCompanyList = UserService.GetUserCompanyList(_currentData.SalesID.Value, null);
            }
            else
            {
                userCompanyList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);
            }

            tsbSelectAll.SetSource(userCompanyList, LocalData.IsEnglish ? "EShortName" : "CShortName");
        }
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.SalesID) == false)
            {
                List<UserOrganizationTreeList> orgList = UserService.GetUserOrganizationTreeList(_currentData.SalesID.Value);

                OrganizationList orginazation = orgList.Find(delegate(UserOrganizationTreeList item) { return item.IsDefault && item.Type == OrganizationType.Department; });
                if (orginazation != null)
                {
                    tsbSelectAll.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _currentData.SalesDepartmentID = orginazation.ID;
                    _currentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    tsbSelectAll.ShowSelectedValue(Guid.Empty, string.Empty);
                    _currentData.SalesDepartmentID = Guid.Empty;
                    _currentData.SalesDepartmentName = string.Empty;
                }
            }
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            stxtOperationNo.Enabled = false;
            spinWeight.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            spinMeasurement.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            if (cmbType.EditValue != null)
            {
                OtOperationType otOperationType = (OtOperationType)cmbType.EditValue;
                switch (otOperationType)
                {
                    case OtOperationType.Booking:
                        stxtOperationNo.Enabled = true;
                        break;
                }
            }
        }

        void customerPopupContainerEdit1_OnOk(object sender, EventArgs e)
        {

            if (customerPopupContainerEdit1.CustomerDescription != null)
                _currentData.ConsigneeDescription = customerPopupContainerEdit1.CustomerDescription;
        }

        void lwNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (lwNotifyParty.CustomerDescription != null)
                _currentData.NotifyDescription = lwNotifyParty.CustomerDescription;
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null)
                _currentData.ShipperDescription = stxtShipper.CustomerDescription;
        }

        private void mcmbOverseasFiler_Enter(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue == null) return;
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelper.SetComboxUsersByRole(mcmbOverseasFiler, depID, "海外拓展", true);
        }

        /// <summary>
        /// 填充“操作”的用户列表供选择
        /// </summary>
        void treeOperation_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelper.SetComboxUsersByRoles(treeOperation, depID, new string[] { "订舱", "客服", "文件" }, true);
        }
        /// <summary>
        /// 保存业务
        /// </summary>
        void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                Save(_currentData, false);
            }
        }
        void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                RefreshData(_currentData.ID);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
            }
        }

        #region  账单
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_currentData == null)
                {
                    return;
                }

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(_currentData.ID, OperationType.Other);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        #endregion

        #region 派车
        private void barTruck_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                ExportUIHelper.ShowTruckEdit(_currentData.ID, _currentData,
                    Workitem, OtherBusinessService,
                   Utility.GetLineNo(_currentData));
            }
        }
        #endregion

        #region 箱信息
        /// <summary>
        /// 新增箱信息
        /// </summary>
        void btnAdd_Click(object sender, EventArgs e)
        {

            OBContainerList preRow = null;
            if (gridView1.RowCount > 0)
            {
                preRow = gridView1.GetRow(gridView1.RowCount - 1) as OBContainerList;
            }
            OBContainerList newBoxRow;

            if (preRow != null)
                newBoxRow = Utility.Clone(preRow);
            else
                newBoxRow = new OBContainerList();

            newBoxRow.ID = Guid.Empty;
            newBoxRow.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newBoxRow.CreateByID = LocalData.UserInfo.LoginID;
            newBoxRow.CreateByName = LocalData.UserInfo.LoginName;
            newBoxRow.Measurement = 0;
            newBoxRow.Quantity = 0;
            newBoxRow.Weight = 0;
            gridView1.ClearSorting();
            (bsContainer.List as List<OBContainerList>).Add(newBoxRow);
            bsContainer.ResetBindings(false);

            gridView1.MoveLast();
            _isChanged = true;
            count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
        }
        /// <summary>
        /// 删除箱信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveFee();
        }
        #endregion

        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        void OtherBusinessEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_currentData.IsDirty)
                {
                    DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else if (dr == DialogResult.Yes)
                    {
                        if (!Save(_currentData, false))
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }
        #region 清理资源和避免多余操作

        void OBBaseEditPart_Disposed(object sender, EventArgs e)
        {
            _con = null;
            _countryList = null;
            _currentData = null;
            lwGridControl1.DataSource = null;
            lwNotifyParty.OnOk -= lwNotifyParty_OnOk;
            mcmbOverseasFiler.Enter -= mcmbOverseasFiler_Enter;
            bsContainer.DataSource = null;
            dxErrorProvider1.DataSource = null;
            bsOtherInfo.DataSource = null;
            bsOtherInfo.Dispose();
            bsContainer.Dispose();
            Saved = null;

            if (Workitem != null)
            {
                Workitem.Items.Remove(this);
                Workitem = null;
            }
        }

        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息

            base.WndProc(ref m);
        }

        #endregion
        #endregion

        #region 方法
        /// <summary>
        /// 根据业务信息显示下拉式控件及其它一些控件的值
        /// </summary>
        private void InitControls()
        {
            //业务类型
            List<EnumHelper.ListItem<OtOperationType>> Types = EnumHelper.GetEnumValues<OtOperationType>(LocalData.IsEnglish);
            foreach (var item in Types)
            {
                if (AddBusinessType == AddType.OtherBusiness && item.Name.Contains("FBA"))
                    continue;
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //业务类型
            cmbType.ShowSelectedValue(_currentData.OtOperationType,
                EnumHelper.GetDescription(_currentData.OtOperationType, LocalData.IsEnglish, true));
            //操作口岸(公司)
            cmbCompany.ShowSelectedValue(_currentData.CompanyID, _currentData.CompanyName);
            //重量（毛重单位）
            cmbWeightUnit.ShowSelectedValue(_currentData.WeightUnitID, _currentData.WeightUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.WeightUnitID)
                && cmbWeightUnit.EditValue != null)
            {
                _currentData.WeightUnitID = new Guid(cmbWeightUnit.EditValue.ToString());
            }
            //体积
            cmbMeasurementUnit.ShowSelectedValue(_currentData.MeasurementUnitID, _currentData.MeasurementUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null)
            {
                _currentData.MeasurementUnitID = new Guid(cmbMeasurementUnit.EditValue.ToString());
            }
            //揽货人
            Utility.SetEnterToExecuteOnec(cmbSales, delegate
            {
                ExportUIHelper.SetMcmbUsers(cmbSales, true, true);
            });
            Utility.SetEnterToExecuteOnec(tsbSelectAll, delegate
            {
                SetSalesDepartment();
            });
            //揽货人
            cmbSales.ShowSelectedValue(_currentData.SalesID, _currentData.SalesName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.SalesID)
                && cmbSales.EditValue != null)
            {
                _currentData.SalesID = new Guid(cmbSales.EditValue.ToString());
            }
            //揽货部门
            tsbSelectAll.ShowSelectedValue(_currentData.SalesDepartmentID, _currentData.SalesDepartmentName);

            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.SalesDepartmentID)
               && tsbSelectAll.EditValue != null)
            {
                _currentData.SalesDepartmentID = new Guid(tsbSelectAll.EditValue.ToString());
            }
            ////3个付款方式
            //cmbPaymentTerm.ShowSelectedValue(this._currentData.PaymentTermID, this._currentData.PaymentTermName);
            //船东
            multiSearchCommonBox1.ShowSelectedValue(_currentData.CarrierID, _currentData.CarrierName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.CarrierID)
                && multiSearchCommonBox1.EditValue != null && !String.IsNullOrEmpty(multiSearchCommonBox1.EditValue.ToString()))
            {
                _currentData.CarrierID = new Guid(multiSearchCommonBox1.EditValue.ToString());
            }
            //包装件数
            lwPackages.ShowSelectedValue(_currentData.QuantityUnitID, _currentData.QuantityUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.QuantityUnitID)
               && lwPackages.EditValue != null)
            {
                _currentData.QuantityUnitID = new Guid(lwPackages.EditValue.ToString());
            }
            //3个付款方式
            cmbPaymentTerm.ShowSelectedValue(_currentData.PaymentTypeID, _currentData.PaymentTypeName);
            //操作
            treeOperation.ShowSelectedValue(_currentData.OperatorID, _currentData.OperatorName);
            //海外部客服
            mcmbOverseasFiler.ShowSelectedValue(_currentData.OverseasFilerID, _currentData.OverseasFilerName);
            orderFeeEditPart1.SetCompanyID(_currentData.CompanyID);
            InitalComboxes();
            SetLocalServiceEnable();

            //箱型
            if (TransportFoundationService != null)
            {
                List<ICP.Common.ServiceInterface.DataObjects.ContainerList> containerList = TransportFoundationService.GetContainerList(string.Empty, true, 0);

                foreach (ICP.Common.ServiceInterface.DataObjects.ContainerList container in containerList)
                {
                    cmbBoxType.Items.Add(new ImageComboBoxItem(container.Code, container.ID));
                }
            }
            //3个付款方式的下拉列表
            Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ExportUIHelper.SetCmbDataDictionary(
                                                    cmbPaymentTerm,
                                                    DataDictionaryType.PaymentTerm, false, true);
            });
            //重量单位
            ExportUIHelper.SetCmbDataDictionary(Unit, DataDictionaryType.QuantityUnit);
            if (_currentData.VoyageID != null)
            {

                stxtVesselVoyage.ShowSelectedValue(_currentData.VoyageID, _currentData.VesselVoyage);
            }
        }
        private void InitMessage()
        {
            RegisterMessage("1110170001", LocalData.IsEnglish ? "Are you sure to deleted the selected cost" : "确认删除选中的费用?");
            RegisterMessage("1110170002", LocalData.IsEnglish ? "Are you sure clear all cost" : "确认清空所有费用?");
        }
        void SetLocalServiceEnable()
        {
            btnWare.Enabled = chkIsWarehouse.Checked;
            btnCustom.Enabled = chkIsCustoms.Checked;
            btnCustom.Properties.ReadOnly = chkIsWarehouse.Checked;
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            if (AddBusinessType == AddType.OtherBusinessOrder)
            {
                if (!LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_EditOrder))
                {
                    barSave.Visibility = BarItemVisibility.Never;
                    barSaveAs.Visibility = BarItemVisibility.Never;
                }
                if (LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices)
                && _currentData != null && _currentData.CreateByID != LocalData.UserInfo.LoginID)
                {
                    barSave.Enabled = false;
                }
            }
        }

        void RegisterRelativeEventsAndRunOnce()
        {
            RunAtOnce();
        }

        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            #region Customer

            //Customer
            DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                GetConditionsForCustomer,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid? oldCustomerId = _currentData.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[5];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;
                              }
                          }

                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[6];
                          if (!approved.HasValue
                              || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          {
                              if (_isFarEastSolution)
                              {
                                  if (approved.Value == CustomerCodeApplyState.Processing)
                                  {
                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                       , LocalData.IsEnglish ? "Tip" : "提示"
                       , MessageBoxButtons.OK
                       , MessageBoxIcon.Question);

                                      return;
                                  }
                                  else if (approved.Value == CustomerCodeApplyState.UnApply)
                                  {
                                      if ((resultData[8] == null ||
                                          string.IsNullOrEmpty(resultData[8].ToString())) &&
                                          (resultData[9] == null ||
                                          string.IsNullOrEmpty(resultData[9].ToString())))
                                      {
                                          XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                      , LocalData.IsEnglish ? "Tip" : "提示"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Question);
                                          return;
                                      }

                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }

                                      return;
                                  }
                                  else if (approved.Value == CustomerCodeApplyState.Unpassed)
                                  {
                                      if ((resultData[8] == null ||
                                          string.IsNullOrEmpty(resultData[8].ToString())) &&
                                          (resultData[9] == null ||
                                          string.IsNullOrEmpty(resultData[9].ToString())))
                                      {
                                          XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                      , LocalData.IsEnglish ? "Tip" : "提示"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Question);
                                          return;
                                      }

                                      DialogResult result = XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }

                                      return;
                                  }
                              }
                              else
                              {
                                  if (approved.Value == CustomerCodeApplyState.Processing || approved.Value == CustomerCodeApplyState.Unpassed)
                                  {
                                      if (PartLoader.PopCustomerUnApproved() != DialogResult.Yes)
                                      {
                                          return;
                                      }
                                  }

                                  if (approved.Value == CustomerCodeApplyState.UnApply)
                                  {
                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }
                                  }
                              }
                          }

                          stxtCustomer.EditValue = _currentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtCustomer.Tag = _currentData.CustomerID = new Guid(resultData[0].ToString());

                          if (oldCustomerId != Guid.Empty && _currentData.CustomerID == oldCustomerId) return;

                      }, delegate
                      {
                          stxtCustomer.Text = _currentData.CustomerName = string.Empty;
                          stxtCustomer.Tag = _currentData.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                      },
                      ClientConstants.MainWorkspace);

            DataFindClientService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _currentData.AgengofCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _currentData.AgentofCarrierID = new Guid(resultData[0].ToString());
               }, Guid.Empty, ClientConstants.MainWorkspace);

            #endregion

            #region SCNA

            //shipper
            Utility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                shipperBridge = new CustomerFinderBridge(
               stxtShipper,
               _countryList,
               DataFindClientService,
               CustomerService,
              _currentData.ShipperDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += stxtShipper_OnOk;

            //lwNotifyParty
            Utility.SetEnterToExecuteOnec(lwNotifyParty, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                notifyBridge = new CustomerFinderBridge(
               lwNotifyParty,
               _countryList,
               DataFindClientService,
               CustomerService,
               _currentData.NotifyDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                notifyBridge.Init();
            });

            lwNotifyParty.OnOk += lwNotifyParty_OnOk;
            //Consignee
            Utility.SetEnterToExecuteOnec(customerPopupContainerEdit1, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                customerPopupContainerEdit1,
                _countryList,
                DataFindClientService,
                CustomerService,
                _currentData.ConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });

            customerPopupContainerEdit1.OnOk += customerPopupContainerEdit1_OnOk;

            //仓库
            DataFindClientService.Register(btnWare, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForWarehouse,
                delegate(object inputSource, object[] resultData)
                {
                    btnWare.Tag = _currentData.WarehouseID = (Guid)resultData[0];
                    btnWare.EditValue = _currentData.WarehouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    btnWare.Tag = _currentData.WarehouseID = null;
                    btnWare.EditValue = _currentData.WarehouseName = string.Empty;
                },
                ClientConstants.MainWorkspace);

            //报关行
            DataFindClientService.Register(btnCustom, CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                GetConditionsForBroker,
                delegate(object inputSource, object[] resultData)
                {
                    btnCustom.Tag = _currentData.CustomsBrokerID = (Guid)resultData[0];
                    btnCustom.EditValue = _currentData.CustomsBrokerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate
                {
                    btnCustom.Tag = _currentData.CustomsBrokerID = null;
                    btnCustom.EditValue = _currentData.CustomsBrokerName = string.Empty;
                },
                ClientConstants.MainWorkspace);
            #endregion

            #region Port
            PortFinderBridge pfbPOL = new PortFinderBridge(stxtDeparture, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(stxtDetination, DataFindClientService, LocalData.IsEnglish);

            LocationFinderBridge pfbPlaceOfDelivery = new LocationFinderBridge(stxtDes, DataFindClientService, LocalData.IsEnglish);
            #endregion

            #region RefNo

            //业务搜索器       
            DataFindClientService.Register(stxtOperationNo, FCMFinderConstants.BusinessFinderForOI, SearchFieldConstants.BusinessNo, SearchFieldConstants.BusinessResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid bookingID = new Guid(resultData[0].ToString());
                      if (_currentData.OperationID != bookingID)
                      {
                          AfterSearchRefNo(bookingID);
                      }
                      else
                      {
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ?
                        "You has been selected this booking." : "您已选择了该票业务!");
                      }
                  },
                  delegate
                  {
                      if (_currentData.IsNew)
                      {
                          stxtOperationNo.Tag = _currentData.OperationID = Guid.Empty;
                          stxtOperationNo.Text = _currentData.OperationNo = string.Empty;
                      }

                  }, ClientConstants.MainWorkspace);

            #endregion
        }
        #endregion


        /// <summary>
        /// bug2972: 业务写订单时，选择客户，只能选择自己有权限的CRM关联的公共客户。
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustomer()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            if (IsOrderEditPart)
            {
                conditions.AddWithValue("IsFromOrder", true, false);
                conditions.AddWithValue("CurruntUserID", LocalData.UserInfo.LoginID, false);
            }

            return conditions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetSource(object value)
        {
            if (value == null) return;
            _isChanged = false;
            bsContainer.DataSource = value;
            bsContainer.ResetBindings(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workitem"></param>
        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
            InitControls();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindingData1(object data)
        {
            _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
            List<OBContainerList> list = data as List<OBContainerList>;
            if (_currentData != null)
            {
                GetConData(_currentData.ID, _currentData.CompanyID);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindingData(object data)
        {
            SuspendLayout();
            orderFeeEditPart1.SetService(Workitem);

            OtherBusinessList listInfo = data as OtherBusinessList;

            if (listInfo == null)
            {
                //新建
                _currentData = new OtherBusinessInfo();
                cmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName);
                SetCon();
                ReadyForNew();
            }
            else
            {
                GetData(listInfo.ID, listInfo.CompanyID);
                GetConData(listInfo.ID, listInfo.CompanyID);
                RefreshBarEnabled();

                if (listInfo.EditMode == EditMode.Copy)
                {
                    PrepareForCopyExistOrder();
                }
            }

            _currentData.CancelEdit();

            InitalComboxes();
            ShowOrder();

            SearchRegister();
            SetLocalServiceEnable();
            SetLazyLoaders();

            ResumeLayout(true);
        }
        public override bool SaveData()
        {
            return Save(_currentData, false);
        }
        void GetData(Guid orderId, Guid companyID)
        {
            _currentData = OtherBusinessService.GetOtherBusinessInfo(orderId, companyID);
        }
        public object SetCon()
        {
            List<OBContainerList> con = new List<OBContainerList>();
            bsContainer.DataSource = con;
            return bsContainer.DataSource as List<OBContainerList>;
        }
        List<OBContainerList> GetConData(Guid orderId, Guid companyID)
        {
            if (orderId != Guid.Empty && orderId != null)
            {
                _con = OtherBusinessService.GetOtherContainerList(orderId, companyID);
                bsContainer.DataSource = _con;
            }
            return bsContainer.DataSource as List<OBContainerList>;
        }

        #region 刷新工具栏按钮的可使用性
        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            stxtOperationNo.Enabled = false;
            if (!chkIsTruck.Enabled)
            {
                chkIsTruck.Checked = _currentData.IsTruck = false;
            }

            RefreshBarEnabled();
        }

        void RefreshBarEnabled()
        {
            txtState.Text = EnumHelper.GetDescription(_currentData.State, LocalData.IsEnglish);
            cmbType.Text = EnumHelper.GetDescription(_currentData.OtOperationType, LocalData.IsEnglish);
        }
        #endregion

        void ShowOrder()
        {
            if (_currentData.ConsigneeDescription == null)
            {
                _currentData.ConsigneeDescription = new CustomerDescription();
            }
            if (_currentData.ShipperDescription == null)
            {
                _currentData.ShipperDescription = new CustomerDescription();
            }
            if (_currentData.NotifyDescription == null)
            {
                _currentData.NotifyDescription = new CustomerDescription();
            }

            bsOtherInfo.DataSource = _currentData;
            bsOtherInfo.ResetBindings(false);
            bsOtherInfo.CancelEdit();

            InitControls();

            List<OBFeeList> feelist = null;
            if (_currentData.ID == Guid.Empty)
            {
                feelist = new List<OBFeeList>();
            }
            else
            {
                feelist = OtherBusinessService.GetOBOrderFeeList(_currentData.ID, _currentData.CompanyID);
            }

            orderFeeEditPart1.SetSource(feelist);
        }

        #region 注册各种联动的事件

        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            panelTabBaseInfo.Click += delegate
            {
                panelTabBaseInfo.Focus();
            };

            cmbSales.SelectedRow += mcmbSales_SelectedRow;
            tsbSelectAll.Enter += tsbSelectAll_Enter;
        }
        /// <summary>
        /// 搜索类型为“仓库”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForWarehouse()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Warehouse, false);
            return conditions;
        }
        /// <summary>
        /// 搜索类型为“报关行”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForBroker()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Broker, false);
            return conditions;
        }

        #endregion

        #region 延迟加载的数据源

        void InitalComboxes()
        {
            //包装件数
            ExportUIHelper.SetCmbDataDictionary(lwPackages, DataDictionaryType.QuantityUnit);
            //重量
            ExportUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);
            //体积
            ExportUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
        }
        /// <summary>
        /// 为需要延迟加载数据源的控件注册事件
        /// 这些数据源不会和其它控件发生联动，加载一次即可
        /// </summary>
        void SetLazyLoaders()
        {
            //操作口岸列表   
            Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByAll(cmbCompany, false);

                if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.CompanyID))
                {
                    _currentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });


            ////3个付款方式的下拉列表
            //Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            //{
            //    List<DataDictionaryList> payments = AirExportUIHelper.SetCmbDataDictionary(
            //                                        cmbPaymentTerm,
            //                                        DataDictionaryType.PaymentTerm, false, true);
            //});

            //船东
            Utility.SetEnterToExecuteOnec(multiSearchCommonBox1, delegate
            {
                List<CustomerList> carriers = ExportUIHelper.SetMCmbCarrier(multiSearchCommonBox1);
            });




            treeOperation.Enter += treeOperation_Click;
            mcmbOverseasFiler.Enter += mcmbOverseasFiler_Enter;
        }

        /// <summary>
        /// 公司改变
        /// </summary>
        private void CompanyChanged()
        {
            SetOperatorByCompany();

            orderFeeEditPart1.SetCompanyID(_currentData.CompanyID);
        }
        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.CompanyID))
            {
            }
            else
            {
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                List<UserList> operators = UserService.GetUnderlingUserList(new Guid[] { _currentData.CompanyID }, new string[] { "操作" }, null, true);
            }
        }
        #endregion

        #region SetSalesDepartment
        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<OrganizationList> userOrganizationTreeLists1 = new List<OrganizationList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.SalesID) == false)
            {
                userOrganizationTreeLists1 = UserService.GetUserCompanyList(_currentData.SalesID.Value, null);

                OrganizationList orginazation = userOrganizationTreeLists1.Find(delegate(OrganizationList item) { return item.IsDefault && item.Type == OrganizationType.Department; });
                if (orginazation != null)
                {
                    tsbSelectAll.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _currentData.SalesDepartmentID = orginazation.ID;
                    _currentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    tsbSelectAll.ShowSelectedValue(Guid.Empty, string.Empty);
                    _currentData.SalesDepartmentID = Guid.Empty;
                    _currentData.SalesDepartmentName = string.Empty;
                }
            }
        }
        #endregion

        /// <summary>
        /// 新增订单逻辑
        /// </summary>
        void ReadyForNew()
        {
            OtherBusinessInfo newData = new OtherBusinessInfo();
            newData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            string userName = LocalData.UserInfo.LoginName;
            if (!LocalData.IsEnglish)
            {
                userName = LocalData.UserInfo.UserName;
            }
            newData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
            //业务列表新增
            if (AddBusinessType == AddType.OtherBusiness)
            {
                newData.OperatorID = LocalData.UserInfo.LoginID;
                newData.OperatorName = userName;
                cmbSales.Enabled = true;
            }
            //其他业务订单新增
            else if (AddBusinessType == AddType.OtherBusinessOrder)
            {
                newData.SalesID = LocalData.UserInfo.LoginID;
                newData.SalesName = userName;
                newData.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
                newData.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;
                cmbSales.Enabled = false;
            }

            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.State = OBOrderState.NewOrder;
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;

            normalDictionary = ExportUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ExportUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            newData.WeightUnitID = normalDictionary.ID;
            newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            newData.OperationDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.Feta = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            normalDictionary = ExportUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            _currentData = newData;

            Utility.EnsureDefaultCompanyExists(UserService);
        }

        /// <summary>
        /// 复制订单逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            _currentData.ID = Guid.Empty;
            _currentData.State = OBOrderState.NewOrder;
            _currentData.NO = string.Empty;
            _currentData.CreateByID = LocalData.UserInfo.LoginID;
            _currentData.CreateByName = LocalData.UserInfo.LoginName;
            _currentData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            if (AddBusinessType == AddType.OtherBusinessOrder)
            {
                _currentData.SalesID = LocalData.UserInfo.LoginID;
                _currentData.SalesName = LocalData.UserInfo.LoginName;

                _currentData.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
                _currentData.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;
            }
            else
            {


                _currentData.OperationID = LocalData.UserInfo.DefaultCompanyID;
                _currentData.OverseasFilerID = LocalData.UserInfo.LoginID;
                _currentData.OverseasFilerName = LocalData.UserInfo.LoginName;
            }
        }

        /// <summary>
        /// 验证界面输入
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            dxErrorProvider1.ClearErrors();
            _currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
            List<bool> isScrrs = new List<bool> { true, true };

            isScrrs[0] = _currentData.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (_currentData.PolID == _currentData.PodID)
                       {

                           if (_currentData.PolID != Guid.Empty || _currentData.PodID != Guid.Empty)
                           {
                               if (_currentData.PolID != null && _currentData.PodID != null)
                               {
                                   e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                               }
                           }

                       }
                       if (_currentData.Eta != null && _currentData.Etd != null
                           && _currentData.Etd >= _currentData.Eta)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                       if (_currentData.OtOperationType == null || _currentData.OtOperationType.GetHashCode() == 0)
                       {
                           e.SetErrorInfo("OperationType", LocalData.IsEnglish ? "Business type must be selected." : "业务类型必须选择.");
                       }
                   }
               );

            #region ContainerDemand

            #endregion

            #region childParts

            #endregion

            bool isScrr = true;
            foreach (var item in isScrrs)
                if (item == false) isScrr = false;

            if (isScrrs[0] == false)
                xtraTabControl1.SelectedTabPageIndex = 0;

            if (!orderFeeEditPart1.ValidateData())
            {
                isScrr = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
            }

            return isScrr;
        }

        private bool Save(OtherBusinessInfo currentData, bool isSavingAs)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                txtNo.Focus();
                EndEdit();
                //_currentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                if (ValidateData() == false)
                {
                    return false;
                }
                if (ConValidateData() == false)
                {
                    return false;
                }
                if (!currentData.IsDirty && !currentData.IsNew && !orderFeeEditPart1.IsChanged && !IsChanged)
                {
                    return true;
                }
                try
                {
                    OtherBusinessSaveRequest originalBooking = null;
                    if (ArgumentHelper.GuidIsNullOrEmpty(currentData.ID))
                    {
                        originalBooking = BuildOtherBusiness(currentData);
                    }
                    else if (_currentData.IsDirty)
                    {
                        originalBooking = BuildOtherBusiness(_currentData);
                    }

                    if (isChangeSales)
                    {
                        Guid[] checkIds = { _currentData.ID };
                        _systemService.SaveUntieLockInfo(UntieLockType.Sales, checkIds, LocalData.UserInfo.LoginID);
                    }

                    List<FeeSaveRequest> originalFees = orderFeeEditPart1.BuildFeeList(_currentData.ID, _currentData.CompanyID);
                    List<ContainerSaveRequest> _container = null;
                    _container = BuildContainer(_currentData.ID);
                    Dictionary<Guid, SaveResponse> saved = OtherBusinessService.SaveOtherBusinessWithTrans(originalBooking, originalFees, _container, null);
                    //基本信息
                    if (originalBooking != null)
                    {
                        SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, saved, true);
                        RefreshUI(originalBooking);
                    }
                    //费用信息
                    if (originalFees != null)
                    {
                        SaveResponse.Analyze(originalFees.Cast<SaveRequest>().ToList(), saved, true);
                        orderFeeEditPart1.RefreshUI(originalFees);
                    }
                    //箱信息
                    if (_container != null)
                    {
                        SaveResponse.Analyze(_container.Cast<SaveRequest>().ToList(), saved, true);
                        RefreshContainerUI(_container);
                    }
                    if (EditMode == EditMode.New || EditMode == EditMode.Copy)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "The company is read only after the shipment is created." : "订单创建后，操作口岸不能更改。");
                    }
                    if (isSavingAs)
                    {
                    }
                    else
                    {
                        AfterSave();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    return false;
                }
            }
        }
        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                _currentData.SalesName = _currentData.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : cmbSales.EditText;
                _currentData.OperatorName = _currentData.OperatorID.ToGuid() == Guid.Empty ?
                    string.Empty : tsbSelectAll.Text;
                Saved(new object[] { _currentData, OperationType.Other });

                _currentData.IsDirty = false;
            }
        }
        private void AfterSave()
        {
            _currentData.CancelEdit();
            _currentData.BeginEdit();

            if (_currentData.ConsigneeDescription != null)
            {
                _currentData.ConsigneeDescription.IsDirty = false;
            }
            if (_currentData.ShipperDescription != null)
            {
                _currentData.ShipperDescription.IsDirty = false;
            }
            if (_currentData.NotifyDescription != null)
            {
                _currentData.NotifyDescription.IsDirty = false;
            }


            TriggerSavedEvent();
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            SetTitle();
        }
        void RefreshUI(OtherBusinessSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;

            OtherBusinessInfo currentData = saveRequest.UnBoxInvolvedObject<OtherBusinessInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.NO = result.GetValue<string>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (OBOrderState)result.GetValue<byte>("State");
            currentData.IsDirty = false;

            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
            }
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.NotifyDescription != null)
            {
                currentData.NotifyDescription.IsDirty = false;
            }

        }
        void SetTitle()
        {
            cmbCompany.Enabled = false;
            if (_currentData == null || _currentData.ID == Guid.Empty)
            {
                Title = LocalData.IsEnglish ? "Add Business" : "新增业务信息";
                cmbCompany.Enabled = true;
            }
            else
            {
                string titleNo = string.Empty;

                if (_currentData.NO.Length > 4)
                {
                    titleNo = _currentData.NO.Substring(_currentData.NO.Length - 4, 4);
                }
                else
                {
                    titleNo = _currentData.NO;
                }

                Title = LocalData.IsEnglish ? "Edit Business " + titleNo : "编辑业务信息：" + titleNo;
            }
        }

        #region 搜索业务号后把Booking的数据填充到当前页面
        /// <summary>
        /// 搜索业务号后把Booking的数据填充到当前页面
        /// </summary>
        private void AfterSearchRefNo(Guid bookingID)
        {
            #region 	如果之前已选择业务号

            if (ArgumentHelper.GuidIsNullOrEmpty(_currentData.OperationID) == false && _currentData.OperationID != bookingID)
            {
                //否则提示：“是否重新导入发货人、收货人、通知人、地点、货物信息？”，如果选择是，则继续执行下一步，否则退出。
                if (XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否重新导入发货人、收货人、通知人、地点、货物信息?"
                                 , LocalData.IsEnglish ? "Tip" : "提示"
                                 , MessageBoxButtons.YesNo
                                 , MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            #endregion

            #region 填充
            OceanBusinessInfo bookingInfo = OceanImportService.GetBusinessInfo(bookingID);

            stxtOperationNo.Text = _currentData.OperationNo = bookingInfo.No;
            stxtOperationNo.Tag = _currentData.OperationID = bookingInfo.ID;

            _currentData.CustomerName = bookingInfo.CustomerName;
            _currentData.Mblno = bookingInfo.MBLNo;
            _currentData.CompanyID = bookingInfo.CompanyID;

            _currentData.SalesDepartmentID = bookingInfo.SalesDepartmentID;
            _currentData.SalesDepartmentName = bookingInfo.SalesDepartmentName;
            tsbSelectAll.ShowSelectedValue(_currentData.SalesDepartmentID, _currentData.SalesDepartmentName);
            _currentData.SalesID = bookingInfo.SalesID;
            _currentData.SalesName = bookingInfo.SalesName;

            _currentData.OverseasFilerID = bookingInfo.OverSeasFilerId;
            _currentData.OverseasFilerName = bookingInfo.OverSeasFilerName;
            mcmbOverseasFiler.ShowSelectedValue(_currentData.OverseasFilerID, _currentData.OverseasFilerName);

            #region 收发通

            #region 发货人
            _currentData.ShipperID = bookingInfo.ShipperID == null ? Guid.Empty : bookingInfo.ShipperID.Value;
            _currentData.ShipperName = bookingInfo.ShipperName;
            stxtShipper.CustomerDescription = _currentData.ShipperDescription = bookingInfo.ShipperDescription;
            //if (_currentData.ShipperDescription != null)
            //    txtShipperDescription.Text = _currentData.ShipperDescription.ToString(LocalData.IsEnglish);
            #endregion

            #region 收货人
            _currentData.ConsigneeID = bookingInfo.ConsigneeID;
            _currentData.ConsigneeName = bookingInfo.ConsigneeName;
            customerPopupContainerEdit1.CustomerDescription = _currentData.ConsigneeDescription = bookingInfo.ConsigneeDescription;
            #endregion

            #region NotifyParty = 通知人描述 = “SAME AS CONSIGNEE”

            _currentData.NotifyPartyID = Guid.Empty;
            _currentData.NotifyPartyName = string.Empty;
            lwNotifyParty.CustomerDescription = _currentData.NotifyDescription = new CustomerDescription();

            #endregion

            #endregion

            #region Port

            _currentData.PolID = bookingInfo.POLID;
            _currentData.PolName = bookingInfo.POLName;

            _currentData.PodID = bookingInfo.PODID;
            _currentData.PodName = bookingInfo.PODName;

            _currentData.FinalDestinationID = bookingInfo.FinalDestinationID == null ? Guid.Empty : bookingInfo.FinalDestinationID.Value;
            _currentData.FinalDestinationName = bookingInfo.FinalDestinationName;

            #endregion

            #region Voyage, PreVoyage, ETD, ETA

            stxtVesselVoyage.Tag = _currentData.VoyageID = bookingInfo.VesselID;
            stxtVesselVoyage.Text = _currentData.VesselVoyage = bookingInfo.VesselVoyage;


            _currentData.Etd = bookingInfo.ETD;
            _currentData.Eta = bookingInfo.ETA;

            //if (bookingInfo.MBLReleaseType != null && bookingInfo.MBLReleaseType.Value != FCMReleaseType.Unknown)
            //    _currentData.ReleaseType = bookingInfo.MBLReleaseType.Value;

            #endregion

            #region 付款方式,运输条款,数量,重量,体积

            _currentData.PaymentTypeID = bookingInfo.PaymentTermID;
            cmbPaymentTerm.Text = _currentData.PaymentTypeName = bookingInfo.PaymentTermName;
            cmbPaymentTerm.ShowSelectedValue(_currentData.PaymentTypeID, _currentData.PaymentTypeName);

            //if (string.IsNullOrEmpty(_CurrentBLInfo.PaymentTermName) == false)
            //{
            //    if (_CurrentBLInfo.PaymentTermName == "CC" || _CurrentBLInfo.PaymentTermName == "到付")
            //        txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT COLLECT";
            //    else
            //        txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT PREPAID";
            //}
            //else
            //    txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = string.Empty;


            _currentData.Quantity = bookingInfo.Quantity;
            if (ArgumentHelper.GuidIsNullOrEmpty(bookingInfo.QuantityUnitID) == false)
            {
                lwPackages.Text = _currentData.QuantityUnitName = bookingInfo.QuantityUnitName;
                _currentData.QuantityUnitID = bookingInfo.QuantityUnitID;

                lwPackages.ShowSelectedValue(_currentData.QuantityUnitID, _currentData.QuantityUnitName);

            }

            _currentData.Weight = bookingInfo.Weight;
            if (ArgumentHelper.GuidIsNullOrEmpty(bookingInfo.WeightUnitID) == false)
            {
                cmbWeightUnit.Text = _currentData.WeightUnitName = bookingInfo.WeightUnitName;
                _currentData.WeightUnitID = bookingInfo.WeightUnitID;

                cmbWeightUnit.ShowSelectedValue(_currentData.WeightUnitID, _currentData.WeightUnitName);
            }
            _currentData.Measurement = bookingInfo.Measurement;
            if (ArgumentHelper.GuidIsNullOrEmpty(bookingInfo.MeasurementUnitID) == false)
            {
                cmbMeasurementUnit.Text = _currentData.MeasurementUnitName = bookingInfo.MeasurementUnitName;
                _currentData.MeasurementUnitID = bookingInfo.MeasurementUnitID;
                cmbMeasurementUnit.ShowSelectedValue(_currentData.MeasurementUnitID, _currentData.MeasurementUnitName);
            }


            #endregion

            bsOtherInfo.EndEdit();
            #endregion
        }
        #endregion

        #region 构建保存其他业务实体
        /// <summary>
        /// 构建保存其他业务实体
        /// </summary>
        /// <param name="currentData"></param>
        /// <returns></returns>
        private OtherBusinessSaveRequest BuildOtherBusiness(OtherBusinessInfo currentData)
        {
            EndEdit();
            if (currentData.IsDirty == true || currentData.IsNew)
            {
                OtherBusinessSaveRequest saveRequest = new OtherBusinessSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.AgentOfCarrierID = currentData.AgentofCarrierID;
                saveRequest.CarrierID = currentData.CarrierID;
                saveRequest.Commodity = currentData.Commodity;
                saveRequest.CompanyID = currentData.CompanyID;
                saveRequest.ConsigneeID = currentData.ConsigneeID;
                saveRequest.CustomerID = currentData.CustomerID;
                saveRequest.CustomsBrokerID = currentData.CustomsBrokerID;
                saveRequest.ETA = currentData.Eta;
                saveRequest.ETD = currentData.Etd;
                saveRequest.FETA = currentData.Feta;

                saveRequest.FinalDestinationID = currentData.FinalDestinationID;


                saveRequest.FinalDestinationName = currentData.FinalDestinationName;
                saveRequest.HBLNO = currentData.Hblno;
                saveRequest.IsCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.IsCustoms = currentData.IsCustoms;
                saveRequest.IsQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.IsTruck = currentData.IsTruck;
                saveRequest.IsWarehouse = currentData.IsWareHouse;
                saveRequest.MBLNO = currentData.Mblno;
                saveRequest.Measurement = currentData.Measurement;
                saveRequest.MeasurementUnitID = currentData.MeasurementUnitID;
                saveRequest.NotifyPartyID = currentData.NotifyPartyID;
                saveRequest.OperationDate = currentData.OperationDate;
                saveRequest.OperationID = currentData.OperationID;
                saveRequest.OperationNo = currentData.OperationNo;
                saveRequest.OperatorID = currentData.OperatorID;
                saveRequest.OTOperationType = currentData.OtOperationType;
                saveRequest.PaymentTypeID = currentData.PaymentTypeID;
                saveRequest.PODID = currentData.PodID;
                saveRequest.PODName = currentData.PodName;
                saveRequest.POLID = currentData.PolID;
                saveRequest.POLName = currentData.PolName;
                saveRequest.Quantity = currentData.Quantity;
                saveRequest.QuantityUnitID = currentData.QuantityUnitID;
                saveRequest.Remark = currentData.Remark;
                saveRequest.SalesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.SalesID = currentData.SalesID;
                saveRequest.OverseasFilerID = currentData.OverseasFilerID;
                saveRequest.ShipperID = currentData.ShipperID;
                saveRequest.SONO = currentData.SoNo;
                saveRequest.UpdateDate = currentData.UpdateDate;
                saveRequest.VoyageID = currentData.VoyageID;
                saveRequest.WarehouseID = currentData.WarehouseID;
                saveRequest.Weight = currentData.Weight;
                saveRequest.WeightUnitID = currentData.WeightUnitID;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.AddInvolvedObject(currentData);


                return saveRequest;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 箱信息
        public bool ConValidateData()
        {
            List<OBContainerList> list = Details;

            List<string> noList = new List<string>();

            foreach (OBContainerList box in list)
            {
                if (!box.Validate())
                {
                    return false;
                }

                if (!noList.Contains(box.No))
                {
                    noList.Add(box.No);
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm()
                   , (LocalData.IsEnglish ? "Box No. Not Repeat" : "箱号不能重复"));

                    return false;
                }
            }

            return true;
        }
        public void AfterSaved()
        {
            _isChanged = false;
        }
        #endregion

        #region 保存箱信息
        /// <summary>
        /// 构建保存箱信息
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<ContainerSaveRequest> BuildContainer(Guid OrderID)
        {
            gridView1.CloseEditor();
            if (!ConValidateData()) { return null; }
            List<OBContainerList> List = bsContainer.DataSource as List<OBContainerList>;
            if (List.Count != 0)
            {
                List<OBContainerList> changedList = List.FindAll(o => o.IsDirty);//明细无更改则不执行保存明细之过程
                if (OrderID == Guid.Empty)
                {
                    changedList = List;
                }

                if (changedList.Count > 0)
                {
                    List<ContainerSaveRequest> commands = new List<ContainerSaveRequest>();

                    List<Guid> IDs = new List<Guid>(); List<string> SONOS = new List<string>(); List<Guid?> TypeIDs = new List<Guid?>();
                    List<string> Nos = new List<string>(); List<Guid?> QuantityUnitIDs = new List<Guid?>(); List<Guid?> WeightUnitIDs = new List<Guid?>();
                    List<Guid?> MeasurementUnitIDs = new List<Guid?>(); List<DateTime?> UpdateDates = new List<DateTime?>(); List<decimal?> Measurements = new List<decimal?>();
                    List<decimal?> Weights = new List<decimal?>(); List<decimal?> Quantitys = new List<decimal?>(); List<string> Commoditys = new List<string>();
                    List<string> SealNos = new List<string>();
                    foreach (var item in changedList)
                    {
                        IDs.Add(item.ID);
                        SONOS.Add(item.SoNo);
                        TypeIDs.Add(item.TypeID);
                        Nos.Add(item.No);
                        QuantityUnitIDs.Add(item.QuantityUnitID);
                        WeightUnitIDs.Add(item.WeightUnitID);
                        MeasurementUnitIDs.Add(item.MeasurementUnitID);
                        UpdateDates.Add(item.UpdateDate);
                        Measurements.Add(item.Measurement);
                        Weights.Add(item.Weight);
                        Quantitys.Add(item.Quantity);
                        Commoditys.Add(item.Commodity);
                        SealNos.Add(item.SealNo);
                    }

                    ContainerSaveRequest feeInfo = new ContainerSaveRequest();
                    feeInfo.OtherBookingID = OrderID;
                    feeInfo.IDs = IDs.ToArray();
                    feeInfo.Commoditys = Commoditys.ToArray();
                    feeInfo.Measurements = Measurements.ToArray();
                    feeInfo.MeasurementUnitIDs = MeasurementUnitIDs.ToArray();
                    feeInfo.Nos = Nos.ToArray();
                    feeInfo.Quantitys = Quantitys.ToArray();
                    feeInfo.QuantityUnitIDs = QuantityUnitIDs.ToArray();
                    feeInfo.SealNos = SealNos.ToArray();
                    feeInfo.SONOS = SONOS.ToArray();
                    feeInfo.TypeIDs = TypeIDs.ToArray();
                    feeInfo.UpdateDates = UpdateDates.ToArray();
                    feeInfo.Weights = Weights.ToArray();
                    feeInfo.WeightUnitIDs = WeightUnitIDs.ToArray();
                    feeInfo.UpdateDates = UpdateDates.ToArray();
                    feeInfo.SaveByID = LocalData.UserInfo.LoginID;
                    feeInfo.isEnglish = LocalData.IsEnglish;
                    changedList.ForEach(o => feeInfo.AddInvolvedObject(o));

                    commands.Add(feeInfo);

                    return commands;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 刷新箱信息
        /// </summary>
        /// <param name="list"></param>
        public void RefreshContainerUI(List<ContainerSaveRequest> list)
        {
            foreach (ContainerSaveRequest ContainerInfo in list)
            {
                List<OBContainerList> changedContainer = ContainerInfo.UnBoxInvolvedObject<OBContainerList>();
                ManyResult result = ContainerInfo.ManyResult;
                for (int i = 0; i < changedContainer.Count; i++)
                {
                    changedContainer[i].ID = result.Items[i].GetValue<Guid>("ID");
                    changedContainer[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UPDATEDATE");
                    changedContainer[i].No = result.Items[i].GetValue<string>("NO");
                    changedContainer[i].IsDirty = false;
                }
            }
            AfterSaved();
        }

        #endregion

        #region 删除箱信息
        /// <summary>
        /// 所有选择的行（箱信息）
        /// </summary>
        List<OBContainerList> SelectRows
        {
            get
            {
                int[] indexs = gridView1.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OBContainerList> list = new List<OBContainerList>();
                foreach (var item in indexs)
                {
                    OBContainerList tager = gridView1.GetRow(item) as OBContainerList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }

        private void RemoveFee()
        {
            List<OBContainerList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid?> IDList = new List<Guid?>();
            List<DateTime?> DateList = new List<DateTime?>();

            foreach (OBContainerList hbl in list)
            {
                if (!ArgumentHelper.GuidIsNullOrEmpty(hbl.ID))
                {
                    IDList.Add(new Guid(hbl.ID.ToString()));
                    DateList.Add(hbl.UpdateDate);
                }
            }
            try
            {
                if (IDList.Count != 0)
                {
                    OtherBusinessService.RemoveOtherContainerList(IDList.ToArray(), _currentData.CompanyID, LocalData.UserInfo.LoginID, DateList.ToArray());
                    _isChanged = true;
                }
                gridView1.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
            count = gridView1.RowCount;
            if (count > 0)
            {
                txtCount.Text = count.ToString();
            }
            else { txtCount.Text = "0"; }
        }
        #endregion

        #region refresh

        void RefreshData(Guid orderId)
        {
            GetData(_currentData.ID, _currentData.CompanyID);
            ShowOrder();
            RunAtOnce();
            SetTitle();
        }
        #endregion

        #region 另存为(此处细节逻辑点待进一步确认）
        private void barSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save as a new Business successfully. Ref. NO. is " + _currentData.NO + "." : "已成功另存为一票新的业务，业务号为" + _currentData.NO + "。");
                    if (Saved != null)
                    {
                        Saved(new object[] { _currentData });
                    }
                }
            }
        }

        bool SaveAs()
        {
            if (ValidateData() == false)
            {
                return false;
            }

            if (XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的业务?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            OtherBusinessInfo orderInfo = Utility.Clone(_currentData);
            orderInfo.ID = Guid.Empty;
            orderInfo.NO = string.Empty;

            orderInfo.State = OBOrderState.NewOrder;

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            orderInfo.OperationDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            orderInfo.UpdateDate = null;

            if (orderInfo.OtOperationType == OtOperationType.CustomsDeclaration)
            {
            }

            orderInfo.IsDirty = true;

            _currentData = orderInfo;

            if (Save(orderInfo, true))
            {
                RefreshData(orderInfo.ID);

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        private void cmbSales_EditValueChanged(object sender, EventArgs e)
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (_currentData.OperationDate != null && _currentData.OperationDate < configure.AccountingClosingdate)
            {
                List<User2OrganizationJobList> jobList = UserService.GetUser2OrganizationJobList(LocalData.UserInfo.LoginID);
                foreach (User2OrganizationJobList job in jobList)
                {
                    if (job.OrganizationJobName.Contains("操作部经理") || job.OrganizationJobName.Contains("操作经理") || job.OrganizationJobName.Contains("操作部->经理"))
                    {
                        isChangeSales = true;
                    }
                }
            }
        }
    }
}