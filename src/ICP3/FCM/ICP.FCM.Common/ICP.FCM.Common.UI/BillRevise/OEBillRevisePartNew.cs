using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents;

namespace ICP.FCM.Common.UI.BillRevise
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OEBillRevisePartNew :  XtraUserControl
    {
        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 海进服务
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
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

        public OperationType OperationType { get; set; }


        public OEBillRevisePartNew()
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


                        if (lstOEBillReviseBasePart != null && lstOEBillReviseBasePart.Count > 0)
                        {
                            foreach (var o in lstOEBillReviseBasePart)
                                workItem.SmartParts.Remove(o);
                        }
                        workItem = null;

                    }
                };
            }
        }

        List<OEBillReviseBasePartNew> lstOEBillReviseBasePart = new List<OEBillReviseBasePartNew>();

        List<SmartPartInfo> lstSPInfo = new List<SmartPartInfo>();
        List<SimpleBusinnessInfo> result = null;

        private void OEBillRevisePartNew_Load(object sender, EventArgs e)
        {
            tabWsCampare = workItem.Workspaces.AddNew(typeof(TabWorkspace), Guid.NewGuid().ToString()) as TabWorkspace;// new Microsoft.Practices.CompositeUI.WinForms.TabWorkspace();

            tabWsCampare.Dock = DockStyle.Fill;
           
            pnlFill.Controls.Add(tabWsCampare);

            lstOEBillReviseBasePart.Clear();
            lstSPInfo.Clear();
            result = FCMCommonService.GetOEIDByOIID(NewOperationID);

            if (result == null) return;

         
            for (int i = 0; i < result.Count; i++)
            {
                  int state = OperationAgentService.UspGetDispatchLogState(result[i].OIBusinessID);
                  if (state > 1 && state < 6)
                  {
                      OEBillReviseBasePartNew ss = workItem.SmartParts.AddNew<OEBillReviseBasePartNew>(Guid.NewGuid().ToString());

                      // ss.Workitem = workItem;
                      ss.opType = result[i].Type;
                      ss.NewOperationID = NewOperationID;
                      ss.OldOperationID = result[i].OIBusinessID;
                      ss.CurrentSimpleBusinnessInfo = result[i];

                      lstOEBillReviseBasePart.Add(ss);

                      SmartPartInfo spinfo = new SmartPartInfo(result[i].OIBusinessNO, result[i].OIBusinessNO);
                      lstSPInfo.Add(spinfo);

                      txtRemark.Text = result[i].Remark;
                      lblDisptUserValue.Text = result[i].DispatchUserName;
                      lblDisptDateValue.Text = result[i].DispatchDate.ToString("yyyy-MM-dd hh:mm:ss");
                      OldOperationID = result[i].OIBusinessID;
                  }
                //tabWsCampare.TabPages.Add(result[i].BusinessNO);
            }
     
            tabWsCampare.Show(lstOEBillReviseBasePart[0], lstSPInfo[0]);

            if (LocalData.IsEnglish)
            {
                simpleButton1.Text = "Accept";
            }

        }

        private void OEBillRevisePartNew_Resize(object sender, EventArgs e)
        {
            tabWsCampare.Height = Height - panelControl1.Height;
            gcRemark.Width = Width - 160;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                string remark = txtRemark.Text.Trim();
                int state = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    state = OperationAgentService.UspGetDispatchLogState(result[i].OIBusinessID);
                    if(state >1 && state <6)
                    {
                        string logresult = FCMCommonService.GetDispatchNewLogID(result[i].OIBusinessID);
                        if (!string.IsNullOrEmpty(logresult))
                        {
                            Guid DispatchFileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(logresult);
                            if (result[i].Type == OperationType.OceanExport || result[i].Type == OperationType.OceanImport)
                            {
                                OceanImportService.AcceptDispatchFiles(NewOperationID, DispatchFileLog, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                            }
                            else
                            {
                                AirImportService.AcceptDispatchFiles(NewOperationID, DispatchFileLog, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                            }
                           
                        }
                    }
                }

                XtraMessageBox.Show(LocalData.IsEnglish ? "accepting revised D/C fees is successful!" : " 接受修订成功!");

                FindForm().Close();
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "ACCEPT", string.Format("签收账单修订;OperationID[{0}]", NewOperationID));
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "ACCEPT", string.Format("签收分发文件失败;OperationID[{0}]", NewOperationID));
                MessageBoxService.ShowInfo((LocalData.IsEnglish ? "Accept Failed" : "签收失败") + ex.Message);
            }
        }
    }
}
