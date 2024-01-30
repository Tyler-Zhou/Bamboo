using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    #region OperationCommonInfo

    /// <summary>
    /// 业务公共信息
    /// </summary>
    [Serializable]
    public class OperationCommonInfo
    {
        /// <summary>
        ///  TradeCustomers Forms 
        /// </summary>
        public OperationCommonInfo()
        {
            this.TradeCustomers = new List<OperationCustomer>();
            this.Forms = new List<FormData>();
        }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 当前表单ID
        /// </summary>
        public Guid CurrentFormID { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 关帐前以业务时间（出口为ETD、进口为ETA、内贸为ETD、其他为创建时间）,关帐后以当天时间设置帐单建立时间
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// 帐单参考列表
        /// </summary>
        public List<FormData> Forms { get; set; }

        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid? FreightID { get; set; }

        /// <summary>
        /// 提单的AgentID,ConsigneeID,CarrierID,CustomerID, 在帐单中可以下拉选择
        /// </summary>
        public List<OperationCustomer> TradeCustomers { get; set; }

    }

    /// <summary>
    /// 业务公共信息
    /// </summary>
    [Serializable]
    public class OperationCustomer : ICP.Common.ServiceInterface.DataObjects.CustomerList
    {
        /// <summary>
        /// 是否业务中的代理
        /// </summary>
        public bool IsAgent { get; set; }
        /// <summary>
        /// 是否业务中的客户
        /// </summary>
        public bool IsCustomer { get; set; }
    }

    /// <summary>
    /// 表单数据
    /// </summary>
    [Serializable]
    public class FormData
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType Type { get; set; }


        /// <summary>
        /// 表单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 箱数量
        /// </summary>
        public Int32 CntQty { get; set; }

        /// <summary>
        /// 是否当前
        /// </summary>
        public bool IsCurrent { get; set; }
    }

    #endregion
}
