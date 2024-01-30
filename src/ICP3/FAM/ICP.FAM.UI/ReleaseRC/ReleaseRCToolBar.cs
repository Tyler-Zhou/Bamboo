using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class ReleaseRCToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public ReleaseRCToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            BulidCommond();

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

        private void BulidCommond()
        {
            barEdit.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_Edit].Execute(); };
            barReceived.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_Received].Execute(); };
            barApply.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_Apply].Execute(); };
            barTransit.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_ReleaseBL].Execute(); };
            //barChangeType.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_ChangeType].Execute(); };

            barRefresh.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_Refresh].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Commond_ShowSearch].Execute(); };

            barViewBusinessInfo .ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Command_ViewBusinessInfo  ].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Command_Bill].Execute(); };

            barExceptionReleaseRC.ItemClick += delegate { Workitem.Commands[ReleaseRCCommondConstants.Command_ExceptionReleaseRC].Execute(); };


            barClose.ItemClick += delegate { FindForm().Close(); };
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
            ReleaseRCList listData = value as ReleaseRCList;
            if (listData == null)
            {
                barApply.Enabled = barEdit.Enabled = barReceived.Enabled //= barTransit.Enabled
                    = barViewBusinessInfo.Enabled =  barBill.Enabled  = false;
            }
            else
            {
                barApply.Enabled = barEdit.Enabled = barReceived.Enabled //= barTransit.Enabled
                    = barViewBusinessInfo.Enabled =  barBill.Enabled = true;

                //if (listData.ReleaseType == ReleaseType.Telex)
                //    barChangeType.Caption = LocalData.IsEnglish ? "Chan&geToOriginal" : "改为正本";
                //else
                //    barChangeType.Caption = LocalData.IsEnglish ? "Chan&geToTelex" : "改为电放";

                #region 签收	如果放单.状态=已放货，则显示该按钮，否则隐藏该按钮。
                if (listData.State == ReleaseRCState.RBL)
                    barReceived.Enabled =true;
                else
                    barReceived.Enabled = false ;
                #endregion

                #region 申请放单	如果放单.已申请放单=true，则隐藏该按钮。

                //if (listData.ReleaseType == ReleaseType.Telex)
                //    barChangeType.Caption = LocalData.IsEnglish ? "Chan&geToOriginal" : "改为正本";
                //else
                //    barChangeType.Caption = LocalData.IsEnglish ? "Chan&geToTelex" : "改为电放";

                //if (listData.IsApplyTelex)
                    //barApply.Caption =LocalData.IsEnglish ? "Cancel&Apply" : "取消申请(&A)";
                //else
                   // barApply.Caption = LocalData.IsEnglish ? "&Apply" : "申请(&A)";
                
               
                #endregion

                #region 放单	如果放单. 已寄正本=true or 放单. 已通知电放=true，则隐藏该按钮。
                //if ((short)listData.State > (short)ReleaseBLState.Released) { }
                ////barReleaseBL.Visibility = BarItemVisibility.Never;
                //else
                //{
                //    if (listData.State == ReleaseRCState.RC)
                //    {
                //        if (listData.ReleaseType == ReleaseType.Original)
                //            barTransit.Caption = LocalData.IsEnglish ? "Cancel&ReleaseRC" : "取消放货(&R)";
                //        else
                //            barTransit.Caption = LocalData.IsEnglish ? "Cancel&ReleaseRC" : "取消放货((&R)";
                //    }
                //    else
                //    {
                //        if (listData.ReleaseType == ReleaseType.Original)
                //            barTransit.Caption = LocalData.IsEnglish ? "&ReleaseRC" : "放货(&R)";
                //        else
                //            barTransit.Caption = LocalData.IsEnglish ? "&ReleaseRC" : "放货(&R)";
                //    }

                //    barTransit.Visibility = BarItemVisibility.Always;
                //}
                #endregion
            }
        }

        #endregion


    }
}
