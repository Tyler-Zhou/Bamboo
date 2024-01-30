using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface.Service_Interface
{
    class OceanMBLInfoGetDescriptionCommand : IGetDescriptionCommand
    {
        public string Get(object billInfo)
        {
            OceanMBLInfo mblInfo = billInfo as OceanMBLInfo;
            return string.Format("POL:{0}{1}POD:{2}{3}ETD:{4},ETA:{5}", mblInfo.POLName, Environment.NewLine,
                mblInfo.PODName, Environment.NewLine, mblInfo.ETD, mblInfo.ETA);
        }
    }
}
