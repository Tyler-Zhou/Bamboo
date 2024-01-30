using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanHBLInfoGetRefNoCommand : IGetRefNoCommand
    {
        public string Get(object billInfo)
        {
            OceanHBLInfo hblInfo = billInfo as OceanHBLInfo;
            return string.Format("SO:{0}{1}CTN:{2}{3}MBL:{4}{5}HBL:{6}", string.Empty, Environment.NewLine,
                hblInfo.ContainerNos, Environment.NewLine, hblInfo.MBLNo,Environment.NewLine,hblInfo.No);
        }

    }
}
