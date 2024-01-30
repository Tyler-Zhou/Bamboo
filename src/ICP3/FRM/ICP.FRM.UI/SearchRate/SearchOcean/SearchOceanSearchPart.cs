using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.UI.ClientService;
using ICP.FRM.UI.OceanPrice;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FRM.UI.SearchRate
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class SearchOceanSearchPart : BaseSearchPart
    {
        #region 构造函数
        public SearchOceanSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                searchOceanParameter = null;
                if (dateMonthControl1 != null)
                {
                    dateMonthControl1.DataValueChaned -= dateMonthControl1_DataValueChaned;
                    dateMonthControl1 = null;
                }
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }
        #endregion

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ISearchRatesService SearchRatesService
        {
            get
            {
                return ServiceClient.GetService<ISearchRatesService>();
            }
        }

        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }
        #endregion

        #region 字段,属性,委托,事件委托定义
        /// <summary>
        /// 是否需要重新绑定船东
        /// </summary>
        private bool isBindCarrier = true;
        /// <summary>
        /// 是否需要重新绑定Port
        /// </summary>
        private bool isBindPort = true;
        /// <summary>
        /// 是否需要重新绑定Comm
        /// </summary>
        private bool isBindComm = true;

        SearchOceanParameter searchOceanParameter = new SearchOceanParameter();
        public override event SearchResultHandler OnSearched;
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null || values.Count <= 0)
            {
                return;
            }
            cmbShipline.ClearEditValue();
            cmbCarrier.ClearEditValue();
            cmbPOL.ClearEditValue();
            cmbPOD.ClearEditValue();
            cmbDelivery.ClearEditValue();
            cmbCommodity.ClearEditValue();
            txtContractNo.Text = string.Empty;
            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();

                switch (item.Key)
                {
                    case "CarrierName":
                        cmbCarrier.SetEditText(value);
                        break;
                    case "POLName":
                        cmbPOL.SetEditText(value);
                        break;
                    case "PODName":
                        cmbPOD.SetEditText(value);
                        break;
                    case "DeliveryName":
                        cmbDelivery.SetEditText(value);
                        break;
                    case "Commodity":
                        cmbCommodity.SetEditText(value);
                        break;
                    case "ContractNo":
                        txtContractNo.Text = value;
                        break;
                }
            }


        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitMessage();
                InitToolTip();
                SetLazyLoaders();
                InitControls();
                SetKeyDownToSearch();
            }
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1112010001", "Please input Code, Chinese name or English name.");
            RegisterMessage("1112010002", "You can use semicolons for dividing multi-Ports, e.g. Yuantian; Hongkong");
            RegisterMessage("1112010003", "You can use semicolons for dividing multi-Commodity, e.g. FAK1; FAK2");
            RegisterMessage("1112010004", "Please input Code, Chinese name or English name");
            RegisterMessage("1112010005", "Please input English name");
            RegisterMessage("1112010006", "Please input English name");


        }

        /// <summary>
        /// 初始化ToolTip
        /// </summary>
        private void InitToolTip()
        {
            cmbCarrier.ToolTip = NativeLanguageService.GetText(this, "1112010001");
            cmbPOD.ToolTip =cmbPOL.ToolTip=cmbDelivery.ToolTip= NativeLanguageService.GetText(this, "1112010002");
            cmbCommodity.ToolTip = NativeLanguageService.GetText(this, "1112010003");
           // this.cmbPOD.NullText =this.cmbPOL.NullText=this.cmbDelivery.NullText= NativeLanguageService.GetText(this, "1112010004");

        }
        /// <summary>
        /// 延迟加载
        /// </summary>
        private void SetLazyLoaders()
        {
            //航线   
            Utility.SetEnterToExecuteOnec(cmbShipline, delegate
            {
                List<ShippingLineList> list = OceanPriceUIDataHelper.ShippingLines;
               
                foreach (ShippingLineList item in list)
                {
                    string name = LocalData.IsEnglish ? item.EName : item.CName;
                    cmbShipline.AddItem(item.ID, name);
                }
            });
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {   
            #region 查询运价状态
            cmbScope.ShowSelectedValue(SearchPriceStatus.All,"All");
            Utility.SetEnterToExecuteOnec(cmbScope, delegate
            {
                List<EnumHelper.ListItem<SearchPriceStatus>> searchPriceStatus
                    = EnumHelper.GetEnumValues<SearchPriceStatus>(LocalData.IsEnglish);
                cmbScope.Properties.BeginUpdate();
                cmbScope.Properties.Items.Clear();
                foreach (var item in searchPriceStatus)
                {
                    cmbScope.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
                }
                cmbScope.Properties.EndUpdate();
                cmbScope.SelectedIndex = 0;
            });
            #endregion
            
            #region 查询运价类型
            cmbType.ShowSelectedValue(OceanTypeBySearch.All, "All");
            Utility.SetEnterToExecuteOnec(cmbType, delegate
            {
                List<EnumHelper.ListItem<OceanTypeBySearch>> oceanType = EnumHelper.GetEnumValues<OceanTypeBySearch>(LocalData.IsEnglish);
                cmbType.Properties.BeginUpdate();
                cmbType.Properties.Items.Clear();
                foreach (var item in oceanType)
                {
                    cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
                cmbType.Properties.EndUpdate();
                cmbType.SelectedIndex = 0;
            });
            #endregion



            dateMonthControl1.From = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            dateMonthControl1.To = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(15);


            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            {
                txtContractNo, cmbShipline,cmbCarrier,cmbPOL,cmbPOD,cmbDelivery,cmbDelivery,cmbCommodity
            }, btnSearch);

            Utility.SearchPartKeyF2ToSearch(new List<Control> 
            {
                txtContractNo, cmbShipline,cmbCarrier,cmbPOL,cmbPOD,cmbDelivery,cmbDelivery,cmbCommodity
            }, btnClear);

        }
        /// <summary>
        /// 定义快捷键
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);

                if (item is CheckBoxComboBox)
                {
                    ((CheckBoxComboBox)item).KeyDownEnter += new KeyEventHandler(item_KeyDown);
                }
            }

        }
        /// <summary>
        /// 实现快速键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定船东
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCarrier_Enter(object sender, EventArgs e)
        {
            if (isBindCarrier)
            {
                Dictionary<Guid, string> list = new Dictionary<Guid, string>();

                try
                {
                    Guid[] shipLineIDs = GetShipLineIDs;
                    if (shipLineIDs != null && shipLineIDs.Length > 0)
                        list = SearchRatesService.GetOceanRateCarrierList(GetStatus, dateMonthControl1.From, dateMonthControl1.To, shipLineIDs);

                    cmbCarrier.BeginUpdate();
                    foreach (var item in list)
                    {
                        cmbCarrier.AddItem(item.Key, item.Value);
                    }
                    cmbCarrier.EndUpdate();

                    isBindCarrier = false;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }

        }
        /// <summary>
        /// 初始化POL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPOL_Enter(object sender, EventArgs e)
        {
            InitPortList();
        }
        /// <summary>
        /// 初始化POD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPOD_Enter(object sender, EventArgs e)
        {
            InitPortList();
        }
        /// <summary>
        /// 初始化Delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDelivery_Enter(object sender, EventArgs e)
        {
            InitPortList();
        }
        /// <summary>
        /// 初始化FinalDestination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFinalDestination_Enter(object sender, EventArgs e)
        {
            InitPortList();
        }
        
        /// <summary>
        /// 初始化港口列表
        /// </summary>
        private void InitPortList()
        {
            if (!isBindPort)
                return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<SearchPortList> list = new List<SearchPortList>();
                    Guid[] shipLineIDs = GetShipLineIDs;
                    if (shipLineIDs != null && shipLineIDs.Length > 0)
                        list = SearchRatesService.GetPortList(GetStatus, dateMonthControl1.From, dateMonthControl1.To, GetShipLineIDs, GetCarrierIDs);
                    cmbDelivery.BeginUpdate();
                    cmbPOD.BeginUpdate();
                    cmbPOL.BeginUpdate();
                    cmbFinalDestination.BeginUpdate();

                    cmbDelivery.ClearItems();
                    cmbPOD.ClearItems();
                    cmbPOL.ClearItems();
                    cmbFinalDestination.ClearItems();
                    
                    foreach (SearchPortList item in list)
                    {
                        if (item.Porttype == PortType.Delivery)
                        {
                            cmbDelivery.AddItem(item.ID, item.PortName);
                        }
                        else if (item.Porttype == PortType.POD)
                        {
                            cmbPOD.AddItem(item.ID, item.PortName);
                        }
                        else if (item.Porttype == PortType.POL)
                        {
                            cmbPOL.AddItem(item.ID, item.PortName);
                        }
                        else if (item.Porttype == PortType.FinalDestination)
                        {
                            cmbFinalDestination.AddItem(item.ID, item.PortName);
                        }
                    }
                    cmbDelivery.EndUpdate();
                    cmbPOD.EndUpdate();
                    cmbPOL.EndUpdate();
                    cmbFinalDestination.EndUpdate();
                    isBindPort = false;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }

        }

        /// <summary>
        /// 初始化品名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCommodity_Enter(object sender, EventArgs e)
        {
            if (!isBindComm)
                return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    Dictionary<Guid, string> list = SearchRatesService.GetCommodityList(GetStatus, dateMonthControl1.From, dateMonthControl1.To);
                    cmbCommodity.BeginUpdate();
                    foreach (var item in list)
                    {
                        cmbCommodity.AddItem(item.Key, item.Value);
                    }
                    cmbCommodity.EndUpdate();
                    isBindComm = false;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }

        }

        #endregion

        #region 控件事件

        /// <summary>
        /// 航线发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbShipline_EditValueChanged(object sender, EventArgs e)
        {
            ResetCarrierAndPort();
        }
        /// <summary>
        /// 状态发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetCarrierAndPort();
        }
        /// <summary>
        /// 重置其他查询数据
        /// </summary>
        private void ResetCarrierAndPort()
        {
            //this.cmbCarrier.ClearItems();
            //this.cmbPOD.ClearItems();
            //this.cmbPOL.ClearItems();
            //this.cmbDelivery.ClearItems();
            //this.cmbCommodity.ClearItems();

            isBindComm = true;
            isBindCarrier = true;
            isBindPort = true;
        }

        /// <summary>
        /// 船东发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCarrier_EditValueChanged(object sender, EventArgs e)
        {
            //this.cmbPOD.ClearItems();
            //this.cmbPOL.ClearItems();
            //this.cmbDelivery.ClearItems();

            isBindPort = true;
        }
        /// <summary>
        /// 日期发生改变
        /// </summary>
        /// <param name="dataCharngedType"></param>
        private void dateMonthControl1_DataValueChaned(DataCharngedType dataCharngedType)
        {
            isBindComm = true;
            isBindCarrier = true;
            isBindPort = true;
        }

        #endregion

        #region 获得控件数据
        /// <summary>
        /// 当前状态
        /// </summary>
        private SearchPriceStatus GetStatus
        {
            get
            {
                return (SearchPriceStatus)cmbScope.SelectedIndex;
            }
        }

        /// <summary>
        /// 获得当前航线ID集合
        /// </summary>
        private Guid[] GetShipLineIDs
        {
            get
            {
                string text = cmbShipline.SelectValues;
                return Utility.GetIds(text);

            }
        }
        /// <summary>
        /// 船东ID集合
        /// </summary>
        private Guid[] GetCarrierIDs
        {
            get
            {
                string text = cmbCarrier.SelectValues;
                return Utility.GetIds(text);

            }
        }

        /// <summary>
        /// POL ID集合
        /// </summary>
        private Guid[] GetPOLIDs
        {
            get
            {
                string text = cmbPOL.SelectValues;
                return Utility.GetIds(text);
            }
        }

        /// <summary>
        /// POL ID集合
        /// </summary>
        private Guid[] GetPODIDs
        {
            get
            {
                string text = cmbPOD.SelectValues;
                return Utility.GetIds(text);
            }
        }

        /// <summary>
        /// Delivery ID集合
        /// </summary>
        private Guid[] GetDeliveryIDs
        {
            get
            {
                string text = cmbDelivery.SelectValues;
                return Utility.GetIds(text);

            }
        }
        /// <summary>
        /// FinalDestination ID集合
        /// </summary>
        private Guid[] GetFinalDestinationIDs
        {
            get
            {
                string text = cmbFinalDestination.SelectValues;
                return Utility.GetIds(text);
            }
        }

        #endregion

        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbShipline.ClearEditValue();
            cmbCarrier.ClearEditValue();
            cmbPOL.ClearEditValue();
            cmbPOD.ClearEditValue();
            cmbDelivery.ClearEditValue();
            cmbFinalDestination.ClearEditValue();
            cmbCommodity.ClearEditValue();
            txtContractNo.Text = string.Empty;
            cmbScope.SelectedIndex = 0;
        }


        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchOceanParameter.ShiplineIDs = cmbShipline.SelectValuesToGuid;
                searchOceanParameter.CarrierIDs = cmbCarrier.SelectValuesToGuid;
                searchOceanParameter.PolNames = cmbPOL.GetTexts().ToArray();
                searchOceanParameter.PodNames = cmbPOD.GetTexts().ToArray();
                searchOceanParameter.DeliveryNames = cmbDelivery.GetTexts().ToArray();
                searchOceanParameter.FinalDestinationNames = cmbFinalDestination.GetTexts().ToArray();
                searchOceanParameter.Commoditys = cmbCommodity.SelectTextToString;
                searchOceanParameter.ContractNo = txtContractNo.Text;
                searchOceanParameter.Status = (SearchPriceStatus)cmbScope.EditValue;
                searchOceanParameter.RateType = (OceanTypeBySearch)cmbType.EditValue;
                searchOceanParameter.DurationStart = dateMonthControl1.From;
                searchOceanParameter.DurationEnd = dateMonthControl1.To;

                searchOceanParameter.DataPageInfo.CurrentPage = 1;

                if (string.IsNullOrEmpty(searchOceanParameter.DataPageInfo.SortByName))
                {
                    searchOceanParameter.DataPageInfo.SortByName = "CreateDate";
                    searchOceanParameter.DataPageInfo.SortOrderType = SortOrderType.Asc;
                }

                if (OnSearched != null)
                {
                    PageList list = GetData() as PageList;
                    if (list != null && list.DataPageInfo != null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                    + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }
                    OnSearched(this, list);
                }
            }
        }
        #endregion

        #region 重写
        
        
        /// <summary>
        /// 热键查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="data"></param>
        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchOceanParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
            {
                OnSearched(this, GetData());
            }
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                
                PageList list = SearchRatesService.GetSearchOceanList(
                                                     LocalData.UserInfo.LoginID,
                                                     searchOceanParameter.ShiplineIDs,
                                                     searchOceanParameter.CarrierIDs,
                                                     searchOceanParameter.PolNames,
                                                     searchOceanParameter.PodNames,
                                                     searchOceanParameter.DeliveryNames,
                                                     searchOceanParameter.FinalDestinationNames,
                                                     searchOceanParameter.Commoditys,
                                                     searchOceanParameter.ContractNo,
                                                     searchOceanParameter.DurationStart,
                                                     searchOceanParameter.DurationEnd,
                                                     searchOceanParameter.Status,
                                                     searchOceanParameter.RateType,
                                                     searchOceanParameter.DataPageInfo);

                #region 根据权限生定义是否显示底价
                //SearchPricePermission viewType=Utility.SearchOceanPermissionType
                //foreach (var item in list.GetList<SearchOceanRateList>())
                //{
                //    if (viewType==SearchPricePermission.ViewReserve)
                //    {
                //        foreach (var unit in item.UnitList) 
                //        {
                //            unit.Rate = unit.ReserveRate;
                //        }
                //    }
                //    else
                //    {
                //        foreach (var unit in item.UnitList) 
                //        {
                //            unit.Rate = unit.SalesRate;
                //        }
                //    }
                //}
                #endregion
                return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return null;
            }
            

        }
        #endregion

        #region 导入到Excel
        /// <summary>
        /// 导入到Excel
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(SearchOceanCommandConstants.Command_ExportToExcel)]
        public void Command_ExportToExcel(object o, EventArgs e)
        {
            if (searchOceanParameter == null)
            {
                return;
            }
            if((searchOceanParameter.CarrierIDs==null||searchOceanParameter.CarrierIDs.Length==0)&&
               (searchOceanParameter.Commoditys==null||searchOceanParameter.Commoditys.Length==0)&&
               (searchOceanParameter.PolNames == null || searchOceanParameter.PolNames.Length == 0) &&
               (searchOceanParameter.PolNames == null || searchOceanParameter.PolNames.Length == 0) &&
               (searchOceanParameter.DeliveryNames == null || searchOceanParameter.DeliveryNames.Length == 0) &&
               (searchOceanParameter.FinalDestinationNames == null || searchOceanParameter.FinalDestinationNames.Length == 0) &&
               (searchOceanParameter.ShiplineIDs==null||searchOceanParameter.ShiplineIDs.Length==0)&&
               (string.IsNullOrEmpty(searchOceanParameter.ContractNo)))
            {
                XtraMessageBox.Show("input Search file") ;
                return;
            }

            if (!LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_EXPORTTOEXCEL))
            {
                XtraMessageBox.Show("没有权限执行此操作");
                return;
            }

            using (new CursorHelper(Cursors.WaitCursor))
            {
                int theradID = 0;
                try
                {
                    SaveFileDialog saveFile = new SaveFileDialog
                    {
                        Filter = "xls files(*.xls)|*.xls",
                        RestoreDirectory = true,
                        FilterIndex = 2
                    };
                    if (saveFile.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    string fileName = saveFile.FileName;

                    theradID=LoadingServce.ShowLoadingForm("Exporting......");


                    OceanRateToExcel list = SearchRatesService.ExportOceanRateToExcelBySearchOcean(
                                            LocalData.UserInfo.LoginID,
                                            searchOceanParameter.ShiplineIDs,
                                            searchOceanParameter.CarrierIDs,
                                            searchOceanParameter.PolNames,
                                            searchOceanParameter.PodNames,
                                            searchOceanParameter.DeliveryNames,
                                            searchOceanParameter.FinalDestinationNames,
                                            searchOceanParameter.Commoditys,
                                            searchOceanParameter.ContractNo,
                                            searchOceanParameter.DurationStart,
                                            searchOceanParameter.DurationEnd,
                                            searchOceanParameter.Status,
                                            searchOceanParameter.RateType,
                                            searchOceanParameter.DataPageInfo);



                    OceanRateExportToExcel toExcelForm = Workitem.Items.AddNew<OceanRateExportToExcel>();
                    toExcelForm.BindData(list.DataList, list.UnitList);

                    //PartLoader.ShowDialog(toExcelForm,"");

                    toExcelForm.ExportToExcel(fileName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);

                }
            }

        }

        /// <summary>
        /// FTP升级到云服务
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        [CommandHandler(SearchOceanCommandConstants.Command_UpgradeCloud)]
        public void Command_Command_UpgradeCloud(object o, EventArgs e)
        {
            try
            {
                new OceanPriceFileClientService().UpgradeCloud();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("升级发生异常:" + ex.Message);
            }
        }

        //searchOceanParameter
        #endregion

    }
    /// <summary>
    /// 查询实体
    /// </summary>
    public class SearchOceanParameter
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        public SearchOceanParameter()
        {
            DataPageInfo = new DataPageInfo();
        }
        /// <summary>
        /// 航线ID集合
        /// </summary>
        public Guid[] ShiplineIDs { get; set; }
        /// <summary>
        /// 船东ID集合
        /// </summary>
        public Guid[] CarrierIDs { get; set; }
        /// <summary>
        /// POL ID 集合
        /// </summary>
        public string[] PolNames { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string[] PodNames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] DeliveryNames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] FinalDestinationNames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Commoditys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DurationStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DurationEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OceanTypeBySearch RateType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SearchPriceStatus Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataPageInfo DataPageInfo { get; set; }
    }
}
