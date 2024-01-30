using System.Globalization;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 基本航线运价编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPBasePortRatesPart : BaseListEditPart
    {
        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 运价服务
        /// </summary>
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
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
        /// 运价UI辅助服务
        /// </summary>
        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }
        #endregion

        #region 本地变量
        /// <summary>
        /// 线程ID:进度面板
        /// </summary>
        static int theradID = 0;
        /// <summary>
        /// 是否最大尺寸
        /// </summary>
        bool isMax = false;
        /// <summary>
        /// 查找器对象
        /// </summary>
        BasePortFilterObject _BasePortFilterObject = null;
        /// <summary>
        /// 进入网格editer时记录text,如果在离开网格时有改变,就执行搜索
        /// </summary>
        string _enterText = string.Empty;
        /// <summary>
        /// 之前行句柄
        /// </summary>
        int _BeforeLeaveRowHandle = -1;
        /// <summary>
        /// 当前加载合约ID
        /// </summary>
        Guid _LoadedOceanID = Guid.Empty;
        /// <summary>
        /// 当前合约列表
        /// </summary>
        OceanList _parentList = null;
        /// <summary>
        /// 数据源
        /// </summary>
        List<ClientBasePortList> CurrentSource
        {
            get
            {
                List<ClientBasePortList> list = new List<ClientBasePortList>();
                list = bsList.DataSource as List<ClientBasePortList> ?? new List<ClientBasePortList>();
                list = (from d in list orderby d.ErrorInfo descending, d.ChangeState ascending, d.No ascending select d).ToList();
                return list;
            }
        }
        /// <summary>
        /// 当前选择的数据
        /// </summary>
        ClientBasePortList CurrentRow
        {
            get { return bsList.Current as ClientBasePortList; }
        }
        /// <summary>
        /// 选择的数据
        /// </summary>
        List<ClientBasePortList> SelectedOceanItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                List<ClientBasePortList> tagers = rowIndexs.Select(item => gvMain.GetRow(item)).OfType<ClientBasePortList>().ToList();
                return tagers;
            }
        }
        /// <summary>
        /// 当前列表中的数据源
        /// </summary>
        List<ClientBasePortList> ListDataSource
        {
            get
            {
                int rowIndexs = gvMain.RowCount;

                if (rowIndexs == 0)
                {
                    return new List<ClientBasePortList>();
                }

                List<ClientBasePortList> tagers = new List<ClientBasePortList>();
                for (int i = 0; i < rowIndexs; i++)
                {
                    ClientBasePortList dr = gvMain.GetRow(i) as ClientBasePortList;
                    if (dr != null)
                    {
                        tagers.Add(dr);
                    }
                }

                return tagers;
            }
        }
        /// <summary>
        /// 最大的No号
        /// </summary>
        int? MaxNo
        {
            get
            {
                if (CurrentSource == null || CurrentSource.Count == 0)
                    return 1;
                return (from d in CurrentSource select d.IndexNo).Max();
            }
        }
        /// <summary>
        /// 是否更改
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_LoadedOceanID.IsNullOrEmpty() || _parentList == null || _LoadedOceanID != _parentList.ID) return false;
                List<ClientBasePortList> source = CurrentSource;
                if (source == null || source.Count == 0) return false;
                return source.Any(item => item.IsNew || item.IsDirty || string.IsNullOrEmpty(item.ErrorInfo) == false);
            }
        }
        /// <summary>
        /// 是否有CopyBasePort
        /// </summary>
        public bool IsCopyBasePort
        {
            get
            {
                if (_LoadedOceanID.IsNullOrEmpty() || _parentList == null || _LoadedOceanID != _parentList.ID) return false;
                List<ClientBasePortList> source = CurrentSource;
                if (source == null || source.Count == 0) return false;
                return source.Any(item => !Utility.GuidIsNullOrEmpty(item.CopyID));
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// 基本航线运价编辑界面
        /// </summary>
        public OPBasePortRatesPart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate
            {
                _parentList = null;
                gcMain.DataSource = null;
                dxErrorProvider1.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
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
        /// 重写加载，初始化控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }

        

        #endregion

        #region interface

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
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
            }
        }

        void BindingData(object data)
        {
            if (_parentList == null || _parentList.OceanUnits == null || _parentList.OceanUnits.Count == 0)
            {
                Enabled = false;
                bsList.DataSource = typeof(ClientBasePortList);
                return;
            }

            gvMain.ActiveFilterString = string.Empty;


            List<ClientBasePortList> source = data as List<ClientBasePortList> ?? new List<ClientBasePortList>();
            if (source.Count > 999)
            {
                gvMain.IndicatorWidth = source.Count.ToString(CultureInfo.InvariantCulture).Length * 13;
            }

            bsList.DataSource = source;
        }

        #region BulidColumns

        private void BulidGridViewColumnsByOceanUnits(IEnumerable<string> unitsNameList)
        {
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
            colRate_53HQ.Visible = false;
            #endregion

            int visibleIndex = 15;

            foreach (var item in unitsNameList)
            {
                #region  SetVisible= true;

                switch (item)
                {
                    case "20GP": colRate_20GP.VisibleIndex = visibleIndex + 1; break;
                    case "40GP": colRate_40GP.VisibleIndex = visibleIndex + 2; break;
                    case "40HQ": colRate_40HQ.VisibleIndex = visibleIndex + 3; break;
                    case "45HQ": colRate_45HQ.VisibleIndex = visibleIndex + 4; break;
                    case "20NOR": colRate_20NOR.VisibleIndex = visibleIndex + 5; break;
                    case "40NOR": colRate_40NOR.VisibleIndex = visibleIndex + 6; break;

                    case "20FR": colRate_20FR.VisibleIndex = visibleIndex + 7; break;
                    case "20RH": colRate_20RH.VisibleIndex = visibleIndex + 8; break;
                    case "20RF": colRate_20RF.VisibleIndex = visibleIndex + 9; break;
                    case "20HQ": colRate_20HQ.VisibleIndex = visibleIndex + 19; break;
                    case "20TK": colRate_20TK.VisibleIndex = visibleIndex + 10; break;
                    case "20OT": colRate_20OT.VisibleIndex = visibleIndex + 11; break;
                    case "20HT": colRate_20HT.VisibleIndex = visibleIndex + 12; break;

                    case "40TK": colRate_40TK.VisibleIndex = visibleIndex + 13; break;
                    case "40OT": colRate_40OT.VisibleIndex = visibleIndex + 14; break;
                    case "40FR": colRate_40FR.VisibleIndex = visibleIndex + 15; break;
                    case "40HT": colRate_40HT.VisibleIndex = visibleIndex + 16; break;
                    case "40RH": colRate_40RH.VisibleIndex = visibleIndex + 17; break;
                    case "40RF": colRate_40RF.VisibleIndex = visibleIndex + 18; break;

                    case "45GP": colRate_45GP.VisibleIndex = visibleIndex + 19; break;
                    case "45RF": colRate_45RF.VisibleIndex = visibleIndex + 20; break;
                    case "45HT": colRate_45HT.VisibleIndex = visibleIndex + 21; break;
                    case "45FR": colRate_45FR.VisibleIndex = visibleIndex + 22; break;
                    case "45OT": colRate_45OT.VisibleIndex = visibleIndex + 23; break;
                    case "45TK": colRate_45TK.VisibleIndex = visibleIndex + 24; break;
                    case "45RH": colRate_45RH.VisibleIndex = visibleIndex + 25; break;

                    case "53HQ": colRate_53HQ.VisibleIndex = visibleIndex + 26; break;
                }

                #endregion
            }

            colSurCharge.VisibleIndex = visibleIndex + 27;
            colClosingDate.VisibleIndex = colSurCharge.VisibleIndex + 28;

            colTransitTime.VisibleIndex = colClosingDate.VisibleIndex + 29;
            colDescription.VisibleIndex = colTransitTime.VisibleIndex + 30;
            colFromDate.VisibleIndex = colDescription.VisibleIndex + 31;
            colToDate.VisibleIndex = colFromDate.VisibleIndex + 32;
        }

        #endregion

        #endregion

        #region IEditPart 成员

        public override void EndEdit()
        {
            Validate();
            bsList.EndEdit();
        }

        #endregion

        #region IPart 成员
        
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as OceanList;
                    if (_parentList == null
                        || _parentList.IsNew
                        || _parentList.Permission < OceanPermission.Edit
                        || _parentList.OceanUnits == null
                        || _parentList.OceanUnits.Count == 0)
                        Enabled = false;
                    else
                    {
                        Enabled = true;
                    }

                    if (Visible && Enabled)
                    {
                        List<object> list = OceanPriceService.GetOceanBasePorts(_parentList.ID).Data;

                        _LoadedOceanID = _parentList.ID;

                        List<string> unitNameList = (List<string>)list[0];
                        BulidGridViewColumnsByOceanUnits(unitNameList);

                        DataSource = list[1];

                    }
                    else _LoadedOceanID = Guid.Empty;

                    #region  刷新 Publish按钮状态
                    if (_parentList == null)
                    {
                        barPublish.Enabled = false;
                    }
                    else
                    {
                        barPublish.Enabled = _parentList.State != OceanState.Expired;
                        if (_parentList.State == OceanState.Expired || _parentList.State == OceanState.Invalidated || _parentList.State == OceanState.Draft)
                        {
                            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
                            barSave.Enabled = true;
                            barRemove.Enabled = true;
                        }
                        else
                        {
                            barPublish.Caption = NativeLanguageService.GetText(this, "Pause");
                            barSave.Enabled = false;
                            barRemove.Enabled = false;
                        }
                    }

                    #endregion
                }
            }
        }
        #endregion

        #endregion

        #region Window Event
        /// <summary>
        /// 新增
        /// </summary>
        private void barInsert_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                AddData();
            }
        }
        /// <summary>
        /// 发布合约
        /// </summary>
        private void barPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute();
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                CopyData();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            DeleteData();
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Workitem.Commands[OPCommonConstants.Command_SaveData].Execute();
            }
        }
        /// <summary>
        /// 导入
        /// </summary>
        private void barImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            if (IsChanged)
            {
                DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "GeneralInfoChanged"),
                                                 "Tip",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (result == DialogResult.Yes) { barSave.PerformClick(); }

                return;
            }
            Import();
        }
        /// <summary>
        /// 查找器
        /// </summary>
        private void barShowFinder_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_parentList == null) return;
            OPBasePortFilterForm of = new OPBasePortFilterForm();
            of.SetSouce(_BasePortFilterObject);

            DialogResult dr = PartLoader.ShowDialog(of, NativeLanguageService.GetText(this, "FilterPartTitel"), FormBorderStyle.Sizable);

            //直接点X关闭是不会更变查找条件的
            if (dr == DialogResult.OK)
            {
                _BasePortFilterObject = of.FilterObject;

                string filter = _BasePortFilterObject.BulidFilterString();
                gvMain.ActiveFilterString = filter;
            }
        }
        /// <summary>
        /// 打开模版
        /// </summary>
        private void barTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    if (_parentList == null)
                        return;
                    string path = OceanPriceUIDataHelper.GetOceanBasePortTemplateFileName();
                    Process.Start(path);
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }
        }
        /// <summary>
        /// 相关价格
        /// </summary>
        private void barAssociatedRates_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewAssociatedRates();
        }
        /// <summary>
        /// 全选
        /// </summary>
        private void barSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridHelper.ToggleSelectAllRows(gvMain);
        }
        /// <summary>
        /// 扩展或缩小界面
        /// </summary>
        private void barMaxScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_MaxOceanItem].Execute();
        }
        /// <summary>
        /// 取消排序
        /// </summary>
        private void barCancelSort_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvMain.SortInfo.Clear();
        }

        #region rbtnEditComm
        /// <summary>
        /// 获取焦点:记录文本，用于失去焦点后比较文本是否改变
        /// </summary>
        private void rbtnEditComm_Enter(object sender, EventArgs e)
        {
            _enterText = ((TextEdit)(sender)).Text;
        }

        /// <summary>
        /// 失去焦点:文本有更改，执行搜索
        /// </summary>
        private void rbtnEditComm_Leave(object sender, EventArgs e)
        {
            _BeforeLeaveRowHandle = gvMain.FocusedRowHandle;
            string leaveText = ((TextEdit)(sender)).Text;
            if (leaveText != _enterText)
            {
                OPSearchCommPart scf = Workitem.Items.AddNew<OPSearchCommPart>();
                scf.SetSource(OceanPriceUIDataHelper.Commoditys, leaveText);
                DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
                if (dr == DialogResult.OK && _BeforeLeaveRowHandle >= 0)
                {
                    ClientBasePortList row = gvMain.GetRow(_BeforeLeaveRowHandle) as ClientBasePortList;
                    if (row != null)
                    {
                        row.Comm = scf.CommString;
                        row.IsDirty = true;
                    }

                    bsList.EndEdit();
                    gvMain.RefreshData();
                }
            }
        }
        /// <summary>
        /// 按键为回车时弹出搜索框
        /// </summary>
        private void rbtnEditComm_KeyDown(object sender, KeyEventArgs e)
        {
            ((TextEdit)(sender)).Leave -= rbtnEditComm_Leave;



            if (e.KeyCode == Keys.Enter)
            {
                string leaveText = ((TextEdit)(sender)).Text;
                if (leaveText != _enterText)
                {
                    SearchComm();
                }
                _enterText = ((TextEdit)(sender)).Text;
            }

            ((TextEdit)(sender)).Leave += rbtnEditComm_Leave;
        }
        /// <summary>
        /// 点击查询按钮弹出搜索框
        /// </summary>
        private void rbtnEditComm_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            SearchComm();
        }
        #endregion

        #region GridViewEvent
        /// <summary>
        /// 更新单元格的值时，重新设置Search条件
        /// </summary>
        private void gvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (gvMain.ActiveFilterString.IsNullOrEmpty())
            {
                return;
            }
            string filer = gvMain.ActiveFilterString;
            List<int?> indexNoList = new List<int?>();

            if ((e.Column == colAccount && filer.Contains("AccountType")) ||
                (e.Column == colPOL && filer.Contains("POLName")) ||
                (e.Column == colVIA && filer.Contains("VIAName")) ||
                (e.Column == colPOD && filer.Contains("PODName")) ||
                (e.Column == colPlaceOfDelivery && filer.Contains("DeliveryName")) ||
                (e.Column == colItemCode && filer.Contains("ItemCode")) ||
                (e.Column == colComm && filer.Contains("Comm")) ||
                (e.Column == colTransportClauseName && filer.Contains("TransportClauseName")) &&
                (e.Column == colSurCharge && filer.Contains("SurCharge")) ||
                (e.Column == colDescription && filer.Contains("Description")))
            {
                SetNoFilter(indexNoList);
            }
        }
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space || gvMain.FocusedRowHandle < 0) return;

            if (gvMain.FocusedColumn == colOriginArb)
            {

                ClientBasePortList dr = gvMain.GetRow(gvMain.FocusedRowHandle) as ClientBasePortList;
                if (dr == null) return;
                dr.OriginArb = !dr.OriginArb;
            }
            else if (gvMain.FocusedColumn == colDestArb)
            {

                ClientBasePortList dr = gvMain.GetRow(gvMain.FocusedRowHandle) as ClientBasePortList;
                if (dr == null) return;
                dr.DestArb = !dr.DestArb;
            }


        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            RefreshBarItemEnabled();
        }

        private void RefreshBarItemEnabled()
        {
            if (CurrentRow == null)
                barCopy.Enabled = barRemove.Enabled = barAssociatedRates.Enabled = false;
            else
            {
                barCopy.Enabled = barRemove.Enabled = barAssociatedRates.Enabled = true;
            }
        }

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || (e.Column != colOriginArb && e.Column != colDestArb)) return;

            ClientBasePortList dr = gvMain.GetRow(e.RowHandle) as ClientBasePortList;
            if (dr == null) return;

            if (e.Column == colOriginArb)
            {
                dr.OriginArb = !dr.OriginArb;
            }
            else if (e.Column == colDestArb)
            {
                dr.DestArb = !dr.DestArb;
            }

            gvMain.RefreshData();
        }

        private void gvMain_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvMain.CalcHitInfo(e.Location);

            if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || (hitInfo.Column != colErrorInfo && hitInfo.Column != colMaxData))
            {
                toolTip1.Hide(this);
                return;
            }
            string message = string.Empty;
            if (hitInfo.Column == colErrorInfo)
            {
                ClientBasePortList item = gvMain.GetRow(hitInfo.RowHandle) as ClientBasePortList;
                if (item == null || item.HasError == false) return;
                string s = toolTip1.GetToolTip(this);
                if (toolTip1.Active && s == item.ErrorInfo) return;

                message = item.ErrorInfo;
            }
            else if (hitInfo.Column == colMaxData)
            {
                message = "Repeat data-max amount!";
                ClientBasePortList item = gvMain.GetRow(hitInfo.RowHandle) as ClientBasePortList;
                if (item == null || item.IsMax == false) return;
                string s = toolTip1.GetToolTip(this);
                if (toolTip1.Active && s == message) return;
            }


            Point pt = gcMain.PointToClient(MousePosition);
            pt.X += 20;
            pt.Y += 30;
            toolTip1.Show(message, this, pt, 5000);
        }

        #endregion
        #endregion

        #region Command
        /// <summary>
        /// Tab页切换
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (Visible == false || _LoadedOceanID.IsNullOrEmpty() == false) return;
                    Enabled = _parentList != null;
                    if (_parentList != null && _parentList.ID.IsNullOrEmpty() == false)
                    {
                        List<Object> list = OceanPriceService.GetOceanBasePorts(_parentList.ID).Data;
                        _LoadedOceanID = _parentList.ID;

                        List<string> unitNameList = (List<string>)list[0];
                        BulidGridViewColumnsByOceanUnits(unitNameList);

                        DataSource = list[1];
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 设置为可以发布
        /// </summary>
        public void SetPublish()
        {
            barPublish.Enabled = true;
            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
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
            RegisterMessage("ValidateDataExist", "BasePort - There are some reduplicate items, please check on icons error.");
            RegisterMessage("ItemCodeDifferent", " Some Itemcode are conflicted because it has two or more different Commodities.");
            RegisterMessage("MaxScreen", "&MaxScreen");
            RegisterMessage("BrackScreen", "Brack(&M)");
            RegisterMessage("FilterPartTitel", "Search Base Port Rates");
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
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
            RegisterMessage("ValidateItemCode", "ItemCode must input.");

            RegisterMessage("ChangeSameName", "Would you change all data of the same name?");

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);
            Utility.SetGridViewClickIndicatorHeader2SelectAll(gvMain);
            InitComboboxSource();
            SearchRegister();
        }
        /// <summary>
        /// 初始化下拉框
        /// </summary>
        void InitComboboxSource()
        {
            #region 运输条款
            foreach (var item in OceanPriceUIDataHelper.TransportClauses)
            {
                rcmbTransportClause.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            #endregion
            rcmbTransportClause.SelectedIndexChanged += delegate
            {
                gvMain.CloseEditor();
                ClientBasePortList currentrow = CurrentRow;
                if (currentrow != null)
                {
                    currentrow.TransportClauseName = OceanPriceUIDataHelper.TransportClauses.Find(t => t.ID == currentrow.TransportClauseID).Code;
                }
            };

            #region AccountType
            List<EnumHelper.ListItem<AccountType>> accountTypes = EnumHelper.GetEnumValues<AccountType>(LocalData.IsEnglish);
            foreach (var item in accountTypes)
            {
                if (item.Value == AccountType.None)
                    cmbAccountType.Items.Add(new ImageComboBoxItem(string.Empty, item.Value));
                else
                    cmbAccountType.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            #endregion

            #region ChangeState

            rcmbChangeState.Items.Add(new ImageComboBoxItem(string.Empty, ChangeState.None, -1));
            rcmbChangeState.Items.Add(new ImageComboBoxItem(string.Empty, ChangeState.New, 0));
            rcmbChangeState.Items.Add(new ImageComboBoxItem(string.Empty, ChangeState.Changed, 1));

            #endregion

            #region ErrorState

            cmbError.Items.Add(new ImageComboBoxItem(string.Empty, false, -1));
            cmbError.Items.Add(new ImageComboBoxItem(string.Empty, true, 0));

            cmbMaxData.Items.Add(new ImageComboBoxItem(string.Empty, false, -1));
            cmbMaxData.Items.Add(new ImageComboBoxItem(string.Empty, true, 0));

            #endregion
        }
        /// <summary>
        /// 批处理更新港口数据
        /// </summary>
        /// <param name="newID">新的ID</param>
        /// <param name="newName">新的名称</param>
        /// <param name="oldID">旧的ID</param>
        /// <param name="oldName">旧的名称</param>
        private void UpdatePortData(Guid? newID, string newName, Guid? oldID, string oldName)
        {
            List<ClientBasePortList> source = ListDataSource;

            //POL
            List<ClientBasePortList> samePOLData = (from d in source
                                                    where
                                                        (!d.POLID.IsNullOrEmpty() && d.POLID == oldID)
                                                        || (d.POLID.IsNullOrEmpty() && !d.OLDPOLName.IsNullOrEmpty() && d.OLDPOLName == oldName)
                                                    select d).ToList();
            //VIA
            List<ClientBasePortList> sameVIAData = (from d in source
                                                    where
                                                        (!d.VIAID.IsNullOrEmpty() && d.VIAID == oldID)
                                                        || (d.VIAID.IsNullOrEmpty() && !d.OLDVIAName.IsNullOrEmpty() && d.OLDVIAName == oldName)
                                                    select d).ToList();
            //POD
            List<ClientBasePortList> samePODData = (from d in source
                                                    where
                                                        (!d.PODID.IsNullOrEmpty() && d.PODID == oldID)
                                                        || (d.PODID.IsNullOrEmpty() && !d.OLDPODName.IsNullOrEmpty() && d.OLDPODName == oldName)
                                                    select d).ToList();
            //Delivery
            List<ClientBasePortList> sameDeliveryData = (from d in source
                                                         where
                                                             (!d.PlaceOfDeliveryID.IsNullOrEmpty() && d.PlaceOfDeliveryID == oldID)
                                                             || (d.PlaceOfDeliveryID.IsNullOrEmpty() && !d.OLDDeliveryName.IsNullOrEmpty() && d.OLDDeliveryName == oldName)
                                                         select d).ToList();

            if ((samePOLData != null && samePOLData.Count > 0) ||
                (sameVIAData != null && sameVIAData.Count > 0) ||
                (samePODData != null && samePODData.Count > 0) ||
                (sameDeliveryData != null && sameDeliveryData.Count > 0))
            {
                DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
                                        LocalData.IsEnglish ? "Tip" : "提示",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (!newID.IsNullOrEmpty())
                    {
                        //POL
                        foreach (var item in samePOLData)
                        {
                            item.POLID = newID.Value;
                            item.POLName = item.OLDPOLName = newName;

                            item.IsDirty = true;
                        }
                        //POD
                        foreach (var item in samePODData)
                        {
                            item.PODID = newID.Value;
                            item.PODName = item.OLDPODName = newName;

                            item.IsDirty = true;
                        }
                    }
                    //VIA
                    foreach (var item in sameVIAData)
                    {
                        item.VIAID = newID;
                        item.VIAName = item.OLDVIAName = newName;

                        item.IsDirty = true;
                    }
                    //Delivery
                    foreach (var item in sameDeliveryData)
                    {
                        item.PlaceOfDeliveryID = newID;
                        item.PlaceOfDeliveryName = item.OLDDeliveryName = newName;

                        item.IsDirty = true;
                    }

                    gvMain.RefreshData();

                    SendKeys.Send("{TAB}");
                }
            }

        }
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {

            #region pol

            DataFindClientService.RegisterGridColumnFinder(colPOL, CommonFinderConstants.OceanLocationFinder
             , "POLID", "POLName"
             , "ID", LocalData.IsEnglish ? "EName" : "EName"
             , delegate(object befocePickedData, object afterPickedData)
             {
                 ClientBasePortList befoceChangedRow = befocePickedData as ClientBasePortList;
                 ClientBasePortList afterChangedRow = afterPickedData as ClientBasePortList;

                 if (befoceChangedRow != null && afterChangedRow != null)
                 {
                     UpdatePortData(afterChangedRow.POLID, afterChangedRow.POLName, befoceChangedRow.POLID, afterChangedRow.OLDPOLName);
                 }
                 afterChangedRow.OLDPOLName = afterChangedRow.POLName;
             });

            #endregion

            #region VIA

            DataFindClientService.RegisterGridColumnFinder(colVIA
             , CommonFinderConstants.OceanLocationFinder
             , "VIAID"
             , "VIAName"
             , "ID"
             , LocalData.IsEnglish ? "EName" : "EName"
             , delegate(object befocePickedData, object afterPickedData)
             {
                 ClientBasePortList befoceChangedRow = befocePickedData as ClientBasePortList;
                 ClientBasePortList afterChangedRow = afterPickedData as ClientBasePortList;

                 if (befoceChangedRow != null && afterChangedRow != null)
                 {
                     UpdatePortData(afterChangedRow.VIAID, afterChangedRow.VIAName, befoceChangedRow.VIAID, afterChangedRow.OLDVIAName);
                 }
                 afterChangedRow.OLDVIAName = afterChangedRow.VIAName;
             });
            #endregion

            #region pod

            DataFindClientService.RegisterGridColumnFinder(colPOD
           , CommonFinderConstants.OceanLocationFinder
           , "PODID"
           , "PODName"
           , "ID"
           , LocalData.IsEnglish ? "EName" : "EName"
           , delegate(object befocePickedData, object afterPickedData)
           {
               ClientBasePortList befoceChangedRow = befocePickedData as ClientBasePortList;
               ClientBasePortList afterChangedRow = afterPickedData as ClientBasePortList;

               if (befoceChangedRow != null && afterChangedRow != null)
               {
                   UpdatePortData(afterChangedRow.PODID, afterChangedRow.PODName, befoceChangedRow.PODID, afterChangedRow.OLDPODName);
               }
               afterChangedRow.OLDPODName = afterChangedRow.PODName;
           });
            #endregion

            #region PlaceOfDelivery
            DataFindClientService.RegisterGridColumnFinder(colPlaceOfDelivery
           , CommonFinderConstants.OceanLocationFinder
           , "PlaceOfDeliveryID"
           , "PlaceOfDeliveryName"
           , "ID"
           , LocalData.IsEnglish ? "EName" : "EName"
           , delegate(object befocePickedData, object afterPickedData)
           {
               ClientBasePortList befoceChangedRow = befocePickedData as ClientBasePortList;
               ClientBasePortList afterChangedRow = afterPickedData as ClientBasePortList;

               if (befoceChangedRow != null && afterChangedRow != null)
               {
                   UpdatePortData(afterChangedRow.PlaceOfDeliveryID, afterChangedRow.PlaceOfDeliveryName, befoceChangedRow.PlaceOfDeliveryID, afterChangedRow.OLDDeliveryName);
               }
               afterChangedRow.OLDDeliveryName = afterChangedRow.PlaceOfDeliveryName;
           });
            #endregion

            #region Carrier
            DataFindClientService.RegisterGridColumnFinder(colCarrier
           , CommonFinderConstants.CustomerCarrierFinder
           , "CarrierID"
           , "CarrierName"
           , "ID"
           , "Code"
           , delegate(object befocePickedData, object afterPickedData)
           {
               ClientBasePortList befoceChangedRow = befocePickedData as ClientBasePortList;
               ClientBasePortList afterChangedRow = afterPickedData as ClientBasePortList;

               if (befoceChangedRow != null && afterChangedRow != null)
               {
                   List<ClientBasePortList> source = ListDataSource;
                   List<ClientBasePortList> sameData = source.FindAll(s => s.CarrierID == befoceChangedRow.CarrierID);
                   if (sameData != null && sameData.Count > 0)
                   {
                       DialogResult result = XtraMessageBox.Show(NativeLanguageService.GetText(this, "ChangeSameName"),
                                               LocalData.IsEnglish ? "Tip" : "提示",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
                       if (result == DialogResult.Yes)
                       {
                           foreach (var item in sameData)
                           {
                               item.CarrierID = afterChangedRow.CarrierID;
                               item.CarrierName = afterChangedRow.CarrierName;
                               item.IsDirty = true;
                           }

                           gvMain.RefreshData();
                           SendKeys.Send("{TAB}");
                       }
                   }
               }
           });
            #endregion
        }
        /// <summary>
        /// 是否为发布状态
        /// </summary>
        /// <returns></returns>
        private bool IsPublish()
        {
            if (_parentList == null)
            {
                return false;
            }
            if (_parentList.State == OceanState.Published)
            {
                XtraMessageBox.Show("Please pause contract");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 添加
        /// </summary>
        private void AddData()
        {
            ClientBasePortList newData = new ClientBasePortList();
            OceanPriceTransformHelper.BulidNewBasePortData(newData, _parentList);
            newData.BulidRateToZeroByOceanUints(_parentList.OceanUnits);
            newData.BeginEdit();

            if (MaxNo >= 0)
            {
                newData.IndexNo = MaxNo + 1;
            }
            else
            {
                newData.IndexNo = 0;
            }

            (bsList.List as List<ClientBasePortList>).Insert(0, newData);
            bsList.ResetBindings(false);

            List<int?> indexNoList = new List<int?>();
            indexNoList.Add(newData.IndexNo);

            SetNoFilter(indexNoList);

            gvMain.FocusedRowHandle = 0;
            gvMain.SelectCell(0, colAccount);

        }
        /// <summary>
        /// 复制
        /// </summary>
        private void CopyData()
        {
            List<ClientBasePortList> selecteds = SelectedOceanItem;
            if (CurrentRow == null || selecteds == null || selecteds.Count == 0) return;

            List<int?> indexNoList = new List<int?>();

            List<ClientBasePortList> copyTager = new List<ClientBasePortList>();
            foreach (var item in selecteds)
            {
                ClientBasePortList newItem = Utility.Clone<ClientBasePortList>(item);
                newItem.ID = Guid.Empty;
                newItem.OceanID = _parentList.ID;
                newItem.CreateByID = LocalData.UserInfo.LoginID;
                newItem.CreateByName = LocalData.UserInfo.LoginName;
                newItem.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                //newItem.FromDate = _parentList.FromDate;
                //newItem.ToDate = _parentList.ToDate;
                newItem.UpdateDate = null;
                newItem.No = null;
                newItem.CopyID = item.ID;

                item.BeginEdit();
                copyTager.Add(newItem);
            }

            List<ClientBasePortList> source = CurrentSource;
            foreach (var item in copyTager)
            {
                if (MaxNo >= 0)
                {
                    item.IndexNo = MaxNo + 1;
                }
                else
                {
                    item.IndexNo = 0;
                }

                source.Insert(0, item);

                indexNoList.Add(item.IndexNo);
            }

            gvMain.SortInfo.Clear();
            bsList.DataSource = source;
            bsList.ResetBindings(false);
            SetNoFilter(indexNoList);
            gvMain.MoveFirst();
            gvMain.SelectRows(0, copyTager.Count - 1);
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void DeleteData()
        {
            gvMain.CloseEditor();
            List<ClientBasePortList> selecteds = SelectedOceanItem;
            if (selecteds == null || selecteds.Count == 0) return;

            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                             NativeLanguageService.GetText(this, "RemoveSelectedItem"),
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion

            #region 构建需数据库中删除的数据

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdates = new List<DateTime?>();

            foreach (var item in selecteds)
            {
                if (item.IsNew) continue;

                needRemoveIDs.Add(item.ID);
                needRemoveUpdates.Add(item.UpdateDate);
            }
            #endregion

            try
            {
                ManyResultData resultData = new ManyResultData();
                if (needRemoveIDs.Count > 0)
                {
                    resultData = OceanPriceService.RemoveBasePorts(needRemoveIDs.ToArray(), LocalData.UserInfo.LoginID, needRemoveUpdates.ToArray());
                }

                List<ClientBasePortList> source = CurrentSource;

                List<Guid> noRemoveIDList = (from d in resultData.ChildResults select d.ID).ToList();
                //如果运价被引用了，则不删除
                foreach (var item in selecteds)
                {
                    if (!noRemoveIDList.Contains(item.ID))
                    {
                        source.Remove(item);
                    }
                }

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "RemoveSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        #region Save

        internal void RefreshUIData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<ClientBasePortList> source = CurrentSource;
                source = source.OrderByDescending(b => b.No).ToList();

                bsList.DataSource = source;
                gvMain.RefreshData();
            }
        }
        /// <summary>
        /// 获得Copy数据
        /// </summary>
        /// <returns></returns>
        public BasePortCopyIDToList GetCopyIDList()
        {
            BasePortCopyIDToList copyInfo = new BasePortCopyIDToList();
            copyInfo.IDList = new List<Guid>();
            copyInfo.CopyIDList = new List<Guid>();

            List<ClientBasePortList> source = (from d in CurrentSource where !Utility.GuidIsNullOrEmpty(d.CopyID) select d).ToList();
            foreach (ClientBasePortList item in source)
            {
                copyInfo.IDList.Add(item.ID);
                copyInfo.CopyIDList.Add(item.CopyID.Value);
            }
            return copyInfo;
        }
        /// <summary>
        /// 清空CopyID
        /// </summary>
        public void ClearCopyID()
        {
            List<ClientBasePortList> source = (from d in CurrentSource where !Utility.GuidIsNullOrEmpty(d.CopyID) select d).ToList();
            source.ForEach(o => o.CopyID = null);

            bsList.ResetBindings(false);
        }

        internal List<ClientBasePortList> GetChangedItem()
        {
            List<ClientBasePortList> source = CurrentSource;
            List<ClientBasePortList> chengedItem = new List<ClientBasePortList>();
            foreach (var item in source)
            {
                if (item.IsNew || item.IsDirty)
                {
                    if (item.FromDate != null)
                    {
                        DateTime dt = new DateTime(item.FromDate.Value.Year, item.FromDate.Value.Month, item.FromDate.Value.Day).ToLocalTime();
                        item.FromDate = DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
                    }
                    if (item.ToDate != null)
                    {
                        DateTime dt = new DateTime(item.ToDate.Value.Year, item.ToDate.Value.Month, item.ToDate.Value.Day).ToLocalTime();
                        item.ToDate = DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
                    }

                    chengedItem.Add(item);
                }
            }
            return chengedItem;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            if (IsChanged == false) return true;

            Validate();
            bsList.EndEdit();

            List<ClientBasePortList> chengedItem = GetChangedItem();

            if (ValidateData(chengedItem) == false) return false;

            return true;
        }

        /// <summary>
        /// 检查重复的数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateExistData()
        {
            bool isExistData = false;
            #region 重复条件: Account+POLID+VIAID+PODID+PlaceOfDeliveryID+OriginArb+DestArb+ItemCode+TransportClauseID+Comm+CarrierID

            List<string> strList = new List<string>();
            List<ClientBasePortList> dataList = new List<ClientBasePortList>();

            foreach (ClientBasePortList item in CurrentSource)
            {
                item.IsMax = false;

                StringBuilder bulider = new StringBuilder();

                bulider.Append(DataTypeHelper.GetString(item.Account, "").ToUpper().Trim());
                bulider.Append(DataTypeHelper.GetGuid(item.POLID, Guid.NewGuid()));
                bulider.Append(item.VIAID);
                bulider.Append(DataTypeHelper.GetGuid(item.PODID, Guid.NewGuid()));
                bulider.Append(DataTypeHelper.GetGuid(item.PlaceOfDeliveryID, Guid.NewGuid()));
                bulider.Append(item.OriginArb);
                bulider.Append(item.DestArb);
                bulider.Append(DataTypeHelper.GetString(item.ItemCode, "").ToUpper().Trim());
                bulider.Append(item.TransportClauseID);
                bulider.Append(DataTypeHelper.GetString(item.Comm, "").ToUpper().Trim());
                bulider.Append(item.CarrierID);

                if (!strList.Contains(bulider.ToString()))
                {
                    strList.Add(bulider.ToString());
                }
                else
                {
                    isExistData = true;
                    int count = (from d in dataList
                                 where
                                  DataTypeHelper.GetString(d.Account, "").ToUpper().Trim() == DataTypeHelper.GetString(item.Account, "").ToUpper().Trim() &&
                                  d.POLID == item.POLID &&
                                  d.VIAID == item.VIAID &&
                                  d.PODID == item.PODID &&
                                  d.PlaceOfDeliveryID == item.PlaceOfDeliveryID &&
                                  d.OriginArb == item.OriginArb &&
                                  d.DestArb == item.DestArb &&
                                  DataTypeHelper.GetString(d.ItemCode, "").ToUpper().Trim() == DataTypeHelper.GetString(item.ItemCode, "").ToUpper().Trim() &&
                                  d.TransportClauseID == item.TransportClauseID &&
                                  DataTypeHelper.GetString(d.Comm, "").ToUpper().Trim() == DataTypeHelper.GetString(item.Comm, "").ToUpper().Trim() &&
                                  d.CarrierID == item.CarrierID
                                 select d).Count();

                    if (count == 0)
                    {
                        ClientBasePortList newItem = new ClientBasePortList();

                        newItem.Account = DataTypeHelper.GetString(item.Account, "").ToUpper().Trim();
                        newItem.POLID = item.POLID;
                        newItem.VIAID = item.VIAID;
                        newItem.PODID = item.PODID;
                        newItem.PlaceOfDeliveryID = item.PlaceOfDeliveryID;
                        newItem.OriginArb = item.OriginArb;
                        newItem.DestArb = item.DestArb;
                        newItem.ItemCode = DataTypeHelper.GetString(item.ItemCode, "").ToUpper().Trim();
                        newItem.TransportClauseID = item.TransportClauseID;
                        newItem.Comm = DataTypeHelper.GetString(item.Comm, "").ToUpper().Trim();
                        newItem.CarrierID = item.CarrierID;

                        dataList.Add(newItem);
                    }
                }
            }
            #endregion

            foreach (ClientBasePortList item in dataList)
            {
                List<ClientBasePortList> errorList = (from d in CurrentSource
                                                      where
                                                       DataTypeHelper.GetString(d.Account, "").ToUpper().Trim() == DataTypeHelper.GetString(item.Account, "").ToUpper().Trim() &&
                                                       d.POLID == item.POLID &&
                                                       d.VIAID == item.VIAID &&
                                                       d.PODID == item.PODID &&
                                                       d.PlaceOfDeliveryID == item.PlaceOfDeliveryID &&
                                                       d.OriginArb == item.OriginArb &&
                                                       d.DestArb == item.DestArb &&
                                                       DataTypeHelper.GetString(d.ItemCode, "").ToUpper().Trim() == DataTypeHelper.GetString(item.ItemCode, "").ToUpper().Trim() &&
                                                       d.TransportClauseID == item.TransportClauseID &&
                                                       DataTypeHelper.GetString(d.Comm, "").ToUpper().Trim() == DataTypeHelper.GetString(item.Comm, "").ToUpper().Trim() &&
                                                       d.CarrierID == item.CarrierID
                                                      select d).ToList();

                if (errorList == null || errorList.Count == 0)
                {
                    continue;
                }
                foreach (ClientBasePortList errorData in errorList)
                {
                    errorData.IsDirty = true;
                    if (!DataTypeHelper.GetString(errorData.ErrorInfo, "").Contains(NativeLanguageService.GetText(this, "ValidateItemExist")))
                    {
                        errorData.ErrorInfo += NativeLanguageService.GetText(this, "ValidateItemExist");
                    }
                }

                if (this.colRate_40GP.Visible && this.colRate_40GP.VisibleIndex > 0)
                {
                    ClientBasePortList maxItem = (from d in errorList orderby d.Rate_40GP descending select d).Take(1).SingleOrDefault<ClientBasePortList>();
                    maxItem.IsMax = true;
                }
                else if (this.colRate_40OT.Visible && this.colRate_40GP.VisibleIndex > 0)
                {
                    ClientBasePortList maxItem = (from d in errorList orderby d.Rate_40OT descending select d).Take(1).SingleOrDefault<ClientBasePortList>();
                    maxItem.IsMax = true;
                }
                else if (this.colRate_53HQ.Visible && this.colRate_40GP.VisibleIndex > 0)
                {
                    ClientBasePortList maxItem = (from d in errorList orderby d.Rate_53HQ descending select d).Take(1).SingleOrDefault<ClientBasePortList>();
                    maxItem.IsMax = true;
                }

            }
            if (isExistData)
            {
                this.gvMain.RefreshData();
            }

            return isExistData;
        }


        private string GetErrorInfo(string errorInfoList, string currentError)
        {
            if (errorInfoList.IsNullOrEmpty())
            {
                errorInfoList = currentError;
            }
            else
            {
                errorInfoList = errorInfoList + Environment.NewLine + currentError;
            }
            return errorInfoList;
        }
        /// <summary>
        /// BasePort非空、长度的验证
        /// </summary>
        /// <param name="basePortList"></param>
        /// <returns></returns>
        private bool ValidateBasePortData(List<ClientBasePortList> basePortList)
        {
            bool isSrcc = true;
            foreach (var item in basePortList)
            {
                string errorMessage = string.Empty;
                if (DataTypeHelper.GetString(item.ErrorInfo, "").Contains(NativeLanguageService.GetText(this, "ValidateItemExist")))
                {
                    item.ErrorInfo = NativeLanguageService.GetText(this, "ValidateItemExist");
                }
                else
                {
                    item.ErrorInfo = string.Empty;
                }


                if (item.ValidateHasRate() == false)
                {
                    errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateRate_20GP"));
                    isSrcc = false;
                }
                if (!item.Account.IsNullOrEmpty() && item.Account.Length > 2000)
                {
                    errorMessage = GetErrorInfo(errorMessage, "Account tength > 2000");
                    isSrcc = false;
                }
                if (item.POLID.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, "POL must input");
                    isSrcc = false;
                }
                if (item.PODID.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, "POD must input");
                    isSrcc = false;
                }
                if (item.PlaceOfDeliveryID.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, "Delivery must input");
                    isSrcc = false;
                }
                if (item.POLID.IsNullOrEmpty() == false && item.PODID.IsNullOrEmpty() == false && item.PODID == item.POLID)
                {
                    errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidatePOLSamePOD"));
                    isSrcc = false;
                }
                if (item.Comm.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateComm"));
                    isSrcc = false;
                }
                if (!item.Comm.IsNullOrEmpty() && item.Comm.Length > 2000)
                {
                    errorMessage = GetErrorInfo(errorMessage, "Comm length >2000");
                    isSrcc = false;
                }
                if (item.TransportClauseID.IsNullOrEmpty())
                {
                    errorMessage = GetErrorInfo(errorMessage, "Term must input");
                    isSrcc = false;
                }
                if (item.FromDate.HasValue && item.ToDate.HasValue && item.FromDate >= item.ToDate)
                {
                    errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateFromDate"));
                    isSrcc = false;
                }
                if (!item.SurCharge.IsNullOrEmpty() && item.SurCharge.Length > 500)
                {
                    errorMessage = GetErrorInfo(errorMessage, "SurCharge length >500");
                    isSrcc = false;
                }
                if (!item.TransitTime.IsNullOrEmpty() && item.TransitTime.Length > 120)
                {
                    errorMessage = GetErrorInfo(errorMessage, "TransitTime length >120");
                    isSrcc = false;
                }
                if (!item.Description.IsNullOrEmpty() && item.Description.Length > 2000)
                {
                    errorMessage = GetErrorInfo(errorMessage, "Description length >2000");
                    isSrcc = false;
                }
                item.ErrorInfo = errorMessage;
            }

            return isSrcc;
        }
        private bool ValidateData(List<ClientBasePortList> chengedItems)
        {

            if (chengedItems == null || chengedItems.Count == 0) return false;

            bool isSrcc = true;

            isSrcc = ValidateBasePortData(chengedItems);

            #region 验证重复数据
            bool isExist = ValidateExistData();

            if (isExist)
            {
                isSrcc = false;
            }
            if (isExist)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ValidateDataExist"));
            }
            #endregion

            if (isSrcc == false)
            {
                if (!gvMain.ActiveFilterString.IsNullOrEmpty())
                {
                    if (gvMain.ActiveFilterString.Contains("[HasError] = 'True'"))
                    {
                        gvMain.ActiveFilterString = "[HasError] = 'True'";
                    }
                }
                gvMain.RefreshData();
            }
            return isSrcc;
        }

        #endregion

        #region MaxScreen

        /// <summary>
        /// 扩展或缩小界面
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_MaxOceanItem)]
        public void Command_MaxOceanItem(object sender, EventArgs e)
        {
            isMax = !isMax;
            barMaxScreen.Caption = NativeLanguageService.GetText(this, isMax ? "BrackScreen" : "MaxScreen");
        }

        #endregion

        #region Finder
        /// <summary>
        /// 设置以单号为查询条件
        /// </summary>
        /// <returns></returns>
        private void SetNoFilter(IEnumerable<int?> indexNoList)
        {
            if (gvMain.ActiveFilterString.IsNullOrEmpty())
            {
                return;
            }

            List<int?> oldIndexNoList = (from d in ListDataSource where d.IndexNo > 0 select d.IndexNo).ToList();
            oldIndexNoList = oldIndexNoList.Union(indexNoList).ToList();

            string strFilters = string.Empty;

            StringBuilder strList = new StringBuilder();

            foreach (int? item in oldIndexNoList)
            {
                if (item > 0)
                {
                    if (strList.Length > 0)
                    {
                        strList.Append(" OR ");
                    }
                    strList.Append(" ([IndexNo] like '" + item + "') ");
                }
            }

            if (strList.Length > 0)
            {
                gvMain.ActiveFilterString = strList.ToString();
            }
        }

        #endregion

        #region ViewAssociatedRates
        private void ViewAssociatedRates()
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;


            List<ArbitraryList> list = OceanPriceService.GetOceanArbitrarysByBasePortID(CurrentRow.ID);
            List<AdditionalFeeList> addFeeList = OceanPriceService.GetOceanAdditionalFeesByBasePort(CurrentRow.ID);

            OPAssociatedRatesPart arf = Workitem.Items.AddNew<OPAssociatedRatesPart>();

            arf.SetArbitrarySource(list, _parentList);
            arf.SetAdditionalFee(addFeeList, _parentList);

            PartLoader.ShowDialog(arf, NativeLanguageService.GetText(this, "AssociatedRatesPartTitel"), FormBorderStyle.Sizable);

        }
        #endregion

        #region 批量修改
        private void barBatchItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsPublish())
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                BatchChangeOceanItem();
            }
        }

        void BatchChangeOceanItem()
        {
            List<ClientBasePortList> source = SelectedOceanItem;

            List<ClientBasePortList> selected = SelectedOceanItem;
            if (selected == null || selected.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, NativeLanguageService.GetText(this, "SelectOneRate"));
                return;
            }

            OPBasePortBatchItemForm opbForm = Workitem.Items.AddNew<OPBasePortBatchItemForm>();
            opbForm.SetSource(_parentList, selected[0]);
            DialogResult dr = PartLoader.ShowDialog(opbForm, NativeLanguageService.GetText(this, "BatchEditPartTitel"), FormBorderStyle.Sizable);
            if (dr != DialogResult.OK) return;

            BasePortBatchItem batchItem = opbForm.BatchItem;
            if (batchItem == null) return;
            List<int?> indexNoList = new List<int?>();
            SetNoFilter(indexNoList);
            try
            {
                Enabled = false;
                foreach (var item in selected)
                {
                    item.BeginEdit();

                    #region Rate
                    foreach (var clientUnitItem in batchItem.OceanClientUnitList)
                    {
                        if (string.IsNullOrEmpty(clientUnitItem.Rate)) continue;

                        decimal rate = Convert.ToDecimal(clientUnitItem.Rate);

                        if (!batchItem.RateAppend)
                        {
                            switch (clientUnitItem.UnitName)
                            {
                                #region 更新价格--直接更新
                                case "45FR": item.Rate_45FR = rate; break;
                                case "40RF": item.Rate_40RF = rate; break;
                                case "45HT": item.Rate_45HT = rate; break;
                                case "20RF": item.Rate_20RF = rate; break;
                                case "20HQ": item.Rate_20HQ = rate; break;
                                case "20TK": item.Rate_20TK = rate; break;
                                case "20GP": item.Rate_20GP = rate; break;
                                case "40TK": item.Rate_40TK = rate; break;
                                case "40OT": item.Rate_40OT = rate; break;
                                case "20FR": item.Rate_20FR = rate; break;
                                case "45GP": item.Rate_45GP = rate; break;
                                case "40GP": item.Rate_40GP = rate; break;
                                case "45RF": item.Rate_45RF = rate; break;
                                case "20RH": item.Rate_20RH = rate; break;
                                case "45OT": item.Rate_45OT = rate; break;
                                case "40NOR": item.Rate_40NOR = rate; break;
                                case "40FR": item.Rate_40FR = rate; break;
                                case "20OT": item.Rate_20OT = rate; break;
                                case "45TK": item.Rate_45TK = rate; break;
                                case "20NOR": item.Rate_20NOR = rate; break;
                                case "40HT": item.Rate_40HT = rate; break;
                                case "40RH": item.Rate_40RH = rate; break;
                                case "45RH": item.Rate_45RH = rate; break;
                                case "45HQ": item.Rate_45HQ = rate; break;
                                case "20HT": item.Rate_20HT = rate; break;
                                case "40HQ": item.Rate_40HQ = rate; break;
                                case "53HQ": item.Rate_53HQ = rate; break;
                                #endregion
                            }
                        }
                        else
                        {
                            #region 更新价格--增加或删除
                            string untiName = clientUnitItem.UnitName;

                            if (untiName == "45FR" && !item.Rate_45FR.IsNullOrZero())
                            {
                                item.Rate_45FR += rate;
                            }
                            else if (untiName == "40RF" && !item.Rate_40RF.IsNullOrZero())
                            {
                                item.Rate_40RF += rate;
                            }
                            else if (untiName == "45HT" && !item.Rate_45HT.IsNullOrZero())
                            {
                                item.Rate_45HT += rate;
                            }
                            else if (untiName == "20RF" && !item.Rate_20RF.IsNullOrZero())
                            {
                                item.Rate_20RF += rate;
                            }
                            else if (untiName == "20HQ" && !item.Rate_20HQ.IsNullOrZero())
                            {
                                item.Rate_20HQ += rate;
                            }
                            else if (untiName == "20TK" && !item.Rate_20TK.IsNullOrZero())
                            {
                                item.Rate_20TK += rate;
                            }
                            else if (untiName == "20GP" && !item.Rate_20GP.IsNullOrZero())
                            {
                                item.Rate_20GP += rate;
                            }
                            else if (untiName == "40TK" && !item.Rate_40TK.IsNullOrZero())
                            {
                                item.Rate_40TK += rate;
                            }
                            else if (untiName == "40OT" && !item.Rate_40OT.IsNullOrZero())
                            {
                                item.Rate_40OT += rate;
                            }
                            else if (untiName == "20FR" && !item.Rate_20FR.IsNullOrZero())
                            {
                                item.Rate_20FR += rate;
                            }
                            else if (untiName == "45GP" && !item.Rate_45GP.IsNullOrZero())
                            {
                                item.Rate_45GP += rate;
                            }
                            else if (untiName == "40GP" && !item.Rate_40GP.IsNullOrZero())
                            {
                                item.Rate_40GP += rate;
                            }
                            else if (untiName == "45RF" && !item.Rate_45RF.IsNullOrZero())
                            {
                                item.Rate_45RF += rate;
                            }
                            else if (untiName == "20RH" && !item.Rate_20RH.IsNullOrZero())
                            {
                                item.Rate_20RH += rate;
                            }
                            else if (untiName == "45OT" && !item.Rate_45OT.IsNullOrZero())
                            {
                                item.Rate_45OT += rate;
                            }
                            else if (untiName == "40NOR" && !item.Rate_40NOR.IsNullOrZero())
                            {
                                item.Rate_40NOR += rate;
                            }
                            else if (untiName == "40FR" && !item.Rate_40FR.IsNullOrZero())
                            {
                                item.Rate_40FR += rate;
                            }
                            else if (untiName == "20OT" && !item.Rate_20OT.IsNullOrZero())
                            {
                                item.Rate_20OT += rate;
                            }
                            else if (untiName == "45TK" && !item.Rate_45TK.IsNullOrZero())
                            {
                                item.Rate_45TK += rate;
                            }
                            else if (untiName == "20NOR" && !item.Rate_20NOR.IsNullOrZero())
                            {
                                item.Rate_20NOR += rate;
                            }
                            else if (untiName == "40RH" && !item.Rate_40RH.IsNullOrZero())
                            {
                                item.Rate_40RH += rate;
                            }
                            else if (untiName == "40HT" && !item.Rate_40HT.IsNullOrZero())
                            {
                                item.Rate_40HT += rate;
                            }
                            else if (untiName == "45RH" && !item.Rate_45RH.IsNullOrZero())
                            {
                                item.Rate_45RH += rate;
                            }
                            else if (untiName == "45HQ" && !item.Rate_45HQ.IsNullOrZero())
                            {
                                item.Rate_45HQ += rate;
                            }
                            else if (untiName == "20HT" && !item.Rate_20HT.IsNullOrZero())
                            {
                                item.Rate_20HT += rate;
                            }
                            else if (untiName == "40HQ" && !item.Rate_40HQ.IsNullOrZero())
                            {
                                item.Rate_40HQ += rate;
                            }
                            else if (untiName == "53HQ" && !item.Rate_53HQ.IsNullOrZero())
                            {
                                item.Rate_53HQ += rate;
                            }

                            #endregion
                        }
                    }

                    #endregion

                    #region Account
                    if (batchItem.CleanAccount)
                        item.Account = string.Empty;
                    else
                    {
                        if (string.IsNullOrEmpty(batchItem.Account) == false)
                        {
                            item.Account = batchItem.Account;
                        }
                    }

                    #endregion

                    #region Ports
                    #region POL

                    if (batchItem.POLID.IsNullOrEmpty() == false)
                    {
                        item.POLID = batchItem.POLID.Value;
                        item.POLName = batchItem.POLName;
                    }

                    #endregion

                    #region VIA

                    if (batchItem.VIAID.IsNullOrEmpty() == false)
                    {
                        item.VIAID = batchItem.VIAID.Value;
                        item.VIAName = batchItem.VIAName;
                    }

                    #endregion

                    #region PODID

                    if (batchItem.PODID.IsNullOrEmpty() == false)
                    {
                        item.PODID = batchItem.PODID.Value;
                        item.PODName = batchItem.PODName;
                    }

                    #endregion

                    #region PlaceOfDeliveryID

                    if (batchItem.PlaceOfDeliveryID.IsNullOrEmpty() == false)
                    {
                        item.PlaceOfDeliveryID = batchItem.PlaceOfDeliveryID.Value;
                        item.PlaceOfDeliveryName = batchItem.PlaceOfDeliveryName;
                    }

                    #endregion

                    #endregion

                    #region OriginArb DestArb ItemCode AccountType TransportClauseList CLS TransitTime FromDate ToDate

                    if (batchItem.OriginArb.HasValue) item.OriginArb = batchItem.OriginArb.Value;

                    if (batchItem.DestArb.HasValue) item.DestArb = batchItem.DestArb.Value;

                    if (batchItem.ItemCode.IsNullOrEmpty() == false) item.ItemCode = batchItem.ItemCode;

                    if (batchItem.TransportClauseListID.HasValue) item.TransportClauseID = batchItem.TransportClauseListID.Value;

                    if (batchItem.AccountType != AccountType.None) item.AccountType = batchItem.AccountType;

                    if (batchItem.CLS.IsNullOrEmpty() == false) item.ClosingDate = batchItem.CLS;

                    if (batchItem.TransitTime.IsNullOrEmpty() == false) item.TransitTime = batchItem.TransitTime;

                    #endregion

                    #region DateTime
                    if (batchItem.CleanFromDate)
                    {
                        item.FromDate = null;
                    }
                    else
                    {
                        if (batchItem.FromDate.HasValue)
                        {
                            item.FromDate = DateTime.SpecifyKind(batchItem.FromDate.Value, DateTimeKind.Unspecified);
                        }
                    }
                    if (batchItem.CleanToDate)
                    {
                        item.ToDate = null;
                    }
                    else
                    {
                        if (batchItem.ToDate.HasValue)
                        {
                            item.ToDate = DateTime.SpecifyKind(batchItem.ToDate.Value, DateTimeKind.Unspecified);
                        }
                    }
                    #endregion

                    #region Comm

                    if (batchItem.CommOperation == ChangeOperation.Clean)
                    {
                        item.Comm = string.Empty;
                    }
                    else if (batchItem.Comm.IsNullOrEmpty() == false)
                    {
                        if (batchItem.CommOperation == ChangeOperation.Append)
                            item.Comm += batchItem.Comm;
                        else
                            item.Comm = batchItem.Comm;
                    }


                    #endregion

                    #region Surcharge

                    if (batchItem.SurchargeOperation == ChangeOperation.Clean)
                    {
                        item.SurCharge = string.Empty;
                    }
                    else if (batchItem.SurCharge.IsNullOrEmpty() == false)
                    {
                        if (batchItem.SurchargeOperation == ChangeOperation.Append)
                            item.SurCharge += batchItem.SurCharge;
                        else
                            item.SurCharge = batchItem.SurCharge;
                    }

                    #endregion

                    #region Description

                    if (batchItem.DescriptionOperation == ChangeOperation.Clean)
                    {
                        item.Description = string.Empty;
                    }
                    else if (batchItem.Description.IsNullOrEmpty() == false)
                    {
                        if (batchItem.DescriptionOperation == ChangeOperation.Append)
                            item.Description += batchItem.Description;
                        else
                            item.Description = batchItem.Description;
                    }

                    #endregion

                    #region TransitTime

                    if (batchItem.TransitTimeOperation == ChangeOperation.Clean)
                    {
                        item.TransitTime = string.Empty;
                    }
                    else if (batchItem.TransitTime.IsNullOrEmpty() == false)
                    {
                        if (batchItem.TransitTimeOperation == ChangeOperation.Append)
                            item.TransitTime += batchItem.TransitTime;
                        else
                            item.TransitTime = batchItem.TransitTime;
                    }

                    #endregion

                }

                Validate();
                bsList.EndEdit();
                gvMain.RefreshData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "BatchItemFaily") + ex.Message);
            }
            finally
            {
                Enabled = true;
            }
        }

        #endregion

        #region Import

        private void Import()
        {
            #region Open File
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Title = "Choose File";
            openFileDialog.Filter = "Excel files (*.xls)|*.xls";
            DialogResult dlt = openFileDialog.ShowDialog();

            if (dlt != DialogResult.OK) return;

            #endregion

            string fileName = openFileDialog.FileName;
            if (string.IsNullOrEmpty(fileName) || File.Exists(fileName) == false) return;


            try
            {
                //导入
                theradID = LoadingServce.ShowLoadingForm();
                gvMain.ActiveFilterString = string.Empty;
                List<ClientBasePortList> basePorts = BingInputOceanItem(fileName);
                AfterImport(basePorts);
                //验证数据重复性
                ValidateExistData();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }

            try
            {
                //生成
                theradID = LoadingServce.ShowLoadingForm("Builer......");
                OceanPriceService.BuilerBaseItemForOceanID(_parentList.ID);
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }

        List<ClientBasePortList> BingInputOceanItem(string fileName)
        {
            DataSet dsExcel;
            try
            {
                if (IntPtr.Size == 8)
                {
                    dsExcel = BasePortImportHelper.X64ReadExcelToDataSet(fileName);
                }
                else
                {
                    dsExcel = BasePortImportHelper.ReadExcelToDataSet(fileName);
                }
                BasePortImportHelper.ValidateBasePortExcelColumn(dsExcel, _parentList.OceanUnits);
                DataTable dt = dsExcel.Tables[0];


                //最终需传到数据库的对象
                List<ClientBasePortList> basePorts = new List<ClientBasePortList>();

                //先把基本信息导入到客户端对象
                BasePortImportHelper.InputBaseInfoIntoBasePort(dt, basePorts, _parentList);

                if (basePorts.Count == 0) throw new ApplicationException("Data Not Find.");

                //	可以忽略逗号间空格的port名称，如：Los Angels, CA -> Los Angels,CA
                //	港口支持“/”号分隔。如：Los Angels,CA/Long Beach,CA，即解释为两条Base Port Rates

                //替换逗号
                BasePortImportHelper.ReplacePortComma(basePorts);

                //港口一转多
                BasePortImportHelper.TransformPortToMutil(basePorts);

                //ValidatePortNameCarrierTransportClause
                BasePortImportHelper.ValidatePortNameCarrierTransportClause(basePorts, GeographyService, OceanPriceUIDataHelper.Carriers, OceanPriceUIDataHelper.TransportClauses);

                //原来提出，当POD为空，自动默认为POD的值 和DELIVERY值相同的，功能没有了，强烈要求保留。(导入时)
                foreach (var item in basePorts)
                {
                    if (item.PODID.IsNullOrEmpty() && string.IsNullOrEmpty(item.PODName))
                    {
                        if (!item.PlaceOfDeliveryID.IsNullOrEmpty())
                        {
                            item.PODID = item.PlaceOfDeliveryID.Value;
                        }
                        item.PODName = item.PlaceOfDeliveryName;
                        item.OLDPODName = item.PODName;
                    }

                }

                #region 类真实数据验证
                foreach (var item in basePorts)
                {
                    string errorMessage = string.Empty;
                    if (item.ValidateHasRate() == false)
                    {
                        errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateRate_20GP"));
                    }

                    if (item.POLID.IsNullOrEmpty() == false && item.PODID.IsNullOrEmpty() == false && item.PODID == item.POLID)
                    {
                        errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidatePOLSamePOD"));
                    }
                    if (item.Comm.IsNullOrEmpty())
                    {
                        errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateComm"));
                    }
                    if (item.TransportClauseID.IsNullOrEmpty())
                    {
                        errorMessage = GetErrorInfo(errorMessage, "Term must input");
                    }
                    if (item.FromDate.HasValue && item.ToDate.HasValue && item.FromDate >= item.ToDate)
                    {
                        errorMessage = GetErrorInfo(errorMessage, NativeLanguageService.GetText(this, "ValidateFromDate"));
                    }


                    item.ErrorInfo += errorMessage;
                }
                #endregion

                basePorts = basePorts.OrderByDescending(o => o.HasError).ToList();

                #region  //验证唯一键定义 Account+CarrierID+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term
                bool itemCodeCommDifferent = false;
                ValidateHasExistItem(CurrentSource, basePorts, ref itemCodeCommDifferent);

                #endregion

                List<ClientBasePortList> canSaveData = basePorts.FindAll(b => b.ErrorInfo.IsNullOrEmpty());

                #region Save--一次性将所有的数据都传输过去，在服务端再去分批调用存储过程

                BasePortCollect oceanItemCollect = new BasePortCollect();


                oceanItemCollect.BasePortItem = OceanPriceTransformHelper.TransformC2S(canSaveData, _parentList.OceanUnits);
                foreach (var item in oceanItemCollect.BasePortItem)
                {
                    item.Comm = item.Comm.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol);
                }
                byte[] items = DataZipStream.CompressionObject(oceanItemCollect);
                ManyResult result = OceanPriceService.SaveOceanBasePortsZip(_parentList.ID, items, LocalData.UserInfo.LoginID);

                ////调用异步执行生成运价
                //try
                //{
                //    AsyncBuilerBaseItem async = new AsyncBuilerBaseItem();
                //    async.AsyncBuilerBaseItemForOceanID(_parentList.ID);
                //}
                //catch { }

                if (result != null && result.Items != null)
                {
                    for (int i = 0; i < result.Items.Count; i++)
                    {
                        canSaveData[i].ID = result.Items[i].GetValue<Guid>("ID");
                        canSaveData[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                        canSaveData[i].No = result.Items[i].GetValue<int>("No");
                        canSaveData[i].IsDirty = false;
                    }
                }

                #endregion

                return basePorts;
            }
            catch (Exception ex)
            {
                LoadingServce.CloseLoadingForm(theradID);
                XtraMessageBox.Show(NativeLanguageService.GetText(this, "ImportFailed") + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// 验证唯一键定义 Account+ POL+ VIA+ POD+ Delivery +Origin Arb+ Dest Arb+item code + Term +Comm+Carrier
        /// </summary>
        /// <param name="oldSource">oldSource</param>
        /// <param name="newItems">newItems</param>
        /// <param name="itemCodeCommDifferent"></param>
        private bool ValidateHasExistItem(List<ClientBasePortList> oldSource, IEnumerable<ClientBasePortList> newItems, ref bool itemCodeCommDifferent)
        {
            bool isExist = false;
            Dictionary<string, int?> existItem = new Dictionary<string, int?>();
            Dictionary<string, string> existItemCodeComm = new Dictionary<string, string>();
            List<ClientBasePortList> oldSouce = CurrentSource.FindAll(s => s.IsNew == false && s.IsDirty == false);

            #region 先Forech旧的数据,为了防止重复时错误先显示到旧数据中
            foreach (var item in oldSouce)
            {
                //ItemCode为空时不作品名判断
                if (item.ItemCode.IsNullOrEmpty() == false && item.ItemCode.Trim().Length > 0)
                {
                    if (existItemCodeComm.ContainsKey(item.ItemCode.ToUpper().Trim()) == false)
                        existItemCodeComm.Add(item.ItemCode.ToUpper().Trim(), item.Comm.ToUpper().Trim());
                }

                StringBuilder bulider = new StringBuilder();
                if (item.Account.IsNullOrEmpty() == false)
                {
                    bulider.Append(item.Account.ToUpper().Trim());
                }
                bulider.Append(item.POLID);
                bulider.Append(item.VIAID);
                bulider.Append(item.PODID);
                bulider.Append(item.PlaceOfDeliveryID);
                bulider.Append(item.OriginArb);
                bulider.Append(item.DestArb);
                bulider.Append(item.ItemCode);
                bulider.Append(item.TransportClauseID);
                bulider.Append(item.Comm);
                bulider.Append(item.CarrierID);

                if (existItem.ContainsKey(bulider.ToString()) == false)
                {
                    existItem.Add(bulider.ToString(), item.No);
                }
            }
            #endregion

            #region 验证新的数据
            foreach (var item in newItems)
            {
                //ItemCode为空时不作品名判断
                if (item.ItemCode.IsNullOrEmpty() == false && item.ItemCode.Trim().Length > 0 && item.Comm.IsNullOrEmpty() == false)
                {
                    if (existItemCodeComm.ContainsKey(item.ItemCode.Trim().ToUpper()) == false)
                        existItemCodeComm.Add(item.ItemCode.ToUpper().Trim(), item.Comm.Trim().ToUpper());
                    else
                    {
                        if (existItemCodeComm[item.ItemCode.Trim().ToUpper()].Trim().ToUpper() != item.Comm.Trim().ToUpper())
                        {
                            itemCodeCommDifferent = true;
                            item.ErrorInfo += string.Format(NativeLanguageService.GetText(this, "ValidateItemCodeDifferent"), item.ItemCode);
                        }
                    }
                }

                StringBuilder bulider = new StringBuilder();

                if (item.Account != null) bulider.Append(item.Account.ToUpper().Trim());

                bulider.Append(item.POLID);
                bulider.Append(item.VIAID);
                bulider.Append(item.PODID);
                bulider.Append(item.PlaceOfDeliveryID);
                bulider.Append(item.OriginArb);
                bulider.Append(item.DestArb);
                bulider.Append(item.ItemCode.IsNullOrEmpty() ? string.Empty : item.ItemCode.Trim().ToUpper());
                bulider.Append(item.TransportClauseID);
                bulider.Append(item.Comm);
                bulider.Append(item.CarrierID);


                if (bulider.ToString().IsNullOrEmpty()) continue;

                if (existItem.ContainsKey(bulider.ToString()))
                {
                    isExist = true;
                    item.ErrorInfo += string.Format(NativeLanguageService.GetText(this, "ValidateItemExist"), existItem[bulider.ToString()].HasValue ? existItem[bulider.ToString()].ToString() : string.Empty);
                }
                else
                    existItem.Add(bulider.ToString(), item.No);
            }
            #endregion
            return isExist;
        }

        #region after Import

        void AfterImport(List<ClientBasePortList> basePorts)
        {
            try
            {
                LoadingServce.CloseLoadingForm(theradID);

                if (basePorts == null || basePorts.Count == 0)
                {
                    return;
                }
                int records = basePorts.Count(b => b.ErrorInfo.IsNullOrEmpty());

                List<ClientBasePortList> source = CurrentSource;
                source.InsertRange(0, basePorts);
                bsList.DataSource = source;
                bsList.ResetBindings(false);
                gvMain.RefreshData();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), string.Format(NativeLanguageService.GetText(this, "ImportingSuccessfully"), records.ToString()));
            }
            catch { LocalCommonServices.ErrorTrace.SetErrorInfo(this, "Refresh Data failed."); }
        }

        #endregion

        #endregion

        #region Comm
        private void SearchComm()
        {
            if (CurrentRow == null) return;

            bsList.EndEdit();
            bsList.ResetCurrentItem();
            gvMain.CloseEditor();
            gvMain.RefreshData();
            Validate();

            OPSearchCommPart scf = Workitem.Items.AddNew<OPSearchCommPart>();
            scf.SetSource(OceanPriceUIDataHelper.Commoditys, CurrentRow.Comm);
            DialogResult dr = PartLoader.ShowDialog(scf, NativeLanguageService.GetText(this, "SearchCommPartTitel"), FormBorderStyle.Sizable);
            if (dr == DialogResult.OK)
            {
                CurrentRow.Comm = scf.CommString;
                bsList.EndEdit();
                bsList.ResetCurrentItem();
                gvMain.CloseEditor();
                gvMain.RefreshData();
                Validate();
            }
        }
        #endregion
        #endregion
    }

    /// <summary>
    /// 基本航线运价复制实体
    /// </summary>
    public class BasePortCopyIDToList
    {
        /// <summary>
        /// 原始ID集合
        /// </summary>
        public List<Guid> IDList { get; set; }
        /// <summary>
        /// 复制ID集合
        /// </summary>
        public List<Guid> CopyIDList { get; set; }
    }

}
