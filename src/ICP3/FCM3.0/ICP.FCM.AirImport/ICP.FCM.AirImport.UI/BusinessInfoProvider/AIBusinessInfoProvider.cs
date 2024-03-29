﻿using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.AirImport.UI.Common;
using ICP.FCM.AirImport.ServiceInterface;


namespace ICP.FCM.AirImport.UI.BusinessInfoProvider
{
    public class AIBusinessInfoProvider : IBusinessInfoProvider
    {
        #region 服务注入



        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public AirImportPrintHelper AirImportPrintHelper { get; set; }

        #endregion

        #region IBusinessInfoProvider 成员

        /// <summary>
        /// 显示业务信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <param name="workspaceName">workspaceName,传入空值以一个对话框形式打开
        public void ShowBusinessInfo(OperationType operationType, Guid operationID, string workspaceName)
        {
            IReportViewer viewer = AirImportPrintHelper.PrintBusinessInfo(operationID);
            viewer.ReportMessage += delegate(object data, EventArgs e)
            {
                ShowBussinessCopyReport(data);
            };

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
            IReportViewer viewer = AirImportPrintHelper.PrintBusinessInfo(operationID);
            viewer.ReportMessage += delegate(object data, EventArgs e)
            {
                ShowBussinessCopyReport(data);
            };
        }

        private void ShowBussinessCopyReport(object data)
        {
            if (data == null || data.ToString() == string.Empty) return;

            string[] datas = data.ToString().Split(';');//报表中的数据返回约定
            Guid blID = new Guid(datas[0].ToString());
            FCMBLType bltype = string.IsNullOrEmpty(datas[1]) ? FCMBLType.MBL : FCMBLType.HBL;

            //OI不支持MBL打印
            if (bltype == FCMBLType.MBL) return;

            //AirImportPrintHelper.PrintBusinessInfoCopy(blID); --pearl
        }

        #endregion
    }
}
