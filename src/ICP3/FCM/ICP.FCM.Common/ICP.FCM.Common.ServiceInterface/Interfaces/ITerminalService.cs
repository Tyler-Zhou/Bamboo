using ICP.Crawler.CommonLibrary.Enum;
using ICP.Crawler.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 码头服务接口
    /// </summary>
    [ServiceInfomation("码头接口")]
    [ServiceContract]
    public interface ITerminalService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchUsing"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Terminals> GetTerminalList(SearchUsing searchUsing);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContainerID"></param>
        /// <param name="VesselName"></param>
        /// <param name="LocationCode"></param>
        /// <param name="ICPTerminalID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Terminals> GeTerminalByContainerList(Guid ContainerID, string VesselName, String LocationCode, Guid? ICPTerminalID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="searchUsing"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Terminals GetTerminalInfo(Guid id, SearchUsing searchUsing);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VesselName"></param>
        /// <param name="LocationCode"></param>
        /// <param name="TerminalID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool SaveVesselTerminals(String VesselName, String LocationCode, Guid TerminalID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VesselName"></param>
        /// <param name="LocationCode"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        VesselTerminals GetVesselTerminalsInfo(String VesselName, String LocationCode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool SaveTerminalVesselRipperLogs(TerminalRipperLogs logs);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userID"></param>
        /// <param name="maxRecords"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<TerminalLogins> GetTerminalLoginsList(String code, String userID, int maxRecords, bool isEnglish);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        TerminalLogins GetTerminalLogins(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool SaveTerminalLogins(Guid id, String code, String userID, String password, Guid updateBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="containerID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        TerminalContainer GetTerminalContainersInfo(Guid terminalID, Guid containerID);
    }
}
