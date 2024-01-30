using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanImport.UI.Business
{
    public partial class UCBusinessBoxList : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        public UCBusinessBoxList()
        {
            InitializeComponent();
        }

        #region 服务

        WorkItem Workitem;

        IOceanImportService oIService
        {
            get
            {
                if (Workitem == null)
                {
                    return null;
                }
                else
                {
                    return Workitem.Services.Get<IOceanImportService>();
                }
            }
        }

        ITransportFoundationService tfService
        {
            get
            {
                if (Workitem == null)
                {
                    return null;
                }
                else
                {
                    return Workitem.Services.Get<ITransportFoundationService>();
                }
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID
        {
            get;
            set;
        }
        /// <summary>
        /// MBLID
        /// </summary>
        public Guid MBLID
        {
            set;
            get;
        }
        private bool isChanged=false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (isChanged)
                {
                    return true;
                }
                else
                {
                    if (DataSource == null)
                    {
                        return false;
                    }
                    foreach (OIBusinessContainerList box in DataSource)
                    {
                        if (box.IsDirty)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            set
            {
                isChanged = value;
            }
        }

        #endregion

        #region 窗体事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            if (tfService != null)
            {
                List<ContainerList> containerList = tfService.GetContainerList(string.Empty, true, 0);

                cmbBoxType.Properties.BeginUpdate();
                foreach (ContainerList container in containerList)
                {
                    cmbBoxType.Properties.Items.Add(new ImageComboBoxItem(container.Code, container.ID));
                }
                cmbBoxType.Properties.EndUpdate();
            }

            //this.gvBoxList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvBoxList_RowCellClick);
        }

        //void gvBoxList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        //{
        //    if (e.Column == this.colIsRelation)
        //    {
        //        //CurrentRow.IsRelation = !CurrentRow.IsRelation;
        //        CurrentRow.IsDirty = true;
        //        //bsContainerInfo.ResetBindings(false);
        //    }
        //}

        private void ckbIsRelation_Click(object sender, System.EventArgs e)
        {
            CurrentRow.IsRelation = !CurrentRow.IsRelation;
            CurrentRow.IsDirty = true;
            bsContainerInfo.ResetBindings(false);
        }

        private void gvBoxList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 私有字段

        /// <summary>
        /// 数据源
        /// </summary>
        private List<OIBusinessContainerList> DataSource
        {
            get
            { 
                return bsContainerInfo.DataSource as List<OIBusinessContainerList>;
            }
            set
            {
                bsContainerInfo.DataSource = value;
            }
        }
        /// <summary>
        /// 当前行
        /// </summary>
        OIBusinessContainerList CurrentRow
        {
            get
            {
                if (bsContainerInfo.Current == null)
                    return null;
                else
                    return bsContainerInfo.Current as OIBusinessContainerList;
            }
        }
        /// <summary>
        /// 所有选择的行
        /// </summary>
        List<OIBusinessContainerList> SelectRows
        {
            get
            {
                int[] indexs = gvBoxList.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OIBusinessContainerList> list = new List<OIBusinessContainerList>();
                foreach (var item in indexs)
                {
                    OIBusinessContainerList tager = gvBoxList.GetRow(item) as OIBusinessContainerList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        #endregion

        #region 外部方法

        /// <summary>
        /// 设置服务
        /// </summary>
        /// <param name="_workitem"></param>
        public void SetService(WorkItem _workitem)
        {
            this.Workitem = _workitem;
            InitControls();
        }

        /// <summary>
        /// 绑定集装箱列表--提货通知书界面调用 
        /// </summary>
        public void BindContainerList( )
        {
            List<OIBusinessContainerList> list = oIService.GetOIContainerList(BusinessID);
            list.ForEach(o => o.IsRelation=false);         
            bsContainerInfo.DataSource = list;           
            bsContainerInfo.ResetBindings(false);
            //gvBoxList.BestFitColumns();
            foreach (var item in list)
            {
                item.IsDirty = false;
                item.BeginEdit();
            }
        }

        /// <summary>
        /// 获得箱号
        /// </summary>
        public string GetContainerNo()
        {
            StringBuilder containerNo = new StringBuilder();
            if (DataSource == null || DataSource.Count == 0)
            {
                return string.Empty;
            }
            foreach (OIBusinessContainerList box in DataSource)
            {
                if (containerNo.Length == 0)
                {
                    containerNo.Append(box.No);
                }
                else
                {
                    containerNo.Append(","+box.No);
                }
            }

            return containerNo.ToString();
        }
        /// <summary>
        /// 绑定集装箱列表--业务编辑界面调用  
        /// <param name="IsRelation">是否需要进行关联</param>
        /// </summary>
        public void BindContainerList(List<OIBusinessContainerList> list)
        {
            bsContainerInfo.DataSource = list;

            bsContainerInfo.ResetBindings(false);
            //gvBoxList.BestFitColumns();
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.BeginEdit();
                }
            }
        }

        /// <summary>
        /// 设置关联
        /// </summary>
        /// <param name="boxList"></param>
        public void SetRelation(List<Guid> boxIDList)
        {
            List<OIBusinessContainerList> list = DataSource;
            if (list == null)
            {
                return;
            }

            foreach (OIBusinessContainerList box in list)
            {
                if (box.ID == Guid.Empty || box.ID == null)
                {
                    box.IsRelation = false;
                }
                if (boxIDList.Contains(new Guid(box.ID.ToString())))
                {
                    box.IsRelation = true;                 
                }
                else
                {
                    box.IsRelation = false ;
                }

                box.IsDirty = false;
            }

            bsContainerInfo.DataSource = list;
            bsContainerInfo.ResetBindings(false);
            //gvBoxList.BestFitColumns();
            isChanged = false;
            //foreach(var item in list)
            //{
            //    item.IsDirty = false;
            //}
        }

        /// <summary>
        /// 获得关联的列表
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetRelation()
        { 
            List<Guid> idList=new List<Guid>();

            List<OIBusinessContainerList> list = DataSource;

            foreach (OIBusinessContainerList box in list)
            {
                if (box.IsRelation)
                {
                    idList.Add(new Guid(box.ID.ToString()));
                }
            }

           return idList;
        }     

        /// <summary>
        /// 验证数据的正确性
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            bsContainerInfo.EndEdit();
            gvBoxList.CloseEditor();
            
            List<OIBusinessContainerList> list = DataSource;

            List<string> noList = new List<string>();

            foreach (OIBusinessContainerList box in list)
            {
                if (!box.Validate())
                {
                    return false;
                }

                if (!noList.Contains(box.No))
                {
                    noList.Add(box.No);
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                   , (LocalData.IsEnglish ? "Box No. Not Repeat" : "箱号不能重复"));

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获得集装箱数据
        /// </summary>
        public List<ContainerSaveRequest> GetContainerList()
        {
            List<ContainerSaveRequest> boxList = new List<ContainerSaveRequest>();
            ContainerSaveRequest saveRequest = new ContainerSaveRequest();

            List<OIBusinessContainerList> list = DataSource;

            if (MBLID != Guid.Empty && list != null)
            {
                list = (from c in list where c.IsDirty select c).ToList<OIBusinessContainerList>();
            }
            if (list != null && list.Count > 0)
            {
                List<Guid?> idList = new List<Guid?>();
                List<string> noList = new List<string>();
                List<Guid> typeList = new List<Guid>();
                List<string> blNoList = new List<string>();
                List<string> sealNoList = new List<string>();
                List<int?> qtyList = new List<int?>();
                List<DateTime?> goDateList = new List<DateTime?>();
                List<DateTime?> lfDateList = new List<DateTime?>();
                List<DateTime?> validDateList = new List<DateTime?>();
                List<DateTime?> truckDateList = new List<DateTime?>();
                List<string> locationList = new List<string>();
                List<string> remarkList = new List<string>();
                List<DateTime?> updateDateList = new List<DateTime?>();
                List<bool> relationList = new List<bool>();
                List<bool> isPartOfList = new List<bool>();
                List<DateTime?> pickUpDateList=new List<DateTime?>();
                List<DateTime?> returnDateList = new List<DateTime?>();

                foreach (OIBusinessContainerList box in list)
                {
                    relationList.Add(box.IsRelation);
                    idList.Add(box.ID);
                    noList.Add(box.No);
                    typeList.Add(box.ContainerTypeID);
                    blNoList.Add(box.BLNo);
                    sealNoList.Add(box.SealNo);
                    qtyList.Add(box.Quantity);
                    goDateList.Add(box.GODate);
                    lfDateList.Add(box.LFDate);
                    validDateList.Add(box.AvailableDate);
                    locationList.Add(box.Location);
                    isPartOfList.Add(box.IsPartOf);
                    remarkList.Add(box.Remark);
                    updateDateList.Add(box.UpdateDate);
                    pickUpDateList.Add(box.PickUpDate);
                    returnDateList.Add(box.ReturnDate);

                }

                saveRequest.IsRelations = relationList.ToArray();
                saveRequest.RelatedContainerIDs = this.GetRelation().ToArray();
                saveRequest.IDs = idList.ToArray();
                saveRequest.Nos = noList.ToArray();
                saveRequest.ContainerTypeIDs = typeList.ToArray();
                saveRequest.SealNos = sealNoList.ToArray();
                saveRequest.Quantitys = qtyList.ToArray();
                saveRequest.GODates = goDateList.ToArray();
                saveRequest.LFDates = lfDateList.ToArray();
                saveRequest.TruckDates = truckDateList.ToArray();
                saveRequest.Locations = locationList.ToArray();
                saveRequest.Remarks = remarkList.ToArray();
                saveRequest.UpdateDates = updateDateList.ToArray();
                saveRequest.BLNos = blNoList.ToArray();
                saveRequest.ValidDates = validDateList.ToArray();
                saveRequest.MBLID = MBLID;
                saveRequest.IsPartOfs = isPartOfList.ToArray();
                saveRequest.saveByID = LocalData.UserInfo.LoginID;
                saveRequest.PickUpdates = pickUpDateList.ToArray();
                saveRequest.Returndates = returnDateList.ToArray();
                saveRequest.MBLID = MBLID;
                list.ForEach(o => saveRequest.AddInvolvedObject(o));
                boxList.Add(saveRequest);
            }

            return boxList;
        }

        //public void EndEdit()
        //{
        //    bsContainerInfo.EndEdit();
        //    this.gvBoxList.CloseEditor();
        //}

        /// <summary>
        /// 另存时，更新ID
        /// </summary>
        public void SaveAs()
        {
            List<OIBusinessContainerList> list = DataSource;

            list.ForEach(o => o.ID = Guid.NewGuid());
            
        }
        /// <summary>
        /// 更新列表数据
        /// </summary>
        /// <param name="list"></param>
        public void RefreshUI(List<ContainerSaveRequest> list)
        {
            foreach (ContainerSaveRequest boxInfo in list)
            {
                List<OIBusinessContainerList> boxList = boxInfo.UnBoxInvolvedObject<OIBusinessContainerList>();
                ManyResult result = boxInfo.ManyResult;

                for (int i = 0; i < boxList.Count; i++)
                {
                    boxList[i].ID = result.Items[i].GetValue<Guid>("ID");
                    boxList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    boxList[i].IsDirty = false;
                }
            }
            this.AfterSaved();
        }
        /// <summary>
        /// 刷新保存
        /// </summary>
        public void AfterSaved()
        {
            isChanged = false;
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OIBusinessContainerList preRow = null;
            if (gvBoxList.RowCount > 0)
            {
                preRow = gvBoxList.GetRow(gvBoxList.RowCount - 1) as OIBusinessContainerList;
            }
            OIBusinessContainerList newBoxRow;

            if (preRow != null)
                newBoxRow = Utility.Clone<OIBusinessContainerList>(preRow);
            else
                newBoxRow = new OIBusinessContainerList();

            newBoxRow.ID = Guid.Empty;
            //newBoxRow.CreateDate = DateTime.Now;
            //newBoxRow.CreateID = LocalData.UserInfo.LoginID;

            (bsContainerInfo.List as List<OIBusinessContainerList>).Add(newBoxRow);
            bsContainerInfo.ResetBindings(false);

            this.gvBoxList.MoveLast();

            isChanged = true;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<OIBusinessContainerList> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid> IDList = new List<Guid>();
            List<DateTime?> DateList = new List<DateTime?>();

            foreach (OIBusinessContainerList hbl in list)
            {
                if (!Utility.GuidIsNullOrEmpty(hbl.ID))
                {
                    IDList.Add(new Guid(hbl.ID.ToString()));
                    DateList.Add(hbl.UpdateDate);
                }
            }
            try
            {
                if (IDList.Count != 0)
                {
                    oIService.RemoveOIContainerInfo(IDList.ToArray(), LocalData.UserInfo.LoginID, DateList.ToArray());
                    isChanged = true;
                }
                gvBoxList.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }

        }

        #endregion
    }
}
