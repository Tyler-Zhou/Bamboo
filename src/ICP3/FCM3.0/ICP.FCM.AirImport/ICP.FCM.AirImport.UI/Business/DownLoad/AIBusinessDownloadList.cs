using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.UI
{
    public partial class OIBusinessDownloadList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        public OIBusinessDownloadList()
        {
            InitializeComponent();
        }

        #region 服务

        [ServiceDependency]
        public IAirImportService oiService { get; set; }

        //[ServiceDependency]
        //IDataFindClientService dfService{ get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }
    
        #endregion

        #region 初始化

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
                List<AirBusinessDownLoadList> list = value as List<AirBusinessDownLoadList>;
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

                if (list.Count.ToString().Length == 1)
                {
                    gvMain.IndicatorWidth = 30;
                }
                else
                {
                    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                }

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
                return bsOEList.Current as AirBusinessDownLoadList;
            }
        }

        public AirBusinessDownLoadList CurrentRow
        {
            get
            {
                return bsOEList.Current as AirBusinessDownLoadList;
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
        [EventPublication(AIBusinessCommandConstants.Command_AIInsertToListAfterDownLoad)]
        public event EventHandler<DataEventArgs<List<AirBusinessList>>> InsertToListAfterDownLoadEvent;

        #endregion

        private void bsOEList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }           

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
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "已经下载，不能重复下载!", LocalData.IsEnglish ? "Tip" : "提示");
                        CurrentRow.IsCheck = false;
                        return;
                    }

                    if (CurrentRow.DownLoadState == DownLoadState.Pending && (CurrentRow.HBLState == AIBLState.Draft || CurrentRow.HBLState == AIBLState.Checking) || (CurrentRow.HBLState == AIBLState.Unknown))
                    {
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

                    if (CurrentRow.ConsigneeName == null)
                    {
                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "收货人为空，你确认下载吗?"
                                  , LocalData.IsEnglish ? "Tip" : "提示"
                                  , MessageBoxButtons.YesNo
                                  , MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            CurrentRow.IsCheck = false;
                        }

                        return;
                    }

                    if (CurrentRow.PlaceofdeliveryDates == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "不能下载,请输入到交货地日.", LocalData.IsEnglish ? "Tip" : "提示");
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
                List<AirBusinessDownLoadList> dataList = DataSource as List<AirBusinessDownLoadList>;
                if (dataList == null || dataList.Count == 0)
                {
                    return;
                }

                List<AirBusinessDownLoadList> list = (from d in dataList where d.IsCheck select d).ToList<AirBusinessDownLoadList>();
                if (list == null || list.Count == 0)
                {
                    return;
                }
                //验证数据
                foreach (AirBusinessDownLoadList oe in list)
                {
                    if (!oe.Validate())
                    {
                        return;
                    }
                }
                List<Guid> mblIDList = new List<Guid>();
                List<Guid> consineeIDList = new List<Guid>();
                List<DateTime?> detas = new List<DateTime?>();
                List<string> hblIDList = new List<string>();
                //转换数据
                foreach (AirBusinessDownLoadList oe in list)
                {
                    mblIDList.Add(oe.ID);
                    consineeIDList.Add(oe.ConsigneeID);
                    detas.Add(oe.PlaceofdeliveryDates);
                    hblIDList.Add(oe.HBLIDs);
                }
                if (mblIDList.Count > 0)
                {
                    List<AirBusinessList> oiList = oiService.DownLoadBusiness(mblIDList.ToArray(), consineeIDList.ToArray(), detas.ToArray(), hblIDList.ToArray(), LocalData.UserInfo.LoginID);
                   
                    if (InsertToListAfterDownLoadEvent != null && oiList != null && oiList.Count > 0)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? " DownLoad Successfully" : "下载成功");
                       
                        InsertToListAfterDownLoadEvent(this, new DataEventArgs<List<AirBusinessList>>(oiList));
                    }

                    RefreshList();
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

            List<AirBusinessDownLoadList> dataList = DataSource as List<AirBusinessDownLoadList>;
            foreach (AirBusinessDownLoadList b in dataList)
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
    }
}