using System;
using System.Collections.Generic;
using System.Drawing;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Linq;
namespace ICP.FCM.OceanExport.UI.BL
{
    /// <summary>
    /// 合并提单界面
    /// </summary>
    public partial class OEMergeBLPart : BaseEditPart
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

        #endregion

        #region init
        /// <summary>
        /// 构造函数
        /// </summary>
        public OEMergeBLPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.gvMain.CellValueChanged -= this.gvMain_CellValueChanged;
                this.gvMain.CellValueChanging -= this.gvMain_CellValueChanging;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.gcCtn.DataSource = null;
                this.bsContainer.DataSource = null;
                this.bsContainer.Dispose();
                this.Saved = null;
                this.mergeBLInfos = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }

            };
            if (!LocalData.IsDesignMode && LocalData.IsEnglish == false)
            {
                SetCNText();
            }
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
            colReservation.Caption = "保留";

            colRelation.Caption = "关联";
            colCtnNo.Caption = "箱号";
            colType.Caption = "类型";
            colSealNo.Caption = "封条号";

            labTip.Text = "提单之间的计量单位不一样.合并操作只保留勾选的提单的计量单位";

        }

        MergeBLInfo CurrentRow
        {
            get { return bsList.Current as MergeBLInfo; }
        }
        FCMBLType _BLType = FCMBLType.Unknown;

        #endregion

        #region Event
        void gvMain_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colReservation) return;

            if (bool.Parse(e.Value.ToString()) == true)
            {
                List<MergeBLInfo> list = bsList.DataSource as List<MergeBLInfo>;
                foreach (var item in list)
                {
                    if (item.ID != CurrentRow.ID) item.Reservation = false;
                }
                bsList.DataSource = list;
                bsList.ResetBindings(false);
            }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void gvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colReservation) return;

            if (CurrentRow.Reservation)
            {
                List<MergeBLInfo> list = bsList.DataSource as List<MergeBLInfo>;
                foreach (var item in list)
                {
                    if (item.ID != CurrentRow.ID) item.Reservation = false;
                }
                bsList.DataSource = list;
                bsList.ResetBindings(false);
            }
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData() == false) return;
        }

        public override bool SaveData()
        {
            if (this.ValidateData() == false) return false;

            try
            {
                List<MergeBLInfo> list = bsList.DataSource as List<MergeBLInfo>;

                #region 保存
                Guid reservationID = Guid.Empty;
                List<Guid> blIDs = new List<Guid>();
                foreach (var item in list)
                {
                    blIDs.Add(item.ID);
                    if (item.Reservation) reservationID = item.ID;
                }
                SingleResult result = OceanExportService.MergeOceanBL(reservationID, blIDs.ToArray(), LocalData.UserInfo.LoginID);
                MergeBLInfo reservationInfo = list.Find(delegate(MergeBLInfo item) { return item.ID == reservationID; });
                #endregion

                #region 生成箱号 及箱操作

                List<OceanBLContainerList> ctnList = null;
                if (_BLType == FCMBLType.MBL)
                    ctnList = OceanExportService.GetOceanMBLContainerList(reservationID);
                else
                    ctnList = OceanExportService.GetOceanHBLContainerList(reservationID);

                reservationInfo.ContainerNo = OEUtility.BulidCtnNOByContainerList(ctnList);

                bsContainer.DataSource = ctnList;
                bsContainer.ResetBindings(false);
                #endregion

                #region 控件控制
                colReservation.Visible = false;
                barSave.Enabled = false;
                gcMain.Size = new Size(709, 64);
                gcCtn.Visible = true;
                labTip.Visible = false;
                #endregion


                result.Add("ContainerNos", reservationInfo.ContainerNo);
                blIDs.Remove(reservationID);
                if (Saved != null) Saved(new object[] { result, blIDs });

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        private bool ValidateData()
        {
            this.Validate();
            this.bsList.EndEdit();

            List<MergeBLInfo> list = bsList.DataSource as List<MergeBLInfo>;
            List<bool> reservations = new List<bool>();
            foreach (var item in list) { reservations.Add(item.Reservation); }

            if (reservations.Contains(true) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this,
                                LocalData.IsEnglish ? "Un Done." : "请选择一条需要保留的提单."
                                , LocalData.IsEnglish ? "Tip" : "提示");

                return false;
            }
            return true;
        }

        #endregion

        #region IEditPart 成员
        List<MergeBLInfo> mergeBLInfos = new List<MergeBLInfo>();
        private delegate void DataBindDelegate();
        private void InnerDataBind()
        {
            bsList.DataSource = mergeBLInfos;

            #region 验证计数单位是否相同

            Guid quantityUnitID = mergeBLInfos[0].QuantityUnitID;
            Guid weightUnitID = mergeBLInfos[0].WeightUnitID;
            Guid measurementUnitID = mergeBLInfos[0].MeasurementUnitID;

            foreach (var item in mergeBLInfos)
            {
                if (weightUnitID != item.WeightUnitID
                    || quantityUnitID != item.QuantityUnitID
                    || measurementUnitID != item.MeasurementUnitID)
                {
                    labTip.Visible = true;
                    break;
                }
            }
            #endregion
        }
        void BindingData(object data)
        {
            List<OceanBLList> list = data as List<OceanBLList>;
            _BLType = list[0].BLType;

            List<Guid> ids = list.Select(item => item.ID).ToList();
            List<OceanContainerCargoList> measureInfos = OceanExportService.GetOceanBLMeasureInfoList(ids.ToArray());


            foreach (var item in list)
            {
                OceanContainerCargoList mItem = measureInfos.Find(delegate(OceanContainerCargoList mitem) { return mitem.ID == item.ID; });
                MergeBLInfo mergeBLInfo = new MergeBLInfo(item.ID, item.No, item.ShipperName, item.ConsigneeName, item.NotifyPartyName, item.ContainerNos,
                                                         mItem.Quantity, mItem.QuantityUnitID,
                                                         mItem.Weight, mItem.WeightUnitID,
                                                         mItem.Measurement, mItem.MeasurementUnitID);
                mergeBLInfos.Add(mergeBLInfo);
            }
            mergeBLInfos[0].Reservation = true;
            InnerDataBind();

        }

        /// <summary>
        /// List OceanBLList
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
        }

        /// <summary>
        /// 返回 SingleResult 和 需要删除的 ID list
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion
    }

    /// <summary>
    /// 分单页面的客户端帮助类
    /// </summary>
    class MergeBLInfo : ICP.Framework.CommonLibrary.Common.BaseDataObject
    {
        public MergeBLInfo() { }
        public MergeBLInfo(Guid id, string no, string shipperName, string consigneeName, string notifyPartyName, string containerNo,
                           int quantity, Guid quantityUnitID,
                           decimal weight, Guid weightUnitID,
                           decimal measurement, Guid measurementUnitID)
        {
            ID = id; No = no;
            ShipperName = shipperName; ConsigneeName = consigneeName; NotifyPartyName = notifyPartyName; ContainerNo = containerNo;
            Quantity = quantity; Weight = weight; Measurement = measurement;
            QuantityUnitID = quantityUnitID; WeightUnitID = weightUnitID; MeasurementUnitID = measurementUnitID;
        }

        public Guid ID { get; set; }
        public bool Reservation { get; set; }
        public string No { get; set; }
        public string NotifyPartyName { get; set; }
        public string ConsigneeName { get; set; }
        public string ShipperName { get; set; }
        public string ContainerNo { get; set; }

        public int Quantity { get; set; }
        public Guid QuantityUnitID { get; set; }
        public decimal Weight { get; set; }
        public Guid WeightUnitID { get; set; }
        public decimal Measurement { get; set; }
        public Guid MeasurementUnitID { get; set; }
    }
}
