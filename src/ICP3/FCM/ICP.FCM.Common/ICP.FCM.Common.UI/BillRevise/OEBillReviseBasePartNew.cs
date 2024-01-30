using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.UI.BillRevise
{
    /// <summary>
    /// 签收海进账修订页面
    /// </summary>
    [SmartPart]
    public partial class OEBillReviseBasePartNew : ICP.FCM.Common.UI.Document.BillReviseBasePart
    {
        public override List<Fee> GetCompareBillAndChargeInfo()
        {
            try
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(OldOperationID) == false)
                {
                    string result = FCMCommonService.GetDispatchNewLogID(OldOperationID);
                    Guid DispatchFileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(result);
                    List<Fee> list = new List<Fee>();
                    if (opType == OperationType.OceanImport || opType == OperationType.OceanExport)
                    {
                         list = FCMCommonService.DispatchCompareBillAndCharge(NewOperationID, OldOperationID, DispatchFileLog, OperationType.OceanImport);
                    }
                    else
                    {
                        list = FCMCommonService.DispatchCompareBillAndCharge(NewOperationID, OldOperationID, DispatchFileLog, OperationType.AirImport);
                    }
                   
                    return list;
                }
                return null;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
        }
       
    }
}
