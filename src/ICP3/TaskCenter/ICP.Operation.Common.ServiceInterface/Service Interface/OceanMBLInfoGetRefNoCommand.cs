using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanMBLInfoGetRefNoCommand : IGetRefNoCommand
    {
        public string Get(object billInfo)
        {
            OceanMBLInfo mblinfo = billInfo as OceanMBLInfo;
            return string.Format("SO:{0}{1}CTN:{2}{3}MBL:{4}{5}HBL:{6}",string.Empty, Environment.NewLine,
                mblinfo.ContainerNos, Environment.NewLine, mblinfo.No,Environment.NewLine, mblinfo.HBLNos);
        }
    }
}
