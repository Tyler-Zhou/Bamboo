using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// ContainerInfoReportData详细对象
    /// </summary>
    [Serializable]
    public class ArrivalNotice2ReportData
    {
        public ArrivalNoticeData ArrivalNoticeData { get; set; }

        public List<ContainerInfoReportData> ArrivalNoticeContainers { get; set; }
    }
}
