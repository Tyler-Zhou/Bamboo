using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// EDI日志服务
    /// </summary>
    [ServiceContract]
    public interface IEDILogService
    {
        /// <summary>
        /// 获取EDI发送日志列表
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [FunctionInfomation("获取EDI发送日志列表")]
        [OperationContract]
        List<LogData> GetLogList(Guid[] mblIds);


        /// <summary>
        /// 获取EDI发送状态列表
        /// </summary>
        /// <param name="SeadbyID">发送人</param>
        /// <returns></returns>
        [FunctionInfomation("获取EDI发送状态列表")]
        [OperationContract]
        string GetLogStateList(Guid? SeadbyID, string query);

        /// <summary>
        /// 回写EDI状态
        /// </summary>
        /// <param name="OperationNo">业务号</param>
        /// <param name="BLNO">提单号</param>
        /// <param name="type">EDI类型</param>
        /// <param name="state">EDI状态</param>
        /// <param name="remark">备注</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件内容(byte类型)</param>
        /// <param name="number">邮件索引(收件箱位置索引)</param>
        /// <param name="saveBy">保存人</param>
        void SaveEDIStateLog(string OperationNo, string BLNO, int type, int state, string remark, string filename, byte[] file, int number, Guid saveBy);
    }
}
