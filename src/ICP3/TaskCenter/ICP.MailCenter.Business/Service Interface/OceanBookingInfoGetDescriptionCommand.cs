using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface
{
    public class OceanBookingInfoGetDescriptionCommand : IGetDescriptionCommand
    {
        public string Get(object billInfo)
        {
            OceanBookingInfo bookingInfo = billInfo as OceanBookingInfo;
            return string.Format("POL:{0}{1}POD:{2}{3}ETD:{4},ETA:{5}",bookingInfo.POLName,Environment.NewLine,
                bookingInfo.PODName,Environment.NewLine,bookingInfo.ETD,bookingInfo.ETA);

        }


    }
}
