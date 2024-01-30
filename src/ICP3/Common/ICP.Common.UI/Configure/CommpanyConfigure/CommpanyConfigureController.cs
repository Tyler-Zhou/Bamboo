//-----------------------------------------------------------------------
// <copyright file="CustomerManagerEditPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Common.ServiceInterface.CompositeObjects;

    /// <summary>
    /// 公司配置-控制器
    /// <remarks>
    /// 控制器主要有两个功能(一个服务端代理)
    /// 1:封装场景的业务逻辑.
    /// 2:封装场景的业务所有验证规则.
    /// </remarks>
    /// </summary>
    public class CompanyConfigureController : Controller
    {
        #region 注入服务

        /// <summary>
        /// 公司配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 与服务交互的逻辑

        /// <summary>
        /// 保存公司配置信息
        /// </summary>
        /// <param name="configureSaveRequest">保存对象</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveConfigureInfo(ConfigureSaveRequest configureSaveRequest)
        {
            SingleResultData data = this.ConfigureService.SaveConfigureInfo(configureSaveRequest);
            return data;
        }

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns>返回公司配置信息</returns>
        public ConfigureInfo GetCompanyConfigureInfo(Guid companyID)
        {
            ConfigureInfo info = this.ConfigureService.GetCompanyConfigureInfo(companyID);
            return info;
        }

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回公司配置信息</returns>
        public ConfigureInfo GetConfigureInfo(Guid id)
        {
            ConfigureInfo info = this.ConfigureService.GetConfigureInfo(id);
            return info;
        }

        /// <summary>
        /// 更改公司配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        SingleResultData ChangeConfigureState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            SingleResultData data = this.ConfigureService.ChangeConfigureState(
            id,
            isValid,
            changeByID,
            updateDate);

            return data;
        }

        /// <summary>
        /// 获取系统配置的关键字列表
        /// </summary>
        /// <returns>返回系统配置的关键字列表</returns>
        public List<ConfigureKeyList> GetConfigureKeyList()
        {
            List<ConfigureKeyList> list = this.ConfigureService.GetConfigureKeyList();
            return list;
        }

        /// <summary>
        /// 获取公司配置下的EDI配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的EDI配置列表</returns>
        public List<CompanyEDIConfigureList> GetCompanyEDIConfigureList(
            Guid configureID,
            bool? isValid)
        {
            List<CompanyEDIConfigureList> list = this.ConfigureService.GetCompanyEDIConfigureList(configureID, isValid);
            return list;
        }

        /// <summary>
        /// 获取EID配置列表
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回EID配置列表</returns>
        public List<EDIConfigureList> GetEDIConfigureList(bool? isValid)
        {
            List<EDIConfigureList> list = this.ConfigureService.GetEDIConfigureList(null, null, isValid, 0);
            return list;
        }

        /// <summary>
        /// 获取公司配置下的报表配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的报表配置列表</returns>
        public List<CompanyReportConfigureList> GetCompanyReportConfigureList(
            Guid configureID,
            bool? isValid)
        {
            List<CompanyReportConfigureList> list = this.ConfigureService.GetCompanyReportConfigureList(
            configureID,
            isValid);

            return list;
        }

        public CompanyReportConfigureList GetReportConfigure(Guid companyID, string reportCode)
        {
            CompanyReportConfigureList data = this.ConfigureService.GetReportConfigureList(companyID, reportCode);
            return data;
        }

        /// <summary>
        /// 获取报表配置列表
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回报表配置列表</returns>
        public List<ReportConfigureList> GetReportConfigureList(bool? isValid)
        {
            List<ReportConfigureList> list = this.ConfigureService.GetReportConfigureList(string.Empty, string.Empty, string.Empty, null, isValid, 0);

            return list;
        }

        /// <summary>
        /// 保存公司EDI配置信息
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="ediConfigureIDs">EDI配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveCompanyEDIConfigureInfo(
            Guid configureID,
            Guid[] ediConfigureIDs,
            Guid saveByID)
        {
            ManyResultData data = this.ConfigureService.SaveCompanyEDIConfigureInfo(
           configureID,
           ediConfigureIDs,
           saveByID);

            return data;
        }

        public List<ConfigureListForEDI> GetEDICompanyConfigureList(Guid ediConfigID)
        {
            List<ConfigureListForEDI> list = this.ConfigureService.GetEDICompanyConfigureListByConfigure(ediConfigID);
            return list;
        }

        /// <summary>
        /// 保存EDI配置信息
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <param name="configureKeyIDs">配置KEY</param>
        /// <param name="carrierIDs">船公司ID</param>
        /// <param name="modes">上传模式</param>
        /// <param name="serverAddresses">服务器地址</param>
        /// <param name="userNames">帐号</param>
        /// <param name="passwords">密码</param>
        /// <param name="receiveAddresses">反馈地址</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回</returns>
        public ManyResultData SaveEDIConfigureInfo(
             Guid?[] ids,
            string[] Codes,
            string[] Components,
            string[] FTPs,
            string[] FileFormats,
            string[] DataFormats,
            string[] RegularFiles,
            string[] StoredProcedures,
            Guid[] configureKeyIDs,
            Guid?[] carrierIDs,
            EDIUploadMode[] modes,
            EDIMode[] itemType,
            string[] serverAddresses,
            string[] userNames,
            string[] passwords,
            string[] receiveAddresses,
            Guid saveByID,
            byte ReceiverType,
            DateTime?[] updateDates)
        {
            ManyResultData data = this.ConfigureService.SaveEDIConfigureInfo(
            ids,
                //Codes,
            Components,
            FTPs,
            FileFormats,
            DataFormats,
            RegularFiles,
            StoredProcedures,
            configureKeyIDs,
            carrierIDs,
            modes,
            itemType,
            serverAddresses,
            userNames,
            passwords,
            receiveAddresses,
            saveByID,
            ReceiverType,
            updateDates);
            return data;
        }

        /// <summary>
        /// 改变EDI配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeEDIConfigureState(
              Guid id,
              bool isValid,
              Guid changeByID,
              DateTime? updateDate)
        {
            SingleResultData data = this.ConfigureService.ChangeEDIConfigureState(
              id,
              isValid,
              changeByID,
              updateDate);
            return data;
        }

        /// <summary>
        /// 删除公司EDI配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveCompanyEDIConfigure(
             Guid id,
             Guid removeByID,
             DateTime? updateDate)
        {
            this.ConfigureService.RemoveCompanyEDIConfigure(id,
             removeByID,
             updateDate);
        }

        /// <summary>
        /// 保存公司报表配置信息
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="reportParameters">报表参数</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveCompanyReportConfigureInfo(
            Guid configureID,
            List<ReportParameterList> reportParameters,
            Guid saveByID)
        {
            ReportCollect collect = new ReportCollect
            {
                Reports = (from r in reportParameters
                           group r by r.ReportID into rp
                           select new ReportItem
                           {
                               ID = rp.Key,
                               Parameters = new ReportParameterCollect
                                   {
                                       Parameters = (from i in reportParameters
                                                     where i.ReportID == rp.Key
                                                     select new XMLReportParameterList
                                                     {
                                                         ID = i.ID,
                                                         Code = i.Code,
                                                         CDescription = i.CDescription,
                                                         EDescription = i.EDescription,
                                                         CreateByID = i.CreateByID,
                                                         CreateByName = i.CreateByName,
                                                         CreateDate = i.CreateDate,
                                                         ParameterType = i.ParameterType,
                                                         ParameterValue = i.ParameterValue,
                                                         UpdateDate = i.UpdateDate
                                                     }).ToList()
                                   }
                           }).ToList()
            };

            string xmlcontent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<ReportCollect>(collect, true, true);

            ManyResultData data = this.ConfigureService.SaveCompanyReportConfigureInfo(
            configureID,
            xmlcontent,
            saveByID);

            return data;
        }

        #endregion
    }
}
