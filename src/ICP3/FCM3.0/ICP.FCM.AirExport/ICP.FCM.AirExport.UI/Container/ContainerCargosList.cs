using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.Common.UI;

namespace ICP.FCM.AirExport.UI.Container
{
    /// <summary>
    /// 箱下的货物列表
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class ContainerCargosList : BaseEditPart
    {
        public WorkItem Workitem { get; set; }

        public AirBookingInfo BookingInfo { get; set; }

        IAirExportService oceanExportService { get { return Workitem.Services.Get<IAirExportService>(); } }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        public ContainerCargosList()
        {
            InitializeComponent();

            this.Load += new EventHandler(ContainerCargosList_Load);

            if (!LocalData.IsEnglish)
            {
                this.SetCnText();
            }
        }

        void InitControls()
        {

            this.ricbxMBLs.Enter += new EventHandler(ricbxMBLs_Enter);

            //包装
            //Utility.SetEnterToExecuteOnec(this.rcmbQuantityUnit, delegate
            //{
            ICPCommUIHelper.SetCmbDataDictionary(this.rcmbQuantityUnit, DataDictionaryType.QuantityUnit);
            //});

            ////重量
            //Utility.SetEnterToExecuteOnec(this.rcmbWeightUnit, delegate
            //{
            ICPCommUIHelper.SetCmbDataDictionary(rcmbWeightUnit, DataDictionaryType.WeightUnit);
            //});

            ////体积
            //Utility.SetEnterToExecuteOnec(rcmbMeasurementUnit, delegate
            //{
            ICPCommUIHelper.SetCmbDataDictionary(rcmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
            //});
        }

        List<AirMBLInfo> _bls;
        public void SetMBLs(List<AirMBLInfo> bls)
        {
            this._bls = bls;
            this.SetMBLCombox();

            InitControls();
        }

        void AddMBL(Guid mblId, string mblNo,Guid quantityUnitId,Guid weightUnitId,Guid measurementUnitId)
        {
            this._bls.RemoveAll(o => o.MBLID == mblId);
            this._bls.Add(
                new AirMBLInfo { 
                No=mblNo, 
                ID =mblId, 
                QuantityUnitID=quantityUnitId, 
                //WeightUnitID=weightUnitId,
                MeasurementUnitID=measurementUnitId }
                );

            this.SetMBLCombox();
        }

        void SetMBLCombox()
        {
            this.ricbxMBLs.Items.Clear();
            foreach (AirMBLInfo info in this._bls)
            {                
                DevExpress.XtraEditors.Controls.ImageComboBoxItem icbi = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
                icbi.Description = info.ID.ToString();
                icbi.Value = info.No;

                this.ricbxMBLs.Items.Add(icbi);
            }
        }

        List<AirContainerCargoList> _allCargos;

        public void SetAllCargoList(List<AirContainerCargoList> cargos, WorkItem workItem)
        {
            _allCargos = cargos;
            this.Workitem = workItem;
        }

        /// <summary>
        /// TODO: 弹出MBL列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ricbxMBLs_Enter(object sender, EventArgs e)
        {
        }

        void ContainerCargosList_Load(object sender, EventArgs e)
        {
            this.bsContainerCargo.PositionChanged += new EventHandler(bsContainerCargo_PositionChanged);

            this.gvCargo.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCargo_BeforeLeaveRow);
            this.gvCargo.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvCargo_CellValueChanging);
            this.gvCargo.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvCargo_CellValueChanged);
        }

        void gvCargo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (CurrentRow != null)
            //{
            //    if (e.Column == this.colQuantityUnitID
            //        || e.Column == this.colMeasurementUnitID
            //        || e.Column == this.colWeightUnitID)
            //    {
            //        if (!CheckUnitsConsistency())
            //        {
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 一个提单下的所有行，单位要一致
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvCargo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (CurrentRow != null)
            {
                if (e.Column == this.colMBLNo)
                {
                    string no = e.Value.ToString().ToLower();
                    AirMBLInfo mbl = this._bls.Find(o => o.No.ToLower() == no);

                    if (mbl != null)
                    {
                        List<AirContainerCargoList> list = bsContainerCargo.DataSource as List<AirContainerCargoList>;
                        if (list.FindAll(o => o.MBLNo.ToLower() == no).Count > 1)
                        {
                            this.gvCargo.DeleteRow(e.RowHandle);
                        }
                        else
                        {
                            CurrentRow.QuantityUnitID = mbl.QuantityUnitID;
                            CurrentRow.MeasurementUnitID = mbl.MeasurementUnitID;
                            //CurrentRow.WeightUnitID = mbl.WeightUnitID;
                        }
                    }
                }
            }
        }

        AirContainerCargoList CurrentRow
        {
            get
            {
                return this.bsContainerCargo.Current as AirContainerCargoList;
            }
        }

        void gvCargo_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (!ValidateData())
            {
                e.Allow = false;
            }
        }

        public bool ValidateData()
        {
            bool isScrr = true;
            this.gvCargo.CloseEditor();
            this.bsContainerCargo.EndEdit();

            List<AirContainerCargoList> list = bsContainerCargo.DataSource as List<AirContainerCargoList>;

            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Validate
                        (
                               delegate(ValidateEventArgs e)
                               {
                                   if (item.Quantity <= 0)
                                   {
                                       e.SetErrorInfo("Quantity", LocalData.IsEnglish ? "Quantity can't be 0." : "件数不能为0.");
                                   }
                                   if (item.Measurement <= 0)
                                   {
                                       e.SetErrorInfo("Measurement", LocalData.IsEnglish ? "Measurement can't be 0." : "体积不能为0.");
                                   }
                                   if (item.Weight <= 0)
                                   {
                                       e.SetErrorInfo("Weight", LocalData.IsEnglish ? "Weight can't be 0." : "重量不能为0.");
                                   }
                                   if (_allCargos.FindAll(o => o.MBLNo == item.MBLNo && o.WeightUnitID != item.WeightUnitID).Count > 0)
                                   {
                                       e.SetErrorInfo("WeightUnitID", LocalData.IsEnglish ? 
                                           "WeightUnit must be same in one MBL " + item.MBLNo + "." 
                                           : 
                                           item.MBLNo + "下的重量单位必须保持一致.");
                                   }
                                   if (_allCargos.FindAll(o => o.MBLNo == item.MBLNo && o.QuantityUnitID != item.QuantityUnitID).Count > 0)
                                   {
                                       e.SetErrorInfo("QuantityUnitID", LocalData.IsEnglish ? 
                                           "QuantityUnit must be same in one MBL " + item.MBLNo + "." 
                                           : 
                                           item.MBLNo + "下的数量单位必须保持一致.");
                                   }
                                   if (_allCargos.FindAll(o => o.MBLNo == item.MBLNo && o.MeasurementUnitID != item.MeasurementUnitID).Count > 0)
                                   {
                                       e.SetErrorInfo("MeasurementUnitID", LocalData.IsEnglish ? 
                                           "Measurement Unit must be same in one MBL " + item.MBLNo + "." 
                                           :
                                           item.MBLNo + "下的体积单位必须保持一致.");
                                   }
                               }
                        ) == false) isScrr = false;

                    //if (Utility.ValidateContainerNumber(item.No) == false) isScrr = false;
                }
            }

            return isScrr;
        }



        bool CheckUnitsConsistency()
        {
            List<AirContainerCargoList> list = bsContainerCargo.DataSource as List<AirContainerCargoList>;
            foreach (var item in list)
            {
                if (list.FindAll(o => o.BLID == item.BLID && o.WeightUnitID == item.WeightUnitID).Count > 0)
                {
                }
            }
            return true;
        }

        void SetCnText()
        {
            this.colMBLNo.Caption = "主提单号";
            colMeasurement.Caption = "体积";
            this.colMeasurementUnitID.Caption = "体积单位";
            colQuantity.Caption = "数量";
            this.colQuantityUnitID.Caption = "数量单位";
            colWeight.Caption = "重量";
            colWeightUnitID.Caption = "重量单位";

            colMarks.Caption = "唛头";
            colCommodity.Caption = "品名";

            this.bbiNew.Caption = "新增";
            this.bbiRemove.Caption = "删除";
        }

        public bool IsChanged
        {
            get
            {
                this.gvCargo.CloseEditor();
                foreach (AirContainerCargoList item in this._allCargos)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public new event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        void bsContainerCargo_PositionChanged(object sender, EventArgs e)
        {
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.ValidateData())
            {
                return;
            }

            this.gvCargo.BeforeLeaveRow -= new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCargo_BeforeLeaveRow);
            
            AirContainerCargoList ctn = new AirContainerCargoList(); 
            ctn.BeginEdit();
            //ctn.ID = Guid.NewGuid();
            ctn.CreateByID = LocalData.UserInfo.LoginID;
            ctn.CreateDate = DateTime.Now;
            ctn.IsDirty = true;
            ctn.AirContainerID = this.ContainerId;
            //if (this.BookingInfo.QuantityUnitID.HasValue)
            //{
            //    ctn.QuantityUnitID = this.BookingInfo.QuantityUnitID.Value;
            //}

            //if (this.BookingInfo.MeasurementUnitID.HasValue)
            //{
            //    ctn.MeasurementUnitID = this.BookingInfo.MeasurementUnitID.Value;
            //}

            //if (this.BookingInfo.WeightUnitID.HasValue)
            //{
            //    ctn.WeightUnitID = this.BookingInfo.WeightUnitID.Value;
            //}

            bsContainerCargo.List.Add(ctn);
            bsContainerCargo.EndEdit();

            this.gvCargo.ClearSorting();

            this._allCargos.Add(ctn);

            this.bsContainerCargo.ResetBindings(false);

            this.gvCargo.MoveLast();

            this.RefreshEnabled();
            
            this.gvCargo.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCargo_BeforeLeaveRow);            
        }

        private void bbiRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            if (this.bsContainerCargo.Current != null)
            {
                AirContainerCargoList cargo = this.bsContainerCargo.Current as AirContainerCargoList;

                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Delete Current Data?" : "是否确认删除当前数据?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
                { 
                    return; 
                }

                bool isDeleteNewRow = cargo.ID == Guid.Empty;

                if (isDeleteNewRow)
                {
                    gvCargo.DeleteRow(gvCargo.FocusedRowHandle);
                    this._allCargos.Remove(cargo);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                }
                else
                {
                    oceanExportService.RemoveAirContainerCargoInfo(cargo.ID, LocalData.UserInfo.LoginID, cargo.UpdateDate);
                    gvCargo.DeleteRow(gvCargo.FocusedRowHandle);
                    this._allCargos.Remove(cargo);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                    if (Saved != null)
                    {
                        Saved(new object[] { this.DataSource });
                    }
                }
            }

            this.RefreshEnabled();
        }

        public Guid ContainerId { get; private set; }

        public void SetCargos(Guid containerId)
        {
            this.ContainerId = containerId;
            //this.bsContainerCargo.DataSource = oeService.GetAirContainerCargoList(containerId);
            //this.bsContainerCargo.ResetBindings(false);
            //this.gvCargo.RefreshData();

            this.bsContainerCargo.DataSource = this._allCargos.FindAll(o => o.AirContainerID == containerId);
            this.bsContainerCargo.ResetBindings(false);

            this.RefreshEnabled();
        }

        void RefreshEnabled()
        {
            this.bbiNew.Enabled = this.ContainerId != Guid.Empty;
            this.bbiRemove.Enabled = this.gvCargo.GetFocusedRow() != null;
        }

        /// <summary>
        /// 由于网格的一个列上又要选择Guid又要允许输入新的MBL号，所以用了这个变通的办法。
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        Guid FindMBLId(string no)
        {
            AirMBLInfo mbl = this._bls.Find(o => o.No == no);
            if (mbl != null)
            {
                return mbl.ID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        List<Guid> GetUniqueInvolvedContainerIds(List<AirContainerCargoList> containers)
        {
            List<Guid> containerIds = new List<Guid>();
            foreach (AirContainerCargoList cargo in containers)
            {
                if (containerIds.FindAll(o => o == cargo.AirContainerID).Count ==0 )
                {
                    containerIds.Add(cargo.AirContainerID);
                }
            }

            return containerIds;
        }

        public List<CargoSaveRequest> CollectCargosData()
        {
            if (!this.ValidateData())
            {
                throw new InvalidAttributeException();
            }

            if (this._allCargos.Count == 0)
            {
                return null;
            }

            List<AirContainerCargoList> listChangedCargos = this._allCargos.FindAll(o => o.IsDirty);

            if (listChangedCargos.Count == 0)
            {
                return null;
            }

            List<Guid> containerIds = this.GetUniqueInvolvedContainerIds(listChangedCargos);

            List<CargoSaveRequest> collection = new List<CargoSaveRequest>();
            Transaction transaction = new Transaction();

            foreach (Guid containerId in containerIds)
            {
                List<AirContainerCargoList> listContainerCargos = listChangedCargos.FindAll(o => o.AirContainerID == containerId);

                #region 收集数据

                List<Guid> cargoMBLIds = new List<Guid>();
                List<string> cargoMBLNos = new List<string>();
                List<Guid?> cargoIds = new List<Guid?>();
                List<string> cargoMarks = new List<string>();
                List<string> cargoComodities = new List<string>();
                List<int> cargoQuantities = new List<int>();
                List<Guid> cargoQuantityUnits = new List<Guid>();
                List<decimal> cargoWeights = new List<decimal>();
                List<Guid> cargoWeightUnits = new List<Guid>();
                List<decimal> cargoMeasurements = new List<decimal>();
                List<Guid> cargoMeasurementUnits = new List<Guid>();

                List<DateTime?> cargoUpdateDates = new List<DateTime?>();

                foreach (AirContainerCargoList item in listContainerCargos)
                {
                    cargoMBLIds.Add(this.FindMBLId(item.MBLNo));
                    cargoMBLNos.Add(item.MBLNo);
                    cargoIds.Add(item.ID);
                    cargoMarks.Add(item.Marks);
                    cargoComodities.Add(item.Commodity);
                    cargoQuantities.Add(item.Quantity);
                    cargoQuantityUnits.Add(item.QuantityUnitID);
                    cargoWeights.Add(item.Weight);
                    cargoWeightUnits.Add(item.WeightUnitID);
                    cargoMeasurements.Add(item.Measurement);
                    cargoMeasurementUnits.Add(item.MeasurementUnitID);
                    cargoUpdateDates.Add(item.UpdateDate);
                }

                #endregion

                CargoSaveRequest saveRequest = new CargoSaveRequest();

                saveRequest.transactionId = transaction.TransactionId;
                saveRequest.containerID = containerId;
                saveRequest.mblIDs = cargoMBLIds.ToArray();
                saveRequest.mblNos = cargoMBLNos.ToArray();
                saveRequest.ids = cargoIds.ToArray();
                saveRequest.marks = cargoMarks.ToArray();
                saveRequest.commodities = cargoComodities.ToArray();
                saveRequest.quantities = cargoQuantities.ToArray();
                saveRequest.quantityUnitIDs = cargoQuantityUnits.ToArray();
                saveRequest.weights = cargoWeights.ToArray();
                saveRequest.weightUnitIDs = cargoWeightUnits.ToArray();
                saveRequest.measurements = cargoMeasurements.ToArray();
                saveRequest.measurementUnitIDs = cargoMeasurementUnits.ToArray();
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.updateDates = cargoUpdateDates.ToArray();

                //ManyResult result = oceanExportService.SaveAirMBLContainerCargoInfo(saveRequest);

                //for (int i = 0; i < result.Items.Count; i++)
                //{
                //    listContainerCargos[i].ID = result.Items[i].GetValue<Guid>("ID");
                //    listContainerCargos[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");

                //    string mblNo = listChangedCargos[i].MBLNo;
                //    if (this._bls.Find(o => o.No == mblNo) == null)
                //    {
                //        this.AddMBL(result.Items[i].GetValue<Guid>("AirMBLID"), mblNo,
                //            listContainerCargos[i].QuantityUnitID,
                //            listContainerCargos[i].WeightUnitID,
                //            listContainerCargos[i].MeasurementUnitID);
                //    }
                //    listContainerCargos[i].IsDirty = false;
                //}

                //this.bsContainerCargo.ResetBindings(false);

                listContainerCargos.ForEach(o => saveRequest.AddInvolvedObject(o));
                collection.Add(saveRequest);
            }

            return collection;
        }

        public void RefreshUI(List<CargoSaveRequest> list)
        {
            foreach (CargoSaveRequest command in list)
            {
                List<AirContainerCargoList> cargos = command.UnBoxInvolvedObject<AirContainerCargoList>();// (List<AirContainerCargoList>)command.InvolvedObjects;

                ManyResult result = command.ManyResult;
                for (int i = 0; i < result.Items.Count; i++)
                {
                    cargos[i].ID = result.Items[i].GetValue<Guid>("ID");
                    cargos[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");

                    string mblNo = cargos[i].MBLNo;
                    if (this._bls.Find(o => o.No == mblNo) == null)
                    {
                        this.AddMBL(result.Items[i].GetValue<Guid>("AirMBLID"), mblNo,
                            cargos[i].QuantityUnitID,
                            cargos[i].WeightUnitID,
                            cargos[i].MeasurementUnitID);
                    }
                    cargos[i].IsDirty = false;
                }
            }

            this.bsContainerCargo.ResetBindings(false);

            this.RefreshEnabled();
        }
    }
}
