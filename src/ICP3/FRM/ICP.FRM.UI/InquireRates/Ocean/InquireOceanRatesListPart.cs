#region Comment

/*
 * 
 * FileName:    InquireOceanRatesListPart.cs
 * CreatedOn:   
 * CreatedBy:   LiXuBin
 * 
 * 
 * Description：
 *      ->询价列表信息展示
 *      ->执行新增主询价、复制主询价、删除询价、复制子询价、Email、询价历史替换、询价历史复制
 *      ->确认询价、取消确认询价
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System.Drawing;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 海运询价列表面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireOceanRatesListPart : BaseListPart
    {
        #region 服务注入

        /// <summary>
        /// Work Item
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// Root Work Item
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        /// <summary>
        /// 询价服务
        /// </summary>
        [ServiceDependency]
        public IInquireRatesService inquireRatesService { get; set; }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        /// <summary>
        /// 国家、省份、地点维护服务
        /// </summary>
        [ServiceDependency]
        public IGeographyService geographyService { get; set; }

        /// <summary>
        /// 询价UI数据帮助类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public InquireRatesUIDataHelper InquireRatesUIDataHelper { get; set; }

        /// <summary>
        /// 询价客户端服务
        /// </summary>
        [ServiceDependency]
        public IClientInquireRateService oceanRate { get; set; }

        #endregion

        #region 委托事件
        /// <summary>
        /// 发送邮件到询问人
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_SendEmailToInquireBy)]
        public event EventHandler<DataEventArgs<List<InquierOceanRate>>> SendEmailToInquireByEvent;

        /// <summary>
        /// 邮件通知已确认询价
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_MailBookingConfirm)]
        public event EventHandler<DataEventArgs<ClientInquierOceanRate>> MailBookingConfirmEvent;
        #endregion

        #region 本地变量
        /// <summary>
        /// 当前询价查询结果集
        /// </summary>
        InquierOceanRatesResult _currentServiceSource;

        /// <summary>
        /// 数据源--询价客户端模型集合
        /// </summary>
        List<ClientInquierOceanRate> _sourceClone;
        public InquireOceanRatesGeneralInfoPart GeneralInfoPart
        {
            get;
            set;
        }
        
        /// <summary>
        /// 当前绑定数据源
        /// </summary>
        List<ClientInquierOceanRate> CurrentDataSource
        {
            get { return bsList.DataSource as List<ClientInquierOceanRate>; }
        }
        /// <summary>
        /// 默认币种ID
        /// </summary>
        public Guid? DefaultCurrencyID
        {
            get
            {
                var configureInfo = InquireRatesUIDataHelper.ConfigureInfo;

                if (configureInfo != null)
                {
                    return configureInfo.DefaultCurrencyID;
                }

                return null;
            }
        }
        /// <summary>
        /// 当前选择询价集合
        /// </summary>
        List<ClientInquierOceanRate> SelectedOceanItem
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;
                return (from TreeListNode item in treeMain.Selection select treeMain.GetDataRecordByNode(item) as ClientInquierOceanRate).ToList();
            }
        }

        /// <summary>
        /// 是否更改过询价
        /// </summary>
        public bool IsChanged
        {
            get
            {
                
                var source = CurrentDataSource;
                if (source == null || source.Count == 0) return false;

                return source.Any(item => item.IsNew || item.IsDirty);
            }
        }

        #endregion

        #region init
        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireOceanRatesListPart()
        {
            InitializeComponent();
            Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (!DesignMode) { InitMessage(); }
        }
        
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        /// <summary>
        /// 刷新邮件面板
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_RefreshEmailPart)]
        public event EventHandler<DataEventArgs<object>> RefreshOceanEmailPartEvent;

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            InitComboboxSource();
            SearchRegister();
        }
        /// <summary>
        /// 初始化下拉框数据源
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
                var currentrow = CurrentRow;
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
                var currentrow = CurrentRow;
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
                var currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.ShippingLineName = InquireRatesUIDataHelper.ShippingLines.Find(t => t.ID == currentrow.ShippingLineID).Code;
                }
            };

            #endregion
        }
        /// <summary>
        /// 查询注册
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
        /// 当前行
        /// </summary>
        protected ClientInquierOceanRate CurrentRow
        {
            get { return Current as ClientInquierOceanRate; }
            set
            {
                var currentRow = CurrentRow;
                currentRow = value;
            }
        }

        /// <summary>
        /// 数据源
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
                var source = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierOceanRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
                _sourceClone = Utility.Clone(source);
                bsList.DataSource = source;
                bsList.ResetBindings(false);

                treeMain.BestFitColumns();
                treeMain.ExpandAll();

                //定位到之前行记录
                if (bakID != null)
                {
                    for (var i = 0; i < bsList.Count; i++)
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
        /// 刷新
        /// </summary>
        /// <param name="items"></param>
        public override void Refresh(object items)
        {
            //List<OceanList> list = this.DataSource as List<OceanList>;
            //if (list == null) return;
            //List<OceanList> newLists = items as List<OceanList>;

            //foreach (var item in newLists)
            //{
            //    OceanList tager = list.Find(delegate(OceanList jItem) { return item.ID == jItem.ID; });
            //    if (tager == null) continue;

            //    Utility.CopyToValue(item, tager, typeof(OceanList));
            //}
            //bsList.ResetBindings(false);
        }

        /// <summary>
        /// 移除项(根据行数(Index)移除)
        /// </summary>
        /// <param name="index"></param>
        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        /// <summary>
        /// 移除项(根据选择项(Item)移除)
        /// </summary>
        /// <param name="item"></param>
        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        /// <summary>
        /// 当前选择行改变
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;

        /// <summary>
        /// 当前选择行改变之前
        /// </summary>
        public override event CancelEventHandler CurrentChanging;

        #region BulidColumns
        /// <summary>
        /// 列可见集合
        /// </summary>
        private List<string> _visibleColumnsNameList;
        /// <summary>
        /// 网格列可见控制
        /// </summary>
        /// <param name="units">箱型(集装箱尺柜)</param>
        private void BulidGridViewColumnsByOceanUnits(IEnumerable<InquireUnit> units)
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

            var visibleIndex = 6;

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

        #region Comm

        #region  rbtnEditComm

        /// <summary>
        /// 进入网格editer时记录text,如果在离开网格时有改变,就执行搜索
        /// </summary>
        private string _enterText = string.Empty;

        /// <summary>
        /// rbtnEditComm回车事件记录_enterText值
        /// </summary>
        private void rbtnEditComm_Enter(object sender, EventArgs e)
        {
            _enterText = ((TextEdit)(sender)).Text;
        }

        /// <summary>
        /// 焦点行排序索引
        /// </summary>
        private int _BeforeLeaveRowHandle = -1;

        /// <summary>
        /// rbtnEditComm焦点离开事件
        /// </summary>
        private void rbtnEditComm_Leave(object sender, EventArgs e)
        {
            _BeforeLeaveRowHandle = treeMain.FocusedColumn.SortIndex;
            var leaveText = ((TextEdit)(sender)).Text;
            if (leaveText != _enterText)
            {
                var scf = Workitem.Items.AddNew<IRSearchCommPart>();
                scf.SetSource(InquireRatesUIDataHelper.Commoditys, leaveText);
                var dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"),
                    FormBorderStyle.Sizable);
                if (dr == DialogResult.OK && _BeforeLeaveRowHandle >= 0)
                {
                    var row =
                        treeMain.GetDataRecordByNode(treeMain.FindNodeByID(_BeforeLeaveRowHandle)) as
                            ClientInquierOceanRate;
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

        /// <summary>
        /// rbtnEditComm按键事件
        /// </summary>
        private void rbtnEditComm_KeyDown(object sender, KeyEventArgs e)
        {
            ((TextEdit)(sender)).Leave -= new EventHandler(rbtnEditComm_Leave);
            if (e.KeyCode == Keys.Enter)
            {
                var leaveText = ((TextEdit)(sender)).Text;
                if (leaveText != _enterText)
                {
                    SearchComm();
                }
                _enterText = ((TextEdit)(sender)).Text;
            }

            ((TextEdit)(sender)).Leave += new EventHandler(rbtnEditComm_Leave);
        }

        /// <summary>
        /// rbtnEditComm按钮点击事件
        /// </summary>
        private void rbtnEditComm_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            SearchComm();
        }

        #endregion

        private void SearchComm()
        {
            if (CurrentRow == null) return;

            bsList.EndEdit();
            bsList.ResetCurrentItem();
            treeMain.CloseEditor();
            treeMain.RefreshDataSource();
            Validate();

            var scf = Workitem.Items.AddNew<IRSearchCommPart>();
            scf.SetSource(InquireRatesUIDataHelper.Commoditys, CurrentRow.Commodity);
            var dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
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

        #region Commond

        bool _hadLoadCurrentPage;

        /// <summary>
        /// Tab Page Changed(Tab选项卡切换)
        /// </summary>
        [CommandHandler(InquireRatesCommandConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            if (Visible == false || _hadLoadCurrentPage) return;

            if (_para != null)
            {
                var data = inquireRatesService.GetInquireOceanRateList(_para.No, _para.pol,
                    _para.delivery,
                    _para.pod,
                    _para.commodity,
                    _para.inquireOrRespondBy,
                    _para.isUnReply,
                    _para.durationFrom,
                    _para.durationTo, _para.StrQuery,
                    LocalData.UserInfo.LoginID);

                DataSource = data;
                _hadLoadCurrentPage = true;
            }
        }

        /// <summary>
        /// 确认业务询价
        /// </summary>
        [CommandHandler(InquireRatesCommandConstants.Command_ConfirmInquirePriceToShipment)]
        public void Command_ConfirmInquirePriceToShipment(object sender, EventArgs e)
        {
            var dr = RootWorkItem.State["BaseCurrentRow"] as DataRow;
            if (dr == null || CurrentRow == null)
            {
                XtraMessageBox.Show("No record is selected.", "Tip");
                return;
            }
            else if (Convert.ToBoolean(dr["SOCCD"]))
            {
                XtraMessageBox.Show("The shipment had been confirmed with this Inquire Price.", "Tip");
                return;
            }

            #region 询问

            var result = XtraMessageBox.Show(
                                             "Are you sure to confirm the selected shipments can be used for the Inquire Price.",
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            #endregion

            var oceanBookingID = new Guid(dr["OceanBookingID"].ToString());
            var resultData = inquireRatesService.ConfirmInquirePriceToShipment(oceanBookingID, LocalData.UserInfo.LoginID, true);
            if (resultData != null)
            {
                if (MailBookingConfirmEvent != null)
                {
                    ClientInquierOceanRate inquierObj = null;
                    foreach (var item in SelectedOceanItem.Where(item => item.MainRecordID == null))
                    {
                        inquierObj = item;
                    }
                    MailBookingConfirmEvent(this, new DataEventArgs<ClientInquierOceanRate>(inquierObj));
                }
                //刷新列表
                Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
            }
        }

        /// <summary>
        /// 取消确认业务询价
        /// </summary>
        [CommandHandler("Command_Un_ConfirmInquirePriceToShipment")]
        public void Command_Un_ConfirmInquirePriceToShipment(object sender, EventArgs e)
        {
            var dr = RootWorkItem.State["BaseCurrentRow"] as DataRow;
            if (dr == null || CurrentRow == null)
            {
                XtraMessageBox.Show("No record is selected.", "Tip");
                return;
            }
            else if (Convert.ToBoolean(dr["SOCCD"]) == false)
            {
                XtraMessageBox.Show("The shipment has not confirm with this Inquire Price.", "Tip");
                return;
            }

            #region 询问

            var result = XtraMessageBox.Show(
                LocalData.IsEnglish ? "Are you sure to un-confirm the selected shipments can be used for the Inquire Price." : "取消确认该询价可用于选择的业务吗？",
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            #endregion

            var oceanBookingID = new Guid(dr["OceanBookingID"].ToString());
            var resultData = inquireRatesService.ConfirmInquirePriceToShipment(oceanBookingID, LocalData.UserInfo.LoginID, false);
            if (resultData != null)
            {
                //刷新列表
                Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
            }
        }

        #endregion

        #region EventSubscription
        /// <summary>
        /// 询价历史替换
        ///     将历史记录与新询价合并,删除新询价,保存历史询价
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_HistoryReplace)]
        public void Command_HistoryReplace(object sender, DataEventArgs<object> e)
        {
            try
            {
                ClientInquierOceanRate historyData = e.Data as ClientInquierOceanRate;
                if (historyData == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (CurrentRow == null || historyData.ID == null || historyData.ID == Guid.Empty)
                    {
                        XtraMessageBox.Show("No record is selected.", "Tip");
                        return;
                    }
                    if (CurrentRow.RateUnitList.Any(item => item.Rate > 0))
                    {
                        XtraMessageBox.Show("The new Inquire Price could not be merged because it has rates already.", "Tip");
                        return;
                    }

                    #region 询问

                    var inquireBys = string.Empty;
                    CurrentRow.InquirePriceInquireBysList = inquireRatesService.GetInquirePriceInquireBys(CurrentRow.ID, CurrentRow.MainRecordID);
                    foreach (var item in CurrentRow.InquirePriceInquireBysList)
                    {
                        if (string.IsNullOrEmpty(inquireBys))
                            inquireBys += item.InquireByEName;
                        else
                            inquireBys += ',' + item.InquireByEName;
                    }

                    var result = XtraMessageBox.Show(
                                                     "Are you sure to use the selected rates instead of the new Inquire Price? \r\nNote: "
                                                     + inquireBys + " will be noticed with the Rates.",
                                                      "Tip",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                    if (result == DialogResult.No) return;

                    #endregion

                    Guid? oldID = Guid.Empty;
                    Guid? newID = Guid.Empty;
                    //只取主线ID（ParentID）
                    oldID = CurrentRow.MainRecordID ?? CurrentRow.ID;
                    newID = historyData.MainRecordID ?? historyData.ID;

                    var resultData = inquireRatesService.ReplaceInquirePrice(oldID.Value, newID.Value);
                    if (resultData != null)
                    {
                        //刷新列表
                        Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
                        //选中替换行
                        foreach (TreeListNode node in treeMain.Nodes)
                        {
                            if (node.GetValue(colID).Equals(resultData.ID))
                            {
                                treeMain.Selection.Clear();
                                //node.Selected = true;
                                treeMain.SetFocusedNode(node);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 询价历史复制
        ///     将历史记录的询价箱型、备注等复制到新询价
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_HistoryCopy)]
        public void Command_HistoryCopy(object sender, DataEventArgs<object> e)
        {
            try
            {
                ClientInquierOceanRate historyData = e.Data as ClientInquierOceanRate;
                if (historyData == null) return;
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (CurrentRow == null || historyData.ID == null || historyData.ID == Guid.Empty)
                    {
                        XtraMessageBox.Show("No record is selected.", "Tip");
                        return;
                    }
                    if (CurrentRow.RateUnitList.Any(item => item.Rate > 0))
                    {
                        XtraMessageBox.Show("The new Inquire Price could not be merged because it has rates already.", "Tip");
                        return;
                    }

                    #region 询问

                    var inquireBys = string.Empty;
                    CurrentRow.InquirePriceInquireBysList = inquireRatesService.GetInquirePriceInquireBys(CurrentRow.ID, CurrentRow.MainRecordID);
                    foreach (var item in CurrentRow.InquirePriceInquireBysList)
                    {
                        if (string.IsNullOrEmpty(inquireBys))
                            inquireBys += item.InquireByEName;
                        else
                            inquireBys += ',' + item.InquireByEName;
                    }

                    var result = XtraMessageBox.Show(
                                                     "Are you sure to copy the selected rates and remark into the new Inquire Price? \r\nNote: " + inquireBys + " will be noticed with the Rates.",
                                                      LocalData.IsEnglish ? "Tip" : "Tip",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                    if (result == DialogResult.No) return;

                    #endregion

                    CurrentRow.Rate_20FR = historyData.Rate_20FR;
                    CurrentRow.Rate_20GP = historyData.Rate_20GP;
                    CurrentRow.Rate_20HQ = historyData.Rate_20HQ;
                    CurrentRow.Rate_20HT = historyData.Rate_20HT;
                    CurrentRow.Rate_20NOR = historyData.Rate_20NOR;
                    CurrentRow.Rate_20OT = historyData.Rate_20OT;
                    CurrentRow.Rate_20RF = historyData.Rate_20RF;
                    CurrentRow.Rate_20RH = historyData.Rate_20RH;
                    CurrentRow.Rate_20TK = historyData.Rate_20TK;
                    CurrentRow.Rate_40FR = historyData.Rate_40FR;
                    CurrentRow.Rate_40GP = historyData.Rate_40GP;
                    CurrentRow.Rate_40HQ = historyData.Rate_40HQ;
                    CurrentRow.Rate_40HT = historyData.Rate_40HT;
                    CurrentRow.Rate_40NOR = historyData.Rate_40NOR;
                    CurrentRow.Rate_40OT = historyData.Rate_40OT;
                    CurrentRow.Rate_40RF = historyData.Rate_40RF;
                    CurrentRow.Rate_40RH = historyData.Rate_40RH;
                    CurrentRow.Rate_40TK = historyData.Rate_40TK;
                    CurrentRow.Rate_45FR = historyData.Rate_45FR;
                    CurrentRow.Rate_45GP = historyData.Rate_45GP;
                    CurrentRow.Rate_45HQ = historyData.Rate_45HQ;
                    CurrentRow.Rate_45HT = historyData.Rate_45HT;
                    CurrentRow.Rate_45OT = historyData.Rate_45OT;
                    CurrentRow.Rate_45RF = historyData.Rate_45RF;
                    CurrentRow.Rate_45RH = historyData.Rate_45RH;
                    CurrentRow.Rate_45TK = historyData.Rate_45TK;
                    CurrentRow.Remark = historyData.Remark;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// Email(发送邮件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(InquireRatesCommandConstants.Command_Mail)]
        public void Command_Mail(object sender, DataEventArgs<object> e)
        {
            if (Visible == false) return;
            if (SelectedOceanItem == null) return;

            var mailContent = string.Empty;
            foreach (var item in SelectedOceanItem)
            {
                if (!string.IsNullOrEmpty(item.CustomerName))
                    mailContent += "SHIPPER NAME: " + item.CustomerName + Environment.NewLine;
                if (!string.IsNullOrEmpty(item.Commodity))
                    mailContent += "COMM: " + item.Commodity + Environment.NewLine;
                else if (!string.IsNullOrEmpty(item.ExpCommodity))
                    mailContent += "COMM: " + item.ExpCommodity + Environment.NewLine;
                mailContent += "POL: " + item.POLName + Environment.NewLine;
                mailContent += "POD: " + (string.IsNullOrEmpty(item.PODName) ? "不限" : item.PODName) + Environment.NewLine;
                mailContent += "DES: " + item.PlaceOfDeliveryName + Environment.NewLine;
                if (!string.IsNullOrEmpty(item.TransportClauseName))
                    mailContent += "MODE: " + item.TransportClauseName + Environment.NewLine;
                mailContent += "VOLUME: " + Environment.NewLine;
                mailContent += "OTHER: " + Environment.NewLine;
                mailContent += Environment.NewLine;
            }

            Clipboard.SetDataObject(mailContent);
        }

        /// <summary>
        /// Copy Sublevel Inquire Rates(复制子询价)
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_Copy)]
        public void Command_Copy(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;
                if (LocalData.UserInfo.LoginID != CurrentRow.RespondByID) return;
                //移除节点得到焦点前事件
                treeMain.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
                //复制前面选择行数据
                var copyData = Utility.Clone<ClientInquierOceanRate>(CurrentRow);
                copyData.ID = Guid.NewGuid();
                copyData.IsNewRecord = true;
                foreach (var item in copyData.RateUnitList)
                {
                    item.ID = Guid.Empty;
                }

                if (CurrentRow.MainRecordID != null)
                {
                    copyData.MainRecordID = CurrentRow.MainRecordID;
                }
                else
                {
                    copyData.MainRecordID = CurrentRow.ID;
                }

                copyData.No = string.Empty;
                copyData.HasUnRead = false;
                copyData.IsNoPriceAll = true;
                copyData.Shared = true;
                copyData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                copyData.UpdateDate = null;
                copyData.IsDirty = true;
                //移除数据源行改变事件
                bsList.PositionChanged -= new EventHandler(bsList_PositionChanged);
                //新增行复制的行
                bsList.Insert(0, copyData);
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
                //重新添加数据源行改变事件
                bsList.PositionChanged += new EventHandler(bsList_PositionChanged);
                //定位Copy行
                var node = treeMain.FindNodeByFieldValue("ID", copyData.ID);
                treeMain.Selection.Clear();
                node.Selected = true;
                treeMain.SetFocusedNode(node);
                //重新绑定焦点行改变前事件
                treeMain.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeMain_BeforeFocusNode);
            }
        }

        /// <summary>
        /// Save Inquire Rates(保存询价：主询价)
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_Save)]
        public void Command_Save(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;

                SaveRateList();
            }
        }

        /// <summary>
        /// New Ocean Rate(新建海运询价单：打开新建询价单窗体)
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_NewOceanRate)]
        public void Command_NewOceanRate(object sender, DataEventArgs<object> e)
        {
            oceanRate.InquireOceanRate();
        }

        /// <summary>
        /// Copy Main Inquire Rate(复制主询价单)
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_ReInquire)]
        public void Command_ReInquire(object sender, DataEventArgs<object> e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Visible == false) return;
                if (Current == null) return;
                var newdata = InquireRatesHelper.TransformC2S(Utility.Clone<ClientInquierOceanRate>(CurrentRow));

                var inquireInfo = inquireRatesService.GetInquierOceanRateInfoForInquireBy(CurrentRow.ID, LocalData.UserInfo.LoginID);
                newdata.ExpTransportClauseID = inquireInfo.ExpTransportClauseID;
                newdata.ExpTransportClauseName = inquireInfo.ExpTransportClauseName;
                newdata.ExpCarrierName = inquireInfo.ExpCarrierName;
                newdata.CustomerID = inquireInfo.CustomerID;
                newdata.CustomerName = inquireInfo.CustomerName;
                newdata.ExpPrice = inquireInfo.ExpPrice;
                newdata.ID = Guid.Empty;
                newdata.InquireByID = LocalData.UserInfo.LoginID;
                newdata.InquireByName = LocalData.UserInfo.LoginName;
                foreach (var sub in newdata.UnitRates)
                {
                    sub.ID = Guid.Empty;
                    sub.Rate = 0;
                }

                var titleNo = LocalData.IsEnglish ? "New Inquire Ocean" : "New Inquire Ocean";
                PartLoader.ShowEditPart<NewInquireOceanRatePart>(Workitem, newdata, titleNo, AfterNewInquireOceanPartSaved);
            }
        }

        /// <summary>
        /// Delete Inquire Rate(删除询价)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(InquireRatesCommandConstants.Command_Delete)]
        public void Command_Delete(object sender, DataEventArgs<object> e)
        {
            if (Visible == false) return;
            treeMain.CloseEditor();
            var selecteds = SelectedOceanItem;
            if (selecteds == null || selecteds.Count == 0) return;

            #region 询问
            var result = XtraMessageBox.Show(
                                             NativeLanguageService.GetText(this, "RemoveSelectedItem"),
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion

            #region 构建需数据库中删除的数据

            var needRemoveIDs = new List<Guid>();
            var needRemoveUpdates = new List<DateTime?>();

            foreach (var item in selecteds)
            {
                if (item.IsNewRecord) continue;
                needRemoveIDs.Add(item.ID);
                needRemoveUpdates.Add(item.UpdateDate);
            }
            #endregion

            try
            {
                if (needRemoveIDs.Count > 0)
                {
                    inquireRatesService.RemoveInquireRate(needRemoveIDs.ToArray(), needRemoveUpdates.ToArray(), LocalData.UserInfo.LoginID);
                }

                var source = CurrentDataSource;

                foreach (var item in selecteds)
                {
                    source.Remove(item);
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                treeMain.BestFitColumns();
                treeMain.ExpandAll();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "RemoveSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }
        InquireRatesSearchParameter _para;

        /// <summary>
        /// This is the subscription for the CustomerAdded event
        /// We're using the default scope, which is Global
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_SearchData)]
        public void Command_SearchData(object sender, DataEventArgs<InquireRatesSearchParameter> e)
        {
            if (Visible == false) return;
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    _para = e.Data as InquireRatesSearchParameter;
                    if (_para != null)
                    {
                        //获取海运询价信息
                        var data = inquireRatesService.GetInquireOceanRateList(_para.No,_para.pol,
                            _para.delivery,
                            _para.pod,
                            _para.commodity,
                            _para.inquireOrRespondBy,
                            _para.isUnReply,
                            _para.durationFrom,
                            _para.durationTo,_para.StrQuery,
                            LocalData.UserInfo.LoginID);

                        DataSource = data;
                        _hadLoadCurrentPage = true;

                        var message = string.Empty;
                        if (data == null || data.InquierOceanRateList == null || data.InquierOceanRateList.Count<=0)
                        {
                            message = "Nothing found!";
                            bsList_PositionChanged(null, null);
                        }
                        else
                        {
                            message = string.Format("{0} records found", data.InquierOceanRateList.Count);
                        }

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        [EventSubscription(InquireRatesCommandConstants.Command_AddNewRecord)]
        public void Command_AddNewRecord(object sender, DataEventArgs<InquierOceanRate> e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    var newObj = InquireRatesHelper.TransformS2C(e.Data, e.Data.UnitRates, DefaultCurrencyID);
                    if (newObj != null)
                    {
                        ClientInquierOceanRate findObj = null;
                        foreach (var item in CurrentDataSource.Where(item => item.ID == newObj.ID))
                        {
                            findObj = item;
                        }
                        if (findObj != null)    //存在记录、替换
                        {
                            findObj = newObj;
                            bsList.ResetBindings(false);
                        }
                        else  //否则插入新记录到首行
                        {
                            //插入新纪录
                            bsList.Insert(0, newObj);
                            bsList.ResetBindings(false);
                        }
                        //选中新增行
                        foreach (var node in treeMain.Nodes.Cast<TreeListNode>().Where(node => node.GetValue(colID).Equals(newObj.ID)))
                        {
                            treeMain.Selection.Clear();
                            treeMain.SetFocusedNode(node);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region GridView Event
        /// <summary>
        /// 数据源行改变事件
        /// </summary>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            SetEditState();

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        /// <summary>
        /// 设置编辑状态：通过判断操作人是否有权限操作来设置控制只读状态
        /// </summary>
        void SetEditState()
        {
            var data = bsList.Current as ClientInquierOceanRate;
            if (data == null) return;
            if (LocalData.UserInfo.LoginID == data.RespondByID)
            {
                //列可见集合不为空时设置界面箱型
                if (_visibleColumnsNameList != null)
                {
                    foreach (var item in _visibleColumnsNameList)
                    {
                        var findItem = (from d in data.RateUnitList where d.UnitName == item select d).SingleOrDefault();

                        if (findItem == null)
                        {
                            treeMain.Columns.ColumnByName("colRate_" + item).OptionsColumn.AllowEdit = false;
                        }
                        else
                        {
                            treeMain.Columns.ColumnByName("colRate_" + item).OptionsColumn.AllowEdit = true;
                        }
                    }
                }

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

        /// <summary>
        /// 节点样式设置
        ///     根据回复价格状态设置背景颜色
        /// </summary>
        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            var listData = (ClientInquierOceanRate)treeMain.GetDataRecordByNode(e.Node);
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

            var args = e.ObjectArgs as IndicatorObjectInfoArgs;
            if (args != null)
            {
                var rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }

        /// <summary>
        /// 节点得到焦点之前:
        ///     判断是否为父子询价
        ///     注册当前行改变事件：当为主询价切换时改变子面板显示数据
        /// </summary>
        private void treeMain_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (!Visible)
                return;
            if (e.OldNode == null || e.Node == null) return;
            ClientInquierOceanRate dataOld = treeMain.GetDataRecordByNode(e.OldNode) as ClientInquierOceanRate;
            ClientInquierOceanRate dataNew = treeMain.GetDataRecordByNode(e.Node) as ClientInquierOceanRate;
            if (dataOld.ID == dataNew.MainRecordID ||
                (dataOld.MainRecordID != null && dataOld.MainRecordID == dataNew.MainRecordID) ||
                dataOld.MainRecordID == dataNew.ID)
            {
            }
            else //不是父子询价，切换行前数据有更新，提示保存
            {

                if (CurrentChanging != null)
                {
                    CancelEventArgs ce = new CancelEventArgs();
                    CurrentChanging(this, ce);
                    e.CanFocus = !ce.Cancel;
                }
            }
        }

        /// <summary>
        /// 当切换到某一行时，如果当前用户不等于该inquire rate的Respond  By，则该行只读，否则可编辑（栏位定义中为只读的，仍然为只读）。
        /// </summary>
        private void treeMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            //ClientInquierOceanRate data = treeMain.GetDataRecordByNode(((LWTreeGridControl)sender).FocusedNode) as ClientInquierOceanRate;
            //if (data != null && LocalData.UserInfo.LoginID != data.RespondByID)
            //{
            //    e.Cancel = true;   
            //}
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl is TreeList)
            {
                var tree = (TreeList)e.SelectedControl;
                var hit = tree.CalcHitInfo(e.ControlMousePosition);
                if (hit.HitInfoType == HitInfoType.Cell && hit.Column.FieldName == "HasUnconfirmedShipment")
                {
                    if (Convert.ToBoolean(hit.Node[hit.Column]))
                    {
                        object cellInfo = new TreeListCellToolTipInfo(hit.Node, hit.Column, null);
                        var toolTip = "Indicates that currently it has some un-confirmed shipments or un-confirmed new inquire price.";//string.Format("{0}(Column:{1},Node ID:{2})", hit.Node[hit.Column], hit.Column.VisibleIndex, hit.Node.Id);
                        e.Info = new ToolTipControlInfo(cellInfo, toolTip);
                    }
                }
            }
        }

        #endregion
        
        #region 方法定义

        /// <summary>
        /// 注册消息
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

        /// <summary>
        /// 重置当前行
        /// </summary>
        public void ResetCurrent()
        {
            var tager = _sourceClone.Find(delegate(ClientInquierOceanRate item) { return item.ID == CurrentRow.ID; });
            if (tager != null)
            {
                Utility.CopyToValue(tager, CurrentRow, typeof(ClientInquierOceanRate));
                bsList.ResetCurrentItem();
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="needSaveList">需保存数据集合</param>
        /// <returns>验证结果</returns>
        bool ValidateData(IEnumerable<ClientInquierOceanRate> needSaveList)
        {
            var value = true;
            var eError = @"[{0}] is required! You must fill-in it.";
            var cError = @"请您输入必填项：[{0}]。";

            foreach (var item in needSaveList)
            {
                if (Utility.GuidIsNullOrEmpty(item.CarrierID))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colCarrier.Caption)
                        : string.Format(cError, colCarrier.Caption), "Tip");

                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.POLID))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colPOL.Caption)
                        : string.Format(cError, colPOL.Caption), "Tip");
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.PlaceOfDeliveryID))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colPlaceOfDelivery.Caption)
                        : string.Format(cError, colPlaceOfDelivery.Caption), "Tip");
                    value = false;
                }

                if (String.IsNullOrEmpty(item.Commodity))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colCommodity.Caption)
                        : string.Format(cError, colCommodity.Caption), "Tip");
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.CurrencyID))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colCurrency.Caption)
                        : string.Format(cError, colCurrency.Caption), "Tip");
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.TransportClauseID))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colTransportClause.Caption)
                        : string.Format(cError, colTransportClause.Caption), "Tip");
                    value = false;
                }

                if (Utility.GuidIsNullOrEmpty(item.ShippingLineID))
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, colShipline.Caption)
                        : string.Format(cError, colShipline.Caption), "Tip");
                    value = false;
                }

                if (item.Rate_45GP == null
                    && item.Rate_40GP == null
                    && item.Rate_45RF == null
                    && item.Rate_20RH == null
                    && item.Rate_45OT == null
                    && item.Rate_40NOR == null
                    && item.Rate_40FR == null
                    && item.Rate_20OT == null
                    && item.Rate_45TK == null
                    && item.Rate_20NOR == null
                    && item.Rate_40HT == null
                    && item.Rate_40RH == null
                    && item.Rate_45RH == null
                    && item.Rate_45HQ == null
                    && item.Rate_20HT == null
                    && item.Rate_40HQ == null
                    && item.Rate_20FR == null
                    && item.Rate_40OT == null
                    && item.Rate_40TK == null
                    && item.Rate_20GP == null
                    && item.Rate_20TK == null
                    && item.Rate_20HQ == null
                    && item.Rate_20RF == null
                    && item.Rate_45HT == null
                    && item.Rate_40RF == null
                    && item.Rate_45FR == null)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ?
                        string.Format(eError, "Rate")
                        : string.Format(cError, "Rate"), "Tip");
                    value = false;
                }

            }

            return value;
        }

        /// <summary>
        /// 通用信息面板改变箱型(集装箱尺柜)事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="inquirePriceID">询价ID</param>
        /// <param name="list">箱型(集装箱尺柜)集合</param>
        public void GeneralInfoPart_ChangedUnitEvent(object sender, Guid inquirePriceID, List<InquireUnit> list)
        {
            var inquireList = (List<ClientInquierOceanRate>)bsList.DataSource;
            foreach (var inquire in inquireList)
            {
                if (inquire.ID == inquirePriceID || inquire.MainRecordID == inquirePriceID)
                {
                    ChangeUnit(inquire, list);
                }
            }
            //设置编辑状态
            SetEditState();

            foreach (var sub in list)
            {
                var found = _currentServiceSource.MaxUnits.Find(item => item.UnitID == sub.UnitID);
                if (found == null)
                    _currentServiceSource.MaxUnits.Add(sub);
            }
            BulidGridViewColumnsByOceanUnits(_currentServiceSource.MaxUnits);
        }

        /// <summary>
        /// 箱型改变(集装箱尺柜)
        /// </summary>
        /// <param name="current">当前项</param>
        /// <param name="list">箱型(集装箱尺柜)集合</param>
        void ChangeUnit(ClientInquierOceanRate current, List<InquireUnit> list)
        {
            foreach (var sub in list)
            {
                InquireUnit found;

                //删除单位
                for (var i = current.RateUnitList.Count - 1; i >= 0; i--)
                {
                    var subCurrent = current.RateUnitList[i];
                    found = list.Find(item => item.UnitID == subCurrent.UnitID);

                    if (found == null)
                    {
                        switch (subCurrent.UnitName)
                        {
                            case "45FR": current.Rate_45FR = 0; break;
                            case "40RF": current.Rate_40RF = 0; break;
                            case "45HT": current.Rate_45HT = 0; break;
                            case "20RF": current.Rate_20RF = 0; break;
                            case "20HQ": current.Rate_20HQ = 0; break;
                            case "20TK": current.Rate_20TK = 0; break;
                            case "20GP": current.Rate_20GP = 0; break;
                            case "40TK": current.Rate_40TK = 0; break;
                            case "40OT": current.Rate_40OT = 0; break;
                            case "20FR": current.Rate_20FR = 0; break;
                            case "45GP": current.Rate_45GP = 0; break;
                            case "40GP": current.Rate_40GP = 0; break;
                            case "45RF": current.Rate_45RF = 0; break;
                            case "20RH": current.Rate_20RH = 0; break;
                            case "45OT": current.Rate_45OT = 0; break;
                            case "40NOR": current.Rate_40NOR = 0; break;
                            case "40FR": current.Rate_40FR = 0; break;
                            case "20OT": current.Rate_20OT = 0; break;
                            case "45TK": current.Rate_45TK = 0; break;
                            case "20NOR": current.Rate_20NOR = 0; break;
                            case "40HT": current.Rate_40HT = 0; break;
                            case "40RH": current.Rate_40RH = 0; break;
                            case "45RH": current.Rate_45RH = 0; break;
                            case "45HQ": current.Rate_45HQ = 0; break;
                            case "20HT": current.Rate_20HT = 0; break;
                            case "40HQ": current.Rate_40HQ = 0; break;
                        }

                        current.RateUnitList.Remove(subCurrent);
                    }
                }

                //新增单位
                found = current.RateUnitList.Find(item => item.UnitID == sub.UnitID);
                if (found == null)
                {
                    current.RateUnitList.Add(new InquireUnit()
                    {
                        Rate = 0,
                        UnitID = sub.UnitID,
                        UnitName = sub.UnitName
                    });
                }
                current.IsDirty = true;
            }

        }

        /// <summary>
        /// 保存询价(网格保存)
        /// </summary>
        /// <returns>保存结果</returns>
        public bool SaveRateList()
        {
            #region 保存General Info面板
            /*
             * 保存General Info面板
             */
            if (!GeneralInfoPart.IsReadOnly && GeneralInfoPart.IsChanged)
            {
                if (!GeneralInfoPart.ValidateData())
                {
                    return false;
                }

                GeneralInfoPart.SaveData();
            } 
            #endregion

            /*
             * 保存Inquire List
             */
            //如果当前用户不等于询价回复者，则不能修改保存。
            if (LocalData.UserInfo.LoginID != CurrentRow.RespondByID) return false;

            treeMain.CloseEditor();
            //获得需要保存的行。
            var needSaveList = new List<ClientInquierOceanRate>();
            var datas = bsList.DataSource as List<ClientInquierOceanRate>;
            if (datas == null)
                return false;
            #region 构建保存集合对象
            if (CurrentRow.IsDirty)
            {
                needSaveList.Add(CurrentRow);
            }
            needSaveList.AddRange(datas.Where(
                item => item.IsDirty && 
                    (item.MainRecordID == CurrentRow.ID 
                        || item.ID == CurrentRow.MainRecordID 
                        || (item.ID != CurrentRow.ID && CurrentRow.MainRecordID != null 
                            && CurrentRow.MainRecordID == item.MainRecordID))));
            if (needSaveList.Count == 0)
            {
                return true;
            }
            var isContainMain = needSaveList.Any(item => item.MainRecordID == null);
            if (!isContainMain)
            {
                foreach (var item in datas.Where(item => item.ID == CurrentRow.MainRecordID))
                {
                    needSaveList.Add(item);
                    break;
                }
            }
            needSaveList = needSaveList.OrderBy(i => i.MainRecordID).ToList(); 
            #endregion

            //验证数据，如必填项。。。
            if (!ValidateData(needSaveList))
            {
                return false;
            }
            try
            {
                //有回复价格
                var isRespondPrice=false;
                //从ClientInquierOceanRate转换为InquierOceanRate
                var transList = InquireRatesHelper.TransformC2S(needSaveList,true);
                //保存询价信息
                var result = inquireRatesService.SaveOceanInquireRateWithTrans(
                    transList, DateTime.Now, LocalData.UserInfo.DefaultCompanyID,LocalData.UserInfo.LoginID);
                //更新界面值
                for (var i = 0; i < result.ChildResults.Count; i++)
                {
                    //任何一项有回复价格,则认为有询价价格有回复
                    if (!needSaveList[i].IsNoPriceAll)
                        isRespondPrice = true;
                    needSaveList[i].ID = result.ChildResults[i].ID;
                    needSaveList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needSaveList[i].IsDirty = false;
                    needSaveList[i].IsNewRecord = false;
                }
                bsList.ResetBindings(false);
                if (isRespondPrice)
                {

                    #region 有询问价格响应操作时邮件反馈结果
                    //发送询价结果
                    if (SendEmailToInquireByEvent != null)
                    {
                        //SendEmailToInquireByEvent(this, new DataEventArgs<List<InquierOceanRate>>(transList));
                    }
                    #endregion
                }

                _sourceClone = bsList.DataSource as List<ClientInquierOceanRate>;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "Save Successfully");
                //刷新列表
                Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }

        /// <summary>
        /// 新增海运询价单保存之后调用方法
        /// </summary>
        /// <param name="prams">参数集合</param>
        private void AfterNewInquireOceanPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            var newData = prams[0] as InquierOceanRate;

            #region  需要重新统计最大箱型，多的加，少的不变
            if (_currentServiceSource == null)
            {
                _currentServiceSource = new InquierOceanRatesResult();
                _currentServiceSource.MaxUnits = newData.UnitRates;
            }
            else
            {
                foreach (var sub in newData.UnitRates)
                {
                    var found = _currentServiceSource.MaxUnits.Find(item => item.UnitID == sub.UnitID);
                    if (found == null)
                        _currentServiceSource.MaxUnits.Add(sub);
                }
            }
            BulidGridViewColumnsByOceanUnits(_currentServiceSource.MaxUnits);

            #region Comment
            //List<InquireUnit> maxUnits = null;
            //if (_currentServiceSource == null)
            //{
            //    _currentServiceSource = new InquierOceanRatesResult();
            //    //InitControls();
            //}

            //if (_currentServiceSource.MaxUnits == null || _currentServiceSource.MaxUnits.Count == 0)
            //{
            //    maxUnits = newData.UnitRates;
            //}
            //else
            //{
            //    maxUnits = Utility.Clone<List<InquireUnit>>(_currentServiceSource.MaxUnits);

            //    foreach (InquireUnit unit in newData.UnitRates)
            //    {
            //        var findItem = (from d in _currentServiceSource.MaxUnits where d.UnitID == unit.UnitID select d).SingleOrDefault();
            //        if (findItem == null)
            //        {
            //            maxUnits.Add(unit);
            //        }
            //    }                
            //}

            //_currentServiceSource.MaxUnits = maxUnits; 
            #endregion

            #endregion

            //这里需要控制tab面板的隐藏，还没实现
            //判断海运单集合对象是否为空
            if (_currentServiceSource.InquierOceanRateList == null || _currentServiceSource.InquierOceanRateList.Count == 0)
            {
                //为空则实例化新对象，并将
                _currentServiceSource.InquierOceanRateList = new List<InquierOceanRate>();
                _currentServiceSource.InquierOceanRateList.Insert(0, newData);
            }
            else
            {
                if (_currentServiceSource.InquierOceanRateList.Contains(newData))
                {
                    _currentServiceSource.InquierOceanRateList.ForEach(
                        obj => { if (obj.ID.Equals(newData.ID)) { obj = newData; } });
                }
            }

            BulidGridViewColumnsByOceanUnits(_currentServiceSource.MaxUnits);
            var dataSource = InquireRatesHelper.TransformS2C(_currentServiceSource.InquierOceanRateList, _currentServiceSource.MaxUnits, DefaultCurrencyID);
            _sourceClone = Utility.Clone<List<ClientInquierOceanRate>>(dataSource);
            bsList.DataSource = dataSource;
            bsList.ResetBindings(false);
            treeMain.BestFitColumns();
            treeMain.ExpandAll();
            //if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            //刷新列表
            Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute();
            //选中新增行
            foreach (TreeListNode node in treeMain.Nodes)
            {
                if (node.GetValue(colID).Equals(newData.ID))
                {
                    treeMain.Selection.Clear();
                    treeMain.SetFocusedNode(node);
                    break;
                }
            }
        }

        #endregion

        #region Comment Code

        #region Comment Code -- pol

        // dfService.RegisterGridColumnFinder(colPOL, CommonFinderConstants.OceanLocationFinder
        //  , "POLID", "POLName"
        //  , "ID", LocalData.IsEnglish ? "EName" : "EName"
        //  , delegate(object befocePickedData, object afterPickedData)
        //  {
        //      ClientInquierOceanRate befoceChangedRow = befocePickedData as ClientInquierOceanRate;
        //      ClientInquierOceanRate afterChangedRow = afterPickedData as ClientInquierOceanRate;

        //      if (befoceChangedRow != null && afterChangedRow != null)
        //      {
        //          List<ClientInquierOceanRate> source = CurrentSource;
        //          List<ClientInquierOceanRate> sameData = source.FindAll(s => s.POLID == befoceChangedRow.POLID);
        //          if (sameData != null && sameData.Count > 0)
        //          {
        //              DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
        //                                      LocalData.IsEnglish ? "Tip" : "提示",
        //                                      MessageBoxButtons.YesNo,
        //                                      MessageBoxIcon.Question);
        //              if (result == DialogResult.Yes)
        //              {
        //                  foreach (var item in sameData)
        //                  {
        //                      item.POLID = afterChangedRow.POLID;
        //                      item.POLName = afterChangedRow.POLName;
        //                      item.IsDirty = true;
        //                  }

        //                  treeMain.RefreshDataSource();
        //              }
        //          }
        //      }
        //  });

        // #endregion

        // #region pod

        // dfService.RegisterGridColumnFinder(colPOD
        //, CommonFinderConstants.OceanLocationFinder
        //, "PODID"
        //, "PODName"
        //, "ID"
        //, LocalData.IsEnglish ? "EName" : "EName"
        //, delegate(object befocePickedData, object afterPickedData)
        //{
        //    ClientInquierOceanRate befoceChangedRow = befocePickedData as ClientInquierOceanRate;
        //    ClientInquierOceanRate afterChangedRow = afterPickedData as ClientInquierOceanRate;

        //    if (befoceChangedRow != null && afterChangedRow != null)
        //    {
        //        List<ClientInquierOceanRate> source = CurrentSource;
        //        List<ClientInquierOceanRate> sameData = source.FindAll(s => s.PODID == befoceChangedRow.PODID);
        //        if (sameData != null && sameData.Count > 0)
        //        {
        //            DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
        //                                    LocalData.IsEnglish ? "Tip" : "提示",
        //                                    MessageBoxButtons.YesNo,
        //                                    MessageBoxIcon.Question);
        //            if (result == DialogResult.Yes)
        //            {
        //                foreach (var item in sameData)
        //                {
        //                    item.PODID = afterChangedRow.PODID;
        //                    item.PODName = afterChangedRow.PODName;
        //                    item.IsDirty = true;
        //                }

        //                treeMain.RefreshDataSource();
        //            }
        //        }
        //    }
        //});
        // #endregion

        // #region PlaceOfDelivery
        // dfService.RegisterGridColumnFinder(colPlaceOfDelivery
        //, CommonFinderConstants.OceanLocationFinder
        //, "PlaceOfDeliveryID"
        //, "PlaceOfDeliveryName"
        //, "ID"
        //, LocalData.IsEnglish ? "EName" : "EName"
        //, delegate(object befocePickedData, object afterPickedData)
        //{
        //    ClientInquierOceanRate befoceChangedRow = befocePickedData as ClientInquierOceanRate;
        //    ClientInquierOceanRate afterChangedRow = afterPickedData as ClientInquierOceanRate;

        //    if (befoceChangedRow != null && afterChangedRow != null)
        //    {
        //        List<ClientInquierOceanRate> source = CurrentSource;
        //        List<ClientInquierOceanRate> sameData = source.FindAll(s => s.PlaceOfDeliveryID == befoceChangedRow.PlaceOfDeliveryID);
        //        if (sameData != null && sameData.Count > 0)
        //        {
        //            DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
        //                                    LocalData.IsEnglish ? "Tip" : "提示",
        //                                    MessageBoxButtons.YesNo,
        //                                    MessageBoxIcon.Question);
        //            if (result == DialogResult.Yes)
        //            {
        //                foreach (var item in sameData)
        //                {
        //                    item.PlaceOfDeliveryID = afterChangedRow.PlaceOfDeliveryID;
        //                    item.PlaceOfDeliveryName = afterChangedRow.PlaceOfDeliveryName;
        //                    item.IsDirty = true;
        //                }

        //                treeMain.RefreshDataSource();
        //            }
        //        }
        //    }
        //});
        #endregion

        //ClientInquierOceanRate befoceChangedRow = befocePickedData as ClientInquierOceanRate;
        //ClientInquierOceanRate afterChangedRow = afterPickedData as ClientInquierOceanRate;

        //if (befoceChangedRow != null && afterChangedRow != null)
        //{
        //    List<ClientInquierOceanRate> source = CurrentSource;
        //    List<ClientInquierOceanRate> sameData = source.FindAll(s => s.CarrierID == befoceChangedRow.CarrierID);
        //    if (sameData != null && sameData.Count > 0)
        //    {
        //        DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
        //                                LocalData.IsEnglish ? "Tip" : "提示",
        //                                MessageBoxButtons.YesNo,
        //                                MessageBoxIcon.Question);
        //        if (result == DialogResult.Yes)
        //        {
        //            foreach (var item in sameData)
        //            {
        //                item.CarrierID = afterChangedRow.CarrierID;
        //                item.CarrierName = afterChangedRow.CarrierName;
        //                item.IsDirty = true;
        //            }

        //            treeMain.RefreshDataSource();
        //        }
        //    }
        //}
        ///// <summary>
        ///// 刷新UI数据
        ///// </summary>
        //internal void RefreshUIData()
        //{
        //    List<ClientInquierOceanRate> source = CurrentDataSource;
        //    //source = source.OrderByDescending(b => b.No).ToList();
        //    bsList.DataSource = source;
        //    treeMain.RefreshDataSource();
        //}
        ///// <summary>
        ///// 获取改变项
        ///// </summary>
        ///// <returns></returns>
        //internal List<ClientInquierOceanRate> GetChangedItem()
        //{
        //    List<ClientInquierOceanRate> source = CurrentDataSource;
        //    List<ClientInquierOceanRate> chengedItem = new List<ClientInquierOceanRate>();
        //    foreach (var item in source)
        //    {
        //        if (item.IsNew || item.IsDirty) chengedItem.Add(item);
        //    }
        //    return chengedItem;
        //}

        ///// <summary>
        ///// 验证数据
        ///// </summary>
        ///// <returns>验证结果</returns>
        //public bool ValidateData()
        //{
        //    if (IsChanged == false) return true;

        //    Validate();
        //    bsList.EndEdit();

        //    List<ClientInquierOceanRate> chengedItem = GetChangedItem();

        //    //if (this.ValidateData(chengedItem) == false) return false;  要的啊

        //    return true;
        //}

        ///// <summary>
        ///// 获取EventObjects对象
        ///// </summary>
        ///// <returns></returns>
        //private EventObjects CreateEventObjects(DataRow dr)
        //{
        //    EventObjects eventobject = new EventObjects();
        //    eventobject.OperationID = (Guid)dr["OceanBookingID"];
        //    eventobject.FormType = ICP.Framework.CommonLibrary.Common.FormType.MBL;
        //    eventobject.FormID = Guid.Empty;
        //    eventobject.MessageID = Guid.Empty;

        //    eventobject.Code = "SOCCD";
        //    eventobject.EventID = Guid.Empty;


        //    eventobject.IsShowAgent = false;
        //    eventobject.IsShowCustomer = false;
        //    eventobject.ManualImportant = false;

        //    eventobject.UpdateBy = LocalData.UserInfo.LoginID;
        //    eventobject.Owner = LocalData.UserInfo.LoginName;
        //    eventobject.UpdateDate = DateTime.Now;
        //    eventobject.CreateDate = DateTime.Now;
        //    eventobject.OccurrenceTime = DateTime.Now;

        //    #region 处理当前添加的事件状态和样式的处理
        //    eventobject.Logged = true;
        //    eventobject.EventIndex = 0;
        //    eventobject.Type = MemoType.Manually;

        //    //系统自有事件
        //    eventobject.UIIndex = 2;

        //    eventobject.CategoryName = eventobject.UIIndex + eventobject.CategoryName;

        //    #endregion
        //    return eventobject;
        //}

        //private void treeMain_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) GvMainDoubleClick();
        //    else if (e.KeyCode == Keys.F5)
        //    {
        //        if (this.KeyDown != null && treeMain.FocusedColumn != null && treeMain.FocusedNode != null)
        //        {
        //            string text = treeMain.FocusedNode.GetDisplayText(treeMain.FocusedColumn);
        //            Dictionary<string, object> keyValue = new Dictionary<string, object>();
        //            keyValue.Add(treeMain.FocusedColumn.FieldName, text);
        //            this.KeyDown(keyValue, null);
        //        }
        //    }
        //    else if (e.KeyCode == Keys.F6)
        //    {
        //        Workitem.Commands[AEBLCommandConstants.Command_ShowSearch].Execute();
        //    }
        //}

        ////pearl
        //private bool ValidateData(List<ClientInquierOceanRate> chengedItems)
        //{
        //    gvMain.ActiveFilterString = string.Empty;

        //    if (chengedItems == null || chengedItems.Count == 0) return false;

        //    bool isSrcc = true;

        //    foreach (var item in chengedItems)
        //    {
        //        string errorMessage = string.Empty;
        //        item.ErrorInfo = string.Empty;

        //        if (item.Validate(ref errorMessage, delegate(ValidateEventArgs e)
        //        {
        //            if (item.ValidateHasRate() == false)
        //                e.SetErrorInfo("Rate_20GP", NativeLanguageService.GetText(this, "ValidateRate_20GP"));

        //            if (item.FromDate.HasValue && item.ToDate.HasValue && item.FromDate >= item.ToDate)
        //                e.SetErrorInfo("FromDate", NativeLanguageService.GetText(this, "ValidateFromDate"));

        //            if (item.POLID.IsNullOrEmpty() == false && item.PODID.IsNullOrEmpty() == false && item.PODID == item.POLID)
        //                e.SetErrorInfo("PODName", NativeLanguageService.GetText(this, "ValidatePOLSamePOD"));

        //            if (item.Comm.IsNullOrEmpty())
        //                e.SetErrorInfo("Comm", NativeLanguageService.GetText(this, "ValidateComm"));

        //        }) == false) isSrcc = false;

        //        item.ErrorInfo = errorMessage;
        //    }

        //    #region  //验证唯一键定义 Account+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term

        //    List<ClientBasePortList> oldSouce = CurrentSource.FindAll(s => s.IsNew == false && s.IsDirty == false);
        //    List<ClientBasePortList> newItems = CurrentSource.FindAll(s => s.IsNew || s.IsDirty);

        //    bool itemCodeCommDifferent = false;
        //    bool isExist = ValidateHasExistItem(oldSouce, newItems, ref itemCodeCommDifferent);

        //    if (isExist || itemCodeCommDifferent) isSrcc = false;

        //    if (isExist) LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ValidateDataExist"));

        //    if (itemCodeCommDifferent) LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ItemCodeDifferent"));

        //    #endregion

        //    if (isSrcc == false) gvMain.RefreshData();
        //    return isSrcc;
        //}
        //private bool ValidateData()
        //{
        //    //this.EndEdit();

        //    bool isScrr = true;
        //    if (_CurrentBLInfo.Validate
        //        (
        //            delegate(ValidateEventArgs e)
        //            {
        //                if (_CurrentBLInfo.DepartureID != Guid.Empty && _CurrentBLInfo.DepartureID == _CurrentBLInfo.DetinationID)
        //                    e.SetErrorInfo("DepartureID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");

        //                if (_CurrentBLInfo.FilightNoID == null || _CurrentBLInfo.FilightNoID == Guid.Empty)
        //                    e.SetErrorInfo("FlightNo", LocalData.IsEnglish ? "Flight No Must Input" : "航班号必须填写");

        //                if (string.IsNullOrEmpty(_CurrentBLInfo.MBLNo))
        //                    e.SetErrorInfo("MBLNo", LocalData.IsEnglish ? "Must Select Or Input MBLNO" : "必须选择或输入MBL号");


        //            }
        //        ) == false) isScrr = false;

        //    return isScrr;
        //}

        //private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    //Workitem.Commands[OPCommonConstants.Command_SaveData].Execute();
        //}


        //private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        //{
        //    if (CurrentChanging != null)
        //    {
        //        CancelEventArgs ce = new CancelEventArgs();
        //        CurrentChanging(this, ce);
        //        e.Allow = !ce.Cancel;
        //    }
        //} 
        #endregion
    }
}
