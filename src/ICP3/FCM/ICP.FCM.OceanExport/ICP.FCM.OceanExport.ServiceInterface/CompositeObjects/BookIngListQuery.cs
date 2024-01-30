namespace ICP.FCM.OceanExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 订舱列表查询参数类
    /// </summary>
    public class BookingListQueryCriteria
    {
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// POD-卸货港
        /// </summary>
        public string POD { get; set; }
        /// <summary>
        /// POL-装货港
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// 船公司
        /// </summary>
        public string Companys { get; set; }

    }
}
