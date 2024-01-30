using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.Common.UI.GoTo
{
    public class ClientServers : IClientServers
    {
    
        /// <summary>
        /// 根据条件生成当前跳转页面的参数
        /// </summary>
        /// <param name="go">实体对象</param>
        /// <param name="methods">方法名</param>
        /// <param name="checkBoxName">控件的名称(如果用到账单的方法需要传当前的控件名称)</param>
        /// <returns></returns>
        public object[] GetGotoparameters(GoToObject go, string methods, string checkBoxName)
        {

            var oj = new List<object>();
         
           
            switch (methods)
            {
                case "EditBooking":
                    {
                        var editPartShowCriteria = new EditPartShowCriteria
                        {
                            BillNo = go.OperationId,
                            OperationNo = go.OperationNo
                        };
                        Dictionary<string, object> dictionary = BusinessParameter(ActionType.Edit, go.OperationNo,go.OperationId);
                        oj.Add(editPartShowCriteria);
                        oj.Add(dictionary);
                         
                    }
                    break;
                case "EditMBL":
                    {
                        Dictionary<string, object> dictionary = BusinessParameter(ActionType.Edit, go.Mblno, go.Mblid);
                        oj.Add(go.OperationNo);
                        oj.Add(go.Mblno);
                        oj.Add(dictionary);
                    }
                    break;
                case "EditHBL":
                    {
                        Dictionary<string, object> dictionary = BusinessParameter(ActionType.Edit, go.Hblno, go.Hblid);
                        oj.Add(go.OperationNo);
                        oj.Add(go.Hblno);
                        oj.Add(dictionary);
                    }
                    break;
                case "EditOceanImportBooking":
                    oj.Add(go.OperationId);
                    break;
                case "OpenBill":
                    oj.Add(go.OperationId);
                    if (checkBoxName.ToUpper().Contains("OE"))
                    {
                        oj.Add(OperationType.OceanExport);
                    }
                    else if (checkBoxName.ToUpper().Contains("OI"))
                    {
                        oj.Add(OperationType.OceanImport);
                    }
                    else if (checkBoxName.ToUpper().Contains("AI"))
                    {
                        oj.Add(OperationType.AirImport);
                    }
                    else if (checkBoxName.ToUpper().Contains("AE"))
                    {
                        oj.Add(OperationType.AirExport);
                    }
                    break;
                case "EditAirExportBooking":
                    oj.Add(go.OperationId);
                    break;
                case "EditAirExportMBL":
                    oj.Add(go.Mblid);
                    break;
                case "EditAirExportHBL":
                    oj.Add(go.Hblid);
                    break;
                case "EditAirImportBooking":
                    oj.Add(go.OperationId);
                    break;
            }
            return oj.ToArray();

        }

        /// <summary>
        /// 返回键值对
        /// </summary>
        /// <param name="actionType">动作类型</param>
        /// <param name="opeationNo">业务号</param>
        /// <param name="operationId">业务ID</param>
        /// <returns></returns>
        public Dictionary<string, object> BusinessParameter(
            ActionType actionType, string opeationNo, Guid operationId)
        {
            var businessOperation = new BusinessOperationParameter();
            if (actionType == ActionType.Edit)
            {
                businessOperation.Context = new BusinessOperationContext
                {
                    OperationNO = opeationNo,
                    OperationID = operationId
                };
            }
            else
            {
                businessOperation.Context = new BusinessOperationContext();
            }
            businessOperation.ActionType = actionType;
            var dic = new Dictionary<string, object> { { "businessOperationParameter", businessOperation } };
            return dic;
        }

    }
}
