using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using ICP.FAM.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.UI.BL
{
    [ToolboxItem(false)]
    public partial class AEBLListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAirExportService oeService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        //[Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        //public ICPCommUIHelperICPCommUIHelper{ get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        #endregion

        #region Init

        public AEBLListPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            colState.Caption = "状态";
            colRefNo.Caption = "业务号";
            colNo.Caption = "提单号";
            colFilightNo.Caption = "航班号";
            colCustomerName.Caption = "客户";
            colDeparture.Caption = "起运港";
            colDetination.Caption = "目的港";
            colPlaceOfDeliveryName.Caption = "交货地";
            colETD.Caption = "起航日";
            colIssueType.Caption = "类型";
            colSales.Caption = "揽货人";
            colCreateDate.Caption = "创建时间";
            colIssueType.Caption = "关单";
            colETA.Caption = "到达日";
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
        protected AirBLList CurrentRow
        {
            get { return Current as AirBLList; }
        }
        List<AirBLList> SelectedItems
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;

                List<AirBLList> tagers = new List<AirBLList>();
                foreach (TreeListNode item in treeMain.Selection)
                {
                    AirBLList bl = treeMain.GetDataRecordByNode(item) as AirBLList;
                    tagers.Add(bl);
                }
                return tagers;

            }
        }


        List<AirBLList> orgSource = null;
        /// <summary>
        /// List AirBLList,缓存一个原始数据源,在更变(ALL,MBL,HBL)视图时切换bsList的DataSource
        /// </summary>
        public override object DataSource
        {
            get
            {
                return orgSource;
            }
            set
            {
                orgSource = value as List<AirBLList>;
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

                treeMain.BestFitColumns();
                treeMain.ExpandAll();

            }
        }

        private void SetListSourceByVisibleMode()
        {
            if (_VisibleMode == VisibleMode.ALL)
                bsList.DataSource = orgSource;
            else if (orgSource != null)
            {
                if (_VisibleMode == VisibleMode.MBL)
                    bsList.DataSource = orgSource.FindAll(delegate(AirBLList item) { return item.BLType == BLType.MAWB; });
                else
                    bsList.DataSource = orgSource.FindAll(delegate(AirBLList item) { return item.BLType == BLType.HAWB; });
            }
            bsList.ResetBindings(false);

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override void Refresh(object items)
        {
            List<AirBLList> list = this.DataSource as List<AirBLList>;
            if (list == null) return;
            List<AirBLList> newLists = items as List<AirBLList>;

            foreach (var item in newLists)
            {
                AirBLList tager = list.Find(delegate(AirBLList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(item, tager, typeof(AirBLList));
            }
            bsList.ResetBindings(false);
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
            if (CurrentRow != null) Workitem.Commands[AEBLCommandConstants.Command_EditData].Execute();
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
                Workitem.Commands[AEBLCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void treeMain_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            AirBLList listData = treeMain.GetDataRecordByNode(e.Node) as AirBLList;
            if (listData == null) return;

            //if (listData.IsValid == false)
            //{
            //    ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            //}
            if (listData.State == AEBLState.Checking)
            {
                e.Appearance.ForeColor = AEBLColorConstant.CheckingColor;
                e.Appearance.Options.UseForeColor = true;
            }
            else if (listData.State == AEBLState.Checked)
            {
                e.Appearance.ForeColor = AEBLColorConstant.CheckedColor;
                e.Appearance.Options.UseForeColor = true;
            }
            else if (listData.State == AEBLState.Release)
            {
                e.Appearance.ForeColor = AEBLColorConstant.ReleaseColor;
                e.Appearance.Options.UseForeColor = true;
            }

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

        [CommandHandler(AEBLCommandConstants.Command_AddMBL)]
        public void Command_AddMBL(object sender, EventArgs e)
        {
            AirMBLInfo newData = new AirMBLInfo();
            //newData.ContactName = Workitem.State["ContactName"] == null ? "" : Workitem.State["ContactName"].ToString();
            //newData.ContactID = Guid.NewGuid();
            newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.NumberOfOriginal = 3;
            newData.ReleaseType = FCMReleaseType.Original;
            newData.IsValid = true;
            newData.State = AEBLState.Draft;
            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            //normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            //newData.WeightUnitID = normalDictionary.ID;
            //newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            newData.DeclaredValueForCarriage = "N.V.D";
            newData.DeclaredValueForCustoms = "N.C.V";
            newData.ChargeableWeightUnitIsKGS = true;
            newData.IATAChargeableWeightUnitIsKGS = true;

            #endregion

            newData.MBLID = Guid.Empty;
            string titleNo = LocalData.IsEnglish ? "Add MAWB" : "新增MAWB";
            PartLoader.ShowEditPart<MBL.MAWBEditPart>(Workitem, newData, titleNo, AfterMBLEditPartSaved);
        }

        [CommandHandler(AEBLCommandConstants.Command_AddHBL)]
        public void Command_AddHBL(object sender, EventArgs e)
        {
            AirHBLInfo newData = new AirHBLInfo();
            //newData.ContactName = Workitem.State["ContactName"] == null ? "" : Workitem.State["ContactName"].ToString();
            //newData.ContactID = Guid.NewGuid();
            newData.IssueByID = newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.IssueByName = newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.Now;
            newData.NumberOfOriginal = 3;
            newData.ReleaseType = FCMReleaseType.Original;
            newData.IsValid = true;
            newData.State = AEBLState.Draft;
            #region 设置默认值
            DataDictionaryList normalDictionary = null;
            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            newData.DeclaredValueForCarriage = "N.V.D";
            newData.DeclaredValueForCustoms = "N.C.V";
            newData.ChargeableWeightUnitIsKGS = true;

            #endregion

            string titleNo = LocalData.IsEnglish ? "Add HAWB" : "新增HAWB";
            PartLoader.ShowEditPart<ICP.FCM.AirExport.UI.HBL.HAWBEditPart>(Workitem, newData, titleNo, AfterHBLEditPartSaved);
        }

        [CommandHandler(AEBLCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            if (CurrentRow.BLType == BLType.MAWB)
            {
                AirMBLInfo CloneData = oeService.GetAirMBLInfo(CurrentRow.ID);

                #region 需清空的数据
                CloneData.ID = Guid.Empty;
                CloneData.State = AEBLState.Draft;
                CloneData.No = string.Empty;
                CloneData.MBLID = Guid.Empty;
                CloneData.HAWBNos = string.Empty;

                CloneData.Measurement = CloneData.GrossKGS = CloneData.ChargeKGS = CloneData.Quantity = 0;
                CloneData.State = AEBLState.Draft;
                #endregion

                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                CloneData.PaymentTermID = normalDictionary.ID;
                CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                CloneData.QuantityUnitID = normalDictionary.ID;
                CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                //normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
                //CloneData.WeightUnitID = normalDictionary.ID;
                //CloneData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                CloneData.MeasurementUnitID = normalDictionary.ID;
                CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                CloneData.DeclaredValueForCarriage = "N.V.D";
                CloneData.DeclaredValueForCustoms = "N.C.V";
                CloneData.ChargeableWeightUnitIsKGS = true;
                CloneData.IATAChargeableWeightUnitIsKGS = true;

                #endregion

                #region 列表的数据

                //CloneData.FilightNo = CurrentRow.FilightNo;
                //CloneData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                //CloneData.AirCompanyName = CurrentRow.AirCompanyName;
                CloneData.CustomerName = CurrentRow.CustomerName;
                CloneData.SalesName = CurrentRow.SalesName;
                CloneData.FilerName = CurrentRow.FilerName;
                CloneData.BookingerName = CurrentRow.BookingerName;
                #endregion

                string title = LocalData.IsEnglish ? "Copy MAWB" : "复制MAWB ";
                PartLoader.ShowEditPart<MBL.MAWBEditPart>(Workitem, CloneData, title, AfterMBLEditPartSaved);
            }
            else
            {
                AirHBLInfo CloneData = oeService.GetAirHBLInfo(CurrentRow.ID);
                CloneData.AirCompanyName = CurrentRow.AirCompanyName;
                #region 需清空的数据

                CloneData.ID = Guid.Empty;
                CloneData.State = AEBLState.Draft;
                CloneData.No = string.Empty;
                CloneData.MBLID = Guid.Empty;
                CloneData.MBLNo = string.Empty;

                CloneData.Measurement = CloneData.GrossKGS = CloneData.ChargeKGS = CloneData.Quantity = 0;
                CloneData.State = AEBLState.Draft;
                #endregion

                #region 设置默认值
                DataDictionaryList normalDictionary = null;
                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.PaymentTerm);
                CloneData.PaymentTermID = normalDictionary.ID;
                CloneData.PaymentTermName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
                CloneData.QuantityUnitID = normalDictionary.ID;
                CloneData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;


                normalDictionary = ICPCommUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
                CloneData.MeasurementUnitID = normalDictionary.ID;
                CloneData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

                CloneData.DeclaredValueForCarriage = "N.V.D";
                CloneData.DeclaredValueForCustoms = "N.C.V";
                CloneData.ChargeableWeightUnitIsKGS = true;

                #endregion

                #region 列表的数据
                //CloneData.FilightNo = CurrentRow.FilightNo;
                //CloneData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                //CloneData.AirCompanyName = CurrentRow.AirCompanyName;
                CloneData.CustomerName = CurrentRow.CustomerName;
                CloneData.SalesName = CurrentRow.SalesName;
                CloneData.FilerName = CurrentRow.FilerName;
                CloneData.BookingerName = CurrentRow.BookingerName;
                #endregion

                string title = LocalData.IsEnglish ? "Copy HAWB" : "复制HAWB";
                PartLoader.ShowEditPart<ICP.FCM.AirExport.UI.HBL.HAWBEditPart>(Workitem, CloneData, title, AfterHBLEditPartSaved);

            }

        }

        [CommandHandler(AEBLCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            if (CurrentRow.BLType == BLType.MAWB)
            {
                AirMBLInfo editData = oeService.GetAirMBLInfo(CurrentRow.ID);
                #region 列表的数据
                //editData.FilightNo = CurrentRow.FilightNo;
                //editData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                //editData.AirCompanyName = CurrentRow.AirCompanyName;
                editData.CustomerName = CurrentRow.CustomerName;
                editData.SalesName = CurrentRow.SalesName;
                editData.FilerName = CurrentRow.FilerName;
                editData.BookingerName = CurrentRow.BookingerName;

                #endregion

                string title = LocalData.IsEnglish ? "Edit MAWB " + CurrentRow.No : "编辑MAWB " + CurrentRow.No;
                PartLoader.ShowEditPart<MBL.MAWBEditPart>(Workitem, editData, title, AfterMBLEditPartSaved, typeof(MBL.MAWBEditPart).ToString() + CurrentRow.ID.ToString());
            }
            else
            {
                AirHBLInfo editData = oeService.GetAirHBLInfo(CurrentRow.ID);
                #region 列表的数据
                //editData.FilightNo = CurrentRow.FilightNo;
                //editData.AgentOfCarrierName = CurrentRow.AgentOfCarrierName;
                //editData.AirCompanyName = CurrentRow.AirCompanyName;
                editData.CustomerName = CurrentRow.CustomerName;
                editData.SalesName = CurrentRow.SalesName;
                editData.FilerName = CurrentRow.FilerName;
                editData.BookingerName = CurrentRow.BookingerName;
                #endregion
                string title = LocalData.IsEnglish ? "Edit HAWB " + CurrentRow.No : "编辑HAWB " + CurrentRow.No;
                PartLoader.ShowEditPart<ICP.FCM.AirExport.UI.HBL.HAWBEditPart>(Workitem, editData, title, AfterHBLEditPartSaved, typeof(ICP.FCM.AirExport.UI.HBL.HAWBEditPart).ToString() + CurrentRow.ID.ToString());
            }
        }

        void AfterMBLEditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            AirBLList data = prams[0] as AirBLList;
            List<AirBLList> source = this.DataSource as List<AirBLList>;
            if (source == null || source.Count == 0)
            {
                orgSource = new List<AirBLList>();
                bsList.DataSource = orgSource;
                bsList.Insert(0, data);
                bsList.ResetBindings(false);
            }
            else
            {
                AirBLList tager = source.Find(delegate(AirBLList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(AirBLList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        void AfterHBLEditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            AirHBLInfo data = prams[0] as AirHBLInfo;
            List<AirBLList> source = this.DataSource as List<AirBLList>;
            if (source == null || source.Count == 0)
            {
                orgSource = new List<AirBLList>();
                bsList.DataSource = orgSource;

                AirBLList mbl = null;
                List<AirBLList> bls = oeService.GetAirBLListByIDs(new Guid[] { data.MBLID });
                if (bls != null && bls.Count > 0) mbl = bls[0];

                if (mbl != null) bsList.Add(mbl);

                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                AirBLList mbl = source.Find(delegate(AirBLList item) { return item.ID == data.MBLID; });
                if (mbl == null)
                {
                    List<AirBLList> bls = oeService.GetAirBLListByIDs(new Guid[] { data.MBLID });
                    if (bls != null && bls.Count > 0) mbl = bls[0];
                    if (mbl != null) bsList.Insert(0, mbl);
                }
                else
                {
                    AirBLList updateMbl = oeService.GetAirBLListByIDs(new Guid[] { data.MBLID })[0];

                    Utility.CopyToValue(updateMbl, mbl, typeof(AirBLList));
                }

                AirBLList tager = source.Find(delegate(AirBLList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(AirBLList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        [CommandHandler(AEBLCommandConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null || (CurrentRow.State != AEBLState.Draft && CurrentRow.State != AEBLState.Checking)) return;

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
                if (CurrentRow.BLType == BLType.MAWB)
                {
                    oeService.RemoveAirMBLInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                    bsList.RemoveCurrent();
                }
                else
                {
                    oeService.RemoveAirHBLInfo(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    List<AirBLList> source = this.DataSource as List<AirBLList>;
                    AirBLList mbl = source.Find(delegate(AirBLList item) { return item.ID == CurrentRow.MBLID; });
                    if (mbl != null)
                    {
                        AirBLList existhbl = source.Find(delegate(AirBLList item) { return item.BLType == BLType.HAWB && item.MBLID == mbl.ID && item.ID != CurrentRow.ID; });
                        if (existhbl == null) mbl.HBLCount = 0;
                        source.Remove(CurrentRow);
                        SetListSourceByVisibleMode();
                    }
                }

                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
                   No + " " + (LocalData.IsEnglish ? "Delete Successfully" : "删除成功!"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region 对单

        //[CommandHandler(AEBLCommandConstants.Command_Check)]
        //public void Command_Check(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null || CurrentRow.State == BLState.Checking) return;

        //    try
        //    {
        //        ICP.Framework.CommonLibrary.Common.SingleResult result;
        //        if (CurrentRow.BLType == BLType.MAWB)
        //            result = oeService.ChangeAirMBLState(CurrentRow.ID, BLState.Checking, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
        //        else
        //            result = oeService.ChangeAirHBLState(CurrentRow.ID, BLState.Checking, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

        //        AirBLList currentData = CurrentRow;
        //        currentData.State = BLState.Checking;
        //        currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

        //        bsList.ResetCurrentItem();
        //        bsMainList_PositionChanged(null, null);
        //        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),
        //            CurrentRow.No + " " + (LocalData.IsEnglish ? "Begin Check." : "开始对单."));
        //    }
        //    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        //}

        [CommandHandler(AEBLCommandConstants.Command_CompleteCheck)]
        public void Command_CompleteCheck(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                ICP.Framework.CommonLibrary.Common.SingleResult result;
                if (CurrentRow.BLType == BLType.MAWB)
                    result = oeService.ChangeAirMBLState(CurrentRow.ID, AEBLState.Checked, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                else
                    result = oeService.ChangeAirHBLState(CurrentRow.ID, AEBLState.Checked, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                AirBLList currentData = CurrentRow;
                currentData.State = AEBLState.Checked;
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

        [CommandHandler(AEBLCommandConstants.Command_PrintBL)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("AirBLList", CurrentRow);
            string no = CurrentRow.No.Length <= 4 ? CurrentRow.No : CurrentRow.No.Substring(CurrentRow.No.Length - 4, 4);
            string title = (LocalData.IsEnglish ? "Print BL" : "打印提单") + ("-" + no);
            PartLoader.ShowEditPart<NewBLPrintPart>(Workitem, null, stateValues, title, null, null);
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

        [CommandHandler(AEBLCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            try
            {
                List<AirBLList> blList = DataSource as List<AirBLList>;
                if (blList == null || blList.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in blList) ids.Add(item.ID);

                List<AirBLList> list = oeService.GetAirBLListByIDs(ids.ToArray());
                this.DataSource = list;


            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        [CommandHandler(AEBLCommandConstants.Command_ReplyAgent)]
        public void Command_ReplyAgent(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                fcmCommonClientService.OpenAgentRequestPart(CurrentRow.AirBookingID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport, Workitem);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        #endregion

        #region 未完成

        [CommandHandler(AEBLCommandConstants.Command_ConfirmReleaseBL)]
        public void Command_ConfirmReleaseBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null || CurrentRow.State != AEBLState.Checked) return;

                try
                {
                    ICP.Framework.CommonLibrary.Common.SingleResult result;
                    if (CurrentRow.BLType == BLType.MAWB)
                        result = oeService.ChangeAirMBLState(CurrentRow.ID, AEBLState.Release, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                    else
                        result = oeService.ChangeAirHBLState(CurrentRow.ID, AEBLState.Release, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    AirBLList currentData = CurrentRow;
                    currentData.State = AEBLState.Release;
                    currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    bsList.ResetCurrentItem();
                    bsMainList_PositionChanged(null, null);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Release." : "已放单.");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        [CommandHandler(AEBLCommandConstants.Command_E_MBL)]
        public void Command_E_MBL(object sender, EventArgs e)
        {

        }
        [CommandHandler(AEBLCommandConstants.Command_ISF)]
        public void Command_ISF(object sender, EventArgs e)
        {

        }
        [CommandHandler(AEBLCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(CurrentRow.AirBookingID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = CurrentRow.ID;
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

        [CommandHandler(AEBLCommandConstants.Command_VisibleALL)]
        public void Command_VisibleALL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.ALL) return;
            _VisibleMode = VisibleMode.ALL;
            SetListSourceByVisibleMode();

        }
        [CommandHandler(AEBLCommandConstants.Command_VisibleMBL)]
        public void Command_VisibleMBL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.MBL) return;
            _VisibleMode = VisibleMode.MBL;
            SetListSourceByVisibleMode();

        }
        [CommandHandler(AEBLCommandConstants.Command_VisibleHBL)]
        public void Command_VisibleHBL(object sender, EventArgs e)
        {
            if (_VisibleMode == VisibleMode.HBL) return;
            _VisibleMode = VisibleMode.HBL;
            SetListSourceByVisibleMode();
        }

        #endregion

        #region 核销单

        [CommandHandler(AEBLCommandConstants.Command_VerifiSheet)]
        public void AECommand_VerifiSheet(object sender, EventArgs e)
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
                data[0] = CurrentRow.AirBookingID;
                data[1] = CurrentRow.RefNo;
                PartLoader.ShowEditPart<VerifiSheetEditPart>(Workitem,
                    data,
                    null,
                    title,
                    null,
                    AEBLCommandConstants.Command_VerifiSheet + CurrentRow.ID.ToString());
            }
        }

        #endregion
    }

    enum VisibleMode
    {
        ALL, MBL, HBL
    }
}
