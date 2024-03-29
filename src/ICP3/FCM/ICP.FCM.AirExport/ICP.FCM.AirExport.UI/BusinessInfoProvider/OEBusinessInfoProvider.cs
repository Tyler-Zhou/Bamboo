﻿using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.UI.BusinessInfoProvider
{
    public class OEBusinessInfoProvider : IBusinessInfoProvider
    {

        #region IBusinessInfoProvider 成员

        /// <summary>
        /// 显示业务信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <param name="workspaceName">workspaceName,传入空值以一个对话框形式打开
        public void ShowBusinessInfo(OperationType operationType, Guid operationID, string workspaceName)
        {
            //IReportViewer viewer = AirExportPrintHelper.PrintBusinessInfo(operationID);
            //viewer.ReportMessage += delegate(object data, EventArgs e)
            //{
            //    ShowBussinessCopyReport(data);
            //};
           
        }


        /// <summary>
        /// 显示业务信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <param name="isMbl">是否MBL</param>
        /// <param name="workspaceName">workspaceName,传入空值以一个对话框形式打开
        public void ShowBusinessInfo(OperationType operationType, Guid operationID, bool isMbl, string workspaceName)
        {
            //IReportViewer viewer = AirExportPrintHelper.PrintBusinessInfo(operationID);
            //viewer.ReportMessage += delegate(object data, EventArgs e)
            //{
            //    ShowBussinessCopyReport(data);
            //};
        }

        private void ShowBussinessCopyReport(object data)
        {
            //if (data == null || data.ToString() == string.Empty) return;

            //string[] datas = data.ToString().Split(';');//报表中的数据返回约定
            //Guid blID = new Guid(datas[0].ToString());
            //BLType bltype = string.IsNullOrEmpty(datas[1]) ? BLType.MAWB : BLType.HAWB;

            //AirExportPrintHelper.PrintBusinessInfoCopy(blID, bltype);
           
        }

        #endregion
    }
}
