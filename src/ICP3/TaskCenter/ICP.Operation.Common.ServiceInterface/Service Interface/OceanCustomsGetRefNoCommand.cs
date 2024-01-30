namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanCustomsGetRefNoCommand : IGetRefNoCommand
    {
        public string Get(object billInfo)
        {
            //OceanBookingInfo bookingInfo = billInfo as OceanBookingInfo;
            //return string.Format("SO:{0}{1}CTN:{2}{3}MBL:{4}{5}HBL:{6}", bookingInfo.No, Environment.NewLine,
            //    bookingInfo.ContainerNo, Environment.NewLine, bookingInfo.MBLNo, bookingInfo.HBLNo);
            return string.Empty;
        }
    }
}
