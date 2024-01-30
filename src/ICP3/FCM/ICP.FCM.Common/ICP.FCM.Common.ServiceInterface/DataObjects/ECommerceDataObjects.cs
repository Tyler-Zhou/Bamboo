#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/18 星期三 18:28:13
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 电商列表
    /// </summary>
    [Serializable]
    public partial class ECommerceList : BaseDataObject
    {
        /// <summary>
        /// 是否选择
        /// </summary>
        bool _isselect;
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsSelect
        {
            get
            {
                return _isselect;
            }
            set
            {
                if (_isselect != value)
                {
                    _isselect = value;
                    OnPropertyChanged("IsSelect", value);
                }
            }

        }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 业务员名称
        /// </summary>
        public string SalesName { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 业务时间
        /// </summary>
        public DateTime OperationDate { get; set; }
        /// <summary>
        /// 起始地
        /// </summary>
        public string POLName { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string PODName { get; set; }
        /// <summary>
        /// 入仓号
        /// </summary>
        public string WarehouseNo { get; set; }
        /// <summary>
        /// 转单号
        /// </summary>
        public string TransferNo { get; set; }
        /// <summary>
        /// 包装件数
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public decimal ProfitAmount { get; set; }
    }
    /// <summary>
    /// 电商查询参数
    /// </summary>
    [Serializable]
    public partial class QueryCriteria4ECommerce
    {
        /// <summary>
        /// 口岸ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 最大记录行数
        /// </summary>
        public int MaxRecords { get; set; }

    }
}
