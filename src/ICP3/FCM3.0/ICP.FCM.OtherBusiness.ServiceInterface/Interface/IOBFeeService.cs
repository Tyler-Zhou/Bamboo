using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
    using System.ServiceModel;
    /// <summary>
    /// 其他业务服务
    /// </summary>
    [ServiceInfomation("其他业务费用服务")]
    [ServiceContract]
    public interface IOBFeeService
    {
        /// <summary>
        /// 获取其他业务订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        [OperationContract]
        List<OBFeeList> GetOBOrderFeeList(Guid orderID, bool IsEnglish);
        


        /// <returns>返回ManyResult</returns>
        [OperationContract]
        ManyResult SaveOBOrderFeeList(FeeSaveRequest feeInfo, bool IsEnglish);
        
        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [OperationContract]
        void RemoveOBOrderFeeList(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates,
             bool IsEnglish);
       
    }
}
