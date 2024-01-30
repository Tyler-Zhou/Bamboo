using System;
using System.Collections.Generic;
using System.Text;
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.UI.Business;

namespace ICP.FCM.OceanImport.UI
{
    public partial class UCBusinessBLList : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        public UCBusinessBLList()
        {
            InitializeComponent();
        }


        #region 属性
        private Guid businessID;
        /// <summary>
        /// 业务单ID
        /// </summary>
        public Guid BusinessID
        {
            get
            {
                return businessID;
            }
            set
            {
                businessID = value;
            }
        }
        private bool _isChanged=false;
        /// <summary>
        /// 是否有数据发生更新
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged)
                {
                    return true;
                }
                else
                {
                    if (DataSource != null)
                    {
                        foreach (OceanBusinessHBLList hbl in DataSource)
                        {
                            if (hbl.IsDirty)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            set
            {
                _isChanged = value;
            }
        }

        #endregion

        #region 私有变量
        /// <summary>
        /// 当前行
        /// </summary>
        OceanBusinessHBLList CurrentRow
        {
            get
            {
                if (bsHBLInfo.Current == null)
                    return null;
                else
                    return bsHBLInfo.Current as OceanBusinessHBLList;
            }
        }
        /// <summary>
        /// 所有选择的行
        /// </summary>
        List<OceanBusinessHBLList> SelectRows
        {
            get
            {
                int[] indexs = gvHBLList.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OceanBusinessHBLList> list = new List<OceanBusinessHBLList>();
                foreach (var item in indexs)
                {
                    OceanBusinessHBLList tager = gvHBLList.GetRow(item) as OceanBusinessHBLList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        List<OceanBusinessHBLList> DataSource
        {
            get
            {
                return bsHBLInfo.DataSource as List<OceanBusinessHBLList>;
            }
        }

        #endregion

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

        IDataFindClientService dfService
        {
            get
            {
                if (Workitem == null)
                {
                    return null;
                }
                else
                {
                    return Workitem.Services.Get<IDataFindClientService>();
                }
            }
        }

        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                

            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            dfService.RegisterGridColumnFinder(colShipperName
                                    , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                    , "ShipperID"
                                    , "ShipperName"
                                    , "ID"
                                    , LocalData.IsEnglish ? "EName" : "CName");
        }

        #endregion

        #region 外部调用

        public void SetTools(bool isVisible)
        {
            this.bar2.Visible = isVisible;
        }


        public void SetService(WorkItem _workItem)
        {
            this.Workitem = _workItem;

            InitControls();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindHBLList(List<OceanBusinessHBLList> list)
        {

             bsHBLInfo.DataSource = list;

             bsHBLInfo.ResetBindings(false);

        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            this.bsHBLInfo.EndEdit();

             foreach (OceanBusinessHBLList hbl in DataSource)
             {
                 if (!hbl.Validate())
                 {
                     return false;
                 }
             }

            return true;
        }
        /// <summary>
        /// 获得分提单号信息
        /// </summary>
        /// <returns></returns>
        public string GetHBLNo()
        {
            StringBuilder hblNo = new StringBuilder();

            if (DataSource == null || DataSource.Count == 0)
            {
                return string.Empty;
            }
            foreach (OceanBusinessHBLList hbl in DataSource)
            {
                if (hblNo.Length == 0)
                {
                    hblNo.Append(hbl.HBLNo);
                }
                else
                {
                    hblNo.Append(",");
                    hblNo.Append(hbl.HBLNo);
                }
            }

            return hblNo.ToString();
        }

        /// <summary>
        /// 获得数据信息
        /// </summary>
        /// <returns></returns>
        public List<HBLInfoSaveRequest> GetHBLSaveInfo() 
        {
            List<OceanBusinessHBLList> hblsSource = this.bsHBLInfo.DataSource as List<OceanBusinessHBLList>;

            if (hblsSource.Count != 0)
            {
                List<OceanBusinessHBLList> changedHBLs = hblsSource.FindAll(o => o.IsDirty);
                if (BusinessID == Guid.Empty && hblsSource!=null)
                {
                    changedHBLs = hblsSource;
                }

                if (changedHBLs.Count > 0)
                {
                    List<HBLInfoSaveRequest> commands = new List<HBLInfoSaveRequest>();
                    List<Guid?> idList = new List<Guid?>();
                    List<string> noList = new List<string>();
                    List<Guid?> shipperIDList = new List<Guid?>();
                    List<string> amsNoList = new List<string>();
                    List<string> isfNoList = new List<string>();
                    List<DateTime?> receiveOBLDateList = new List<DateTime?>();
                    List<DateTime?> updateDateList = new List<DateTime?>();
                    List<string> customerDescList = new List<string>();
                    List<string> goodsList = new List<string>();
                    List<Int32?> qtyList = new List<int?>();
                    List<decimal?> weightList = new List<decimal?>();
                    List<decimal?> measurementList = new List<decimal?>();

                    foreach (OceanBusinessHBLList hbl in hblsSource)
                    {
                        idList.Add(hbl.ID);
                        noList.Add(hbl.HBLNo);
                        shipperIDList.Add(hbl.ShipperID);
                        amsNoList.Add(hbl.AMSNo);
                        isfNoList.Add(hbl.ISFNo);
                        receiveOBLDateList.Add(hbl.ReceiveOBLDate);
                        updateDateList.Add(hbl.UpdateDate);
                        string customsDescription = SerializerHelper.SerializeToString<CustomerDescription>(new CustomerDescription(), true, false);
                        customerDescList.Add(customsDescription);

                        goodsList.Add(hbl.GoodsInfo);
                        qtyList.Add(hbl.Qty);
                        weightList.Add(hbl.Weight);
                        measurementList.Add(hbl.Measurement);

                    }

                    HBLInfoSaveRequest saveRequest = new HBLInfoSaveRequest();
                    saveRequest.IDs = idList.ToArray();
                    saveRequest.BLNos = noList.ToArray();
                    saveRequest.ShipperIDs = shipperIDList.ToArray();
                    saveRequest.AMSNos = amsNoList.ToArray();
                    saveRequest.ISFNos = isfNoList.ToArray();
                    saveRequest.ReceiveOBLDates = receiveOBLDateList.ToArray();
                    saveRequest.UpdateDates = updateDateList.ToArray();
                    saveRequest.ShipperDescriptions = customerDescList.ToArray();
                    saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                    saveRequest.OIBookingID = businessID;
                    saveRequest.GoodsInfos = goodsList.ToArray();
                    saveRequest.Qtys = qtyList.ToArray();
                    saveRequest.Weights = weightList.ToArray();
                    saveRequest.Measurements = measurementList.ToArray();

                    changedHBLs.ForEach(o => saveRequest.AddInvolvedObject(o));

                    commands.Add(saveRequest);

                    return commands;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }           
        }

        /// <summary>
        /// 另存为时，将所有的ID清空
        /// </summary>
        public void SaveAs()
        {
            List<OceanBusinessHBLList> list = DataSource;
            if (list != null)
            {
                list.ForEach(o => o.ID = Guid.NewGuid());
            }
        }
        /// <summary>
        /// 更新列表数据
        /// </summary>
        /// <param name="list"></param>
        public void RefreshUI(List<HBLInfoSaveRequest> list)
        {
            foreach (HBLInfoSaveRequest hblInfo in list)
            {
                List<OceanBusinessHBLList> hblListList = hblInfo.UnBoxInvolvedObject<OceanBusinessHBLList>();
                ManyResult result = hblInfo.ManyResult;

                for (int i = 0; i < hblListList.Count; i++)
                {
                    hblListList[i].ID = result.Items[i].GetValue<Guid>("ID");
                    hblListList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    hblListList[i].IsDirty = false;
                }
            }
            this.AfterSaved();
        }
        /// <summary>
        /// 刷新保存
        /// </summary>
        public void AfterSaved()
        {
            _isChanged = false;
        }
        /// <summary>
        /// 设置收到正本日
        /// </summary>
        public void SetReceive(FCMReleaseType releaseType)
        {
            if (releaseType == FCMReleaseType.Original)
            {
                colOBLDate.OptionsColumn.AllowEdit = true;
            }
            else
            {
                colOBLDate.OptionsColumn.AllowEdit = false;
            }
        }
        /// <summary>
        /// 获得收到正本状态
        /// </summary>
        /// <returns></returns>
        public bool GetReceiverState()
        {
            bool isAll=true;
            List<OceanBusinessHBLList> list = DataSource;
            if (list == null || list.Count == 0)
            {
                return false;
            }

            foreach (OceanBusinessHBLList hbl in list)
            {
                if (hbl.ReceiveOBLDate == null)
                {
                    isAll = false;
                }
            }

            return isAll;
        }

        //public void EndEdit()
        //{
        //   bsHBLInfo.EndEdit();
        //}

        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OceanBusinessHBLList preRow = null;
            if (gvHBLList.RowCount > 0)
            {
                preRow = gvHBLList.GetRow(gvHBLList.RowCount - 1) as OceanBusinessHBLList;
            }
            OceanBusinessHBLList newHBLRow;

            if (preRow != null)
                newHBLRow = Utility.Clone<OceanBusinessHBLList>(preRow);
            else
                newHBLRow = new OceanBusinessHBLList();

            newHBLRow.ID = Guid.Empty;
            newHBLRow.CreateDate = DateTime.Now;
            newHBLRow.CreateID = LocalData.UserInfo.LoginID;

            (bsHBLInfo.List as List<OceanBusinessHBLList>).Add(newHBLRow);
            bsHBLInfo.ResetBindings(false);

            _isChanged = true;

            this.gvHBLList.MoveLast();
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
            OceanBusinessHBLList hblList = CurrentRow;
            if (hblList == null)
            {
                return;
            }
            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }
         
            try
            {
                if (hblList.ID!=Guid.Empty)
                {
                    oIService.RemoveOIBookingHBLInfo(hblList.ID, LocalData.UserInfo.LoginID, hblList.UpdateDate);

                }
                gvHBLList.DeleteSelectedRows();
                _isChanged = true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }
        #endregion

        #region 行发生改变
        /// <summary>
        /// 行发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsHBLInfo_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                this.barCtn.Enabled = false;
            }
            if(Utility.GuidIsNullOrEmpty(CurrentRow.ID))
            {
              this.barCtn.Enabled = false;
            }
            else
            {
                this.barCtn.Enabled = true ;
            }
        }

        #endregion

        #region 关联箱
        /// <summary>
        /// 关联箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID))
            {
                return;
            }

            UCHBL2CtnList hbl2Ctn = Workitem.Items.AddNew<UCHBL2CtnList>();
            hbl2Ctn.HBLID = CurrentRow.ID;
            hbl2Ctn.Workitem = this.Workitem;
            string title = LocalData.IsEnglish ? "Set HBL Container" : "设置HBL关联箱";
            ICP.Framework.ClientComponents.Controls.PartLoader.ShowDialog(hbl2Ctn, title);

        }
        #endregion
    }
}
