using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.UI.Common
{
    public class AirExportPrintHelper
    {
        #region services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        public IAirExportService AirReportSrvice
        {
            get
            {
                return ServiceClient.GetService<IAirExportService>();
            }
        }

        #endregion

        /// <summary>
        /// 获取空运出口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\
        /// </summary>
        public string GetAEReportPath()
        {
            return Application.StartupPath + "\\Reports\\AirExport\\";
        }


        public IReportViewer PrintAEOrder(Guid bookID,Guid companyID)
        {
            AEOrderReportData data = AirReportSrvice.GetAEOrderReportData(bookID, companyID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetAEReportPath();
            if (LocalData.IsEnglish) fileName += "AE_OrderInfo_EN.frx";
            else fileName += "AE_OrderInfo_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            reportSource.Add("OrderFee", data.Fees);

            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.AirExport;
            message.UserProperties.OperationId = bookID;
            message.UserProperties.FormType = FormType.ShippingOrder;
            message.UserProperties.FormId = bookID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }
    }
}
