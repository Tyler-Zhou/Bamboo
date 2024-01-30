using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.DomesticTrade.UI.Container
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class LoadContainerPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        public DomesticTradePrintHelper DomesticTradePrintHelper
        {
            get
            {
                return ClientHelper.Get<DomesticTradePrintHelper, DomesticTradePrintHelper>();
            }
        }

        public IDomesticTradeService DomesticTradeService
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

        #endregion

        #region Property

        /// <summary>
        /// 各个区域的数据是否有改动
        /// </summary>
        bool IsChanged
        {
            get
            {
                EndEdit();
                bsContainer.EndEdit();
                if (CurrentRow == null)
                {
                    return false;
                }

                return CurrentRow.IsDirty 
                    || _ccl.IsChanged;
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
            Disposed += delegate {
                _ccl = null;
                _containerTypes = null;
                bsContainer.PositionChanged -= bsContainer_PositionChanged;
                ctnGridControl.DataSource = null;
                bsBookingInfo.DataSource = null;
                bsBookingInfo.Dispose();
                bsContainer.DataSource = null;
                bsContainer.Dispose();
                Saved = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            Load += new EventHandler(LoadContainerPart_Load);
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
            bsContainer.PositionChanged += new EventHandler(bsContainer_PositionChanged);

            gvCtn.FocusedRowChanged += new FocusedRowChangedEventHandler(gvCtn_FocusedRowChanged);
            gvCtn.BeforeLeaveRow += new RowAllowEventHandler(gvCtn_BeforeLeaveRow);
            gvCtn.RowCountChanged += new EventHandler(gvCtn_RowCountChanged);

            if (bsContainer.Count == 0)
            {
                barAdd_ItemClick(null, null);
            }

            txtNo.Select();
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
        void gvCtn_BeforeLeaveRow(object sender, RowAllowEventArgs e)
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


            if (!ValidateData())
            {
                e.Allow = false;
            }
        }

        void gvCtn_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gvCtn.FocusedRowChanged -= new FocusedRowChangedEventHandler(gvCtn_FocusedRowChanged);
            if (_currentContainerId != Guid.Empty)
            {
                BindingList<DTContainerList> containers = bsContainer.DataSource as BindingList<DTContainerList>;
                int index = containers.IndexOf(containers.First(o => o.ID == _currentContainerId));
                if (index >= 0)
                {
                    int rowHandle = gvCtn.GetRowHandle(index);
                    if (rowHandle >= 0)
                    {
                        gvCtn.FocusedRowHandle = rowHandle;

                        SetCargosOrPos(_currentContainerId);
                    }
                }
            }
            else
            {
                SetCargosOrPos(CurrentRow.ID);
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
            _containerTypes = ICPCommUIHelper.SetCmbContainerType(rcmbType);
            ICPCommUIHelper.SetCmbContainerType(cmbType, _containerTypes);

            #endregion
        }

        /// <summary>
        /// 刷新工具栏
        /// </summary>
        private void RefreshBarEnabled()
        {
            if (CurrentRow == null)
            {
                barDelete.Enabled = false;
            }
            else
            {
                barDelete.Enabled = true;
            }

            if (bsContainer.List.Count == 0)
            {
                barSave.Enabled = barPrint.Enabled = false;
            }
            else
            {
                barSave.Enabled = barPrint.Enabled = true;
            }

            groupBoxCtn.Enabled = CurrentRow != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool ValidateData()
        {
            bool isScrr = true;

            EndEdit();
            gvCtn.CloseEditor();
            bsContainer.EndEdit();

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
            if (_ccl != null && _ccl.ValidateData() == false)
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
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
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

            List<CargoSaveRequest> originalCargos =  _ccl.CollectCargosData();

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

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

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
            SetCargosOrPos(CurrentRow.ID);
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
                BindingList<DTContainerList> list = bsContainer.DataSource as BindingList<DTContainerList>;
                if (list != null)
                {
                    foreach (DTContainerList item in list)
                    {
                        if (string.IsNullOrEmpty(item.TypeName))
                        {
                            bool isChanged = item.IsDirty;
                            item.TypeName = _containerTypes.First(o => o.ID == item.TypeID).Code;
                            item.IsDirty = isChanged;//避免设置TypeName时影响其IsChanged属性
                        }
                    }
                    Saved(new object[] { bookingInfo,
                list.Cast<DTContainerList>().ToList()});
                }
            }
        }

        #endregion

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsChanged && SaveData() == false) return;

            if (CurrentRow == null || CurrentRow.IsNew) return;

            DomesticTradePrintHelper.PrintOELoadContainer(CurrentRow.ID);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            if (XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Delete Current Data?" : "是否确认删除当前数据?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
            { return; }

            bool isDeleteNewRow = CurrentRow.ID == Guid.Empty;

            if (isDeleteNewRow)
            {
                gvCtn.DeleteRow(gvCtn.FocusedRowHandle);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
            }
            else
            {
                try
                {
                    DomesticTradeService.RemoveDTContaierInfo(new Guid[] { CurrentRow.ID }, LocalData.UserInfo.LoginID, new DateTime?[] { CurrentRow.UpdateDate },LocalData.IsEnglish);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                    gvCtn.DeleteRow(gvCtn.FocusedRowHandle);
                    if (Saved != null)
                    {
                        Saved(new object[] { DataSource });
                    }
                }
                catch(Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete failed" : "删除失败");                    
                }
            }

            RefreshBarEnabled();
            SetChildParts();
        }

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (!ValidateData())
            {
                return;
            }
            

            gvCtn.BeforeLeaveRow -=new RowAllowEventHandler(gvCtn_BeforeLeaveRow);
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
            gvCtn.SelectRow(gvCtn.FocusedRowHandle);

            //this.gvCtn.SelectRow(newRowHandle);
            txtNo.Select();
            RefreshBarEnabled();

            gvCtn.BeforeLeaveRow += new RowAllowEventHandler(gvCtn_BeforeLeaveRow);

            SetChildParts();
        }
        const int newRowHandle = int.MinValue + 1;//DEV里的新行常量
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #region Changed

        ContainerCargosList _ccl;

        void bsContainer_PositionChanged(object sender, EventArgs e)
        {
            SetChildParts();
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

            CompositeContainerObjects cco = DomesticTradeService.GetCompositeContainerObjects(bookingList.ID,LocalData.IsEnglish);

            if (cco == null)
            {
                throw new Exception("");
            }

            _ccl = new ContainerCargosList();
            _ccl.Dock = DockStyle.Fill;
            tabPageCargo.Controls.Add(_ccl);

            bsBookingInfo.DataSource = cco.BookingInfo;

            bookingInfo = cco.BookingInfo;

            BindingList<DTContainerList> containerList = new BindingList<DTContainerList>();
            foreach (DTContainerList container in cco.ContainerList)
            {
                containerList.Add(container);
            }
            bsContainer.DataSource = containerList;
            _ccl.BookingInfo = cco.BookingInfo;

            InitControls();
            RefreshBarEnabled();       
        }

        void SetSelected(Guid containerId)
        {
            BindingList<DTContainerList> containerList = bsContainer.DataSource as BindingList<DTContainerList>;

            int index = containerList.IndexOf(containerList.First(o => o.ID == containerId));

            bsContainer.Position = index;
        }

        public override object DataSource
        {
            get { return bsContainer.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            bsContainer.EndEdit();
            Validate();
        }

        public override event SavedHandler Saved;

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

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            SetChildParts();
        }

        void SetChildParts()
        {

                if (CurrentRow != null)
                {
                    _ccl.SetCargos(CurrentRow.ID);
                }
                _ccl.Enabled = CurrentRow != null;
        }
    }
}
