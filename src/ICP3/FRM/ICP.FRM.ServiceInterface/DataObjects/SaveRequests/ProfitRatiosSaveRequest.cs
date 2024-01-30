using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FRM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 事务保存对象(利润配比调整)
    /// </summary>
    [Serializable]
    public class ProfitRatiosAdjustmentSaveRequest : SaveRequest
    {
        /// <summary>
        /// 调整类型
        /// </summary>
        public AdjustmnetType AdjustmnetType { get; set; }
        /// <summary>
        /// 合约基本港费用ID
        /// </summary>
        public Guid[] ContractBaseItemIDs { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid[] OperationIDs { get; set; }

        /// <summary>
        /// 箱型ID
        /// </summary>
        public Guid[] ContainerTypeIDs { get; set; }

        /// <summary>
        /// 调整金额
        /// </summary>
        public decimal[] Amounts { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID;
        /// <summary>
        /// 更新时间 
        /// </summary>
        public DateTime UpdateDate;
    }
}
