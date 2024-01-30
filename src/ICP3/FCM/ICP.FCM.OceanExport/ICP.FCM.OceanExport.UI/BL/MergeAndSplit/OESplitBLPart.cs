using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Linq;
using ICP.Common.UI;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanExport.UI.BL
{
    /// <summary>
    /// 海出提单分单
    /// </summary>
    public partial class OESplitBLPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        #endregion

        #region init
        /// <summary>
        /// 构造函数
        /// </summary>
        public OESplitBLPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.dxErrorProvider1.DataSource = null;
                this.dxErrorProvider2.DataSource = null;
                this.lwGridControl1.DataSource = null;
                this.gvCtn.CellValueChanged -= this.gvCtn_CellValueChanged;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowCellStyle -= this.gvMain_RowCellStyle;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.Dispose();
                this.bsContainer.DataSource = null;
                this.bsContainer.Dispose();
                this.bsOrg.DataSource = null;
                this.bsOrg.Dispose();
                this._CurrentSource = null;
                this._MBLs = null;
                this._OrgCtn = null;
                this._OrgData = null;
                this.ctns = null;

                this.stateValues = null;

                this.Saved = null;
                if (shipperFinder != null)
                {
                    shipperFinder.Dispose();
                }
                if (consigneeFinder != null)
                {
                    consigneeFinder.Dispose();
                }
                if (notifyPartyFinder != null)
                {
                    notifyPartyFinder.Dispose();
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
            if (LocalData.IsEnglish == false) SetCNText();

        }

        private void SetCNText()
        {
            barSave.Caption = "保存(&S)";
            barClose.Caption = "关闭(&S)";

            colConsignee.Caption = "收货人";
            colContainerNo.Caption = "箱号";
            colNO.Caption = "提单号";
            colNotifyParty.Caption = "通知人";
            colShipper.Caption = "发货人";
            colRelation.Caption = "关联";
            colSealNo.Caption = "封条号";
            colContainerNo.Caption = "箱号";

            colRelation.Caption = "关联";
            colCommodity.Caption = "品名";
            colCtnNO.Caption = "箱号";
            colSealNo.Caption = "封条号";
            colType.Caption = "箱型";
            colQuantity.Caption = "件数";
            colMeasurement.Caption = "体积";
            colWeight.Caption = "重量";
            colIsSOC.Caption = "自有箱";
            colIsPartOf.Caption = "Part单";
            colRelation.Caption = "关联";
            colMarks.Caption = "唛头";

            groupContainer.Text = "箱信息";
            groupCurrentBLInfo.Text = "当前提单信息";
            groupSplitBLInfo.Text = "拆分提单信息";

            barDelete.Caption = "删除(&D)";
            barAdd.Caption = "新增(&A)";

            labNo.Text = "提单号";
            labNotifyParty.Text = "通知人";
            labShipper.Text = "发货人";
            labConsignee.Text = "收货人";
            labCtnNo.Text = "箱号";
            labRefNo.Text = "业务号";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }
        private IDisposable shipperFinder, consigneeFinder, notifyPartyFinder;
        private void InitControls()
        {
            shipperFinder = DataFindClientService.RegisterGridColumnFinder(colShipper
                                                  , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                                  , "ShipperID"
                                                  , "ShipperName"
                                                  , "ID"
                                                  , LocalData.IsEnglish ? "EName" : "CName");

            consigneeFinder = DataFindClientService.RegisterGridColumnFinder(colConsignee
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                                , "ConsigneeID"
                                                , "ConsigneeName"
                                                , "ID"
                                                , LocalData.IsEnglish ? "EName" : "CName");

            notifyPartyFinder = DataFindClientService.RegisterGridColumnFinder(colNotifyParty
                                                , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                                , "NotifyPartyID"
                                                , "NotifyPartyName"
                                                , "ID"
                                                , LocalData.IsEnglish ? "EName" : "CName");

            ICPCommUIHelper.SetCmbContainerType(cmbType);

            if (_BLType == FCMBLType.HBL)
            {
                rtxtNO.NullText = LocalData.IsEnglish ? "Auto Bulid" : "自动生成";
                this.gvMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMain_RowCellStyle);

                foreach (var item in _MBLs)
                {
                    rcmbMBLNO.Items.Add(item.NO);
                }

                colNO.OptionsColumn.AllowEdit = false;
            }
            else
            {
                colMBLNO.Visible = false;
            }
        }

        #endregion

        #region 事件

        #region bs和gridview

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            bool isSrcc = true;
            foreach (var item in CurrentRow.Containers)
            {
                if (item.Validate(delegate(ValidateEventArgs ve)
                {
                    if (item.Relation)
                    {
                        if (item.Measurement <= 0)
                            ve.SetErrorInfo("Measurement", LocalData.IsEnglish ? "Measurement can't be zero." : "体积不能为0.");
                        if (item.Weight <= 0)
                            ve.SetErrorInfo("Weight", LocalData.IsEnglish ? "Weight can't be zero." : "重量不能为0.");
                        if (item.Quantity <= 0)
                            ve.SetErrorInfo("Quantity", LocalData.IsEnglish ? "Quantity can't be zero." : "数量不能为0.");
                    }

                }) == false) isSrcc = false;
            }
            e.Allow = isSrcc;
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            //原始记录不能更改
            if (CurrentRow.ID == _OrgData.ID)
                barDelete.Enabled = gvMain.OptionsBehavior.Editable = false;
            else
                barDelete.Enabled = gvMain.OptionsBehavior.Editable = true;

            if (CurrentRow.Containers == null)
            {
                CurrentRow.Containers = new List<ClientContainerList>();
                foreach (var item in _OrgCtn)
                {
                    ClientContainerList c = OEUtility.Clone<ClientContainerList>(item.Value);
                    c.TypeName = item.Value.TypeName;
                    CurrentRow.Containers.Add(c);
                }
            }
            else
            {
                foreach (var ctn in CurrentRow.Containers)
                {
                    ctn.IsPartOf = _OrgCtn[ctn.ID].IsPartOf;
                    ctn.IsSOC = _OrgCtn[ctn.ID].IsSOC;
                    ctn.No = _OrgCtn[ctn.ID].No;
                    ctn.SealNo = _OrgCtn[ctn.ID].SealNo;
                    ctn.TypeID = _OrgCtn[ctn.ID].TypeID;
                    ctn.TypeName = _OrgCtn[ctn.ID].TypeName;
                }
            }

            bsContainer.DataSource = CurrentRow.Containers;
        }

        private void gvCtn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || CurrentCtn == null) return;

            if (e.Column == colRelation)
            {
                #region 更新Relation
                if (CurrentCtn.Relation)
                {
                    CurrentCtn.BLID = CurrentRow.ID;
                }
                else
                {
                    CurrentCtn.BLID = Guid.Empty;
                    CurrentCtn.Quantity = 0;
                    CurrentCtn.Weight = 0;
                    CurrentCtn.Measurement = 0;
                    CurrentCtn.Marks = CurrentCtn.Commodity = string.Empty;
                }

                StringBuilder strBulider = new StringBuilder();
                foreach (var item in CurrentRow.Containers)
                {
                    if (item.Relation)
                    {
                        if (strBulider.Length > 0) strBulider.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);
                        strBulider.Append(item.No);
                    }
                }
                CurrentRow.ContainerNos = strBulider.ToString();
                bsList.EndEdit();
                #endregion
            }
            //更新了箱的数据
            else if (e.Column != colWeight && e.Column != colMeasurement && e.Column != colQuantity && e.Column != colCommodity && e.Column != colMarks)
            {
                _OrgCtn[CurrentCtn.ID].IsPartOf = CurrentCtn.IsPartOf;
                _OrgCtn[CurrentCtn.ID].IsSOC = CurrentCtn.IsSOC;
                _OrgCtn[CurrentCtn.ID].No = CurrentCtn.No;
                _OrgCtn[CurrentCtn.ID].SealNo = CurrentCtn.SealNo;
                _OrgCtn[CurrentCtn.ID].TypeID = CurrentCtn.TypeID;
                _OrgCtn[CurrentCtn.ID].TypeName = CurrentCtn.TypeName;
            }
            else if (CurrentCtn.Measurement != 0 || CurrentCtn.Weight != 0 || CurrentCtn.Quantity != 0
                || string.IsNullOrEmpty(CurrentCtn.Marks) || string.IsNullOrEmpty(CurrentCtn.Commodity))
            {
                CurrentCtn.Relation = true;
                CurrentCtn.BLID = CurrentRow.ID;
            }
        }

        private void gvMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column == colNO && e.CellValue == null)
            {
                e.Appearance.ForeColor = Color.DimGray;
            }
        }

        #endregion

        #region 增删关
        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            //复制首条
            ClientSplitBLInfo list = OEUtility.Clone<ClientSplitBLInfo>(_CurrentSource[0]);
            list.ID = Guid.NewGuid();
            list.No = _BLType == FCMBLType.MBL ? string.Empty : null;
            list.ContainerNos = string.Empty;
            if (_BLType == FCMBLType.MBL) list.MBLNO = string.Empty;
            list.UpdateDate = null;

            list.Containers = new List<ClientContainerList>();
            foreach (var item in _OrgCtn)
            {
                ClientContainerList c = OEUtility.Clone<ClientContainerList>(item.Value);
                list.Containers.Add(c);
            }
            bsList.Add(list);
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow == null || CurrentRow.ID == _OrgData.ID) return;
            bsList.RemoveCurrent();
        }
        #endregion

        #region 保存

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData() == false) return;

            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        public override bool SaveData()
        {
            if (ValidateData() == false) return false;

            #region 提示
            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue split this BL?" : "您确认要分拆此提单?"
                                , LocalData.IsEnglish ? "" : ""
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }
            #endregion

            #region 更新箱的数据源

            foreach (var item in _CurrentSource)
            {
                foreach (var ctn in item.Containers)
                {
                    ctn.IsPartOf = _OrgCtn[CurrentCtn.ID].IsPartOf;
                    ctn.IsSOC = _OrgCtn[CurrentCtn.ID].IsSOC;
                    ctn.No = _OrgCtn[CurrentCtn.ID].No;
                    ctn.SealNo = _OrgCtn[CurrentCtn.ID].SealNo;
                    ctn.TypeID = _OrgCtn[CurrentCtn.ID].TypeID;
                    ctn.TypeName = _OrgCtn[CurrentCtn.ID].TypeName;
                }
            }
            #endregion

            try
            {
                #region 构建XML结构
                System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();
                XElement xeRoot = new XElement("SplitInfo");
                doc.Add(xeRoot);

                #region Containers
                XElement xeContainers = new XElement("Containers");
                foreach (var item in _OrgCtn)
                {
                    XElement xeCtn = new XElement("Container");
                    xeCtn.Add(new XElement("ID", item.Value.ID));
                    xeCtn.Add(new XElement("TypeID", item.Value.TypeID));
                    xeCtn.Add(new XElement("No", item.Value.No));
                    xeCtn.Add(new XElement("SealNo", item.Value.SealNo));
                    xeCtn.Add(new XElement("ShippingOrderNo", item.Value.ShippingOrderNo));
                    xeCtn.Add(new XElement("IsPartOf", item.Value.IsPartOf));
                    xeCtn.Add(new XElement("IsSOC", item.Value.IsSOC));
                    xeCtn.Add(new XElement("UpdateDate", item.Value.UpdateDate));
                    xeContainers.Add(xeCtn);
                }
                xeRoot.Add(xeContainers);

                #endregion
                #region BLs
                XElement xeBLs = new XElement("BLs");
                foreach (ClientSplitBLInfo item in _CurrentSource)
                {
                    XElement xeBL = new XElement("BL");
                    xeBL.Add(new XElement("ID", item.ID));
                    xeBL.Add(new XElement("ShipperID", item.ShipperID));
                    xeBL.Add(new XElement("ConsigneeID", item.ConsigneeID));
                    xeBL.Add(new XElement("NotifyPartyID", item.NotifyPartyID));
                    xeBL.Add(new XElement("OceanBookingID", item.OceanBookingID));
                    xeBL.Add(new XElement("No", item.No));
                    xeBL.Add(new XElement("MBLNO", item.MBLNO));
                    xeBL.Add(new XElement("UpdateDate", item.UpdateDate));
                    xeBLs.Add(xeBL);
                }
                xeRoot.Add(xeBLs);
                #endregion
                #region Cargos
                XElement xeCargos = new XElement("Cargos");
                foreach (ClientSplitBLInfo blItem in _CurrentSource)
                {
                    if (blItem.Containers == null || blItem.Containers.Count == 0) continue;
                    foreach (ClientContainerList item in blItem.Containers)
                    {
                        if (item.Relation == false) continue;
                        XElement xeCargo = new XElement("Cargo");
                        xeCargo.Add(new XElement("CtnID", item.ID));
                        xeCargo.Add(new XElement("CargoID", item.CargoID));
                        xeCargo.Add(new XElement("BLID", item.BLID));
                        xeCargo.Add(new XElement("Commodity", item.Commodity));
                        xeCargo.Add(new XElement("Marks", item.Marks));
                        xeCargo.Add(new XElement("Quantity", item.Quantity));
                        xeCargo.Add(new XElement("Weight", item.Weight));
                        xeCargo.Add(new XElement("Measurement", item.Measurement));
                        xeCargo.Add(new XElement("CargoUpdateDate", item.CargoUpdateDate));
                        xeCargos.Add(xeCargo);
                    }
                }
                xeRoot.Add(xeCargos);
                #endregion

                #endregion

                ManyResult result = OceanExportService.SplitOceanBL(doc.ToString(), LocalData.UserInfo.LoginID);
                List<Guid> ids = new List<Guid>();
                foreach (var item in result.Items)
                {
                    ids.Add(item.GetValue<Guid>("ID"));

                    Guid mblId = item.GetValue<Guid>("OceanMBLID");
                    if (mblId != Guid.Empty && ids.Contains(mblId) == false) ids.Add(mblId);
                }

                if (this.Saved != null) this.Saved(new object[] { ids });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        private bool ValidateData()
        {
            this.Validate();
            bsList.EndEdit();
            bsContainer.EndEdit();

            if (_CurrentSource.Count <= 1)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Un Done" : "拆分提单信息中只有一票提单，不能进行拆分。");
                return false;
            }

            bool isSrcc = true;
            foreach (var item in _CurrentSource)
            {
                if (item.Validate
                    (delegate(ValidateEventArgs e)
                    {
                        if (_BLType == FCMBLType.HBL && string.IsNullOrEmpty(item.MBLNO))
                            e.SetErrorInfo("MBLNO", LocalData.IsEnglish ? "MBL NO Must Input" : "MBL NO 必须输入.");


                        if (_BLType == FCMBLType.MBL && string.IsNullOrEmpty(item.No))
                            e.SetErrorInfo("No", LocalData.IsEnglish ? "MBL NO Must Input" : "MBL NO 必须输入.");


                        foreach (var ctn in item.Containers)
                        {
                            if (ctn.Validate(delegate(ValidateEventArgs ce)
                            {
                                if (ctn.Relation)
                                {
                                    if (ctn.Quantity <= 0)
                                    {
                                        ce.SetErrorInfo("Quantity", LocalData.IsEnglish ? "Quantity can't be 0." : "件数不能为0.");
                                    }
                                    if (ctn.Measurement <= 0)
                                    {
                                        ce.SetErrorInfo("Measurement", LocalData.IsEnglish ? "Measurement can't be 0." : "体积不能为0.");
                                    }
                                    if (ctn.Weight <= 0)
                                    {
                                        ce.SetErrorInfo("Weight", LocalData.IsEnglish ? "Weight can't be 0." : "重量不能为0.");
                                    }
                                }

                            }) == false)
                            { isSrcc = false; }
                        }
                    }
                    ) == false) isSrcc = false;
            }

            return isSrcc;
        }

        #endregion

        #endregion

        #region 本地变量

        ClientSplitBLInfo CurrentRow
        {
            get { return bsList.Current as ClientSplitBLInfo; }
            set
            {
                ClientSplitBLInfo current = CurrentRow;
                current = value;
            }
        }

        ClientContainerList CurrentCtn
        {
            get { return bsContainer.Current as ClientContainerList; }
            set
            {
                ClientContainerList current = CurrentCtn;
                current = value;
            }
        }

        /// <summary>
        /// 当前数据源
        /// </summary>
        List<ClientSplitBLInfo> _CurrentSource = new List<ClientSplitBLInfo>();



        #region 原始数据

        /// <summary>
        /// 原始数据
        /// </summary>
        OceanBLList _OrgData = null;

        /// <summary>
        /// 原始数据的ClientContainerList字典
        /// </summary>
        Dictionary<Guid, ClientContainerList> _OrgCtn = new Dictionary<Guid, ClientContainerList>();

        #endregion

        FCMBLType _BLType
        {
            get
            {
                if (_OrgData == null) return FCMBLType.Unknown;
                else return _OrgData.BLType;
            }
        }

        #endregion

        #region IEditPart 成员
        private IDictionary<string, object> stateValues;
        public override void Init(IDictionary<string, object> values)
        {
            this.stateValues = values;
        }
        OceanBLList oceanBLList;
        //箱
        List<OceanBLContainerList> ctns = null;
        List<BookingBLInfo> _MBLs = null;
        private delegate void DataBindDelegate();
        private void InnerDataBind()
        {
            bsOrg.DataSource = _OrgData;
            bsList.DataSource = _CurrentSource;
            bsContainer.DataSource = _CurrentSource[0].Containers;

        #endregion
        }
        void BindingData(object data)
        {
            if (data == null)
            {
                string operationNo = this.stateValues["OperationNo"] as string;
                string blNo = this.stateValues["BLNo"] as string;
                string result = OceanExportService.GetOceanBLList(null, operationNo, blNo, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, DateSearchDispatchType.All, null, null, 10, ReleaseBLSearchStatue.All, ReleaseRCSearchStatue.All, ApplyReleaseSearchStatue.All, ReceiveRCSearchStatue.All, null);
                oceanBLList = JSONSerializerHelper.DeserializeFromJson<List<OceanBLList>>(result)[0];
            }
            else
            {
                oceanBLList = data as OceanBLList;
            }
            _OrgData = oceanBLList;
            if (_BLType == FCMBLType.MBL)
                ctns = OceanExportService.GetOceanMBLContainerList(_OrgData.ID);
            else
                ctns = OceanExportService.GetOceanHBLContainerList(_OrgData.ID);
            //首条数据
            ClientSplitBLInfo info = new ClientSplitBLInfo();
            OEUtility.CopyToValue(_OrgData, info, typeof(ClientSplitBLInfo));
            if (_BLType == FCMBLType.HBL)
            {
                _MBLs = OceanExportService.GetOceanBookingListByIds(new Guid[] { _OrgData.OceanBookingID })[0].OceanMBLs;
                info.MBLNO = _MBLs.Find(delegate(BookingBLInfo item) { return item.ID == _OrgData.MBLID; }).NO;
            }
            else info.MBLNO = string.Empty;
            info.Containers = new List<ClientContainerList>();
            foreach (var item in ctns)
            {
                ClientContainerList c = new ClientContainerList();
                OEUtility.CopyToValue(item, c, typeof(ClientContainerList));
                info.Containers.Add(c);
                //复制一条到原始数据
                _OrgCtn.Add(c.ID, OEUtility.Clone<ClientContainerList>(c));
                _OrgCtn[c.ID].Relation = false;
                _OrgCtn[c.ID].CargoID = Guid.Empty;
                _OrgCtn[c.ID].Measurement = _OrgCtn[c.ID].Weight = _OrgCtn[c.ID].Quantity = 0;
                _OrgCtn[c.ID].Commodity = string.Empty;
                _OrgCtn[c.ID].Marks = string.Empty;
            }
            _CurrentSource.Add(info);
            InnerDataBind();
        }

        /// <summary>
        /// 接收OceanBLList
        /// </summary>
        public override object DataSource
        {
            get { return bsList.DataSource; }
            set { BindingData(value); }
        }

        public override void EndEdit()
        {
            this.Validate();
            bsList.EndEdit();
            bsContainer.EndEdit();
        }

        /// <summary>
        /// 返回List Guid
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;





    }

    #region Client Objects

    /// <summary>
    /// ClientSplitBLInfo
    /// </summary>
    public class ClientSplitBLInfo : BaseDataObject
    {
        public Guid ID { get; set; }

        #region 收发通

        //public Guid ShipperID { get; set; }
        Guid _ShipperID;
        /// <summary>
        /// 客户
        /// </summary>
        public Guid ShipperID
        {
            get
            {
                return _ShipperID;
            }
            set
            {
                if (_ShipperID != value)
                {
                    _ShipperID = value;
                    base.OnPropertyChanged("ShipperID", value);
                }
            }
        }
        string _ShipperName;
        /// <summary>
        /// 客户
        /// </summary>
        public string ShipperName
        {
            get
            {
                return _ShipperName;
            }
            set
            {
                if (_ShipperName != value)
                {
                    _ShipperName = value;
                    base.OnPropertyChanged("ShipperName", value);
                }
            }
        }

        public Guid? ConsigneeID { get; set; }
        public Guid? NotifyPartyID { get; set; }
        public Guid OceanBookingID { get; set; }
        //public string ShipperName { get; set; }
        public string ConsigneeName { get; set; }
        public string NotifyPartyName { get; set; }

        #endregion
        public string ContainerNos { get; set; }
        #region 单号

        public string No { get; set; }
        public string MBLNO { get; set; }

        public DateTime? UpdateDate { get; set; }

        #endregion

        /// <summary>
        /// 箱列表
        /// </summary>
        public List<ClientContainerList> Containers { get; set; }
    }

    /// <summary>
    /// ClientContainerList
    /// </summary>
    [Serializable]
    public partial class ClientContainerList : BaseDataObject
    {
        public Guid ID { get; set; }
        public Guid CargoID { get; set; }

        /// <summary>
        ///  箱号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        ///  ShippingOrderNo
        /// </summary>
        public string ShippingOrderNo { get; set; }

        /// <summary>
        /// 箱型
        /// </summary>
        public Guid TypeID { get; set; }
        /// <summary>
        /// 箱型
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 封条号
        /// </summary>
        public string SealNo { get; set; }
        /// <summary>
        /// 自有箱
        /// </summary>
        public bool IsSOC { get; set; }
        /// <summary>
        /// 多套提单
        /// </summary>
        public bool IsPartOf { get; set; }

        /// <summary>
        /// 唛头
        /// </summary>
        public string Marks { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement { get; set; }
        /// <summary>
        /// 提单ID
        /// </summary>
        public Guid? BLID { get; set; }

        /// <summary>
        /// 已关联
        /// </summary>
        public bool Relation { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CargoUpdateDate { get; set; }
    }

    #endregion
}

