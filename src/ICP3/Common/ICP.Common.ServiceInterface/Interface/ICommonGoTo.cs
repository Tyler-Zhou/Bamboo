using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.Common.ServiceInterface
{
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ICommonGoTo
    {

        /// <summary>
        /// 返回列表查询数据
        /// </summary>
        /// <param name="type">业务类型(海出:OE 海进:OI 空出:AE 空进:AI)</param>
        /// <param name="no">查询条件</param>
        /// <param name="companyId">当前人员组织架构ID</param>
        /// <param name="fromType">当前业务的类别</param>
        /// <param name="opd">是否查询最近业务</param>
        [FunctionInfomation]
        [OperationContract]
        List<GoToObject> GetGoToList(string type, string no, string companyId,  int opd);

        /// <summary>
        /// 查询当前用户的配置文件
        /// </summary>
        /// <param name="userId">当前用户的ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        GotoSetting GotoSettingsList(Guid userId);


        /// <summary>
        /// 添加用户设置的信息
        /// </summary>
        /// <param name="gotoSetting">实体对象</param>
        /// <param name="type">操作类型</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int SettingSave(GotoSetting gotoSetting, string type);
    }
}
