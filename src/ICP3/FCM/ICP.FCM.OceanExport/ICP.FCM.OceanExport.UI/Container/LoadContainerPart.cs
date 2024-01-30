using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI.SmartParts;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;

namespace ICP.FCM.OceanExport.UI.Container
{
    /// <summary>
    /// 订舱单装箱界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class LoadContainerPart : BaseEditPart
    {
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        #endregion

        #region 成员
        /// <summary>
        /// 
        /// </summary>
        private bool isInited = false;
        /// <summary>
        /// DEV里的新行常量
        /// </summary>
        const int newRowHandle = int.MinValue + 1;
        /// <summary>
        /// 
        /// </summary>
        private object synObj = new object();
        /// <summary>
        /// 
        /// </summary>
        OceanBookingInfo bookingInfo = null;
        /// <summary>
        /// 
        /// </summary>
        ContainerCargosList _ccl;
        /// <summary>
        /// 
        /// </summary>
        List<ContainerList> _containerTypes;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cco"></param>
        delegate void DataBindDelegate(CompositeContainerObjects cco);
        /// <summary>
        /// 
        /// </summary>
        public override event SavedHandler Saved;
        #endregion

        #region Property

        /// <summary>
        /// 各个区域的数据是否有改动
        /// </summary>
        bool IsChanged
        {
            get
            {
                this.EndEdit();
                this.bsContainer.EndEdit();
                if (CurrentRow == null)
                {
                    return false;
                }

                return CurrentRow.IsDirty || this.poItemPart1.IsChanged
                    || this._ccl.IsChanged;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        OceanContainerList CurrentRow
        {
            get
            {
                return bsContainer.Current as OceanContainerList;
            }
        }
        #endregion

        #region init
        /// <summary>
        /// 
        /// </summary>
        public LoadContainerPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
            this.Disposed += delegate
            {
                this.Saved = null;
                
                this.bsContainer.PositionChanged -= this.bsContainer_PositionChanged;
                this.ctnGridControl.DataSource = null;
                this.bsBookingInfo.DataSource = null;
                this.bsContainer.DataSource = null;
                this.bsBookingInfo.Dispose();
                this.bsContainer.Dispose();
                this.bookingInfo = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetCnText()
        {
            colNo.Caption = "箱号";
            colSealNo.Caption = "封号";
            colType.Caption = "箱型";

            barAdd.Caption = "新增(&A)";
            barSave.Caption = "保存(&S)";
            barDelete.Caption = "删除(&D)";
            barPrint.Caption = "打印(&P)";
            barClose.Caption = "关闭(&C)";

            labOperationNo.Text = "业务号";
            labArriveDate.Text = "到达时间";
            labCarNo.Text = "车牌号";
            labDeliveryDate.Text = "出发时间";
            labDriver.Text = "司机";
            labNo.Text = "箱号";
            labOperationNo.Text = "业务号";
            labPOD.Text = "卸货港";
            labPOL.Text = "装货港";
            labReturnDate.Text = "还柜时间";
            labReturnLocation.Text = "还柜地";
            labSealNo.Text = "封号";
            labShippingOrderNo.Text = "订舱号";
            labType.Text = "箱型";
            labVessel.Text = "船名航次";

            groupBookingInfo.Text = "订舱信息";
            xtraTabControl1.TabPages[0].Text = "货物";

            barNew.Caption = "新增(&N)";
            barRemove.Caption = "删除(&R)";

        }
        /// <summary>
        /// 
        /// </summary>
        private void InnerLoad()
        {
            lock (synObj)
            {
                if (!isInited)
                {
                    isInited = true;
                    if (this.bsContainer.Count == 0)
                    {
                        this.barAdd_ItemClick(null, null);
                    }

                    this.txtNo.Select();
                }
            }
        }
        /// <summary>
        /// 新箱子的ContainerId必须被正确传递到子面板才行
        /// 所以离开前一定要提示保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvCtn_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            //if (this.IsChanged)
            //{
            //    DialogResult dialogResult = PartLoader.EnquireIsSaveCurrentDataByUpdated();
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        if (!this.Save())
            //        {
            //            e.Allow = false;
            //        }
            //    }
            //    else if (dialogResult == DialogResult.No)
            //    {
            //        if (this.CurrentRow.IsNew)
            //        {
            //            this.bsContainer.RemoveCurrent();
            //        }
            //    }
            //    else
            //    {
            //        e.Allow = false;
            //    }
            //}

            this.poItemPart1.SaveTempRelations();

            if (!this.ValidateData())
            {
                e.Allow = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvCtn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.gvCtn.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvCtn_FocusedRowChanged);
            if (this._currentContainerId != Guid.Empty)
            {
                BindingList<OceanContainerList> containers = this.bsContainer.DataSource as BindingList<OceanContainerList>;
                int index = containers.IndexOf(containers.First(o => o.ID == this._currentContainerId));
                if (index >= 0)
                {
                    int rowHandle = gvCtn.GetRowHandle(index);
                    if (rowHandle >= 0)
                    {
                        gvCtn.FocusedRowHandle = rowHandle;

                        this.SetCargosOrPos(this._currentContainerId);
                    }
                }
            }
            else
            {
                this.SetCargosOrPos(this.CurrentRow.ID);
            }
        }
        /// <summary>
        /// TODO: 做成延迟加载
        /// </summary>
        private void InitControls()
        {
            #region 绑定下拉列表

            //箱型
            _containerTypes = ICPCommUIHelper.SetCmbContainerType(rcmbType);
            ICPCommUIHelper.SetCmbContainerType(this.cmbType, _containerTypes);

            #endregion
        }

        /// <summary>
        /// 刷新工具栏
        /// </summary>
        private void RefreshBarEnabled()
        {
            if (CurrentRow == null)
            {
                this.barDelete.Enabled = false;
            }
            else
            {
                this.barDelete.Enabled = true;
            }

            if (bsContainer.List.Count == 0)
            {
                this.barSave.Enabled = this.barPrint.Enabled = false;
            }
            else
            {
                this.barSave.Enabled = this.barPrint.Enabled = true;
            }

            this.groupBoxCtn.Enabled = this.CurrentRow != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool ValidateData()
        {
            bool isScrr = true;

            this.EndEdit();
            this.gvCtn.CloseEditor();
            this.bsContainer.EndEdit();

            BindingList<OceanContainerList> list = bsContainer.DataSource as BindingList<OceanContainerList>;

            foreach (var item in list)
            {
                if (item.Validate
                    (
                           delegate(ValidateEventArgs e)
                           {
                               //if (item.Quantity <= 0)
                               //{
                               //    e.SetErrorInfo("Quantity", LocalData.IsEnglish ? "Quantity can't be 0." : "件数不能为0.");
                               //}
                               //if (item.Measurement <= 0)
                               //{
                               //    e.SetErrorInfo("Measurement", LocalData.IsEnglish ? "Measurement can't be 0." : "体积不能为0.");
                               //}
                               //if (item.Weight <= 0)
                               //{
                               //    e.SetErrorInfo("Weight", LocalData.IsEnglish ? "Weight can't be 0." : "重量不能为0.");
                               //}
                           }
                    ) == false) isScrr = false;

                //if (Utility.ValidateContainerNumber(item.No) == false) isScrr = false;
            }
            if (this.poItemPart1 != null && this.poItemPart1.ValidateData() == false)
                isScrr = false;
            if (this._ccl != null && this._ccl.ValidateData() == false)
                isScrr = false;
            return isScrr;
        }

        #endregion

        #region 事件

        /// <summary>
        /// TODO: 这种记录最终异常信息反馈给用户的方式，有严重bug。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            Save();
        }
        
        

        /// <summary>
        /// 保存完毕立刻更新ContainerId的值到各个子控件
        /// </summary>
        /// <returns></returns>
        List<ContainerSaveRequest> CollectContainersData()
        {
            BindingList<OceanContainerList> ctnList = bsContainer.DataSource as BindingList<OceanContainerList>;

            List<OceanContainerList> listChangedContainers = ctnList.ToList().FindAll(o => o.IsDirty);

            if (listChangedContainers.Count <= 0)
            {
                return null;
            }

            List<ContainerSaveRequest> commands = new List<ContainerSaveRequest>();

            #region 收集箱列表数据

            List<Guid?> containerIDs = new List<Guid?>();
            List<Guid> shippingOrderID = new List<Guid>(), containerTypeIDs = new List<Guid>();
            List<string> containerNos = new List<string>(), containerSOs = new List<string>(), containerSealNos = new List<string>(),
                containerMarks = new List<string>(), containerCommoditys = new List<string>();
            List<bool> containerIsSOCs = new List<bool>(), containerIsPartOfs = new List<bool>();
            List<int> containerQuantitys = new List<int>();
            List<decimal> containerWeights = new List<decimal>(), containerMeasurements = new List<decimal>();
            List<DateTime?> containerUpdateDates = new List<DateTime?>();

            List<string> containerShippingOrderNos = new List<string>();
            List<string> containerDrivers = new List<string>();
            List<string> containerCarNos = new List<string>();
            List<DateTime?> containerDeliveryTime = new List<DateTime?>();
            List<DateTime?> containerArriveTime = new List<DateTime?>();
            List<DateTime?> containerReturnTime = new List<DateTime?>();

            foreach (var item in listChangedContainers)
            {
                containerShippingOrderNos.Add(bookingInfo.OceanShippingOrderNo);

                containerIDs.Add(item.ID);
                containerNos.Add(item.No);
                containerSealNos.Add(item.SealNo);
                containerTypeIDs.Add(item.TypeID);
                containerDrivers.Add(item.DriverName);
                containerCarNos.Add(item.CarNo);
                containerArriveTime.Add(item.ArriveDate);
                containerDeliveryTime.Add(item.DeliveryDate);
                containerReturnTime.Add(item.ReturnDate);
                containerUpdateDates.Add(item.UpdateDate);
            }

            #endregion

            ContainerSaveRequest saveRequest = new ContainerSaveRequest();

            saveRequest.oceanBookingID = bookingInfo.ID;
            saveRequest.containerShippingOrderNos = containerShippingOrderNos.ToArray();
            saveRequest.containerIDs = containerIDs.ToArray();
            saveRequest.containerNos = containerNos.ToArray();
            saveRequest.containerTypeIDs = containerTypeIDs.ToArray();
            saveRequest.containerSealNos = containerSealNos.ToArray();
            saveRequest.deliveryDates = containerDeliveryTime.ToArray();
            saveRequest.arriveDates = containerArriveTime.ToArray();
            saveRequest.returnDates = containerReturnTime.ToArray();
            saveRequest.driverNames = containerDrivers.ToArray();
            saveRequest.carNos = containerCarNos.ToArray();
            saveRequest.saveByID = LocalData.UserInfo.LoginID;
            saveRequest.containerUpdateDates = containerUpdateDates.ToArray();
            saveRequest.isEnglish = LocalData.IsEnglish;

            listChangedContainers.ForEach(o => saveRequest.AddInvolvedObject(o));

            commands.Add(saveRequest);

            return commands;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsChanged && SaveData() == false) return;

            if (CurrentRow == null || CurrentRow.IsNew) return;

            OceanExportPrintHelper.PrintOELoadContainer(CurrentRow.ID, bookingInfo.ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Delete Current Data?" : "是否确认删除当前数据?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
            { return; }

            bool isDeleteNewRow = CurrentRow.ID == Guid.Empty;

            if (isDeleteNewRow)
            {
                gvCtn.DeleteRow(gvCtn.FocusedRowHandle);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
            }
            else
            {
                try
                {
                    OceanExportService.RemoveOceanContaierInfo(new Guid[] { CurrentRow.ID }, LocalData.UserInfo.LoginID, new DateTime?[] { CurrentRow.UpdateDate });
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                    gvCtn.DeleteRow(gvCtn.FocusedRowHandle);
                    if (Saved != null)
                    {
                        Saved(new object[] { this.DataSource });
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete failed" : "删除失败");
                }
            }

            this.RefreshBarEnabled();
            this.SetChildParts();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.poItemPart1.SaveTempRelations();

            if (!this.ValidateData())
            {
                return;
            }


            this.gvCtn.BeforeLeaveRow -= new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCtn_BeforeLeaveRow);
            OceanContainerList ctn = new OceanContainerList();

            ctn.ID = Guid.NewGuid();
            ctn.CreateByID = LocalData.UserInfo.LoginID;
            ctn.CreateDate = DateTime.Now;
            ctn.BeginEdit();
            ctn.IsDirty = true;

            bsContainer.Add(ctn);
            bsContainer.EndEdit();

            SetSelected(ctn.ID);
            gvCtn.RefreshData();
            gvCtn.ClearSelection();
            gvCtn.SelectRow(this.gvCtn.FocusedRowHandle);

            //this.gvCtn.SelectRow(newRowHandle);
            this.txtNo.Select();
            RefreshBarEnabled();

            this.gvCtn.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCtn_BeforeLeaveRow);

            this.SetChildParts();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
        /// <summary>
        /// 
        /// </summary>
        void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.SetChildParts();
        }

        #region Changed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bsContainer_PositionChanged(object sender, EventArgs e)
        {
            this.SetChildParts();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerId"></param>
        void SetCargosOrPos(Guid containerId)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.tabPagePO)
            {
                this.poItemPart1.SetRelationShips(containerId);
            }
            else
            {
                this._ccl.SetCargos(containerId);
            }
        }

        #endregion

        #endregion

        #region IEditPart 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cco"></param>
        private void InnerBindData(CompositeContainerObjects cco)
        {
            if (cco == null)
            {
                //throw new Exception(""); 
                return;
            }

            this._ccl = new ContainerCargosList();
            this._ccl.Dock = DockStyle.Fill;
            this.tabPageCargo.Controls.Add(this._ccl);



            this.bsBookingInfo.DataSource = cco.BookingInfo;

            this.bookingInfo = cco.BookingInfo;

            BindingList<OceanContainerList> containerList = new BindingList<OceanContainerList>();
            foreach (OceanContainerList container in cco.ContainerList)
            {
                containerList.Add(container);
            }


            this._ccl.SetAllCargoList(cco.CargoList);
            this._ccl.BookingInfo = cco.BookingInfo;
            this._ccl.SetMBLs(cco.MBLs);
            poItemPart1.OperationID = bookingInfo.ID;
            poItemPart1.OperationType = OperationType.OceanExport;
            poItemPart1.SetAllPO(bookingInfo.ID);
            poItemPart1.SetHBLCombox(cco.HBLs);
            this.bsContainer.DataSource = containerList;
            InitControls();
            RefreshBarEnabled();
            InnerLoad();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void BindingData(object value)
        {
            CompositeContainerObjects cco = OceanExportService.GetCompositeContainerObjects(this._oceanBookingId);
            InnerBindData(cco);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerId"></param>
        void SetSelected(Guid containerId)
        {
            BindingList<OceanContainerList> containerList = this.bsContainer.DataSource as BindingList<OceanContainerList>;

            int index = containerList.IndexOf(containerList.First(o => o.ID == containerId));

            this.bsContainer.Position = index;
        }
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get { return bsContainer.DataSource; }
            set { BindingData(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return this.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void EndEdit()
        {
            bsContainer.EndEdit();
            this.Validate();
        }
        #endregion

        #region IPart 成员
        Guid _BLID = Guid.Empty;
        Guid _ShippingOrderID = Guid.Empty;

        Guid _currentContainerId = Guid.Empty;
        Guid _oceanBookingId;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            _oceanBookingId = (Guid)values["OceanBookingId"];
            foreach (var item in values)
            {
                if (item.Key == "BLID")
                {
                    _BLID = new Guid(item.Value.ToString());
                }
                if (item.Key == "ShippingOrderID")
                {
                    _ShippingOrderID = new Guid(item.Value.ToString());
                }

                if (item.Key == "ContainerId")
                {
                    _currentContainerId = (Guid)item.Value;
                }
            }
        }
        #endregion
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void RefreshUI(List<ContainerSaveRequest> list)
        {
            foreach (ContainerSaveRequest saveRequest in list)
            {
                ManyResult result = saveRequest.ManyResult;
                List<OceanContainerList> listChangedContainers = saveRequest.UnBoxInvolvedObject<OceanContainerList>();

                for (int i = 0; i < result.Items.Count; i++)
                {
                    listChangedContainers[i].ID = result.Items[i].GetValue<Guid>("ID");
                    listChangedContainers[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    listChangedContainers[i].IsDirty = false;
                }
            }

            bsContainer.ResetBindings(false);
            SetCargosOrPos(this.CurrentRow.ID);
        }
        /// <summary>
        /// 
        /// </summary>
        void SetChildParts()
        {
            List<OceanContainerList> containers = bsContainer.DataSource as List<OceanContainerList>;
            if (containers!=null)
            {
                poItemPart1.SetContainerCombox(containers);
            }
            if (xtraTabControl1.SelectedTabPage == this.tabPagePO)
            {
                if (CurrentRow != null)
                {
                    poItemPart1.SetRelationShips(this.CurrentRow.ID);
                }
            }
            else
            {
                if (CurrentRow != null)
                {
                    _ccl.SetCargos(CurrentRow.ID);
                }
                _ccl.Enabled = CurrentRow != null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Save()
        {
            if (!ValidateData())
            {
                return false;
            }

            if (!IsChanged)
            {
                return true;
            }

            List<ContainerSaveRequest> originalContainers = CollectContainersData();

            // TODO: 刷新当前焦点行，设置其ContainerId的值至各个子面板
            //if (!isSaveCtnSuccessfully)
            //{
            //    return false;
            //}
            //else
            //{
            //    this.SetCargosOrPos(this.CurrentRow.ID);
            //}

            //Fetch data from control
            //List<CargoSaveRequest> originalCargos = this._ccl.CollectCargo();

            List<CargoSaveRequest> originalCargos = this._ccl.CollectCargosData();
            //List<POSaveRequest> originalPos = null;

            //if (this.poItemPart1.IsChanged)
            //{
            //    originalPos = this.poItemPart1.CollectPosData();
            //}

            //PoItemContainersRelationSaveRequest relations = this.poItemPart1.GetRelations();

            //if (relations != null)
            //{
            //    BindingList<OceanContainerList> ctns = bsContainer.DataSource as BindingList<OceanContainerList>;

            //    if (ctns != null)
            //    {
            //        foreach (OceanContainerList ctn in ctns)
            //        {
            //            if (relations.poContainerIds.ToList().FindAll(o => o == ctn.ID).Count == 0)
            //            {
            //                List<Guid> temp = relations.poContainerIds.ToList();
            //                temp.Add(ctn.ID);
            //                relations.poContainerIds = temp.ToArray();
            //            }

            //            if (relations.itemContainerIds.ToList().FindAll(o => o == ctn.ID).Count == 0)
            //            {
            //                List<Guid> temp = relations.itemContainerIds.ToList();
            //                temp.Add(ctn.ID);
            //                relations.itemContainerIds = temp.ToArray();
            //            }
            //        }
            //    }
            //}

            List<SaveRequestPurchaseOrderItem> originalPOData = poItemPart1.BuildSaveRequest();


            Dictionary<Guid, SaveResponse> saved = OceanExportService.SaveContainerCargos(originalContainers,
                originalCargos, originalPOData);

            if (originalContainers != null)
            {
                SaveResponse.Analyze(originalContainers.Cast<SaveRequest>().ToList(), saved, true);
                this.RefreshUI(originalContainers);
            }

            if (originalCargos != null)
            {
                SaveResponse.Analyze(originalCargos.Cast<SaveRequest>().ToList(), saved, true);
                this._ccl.RefreshUI(originalCargos);
            }

            AfterSave();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        void AfterSave()
        {
            if (Saved != null)
            {
                BindingList<OceanContainerList> list = this.bsContainer.DataSource as BindingList<OceanContainerList>;
                if (list != null)
                {
                    foreach (OceanContainerList item in list)
                    {
                        if (string.IsNullOrEmpty(item.TypeName))
                        {
                            bool isChanged = item.IsDirty;
                            item.TypeName = this._containerTypes.First(o => o.ID == item.TypeID).Code;
                            item.IsDirty = isChanged;//避免设置TypeName时影响其IsChanged属性
                        }
                    }
                    Saved(new object[] { this.bookingInfo,
                list.Cast<OceanContainerList>().ToList()});
                }
            }
        }

    }
}
