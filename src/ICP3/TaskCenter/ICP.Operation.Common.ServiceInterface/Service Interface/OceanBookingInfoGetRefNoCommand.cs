using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class OceanBookingInfoGetRefNoCommand : IGetRefNoCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billInfo"></param>
        /// <returns></returns>
        public string Get(object billInfo)
        {
            OceanBookingInfo bookingInfo = billInfo as OceanBookingInfo;
            return string.Format("SO:{0}{1}CTN:{2}{3}MBL:{4}{5}HBL:{6}", bookingInfo.No, Environment.NewLine,
                bookingInfo.ContainerNo, Environment.NewLine, bookingInfo.MBLNo,Environment.NewLine,bookingInfo.HBLNo);
        }

  
    }
}
