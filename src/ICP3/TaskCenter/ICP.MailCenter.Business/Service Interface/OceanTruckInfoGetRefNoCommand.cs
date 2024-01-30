using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanTruckInfoGetRefNoCommand : IGetRefNoCommand
    {
        public string Get(object billInfo)
        {
            OceanTruckInfo truckInfo = billInfo as OceanTruckInfo;
            return string.Format("SO:{0}{1}CTN:{2}{3}MBL:{4}{5}HBL:{6}", truckInfo.ShippingOrderNo, Environment.NewLine,
                truckInfo.CarrierName, Environment.NewLine, truckInfo.ShippingOrderNo,Environment.NewLine, truckInfo.TruckerName);
        }
    }
}
