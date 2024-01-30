using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraTreeList.Columns;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireTruckingRatesHistoryListPart : BaseListPart, IInquierRateDataBind
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 询价操作服务
        /// </summary>
        public IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }

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
        /// 询价UI数据帮助类
        /// </summary>
        public InquireRatesUIDataHelper InquireRatesUIDataHelper
        {
            get
            {
                return ClientHelper.Get<InquireRatesUIDataHelper, InquireRatesUIDataHelper>();
            }
        }

        #endregion

        #region Local variables
        /// <summary>
        /// 是否加载当前页
        /// </summary>
        bool _hadLoadCurrentPage = false;

        /// <summary>
        /// 当前查询结果
        /// </summary>
        InquierTruckingRatesResult _currentServiceSource = null;

        #region 属性-当前数据源
        /// <summary>
        /// 当前数据源
        /// </summary>
        List<ClientInquierTruckingRate> CurrentSource
        {
            get { return bsList.DataSource as List<ClientInquierTruckingRate>; }
        }
        #endregion

        #region 属性-当前选择拖车询价
        /// <summary>
        /// 当前选择拖车询价
        /// </summary>
        List<ClientInquierTruckingRate> SelectedTruckingItem
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;
                return (from TreeListNode item in treeMain.Selection select treeMain.GetDataRecordByNode(item) as ClientInquierTruckingRate).ToList();
            }
        }
        #endregion

        #region 属性-默认币种
        /// <summary>
        /// 默认币种
        /// </summary>
        public Guid? DefaultCurrencyID
        {
            get
            {
                if (InquireRatesUIDataHelper.ConfigureInfo != null)
                    return InquireRatesUIDataHelper.ConfigureInfo.DefaultCurrencyID;
                return null;
            }
        }
        #endregion

        #endregion

        #region  Init

        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireTruckingRatesHistoryListPart()
        {
            InitializeComponent();
            EventRegister(false);
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                _currentServiceSource = null;
                treeMain.DataSource = null;
                bsList.DataSource = null;
                EventRegister(true);
                bsList.Dispose();
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }

        /// <summary>
        /// 注册Message提示字符串
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
            RegisterMessage("FilterPartTitel", "Base Port Rates Filter");
            RegisterMessage("SearchCommPartTitel", "Comm");
            RegisterMessage("AssociatedRatesPartTitel", "Associated Rates");
            RegisterMessage("BatchItemFaily", "Batch Item Faily.");
            RegisterMessage("RemoveSuccessfully", "Remove Successfully");
            RegisterMessage("SelectOneRate", "You should select at least one BasePort Rate.");
            RegisterMessage("GeneralInfoChanged", "General Info is changed, you should save it first.");
            RegisterMessage("ImportFailed", "Importing Base Port Rates is failed.\r\n");
            RegisterMessage("ImportingSuccessfully", "Importing Base Port Rates is successful with {0} records.");
            RegisterMessage("ValidateItemExist", "Some items are existing in the Base Port Rates {0};");
            RegisterMessage("ValidateItemCodeDifferent", "The Itemcode –{0}-- are conflicted because it has two or more different Commodities.");
            RegisterMessage("CurrentChanging", "The current record is changed and has not yet been saved. \r\nClicks Yes to save the changes and go to the next record. \r\nClicks No to desert all of changing. \r\nClicks Cancel to return.");
          
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
            RegisterMessage("ValidateItemCode", "ItemCode must input.");

            RegisterMessage("ChangeSameName", "Would you change all data of the same name?");

        }

        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
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
        }

        /// <summary>
        /// 初始化下拉框的值
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
                ClientInquierTruckingRate currentrow = CurrentRow;
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
                ClientInquierTruckingRate currentrow = CurrentRow;
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
                ClientInquierTruckingRate currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.ShippingLineName = InquireRatesUIDataHelper.ShippingLines.Find(t => t.ID == currentrow.ShippingLineID).Code;
                }
            };

            #endregion
        }

        /// <summary>
        /// 事件注册
        /// </summary>
        /// <param name="isDisposed">是否注册添加事件</param>
        private void EventRegister(bool isDisposed)
        {
            if (!isDisposed)
            {
                //TreeList
                bsList.PositionChanged += bsList_PositionChanged;       //焦点行改变时间
                treeMain.NodeCellStyle += treeMain_NodeCellStyle;       //节点样式改变
                treeMain.CustomDrawNodeIndicator += treeMain_CustomDrawNodeIndicator;       //绘制行号
            }
            else
            {
                //TreeList
                bsList.PositionChanged -= bsList_PositionChanged;       //焦点行改变时间
                treeMain.NodeCellStyle -= treeMain_NodeCellStyle;       //节点样式改变
                treeMain.CustomDrawNodeIndicator -= treeMain_CustomDrawNodeIndicator;       //绘制行号
            }
        }
        #endregion

        #region Base & Interface

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        protected ClientInquierTruckingRate CurrentRow
        {
            get { return Current as ClientInquierTruckingRate; }
            set
            {
                ClientInquierTruckingRate currentRow = CurrentRow;
                currentRow = value;
            }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
                //if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        private void BindingData(object data)
        {
            _currentServiceSource = data as InquierTruckingRatesResult;

            if (_currentServiceSource == null || _currentServiceSource.InquierTruckingRateList == null || _currentServiceSource.InquierTruckingRateList.Count == 0)
            {
                //ratesResult.InquierTruckingRateList = new List<InquierTruckingRate>();
                bsList.DataSource = null;
                bsList.ResetBindings(false);
            }
            else
            {
                //备份ID.
                //用于重新加载后定位到之前行记录。
                Guid? bakID = null;
                if (bsList.DataSource != null && bsList.Current != null)
                    bakID = ((ClientInquierTruckingRate)bsList.Current).ID;

                bsList.DataSource = null;
                bsList.ResetBindings(false);

                List<ClientInquierTruckingRate> source = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierTruckingRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
                bsList.DataSource = source;
                bsList.ResetBindings(false);
                treeMain.BestFitColumns();
                treeMain.ExpandAll();

                //定位到之前行记录
                if (bakID != null)
                {
                    for (int i = 0; i < bsList.Count; i++)
                    {
                        var item = ((ClientInquierTruckingRate)bsList[i]);
                        if (item.ID == bakID)
                        {
                            bsList.Position = i;
                            break;
                        }
                    }
                }
            }
        }

        public override void Refresh(object items)
        {
            //List<TruckingList> list = this.DataSource as List<TruckingList>;
            //if (list == null) return;
            //List<TruckingList> newLists = items as List<TruckingList>;

            //foreach (var item in newLists)
            //{
            //    TruckingList tager = list.Find(delegate(TruckingList jItem) { return item.ID == jItem.ID; });
            //    if (tager == null) continue;

            //    Utility.CopyToValue(item, tager, typeof(TruckingList));
            //}
            //bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;



        #endregion

        #endregion

        #region Commond & Event

        #region Commond

        #endregion

        #region EventSubscription


        #endregion

        #region TreeList Event

        /// <summary>
        /// 焦点行改变事件:设置列的编辑状态、验证数据是否更改
        /// </summary>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {

            SetEditState();

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        /// <summary>
        /// 节点样式改变：区分价格回复
        /// </summary>
        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            ClientInquierTruckingRate listData = (ClientInquierTruckingRate)treeMain.GetDataRecordByNode(e.Node);
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
                e.Appearance.BackColor = Color.White;

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

        /// <summary>
        /// 设置编辑框是否启用编辑：当前登录用户为答复人时编辑可用
        /// </summary>
        void SetEditState()
        {
            ClientInquierTruckingRate data = (ClientInquierTruckingRate)bsList.Current;
            if (data == null) return;
            if (LocalData.UserInfo.LoginID == data.RespondByID)
            {
                foreach (TreeListColumn sub in treeMain.Columns)
                {
                    sub.OptionsColumn.ReadOnly = false;
                }
            }
            else
            {
                foreach (TreeListColumn sub in treeMain.Columns)
                {
                    sub.OptionsColumn.ReadOnly = true;
                }
            }
        }

        #endregion

        #endregion

        #region Method

        #endregion

        #region IInquierRateDataBind
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据对象</param>
        public void DataSourceBind(object data)
        {
            ClientInquierTruckingRate inquierTruckingRate = data as ClientInquierTruckingRate;

            if (inquierTruckingRate == null)
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
                    InquierTruckingRatesResult item = InquireRatesService.GetInquireTruckingRateHistoryList(inquierTruckingRate.ID, inquierTruckingRate.MainRecordID,
                        inquierTruckingRate.POLID, inquierTruckingRate.PODID, inquierTruckingRate.PlaceOfDeliveryID, inquierTruckingRate.CarrierID);

                    DataSource = item;

                    string message = string.Empty;
                    if (item == null || item.InquierTruckingRateList == null)
                    {
                        message = "Nothing found!";
                    }
                    else
                    {
                        message = string.Format("{0} records found", item.InquierTruckingRateList.Count);
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
