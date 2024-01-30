using System.Diagnostics;
using System.Reflection;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FCM.Common.UI.BillRevise
{
    [SmartPart]
    public partial class OEBillRevisePart : XtraUserControl    // : DispatchDocumentCompareBase
    {

        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;
        }

        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        private RefreshService RefreshService
        {
            get { return ClientHelper.Get<RefreshService, RefreshService>(); }
        }
        /// <summary>
        /// 海出操作ID
        /// </summary>
        public Guid NewOperationID { get; set; }

        /// <summary>
        /// 海进操作ID
        /// </summary>
        public Guid OldOperationID { get; set; }


        public OEBillRevisePart()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                Disposed += delegate
                {   
                    
                    lstOEBillReviseBasePart = null;
                    result = null;
                    lstSPInfo = null;
                    if (workItem != null)
                    {
                        workItem.Workspaces.Remove(tabWsCampare);
                        workItem.SmartParts.Remove(this);
                        

                        if (lstOEBillReviseBasePart != null && lstOEBillReviseBasePart.Count>0)
                        {
                            foreach (var o in lstOEBillReviseBasePart)
                                workItem.SmartParts.Remove(o);
                        }
                        workItem = null;

                    }
                };

            }
           

        }



        List<OEBillReviseBasePart> lstOEBillReviseBasePart = new List<OEBillReviseBasePart>();

        List<SmartPartInfo> lstSPInfo = new List<SmartPartInfo>();
        List<SimpleBusinnessInfo> result = null;

        private void OIDispachCompare_Load(object sender, EventArgs e)
        {

            tabWsCampare = workItem.Workspaces.AddNew(typeof(TabWorkspace), Guid.NewGuid().ToString()) as TabWorkspace;// new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();
            tabWsCampare.Dock = DockStyle.Fill;
            pnlCenter.Controls.Add(tabWsCampare);

            lstOEBillReviseBasePart.Clear();
            lstSPInfo.Clear();
            result = FCMCommonService.GetSimpleBusinessByBookingID(NewOperationID);
            // result.Add( new SimpleBusinnessInfo(){ BusinessID =result[0].BusinessID, BusinessNO= result[0].BusinessNO+"(B)"});
            if (result == null) return;

            txtRemark.Text = result[0].Remark;
            lblDisptUserValue.Text = result[0].DispatchUserName;
            lblDisptDateValue.Text = result[0].DispatchDate.ToString("yyyy-MM-dd hh:mm:ss");

            for (int i = 0; i < result.Count; i++)
            {
                OEBillReviseBasePart ss = workItem.SmartParts.AddNew<OEBillReviseBasePart>(Guid.NewGuid().ToString());

                ss.NewOperationID = NewOperationID;
                ss.OldOperationID = result[i].OIBusinessID;
                ss.CurrentSimpleBusinnessInfo = result[i];
               
                lstOEBillReviseBasePart.Add(ss);

                SmartPartInfo spinfo = new SmartPartInfo(result[i].OIBusinessNO, result[i].OIBusinessNO);
                lstSPInfo.Add(spinfo);

            }
            OldOperationID = result[0].OIBusinessID;
            for (int i = result.Count - 1; i >= 0; i--)
            {
                tabWsCampare.Show(lstOEBillReviseBasePart[i], lstSPInfo[i]);
            }

            if (LocalData.IsEnglish)
            {
                simpleButton1.Text = "Accept";
            }

        }

        private void OIDispachCompare_Resize(object sender, EventArgs e)
        {
            tabWsCampare.Height = Height - panelControl1.Height;
            gcRemark.Width = Width - 160;
        }
        ////签收按钮事件
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                string remark = txtRemark.Text.Trim();

                int result= OceanExportService.AcceptBillRevise(NewOperationID, remark,LocalData.UserInfo.LoginID);

                XtraMessageBox.Show(LocalData.IsEnglish ? "accepting revised D/C fees is successful!" : " 接受修订成功!");             
                
                    if (RefreshService != null && RefreshService.Refresh != null)
                        RefreshService.Refresh();

                    if (RefreshService != null && RefreshService.RefreshAcceptReviseState != null)
                        RefreshService.RefreshAcceptReviseState();

                FindForm().Close();
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName,"VIEW", "签收账单修订");
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowError((LocalData.IsEnglish ? "Accept Failed" : "签收失败") + ex.Message);
                //LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Accept Failed" : "签收失败") + ex.Message);
                //return false;
            }

        }

        private void butReject_Click(object sender, EventArgs e)
        {
            RejectReasonPart RejectReasonPart = new RejectReasonPart();
            RejectReasonPart.OperationID = OldOperationID;
            RejectReasonPart.OperationType = OperationType.OceanImport;
            //DialogResult result = Utility.ShowDialog(RejectReasonPart, LocalData.IsEnglish ? "Reject Reason" : "拒签原因");
            DialogResult result = PartLoader.ShowDialog(RejectReasonPart, LocalData.IsEnglish ? "Reject Reason" : "拒签原因");
            if (result == DialogResult.OK)
            {
                FindForm().Close();
            }
        }

    }
}
