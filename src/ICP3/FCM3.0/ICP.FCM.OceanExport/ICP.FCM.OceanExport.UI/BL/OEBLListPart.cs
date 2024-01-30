using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Common.ServiceInterface;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI.BL
{
    [ToolboxItem(false)]
    public partial class OEBLListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanExportPrintHelper OceanExportPrintHelper { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [ServiceDependency]
        public IVerificationSheetService verificationSheetService { get; set; }
        /// <summary>
        /// 业务服务
        /// </summary>
        [ServiceDependency]
        public IOceanExportService oeBookingService { get; set; }




        [ServiceDependency]
        public IEDIClientService ediClientService
        {
            get;
            set;
        }

        #endregion

        #region Init

        public OEBLListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {

                if (orgSource != null) orgSource.Clear(); orgSource = null;
                if (Workitem != null) Workitem.Items.Remove(this);
            };
            //if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colRefNo.Caption = "业务号";
            colNo.Caption = "提单号";
            colSoNo.Caption = "订舱号";
            colCustomerName.Caption = "客户";
            colVesselVoyage.Caption = "船名航次";
            colPOLName.Caption = "装货港";
            colPODName.Caption = "卸货港";
            colPlaceOfDeliveryName.Caption = "交货地";
            colETD.Caption = "离港日";
            colIssueType.Caption = "类型";
            colSales.Caption = "揽货人";
            colCreateDate.Caption = "创建时间";
            colState.Caption = "状态";
            colContainerNos.Caption = "箱号";
            colIssueType.Caption = "签单类型";
            colETA.Caption = "到港日";
            colBookingerName.Caption = "订舱";
            colFiler.Caption = "文件";
            colReleaseType.Caption = "放单类型";
            colConsignee.Caption = "收货人";
            colNotifyParty.Caption = "通知人";
            colShipper.Caption = "发货人";
            colAgentOfCarrierName.Caption = "承运人";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            treeMain.ExpandAll();
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected OceanBLList CurrentRow
        {
            get { return Current as OceanBLList; }
        }
        List<OceanBLList> SelectedItems
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;

                List<OceanBLList> tagers = new List<OceanBLList>();
                foreach (TreeListNode item in treeMain.Selection)
                {
                    OceanBLList bl = treeMain.GetDataRecordByNode(item) as OceanBLList;
                    tagers.Add(bl);
                }
                return tagers;

            }
        }


        List<OceanBLList> orgSource = null;
        /// <summary>
        /// List OceanBLList,缓存一个原始数据源,在更变(ALL,MBL,HBL)视图时切换bsList的DataSource
        /// </summary>
        public override object DataSource
        {
            get
            {
                return orgSource;
            }
            set
            {
                treeMain.BeginUpdate();

                orgSource = value as List<OceanBLList>;
                if (orgSource == null || orgSource.Count == 0)
                {
                    bsList.DataSource = orgSource;
                    bsList.ResetBindings(false);
                    if (CurrentChanged != null) CurrentChanged(this, Current);
                }
                else
                {
                    SetListSourceByVisibleMode();
                }

                if (orgSource == null || orgSource.Count == 0) treeMain.IndicatorWidth = 15;
                else
                {
                    int count = orgSource.Count.ToString().Length - 1;
                    int width = 10 + count * 10;
                    treeMain.IndicatorWidth = 15 + width;
                }

                //treeMain.BestFitColumns();
                treeMain.ExpandAll();

                treeMain.EndUpdate();
            }
        }

        private void SetListSourceByVisibleMode()
        {
            if (_VisibleMode == VisibleMode.ALL)
                bsList.DataSource = orgSource;
            else if (orgSource != null)
            {
                if (_VisibleMode == VisibleMode.MBL)
                    bsList.DataSource = orgSource.FindAll(delegate(OceanBLList item) { return item.BLType == FCMBLType.MBL; });
                else
                    bsList.DataSource = orgSource.FindAll(delegate(OceanBLList item) { return item.BLType == FCMBLType.HBL; });
            }
            bsList.ResetBindings(false);

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<OceanBLList> list = this.DataSource as List<OceanBLList>;
                if (list == null) return;
                List<OceanBLList> newLists = items as List<OceanBLList>;

                foreach (var item in newLists)
                {
                    OceanBLList tager = list.Find(delegate(OceanBLList jItem) { return item.ID == jItem.ID; });
                    if (tager == null) continue;

                    Utility.CopyToValue(item, tager, typeof(OceanBLList));
                }
                bsList.ResetBindings(false);
            }
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public new event KeyEventHandler KeyDown;
        #endregion

        #region TreeView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        protected virtual void GvMainDoubleClick()
        {
            if (CurrentRow != null) Workitem.Commands[OEBLCommandConstants.Command_EditData].Execute();
        }

        private void treeMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) GvMainDoubleClick();
        }
        private void treeMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) GvMainDoubleClick();
            else if (e.KeyCode == Keys.F5)
            {
                if (this.KeyDown != null && treeMain.FocusedColumn != null && treeMain.FocusedNode != null)
                {
                    string text = treeMain.FocusedNode.GetDisplayText(treeMain.FocusedColumn);
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add(treeMain.FocusedColumn.FieldName, text);
                    this.KeyDown(keyValue, null);
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[OEBLCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void treeMain_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            OceanBLList listData = treeMain.GetDataRecordByNode(e.Node) as OceanBLList;
            if (listData == null) return;

            if (listData.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            //else if (listData.State == OEBLState.Checking)
            //{
            //    e.Appearance.ForeColor = OEBLColorConstant.CheckingColor;
            //    e.Appearance.Options.UseForeColor = true;
            //}
            //else if (listData.State == OEBLState.Checked)
            //{
            //    e.Appearance.ForeColor = OEBLColorConstant.CheckedColor;
            //    e.Appearance.Options.UseForeColor = true;
            //}
            //else if (listData.State == OEBLState.Release)
            //{
            //    e.Appearance.ForeColor = OEBLColorConstant.ReleaseColor;
            //    e.Appearance.Options.UseForeColor = true;
            //}

        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        private void treeMain_CustomDrawNodeIndicator(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = e.ObjectArgs as DevExpress.Utils.Drawing.IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }

        #endregion

        #region Workitem Common

        #region 增删

        [CommandHandler(OEBLCommandConstants.Command_AddMBL)]
        public void Command_AddMBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanMBLInfo newData = new OceanMBLInfo();
                //newData.ContactName = Workitem.State["ContractName"] == null ? "" : Workitem.State["ContractName"].ToString();
                //newData.ContactID = Guid.NewGuid();
                newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.NumberOfOriginal = 3;
                newData.VoyageShowType = VoyageShowType.All;
                newData.ReleaseType = FCMReleaseType.Original;
                newData.IssueType = IssueType.Normal;
                newData.IsValid = true;
                newData.WoodPacking = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                newData.State = OEBLState.Draft;
                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                newData.QuantityUnitID = normalDictionary.ID;
                newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                newData.WeightUnitID = normalDictionary.ID;
                newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                newData.MeasurementUnitID = normalDictionary.ID;
                newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                #endregion


                newData.MBLID = Guid.Empty;
                string titleNo = LocalData.IsEnglish ? "Add MBL" : "新增MBL";
                PartLoader.ShowEditPart<MBL.MBLEditPart>(Workitem, newData, titleNo, AfterMBLEditPartSaved);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_AddHBL)]
        public void Command_AddHBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanHBLInfo newData = new OceanHBLInfo();
                //newData.ContactName = Workitem.State["ContractName"] == null ? "" : Workitem.State["ContractName"].ToString();
                //newData.ContactID = Guid.NewGuid();
                newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.NumberOfOriginal = 3;
                newData.VoyageShowType = VoyageShowType.All;
                newData.ReleaseType = FCMReleaseType.Original;
                newData.IssueType = IssueType.Normal;
                newData.IsValid = true;
                newData.State = OEBLState.Draft;
                newData.WoodPacking = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                //normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                //newData.PaymentTermID = normalDictionary.ID;
                //newData.PaymentTermName = normalDictionary.EName;

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

                string titleNo = LocalData.IsEnglish ? "Add HBL" : "新增HBL";
                PartLoader.ShowEditPart<ICP.FCM.OceanExport.UI.HBL.HBLEditPart>(Workitem, newData, titleNo, AfterHBLEditPartSaved);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                if (CurrentRow.BLType == FCMBLType.MBL)
                {
                    OceanMBLInfo CloneData = oeService.GetOceanMBLInfo(CurrentRow.ID);

                    #region 需清空的数据
                    CloneData.ID = Guid.Empty;
                    CloneData.State = OEBLState.Draft;
                    CloneData.No = string.Empty;
                    CloneData.MBLID = Guid.Empty;
                    CloneData.HBLNos = string.Empty;
                    CloneData.ContainerDescription = string.Empty;
                    CloneData.CtnQtyInfo = string.Empty;
                    CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
                    CloneData.State = OEBLState.Draft;
                    #endregion

                    #region 设置默认值
                    DataDictionaryList normalDictionary = null;
                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                    CloneData.PaymentTermID = normalDictionary.ID;
                    CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                    CloneData.QuantityUnitID = normalDictionary.ID;
                    CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                    CloneData.WeightUnitID = normalDictionary.ID;
                    CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                    CloneData.MeasurementUnitID = normalDictionary.ID;
                    CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
                    #endregion

                    #region 列表的数据

                    CloneData.SONO = CurrentRow.SONO;
                    CloneData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                    CloneData.CarrierName = CurrentRow.CarrierName;
                    CloneData.SalesName = CurrentRow.SalesName;
                    CloneData.FilerName = CurrentRow.FilerName;
                    CloneData.BookingerName = CurrentRow.BookingerName;
                    CloneData.OverseasFilerName = CurrentRow.OverseasFilerName;
                    #endregion

                    string title = LocalData.IsEnglish ? "Copy MBL" : "复制MBL";
                    PartLoader.ShowEditPart<MBL.MBLEditPart>(Workitem, CloneData, title, AfterMBLEditPartSaved);
                }
                else
                {
                    OceanHBLInfo CloneData = oeService.GetOceanHBLInfo(CurrentRow.ID);
                    CloneData.CarrierName = CurrentRow.CarrierName;
                    #region 需清空的数据
                    CloneData.ID = Guid.Empty;
                    CloneData.AMSNo = string.Empty;
                    CloneData.ISFNo = string.Empty;
                    CloneData.State = OEBLState.Draft;
                    CloneData.No = string.Empty;
                    CloneData.ContainerDescription = string.Empty;
                    CloneData.CtnQtyInfo = string.Empty;
                    CloneData.Measurement = CloneData.Weight = CloneData.Quantity = 0;
                    #endregion

                    #region 设置默认值
                    DataDictionaryList normalDictionary = null;
                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                    CloneData.PaymentTermID = normalDictionary.ID;
                    CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                    CloneData.QuantityUnitID = normalDictionary.ID;
                    CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                    CloneData.WeightUnitID = normalDictionary.ID;
                    CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                    normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                    CloneData.MeasurementUnitID = normalDictionary.ID;
                    CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
                    #endregion

                    #region 列表的数据
                    CloneData.SONO = CurrentRow.SONO;
                    CloneData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                    CloneData.CarrierName = CurrentRow.CarrierName;
                    CloneData.SalesName = CurrentRow.SalesName;
                    CloneData.FilerName = CurrentRow.FilerName;
                    CloneData.BookingerName = CurrentRow.BookingerName;
                    CloneData.OverseasFilerName = CurrentRow.OverseasFilerName;
                    #endregion

                    string title = LocalData.IsEnglish ? "Copy HBL" : "复制HBL";
                    PartLoader.ShowEditPart<ICP.FCM.OceanExport.UI.HBL.HBLEditPart>(Workitem, CloneData, title, AfterHBLEditPartSaved);

                }

            }
        }


        [CommandHandler(OEBLCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;



                if (CurrentRow.BLType == FCMBLType.MBL)
                {

                    OceanMBLInfo editData = oeService.GetOceanMBLInfo(CurrentRow.ID);
                    #region 列表的数据
                    editData.SONO = CurrentRow.SONO;
                    editData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                    editData.CarrierName = CurrentRow.CarrierName;
                    editData.SalesName = CurrentRow.SalesName;
                    editData.FilerName = CurrentRow.FilerName;
                    editData.BookingerName = CurrentRow.BookingerName;
                    editData.OverseasFilerName = CurrentRow.OverseasFilerName;
                    #endregion
                    string title = LocalData.IsEnglish ? "Edit MBL " + CurrentRow.No : "编辑MBL " + CurrentRow.No;
                    PartLoader.ShowEditPart<MBL.MBLEditPart>(Workitem, editData, title, AfterMBLEditPartSaved, typeof(MBL.MBLEditPart).ToString() + CurrentRow.ID.ToString());
                }
                else
                {

                    OceanHBLInfo editData = oeService.GetOceanHBLInfo(CurrentRow.ID);
                    #region 列表的数据
                    editData.SONO = CurrentRow.SONO;
                    editData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                    editData.CarrierName = CurrentRow.CarrierName;
                    editData.SalesName = CurrentRow.SalesName;
                    editData.FilerName = CurrentRow.FilerName;
                    editData.BookingerName = CurrentRow.BookingerName;
                    editData.OverseasFilerName = CurrentRow.OverseasFilerName;
                    editData.ContainerNos = CurrentRow.ContainerNos;
                    editData.IsDirty = false;
                    #endregion
                    string title = LocalData.IsEnglish ? "Edit HBL " + CurrentRow.No : "编辑HBL " + CurrentRow.No;
                    PartLoader.ShowEditPart<ICP.FCM.OceanExport.UI.HBL.HBLEditPart>(Workitem, editData, title, AfterHBLEditPartSaved, typeof(ICP.FCM.OceanExport.UI.HBL.HBLEditPart).ToString() + CurrentRow.ID.ToString());
                }

            }
        }
        public void AfterMBLEditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            OceanBLList data = prams[0] as OceanBLList;
            List<OceanBLList> source = this.DataSource as List<OceanBLList>;
            if (source == null || source.Count == 0)
            {
                orgSource = new List<OceanBLList>();
                bsList.DataSource = orgSource;

                if (!bsList.Contains(data))
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
            }
            else
            {
                OceanBLList tager = source.Find(delegate(OceanBLList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    OceanBLList oceanBLList = FindExistItem(bsList.DataSource, data);
                    if (oceanBLList == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OceanBLList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        public void AfterHBLEditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            OceanHBLInfo data = prams[0] as OceanHBLInfo;
            List<OceanBLList> source = this.DataSource as List<OceanBLList>;
            if (source == null || source.Count == 0)
            {
                orgSource = new List<OceanBLList>();
                bsList.DataSource = orgSource;

                OceanBLList mbl = null;
                List<OceanBLList> bls = oeService.GetOceanBLListByIds(new Guid[] { data.MBLID });
                if (bls != null && bls.Count > 0) mbl = bls[0];

                if (mbl != null)
                {
                    if (!bsList.Contains(mbl))
                        bsList.Add(mbl);
                }

                if (!bsList.Contains(data))
                {
                    bsList.Add(data);
                }
                bsList.ResetBindings(false);
            }
            else
            {
                OceanBLList mbl = source.Find(delegate(OceanBLList item) { return item.ID == data.MBLID; });
                if (mbl == null)
                {
                    List<OceanBLList> bls = oeService.GetOceanBLListByIds(new Guid[] { data.MBLID });
                    if (bls != null && bls.Count > 0) mbl = bls[0];
                    if (mbl != null)
                    {
                        OceanBLList oceanBLList = FindExistItem(bsList.DataSource, mbl);
                        if (oceanBLList == null)
                        {
                            bsList.Insert(0, mbl);
                            bsList.ResetBindings(false);
                        }
                    }
                }
                else
                {
                    OceanBLList updateMbl = oeService.GetOceanBLListByIds(new Guid[] { data.MBLID })[0];

                    Utility.CopyToValue(updateMbl, mbl, typeof(OceanBLList));
                }

                OceanBLList tager = source.Find(delegate(OceanBLList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    OceanBLList oceanBLList = FindExistItem(bsList.DataSource, data);
                    if (!bsList.Contains(data))
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OceanBLList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        public OceanBLList FindExistItem(object datasource, OceanBLList oceanBLList)
        {
            List<OceanBLList> list = datasource as List<OceanBLList>;
            OceanBLList _oceanBLList = list.Find(delegate(OceanBLList item) { return item.ID == oceanBLList.ID; });
            return _oceanBLList;
        }

        [CommandHandler(OEBLCommandConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null || (CurrentRow.State != OEBLState.Draft && CurrentRow.State != OEBLState.Checking)) return;

            string No = CurrentRow.No;

            if (CurrentRow.ExistFees)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "UN Done" : "若要执行此操作,请先删除提单下的费用."
                               , LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            if (DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "Sure Delete BL " : "你真的要删除提单") + No + "?"
                               , LocalData.IsEnglish ? "Tip" : "提示"
                               , MessageBoxButtons.YesNo
                               , MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                if (CurrentRow.BLType == FCMBLType.MBL)
                {
                    oeService.RemoveOceanMBLInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                    bsList.RemoveCurrent();
                }
                else
                {
                    oeService.RemoveOceanHBLInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    List<OceanBLList> source = this.DataSource as List<OceanBLList>;
                    OceanBLList mbl = source.Find(delegate(OceanBLList item) { return item.ID == CurrentRow.MBLID; });
                    if (mbl != null)
                    {
                        OceanBLList existhbl = source.Find(delegate(OceanBLList item) { return item.BLType == FCMBLType.HBL && item.MBLID == mbl.ID && item.ID != CurrentRow.ID; });
                        if (existhbl == null) mbl.HBLCount = 0;
                        source.Remove(CurrentRow);
                        SetListSourceByVisibleMode();
                    }
                }

                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                   No + " " + (LocalData.IsEnglish ? "Delete Successfully" : "删除成功"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region 对单

        [CommandHandler(OEBLCommandConstants.Command_Check)]
        public void Command_Check(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.State == OEBLState.Checking) return;

            try
            {
                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == FCMBLType.MBL)
                    result = oeService.ChangeOceanMBLState(CurrentRow.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = oeService.ChangeOceanHBLState(CurrentRow.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                OceanBLList currentData = CurrentRow;
                currentData.State = OEBLState.Checking;
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                    CurrentRow.No + " " + (LocalData.IsEnglish ? "Begin Check." : "开始对单."));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(OEBLCommandConstants.Command_CompleteCheck)]
        public void Command_CompleteCheck(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == FCMBLType.MBL)
                    result = oeService.ChangeOceanMBLState(CurrentRow.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = oeService.ChangeOceanHBLState(CurrentRow.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                OceanBLList currentData = CurrentRow;
                currentData.State = OEBLState.Checked;
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                    CurrentRow.No + " " + (LocalData.IsEnglish ? "Check Done." : "完成对单."));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region Print

        [CommandHandler(OEBLCommandConstants.Command_PrintBL)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("OceanBLList", CurrentRow);
            string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print BL" : "打印提单") + ("-" + no);
            PartLoader.ShowEditPart<NewBLPrintPart>(Workitem, null, stateValues, title, null, null);
        }

        [CommandHandler(OEBLCommandConstants.Command_PrintProfit)]
        public void Command_PrintProfit(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            List<Guid> list = new List<Guid>();
            if (!Utility.GuidIsNullOrEmpty(CurrentRow.OceanBookingID))
            {
                list.Add(CurrentRow.OceanBookingID);

                List<OceanBookingList> oceanBookList = oeBookingService.GetOceanBookingListByIds(list.ToArray());

                if (oceanBookList != null && oceanBookList.Count > 0)
                {
                    OceanExportPrintHelper.PrintOEBookingProfit(oceanBookList[0]);
                }
            }

            //OceanExportPrintHelper.PrintOEBookingProfit();
        }




        [CommandHandler(OEBLCommandConstants.Command_PrintLoadCtn)]
        public void Command_PrintLoadCtn(object sender, EventArgs e)
        {
            //if (CurrentRow == null || CurrentRow.MBLID ==Guid.Empty) return;

            //OceanExportPrintHelper.PrintOELoadContainer(CurrentRow.MBLID);
        }
        [CommandHandler(OEBLCommandConstants.Command_PrintLoadGoods)]
        public void Command_PrintLoadGoods(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.MBLID == Guid.Empty) return;

            OceanExportPrintHelper.PrintOELoadGoods(CurrentRow);
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }

        #endregion

        #region LoadShip RefreshData ReplyAgent

        [CommandHandler(OEBLCommandConstants.Command_LoadShip)]
        public void Command_LoadShip(object sender, EventArgs e)
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.MBLID)) return;

            System.Text.StringBuilder strb = new StringBuilder();
            if (string.IsNullOrEmpty(CurrentRow.No))
                strb.Append("MBLNO");

            if (string.IsNullOrEmpty(CurrentRow.POLName))
                strb.Append(" POLName");

            if (strb.Length > 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }
            OceanMBLInfo mbl = oeService.GetOceanMBLInfo(CurrentRow.MBLID);

            if ((mbl.OEOperationType == FCMOperationType.BULK || mbl.OEOperationType == FCMOperationType.LCL)
                && string.IsNullOrEmpty(mbl.ContainerNos))
            {
                string message = LocalData.IsEnglish ? "LCL or BULK groceries in the case of box number can not confirm the shipment" : "拼箱或散杂货在没有箱号的情况下，不能进行确认装船";
                DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "The following information is incomplete" : "以下信息不完整: ") + strb.ToString());
                return;
            }



            #region 列表的数据
            mbl.SONO = CurrentRow.SONO;
            mbl.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
            mbl.CarrierName = CurrentRow.CarrierName;
            mbl.SalesName = CurrentRow.SalesName;
            mbl.FilerName = CurrentRow.FilerName;
            mbl.BookingerName = CurrentRow.BookingerName;
            mbl.OverseasFilerName = CurrentRow.OverseasFilerName;
            #endregion
            string title = LocalData.IsEnglish ? "Confirm LoadShip" : "确认装船";
            PartLoader.ShowEditPartInDialog<ICP.FCM.OceanExport.UI.MBL.LoadShipConfirm>(Workitem, mbl, title, AfterLoadShip);
        }

        /// <summary>
        /// 装船完成后
        /// </summary>
        void AfterLoadShip(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            Guid shippingOrderId = new Guid(prams[0].ToString());
            List<OceanBLList> source = this.DataSource as List<OceanBLList>;
            if (source == null || source.Count == 0) return;

            List<OceanBLList> needUpdateBLs = source.FindAll(delegate(OceanBLList item) { return item.ShippingOrderID == shippingOrderId; });
            if (needUpdateBLs == null || needUpdateBLs.Count == 0) return;

            List<Guid> needUpdateIds = new List<Guid>();
            foreach (var bl in needUpdateBLs) { needUpdateIds.Add(bl.ID); }
            List<OceanBLList> bls = oeService.GetOceanBLListByIds(needUpdateIds.ToArray());

            foreach (var bl in needUpdateBLs)
            {
                OceanBLList temp = bls.Find(delegate(OceanBLList item) { return item.ID == bl.ID; });
                if (temp != null)
                {
                    bl.State = temp.State;
                    bl.UpdateDate = temp.UpdateDate;
                    bl.ShippingOrderUpdateDate = temp.ShippingOrderUpdateDate;
                    bl.VesselVoyage = temp.VesselVoyage;
                }
            }
            SetListSourceByVisibleMode();
        }

        [CommandHandler(OEBLCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            try
            {
                List<OceanBLList> blList = DataSource as List<OceanBLList>;
                if (blList == null || blList.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in blList) ids.Add(item.ID);

                List<OceanBLList> list = oeService.GetOceanBLListByIds(ids.ToArray());
                this.DataSource = list;


            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(OEBLCommandConstants.Command_ReplyAgent)]
        public void Command_ReplyAgent(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                fcmCommonClientService.OpenAgentRequestPart(CurrentRow.OceanBookingID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, Workitem);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        #endregion

        #region 未完成

        [CommandHandler(OEBLCommandConstants.Command_ConfirmReleaseBL)]
        public void Command_ConfirmReleaseBL(object sender, EventArgs e)
        {
            if (CurrentRow == null || CurrentRow.State != OEBLState.Checked) return;

            try
            {
                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == FCMBLType.MBL)
                    result = oeService.ChangeOceanMBLState(CurrentRow.ID, OEBLState.Release, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = oeService.ChangeOceanHBLState(CurrentRow.ID, OEBLState.Release, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                OceanBLList currentData = CurrentRow;
                currentData.State = OEBLState.Release;
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                bsList.ResetCurrentItem();
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Release." : "已放单.");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        [CommandHandler(OEBLCommandConstants.Command_E_MBL)]
        public void Command_E_MBL(object sender, EventArgs e)
        {
            List<OceanBLList> mbls = SelectedItems;
            if (mbls == null || mbls.Count == 0)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }

            int i = (from d in mbls where d.BLType == FCMBLType.HBL select d).Count();
            if (i > 0)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Please select MBL BL." : "请选择MBL提单进行发送.");
                return;
            }


            OceanBLList hjMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
            OceanBLList zhMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
            OceanBLList fyMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
            if (hjMBL == null && zhMBL == null && fyMBL == null)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Shipowners are now only supports China Shipping , Hanjin,Hainan Pan Ocean Shipping Electronic batch." : "现在只支持船东是 [韩进]、[中海]、[海南泛洋] 的电子补料。");

                return;
            }

            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            #region 韩进
            if (hjMBL != null)
            {
                List<OceanBLList> hjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
                foreach (var item in hjMBLs)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.SONO);

                    operationNos.Add(item.No);

                    mblIds.Add(item.MBLID);
                }
                subjuect = LocalData.IsEnglish ? "HANJIN SHIPPING(" + mblNoBuilder.ToString() + ")" : "韩进电子补料(" + mblNoBuilder.ToString() + ")";
                key = "HANJIN_SI";
                tip = LocalData.IsEnglish ? "HANJIN SHIPPING" : "韩进";
            }
            #endregion

            #region 中海
            else if (zhMBL != null)
            {
                List<OceanBLList> zjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
                foreach (var item in zjMBLs)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.SONO);

                    operationNos.Add(item.No);
                    mblIds.Add(item.MBLID);
                }
                subjuect = LocalData.IsEnglish ? "CHINA SHIPPING(" + mblNoBuilder.ToString() + ")" : "中海电子补料(" + mblNoBuilder.ToString() + ")";
                key = "CSCL_SI";
                tip = LocalData.IsEnglish ? "CHINA SHIPPING" : "中海";
            }
            #endregion

            #region 泛洋
            else if (fyMBL != null)
            {
                List<OceanBLList> fyMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
                foreach (var item in fyMBLs)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.SONO);

                    operationNos.Add(item.No);
                    mblIds.Add(item.MBLID);
                }
                subjuect = LocalData.IsEnglish ? "HAINAN PAN SHIPPING(" + mblNoBuilder.ToString() + ")" : "海南泛洋电子补料(" + mblNoBuilder.ToString() + ")";
                key = "FYCW_SI";
                tip = LocalData.IsEnglish ? "HAINAN PAN SHIPPING" : "海南泛洋";
            }
            #endregion

            try
            {
                bool isSucc = false;

                if (mblIds.Count > 0)
                {
                    isSucc = ediClientService.SendEDI(
                        key,
                        EDILogType.SI,
                        CurrentRow.CompanyID,
                        subjuect,
                        mblIds.ToArray(),
                        operationNos.ToArray(),
                        ICP.Framework.CommonLibrary.Common.OperationType.OceanExport);
                }

                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Send failed" : "发送失败!" + System.Environment.NewLine + ex.Message);
            }
        }
        [CommandHandler(OEBLCommandConstants.Command_ISF)]
        public void Command_ISF(object sender, EventArgs e)
        {

        }
        [CommandHandler(OEBLCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            OpenBill(CurrentRow);
        }

        public void OpenBill(OceanBLList oceanBLList)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (oceanBLList == null) return;
                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(oceanBLList.OceanBookingID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = oceanBLList.ID;
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }

        #endregion

        #region Visible Mode

        VisibleMode _VisibleMode = VisibleMode.ALL;

        [CommandHandler(OEBLCommandConstants.Command_VisibleALL)]
        public void Command_VisibleALL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.ALL) return;
            _VisibleMode = VisibleMode.ALL;
            SetListSourceByVisibleMode();

        }
        [CommandHandler(OEBLCommandConstants.Command_VisibleMBL)]
        public void Command_VisibleMBL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.MBL) return;
            _VisibleMode = VisibleMode.MBL;
            SetListSourceByVisibleMode();

        }
        [CommandHandler(OEBLCommandConstants.Command_VisibleHBL)]
        public void Command_VisibleHBL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.HBL) return;
            _VisibleMode = VisibleMode.HBL;
            SetListSourceByVisibleMode();
        }

        #endregion

        #region Split Merge

        [CommandHandler(OEBLCommandConstants.Command_SplitBL)]
        public void Command_SplitBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.HBLCount > 0) return;

                OESplitBLPart part = this.Workitem.Items.AddNew<OESplitBLPart>();
                part.Saved += delegate(object[] prams)
                {
                    List<Guid> ids = prams[0] as List<Guid>;
                    if (ids == null || ids.Count == 0) return;
                    List<OceanBLList> newList = oeService.GetOceanBLListByIds(ids.ToArray());

                    List<OceanBLList> source = this.DataSource as List<OceanBLList>;

                    List<OceanBLList> needRemove = source.FindAll(delegate(OceanBLList item) { return ids.Contains(item.ID); });
                    foreach (var item in needRemove) { source.Remove(item); }
                    source.InsertRange(0, newList);
                    bsList.DataSource = source;
                    bsList.ResetBindings(false);
                };

                part.DataSource = CurrentRow;
                IWorkspace mainWorkspace = Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Split BL" : "分单";
                mainWorkspace.Show(part, smartPartInfo);
            }
        }

        [CommandHandler(OEBLCommandConstants.Command_Merge)]
        public void Command_Merge(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<OceanBLList> selecteds = SelectedItems;
                if (selecteds == null) return;

                #region 验证
                if (selecteds.Count <= 1)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择两个以上的提单.", LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FCMBLType bltype = selecteds[0].BLType;
                Guid oceanBookingId = selecteds[0].OceanBookingID;

                Guid mblID = Guid.Empty;
                if (bltype == FCMBLType.HBL) mblID = selecteds[0].MBLID;

                foreach (var item in selecteds)
                {
                    //IF 选择的提单类型不同
                    if (item.BLType != bltype)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择两票以上的MBL或HBL"
                                      , LocalData.IsEnglish ? "Tip" : "提示"
                                      , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    //IF 选择的提单业务不同
                    if (item.OceanBookingID != oceanBookingId)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择相同业务下的提单"
                                      , LocalData.IsEnglish ? "Tip" : "提示"
                                      , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                    if (mblID != Guid.Empty)
                    {
                        //IF 选择的HBL挂的MBL不同
                        if (item.MBLID != mblID)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "请选择相同MBL下的HBL"
                                          , LocalData.IsEnglish ? "Tip" : "提示"
                                          , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    //IF 选择的提单.状态=对单完成 或 完成提示：“提单[XXXXXXXXX]已经对单完成，所以不能进行合并。
                    if (item.State == OEBLState.Checked)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "BL:" : "提单:") + item.No + (LocalData.IsEnglish ? " has been checked,can't merge" : " 已对单完成.不能合并.")
                                        , LocalData.IsEnglish ? "Tip" : "提示"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    //IF 选择的提单.状态=对单放单：“提单[XXXXXXXXX]已经放单，所以不能进行合并。
                    if (item.State == OEBLState.Release)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "BL:" : "提单:") + item.No + (LocalData.IsEnglish ? " has been Released,can't merge" : " 已放单.不能合并.")
                                        , LocalData.IsEnglish ? "Tip" : "提示"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                }

                if (selecteds[0].BLType == FCMBLType.HBL)
                {

                }

                #endregion

                OEMergeBLPart part = this.Workitem.Items.AddNew<OEMergeBLPart>();
                part.Saved += delegate(object[] prams)
                {
                    SingleResult result = prams[0] as SingleResult;
                    List<Guid> ids = prams[1] as List<Guid>;

                    List<OceanBLList> blLists = this.bsList.DataSource as List<OceanBLList>;
                    List<OceanBLList> needRemove = blLists.FindAll(delegate(OceanBLList item) { return ids.Contains(item.ID); });
                    if (needRemove != null && needRemove.Count > 0)
                    {
                        foreach (var item in needRemove) { blLists.Remove(item); }
                    }

                    OceanBLList needRefreshData = blLists.Find(delegate(OceanBLList item) { return item.ID == result.GetValue<Guid>("ID"); });
                    needRefreshData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    needRefreshData.ContainerNos = result.GetValue<string>("ContainerNos");

                    bsList.DataSource = blLists;
                    bsList.ResetBindings(false);
                };
                part.DataSource = selecteds;
                PartLoader.ShowDialog(part, LocalData.IsEnglish ? "Merge BL" : "合并提单");
            }
        }

        #endregion

        #region 核销单

        [CommandHandler(OEBLCommandConstants.Command_VerifiSheet)]
        public void Command_VerifiSheet(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.IsNew)
                {
                    return;
                }

                string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
                string title = LocalData.IsEnglish ? "Verifi.Sheet" : "核销单" + ("-" + no);
                object[] data = new object[2];
                data[0] = CurrentRow.OceanBookingID;
                data[1] = CurrentRow.RefNo;
                PartLoader.ShowEditPart<VerifiSheetEditPart>(Workitem,
                    data,
                    null,
                    title,
                    null,
                    OEBLCommandConstants.Command_VerifiSheet + CurrentRow.ID.ToString());
            }
        }
        #endregion
        #region 装箱
        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OEBLCommandConstants.Command_LoadContainer)]
        public void Command_LoadContainer(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.IsNew)
                {
                    return;
                }

                string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
                string title = LocalData.IsEnglish ? "Load Container" : "装箱" + ("-" + no);
                OceanBookingInfo bookingInfo = oeService.GetOceanBookingInfo(CurrentRow.OceanBookingID);
                PartLoader.ShowEditPart<ICP.FCM.OceanExport.UI.Container.LoadContainerPart>(Workitem, bookingInfo, null, title, null,
                    OEBLCommandConstants.Command_LoadContainer + CurrentRow.ID.ToString());
            }
        }

        #endregion

        #endregion
    }

    enum VisibleMode
    {
        ALL, MBL, HBL
    }
}
