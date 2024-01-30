using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FCM.Common.UI.Document;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using System.Reflection;
using ICP.DataCache.ServiceInterface;
using System.Diagnostics;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OIDispachCompareNew : BasePart
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem workItem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }


        /// <summary>
        /// 出口操作ID
        /// </summary>
        public Guid NewOperationID { get; set; }

        /// <summary>
        /// 进口操作ID
        /// </summary>
        public Guid OldOperationID { get; set; }

        /// <summary>
        /// 分文档日志ID
        /// </summary>
        public Guid fileLog { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsVisibleAccept = false;

        /// <summary>
        /// 签收当前行数据
        /// </summary>
        public OceanBusinessDownLoadList OceanBusinessDownLoadList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OIDispachCompareNew()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                Disposed += delegate
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
        List<DispatchDocumentCompareNew> lstDispatchDocumentCompare = new List<DispatchDocumentCompareNew>();
        List<SmartPartInfo> lstSPInfo = new List<SmartPartInfo>();
        List<SimpleBusinnessInfo> result = null;

        private void OIDispachCompareNew_Load(object sender, EventArgs e)
        {
            tabWsCampare = workItem.Workspaces.AddNew(typeof(TabWorkspace), Guid.NewGuid().ToString()) as TabWorkspace;

            tabWsCampare.Location = new System.Drawing.Point(0, 0);
            tabWsCampare.Dock = DockStyle.Fill;
            tabWsCampare.Height = Height - panelControl1.Height;


            pnlTop.Controls.Add(tabWsCampare);
            tabWsCampare.Dock = DockStyle.Fill;

            lstDispatchDocumentCompare.Clear();
            lstSPInfo.Clear();

            result = FCMCommonService.GetOEIDByOIID(OldOperationID);
            string fileLogstr = FCMCommonService.GetDispatchNewLogID(result[0].OEBusinessID);
            fileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(fileLogstr);

            DispatchDocumentCompareNew ss;
            if (result[0].Type == OperationType.OceanImport || result[0].Type == OperationType.OceanExport)
            {
                ss = workItem.SmartParts.AddNew<OIDispachCompareBasePartNew>(result[0].OEBusinessNO);
            }
            else
            {
                ss = workItem.SmartParts.AddNew<OIDispachCompareBasePartAir>(result[0].OEBusinessNO);
            }

            ss.CurrentSimpleBusinnessInfo = result[0];
            ss.NewOperationID = result[0].OEBusinessID;
            ss.OldOperationID = result[0].OIBusinessID;


            ss.OceanBusinessDownLoadList = OceanBusinessDownLoadList;
            lstDispatchDocumentCompare.Add(ss);


            SmartPartInfo spinfo = new SmartPartInfo(result[0].OIBusinessNO, result[0].OIBusinessNO);
            lstSPInfo.Add(spinfo);
            txtRemark.Text = result[0].Remark;


            tabWsCampare.Show(lstDispatchDocumentCompare[0], lstSPInfo[0]);

            if (IsVisibleAccept)
            {
                butAccept.Visible = false;
                txtRemark.Dock = DockStyle.Fill;
            }
            if (LocalData.IsEnglish)
            {
                butAccept.Text = "Accept";
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ColumnName GetControlAndName()
        {
            ColumnName colName = new ColumnName();
            if (LocalData.IsEnglish)
            {
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

        /// <summary>
        /// 签收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAccept_Click(object sender, EventArgs e)
        {
            string exceptionstr = string.Empty;
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                ClientHelper.SetApplicationContext();
                //AirBusinessInfo info =  AirImportService.GetBusinessInfo(OldOperationID);
                if (result[0].Type == OperationType.AirExport || result[0].Type == OperationType.AirImport)
                {
                    AirImportService.AcceptDispatchFiles(OldOperationID, fileLog, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                }
                else
                {
                    OceanImportService.AcceptDispatchFiles(OldOperationID, fileLog, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                }

                FindForm().Close();
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "ACCEPT", string.Format("签收分发文件;OperationID[{0}]", OldOperationID));

                string message = "";
                if (!LocalData.IsEnglish)
                {
                    message = "签收文件后，请仔细核对文档与账单列表代理账单金额是否一致！";
                }
                else
                {
                    message = "Accepting Docs is done. Please verify and make sure the downloaded fees are consistent with PDF fees.";
                }

                DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                     LocalData.IsEnglish ? "Tip" : "提示",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Question);
            }
            catch (Exception ex)
            {
                string strmessage = ClientHelper.GetErrorMessage(ex);
                MethodBase method = MethodBase.GetCurrentMethod();
                StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "ACCEPT", string.Format("签收分发文件失败;OperationID[{0}]", OldOperationID));
                MessageBoxService.ShowInfo((LocalData.IsEnglish ? "Accept Failed" : "签收失败") + strmessage);
            }
        }
        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }
    }
}
