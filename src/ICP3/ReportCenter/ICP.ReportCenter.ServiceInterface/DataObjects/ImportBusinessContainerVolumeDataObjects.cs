using System;
using System.Collections.Generic;

namespace ICP.ReportCenter.ServiceInterface.DataObjects
{
    #region  进口箱列表-数据展示
    /// <summary>
    /// 进口箱列表
    /// </summary>
    [Serializable]
    public class OIContaierList
    {
        public string No { get; set; }
        public Guid? FreightLocationID { get; set; }
        public string FreightLocationName { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeName { get; set; }
        public string TransportName { get; set; }
        public string VeVoNo { get; set; }
    }

    /// <summary>
    /// 进口箱箱型数量
    /// </summary>
    [Serializable]
    public class OIContainerTypeCount
    {
        public string ContainerTypeName { get; set; }
        public string FreightLocationName { get; set; }
        public string VeVoNo { get; set; }
        public int Num { get; set; }
    }

    /// <summary>
    /// 进口箱箱型数量
    /// </summary>
    [Serializable]
    public class OIContainerReportData
    {
        public List<OIContaierList> OIContaierList { get; set; }
        public List<OIContainerTypeCount> OIContainerTypeCount { get; set; }
    }
    #endregion

    #region 进口箱列表-数据查询
    /// <summary>
    /// 查询参数进口箱列表
    /// </summary>
    [Serializable]
    public class QueryCriteria4OIContainerVolumeTotal
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 操作口岸口岸
        /// </summary>
        public string CompanyIDs { get; set; }
        /// <summary>
        /// 提柜点
        /// </summary>
        public string FreightLocationIDs { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string VesselIDs { get; set; }
        /// <summary>
        /// 航次
        /// </summary>
        public string VoyageIDs { get; set; }
        /// <summary>
        /// 箱型
        /// </summary>
        public string ContainerTypeIDs { get; set; }

    }
    #endregion
}
