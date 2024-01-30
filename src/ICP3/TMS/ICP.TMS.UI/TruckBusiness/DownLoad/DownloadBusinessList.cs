using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.TMS.ServiceInterface;
using ICP.Framework.ClientComponents.Service;

namespace ICP.TMS.UI
{
    public partial class DownloadBusinessList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        public DownloadBusinessList()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.gvMain.RowCellClick -= this.gvMain_RowCellClick;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.Saved = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
            }
        }

        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("BusinessDownLoad", LocalData.IsEnglish ? "Re-downloading the item is rejected. Because it has been already downloaded. " : "该业务已经下载,无法进行重复下载");
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            Utility.ShowGridRowNo(this.gvMain);
            gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvMain_RowCellClick);

        }

        void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            if (e.Column == this.colSelect)
            {
                if (CurrentRow.IsSelect)
                {
                    CurrentRow.IsSelect = !CurrentRow.IsSelect;
                    bsList.ResetCurrentItem();
                }
                else
                {    //下载状态是已下载，或者是下载中，是不能勾选的
                    if ((CurrentRow.DownloadState == DownLoadState.Downloaded) || (CurrentRow.DownloadState == DownLoadState.Downloading))
                    {
                        Utility.ShowMessage(NativeLanguageService.GetText(this, "BusinessDownLoad"));
                        return;
                    }
                    else
                    {
                        CurrentRow.IsSelect = !CurrentRow.IsSelect;
                        bsList.ResetCurrentItem();
                    }
                }
            }

        }
        #endregion

        #region 属性
        /// <summary>
        /// 当前行数据
        /// </summary>
        private DownLoadOceanBusinessList CurrentRow
        {
            get
            {
                return bsList.Current as DownLoadOceanBusinessList;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        private List<DownLoadOceanBusinessList> DataList
        {
            get
            {
                List<DownLoadOceanBusinessList> list=bsList.DataSource as List<DownLoadOceanBusinessList>;
                if(list==null)
                {
                    list=new List<DownLoadOceanBusinessList>();
                }
                return list;
            }
        }

        #endregion

        #region 外部接口
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                this.gvMain.BeginUpdate();
                List<DownLoadOceanBusinessList> list = value as List<DownLoadOceanBusinessList>;
                bsList.DataSource = list;
                bsList.ResetBindings(false);

                gvMain.BestFitColumns();

                this.gvMain.EndUpdate();

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
                    gvMain.IndicatorWidth = list.Count.ToString().Length * 15;
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }

        /// <summary>
        /// 下载数据
        /// </summary>
        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;


        #endregion


        #region 下载数据
        /// <summary>
        /// 当前选择的数据
        /// </summary>
        private List<DownLoadOceanBusinessList> SelectDataList
        {
            get
            {
                List<DownLoadOceanBusinessList> list = new List<DownLoadOceanBusinessList>();

                list = (from d in DataList where d.IsSelect select d).ToList();

                if (list == null)
                {
                    list = new List<DownLoadOceanBusinessList>();
                }

                return list;
            }


        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TMSDownLoadCommandConstants.Command_DownLoad)]
        public void Command_DownLoad(object sender, EventArgs e)
        {
            if (SelectDataList.Count == 0)
            {
                return;
            }

            List<Guid> idList = new List<Guid>();
            List<TruckBookingType> typeList = new List<TruckBookingType>();

            foreach (DownLoadOceanBusinessList item in SelectDataList)
            {
                idList.Add(item.ID);
                typeList.Add(item.BusinessType);
            }

            try
            {
                ///下载数据
                List<TruckBookingsList> list = TruckBookingService.DownLoadTruckList(typeList.ToArray(), idList.ToArray(), LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.LoginID, LocalData.IsEnglish);


                foreach (DownLoadOceanBusinessList item in SelectDataList)
                {
                    item.DownloadState = DownLoadState.Downloaded;
                }

                bsList.ResetBindings(false);


                if (Saved != null)
                {
                    Saved(list);
                }

                Workitem.Commands[TMSDownLoadCommandConstants.Command_SearchDate].Execute();
    
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "DownLoad Successfully!" : "下载成功!");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }


        }
        #endregion

    }
}