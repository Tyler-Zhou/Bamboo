#region Comment

/*
 * 
 * FileName:    InquireOceanRatesWorkitem.cs
 * CreatedOn:   
 * CreatedBy:   LiXuBin
 * 
 * 
 * Description：
 *      ->海运询价界面运行容器
 *      ->1.初始化海运询价界面布局及其事件
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Windows.Forms;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 海运询价界面运行容器
    /// </summary>
    public class InquireOceanRatesWorkitem : WorkItem
    {
        #region 服务

        /// <summary>
        /// 询价服务
        /// </summary>
        [ServiceDependency]
        public IInquireRatesService inquireRatesService { get; set; }
      
        #endregion

        public InquireOceanRatesMainWorkspace OceanRateMainWorkSpace
        {
            get;
            set;
        }

        #region Override
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        } 
        #endregion

        #region Show
        
        /// <summary>
        /// 显示面板
        /// </summary>
        private void Show()
        {
            InquireOceanRatesMainWorkspace srMainWorkspace = SmartParts.Get<InquireOceanRatesMainWorkspace>("InquireOceanRatesMainWorkspace");
            IWorkspace mainWorkspace = Workspaces[InquireRatesWorkSpaceConstants.OceanRatesWorkspace];
            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<InquireOceanRatesMainWorkspace>("InquireOceanRatesMainWorkspace");

                #region AddPart(增加面板)
                //询价信息集合
                InquireOceanRatesListPart ioMainListPart = SmartParts.AddNew<InquireOceanRatesListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[InquireOceanRatesWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(ioMainListPart);
                //沟通历史记录
                InquireOceanRatesEmailPart mailPart = SmartParts.AddNew<InquireOceanRatesEmailPart>();
                mailPart.Dock = DockStyle.Fill;
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[InquireOceanRatesWorkSpaceConstants.CommunicationHistoryWorkspace];
                faxMailListWorkspace.Show(mailPart);
                //询价历史记录工具栏
                InquireRatesHistoryToolBar historyToolBar = SmartParts.AddNew<InquireRatesHistoryToolBar>();
                historyToolBar.Dock = DockStyle.Fill;
                IWorkspace historyToolBarWorkspace = (IWorkspace)Workspaces[InquireOceanRatesWorkSpaceConstants.HistoryToolBarWorkspace];
                historyToolBarWorkspace.Show(historyToolBar);
                //询价历史记录集合
                InquireOceanRatesHistoryListPart historyListPart = SmartParts.AddNew<InquireOceanRatesHistoryListPart>();
                historyListPart.Dock = DockStyle.Fill;
                IWorkspace historyWorkspace = (IWorkspace)Workspaces[InquireOceanRatesWorkSpaceConstants.HistoryWorkspace];
                historyWorkspace.Show(historyListPart);
                //询价通用信息
                InquireOceanRatesGeneralInfoPart generalInfoPart = SmartParts.AddNew<InquireOceanRatesGeneralInfoPart>();
                IWorkspace generalInfoWorkspace = (IWorkspace)Workspaces[InquireOceanRatesWorkSpaceConstants.GeneralInfoWorkspace];
                generalInfoWorkspace.Show(generalInfoPart);

                //当前登录用户是否非商务部
                bool notBusiness = !LocalData.UserInfo.DefaultDepartmentName.Contains("商务") &&
                                     !LocalData.UserInfo.DefaultDepartmentName.Contains("Business");
                //非商务操作时移除业务信息面板
                if (notBusiness)
                    srMainWorkspace.dpShipment.Visible = false;
                //else
                //{
                //    InquireOceanRatesShipmentPart ShipmentPart = this.SmartParts.AddNew<InquireOceanRatesShipmentPart>();
                //    IWorkspace shipmentWorkspace = (IWorkspace)this.Workspaces[InquireOceanRatesWorkSpaceConstants.ShipmentWorkspace];
                //    shipmentWorkspace.Show(ShipmentPart);
                //}

                ioMainListPart.GeneralInfoPart = generalInfoPart;
                ioMainListPart.GeneralInfoPart.ChangedUnitEvent += ioMainListPart.GeneralInfoPart_ChangedUnitEvent;
                #endregion

                #region 定义面板连接

                #region 询价信息集合当前选择行信息改变之前
                ioMainListPart.CurrentChanging += (sender, e) =>
                {
                    bool isCancel = false;
                    if (ioMainListPart.IsChanged || generalInfoPart.IsChanged)
                    {
                        #region 当前选择行改变

                        //行转换之前提示信息是否保存
                        DialogResult result = XtraMessageBox.Show(
                            NativeLanguageService.GetText(ioMainListPart, "CurrentChanging")
                            , "Tip"
                            , MessageBoxButtons.YesNoCancel
                            , MessageBoxIcon.Question);

                        if (result == DialogResult.Cancel) isCancel = true;
                        else if (result == DialogResult.No)
                        {
                            //重置行信息(原始)
                            ioMainListPart.ResetCurrent();
                            isCancel = false;
                        }
                        else
                        {
                            //网格信息改变
                            if (ioMainListPart.IsChanged)
                            {
                                //保存列表信息
                                isCancel = !ioMainListPart.SaveRateList();
                            }
                            else //通用面板信息改变
                            {
                                //保存通用面板信息
                                generalInfoPart.RaiseSaved();
                            }
                        }

                        #endregion
                    }
                    e.Cancel = isCancel;
                };
                #endregion

                #region 询价信息集合当前选择行信息改变后
                ioMainListPart.CurrentChanged += (sender, data) =>
                {
                    //当前选择行数据
                    ClientInquierOceanRate currentData = data as ClientInquierOceanRate;
                    if (currentData == null) //为空
                    {
                        //各面板清空原有数据
                        srMainWorkspace.CurrentInquierRate = null;
                        mailPart.DataSourceBind(null);
                        generalInfoPart.DataSourceBind(null);
                        historyListPart.DataSourceBind(null);
                        historyToolBar.Enabled = false;
                    }
                    else
                    {
                        srMainWorkspace.NotBusiness = notBusiness;
                        srMainWorkspace.CurrentInquierRate = currentData;
                        srMainWorkspace.InitShipmentPart();
                        srMainWorkspace.PanelDataBind();
                        #region Comment Code
                        //#region 询价信息当前选中行不为空

                        //#region 只有主询价信息改变，子面板信息才随之改变

                        ////判断子面板对应询价信息是否为当前选中主询价
                        ////否则加载重新查询子面板对应数据
                        //if (mailPart.CurrentInquierRate == null ||
                        //    (currentData.ID != mailPart.CurrentInquierRate.ID &&
                        //     currentData.ID != mailPart.CurrentInquierRate.MainRecordID &&
                        //     currentData.MainRecordID != mailPart.CurrentInquierRate.ID &&
                        //     (currentData.MainRecordID == null ||
                        //      currentData.MainRecordID != mailPart.CurrentInquierRate.MainRecordID)))
                        //{
                        //    //1.获取询价询问人列表
                        //    currentData.InquirePriceInquireBysList =
                        //        inquireRatesService.GetInquirePriceInquireBys(currentData.ID, currentData.MainRecordID);
                        //    //2.generalInfoPart设置数据源
                        //    generalInfoPart.DataSource = currentData;
                        //    //3.historyToolBar设置数据源
                        //    historyListPart.SetData(this, currentData);
                        //    historyToolBar.Enabled = true;

                        //    if (currentData.RespondByID == LocalData.UserInfo.LoginID)
                        //        historyToolBar.Enabled = true;
                        //    else
                        //        historyToolBar.Enabled = false;
                        //    //4.询价人字符串集合
                        //    string inquireBys = string.Empty;
                        //    //组合串联询价人字符串
                        //    foreach (var item in currentData.InquirePriceInquireBysList)
                        //    {
                        //        if (string.IsNullOrEmpty(inquireBys))
                        //            inquireBys += item.InquireByEName;
                        //        else
                        //            inquireBys += ',' + item.InquireByEName;
                        //    }
                        //    //询价人字符串不为空，历史记录替换按钮增加提示
                        //    if (!string.IsNullOrEmpty(inquireBys))
                        //    {
                        //        historyToolBar.barReplace.SuperTip.Items.Clear();
                        //        historyToolBar.barReplace.SuperTip.Items.AddTitle(
                        //            "Merge the new Inquire Price into the selected Inquire Price of History.When you click it, " +
                        //            inquireBys + " will be noticed with the Rates.");
                        //    }
                        //    //5 业务面板不存在或业务面板模板代码与装运模板代码不一致
                        //    if (!isNotBusiness)
                        //    {
                        //        if (shipmentPart == null || string.IsNullOrEmpty(shipmentPart.TemplateCode) ||
                        //            !shipmentPart.TemplateCode.Equals(UIConstants.ShipmentTemplateCode))
                        //        {
                        //            //加载面板
                        //            shipmentPart =
                        //                BusinessPartFactory.Get<ICP.Operation.Common.UI.ListBaseBusinessPart>(
                        //                    UIConstants.ShipmentTemplateCode, currentData);
                        //            IWorkspace shipmentWorkspace = 
                        //                (IWorkspace)this.Workspaces[InquireOceanRatesWorkSpaceConstants.ShipmentWorkspace];
                        //            srMainWorkspace.dpShipment.Visible = true;
                        //            shipmentWorkspace.Show(shipmentPart);
                        //        }
                        //    }
                        //} 
                        ////6.为商务操作且在业务面板
                        //if (!notBusiness &&
                        //    srMainWorkspace.CurrentDockPanel == srMainWorkspace.dpShipment)
                        //{
                        //    int theradID = 0;
                        //    theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
                        //    //绑定数据
                        //    BindingData(currentData);
                        //    ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
                        //}

                        #endregion

                        //#endregion

                        ////不管是否选择主询价，沟通记录信息都查询
                        ////CommunicationHistory设置数据源
                        //mailPart.SetDataSource(currentData);

                        //#endregion
                    }
                };
                #endregion

                #region 询价历史记录面板当前选中行改变后
                //在为商务操作时设置数据源
                historyListPart.CurrentChanged += (sender, data) =>
                {
                    //当前选中询价单
                    ClientInquierOceanRate currentData = data as ClientInquierOceanRate;
                    //判断当前选中询价单信息
                    if (currentData == null)
                    {
                        historyToolBar.DataSource = null;
                    }
                    if (!notBusiness &&
                        srMainWorkspace.CurrentDockPanel == srMainWorkspace.dpShipment)
                    {
                        historyToolBar.DataSource = currentData;
                    }
                };

                //设置工具栏数据源
                historyListPart.CurrentChanged += delegate(object sender, object data)
                       {
                           ClientInquierOceanRate currentData = data as ClientInquierOceanRate;
                           if (currentData == null)
                           {
                               historyToolBar.DataSource = null;
                           }
                           else
                           {
                               historyToolBar.DataSource = currentData;
                           }
                       }; 
                #endregion

                #endregion

                
                if (mainWorkspace!=null)
                    mainWorkspace.Show(srMainWorkspace);
            }
            else
            {
                if (mainWorkspace != null)
                    mainWorkspace.Activate(srMainWorkspace);
            }
            OceanRateMainWorkSpace = srMainWorkspace;
        }

        #endregion

        #region Comment Code
        ///// <summary>
        ///// 业务工厂名称
        ///// </summary>
        //private string businessFactoryName = "businessFactoryName";

        ///// <summary>
        ///// 业务面板
        ///// </summary>
        //public BusinessPartFactory BusinessPartFactory
        //{
        //    get
        //    {
        //        if (RootWorkItem.Items.Contains(businessFactoryName))
        //        {
        //            return RootWorkItem.Items.Get<BusinessPartFactory>(businessFactoryName);
        //        }
        //        else
        //        {
        //            BusinessPartFactory businessFactroy = RootWorkItem.Items.AddNew<BusinessPartFactory>(businessFactoryName);
        //            return businessFactroy;
        //        }
        //    }
        //} 
        ///// <summary>
        ///// 业务信息面板
        ///// </summary>
        //ICP.Operation.Common.UI.ListBaseBusinessPart shipmentPart;

        ///// <summary>
        ///// 船公司
        ///// </summary>
        //public BusinessQueryCriteria Criteria { get; set; }

        ///// <summary>
        ///// 设置数据源
        ///// </summary>
        ///// <param name="data"></param>
        //void SetData(object data)
        //{
        //    ClientInquierOceanRate currentData = data as ClientInquierOceanRate;
        //    shipmentPart.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => BindingData(currentData)));
        //}
        ///// <summary>
        ///// 绑定数据
        ///// </summary>
        ///// <param name="currentData">询价信息对象</param>
        //private void BindingData(ClientInquierOceanRate currentData)
        //{
        //    shipmentPart.baseBindingSource = null;
        //    //shipmentPart.BindData(currentData);
        //    currentData.ViewCode = UIConstants.ShipmentTemplateCode;
        //    currentData.AdvanceQueryString = "$@InquirePriceOceanID@ =  '" + currentData.ID + "'";
        //    currentData.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
        //    currentData.TopCount = 0;

        //    object result;
        //    Criteria = shipmentPart.GetQueryCriteria(!string.IsNullOrEmpty(currentData.AdvanceQueryString));
        //    result = shipmentPart.Getter.Query(Criteria, currentData);
        //    DataTable dt = shipmentPart.GetInnerTable(result);
        //    shipmentPart.SetDataSource(dt);
        //}
        #endregion
    }

    #region Constants

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class InquireOceanRatesWorkSpaceConstants
    {
        /// <summary>
        /// Inquire Rates List Workspace(询价集合信息容器)
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// General Info Workspace(通用信息容器)
        /// </summary>
        public const string GeneralInfoWorkspace = "GeneralInfoWorkspace";
        /// <summary>
        /// Communication History Workspace(沟通历史记录容器)
        /// </summary>
        public const string CommunicationHistoryWorkspace = "CommunicationHistoryWorkspace";
        /// <summary>
        /// History ToolBar Workspace(询价历史记录工具栏容器)
        /// </summary>
        public const string HistoryToolBarWorkspace = "HistoryToolBarWorkspace";
        /// <summary>
        /// History Workspace(询价历史记录容器)
        /// </summary>
        public const string HistoryWorkspace = "HistoryWorkspace";
        /// <summary>
        /// Shipment Workspace(业务容器)
        /// </summary>
        public const string ShipmentWorkspace = "ShipmentWorkspace";
    }

    #endregion
}
