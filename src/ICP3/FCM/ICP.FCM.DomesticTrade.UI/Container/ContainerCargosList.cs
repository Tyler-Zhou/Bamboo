using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.Common.UI;

namespace ICP.FCM.DomesticTrade.UI.Container
{
    /// <summary>
    /// 箱下的货物列表
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class ContainerCargosList : BaseEditPart
    {
        public WorkItem Workitem { get; set; }

        public DTBookingInfo BookingInfo { get; set; }

        IDomesticTradeService DomesticTradeService
        {
            get
            {
                return ServiceClient.GetService<IDomesticTradeService>();
            }
        }
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
              return  ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public ContainerCargosList()
        {
            InitializeComponent();

            Load += new EventHandler(ContainerCargosList_Load);

            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
            Disposed += delegate
            {
                lwgrid.DataSource = null;
                BookingInfo = null;
                _allCargos = null;
                bsContainerCargo.PositionChanged -= bsContainerCargo_PositionChanged;
                bsContainerCargo.DataSource = null;
                bsContainerCargo.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        void InitControls()
        {

            ricbxMBLs.Enter += new EventHandler(ricbxMBLs_Enter);

            //包装
            //Utility.SetEnterToExecuteOnec(this.rcmbQuantityUnit, delegate
            //{
            ICPCommUIHelper.SetCmbDataDictionary(rcmbQuantityUnit, DataDictionaryType.QuantityUnit);
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

        //List<DTMBLInfo> _bls;
        //public void SetMBLs(List<OceanMBLInfo> bls)
        //{
        //    this._bls = bls;
        //    this.SetMBLCombox();

        //    InitControls();
        //}

        //void AddMBL(Guid mblId, string mblNo,Guid quantityUnitId,Guid weightUnitId,Guid measurementUnitId)
        //{
        //    this._bls.RemoveAll(o => o.MBLID == mblId);
        //    this._bls.Add(
        //        new OceanMBLInfo { 
        //        No=mblNo, 
        //        ID =mblId, 
        //        QuantityUnitID=quantityUnitId, 
        //        WeightUnitID=weightUnitId,
        //        MeasurementUnitID=measurementUnitId }
        //        );

        //    this.SetMBLCombox();
        //}

        //void SetMBLCombox()
        //{
        //    this.ricbxMBLs.Items.Clear();
        //    foreach (DTMBLInfo info in this._bls)
        //    {                
        //        DevExpress.XtraEditors.Controls.ImageComboBoxItem icbi = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
        //        icbi.Description = info.ID.ToString();
        //        icbi.Value = info.No;

        //        this.ricbxMBLs.Items.Add(icbi);
        //    }
        //}

        List<DTContainerCargoList> _allCargos;

        public void SetAllCargoList(List<DTContainerCargoList> cargos, WorkItem workItem)
        {
            _allCargos = cargos;
            Workitem = workItem;
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
            bsContainerCargo.PositionChanged += new EventHandler(bsContainerCargo_PositionChanged);

            gvCargo.BeforeLeaveRow += new RowAllowEventHandler(gvCargo_BeforeLeaveRow);
        }

 


        DTContainerCargoList CurrentRow
        {
            get
            {
                return bsContainerCargo.Current as DTContainerCargoList;
            }
        }

        void gvCargo_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (!ValidateData())
            {
                e.Allow = false;
            }
        }

        public bool ValidateData()
        {
            bool isScrr = true;
            gvCargo.CloseEditor();
            bsContainerCargo.EndEdit();

            List<DTContainerCargoList> list = bsContainerCargo.DataSource as List<DTContainerCargoList>;

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
            List<DTContainerCargoList> list = bsContainerCargo.DataSource as List<DTContainerCargoList>;
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
            colMBLNo.Caption = "主提单号";
            colMeasurement.Caption = "体积";
            colMeasurementUnitID.Caption = "体积单位";
            colQuantity.Caption = "数量";
            colQuantityUnitID.Caption = "数量单位";
            colWeight.Caption = "重量";
            colWeightUnitID.Caption = "重量单位";

            colMarks.Caption = "唛头";
            colCommodity.Caption = "品名";

            bbiNew.Caption = "新增";
            bbiRemove.Caption = "删除";
        }

        public bool IsChanged
        {
            get
            {
                gvCargo.CloseEditor();
                foreach (DTContainerCargoList item in _allCargos)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public new event SavedHandler Saved;

        void bsContainerCargo_PositionChanged(object sender, EventArgs e)
        {
        }

        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

            gvCargo.BeforeLeaveRow -= new RowAllowEventHandler(gvCargo_BeforeLeaveRow);
            
            DTContainerCargoList ctn = new DTContainerCargoList(); 
            ctn.BeginEdit();
            //ctn.ID = Guid.NewGuid();
            ctn.CreateByID = LocalData.UserInfo.LoginID;
            ctn.CreateDate = DateTime.Now;
            ctn.IsDirty = true;
            ctn.OceanContainerID = ContainerId;
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

            gvCargo.ClearSorting();

            _allCargos.Add(ctn);

            bsContainerCargo.ResetBindings(false);

            gvCargo.MoveLast();

            RefreshEnabled();
            
            gvCargo.BeforeLeaveRow += new RowAllowEventHandler(gvCargo_BeforeLeaveRow);            
        }

        private void bbiRemove_ItemClick(object sender, ItemClickEventArgs e)
        {            
            if (bsContainerCargo.Current != null)
            {
                DTContainerCargoList cargo = bsContainerCargo.Current as DTContainerCargoList;

                if (XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Delete Current Data?" : "是否确认删除当前数据?"
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
                    _allCargos.Remove(cargo);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                }
                else
                {
                    DomesticTradeService.RemoveDTContainerCargoInfo(cargo.ID, LocalData.UserInfo.LoginID, cargo.UpdateDate,LocalData.IsEnglish);
                    gvCargo.DeleteRow(gvCargo.FocusedRowHandle);
                    _allCargos.Remove(cargo);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                    if (Saved != null)
                    {
                        Saved(new object[] { DataSource });
                    }
                }
            }

            RefreshEnabled();
        }

        public Guid ContainerId { get; private set; }

        public void SetCargos(Guid containerId)
        {
            ContainerId = containerId;
            //this.bsContainerCargo.DataSource = oeService.GetOceanContainerCargoList(containerId);
            //this.bsContainerCargo.ResetBindings(false);
            //this.gvCargo.RefreshData();

            bsContainerCargo.DataSource = _allCargos.FindAll(o => o.OceanContainerID == containerId);
            bsContainerCargo.ResetBindings(false);

            RefreshEnabled();
        }

        void RefreshEnabled()
        {
            bbiNew.Enabled = ContainerId != Guid.Empty;
            bbiRemove.Enabled = gvCargo.GetFocusedRow() != null;
        }

        /// <summary>
        /// 由于网格的一个列上又要选择Guid又要允许输入新的MBL号，所以用了这个变通的办法。
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        Guid FindMBLId(string no)
        {
            //DTMBLInfo mbl = this._bls.Find(o => o.No == no);
            //if (mbl != null)
            //{
            //    return mbl.ID;
            //}
            //else
            //{
            //    return Guid.Empty;
            //}
            return Guid.Empty;
        }

        List<Guid> GetUniqueInvolvedContainerIds(List<DTContainerCargoList> containers)
        {
            List<Guid> containerIds = new List<Guid>();
            foreach (DTContainerCargoList cargo in containers)
            {
                if (containerIds.FindAll(o => o == cargo.OceanContainerID).Count ==0 )
                {
                    containerIds.Add(cargo.OceanContainerID);
                }
            }

            return containerIds;
        }

        public List<CargoSaveRequest> CollectCargosData()
        {
            if (!ValidateData())
            {
                throw new InvalidAttributeException();
            }

            if (_allCargos.Count == 0)
            {
                return null;
            }

            List<DTContainerCargoList> listChangedCargos = _allCargos.FindAll(o => o.IsDirty);

            if (listChangedCargos.Count == 0)
            {
                return null;
            }

            List<Guid> containerIds = GetUniqueInvolvedContainerIds(listChangedCargos);

            List<CargoSaveRequest> collection = new List<CargoSaveRequest>();
            Transaction transaction = new Transaction();

            foreach (Guid containerId in containerIds)
            {
                List<DTContainerCargoList> listContainerCargos = listChangedCargos.FindAll(o => o.OceanContainerID == containerId);

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

                foreach (DTContainerCargoList item in listContainerCargos)
                {
                    cargoMBLIds.Add(FindMBLId(item.MBLNo));
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

                //ManyResult result = oceanExportService.SaveOceanMBLContainerCargoInfo(saveRequest);

                //for (int i = 0; i < result.Items.Count; i++)
                //{
                //    listContainerCargos[i].ID = result.Items[i].GetValue<Guid>("ID");
                //    listContainerCargos[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");

                //    string mblNo = listChangedCargos[i].MBLNo;
                //    if (this._bls.Find(o => o.No == mblNo) == null)
                //    {
                //        this.AddMBL(result.Items[i].GetValue<Guid>("OceanMBLID"), mblNo,
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
                List<DTContainerCargoList> cargos = command.UnBoxInvolvedObject<DTContainerCargoList>();// (List<OceanContainerCargoList>)command.InvolvedObjects;

                ManyResult result = command.ManyResult;
                for (int i = 0; i < result.Items.Count; i++)
                {
                    cargos[i].ID = result.Items[i].GetValue<Guid>("ID");
                    cargos[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");

                    string mblNo = cargos[i].MBLNo;
                    //if (this._bls.Find(o => o.No == mblNo) == null)
                    //{
                    //    this.AddMBL(result.Items[i].GetValue<Guid>("OceanMBLID"), mblNo,
                    //        cargos[i].QuantityUnitID,
                    //        cargos[i].WeightUnitID,
                    //        cargos[i].MeasurementUnitID);
                    //}
                    cargos[i].IsDirty = false;
                }
            }

            bsContainerCargo.ResetBindings(false);

            RefreshEnabled();
        }
    }
}
