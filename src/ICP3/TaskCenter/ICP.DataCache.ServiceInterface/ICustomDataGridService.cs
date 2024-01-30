using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Serialization;
using System.Data;


namespace ICP.DataCache.ServiceInterface1
{   
    /// <summary>
    /// 用户列表自定义服务接口
    /// <remarks>用户自定义列表的显示。可以通过全局变量LocalData.EnableCustomDataGrid禁用此功能</remarks>
    /// </summary>
    [ServiceContract]
   public interface ICustomDataGridService
    {    
        /// <summary>
        /// 保存用户自定义列表显示信息
        /// </summary>
        /// <param name="customInfo"></param>
        /// <returns></returns>
        [OperationContract]
         SingleResult Save(UserCustomGridInfo customInfo);

        [OperationContract]
        DataTable GetUserColumns(Guid? userId, string templateCode);
       
    }
}
