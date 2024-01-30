namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanTruckInfoGetDescriptionCommand : IGetDescriptionCommand
    {
        public string Get(object billInfo)
        {
            //OceanTruckInfo truckInfo = billInfo as OceanTruckInfo;
            //return string.Format("POL:{0}{1}POD:{2}{3}ETD:{4},ETA:{5}", truckInfo.polr, Environment.NewLine,
            //    truckInfo.PODName, Environment.NewLine, truckInfo.e, truckInfo.ETA);
            return string.Empty;
        }
    }
}
