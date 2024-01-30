using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.UI.BillRevise
{
    /// <summary>
    /// 签收海进账修订页面
    /// </summary>
    [SmartPart]
    public partial class OEBillReviseBasePart : ICP.FCM.Common.UI.Document.BillReviseBasePart
    {
        public override List<Fee> GetCompareBillAndChargeInfo()
        {
            try
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(OldOperationID) == false)
                {
                    List<Fee> list = FCMCommonService.GetCompareBillAndChargeInfo(OldOperationID, OperationType.OceanImport);

                    return list;
                }
                return null;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
        }
       
    }
}
