#region Comment

/*
 * 
 * FileName:    InquireOceanRatesHistoryListPart.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->海运询价历史面板
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 海运询价历史面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireOceanRatesHistoryListPart : BaseListPart, IInquierRateDataBind
    {
        #region 服务注入

        /// <summary>
        /// Work Item
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 询价服务
        /// </summary>
        [ServiceDependency]
        public IInquireRatesService inquireRatesService { get; set; }

        /// <summary>
        /// 基础数据服务
        /// </summary>
        [ServiceDependency]
        public ITransportFoundationService tfService { get; set; }

        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        /// <summary>
        /// 询价界面数据帮助类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public InquireRatesUIDataHelper InquireRatesUIDataHelper { get; set; }

        #endregion

        #region 本地变量
        /// <summary>
        /// 当前海运询价数据集
        /// </summary>
        InquierOceanRatesResult _currentServiceSource = null;

        /// <summary>
        /// 默认币种ID
        /// </summary>
        public Guid? DefaultCurrencyID
        {
            get
            {
                //获取所有币种
                ConfigureInfo configureInfo = InquireRatesUIDataHelper.ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo != null)
                {
                    return configureInfo.DefaultCurrencyID;
                }
                return null;
            }
        }

        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireOceanRatesHistoryListPart()
        {
            InitializeComponent();
            Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (!DesignMode) { InitMessage(); }
        }

        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully", "Save Successfully");
            RegisterMessage("RemoveSuccessfully", "Remove Successfully");
            RegisterMessage("RemoveSelectedItem", "Are you sure you want to remove the selected item?");
            RegisterMessage("ValidateRate_20GP", "Price must  great than zero.");
            RegisterMessage("ValidateFromDate", "Duration(Form) must be less than Duration(To).");
            RegisterMessage("ValidatePOLSamePOD", "POD can not same as POL.");
            RegisterMessage("ValidateComm", "Comm must input.");
            RegisterMessage("ValidateSurCharge", "SurCharge must input.");
            RegisterMessage("ValidateDataExist", "BasePort - Data has exist.");
            RegisterMessage("ItemCodeDifferent", " Some Itemcode are conflicted because it has two or more different Commodities.");
            RegisterMessage("MaxScreen", "&MaxScreen");
            RegisterMessage("BrackScreen", "Brack(&M)");

            RegisterMessage("SearchCommPartTitel", "Comm");
            RegisterMessage("AssociatedRatesPartTitel", "Associated Rates");
            RegisterMessage("SelectOneRate", "You should select at least one BasePort Rate.");
            RegisterMessage("GeneralInfoChanged", "General Info is changed, you should save it first.");
            RegisterMessage("CurrentChanging", "The current record is changed and has not yet been saved. \r\nClicks Yes to save the changes and go to the next record. \r\nClicks No to desert all of changing. \r\nClicks Cancel to return.");
            RegisterMessage("ValidateItemExist", "Some items are existing in the Base Port Rates {0};");
            RegisterMessage("ValidateItemCodeDifferent", "The Itemcode –{0}-- are conflicted because it has two or more different Commodities.");

            RegisterMessage("ValidateItemCode", "ItemCode must input.");

            RegisterMessage("ChangeSameName", "Would you change all data of the same name?");

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            InitComboboxSource();
            SearchRegister();
        }
        /// <summary>
        /// 初始化下拉框绑定值
        /// </summary>
        void InitComboboxSource()
        {
            #region 运输条款
            foreach (var item in InquireRatesUIDataHelper.TransportClauses)
            {
                rcmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }

            rcmbTransportClause.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierOceanRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.TransportClauseName = InquireRatesUIDataHelper.TransportClauses.Find(t => t.ID == currentrow.TransportClauseID).Code;
                }
            };

            #endregion

            #region 币种

            foreach (var curr in InquireRatesUIDataHelper.Currencys)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(curr.Code, curr.ID));
            }

            cmbCurrency.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierOceanRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.CurrencyName = InquireRatesUIDataHelper.Currencys.Find(t => t.ID == currentrow.CurrencyID).Code;
                }
            };

            #endregion

            #region 航线

            foreach (var line in InquireRatesUIDataHelper.ShippingLines)
            {
                cmbShipline.Properties.Items.Add(new ImageComboBoxItem(line.EName, line.ID));
            }

            cmbShipline.SelectedIndexChanged += delegate
            {
                treeMain.CloseEditor();
                ClientInquierOceanRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.ShippingLineName = InquireRatesUIDataHelper.ShippingLines.Find(t => t.ID == currentrow.ShippingLineID).Code;
                }
            };

            #endregion
        }
        /// <summary>
        /// Search Register
        /// </summary>
        private void SearchRegister()
        {
            #region Carrier
            dfService.RegisterTreeListColumnFinder(
                treeMain
                , colCarrier
           , CommonFinderConstants.CustomerCarrierFinder
           , new string[] { "CarrierID", "CarrierName" }
           , new string[] { "ID", "Code" }
           , null
           , delegate(object befocePickedData, object afterPickedData)
           {
           });

            #endregion
        }

        #endregion

        #region interface

        #region IListPart 成员
        /// <summary>
        /// 当前选中项
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 当前选中行
        /// </summary>
        protected ClientInquierOceanRate CurrentRow
        {
            get { return Current as ClientInquierOceanRate; }
            set
            {
                ClientInquierOceanRate currentRow = CurrentRow;
                currentRow = value;
            }
        }
        /// <summary>
        /// 数据集
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        private void BindingData(object data)
        {
            _currentServiceSource = data as InquierOceanRatesResult;

            if (_currentServiceSource == null || _currentServiceSource.InquierOceanRateList == null || _currentServiceSource.InquierOceanRateList.Count == 0)
            {
                bsList.DataSource = null;
                bsList.ResetBindings(false);
            }
            else
            {
                //备份ID.
                //用于重新加载后定位到之前行记录。
                Guid? bakID = null;
                if (bsList.DataSource != null && bsList.Current != null)
                    bakID = ((ClientInquierOceanRate)bsList.Current).ID;

                bsList.DataSource = null;
                bsList.ResetBindings(false);
                BulidGridViewColumnsByOceanUnits(_currentServiceSource.MaxUnits);
                //InitControls();
                List<ClientInquierOceanRate> source = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierOceanRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
                bsList.DataSource = source;
                bsList.ResetBindings(false);

                treeMain.BestFitColumns();
                treeMain.ExpandAll();

                //定位到之前行记录
                if (bakID != null)
                {
                    for (int i = 0; i < bsList.Count; i++)
                    {
                        var item = ((ClientInquierOceanRate)bsList[i]);
                        if (item.ID == bakID)
                        {
                            bsList.Position = i;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="index">索引</param>
        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }
        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="item"></param>
        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        /// <summary>
        /// 当前项改变后
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 当前项改变时
        /// </summary>
        public override event CancelEventHandler CurrentChanging;

        #region BulidColumns

        private List<string> _visibleColumnsNameList = null;
        /// <summary>
        /// 网格绑定箱型
        /// </summary>
        /// <param name="units"></param>
        private void BulidGridViewColumnsByOceanUnits(List<InquireUnit> units)
        {
            _visibleColumnsNameList = new List<string>();

            #region  SetVisible= false;
            colRate_45FR.Visible = false;
            colRate_40RF.Visible = false;
            colRate_45HT.Visible = false;
            colRate_20RF.Visible = false;
            colRate_20HQ.Visible = false;
            colRate_20TK.Visible = false;
            colRate_20GP.Visible = false;
            colRate_40TK.Visible = false;
            colRate_40OT.Visible = false;
            colRate_20FR.Visible = false;
            colRate_45GP.Visible = false;
            colRate_40GP.Visible = false;
            colRate_45RF.Visible = false;
            colRate_20RH.Visible = false;
            colRate_45OT.Visible = false;
            colRate_40NOR.Visible = false;
            colRate_40FR.Visible = false;
            colRate_20OT.Visible = false;
            colRate_45TK.Visible = false;
            colRate_20NOR.Visible = false;
            colRate_40HT.Visible = false;
            colRate_40RH.Visible = false;
            colRate_45RH.Visible = false;
            colRate_45HQ.Visible = false;
            colRate_20HT.Visible = false;
            colRate_40HQ.Visible = false;
            #endregion

            int visibleIndex = 6;

            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "20GP": colRate_20GP.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20GP"); break;
                    case "40GP": colRate_40GP.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40GP"); break;
                    case "40HQ": colRate_40HQ.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40HQ"); break;
                    case "45HQ": colRate_45HQ.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45HQ"); break;
                    case "20NOR": colRate_20NOR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20NOR"); break;
                    case "40NOR": colRate_40NOR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40NOR"); break;

                    case "20FR": colRate_20FR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20FR"); break;
                    case "20RH": colRate_20RH.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20RH"); break;
                    case "20RF": colRate_20RF.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20RF"); break;
                    case "20HQ": colRate_20HQ.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20HQ"); break;
                    case "20TK": colRate_20TK.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20TK"); break;
                    case "20OT": colRate_20OT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20OT"); break;
                    case "20HT": colRate_20HT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("20HT"); break;

                    case "40TK": colRate_40TK.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40TK"); break;
                    case "40OT": colRate_40OT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40OT"); break;
                    case "40FR": colRate_40FR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40FR"); break;
                    case "40HT": colRate_40HT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40HT"); break;
                    case "40RH": colRate_40RH.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40RH"); break;
                    case "40RF": colRate_40RF.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("40RF"); break;

                    case "45GP": colRate_45GP.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45GP"); break;
                    case "45RF": colRate_45RF.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45RF"); break;
                    case "45HT": colRate_45HT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45HT"); break;
                    case "45FR": colRate_45FR.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45FR"); break;
                    case "45OT": colRate_45OT.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45OT"); break;
                    case "45TK": colRate_45TK.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45TK"); break;
                    case "45RH": colRate_45RH.VisibleIndex = visibleIndex; _visibleColumnsNameList.Add("45RH"); break;
                }

                visibleIndex++;
                #endregion
            }
        }

        #endregion

        #endregion

        #endregion

        #region 窗体事件

        #region Common Event

        /// <summary>
        /// 进入网格editer时记录text,如果在离开网格时有改变,就执行搜索
        /// </summary>
        string _enterText = string.Empty;
        private void rbtnEditComm_Enter(object sender, EventArgs e)
        {
            _enterText = ((TextEdit)(sender)).Text;
        }

        int _BeforeLeaveRowHandle = -1;
        private void rbtnEditComm_Leave(object sender, EventArgs e)
        {
            //_BeforeLeaveRowHandle = treeMain.FocusedRowHandle;
            _BeforeLeaveRowHandle = treeMain.FocusedColumn.SortIndex;
            string leaveText = ((TextEdit)(sender)).Text;
            if (leaveText != _enterText)
            {
                IRSearchCommPart scf = Workitem.Items.AddNew<IRSearchCommPart>();
                scf.SetSource(InquireRatesUIDataHelper.Commoditys, leaveText);
                DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
                if (dr == DialogResult.OK && _BeforeLeaveRowHandle >= 0)
                {
                    ClientInquierOceanRate row = treeMain.GetDataRecordByNode(treeMain.FindNodeByID(_BeforeLeaveRowHandle)) as ClientInquierOceanRate;
                    if (row != null)
                    {
                        row.Commodity = scf.CommString;
                        row.IsDirty = true;
                    }

                    bsList.EndEdit();
                    treeMain.RefreshDataSource();
                }
            }
        }

        private void rbtnEditComm_KeyDown(object sender, KeyEventArgs e)
        {
            ((TextEdit)(sender)).Leave -= new EventHandler(rbtnEditComm_Leave);



            if (e.KeyCode == Keys.Enter)
            {
                string leaveText = ((TextEdit)(sender)).Text;
                if (leaveText != _enterText)
                {
                    SearchComm();
                }
                _enterText = ((TextEdit)(sender)).Text;
            }

            ((TextEdit)(sender)).Leave += new EventHandler(rbtnEditComm_Leave);
        }

        private void rbtnEditComm_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            SearchComm();
        }

        private void SearchComm()
        {
            if (CurrentRow == null) return;

            bsList.EndEdit();
            bsList.ResetCurrentItem();
            treeMain.CloseEditor();
            treeMain.RefreshDataSource();
            Validate();

            IRSearchCommPart scf = Workitem.Items.AddNew<IRSearchCommPart>();
            scf.SetSource(InquireRatesUIDataHelper.Commoditys, CurrentRow.Commodity);
            DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
            if (dr == DialogResult.OK)
            {
                CurrentRow.Commodity = scf.CommString;
                bsList.EndEdit();
                bsList.ResetCurrentItem();
                treeMain.CloseEditor();
                treeMain.RefreshDataSource();
                Validate();
            }
        }

        #endregion

        #region GridView Event
        /// <summary>
        /// 树节点样式
        /// </summary>
        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            ClientInquierOceanRate listData = (ClientInquierOceanRate)treeMain.GetDataRecordByNode(e.Node);
            if (listData == null) return;

            //IsNoPriceAll = true:都没回复价格的询价
            if (listData.IsNoPriceAll)
            {
                if (e.Node.Selected)
                    e.Appearance.BackColor = Color.LightBlue;
                else
                    e.Appearance.BackColor = Color.LightYellow;
            }
            else
            {
                if (e.Node.Selected)
                    e.Appearance.BackColor = Color.LightBlue;
                else
                    e.Appearance.BackColor = Color.White;
            }

            e.Appearance.ForeColor = Color.Black;
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        private void treeMain_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            IndicatorObjectInfoArgs args = e.ObjectArgs as IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }

        #endregion

        /// <summary>
        /// 当前行改变
        /// </summary>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        } 
        #endregion

        #region 方法定义
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据对象</param>
        public void DataSourceBind(object data)
        {
            ClientInquierOceanRate inquierOceanRate = data as ClientInquierOceanRate;

            if (inquierOceanRate == null)
            {
                DataSource = null;
                Enabled = false;
                return;
            }

            Enabled = true;

            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    //获取询价历史记录
                    InquierOceanRatesResult item = inquireRatesService.GetInquireOceanRateHistoryList(inquierOceanRate.ID, inquierOceanRate.MainRecordID,
                        inquierOceanRate.POLID, inquierOceanRate.PODID, inquierOceanRate.PlaceOfDeliveryID, inquierOceanRate.CarrierID);

                    DataSource = item;

                    string message = string.Empty;
                    if (item == null || item.InquierOceanRateList == null)
                    {
                        message = "Nothing found!";
                    }
                    else
                    {
                        message = string.Format("{0} records found", item.InquierOceanRateList.Count);
                    }

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion
    }
}
