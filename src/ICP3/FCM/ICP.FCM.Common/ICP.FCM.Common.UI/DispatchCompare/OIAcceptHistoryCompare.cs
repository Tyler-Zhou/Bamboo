using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.UI.Document;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    [SmartPart]
    public partial class OIAcceptHistoryCompare : DevExpress.XtraEditors.XtraUserControl    // : DispatchDocumentCompareBase
    {

        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;
        }

        public IFCMCommonService FCMCommonService 
        {
            get 
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }


        ///// <summary>
        ///// 当业务操作上下文类
        ///// </summary>
        //public ICP.DataCache.ServiceInterface.BusinessOperationContext CurrentOperationContext { get; set; }

        //// [ServiceDependency]
        ///// <summary>
        ///// 历史业务操作上下文类
        ///// </summary>
        //public ICP.DataCache.ServiceInterface.BusinessOperationContext HistoryOperationContext { get; set; }

        /// <summary>
        /// 海出操作ID
        /// </summary>
        public Guid NewOperationID { get; set; }

        /// <summary>
        /// 海进操作ID
        /// </summary>
        public Guid OldOperationID { get; set; }

        public Guid OperationID
        {
            get;
            set;
        }
        public Guid BeforeApplyID
        {
            get;
            set;
        }
        public Guid AfterApplyID
        {
            get;
            set;
        }

        /// <summary>
        /// 签收当前行数据
        /// </summary>
        public OceanBusinessDownLoadList OceanBusinessDownLoadList { get; set; }


        public OIAcceptHistoryCompare()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    OceanBusinessDownLoadList = null;
                    lstDispatchDocumentCompare = null;
                    result = null;
                    lstSPInfo = null;
                    
                    if (workItem != null)
                    {
                        workItem.Workspaces.Remove(tabWsCampare);
                        workItem.SmartParts.Remove(this);
                        workItem = null;
                        if (lstDispatchDocumentCompare != null && lstDispatchDocumentCompare.Count > 0)
                        {
                            foreach (var o in lstDispatchDocumentCompare)
                                workItem.SmartParts.Remove(o);
                        }
                    }
                };
            }
        }



        List<OIAcceptHistoryBasePart> lstDispatchDocumentCompare = new List<OIAcceptHistoryBasePart>();

        List<SmartPartInfo> lstSPInfo = new List<SmartPartInfo>();
        List<SimpleBusinnessInfo> result = null;

        private void OIDispachCompare_Load(object sender, EventArgs e)
        {

            this.tabWsCampare = workItem.Workspaces.AddNew(typeof(TabWorkspace), Guid.NewGuid().ToString()) as TabWorkspace;// new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();

            // tabWsCampare
            // 
            this.tabWsCampare.Location = new System.Drawing.Point(0, 0);
            this.tabWsCampare.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabWsCampare.Height = this.Height - panelControl1.Height;

            //this.tabWsCampare.Name = "tabWsCampare";
            //this.tabWsCampare.SelectedIndex = 0;
            //this.tabWsCampare.Size = new System.Drawing.Size(802, 551);
            //this.tabWsCampare.TabIndex = 2;
            //  this.tabWsCampare.SelectedIndexChanged += new System.EventHandler(this.tabWsCampare_SelectedIndexChanged);
            pnlFill.Controls.Add(this.tabWsCampare);

            lstDispatchDocumentCompare.Clear();
            lstSPInfo.Clear();
            result = FCMCommonService.GetSimpleBusinessByBookingID(OperationID);
            // result.Add( new SimpleBusinnessInfo(){ BusinessID =result[0].BusinessID, BusinessNO= result[0].BusinessNO+"(B)"});

            for (int i = 0; i < result.Count; i++)
            {
                OIAcceptHistoryBasePart ss = workItem.SmartParts.AddNew<OIAcceptHistoryBasePart>(result[i].OIBusinessNO);

                // ss.Workitem = workItem;
                ss.OperationID = result[i].OIBusinessID;
      
                ss.CurrentSimpleBusinnessInfo = result[i];
                ss.BeforeApplyID = BeforeApplyID;
                ss.AfterApplyID = AfterApplyID;

                ss.OceanBusinessDownLoadList = this.OceanBusinessDownLoadList;
                lstDispatchDocumentCompare.Add(ss);

                SmartPartInfo spinfo = new SmartPartInfo(result[i].OIBusinessNO, result[i].OIBusinessNO);
                lstSPInfo.Add(spinfo);
                //tabWsCampare.TabPages.Add(result[i].BusinessNO);

            }
            for (int i = result.Count - 1; i >= 0; i--)
            {
                tabWsCampare.Show(lstDispatchDocumentCompare[i], lstSPInfo[i]);
            }

        }

        public ColumnName GetControlAndName()
        {
            ColumnName colName = new ColumnName();
            if (LocalData.IsEnglish)
            {
                //colName.ColOEBillNO = "OE Bill NO";
                //colName.ColOIBillNO = "OI Bill NO";
                //colName.ColOECurrencyType = "OE Currency";
                //colName.ColOICurrencyType = "OI Currency";
                //colName.ColOESumMoney = "OE Amount";
                //colName.ColOISumMoney = "OI Amount";

                colName.ColFeeCode = "Fee Code";
                colName.ColWay = "Way";
                colName.ColAgent = "Agent";
                colName.ColOEFeeSumMoney = "OE Amount";
                colName.ColOIFeeSumMoney = "OI Amount";
                colName.ColOERemark = "OE Remark";
                colName.ColOIRemark = "OI Remark";
            }
            else
            {
                colName.ColOEBillNO = "账单号";
                colName.ColOIBillNO = "账单号";
                colName.ColOECurrencyType = "新币种";
                colName.ColOICurrencyType = "旧币种";
                colName.ColOESumMoney = "新金额";
                colName.ColOISumMoney = "旧金额";

                colName.ColFeeCode = "费用代码";
                colName.ColWay = "方   向";
                colName.ColAgent = "代   理";
                colName.ColOEFeeSumMoney = "新金额";
                colName.ColOIFeeSumMoney = "旧金额";
                colName.ColOERemark = "新备注";
                colName.ColOIRemark = "旧备注";

            }
            return colName;
        }

        private void OIDispachCompare_Resize(object sender, EventArgs e)
        {
            this.tabWsCampare.Height = this.Height - panelControl1.Height;
            gcRemark.Width = this.Width - 160;
        }
      
    }
}
