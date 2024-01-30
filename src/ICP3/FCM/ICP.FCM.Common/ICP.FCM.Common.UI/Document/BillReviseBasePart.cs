using System;
using System.Collections.Generic;
using System.Drawing;
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.Document
{
    /// <summary>
    /// 签收海进账修订页面
    /// </summary>
    [SmartPart]
    public partial class BillReviseBasePart : BaseEditPart, IDisposable
    {
        /// <summary>
        /// 工作服务
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }


        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }

        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// FCM公共服务
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        /// <summary>
        /// 海出操作ID
        /// </summary>
        public Guid NewOperationID { get; set; }

        /// <summary>
        /// 海进操作ID
        /// </summary>
        public Guid OldOperationID { get; set; }


        public OperationType opType { get; set; }


        /// <summary>
        /// 当前比较的账单列表
        /// </summary>
        protected List<Fee> CurrBill = new List<Fee>();


        public SimpleBusinnessInfo CurrentSimpleBusinnessInfo = null;
        public BillReviseBasePart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.gridBill.DataSource = null;
                this.CurrBill = null;
                this.CurrentSimpleBusinnessInfo = null;
                this.bsBills.DataSource = null;
                this.bsBills.Dispose();

                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        private void OEBillReviseBasePart_Load(object sender, EventArgs e)
        {

            BindData();
            ChangeEnglish();

            //profitComparePart1.Init(NewOperationID, OldOperationID, opType);
        }

        public virtual List<Fee> GetCompareBillAndChargeInfo()
        {
            return null;
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            CurrBill = GetCompareBillAndChargeInfo();
            //if (CurrBill != null && CurrBill.Count > 0)
            //{
            //    foreach (Bill b in CurrBill)
            //    {
            //        if (b.UpdateState == 3)
            //        {
            //            b.UpdateState = 1;
            //        }
            //        else if (b.UpdateState == 1)
            //        {
            //            b.UpdateState = 3;
            //        }

            //        foreach (Fee f in b.Fees)
            //        {
            //            if (f.UpdateState == 3)
            //            {
            //                f.UpdateState = 1;
            //            }
            //            else if (f.UpdateState == 1)
            //            {
            //                f.UpdateState = 3;
            //            }
            //        }
            //    }
            //}
            if (CurrBill!=null)
            {
                bsBills.DataSource = CurrBill.FindAll(delegate(Fee item) { return item.UpdateState != 0; });
            } 

        
          //  ExpandGridView();
            chkAllCompare_CheckedChanged(null, null);
        }
        ///// <summary>
        ///// 默认展开子表
        ///// </summary>
        //private void ExpandGridView()
        //{
        //    gvBill.BeginUpdate();
        //    for (int i = 0; i < bsBills.Count; i++)
        //    {
        //        gvBill.SetMasterRowExpanded(i, true);
        //    }
        //    gvBill.EndUpdate();


        //}
        /// <summary>
        /// 显示对比信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAllCompare_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllCompare.Checked)
            //{


            //    gridColNewSumMoney.Visible = true;

            //    gridColWay.VisibleIndex = 0;
            //    gridColChargeCode.VisibleIndex = 1;
            //    gridColChargeName.VisibleIndex = 2;
            //    gridColIsAgent.VisibleIndex = 3;
            //    gridColOldSumMoney.VisibleIndex = 4;
            //    gridColNewSumMoney.VisibleIndex = 5;
            //    gridColOldRemark.VisibleIndex = 6;
            //    gridColNewRemark.VisibleIndex = 7;
            //    gridColFeeIsState.VisibleIndex = 8;
            //    bsBills.DataSource = CurrBill;
            //}
            //else
            //{
                if (CurrBill != null)
                {
                    bsBills.DataSource = CurrBill.FindAll(delegate(Fee item) { return item.UpdateState != 0; });
                }
                //gridColNewSumMoney.Visible = false;
                //gridColNewRemark.Visible = false;
                gridColOldSumMoney.Visible = false;
                gridColOldRemark.Visible = false;
            //}
        }

        private void gvBill_MasterRowCollapsing(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventArgs e)
        {
            e.Allow = false;
        }

        public void  ChangeEnglish()
        {

            if (LocalData.IsEnglish)
            {
                gridColWay.Caption = "Way";
                gridColChargeCode.Caption = "Change Code";
                gridColChargeName.Caption = "Change name";
                gridColIsAgent.Caption =  "Agent";
                gridColOldSumMoney.Caption = "OE Amount";
                gridColNewSumMoney.Caption = "OI Amount";
                gridColOldRemark.Caption = "OE Remark";
                gridColNewRemark.Caption = "OI Remark";
                gridColFeeIsState.Caption = "State";
                //chkAllCompare.Text ="Show Original Value";
                gcBillInfo.Text = "Fees Information";

            }
           
        }

        private void gvFees_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName == "NewSumMoney")
            {
                GridView gv = sender as GridView;
                Fee obj = gv.GetRow(e.RowHandle) as Fee;
                if (obj == null) return;

                if (obj.OldSumMoney != obj.NewSumMoney)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }

        }
        
        private void gvFees_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (CurrBill != null && CurrBill.Count == 0)
            {
                string str = LocalData.IsEnglish ? "Fees has not yet been revised!" : "费用没有发生修改!";

                Font f = new Font("宋体", 10, FontStyle.Bold);
                Rectangle r = new Rectangle(e.Bounds.Top + 10, e.Bounds.Left + 30, e.Bounds.Right - 5, e.Bounds.Height - 5);
                e.Graphics.DrawString(str, f, Brushes.Black, r);

            }
        }


       
    }
}
