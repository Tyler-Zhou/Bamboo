using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// IOceanPriceFileClientService
    /// </summary>
    public interface IOceanPriceFileClientService
    {
        /// <summary>
        /// 获取合约下文件列表 
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="userID">当前用户ID</param>
        /// <returns>返回合约下文件列表 </returns>
       [FunctionInfomation]  [OperationContract]
        List<OceanFileList> GetOceanFileList(
            Guid oceanID,
            Guid userID);


        /// <summary>
        /// 保存合约下文件信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="id">ID</param>
        /// <param name="fileID">文件ID</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileContent">文件内容</param>
        /// <param name="remarks">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回SingleResult</returns>
       [FunctionInfomation]  [OperationContract]
        ManyResult SaveOceanFileInfo(
            Guid oceanID,
            Guid?[] ids,
            string[] names,
            string[] descriptions,
            byte[][] fileContent,
            Guid saveByID
            , DateTime?[] updateDates);

        /// <summary>
        /// 删除合约下文件信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
       [FunctionInfomation]  [OperationContract]
        void RemoveOceanFileInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 下载文件
        /// </summary>
       [FunctionInfomation]  [OperationContract]
        byte[] DownloadOceanFileList(Guid fileID);

    }
}
