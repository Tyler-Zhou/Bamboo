using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanHBLInfoGetDescriptionCommand : IGetDescriptionCommand
    {
        public string Get(object billInfo)
        {
            OceanHBLInfo hblInfo = billInfo as OceanHBLInfo;
            return string.Format("POL:{0}{1}POD:{2}{3}ETD:{4},ETA:{5}", hblInfo.POLName, Environment.NewLine,
                hblInfo.PODName, Environment.NewLine, hblInfo.ETD, hblInfo.ETA);

        }
    }
}
