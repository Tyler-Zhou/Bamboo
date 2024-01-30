using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.Common.ServiceInterface
{

    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IOCRService
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="clientId">百度客户端ID</param>
        /// <param name="clientSecret">百度客户端密钥</param>
        /// <returns>AccessToken</returns>
        [FunctionInfomation]
        [OperationContract]
        BaiDuAIAccessToken GetAccessToken(string clientId, string clientSecret);
        /// <summary>
        /// 识别数字
        /// </summary>
        /// <param name="token">token 字符串</param>
        /// <param name="body">数字图片 Base64字符串</param>
        /// <returns>数字字符串</returns>
        [FunctionInfomation]
        [OperationContract]
        string GetNumerResult(string tokenString, string base64StringBody);
    }
}
