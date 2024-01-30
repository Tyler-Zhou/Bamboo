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
    /// 其他业务箱信息服务
    /// </summary>
    [ServiceInfomation("其他业务箱信息服务")]
    [ServiceContract]
    public interface IOBContainerService
    {
        /// <summary>
        /// 获取箱列表
        /// </summary>
        /// <param name="BookingID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [OperationContract]
        List<ContainerList> GetOtherContainerList(
            Guid BookingID,
            bool isEnglish);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ContainerInfo"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
        [OperationContract]
        ManyResult SaveOtherContanerList(ContainerSaveRequest ContainerInfo, bool IsEnglish);
        /// <summary>
        /// 删除箱信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="removeByID"></param>
        /// <param name="isEnglish"></param>
        /// <param name="updateDates"></param>
        [OperationContract]
        void RemoveOtherContainerList(Guid?[] ids, Guid removeByID,
                         bool isEnglish, DateTime?[] updateDates);
    }
}
