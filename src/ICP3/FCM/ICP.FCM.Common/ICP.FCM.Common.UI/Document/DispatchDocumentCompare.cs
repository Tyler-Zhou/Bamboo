using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.UI.Common.Parts;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.Common.UI.Document
{
    //joe 2013-06-14 init
    /// <summary>
    /// 分发文件签收时比较页面基类
    /// </summary>
    [SmartPart]
    public partial class DispatchDocumentCompareBase : BaseEditPart, IDisposable
    {

        /// <summary>
        /// 工作服务
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        /// <summary>
        /// FCM公共服务
        /// </summary>
        [ServiceDependency]
        public IFCMCommonService FCMCommonService { get; set; }

        /// <summary>
        /// 业务代理服务接口
        /// </summary>
        [ServiceDependency]
        public IOperationAgentService OperationAgentService { get; set; }


        /// <summary>
        /// 账单服务
        /// </summary>
        [ServiceDependency]
        public IFinanceService finService { get; set; }


        /// <summary>
        /// 海出服务
        /// </summary>
        [ServiceDependency]
        public IOceanExportService oeServicer { get; set; }



        [ServiceDependency]
        public IOceanImportService oiService { get; set; }


        /// <summary>
        /// 文件列表比较控件
        /// </summary>
        public UCDocumentDispatchPart ucDocumentDispatchPart1;
        public UCDocumentHistoryPart ucDocumentHistoryPart;

        /// <summary>
        /// 海出操作ID
        /// </summary>
        public Guid NewOperationID { get; set; }

        /// <summary>
        /// 海进操作ID
        /// </summary>
        public Guid OldOperationID { get; set; }

        /// <summary>
        /// 分文件比较类。
        /// </summary>
        public DispatchCompareObject dispatchCompareObject;

        /// <summary>
        /// 当前比较的账单列表
        /// </summary>
        protected List<Fee> CurrBill = new List<Fee>();


        /// <summary>
        /// 当前比较的业务信息修改列表
        /// </summary>
        protected List<BusinessUpdateField> CurrBusinessUpdate = new List<BusinessUpdateField>();

        /// <summary>
        /// 分发签收文档比较类型
        /// </summary>
        public DipatchCompareType CurrDipatchCompareType = DipatchCompareType.OIAccept;


        /// <summary>
        /// 签收当前行数据
        /// </summary>
        public OceanBusinessDownLoadList OceanBusinessDownLoadList { get; set; }

        protected OceanBusinessInfo _businessInfo = null;

        /// <summary>
        /// 分提单是否修改
        /// </summary>
        protected bool isHblChange = false;
        /// <summary>
        /// 箱信息是否修改
        /// </summary>
        protected bool isContainerChange = false;

        /// <summary>
        /// 账单是否有修改
        /// </summary>
        protected bool isBillChange = false;

        public SimpleBusinnessInfo CurrentSimpleBusinnessInfo = null;


        List<SolutionCurrencyList> _CurrencyList = null;


        //delegate void InitalDelegate();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DispatchDocumentCompareBase()
        {
            InitializeComponent();


            //initDataTableColumns();

            this.Disposed += delegate
            {
                profitComparePart1 = null;
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(this.deckWsDocument);
                    Workitem.Items.Remove(this);
                }
            };
            dispatchCompareObject = new DispatchCompareObject();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearList()
        {
            CurrBill.Clear();
            CurrBusinessUpdate.Clear();
        }


        public void BindData()
        {


            ClearList();
            // InitData(OEOperationID, OIOperationID);
            InnerInitData();

            bsBusiness.DataSource = CurrBusinessUpdate;
            bsBills.DataSource = CurrBill.FindAll(delegate(Fee item) { return item.UpdateState != 0; });

            ExpandGridView();
            //修改业务基本信息列表高度
            if (CurrBusinessUpdate.Count == 0)
            {
                splitContainerControl1.SplitterPosition = 100;
            }
        }


        /// <summary>
        ///  绑定数据
        /// </summary>
        public virtual void InnerInitData()
        {

        }
        /// <summary>
        /// 添加业务更改的列对象
        /// </summary>
        /// <param name="fieldDBName">"1"为普通列，"2"为HBL，"3"为集装箱</param>
        /// <param name="fieldName">列名</param>
        /// <param name="oldValue">原值</param>
        /// <param name="newValue">新值</param>
        public void AddBusiness(string fieldDBName, string fieldName, string oldValue, string newValue)
        {
            BusinessUpdateField obj = new BusinessUpdateField()
            {
                FieldDBName = fieldDBName,
                FieldName = fieldName,
                OldValue = oldValue,
                NewValue = newValue
            };
            CurrBusinessUpdate.Add(obj);

        }
        /// <summary>
        /// 添加业务更改的列对象
        /// </summary>
        /// <param name="fieldDBName">"1"为普通列，"2"为HBL，"3"为集装箱</param>
        /// <param name="fieldName">列名</param>
        /// <param name="oldValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="hblList">分提单更改信息列表</param>
        /// <param name="containerList">箱更改信息列表</param>
        public void AddBusiness(string fieldDBName, string fieldName, string oldValue, string newValue, List<HBLInfo> hblList, List<ICP.FCM.Common.ServiceInterface.ContainerInfo> containerList)
        {
            BusinessUpdateField obj = new BusinessUpdateField()
            {
                FieldDBName = fieldDBName,
                FieldName = fieldName,
                OldValue = oldValue,
                NewValue = newValue,
                HBLs = hblList,
                Containers = containerList
            };
            CurrBusinessUpdate.Add(obj);


        }





        #region IDisposable Members

        void IDisposable.Dispose()
        {


        }

        #endregion

        // DeckWorkspace deckws = null;
        private void DispatchDocumentCompare_Load(object sender, EventArgs e)
        {

            try
            {

                InnerInitControlAndName();
                BindData();
            }
            catch (System.Exception ex)
            {

            }


        }
        DateTime AsyTime = new DateTime(2013, 10, 1);
        /// <summary>
        /// 默认展开子表
        /// </summary>
        private void ExpandGridView()
        {

            gvBusiness.BeginUpdate();
            for (int i = 0; i < bsBusiness.Count; i++)
            {
                gvBusiness.SetMasterRowExpanded(i, true);
            }
            gvBusiness.EndUpdate();





        }
        /// <summary>
        /// 页面正在关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatchDocumentCompare_Closing(object sender, FormClosingEventArgs e)
        {

            Workitem.Workspaces.Remove(this.deckWsDocument);
        }

        /// <summary>
        /// 绘制业务列表的没有查出数据的背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBusiness_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (bsBusiness.Count == 0)
            {
                string str = LocalData.IsEnglish ? "BL has not yet been revised!" : "提单没有发生修改!";

                Font f = new Font("宋体", 10, FontStyle.Bold);
                Rectangle r = new Rectangle(e.Bounds.Top + 10, e.Bounds.Left + 30, e.Bounds.Right - 5, e.Bounds.Height - 5);
                e.Graphics.DrawString(str, f, Brushes.Black, r);

            }
        }

        /// <summary>
        /// 绘制账单列表的没有查出数据的背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBill_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (bsBills.Count == 0)
            {
                string str = LocalData.IsEnglish ? "Distribution bill information without modification!" : "分发账单信息没有修改!";

                Font f = new Font("宋体", 10, FontStyle.Bold);
                Rectangle r = new Rectangle(e.Bounds.Top + 10, e.Bounds.Left + 30, e.Bounds.Right - 5, e.Bounds.Height - 5);
                e.Graphics.DrawString(str, f, Brushes.Black, r);

            }
        }

        /// <summary>
        /// 签收业务
        /// </summary>
        public virtual void AcceptData()
        {

        }

        /// <summary>
        /// 设置控件信息
        /// </summary>
        private void InnerInitControlAndName()
        {
            //设计中英文

            Type type = typeof(DeckWorkspace);
            this.deckWsDocument = Workitem.Workspaces.AddNew(type, Guid.NewGuid().ToString()) as DeckWorkspace;
            //this.deckWsDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.deckWsDocument.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Panel2.Controls.Add(this.deckWsDocument);
            this.deckWsDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWsDocument.Location = new System.Drawing.Point(0, 0);

            if (LocalData.IsEnglish)
            {
                //btnConfirm.Text = "Confirm";
                //btnCancel.Text = "Cancel";
                //btnAccept.Text = "Accept";
                gcBusinessInfo.Text = "Business Information";
                gcBillInfo.Text = "Charge Information";
                chkAllCompare.Text = "Show Original Value";
                colFieldName.Caption = "Field Name";
                // colIsUpdate.Caption = "Update State";
                colFieldOldValue.Caption = "Old Value";
                colFieldNewValue.Caption = "New Value";
                lblOEBusinessNo.Text = "OE Business No:";
                lblOIBusinessNo.Text = "OI Business No:";
                lblOEBusinessNo.Text = "OE Business No:";
                lblDisptUser.Text = "Dispatch user:";
                lblDisptDate.Text = "Dispatch Date:";

                lblBusinessNO.Text = "Business No:";
                lblProDisptUser.Text = "Previous Dispatch User:";
                lblProAcceptDate.Text = "Previous Dispatch Date:";
                lblProAcceptUser.Text = "Previous Accept User:";
                lblProAcceptDate.Text = "Previous Accept Date:";
                lblCurrDisptUser.Text = "Current Dispatch User:";
                lblCurrAcceptDate.Text = "Current Dispatch Date:";
                lblCurrAcceptUser.Text = "Current Accept User:";
                lblCurrAcceptDate.Text = "Current Accept Date:";

                gridColWay.Caption = "Way";
                gridColChargeCode.Caption = "Change Code";
                gridColChargeName.Caption = "Change name";
                gridColIsAgent.Caption = "Agent";
                gridColOldSumMoney.Caption = "OE Amount";
                gridColNewSumMoney.Caption = "OI Amount";
                gridColOldRemark.Caption = "OE Remark";
                gridColNewRemark.Caption = "OI Remark";
                gridColFeeIsState.Caption = "State";

            }



            //控制控件位置
            if (CurrDipatchCompareType == DipatchCompareType.OIAccept || CurrDipatchCompareType == DipatchCompareType.OEAccept)
            {
                pnlCurrent.Visible = true;
                pnlCurrent.Dock = DockStyle.Top;
                pnlHestory.Visible = false;
                pnlHestory.Dock = DockStyle.None;

                lblOIBusinessNoValue.Text = CurrentSimpleBusinnessInfo.OIBusinessNO;

                lblOEBusinessNoValue.Text = CurrentSimpleBusinnessInfo.OEBusinessNO;
                lblDisptUserValue.Text = CurrentSimpleBusinnessInfo.DispatchUserName;
                lblDisptDateValue.Text = CurrentSimpleBusinnessInfo.DispatchDate.ToLongDateString();


                ucDocumentDispatchPart1 = Workitem.SmartParts.AddNew<UCDocumentDispatchPart>(Guid.NewGuid().ToString());
                ucDocumentDispatchPart1.ContextHistory = new BusinessOperationContext() { OperationID = OldOperationID };
                ucDocumentDispatchPart1.ContextCurrent = new BusinessOperationContext() { OperationID = NewOperationID};
                ucDocumentDispatchPart1.ContextCurrent.Add("DocumentState", DocumentState.Dispatched);

                ucDocumentDispatchPart1.SetRemarkVisible(0);
                ucDocumentDispatchPart1.Remark = CurrentSimpleBusinnessInfo.Remark;
                ucDocumentDispatchPart1.RemarkReadOnly = true;
                ucDocumentDispatchPart1.IsCurrentUpdateHide = true;


                deckWsDocument.Show(ucDocumentDispatchPart1);
                ucDocumentDispatchPart1.SetDataSource();
                //控制文件列表比较控件的显示
                //if ((ucDocumentDispatchPart1.DocumentSourceCurrent == null || ucDocumentDispatchPart1.DocumentSourceCurrent.Count == 0) && (ucDocumentDispatchPart1.DocumentSourceHistory == null || ucDocumentDispatchPart1.DocumentSourceHistory.Count == 0))
                //{
                //    splitContainerControl2.SplitterPosition = 0;
                //    splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
                //}
                //else
                //{
                //splitContainerControl2.SplitterPosition = 130;
                //splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
                //}
            }
            else
            {
                pnlHestory.Visible = false;
                pnlHestory.Dock = DockStyle.Top;
                pnlCurrent.Visible = false;
                pnlCurrent.Dock = DockStyle.None;
                ucDocumentHistoryPart = Workitem.SmartParts.AddNew<UCDocumentHistoryPart>(Guid.NewGuid().ToString());
                deckWsDocument.Show(ucDocumentHistoryPart);
            }
            gridBusiness.Dock = DockStyle.Fill;






            //设计名称
            ColumnName colName = GetControlAndName();
            if (colName != null)
            {


                gridColWay.Caption = colName.ColWay;
                gridColChargeCode.Caption = colName.ColFeeCode;
                gridColIsAgent.Caption = colName.ColAgent;
                gridColOldSumMoney.Caption = colName.ColOIFeeSumMoney;
                gridColNewSumMoney.Caption = colName.ColOEFeeSumMoney;



            }

        }


        /// <summary>
        /// 初始化控件
        /// </summary>
        public virtual ColumnName GetControlAndName()
        {
            return null;


        }

        private void gvFees_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName == "NewSumMoney")
            {
                GridView gv = sender as GridView;
                Fee obj = gv.GetRow(e.RowHandle) as Fee;
                if (obj == null) return;

                if (obj.OldSumMoney != obj.NewSumMoney)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }

        }


        private void chkAllCompare_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCompare.Checked)
            {
                colFieldOldValue.VisibleIndex = 1;

                colFieldOldValue.Visible = true;
                gbHBLOldValue.Visible = true;
                gbContainerOldValue.Visible = true;

                gridColOldSumMoney.Visible = true;




                gridColWay.VisibleIndex = 0;
                gridColChargeCode.VisibleIndex = 1;
                gridColChargeName.VisibleIndex = 2;
                gridColIsAgent.VisibleIndex = 3;
                gridColOldSumMoney.VisibleIndex = 4;
                gridColNewSumMoney.VisibleIndex = 5;
                gridColOldRemark.VisibleIndex = 6;
                gridColNewRemark.VisibleIndex = 7;
                gridColFeeIsState.VisibleIndex = 8;

                bsBills.DataSource = CurrBill;
            }
            else
            {
                colFieldOldValue.Visible = false;
                gbHBLOldValue.Visible = false;
                gridColOldRemark.Visible = false;

                gridColOldSumMoney.Visible = false;
                bsBills.DataSource = CurrBill.FindAll(delegate(Fee item) { return item.UpdateState != 0; });
            }
        }
        #region 保存

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                BusinessSaveRequest originalBusiness = SaveBusiness(dispatchCompareObject.OIOceanBusinessInfo, dispatchCompareObject.OEBookingInfo);

                MBLInfoSaveRequest originalMBL = GetMBLInfo(this._businessInfo.ID);

                this.oiService.AcceptDispatchWithTrans(
                    originalBusiness,
                    originalMBL,
                    OldOperationID,
                    LocalData.UserInfo.LoginID,
                    isHblChange, isContainerChange, true);

                return true;
            }
            catch (Exception ex)
            {
                ////LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                //MessageBoxService.ShowInfo(ex.Message);
                throw ex;
                //return false;
            }
        }


        /// <summary>
        /// 获得保存业务数据实体
        /// </summary>
        /// <param name="currentData"></param>
        private BusinessSaveRequest SaveBusiness(OceanBusinessInfo oiBusInfo, OceanBusinessInfo oeBusInfo)
        {
            this.EndEdit();

            if (oeBusInfo == null)
            {
                oeBusInfo = new OceanBusinessInfo();
            }
            BusinessSaveRequest saveRequest = new BusinessSaveRequest();
            saveRequest.ID = oiBusInfo.ID;
            saveRequest.No = oiBusInfo.No;//== oeBusInfo.No?oiBusInfo.No:oeBusInfo.No;
            saveRequest.MBLID = oiBusInfo.MBLID.ToGuid();

            saveRequest.CompanyID = oiBusInfo.CompanyID;
            saveRequest.OperationType = oiBusInfo.OIOperationType;
            saveRequest.CustomerID = oiBusInfo.CustomerID;//== oeBusInfo.CustomerID ? oiBusInfo.CustomerID : oeBusInfo.CustomerID;
            saveRequest.CustomerNo = oiBusInfo.CustomerNo;
            saveRequest.TradeTermID = oiBusInfo.TradeTermID;// == oeBusInfo.TradeTermID ? oiBusInfo.TradeTermID : oeBusInfo.TradeTermID;
            saveRequest.SalesTypeID = oiBusInfo.SalesTypeID;//== oeBusInfo.SalesTypeID ? oiBusInfo.SalesTypeID : oeBusInfo.SalesTypeID;
            saveRequest.SalesID = (oiBusInfo.SalesID == oeBusInfo.SalesID || oeBusInfo.SalesID == null || oeBusInfo.SalesID == Guid.Empty) ? oiBusInfo.SalesID : oeBusInfo.SalesID;
            saveRequest.SalesDepartmentID = (oiBusInfo.SalesDepartmentID == oeBusInfo.SalesDepartmentID || oeBusInfo.SalesDepartmentID == null || oeBusInfo.SalesDepartmentID == Guid.Empty) ? oiBusInfo.SalesDepartmentID : oeBusInfo.SalesDepartmentID;
            saveRequest.TransportClauseID = (oiBusInfo.TransportClauseID == oeBusInfo.TransportClauseID || oeBusInfo.TransportClauseID == null || oeBusInfo.TransportClauseID == Guid.Empty) ? oiBusInfo.TransportClauseID : oeBusInfo.TransportClauseID;
            saveRequest.FilerId = oiBusInfo.FilerId;
            saveRequest.PaymentTermID = (oiBusInfo.PaymentTermID == oeBusInfo.PaymentTermID || oeBusInfo.PaymentTermID == null || oeBusInfo.PaymentTermID == Guid.Empty) ? oiBusInfo.PaymentTermID : oeBusInfo.PaymentTermID;
            saveRequest.CustomerServiceID = oiBusInfo.customerService;
            saveRequest.BookingMode = oiBusInfo.BookingMode;//== oeBusInfo.BookingMode ? oiBusInfo.BookingMode : oeBusInfo.BookingMode;
            saveRequest.POLFilerID = oiBusInfo.POLFilerID;
            saveRequest.POLFilerName = oiBusInfo.POLFilerName;
            saveRequest.BookingDate = oiBusInfo.BookingDate;//== oeBusInfo.BookingDate ? oiBusInfo.BookingDate : oeBusInfo.BookingDate;
            saveRequest.OverSeasFilerId = oiBusInfo.OverSeasFilerId;// == oeBusInfo.OverSeasFilerId ? oiBusInfo.OverSeasFilerId : oeBusInfo.OverSeasFilerId;
            saveRequest.AgentID = (oiBusInfo.AgentID == oeBusInfo.AgentID || oeBusInfo.AgentID == null || oeBusInfo.AgentID == Guid.Empty) ? oiBusInfo.AgentID : oeBusInfo.AgentID;
            saveRequest.AgentNo = oiBusInfo.AgentNo;
            saveRequest.ShipperID = (oiBusInfo.ShipperID == oeBusInfo.ShipperID || oeBusInfo.ShipperID == null || oeBusInfo.ShipperID == Guid.Empty) ? oiBusInfo.ShipperID : oeBusInfo.ShipperID;
            saveRequest.ShipperDescription = oiBusInfo.ShipperDescription;
            saveRequest.ConsigneeID = (oiBusInfo.ConsigneeID == oeBusInfo.ConsigneeID || oeBusInfo.ConsigneeID == null || oeBusInfo.ConsigneeID == Guid.Empty) ? oiBusInfo.ConsigneeID : oeBusInfo.ConsigneeID;
            saveRequest.CargoDescription = oiBusInfo.CargoDescription;// == oeBusInfo.CargoDescription ? oiBusInfo.CargoDescription : oeBusInfo.CargoDescription; //11-06
            saveRequest.ConsigneeDescription = oiBusInfo.ConsigneeDescription;

            saveRequest.NotifyPartyID = oeBusInfo.NotifyPartyID == null ? Guid.Empty : oeBusInfo.NotifyPartyID; //
            saveRequest.NotifyPartyDescription = oeBusInfo.NotifyPartyDescription;
            saveRequest.PlaceOfReceiptID = (oiBusInfo.PlaceOfReceiptID == oeBusInfo.PlaceOfReceiptID || oeBusInfo.PlaceOfReceiptID == null || oeBusInfo.PlaceOfReceiptID == Guid.Empty) ? oiBusInfo.PlaceOfReceiptID : oeBusInfo.PlaceOfReceiptID;
            saveRequest.POLID = (oiBusInfo.POLID == oeBusInfo.POLID || oeBusInfo.POLID == null || oeBusInfo.POLID == Guid.Empty) ? oiBusInfo.POLID : oeBusInfo.POLID;
            saveRequest.PODID = oiBusInfo.PODID;// == oeBusInfo.PODID ? oiBusInfo.PODID : oeBusInfo.PODID;
            saveRequest.PlaceOfDeliveryID = oiBusInfo.PlaceOfDeliveryID;//== oeBusInfo.PlaceOfDeliveryID ? oiBusInfo.PlaceOfDeliveryID : oeBusInfo.PlaceOfDeliveryID;
            saveRequest.FinalDestinationID = oiBusInfo.FinalDestinationID;// == oeBusInfo.FinalDestinationID ? oiBusInfo.FinalDestinationID : oeBusInfo.FinalDestinationID;

            //Port Of Discharge\Place Of Delivery\Final Destination不需要再从装货港代理更新过来。
            saveRequest.ETD = oiBusInfo.ETD;
            saveRequest.ETA = oiBusInfo.ETA;
            saveRequest.DETA = oiBusInfo.DETA;// == oeBusInfo.DETA ? oiBusInfo.DETA : oeBusInfo.DETA;
            saveRequest.FETA = oiBusInfo.FETA;//== oeBusInfo.DETA ? oiBusInfo.DETA : oeBusInfo.DETA;
            saveRequest.IsTruck = oiBusInfo.IsTruck;
            saveRequest.IsCustoms = oiBusInfo.IsCustoms;
            saveRequest.IsCommodityInspection = oiBusInfo.IsCommodityInspection;
            saveRequest.IsQuarantineInspection = oiBusInfo.IsQuarantineInspection;
            saveRequest.IsWareHouse = oiBusInfo.IsWareHouse;
            //saveRequest.IsTelex = currentData.IsTelex;
            saveRequest.IsReleaseNotify = oiBusInfo.IsReleaseNotify;
            saveRequest.IsTransport = oiBusInfo.IsTransport;
            saveRequest.Commodity = oiBusInfo.Commodity;// == oeBusInfo.Commodity ? oiBusInfo.Commodity : oeBusInfo.Commodity;
            saveRequest.Quantity = (oiBusInfo.Quantity == oeBusInfo.Quantity || oeBusInfo.Quantity == null || oeBusInfo.Quantity == 0) ? oiBusInfo.Quantity : oeBusInfo.Quantity;
            saveRequest.QuantityUnitID = (oiBusInfo.QuantityUnitID == oeBusInfo.QuantityUnitID || oeBusInfo.QuantityUnitID == null || oeBusInfo.QuantityUnitID == Guid.Empty) ? oiBusInfo.QuantityUnitID : oeBusInfo.QuantityUnitID;
            saveRequest.Weight = (oiBusInfo.Weight == oeBusInfo.Weight || oeBusInfo.Weight == null || oeBusInfo.Weight == 0) ? oiBusInfo.Weight : oeBusInfo.Weight;
            saveRequest.WeightUnitID = (oiBusInfo.WeightUnitID == oeBusInfo.WeightUnitID || oeBusInfo.WeightUnitID == null || oeBusInfo.WeightUnitID == Guid.Empty) ? oiBusInfo.WeightUnitID : oeBusInfo.WeightUnitID;
            saveRequest.Measurement = (oiBusInfo.Measurement == oeBusInfo.Measurement || oeBusInfo.Measurement == null || oeBusInfo.Measurement == 0) ? oiBusInfo.Measurement : oeBusInfo.Measurement;
            saveRequest.MeasurementUnitID = (oiBusInfo.MeasurementUnitID == oeBusInfo.MeasurementUnitID || oeBusInfo.MeasurementUnitID == null || oeBusInfo.MeasurementUnitID == Guid.Empty) ? oiBusInfo.MeasurementUnitID : oeBusInfo.MeasurementUnitID;
            saveRequest.CargoDescription = oiBusInfo.CargoDescription;
            saveRequest.GoodDescription = oiBusInfo.GoodDescription;
            saveRequest.ContainerDescription = oiBusInfo.ContainerDescription;
            saveRequest.WareHouseID = oiBusInfo.WareHouseID;//== oeBusInfo.WareHouseID ? oiBusInfo.WareHouseID : oeBusInfo.WareHouseID;
            saveRequest.CustomsID = oiBusInfo.CustomsID;// == oeBusInfo.CustomsID ? oiBusInfo.CustomsID : oeBusInfo.CustomsID;
            saveRequest.CustomsDescription = oiBusInfo.CustomsDescription;
            saveRequest.IsClearance = oiBusInfo.IsClearance;
            saveRequest.ClearanceDate = oiBusInfo.ClearanceDate;
            saveRequest.ReleaseType = oiBusInfo.IsTelex ? FCMReleaseType.Telex : FCMReleaseType.Original;
            saveRequest.ReleaseDate = oiBusInfo.ReleaseDate;
            //saveRequest.IsTelex = oiBusInfo.IsTelex;
            saveRequest.Remark = oiBusInfo.Remark;
            saveRequest.saveByID = LocalData.UserInfo.LoginID;
            saveRequest.Updatedate = oiBusInfo.UpdateDate;


            //saveRequest.CarrierID = oiBusInfo.MBLInfo.CarrierID == dispatchCompareObject.OEMBLInfo.CarrierID ? oiBusInfo.MBLInfo.CarrierID : dispatchCompareObject.OEMBLInfo.CarrierID;
            //saveRequest.CarrierID=oiBusInfo.car

            saveRequest.AddInvolvedObject(oiBusInfo);
            return saveRequest;
        }

        #region 保存MBL

        public MBLInfoSaveRequest GetMBLInfo(Guid BusinessID)
        {
            OceanBusinessMBLList mblInfo = _businessInfo.MBLInfo;
            OceanBusinessMBLList oeMbl = dispatchCompareObject.OEMBLInfo;
            if (mblInfo != null)
            {
                if (oeMbl == null)
                {
                    oeMbl = new OceanBusinessMBLList();
                }
                MBLInfoSaveRequest mblInfoToSave = new MBLInfoSaveRequest();

                mblInfoToSave.ID = mblInfo.ID;
                mblInfoToSave.UpdateDates = mblInfo.UpdateDate;
                mblInfoToSave.MBLNo = mblInfo.MBLNo; //mblInfo.MBLNo == oeMbl.MBLNo ?: oeMbl.MBLNo;

                mblInfoToSave.SubNo = mblInfo.SubNo;// == oeMbl.SubNo ? mblInfo.SubNo : oeMbl.SubNo; ;
                mblInfoToSave.CarrierID = (mblInfo.CarrierID == oeMbl.CarrierID || oeMbl.CarrierID == Guid.Empty || oeMbl.CarrierID == null) ? mblInfo.CarrierID : oeMbl.CarrierID; ;
                mblInfoToSave.AgentOfCarrierID = mblInfo.AgentOfCarrierID;// (mblInfo.AgentOfCarrierID == oeMbl.AgentOfCarrierID || oeMbl.AgentOfCarrierID == Guid.Empty || oeMbl.AgentOfCarrierID == null) ? mblInfo.AgentOfCarrierID : oeMbl.AgentOfCarrierID;// ;= mblInfo.AgentOfCarrierID ;//==
                mblInfoToSave.VesselID = mblInfo.VoyageID;// == oeMbl.VoyageID ? mblInfo.VoyageID : oeMbl.VoyageID;
                mblInfoToSave.PreVoyageID = mblInfo.PreVoyageID;//  == oeMbl.PreVoyageID ? mblInfo.PreVoyageID : oeMbl.PreVoyageID; ;
                mblInfoToSave.FinalWareHouseID = mblInfo.FinalWareHouseID;// == oeMbl.FinalWareHouseID ? mblInfo.FinalWareHouseID : oeMbl.FinalWareHouseID; ;
                mblInfoToSave.ReturnLocationID = mblInfo.ReturnLocationID;//== oeMbl.ReturnLocationID ? mblInfo.ReturnLocationID : oeMbl.ReturnLocationID; ;
                mblInfoToSave.ITNO = mblInfo.ITNO;// == oeMbl.ITNO ? mblInfo.ITNO : oeMbl.ITNO; ;
                mblInfoToSave.ITDate = mblInfo.ITDate;// == oeMbl.ITDate ? mblInfo.ITDate : oeMbl.ITDate; ;
                mblInfoToSave.ITPalce = mblInfo.ITPalce;// == oeMbl.ITPalce ? mblInfo.ITPalce : oeMbl.ITPalce; ;
                mblInfoToSave.ReleaseType = mblInfo.ReleaseType;
                mblInfoToSave.MBLTransportClauseID = (mblInfo.MBLTransportClauseID == oeMbl.MBLTransportClauseID || oeMbl.MBLTransportClauseID == Guid.Empty || oeMbl.MBLTransportClauseID == null) ? mblInfo.MBLTransportClauseID : oeMbl.MBLTransportClauseID; ;
                mblInfoToSave.SaveByID = LocalData.UserInfo.LoginID;
                mblInfoToSave.ETD = _businessInfo.ETD;
                mblInfoToSave.ETA = _businessInfo.ETA;

                //mblInfoToSave.UpdateDates = mblInfo.UpdateDate;

                mblInfoToSave.AddInvolvedObject(mblInfo);
                return mblInfoToSave;
            }
            else
            {
                return null;
            }
        }



        #endregion

        private void DispatchDocumentCompareBase_Resize(object sender, EventArgs e)
        {
            if (this.deckWsDocument != null)
            {
                this.deckWsDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            }

        }

        ////签收按钮事件
        //private void btnAccept_Click(object sender, EventArgs e)
        //{
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        Save(this._businessInfo, false);
        //    }

        //}

        #endregion

        private void gvFees_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (CurrBill.Count == 0)
            {
                isBillChange = false;
                string str = LocalData.IsEnglish ? "Fees has not yet been revised!" : "费用没有发生修改!";

                Font f = new Font("宋体", 10, FontStyle.Bold);
                Rectangle r = new Rectangle(e.Bounds.Top + 10, e.Bounds.Left + 30, e.Bounds.Right - 5, e.Bounds.Height - 5);
                e.Graphics.DrawString(str, f, Brushes.Black, r);

            }
            else
            {
                isBillChange = true;
            }
        }


    }

    /// <summary>
    /// 显示列名
    /// </summary>
    public class ColumnName
    {
        #region 账单列名

        /// <summary>
        /// 新值账单号
        /// </summary>
        public string ColOEBillNO { get; set; }

        /// <summary>
        /// 旧值账单号
        /// </summary>
        public string ColOIBillNO { get; set; }

        /// <summary>
        /// 新值币种
        /// </summary>
        public string ColOECurrencyType { get; set; }

        /// <summary>
        /// 旧值币种
        /// </summary>
        public string ColOICurrencyType { get; set; }

        /// <summary>
        /// 新值总金额
        /// </summary>
        public string ColOESumMoney { get; set; }

        /// <summary>
        /// 旧值总金额
        /// </summary>
        public string ColOISumMoney { get; set; }

        #endregion 账单列名


        #region 费用列名

        /// <summary>
        /// 费用代码
        /// </summary>
        public string ColFeeCode { get; set; }

        /// <summary>
        ///费用类型
        /// </summary>
        public string ColWay { get; set; }

        /// <summary>
        /// 费用代理
        /// </summary>
        public string ColAgent { get; set; }

        /// <summary>
        /// 新值费用总金额
        /// </summary>
        public string ColOEFeeSumMoney { get; set; }

        /// <summary>
        /// 旧值费用总金额
        /// </summary>
        public string ColOIFeeSumMoney { get; set; }

        /// <summary>
        /// 新值备注
        /// </summary>
        public string ColOERemark { get; set; }

        /// <summary>
        /// 旧值备注
        /// </summary>
        public string ColOIRemark { get; set; }


        #endregion 费用列名
    }



}
