using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 联系人列表数据服务
    /// </summary>
    [ServiceInfomation("联系人接口")]
    [ServiceContract]
    public interface IContactService
    {
        /// <summary>
        /// 根据业务ID和业务类型获取联系列表数据
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ContactObjects GetContactList(Guid OperationID, OperationType operationType);

        /// <summary>
        /// 根据获取联系人列表
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ReleaseAndArContact> GetReleaseAndArContactList(Guid OperationID);

        /// <summary>
        /// 根据沟通阶段，获取业务联系人
        /// </summary>
        /// <param name="operationId">业务Id</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="contactStage">联系人所属业务阶段,取值为UnKnown时，返回所有沟通阶段的联系人</param>
        /// <param name="contactType">联系人类型</param>
        /// <param name="CustomerID">业务联系人所属客户ID</param>
        /// <returns></returns>
        [OperationContract]
        List<CustomerCarrierObjects> GetContactListByContactStage(Guid operationId, OperationType operationType, ContactType contactType, ContactStage contactStage, Guid? CustomerID);
        /// <summary>
        /// 获取指定客户最近一票的联系人信息
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="companyID">操作口岸ID</param>
        /// <param name="customerId"></param>
        /// <param name="contactType"></param>
        /// <param name="contactStage"></param>
        /// <returns></returns>
        [OperationContract]
        List<CustomerCarrierObjects> GetLatestContactList(OperationType operationType, Guid companyID, Guid customerId, ContactType contactType, ContactStage contactStage);

        /// <summary>
        /// 获取指定客户最近一票的联系人信息
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="companyID">操作口岸ID</param>
        /// <param name="customerId"></param>
        /// <param name="contactType"></param>
        /// <param name="contactStage"></param>
        /// <returns></returns>
        [OperationContract]
        List<CustomerCarrierObjects> GetLatesterContactList(OperationType operationType, Guid? companyID, Guid? customerId, ContactType contactType, ContactStage contactStage);

        /// <summary>
        /// 保存联系人列表数据
        /// </summary>
        /// <param name="Ids">Id</param>
        /// <param name="OceanBookingIDs"></param>
        /// <param name="operationTypes"></param>
        /// <param name="Types"></param>
        /// <param name="SOs"></param>
        /// <param name="Trks"></param>
        /// <param name="CFs"></param>
        /// <param name="CIs"></param>
        /// <param name="Whss"></param>
        /// <param name="QIs"></param>
        /// <param name="SIs"></param>
        /// <param name="BLs"></param>
        /// <param name="ANs">是否提货通知书阶段</param>
        /// <param name="FUs"></param>
        /// <param name="INs"></param>
        /// <param name="CCs"></param>
        /// <param name="Names"></param>
        /// <param name="Emails"></param>
        /// <param name="Tels"></param>
        /// <param name="Faxs"></param>
        /// <param name="ARs"></param>
        /// <param name="Releases"></param>
        /// <param name="UpdateDates"></param>
        /// <param name="CreateByIDs"></param>
        /// <param name="CreateDates"></param>
        ///<param name="CustomerIDs">所属客户ID</param>
        /// <param name="BillIDs"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveContactLists")]
        ManyResult SaveContactList(Guid?[] Ids,
                   Guid[] OceanBookingIDs,
                   OperationType[] operationTypes,
                   ContactType[] Types,
                   bool[] SOs,
                   bool[] Trks,
                   bool[] CFs,
                   bool[] CIs,
                   bool[] Whss,
                   bool[] QIs,
                   bool[] SIs,
                   bool[] BLs,
                   bool[] ANs,
                   bool[] FUs,
                   bool[] INs,
                   bool[] CCs,
                   string[] Names,
                   string[] Emails,
                   string[] Tels,
                   string[] Faxs,
                   bool[] ARs,
                   bool[] Releases,
                   DateTime?[] UpdateDates,
                   Guid?[] CreateByIDs,
                   DateTime?[] CreateDates,
                   Guid?[] CustomerIDs,
                   Guid?[] BillIDs);

        /// <summary>
        /// 保存多个联系人
        /// </summary>
        /// <param name="customerList"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveContactList")]
        ManyResult SaveContactList(List<CustomerCarrierObjects> customerList);

        /// <summary>
        /// 获取业务联系人
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerCarrierObjects> GetOceanContactsByEmails(List<string> emails);

        /// <summary>
        /// 移除联系人列表数据
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="removeByID"></param>
        /// <param name="updateDate"></param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveContactInfo(
            Guid Id,  //
            Guid removeByID,
            DateTime? updateDate);

    }
}
