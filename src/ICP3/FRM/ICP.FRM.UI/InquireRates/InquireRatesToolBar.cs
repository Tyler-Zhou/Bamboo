#region Comment

/*
 * 
 * FileName:    InquireRatesToolBar.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->询价工具栏
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.FRM.UI.InquireRates.Trucking;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 询价工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireRatesToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 成员变量

        public bool IsTaskCenter
        {
            get;
            set;
        }

        public InquierType CurInquierType
        {
            get;
            set;
        }
        #endregion

        #region 构造函数
        public InquireRatesToolBar()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            BulidCommand();
            if (!DesignMode)
            {
                InitMessage();
                
            }
            Load += (sender, arg) =>
            {
                if (IsTaskCenter)
                {
                    barSearch.Visibility = BarItemVisibility.Never;
                    barMail.Visibility = BarItemVisibility.Never;
                    barNewOceanRate.Visibility = BarItemVisibility.Never;
                    barNewTruckingRate.Visibility = BarItemVisibility.Never;
                    barNewAirRate.Visibility = BarItemVisibility.Never;
                    switch (CurInquierType)
                    {
                        case InquierType.OceanRates:
                            barNewOceanRate.Visibility = BarItemVisibility.Always;
                            break;
                        case InquierType.TruckingRates:
                            barNewTruckingRate.Visibility = BarItemVisibility.Always;
                            break;
                        case InquierType.AirRates:
                            barNewAirRate.Visibility = BarItemVisibility.Always;
                            break;
                    }
                }
            };
        } 
        #endregion

        #region Override
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                BindingData(value);
            }
        }
        #endregion

        #region 委托事件
        /// <summary>
        /// 新增海运询价单
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_NewOceanRate)]
        public event EventHandler<DataEventArgs<object>> NewOceanRateEvent;
        /// <summary>
        /// 新增空运询价单
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_NewAirRate)]
        public event EventHandler<DataEventArgs<object>> NewAirRateEvent;
        /// <summary>
        /// 新增拖车询价单
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_NewTruckingRate)]
        public event EventHandler<DataEventArgs<object>> NewTruckingRateEvent;
        /// <summary>
        /// 保存
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_Save)]
        public event EventHandler<DataEventArgs<object>> SaveEvent;
        /// <summary>
        /// 复制询价
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_Copy)]
        public event EventHandler<DataEventArgs<object>> CopyEvent;
        /// <summary>
        /// 复制主询价单
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_ReInquire)]
        public event EventHandler<DataEventArgs<object>> ReInquireEvent;
        /// <summary>
        /// 删除询价单
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_Delete)]
        public event EventHandler<DataEventArgs<object>> DeleteEvent;
        /// <summary>
        /// 新建WorkItem
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_AddNewWorkItem)]
        public event EventHandler<DataEventArgs<InquierType>> AddNewWorkItemEvent;
        /// <summary>
        /// 发送邮件
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_Mail)]
        public event EventHandler<DataEventArgs<object>> MailEvent; 
        #endregion

        #region 窗体事件
        /// <summary>
        /// 邮件
        /// </summary>
        private void barMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MailEvent != null)
            {
                MailEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 复制主询价单:
        /// 1.打开新增询价面板
        /// 2.面板数据初始化为当前选中询价单信息
        /// </summary>
        private void barReInquire_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ReInquireEvent != null)
            {
                ReInquireEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 删除询价单
        /// </summary>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DeleteEvent != null)
            {
                DeleteEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 复制询价单
        /// </summary>
        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CopyEvent != null)
            {
                CopyEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 保存询价单
        /// </summary>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SaveEvent != null)
            {
                SaveEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 新增海运询价单：打开海运询价单新建面板
        /// </summary>
        private void barNewOceanRate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (NewOceanRateEvent != null)
            {
                NewOceanRateEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 新增空运询价单
        /// </summary>
        private void barNewAirRate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AddNewWorkItemEvent != null)
            {
                AddNewWorkItemEvent(this, new DataEventArgs<InquierType>(InquierType.AirRates));
            }

            if (NewAirRateEvent != null)
            {
                NewAirRateEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 新增拖车询价单
        /// </summary>
        private void barNewTruckingRate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AddNewWorkItemEvent != null)
            {
                AddNewWorkItemEvent(this, new DataEventArgs<InquierType>(InquierType.TruckingRates));
            }

            if (NewTruckingRateEvent != null)
            {
                NewTruckingRateEvent(this, new DataEventArgs<object>(new object()));
            }
        }
        /// <summary>
        /// 修改拖车附加费：显示更新拖车附加费面板
        /// </summary>
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            InquireTruckFUEL updateFUEL = Workitem.Items.AddNew<InquireTruckFUEL>();
            string title = LocalData.IsEnglish ? "UpdateTruckFUEL" : "更新拖车附加费";
            PartLoader.ShowDialog(updateFUEL, title);
        } 
        #endregion

        #region 方法定义
        /// <summary>
        /// 注册Message
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("Invalidate", "&Invalidate");
            RegisterMessage("Resume", "Resume(&I)");
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="value">数据源</param>
        private void BindingData(object value)
        {
            OceanList listData = value as OceanList;

            if (listData != null && listData.IsNew)
                barNew.Enabled = false;
            else
                barNew.Enabled = true;


            if (listData == null || listData.IsNew)
            {
                barDelete.Enabled = barCopy.Enabled = barReInquire.Enabled =
                barSave.Enabled = false;
            }
            else
            {
                barDelete.Enabled = barCopy.Enabled = barSave.Enabled = true;
                //未过期则启用启用
                barReInquire.Enabled = listData.State != OceanState.Expired;
                //根据有效状态设置文本
                barReInquire.Caption = NativeLanguageService.GetText(this, listData.State != OceanState.Invalidated ? "Invalidate" : "Resume");


                //if (listData.State == OceanState.Draft)
                //    barPause.Caption = NativeLanguageService.GetText(this, "Publish"); 
                //else
                //    barPause.Caption = NativeLanguageService.GetText(this, "Pause"); 
            }
        }
        /// <summary>
        /// BulidCommand
        /// </summary>
        private void BulidCommand()
        {
            barNewOceanRate.ItemClick += barNewOceanRate_ItemClick;
            barNewAirRate.ItemClick += barNewAirRate_ItemClick;
            barNewTruckingRate.ItemClick += barNewTruckingRate_ItemClick;
            barSave.ItemClick += barSave_ItemClick;
            //barDelete.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_DeleteData].Execute(); };
            barCopy.ItemClick += barCopy_ItemClick;
            barSearch.ItemClick += delegate { Workitem.Commands[InquireRatesCommandConstants.Command_ShowSearch].Execute(); };
            ////barInquiery.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_Inquiery].Execute(); };
            barReInquire.ItemClick += barReInquire_ItemClick;
            barDelete.ItemClick += barDelete_ItemClick;
            barMail.ItemClick += barMail_ItemClick;
            barRefresh.ItemClick += delegate { Workitem.Commands[InquireRatesCommandConstants.Command_RefreshData].Execute(); };

            barClose.ItemClick += delegate { FindForm().Close(); };
        }
        #endregion
    }
}
