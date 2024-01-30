using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;

namespace ICP.FCM.DomesticTrade.UI.Container
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class LoadContainerPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IOrganizationService organizationService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DomesticTradePrintHelper DomesticTradePrintHelper { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public IDTReportDataService oeReportSrvice { get; set; }

        [ServiceDependency]
        public IReportViewService reportViewService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

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

                return CurrentRow.IsDirty 
                    || this._ccl.IsChanged;
            }
        }

        DTContainerList CurrentRow
        { 
            get 
            { 
                return bsContainer.Current as DTContainerList; 
            } 
        }

        #endregion
        
        DTBookingInfo bookingInfo = null;

        #region init

        public LoadContainerPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            this.Load += new EventHandler(LoadContainerPart_Load);
        }

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
            labCarNo .Text = "车牌号";
            labDeliveryDate .Text = "出发时间";
            labDriver .Text = "司机";
            labNo .Text = "箱号";
            labOperationNo.Text = "业务号";
            labPOD.Text = "卸货港";
            labPOL.Text = "装货港";
            labReturnDate .Text = "还柜时间";
            labReturnLocation .Text = "还柜地";
            labSealNo .Text = "封号";
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LoadContainerPart_Load(object sender, EventArgs e)
        {
            this.bsContainer.PositionChanged += new EventHandler(bsContainer_PositionChanged);

            this.gvCtn.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvCtn_FocusedRowChanged);
            this.gvCtn.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCtn_BeforeLeaveRow);
            this.gvCtn.RowCountChanged += new EventHandler(gvCtn_RowCountChanged);

            if (this.bsContainer.Count == 0)
            {
                this.barAdd_ItemClick(null, null);
            }

            this.txtNo.Select();
        }

        void gvCtn_RowCountChanged(object sender, EventArgs e)
        {
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


            if (!this.ValidateData())
            {
                e.Allow = false;
            }
        }

        void gvCtn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.gvCtn.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvCtn_FocusedRowChanged);
            if (this._currentContainerId != Guid.Empty)
            {
                BindingList<DTContainerList> containers = this.bsContainer.DataSource as BindingList<DTContainerList>;
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

        List<ContainerList> _containerTypes;


        /// <summary>
        /// TODO: 做成延迟加载
        /// </summary>
        private void InitControls()
        {
            #region 绑定下拉列表

            //箱型
            _containerTypes = ICPCommUIHelperService.SetCmbContainerType(rcmbType);
            ICPCommUIHelperService.SetCmbContainerType(this.cmbType, _containerTypes);

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

            BindingList<DTContainerList> list = bsContainer.DataSource as BindingList<DTContainerList>;

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
            if (this._ccl != null && this._ccl.ValidateData() == false)
                isScrr = false;
            return isScrr;
        }

        #endregion

        #region bar

        #region Save

        /// <summary>
        /// TODO: 这种记录最终异常信息反馈给用户的方式，有严重bug。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            if (!this.ValidateData())
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

            List<CargoSaveRequest> originalCargos =  this._ccl.CollectCargosData();

            //if (this.poItemPart1.IsChanged)
            //{
            //    originalPos = this.poItemPart1.CollectPosData();
            //}

            //PoItemContainersRelationSaveRequest relations = this.poItemPart1.GetRelations();

            //if (relations != null)
            //{
            //    BindingList<DTContainerList> ctns = bsContainer.DataSource as BindingList<DTContainerList>;

            //    if (ctns != null)
            //    {
            //        foreach (DTContainerList ctn in ctns)
            //        {
            //            if (relations.poContainerIds.ToList().FindAll(o => o == ctn.ID).Count ==0)
            //            {
            //                List<Guid> temp = relations.poContainerIds.ToList();
            //                temp.Add(ctn.ID);
            //                relations.poContainerIds = temp.ToArray();
            //            }

            //            if (relations.itemContainerIds.ToList().FindAll(o => o == ctn.ID).Count ==0)
            //            {
            //                List<Guid> temp = relations.itemContainerIds.ToList();
            //                temp.Add(ctn.ID);
            //                relations.itemContainerIds = temp.ToArray();
            //            }
            //        }
            //    }
            //}









            //Dictionary<Guid,SaveResponse> saved = this.oeService.SaveContainerCargos(originalContainers,
            //    originalCargos,LocalData.IsEnglish);

            //if (originalContainers != null)
            //{
            //    SaveResponse.Analyze(originalContainers.Cast<SaveRequest>().ToList(), saved, true);
            //    this.RefreshUI(originalContainers);
            //}

            //if (originalCargos != null)
            //{
            //    SaveResponse.Analyze(originalCargos.Cast<SaveRequest>().ToList(), saved, true);
            //    this._ccl.RefreshUI(originalCargos);
            //}

            AfterSave();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            return true;
        }

        public void RefreshUI(List<ContainerSaveRequest> list)
        {
            foreach (ContainerSaveRequest saveRequest in list)
            {
                ManyResult result = saveRequest.ManyResult;
                List<DTContainerList> listChangedContainers = saveRequest.UnBoxInvolvedObject<DTContainerList>();

                for (int i = 0; i < result.Items.Count; i++)
                {
                    listChangedContainers[i].ID = result.Items[i].GetValue<Guid>("ID");
                    listChangedContainers[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    listChangedContainers[i].IsDirty = false;
                }
            }

            bsContainer.ResetBindings(false);
            this.SetCargosOrPos(this.CurrentRow.ID);
        }

        /// <summary>
        /// 保存完毕立刻更新ContainerId的值到各个子控件
        /// </summary>
        /// <returns></returns>
        private List<ContainerSaveRequest> CollectContainersData()
        {
            BindingList<DTContainerList> ctnList = bsContainer.DataSource as BindingList<DTContainerList>;

            List<DTContainerList> listChangedContainers = ctnList.ToList().FindAll(o => o.IsDirty);

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
                //containerIsSOCs.Add(item.IsSOC);
                //containerIsPartOfs.Add(item.IsPartOf);

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

            //ManyResult result = oeService.SaveOceanContainerInfo(saveRequest);
            listChangedContainers.ForEach(o => saveRequest.AddInvolvedObject(o));

            commands.Add(saveRequest);

            return commands;
        }

        /// <summary>
        /// 刷新订舱单列表上订舱单附带的箱列表信息
        /// </summary>
        void AfterSave()
        {
            if (Saved != null)
            {
                BindingList<DTContainerList> list = this.bsContainer.DataSource as BindingList<DTContainerList>;
                if (list != null)
                {
                    foreach (DTContainerList item in list)
                    {
                        if (string.IsNullOrEmpty(item.TypeName))
                        {
                            bool isChanged = item.IsDirty;
                            item.TypeName = this._containerTypes.First(o => o.ID == item.TypeID).Code;
                            item.IsDirty = isChanged;//避免设置TypeName时影响其IsChanged属性
                        }
                    }
                    Saved(new object[] { this.bookingInfo,
                list.Cast<DTContainerList>().ToList()});
                }
            }
        }

        #endregion

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsChanged && SaveData() == false) return;

            if (CurrentRow == null || CurrentRow.IsNew) return;

            DomesticTradePrintHelper.PrintOELoadContainer(CurrentRow.ID);
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    oeService.RemoveDTContaierInfo(new Guid[] { CurrentRow.ID }, LocalData.UserInfo.LoginID, new DateTime?[] { CurrentRow.UpdateDate },LocalData.IsEnglish);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                    gvCtn.DeleteRow(gvCtn.FocusedRowHandle);
                    if (Saved != null)
                    {
                        Saved(new object[] { this.DataSource });
                    }
                }
                catch(Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete failed" : "删除失败");                    
                }
            }

            this.RefreshBarEnabled();
            this.SetChildParts();
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!this.ValidateData())
            {
                return;
            }
            

            this.gvCtn.BeforeLeaveRow -=new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvCtn_BeforeLeaveRow);
            DTContainerList ctn = new DTContainerList();

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
        const int newRowHandle = int.MinValue + 1;//DEV里的新行常量
        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #region Changed

        ContainerCargosList _ccl;

        void bsContainer_PositionChanged(object sender, EventArgs e)
        {
            this.SetChildParts();
        }

        void SetCargosOrPos(Guid containerId)
        {
            //if (this.xtraTabControl1.SelectedTabPage == this.tabPagePO)
            //{
            //    this.poItemPart1.SetRelationShips(containerId);
            //}
            //else
            //{
            //    this._ccl.SetCargos(containerId);
            //}
        }

        #endregion

        #endregion

        #region IEditPart 成员

        void BindingData(object value)
        {
            DTBookingList bookingList = value as DTBookingList;

            CompositeContainerObjects cco = oeService.GetCompositeContainerObjects(bookingList.ID,LocalData.IsEnglish);

            if (cco == null)
            {
                throw new Exception("");
            }

            this._ccl = new ContainerCargosList();
            this._ccl.Dock = DockStyle.Fill;
            this.tabPageCargo.Controls.Add(this._ccl);

            if (cco.ContainerList.Count == 0)
            {
                //OceanContainerList ctn = new OceanContainerList();

                //ctn.ID = Guid.NewGuid();
                //ctn.IsDirty = true;
                //cco.ContainerList.Add(ctn);
                ////pods = new List<OceanBookingPOList>();
                //cco.CargoList = new List<OceanContainerCargoList>();
            }
            else
            {
                //cargolist = oeService.GetOceanContainerCargoList(ctnList[0].ID);
                //pods = oeService.GetOceanContainerPOList(ctnList[0].ID);
            }

            this.bsBookingInfo.DataSource = cco.BookingInfo;

            this.bookingInfo = cco.BookingInfo;

            BindingList<DTContainerList> containerList = new BindingList<DTContainerList>();
            foreach (DTContainerList container in cco.ContainerList)
            {
                containerList.Add(container);
            }
            this.bsContainer.DataSource = containerList;

            //this._ccl.SetAllCargoList(cco.CargoList, this.Workitem);
            this._ccl.ICPCommUIHelperService = this.ICPCommUIHelperService;
            this._ccl.BookingInfo = cco.BookingInfo;
            //this._ccl.SetMBLs(cco.);

            InitControls();
            RefreshBarEnabled();       
        }

        void SetSelected(Guid containerId)
        {
            BindingList<DTContainerList> containerList = this.bsContainer.DataSource as BindingList<DTContainerList>;

            int index = containerList.IndexOf(containerList.First(o => o.ID == containerId));

            this.bsContainer.Position = index;
        }

        public override object DataSource
        {
            get { return bsContainer.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            bsContainer.EndEdit();
            this.Validate();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region IPart 成员
        Guid _BLID = Guid.Empty;
        Guid _ShippingOrderID = Guid.Empty;

        Guid _currentContainerId = Guid.Empty;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
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

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.SetChildParts();
        }

        void SetChildParts()
        {

                if (this.CurrentRow != null)
                {
                    this._ccl.SetCargos(this.CurrentRow.ID);
                }
                this._ccl.Enabled = this.CurrentRow != null;
        }
    }
}
