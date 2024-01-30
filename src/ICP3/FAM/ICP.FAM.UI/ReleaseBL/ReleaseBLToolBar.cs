using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.UI.ReleaseBL
{
    [ToolboxItem(false)]
    public partial class ReleaseBLToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        string users = "VICKIWANG,GARRETTLI,ZHANGBIAO";
        public ReleaseBLToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };


            BulidCommond();

            SetCtnText();


            base.RegisterMessage("Apply", "A&pply");
            base.RegisterMessage("ReleaseOriginal", "Release&Original");
            base.RegisterMessage("ReleaseTelex", "Release&Telex");
            base.RegisterMessage("NoticeTelex", "&NoticeTelex");
            base.RegisterMessage("Change2Original", "Chan&ge2Original");

            base.RegisterMessage("CancelApply", "CancelA&pply");
            base.RegisterMessage("CancelReleaseOriginal", "CancelRelease&Original");
            base.RegisterMessage("CancelReleaseTelex", "CancelRelease&Telex");
            base.RegisterMessage("CancelNoticeTelex", "Cancel&NoticeTelex");
            base.RegisterMessage("Change2Telex", "Chan&ge2Telex");
        }


        private void SetCtnText()
        {
            if (!LocalData.IsEnglish)
            {
                barVisible.Caption = "全部";
                barVisibleAll.Caption = "全部";
                barVisibleHBL.Caption = "HBL";
                barVisibleMBL.Caption = "MBL";
            }
        }


        private void BulidCommond()
        {
            barEdit.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_Edit].Execute(); };
            barReceived.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_Received].Execute(); };
            barApply.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_Apply].Execute(); };
            barCancelApply.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_CancelApply].Execute(); };
            barReleaseBL.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_ReleaseBL].Execute(); };
            barCancelReleaseBL.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_CancelReleaseBL].Execute(); };
            barChangeToOriginal.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_Change2Original].Execute(); };
            barChangeToTelex.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_Change2Telex].Execute(); };
            barExceptionCus.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_ExceptionCustomer].Execute(); };
            barExRelease.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_ExRelease].Execute(); };
            barPressMoney.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_PressMoney].Execute(); };
            barSetArEmail.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_SetArEmail].Execute(); };

            barRefresh.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_Refresh].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Commond_ShowSearch].Execute(); };
            barPrintBL.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Command_PrintBL].Execute(); };
            barViewBusinessInfo.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Command_ViewBusinessInfo].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Command_Bill].Execute(); };

            barExportToExcel.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Command_ExportToExcel].Execute(); };

            barClose.ItemClick += delegate { FindForm().Close(); };

            barVisibleAll.ItemClick += delegate
            {
                barVisibleAll.Checked = true;
                barVisible.Caption = barVisibleAll.Caption;
                barVisible.Refresh();
                Workitem.Commands[ReleaseBLCommondConstants.Command_VisibleALL].Execute();
                barVisibleHBL.Checked = barVisibleMBL.Checked = false;
            };
            barVisibleHBL.ItemClick += delegate
            {
                barVisibleHBL.Checked = true;
                barVisible.Caption = "HBL";
                barVisible.MenuCaption = "HBL";
                barVisible.Refresh();
                Workitem.Commands[ReleaseBLCommondConstants.Command_VisibleHBL].Execute();
                barVisibleAll.Checked = barVisibleMBL.Checked = false;
            };
            barVisibleMBL.ItemClick += delegate
            {
                barVisibleMBL.Checked = true;
                barVisible.Caption = "MBL";
                barVisible.MenuCaption = "MBL";
                barVisible.Refresh();
                Workitem.Commands[ReleaseBLCommondConstants.Command_VisibleMBL].Execute();
                barVisibleHBL.Checked = barVisibleAll.Checked = false;

            };

            barBtRecevied.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Command_Recevied].Execute(); };
            barBtNotRecevied.ItemClick += delegate { Workitem.Commands[ReleaseBLCommondConstants.Command_CancelRecevied].Execute(); };
        }

        #region IToolBar成员

        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                BindingSource(value);
            }
        }

        private void BindingSource(object value)
        {
            ReleaseBLList listData = value as ReleaseBLList;
            if (listData == null)
            {
                barApply.Enabled = barCancelApply.Enabled = barEdit.Enabled = barReceived.Enabled = barReleaseBL.Enabled
                    = barViewBusinessInfo.Enabled = barBill.Enabled = barChangeToOriginal.Enabled = barChangeToTelex.Enabled = false;
            }
            else
            {
                barApply.Enabled = barCancelApply.Enabled = barEdit.Enabled = barReceived.Enabled = barReleaseBL.Enabled =
                barCancelReleaseBL.Enabled= barViewBusinessInfo.Enabled = barBill.Enabled = barChangeToOriginal.Enabled = 
                barChangeToTelex.Enabled = true;

                //if (listData.ReleaseType == ReleaseType.Telex)
                //    barChangeToOriginal.Caption = LocalData.IsEnglish ? "Chan&geToOriginal" : "改为正本";
                //else
                //    barChangeToOriginal.Caption = LocalData.IsEnglish ? "Chan&geToTelex" : "改为电放";

                //#region 签收	如果放单.状态=已创建，则显示该按钮，否则隐藏该按钮。
                //if (listData.State == ReleaseBLState.Created)
                //    barReceived.Visibility = BarItemVisibility.Always;
                //else
                //    barReceived.Visibility = BarItemVisibility.Never;
                //#endregion
                if ((short)listData.State <= (short)ReleaseBLState.Received)
                {
                    if (listData.ReleaseType == ReleaseType.Telex)
                    {
                        barChangeToOriginal.Visibility = BarItemVisibility.Always;
                        barChangeToTelex.Visibility = BarItemVisibility.Never;
                    }
                    else if (listData.ReleaseType == ReleaseType.Original)
                    {
                        barChangeToOriginal.Visibility = BarItemVisibility.Never;
                        barChangeToTelex.Visibility = BarItemVisibility.Always;
                    }
                }

                List<ExRelease> list = FinanceService.CheckExRelease(listData.ID);
                if (list == null || list.Count == 0)
                {
                    barExRelease.Caption = LocalData.IsEnglish ? "SetExRelease" : "设置不发送催放单";
                }
                else
                {
                    barExRelease.Caption = LocalData.IsEnglish ? "CancelExRelease" : "取消不发送催放单";
                }

                //if (listData.RBLD)
                //{
                //    barChangeToOriginal.Enabled = barChangeToTelex.Enabled = false;
                //}
                //else
                //{
                //    barChangeToOriginal.Enabled = barChangeToTelex.Enabled = true;
                //}


                //if (listData.ReleaseType == ReleaseType.Telex)
                //    barChangeToOriginal.Caption = LocalData.IsEnglish ? "Chan&geToOriginal" : "改为正本";
                //else if (listData.ReleaseType == ReleaseType.Original)
                //    barChangeToOriginal.Caption = LocalData.IsEnglish ? "Chan&geToTelex" : "改为电放";
                //if (listData.ReleaseType == ReleaseType.OconvT)
                //    barChangeToOriginal.Caption = LocalData.IsEnglish ? "Chan&geToOriginal" : "改为正本";
                //else
                //    barChangeToOriginal.Caption = LocalData.IsEnglish ? "Chan&geToTelex" : "改为电放";

                #region 申请放单	如果放单.已申请放单=true，则隐藏该按钮。
                if ((short)listData.State >= (short)ReleaseBLState.Released)
                {
                    barApply.Visibility = BarItemVisibility.Never;
                    barCancelApply.Visibility = BarItemVisibility.Never;
                }
                else if (listData.IsApplyTelex)
                {
                    barApply.Visibility = BarItemVisibility.Never;
                    barCancelApply.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    barApply.Visibility = BarItemVisibility.Always;
                    barCancelApply.Visibility = BarItemVisibility.Never;
                }

                //if (listData.IsApplyTelex)
                //    barApply.Caption = LocalData.IsEnglish ? "Cancel&Apply" : "取消申请(&A)";
                //else
                //    barApply.Caption = LocalData.IsEnglish ? "&Apply" : "客户申请电放(&A)";

                #endregion

                #region 放单	如果放单. 已寄正本=true or 放单. 已通知电放=true，则隐藏该按钮。
                if ((short)listData.State >= (short)ReleaseBLState.RC)
                {
                    barReleaseBL.Visibility = BarItemVisibility.Never;
                    barCancelReleaseBL.Visibility = BarItemVisibility.Never;
                }
                else if ((short)listData.State >= (short)ReleaseBLState.Released)
                {
                    barReleaseBL.Visibility = BarItemVisibility.Never;
                    barCancelReleaseBL.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    barReleaseBL.Visibility = BarItemVisibility.Always;
                    barCancelReleaseBL.Visibility = BarItemVisibility.Never;
                }

                //if ((short)listData.State >= (short)ReleaseBLState.RC)
                //    barReleaseBL.Visibility = BarItemVisibility.Never;
                //else
                //{
                //    if (listData.State == ReleaseBLState.Released)
                //    {
                //        if (listData.ReleaseType == ReleaseType.Original)
                //            barReleaseBL.Caption = LocalData.IsEnglish ? "Cancel&Release" : "取消放单(&R)";
                //        else
                //            barReleaseBL.Caption = LocalData.IsEnglish ? "Cancel&Release" : "取消放单(&R)";
                //    }
                //    else
                //    {
                //        if (listData.ReleaseType == ReleaseType.Original)
                //            barReleaseBL.Caption = LocalData.IsEnglish ? "&Release" : "放单(&R)";
                //        else
                //            barReleaseBL.Caption = LocalData.IsEnglish ? "&Release" : "放单(&R)";
                //    }

                //    barReleaseBL.Visibility = BarItemVisibility.Always;
                //}
                #endregion

                if ((listData.OperationType == OperationType.Other || listData.OperationType == OperationType.AirExport)
                    && !users.Contains(ApplicationContext.Current.Username.ToUpper()))
                {
                    barApply.Enabled = barCancelApply.Enabled = barEdit.Enabled = barReceived.Enabled = barReleaseBL.Enabled
                    = barCancelReleaseBL.Enabled = barChangeToOriginal.Enabled = barChangeToTelex.Enabled = false;
                }
                 
            }
        }

        #endregion
    }
}
