using System;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.Sys.ServiceInterface
{
    /// <summary>
    /// 用户客户端服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    public interface IUserClientService
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        [FunctionInfomation]
        byte[] DownloadUserPhoto(Guid fileID);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="sex">性别</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="mobile">移动电话</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="address">地址</param>
        /// <param name="remark">remark</param>
        /// <param name="birthday">birthday</param>
        /// <param name="photo">photo</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        [FunctionInfomation]
        SingleResultData SaveUserDetailInfo(
            Guid id,
            string cname,
            string ename,
            GenderType sex,
            string tel,
            string fax,
            string mobile,
            string address,
            string remark,
            DateTime? birthday,
            byte[] photo,
            Guid saveById,
            DateTime? updateDate);
    }
}
