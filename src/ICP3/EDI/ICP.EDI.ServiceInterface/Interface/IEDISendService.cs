using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.Data;
using System.ServiceModel;

namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// EDI发送服务
    /// </summary>
    [ServiceContract]
    public interface IEDISendService
    {
        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendEDIItem">EDI发送参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        [Obsolete("方法已过时，请使用 Send(EDISendOption sendOption, EDIConfigItem configOption) 代替")]
        void SendEDI(EDISendOption sendEDIItem);

        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <param name="configOption">配置选项</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void Send(EDISendOption sendOption, EDIConfigItem configOption);

        /// <summary>
        /// 中海订舱
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <returns>多单订舱，返回后在客户端组合成一个txt并保存</returns>
        [FunctionInfomation]
        [OperationContract]
        DataSet SendCSCLBookingEDI(EDISendOption sendEDIItem);

        [FunctionInfomation]
        [OperationContract]
        DataSet SendCSCLSIEDI(EDISendOption sendEDIItem);


        /// <summary>
        /// 获取业务号
        /// </summary>
        /// <param name="opOrMblNo">MBLNO</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string GetOPNOByMBONO(string opOrMblNo);
        [FunctionInfomation]
        [OperationContract]
        Guid GetOceanBookingIDByMBLID(Guid MBLID);
        [FunctionInfomation]
        [OperationContract]
        Guid GetOceanBookingIDByHBLID(Guid HBLID);
    }
}
