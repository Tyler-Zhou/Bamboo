using ICP.Crawler.CommonLibrary;
using ICP.Crawler.CommonLibrary.Enum;
using ICP.Crawler.ServiceInterface;
using ICP.Crawler.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TerminalService : ICP.FCM.Common.ServiceInterface.ITerminalService
    {
        /// <summary>
        /// 
        /// </summary>
        static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchUsing"></param>
        /// <returns></returns>
        public List<Terminals> GetTerminalList(SearchUsing searchUsing)
        {
            return terminalService.GetTerminalList(searchUsing);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContainerID"></param>
        /// <param name="VesselName"></param>
        /// <param name="LocationCode"></param>
        /// <param name="ICPTerminalID"></param>
        /// <returns></returns>
        public List<Terminals> GeTerminalByContainerList(Guid ContainerID, string VesselName, String LocationCode, Guid? ICPTerminalID)
        {
            return terminalService.GeTerminalByContainerList(ContainerID, VesselName, LocationCode, ICPTerminalID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="searchUsing"></param>
        /// <returns></returns>
        public Terminals GetTerminalInfo(Guid id, SearchUsing searchUsing)
        {
            return terminalService.GetTerminalInfo(id, searchUsing);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VesselName"></param>
        /// <param name="LocationCode"></param>
        /// <param name="TerminalID"></param>
        /// <returns></returns>
        public bool SaveVesselTerminals(String VesselName, String LocationCode, Guid TerminalID)
        {
            return terminalService.SaveVesselTerminals(VesselName, LocationCode, TerminalID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VesselName"></param>
        /// <param name="LocationCode"></param>
        /// <returns></returns>
        public VesselTerminals GetVesselTerminalsInfo(String VesselName, String LocationCode)
        {
            return terminalService.GetVesselTerminalsInfo(VesselName, LocationCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public bool SaveTerminalVesselRipperLogs(TerminalRipperLogs logs)
        {
            return terminalService.SaveTerminalVesselRipperLogs(logs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userID"></param>
        /// <param name="maxRecords"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<TerminalLogins> GetTerminalLoginsList(String code, String userID, int maxRecords, bool isEnglish)
        {
            return terminalService.GetTerminalLoginsList(code, userID, maxRecords, isEnglish);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TerminalLogins GetTerminalLogins(Guid id)
        {
            return terminalService.GetTerminalLogins(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public bool SaveTerminalLogins(Guid id, String code, String userID, String password, Guid updateBy)
        {
            return terminalService.SaveTerminalLogins(id, code, userID, password, updateBy);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="containerID"></param>
        /// <returns></returns>
        public TerminalContainer GetTerminalContainersInfo(Guid terminalID, Guid containerID)
        {
            return terminalService.GetTerminalContainersInfo(terminalID, containerID);
        }
    }
}
