using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessDownloadList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        public OIBusinessDownloadList()
        {
            InitializeComponent();
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [ServiceDependency]
        IDataFindClientService dfService
        {
            get
            {
                return Workitem.Services.Get<IDataFindClientService>();
            }
        }
        #endregion

        #region 初始化

        private void gcMain_Click(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            //gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvMain_RowCellClick);

            dfService.RegisterGridColumnFinder(colConsigneeID
                        , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                        , "ConsigneeID"
                        , "ConsigneeName"
                        , "ID"
                        , LocalData.IsEnglish ? "EName" : "CName");
        }

        //void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        //{
        //    if (CurrentRow == null)
        //    {
        //        return;
        //    }
        //    if (e.Column == this.colCheck)
        //    {
        //        //下载状态是已下载，或者提单状态是：草稿、对单中，是不能勾选的
        //        if ((CurrentRow.DownLoadState == DownLoadState.Downloaded) || (CurrentRow.HBLState == BLState.Draft) || (CurrentRow.HBLState == BLState.Checking) || (CurrentRow.HBLState == BLState.Unknown))
        //        {
        //            CurrentRow.IsCheck = false;
        //        }
        //        else
        //        {
        //            CurrentRow.IsCheck = !CurrentRow.IsCheck;
        //        }
        //        bsOEList.ResetCurrentItem();
        //    }

        //}
        #endregion

        #region 属性


        #endregion

        #region 外部接口
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsOEList.DataSource;
            }
            set
            {
                List<OceanBusinessDownLoadList> list = value as List<OceanBusinessDownLoadList>;
                bsOEList.DataSource = list;
                bsOEList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                gvMain.BestFitColumns();

                string message = string.Empty;
                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                //if (list.Count.ToString().Length == 1)
                //{
                //    gvMain.IndicatorWidth = 30;
                //}
                //else
                //{
                //    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                //}

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get
            {
                return bsOEList.Current as OceanBusinessDownLoadList;
            }
        }

        public OceanBusinessDownLoadList CurrentRow
        {
            get
            {
                return bsOEList.Current as OceanBusinessDownLoadList;
            }
        }

        /// <summary>
        /// 选择的行发生改变
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 下载数据
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        // Declares a global scoped event that signals that a customer was added to the global list
        [EventPublication(OIBusinessCommandConstants.Command_InsertToListAfterDownLoad)]
        public event EventHandler<DataEventArgs<List<OceanBusinessList>>> InsertToListAfterDownLoadEvent;

        #endregion

        private void bsOEList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            //下载状态是已下载，或者提单状态是：草稿、对单中，是不能勾选的
            //if ((CurrentRow.DownLoadState == DownLoadState.Downloaded) || (CurrentRow.HBLState == BLState.Draft) || (CurrentRow.HBLState == BLState.Checking) || (CurrentRow.HBLState == BLState.Unknown))
            //{
            //    this.colConsigneeID.OptionsColumn.AllowEdit = false;
                //this.colCheck.OptionsColumn.AllowEdit = false;
                //this.ckbIsCheck.Enabled = false;
            //}
            //else
            //{
            //    this.colConsigneeID.OptionsColumn.AllowEdit = true;
                //this.colCheck.OptionsColumn.AllowEdit = true;
                //this.ckbIsCheck.Enabled = true;
            //}

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == this.colCheck)
            {
                bsOEList.EndEdit();
                if (!CurrentRow.IsCheck)
                {
                    if (CurrentRow.DownLoadState == DownLoadState.Downloaded)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The selected operation is already downloaded." : "已经下载，不能重复下载!", LocalData.IsEnglish ? "Tip" : "提示");
                        CurrentRow.IsCheck = false;
                        return;
                    }

                    if (CurrentRow.DownLoadState == DownLoadState.Pending && (CurrentRow.HBLState == OEBLState.Draft || CurrentRow.HBLState == OEBLState.Checking) || (CurrentRow.HBLState == OEBLState.Unknown))
                    {
                        //DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "You could not download the operation because it's still being held by Off-shore agent." : "不能下载,因为该业务港前客服没有处理完成，请联系港前客服:" + CurrentRow.POLFilerName, LocalData.IsEnglish ? "Tip" : "提示");
                        //CurrentRow.IsCheck = false;

                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure you want to download it when it's still being held by Off-shore agent?" : "该业务港前客服没有处理完成,是否确定要下载?"
                            , LocalData.IsEnglish ? "Tip" : "提示"
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                        {
                            CurrentRow.IsCheck = false;
                            return;
                        }
                    }

                    if (Utility.GuidIsNullOrEmpty(CurrentRow.ConsigneeID))
                    {
                        //DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to download the operation whose consignee is blank?" : "收货人为空，你确认下载吗?"
                        //          , LocalData.IsEnglish ? "Tip" : "提示"
                        //          , MessageBoxButtons.YesNo
                        //          , MessageBoxIcon.Question);

                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "It could not be downloaded because you have not yet fill in the consignee." : "不能下载,请输入收货人.", LocalData.IsEnglish ? "Tip" : "提示");
                        CurrentRow.IsCheck = false;
                        return;
                    }

                    if (CurrentRow.PlaceofdeliveryDates == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "It could not be downloaded because you have not yet fill in the Delivery Date." : "不能下载,请输入到交货地日.", LocalData.IsEnglish ? "Tip" : "提示");
                        CurrentRow.IsCheck = false;
                        return;
                    }
                }

                CurrentRow.IsCheck = !CurrentRow.IsCheck;
                bsOEList.ResetCurrentItem();
            }
        }
        
        /// <summary>
        /// 下载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_DownLoad)]
        public void Command_DownLoad(object sender, EventArgs e)
        {
            bsOEList.EndEdit();
            
            try
            {
                List<OceanBusinessDownLoadList> dataList = DataSource as List<OceanBusinessDownLoadList>;
                if (dataList == null || dataList.Count == 0)
                {
                    return;
                }

                List<OceanBusinessDownLoadList> list = (from d in dataList where d.IsCheck select d).ToList<OceanBusinessDownLoadList>();
                if (list == null || list.Count == 0)
                {
                    return;
                }
                //验证数据
                foreach (OceanBusinessDownLoadList oe in list)
                {
                    if (!oe.Validate())
                    {
                        return;
                    }
                    //Add by Sunny
                    //下载时收货人不允许为空
                    if (oe.ConsigneeID == null || oe.ConsigneeID == Guid.Empty)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Consignee couldn't be blank,please select consignee." : "收货人不允许为空,请先选择收货人"));
                        return;
                    }
                    //船名航次为空 不允许下载
                    if (string.IsNullOrEmpty(oe.VesselVoyage))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Vessel and voyage No. couldn't be blank,please select vessel and voyage No.." : "船名航次不允许为空,请先选择船名航次"));
                        return;
                    }
                    
                }
                List<Guid> mblIDList = new List<Guid>();
                List<Guid> consineeIDList = new List<Guid>();
                List<DateTime?> detas = new List<DateTime?>();
                List<string> hblIDList = new List<string>();
                //转换数据
                foreach (OceanBusinessDownLoadList oe in list)
                {
                    mblIDList.Add(oe.ID);
                    consineeIDList.Add(oe.ConsigneeID);
                    detas.Add(oe.PlaceofdeliveryDates);
                    hblIDList.Add(oe.HBLIDs);
                }
                if (mblIDList.Count > 0)
                {
                    OIAfterDownLoadRerurnData returnData = oiService.DownLoadBusiness(mblIDList.ToArray(), consineeIDList.ToArray(), detas.ToArray(), hblIDList.ToArray(), LocalData.UserInfo.LoginID);
                    if (returnData == null || returnData.PODRefNoList==null) return;
                   
                    #region 刷新列表

                    this.gvMain.CloseEditor();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].PODRefNo = returnData.PODRefNoList[i];
                        list[i].DownLoadState = DownLoadState.Downloaded;
                        list[i].DownLoadStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(DownLoadState.Downloaded, LocalData.IsEnglish);
                        list[i].IsCheck = false;
                    }

                    gvMain.RefreshData();

                    #endregion

                    #region 在海进业务列表插入下载成功的业务
                    if (InsertToListAfterDownLoadEvent != null && returnData.BusinessList != null && returnData.BusinessList.Count > 0)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? " DownLoad Successfully" : "下载成功");

                        InsertToListAfterDownLoadEvent(this, new DataEventArgs<List<OceanBusinessList>>(returnData.BusinessList));
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "DownLoad Failed" : "下载失败") + ex.Message);
            }
        }

        /// <summary>
        /// 保存后刷新列表
        /// </summary>
        private void RefreshList()
        {
            this.gvMain.CloseEditor();

            List<OceanBusinessDownLoadList> dataList = DataSource as List<OceanBusinessDownLoadList>;
            foreach (OceanBusinessDownLoadList b in dataList)
            {
                if (b.IsCheck)
                {
                    b.DownLoadState = DownLoadState.Downloaded;
                    b.DownLoadStateDescription = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DownLoadState>(DownLoadState.Downloaded, LocalData.IsEnglish);
                    b.IsCheck = false;
                }
            }

            //this.bsOEList.ResetBindings(false);

            gvMain.RefreshData();

        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
    }
}