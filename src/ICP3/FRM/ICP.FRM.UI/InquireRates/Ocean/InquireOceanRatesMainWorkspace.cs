#region Comment

/*
 * 
 * FileName:    InquireOceanRatesMainWorkspace.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->海运询价主界面布局
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 海运询价主界面布局定义
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireOceanRatesMainWorkspace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 业务工厂名称
        /// </summary>
        private string businessFactoryName = "businessFactoryName";

        /// <summary>
        /// 询价服务
        /// </summary>
        [ServiceDependency]
        public IInquireRatesService inquireRatesService { get; set; }

        /// <summary>
        /// 业务面板
        /// </summary>
        public BusinessPartFactory BusinessPartFactory
        {
            get
            {
                if (Workitem.Items.Contains(businessFactoryName))
                {
                    return Workitem.Items.Get<BusinessPartFactory>(businessFactoryName);
                }
                else
                {
                    BusinessPartFactory businessFactroy = Workitem.Items.AddNew<BusinessPartFactory>(businessFactoryName);
                    return businessFactroy;
                }

            }
        }
        #endregion      

        #region 成员变量


        public string ViewCode
        {
            get;
            set;
        }
        public string StrQuery
        {
            get;
            set;
        }

        private DockPanel _CurrentDockPanel;
        /// <summary>
        /// 当前停靠面板
        /// </summary>
        public DockPanel CurrentDockPanel
        {
            get
            {
                if (_CurrentDockPanel == null)
                {
                    _CurrentDockPanel = dpFaxEmailEDI;
                }
                return _CurrentDockPanel;
            }
            set { _CurrentDockPanel = value; }
        }
        
        /// <summary>
        /// 当前询价单对象
        /// </summary>
        private ClientInquierOceanRate _CurrentInquierRate ;
        /// <summary>
        /// 当前询价单对象
        /// </summary>
        public ClientInquierOceanRate CurrentInquierRate
        {
            get { return _CurrentInquierRate; }
            set
            {
                //初始化
                IsMainInfoCutover = false;
                ClientInquierOceanRate temp = value as ClientInquierOceanRate;
                if (temp == null)
                {
                    _CurrentInquierRate = null;
                    IsMainInfoCutover = true;
                    return;
                }
                //是否主询价切换判断
                if (_CurrentInquierRate == null ||
                    (_CurrentInquierRate.ID != temp.ID &&
                     _CurrentInquierRate.ID != temp.MainRecordID &&
                     _CurrentInquierRate.MainRecordID != temp.ID &&
                     (_CurrentInquierRate.MainRecordID == null ||
                      _CurrentInquierRate.MainRecordID != temp.MainRecordID)))
                {
                    IsMainInfoCutover = true;
                   
                }
                //设置当前询价
                _CurrentInquierRate = temp;
                if(_CurrentInquierRate!=null&&IsMainInfoCutover)
                    _CurrentInquierRate.InquirePriceInquireBysList =
                       inquireRatesService.GetInquirePriceInquireBys(_CurrentInquierRate.ID, _CurrentInquierRate.MainRecordID);

            }
        }
        /// <summary>
        /// 业务信息面板
        /// </summary>
        ListBaseBusinessPart shipmentPart;
        /// <summary>
        /// 是否主询价信息切换
        /// </summary>
        public bool IsMainInfoCutover { get; set; }
        /// <summary>
        /// 非商务操作员
        /// </summary>
        public bool NotBusiness { get; set; }
        /// <summary>
        /// 船公司
        /// </summary>
        public BusinessQueryCriteria Criteria { get; set; }
        #endregion

        #region 构造函数
        public InquireOceanRatesMainWorkspace()
        {
            InitializeComponent();
            dockManager1.ActivePanel = dpFaxEmailEDI;
            dockManager1.ActivePanelChanged += dockManager1_ActivePanelChanged;
            Disposed += delegate
            {
                dockManager1.ActivePanelChanged -= dockManager1_ActivePanelChanged;
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Workspaces.Remove(CommunicationHistoryWorkspace);
                    Workitem.Workspaces.Remove(GeneralInfoWorkspace);
                    Workitem.Items.Remove(this);
                    ListWorkspace.PerformLayout();
                    CommunicationHistoryWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    GeneralInfoWorkspace.PerformLayout();


                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region 窗体事件
        /// <summary>
        /// 当前焦点面板改变
        /// </summary>
        private void dockManager1_ActivePanelChanged(object sender, ActivePanelChangedEventArgs e)
        {
            if (e.Panel != null && e.Panel.Name != "mainPanel")
            {
                CurrentDockPanel = e.Panel;
                PanelDataBind();
            }
        }
        
        #endregion

        #region 方法定义
        /// <summary>
        /// 初始化Shipment面板
        /// </summary>
        public void InitShipmentPart()
        {
            if (IsMainInfoCutover)
            {
                //业务面板不存在或业务面板模板代码与装运模板代码不一致
                if (!NotBusiness)
                {
                    if (shipmentPart == null || string.IsNullOrEmpty(shipmentPart.TemplateCode) ||
                        !shipmentPart.TemplateCode.Equals(UIConstants.ShipmentTemplateCode))
                    {
                        //加载面板
                        shipmentPart =
                            BusinessPartFactory.Get<ListBaseBusinessPart>(
                                UIConstants.ShipmentTemplateCode, _CurrentInquierRate);
                        dpShipment.Visible = true;
                        ShipmentWorkspace.Show(shipmentPart);
                    }
                }
            }
        }

        /// <summary>
        /// 面板数据绑定
        /// </summary>
        public void PanelDataBind()
        {
            //ShipmentPart
            if (CurrentDockPanel.Name.Equals("dpShipment"))
            {
                ShipmentPartBindData();
            }
            else
            {
                var dataBindControl = EnsureDockPanelControlExists();
                if (dataBindControl != null)
                {
                    dataBindControl.DataSourceBind(CurrentInquierRate);
                }
            }
        }
        /// <summary>
        /// 在当前DockPanel查找用户控件
        /// </summary>
        /// <returns></returns>
        private IInquierRateDataBind EnsureDockPanelControlExists()
        {
            UserControl userControl = null;
            //当前DockPanel未激活
            if (CurrentDockPanel == null)
                return null;
            switch (CurrentDockPanel.Name)
            {
                case "dpGeneralInfo":
                    if (IsMainInfoCutover)
                        userControl = GeneralInfoWorkspace.Controls[0] as UserControl;
                    else
                        userControl = null;
                    break;
                case "dpHistory":
                    #region History
                    if (IsMainInfoCutover)
                    {
                        InquireRatesHistoryToolBar historyToolBar = HistoryToolBarWorkspace.Controls[0] as InquireRatesHistoryToolBar;
                        if (historyToolBar != null)
                        {
                            historyToolBar.Enabled = true;
                            //historyToolBar.Enabled = true;

                            if (_CurrentInquierRate.RespondByID == LocalData.UserInfo.LoginID)
                                historyToolBar.Enabled = true;
                            else
                                historyToolBar.Enabled = false;
                            //4.询价人字符串集合
                            string inquireBys = string.Empty;
                            if (_CurrentInquierRate.InquirePriceInquireBysList != null)
                            {
                                //组合串联询价人字符串
                                foreach (var item in _CurrentInquierRate.InquirePriceInquireBysList)
                                {
                                    if (string.IsNullOrEmpty(inquireBys))
                                        inquireBys += item.InquireByEName;
                                    else
                                        inquireBys += ',' + item.InquireByEName;
                                }
                                //询价人字符串不为空，历史记录替换按钮增加提示
                                if (!string.IsNullOrEmpty(inquireBys))
                                {
                                    historyToolBar.barReplace.SuperTip.Items.Clear();
                                    historyToolBar.barReplace.SuperTip.Items.AddTitle(
                                        "Merge the new Inquire Price into the selected Inquire Price of History.When you click it, " +
                                        inquireBys + " will be noticed with the Rates.");
                                }
                            }
                        }
                        userControl = HistoryWorkspace.Controls[0] as UserControl;
                    }
                    else
                        userControl = null;
                    #endregion
                    break;
                //case "dpShipment":
                //    if (IsMainInfoCutover)
                //        userControl = ShipmentWorkspace.Controls[0] as UserControl;
                //    else
                //        userControl = null;
                //    break;
                case "dpFaxEmailEDI":
                    userControl = CommunicationHistoryWorkspace.Controls[0] as UserControl;
                    break;
            }
            if (userControl!=null)
                return userControl as IInquierRateDataBind;
            else
                return null;
        }
        /// <summary>
        /// Shipment Part 绑定数据
        /// </summary>
        private void ShipmentPartBindData()
        {
            if (_CurrentInquierRate == null)
                return;
            if (shipmentPart != null)
            {
                shipmentPart.baseBindingSource = null;
                _CurrentInquierRate.ViewCode = UIConstants.ShipmentTemplateCode;
                _CurrentInquierRate.AdvanceQueryString = "$@InquirePriceOceanID@ =  '" + _CurrentInquierRate.ID + "'";
                _CurrentInquierRate.OperationType = OperationType.OceanExport;
                _CurrentInquierRate.TopCount = 0;

                object result;
                Criteria = shipmentPart.GetQueryCriteria(!string.IsNullOrEmpty(_CurrentInquierRate.AdvanceQueryString));
                result = shipmentPart.Getter.Query(Criteria, _CurrentInquierRate);
                if (result != null)
                {
                    DataTable dt = shipmentPart.GetInnerTable(result);
                    shipmentPart.SetDataSource(dt);
                }else
                {
                    
                }
                
            }
        }
        #endregion
    }
}
