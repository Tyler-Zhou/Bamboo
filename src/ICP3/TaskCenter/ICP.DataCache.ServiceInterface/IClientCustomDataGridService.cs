using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System.Xml.Serialization;


namespace ICP.DataCache.ServiceInterface1
{    
    /// <summary>
    /// 客户端用户列表显示自定义服务接口
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
    [XmlSerializerFormat]
    [XmlSerializerAssembly(AssemblyName = "ICP.DataCache.ServiceInterface.XmlSerializers.dll")]
   public interface IClientCustomDataGridService
    {  
        /// <summary>
        /// 保存用户列表自定义显示信息
        /// </summary>
        /// <param name="customInfo"></param>
        [OperationContract]
        [XmlSerializerFormat]
        void Save(UserCustomGridInfo customInfo);
        /// <summary>
        /// 获取用户列表自定义显示信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="listType">列表类型</param>
        /// <returns></returns>
        [OperationContract(Name="GetByUserId")]
        [XmlSerializerFormat]
        UserCustomGridInfo Get(Guid userId, ListFormType listType);
        /// <summary>
        /// 获取用户列表自定义显示信息
        /// </summary>
        /// <param name="listType">列表类型</param>
        /// <returns></returns>
        [OperationContract]
        [XmlSerializerFormat]
        UserCustomGridInfo Get(ListFormType listType);

        #region joe 2013-05-20 添加
        /// <summary>
        /// 获取用户列表自定义显示信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="listType">列表类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetByUserIdAndTemplateCode")]
        [XmlSerializerFormat]
        UserCustomGridInfo Get(Guid userId, string templateCode);
        /// <summary>
        /// 获取用户列表自定义显示信息
        /// </summary>
        /// <param name="listType">列表类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetByTemplateCode")]
        [XmlSerializerFormat]
        UserCustomGridInfo Get(string templateCode);
        #endregion

    }
}
