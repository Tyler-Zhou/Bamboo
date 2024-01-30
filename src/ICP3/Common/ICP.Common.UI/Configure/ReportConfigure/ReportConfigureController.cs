//-----------------------------------------------------------------------
// <copyright file="ReportConfigureController.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Configure.ReportConfigure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// Report配置-控制器
    /// <remarks>
    /// 控制器主要有两个功能(一个服务端代理)
    /// 1:封装场景的业务逻辑.
    /// 2:封装场景的业务所有验证规则.
    /// </remarks>
    /// </summary>
    public class ReportConfigureController : Controller
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
        /// 获取报表类型列表
        /// </summary>
        /// <returns></returns>
        public List<ReportType> GetReportTypes()
        {
            List<ReportType> list = this.ConfigureService.GetReportTypes();
            return list;
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
        /// 获取公司配置下的报表配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的报表配置列表</returns>
        public List<CompanyReportConfigureList> GetCompanyReportConfigureList(
            Guid configureID,
            bool? isValid)
        {
            List<CompanyReportConfigureList> list = this.ConfigureService.GetCompanyReportConfigureList(configureID, isValid);
            return list;
        }

        /// <summary>
        /// 保存报表公司配置信息
        /// </summary>
        /// <param name="reportID">报表配置ID</param>
        /// <param name="reportParameters">公司配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveReportCompanyConfigureInfo(
              Guid reportID,
              ConfigureListForReport[] parameter,
              Guid saveByID)
        {
            CompanyReportCollect items = new CompanyReportCollect
            {
                Companies = (from p in parameter
                             select new CompanyReportItem
                             {
                                 ID = p.ID,
                                 Reports = (from r in parameter
                                            where r.ID == p.ID
                                            select new ReportItem
                                            {
                                                ID = r.ReportConfigureID,
                                                Parameters = new ReportParameterCollect
                                                {
                                                    Parameters = (from pam in r.Parameters
                                                                  select new XMLReportParameterList
                                                                  {
                                                                      ID = pam.ID,
                                                                      Code=pam.Code,
                                                                      CDescription= pam.CDescription,
                                                                      EDescription=pam.EDescription,
                                                                      CreateByID = pam.CreateByID,
                                                                      CreateByName = pam.CreateByName,
                                                                      CreateDate = pam.CreateDate,
                                                                      ParameterType = pam.ParameterType,
                                                                      ParameterValue = pam.ParameterValue,
                                                                      UpdateDate = pam.UpdateDate
                                                                  }).ToList()
                                                }
                                            }).ToList()
                             }).ToList()
            };

            //ReportCollect item = new ReportItem
            //                {
            //                    ID = parameter.FirstOrDefault() == null ? Guid.Empty : parameter.FirstOrDefault().ReportID,
            //                    Parameters = new ReportParameterCollect
            //                    {
            //                        Parameters = (from i in parameter
            //                                      select new XMLReportParameterList
            //                                      {
            //                                          ID = i.ID,
            //                                          CName = i.CName,
            //                                          EName = i.EName,
            //                                          CreateByID = i.CreateByID,
            //                                          CreateByName = i.CreateByName,
            //                                          CreateDate = i.CreateDate,
            //                                          Description = i.Description,
            //                                          ParameterType = i.ParameterType,
            //                                          ParameterValue = i.ParameterValue,
            //                                          UpdateDate = i.UpdateDate
            //                                      }).ToList()
            //                    }
            //                };

            string xmlcontent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<CompanyReportCollect>(items, true, true);

            ManyResultData data = this.ConfigureService.SaveReportCompanyConfigureInfo(
              reportID,
              xmlcontent,
              saveByID);
            return data;
        }

        /// <summary>
        /// 保存报表配置信息
        /// </summary>
        /// <param name="id">ID列表</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名称</param>
        /// <param name="eName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="fileName">报表文件</param>
        /// <param name="parameter">参数</param>
        /// <param name="type">报表类型</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveReportConfigureInfo(
             Guid? id,
             string code,
             string cDescription,
             string eDescription,      
             ReportParameterList[] parameter,
             int? reportTypeID,
             Guid saveByID,
             DateTime? updateDate)
        {         
            ReportItem item = new ReportItem
                           {
                               ID = parameter.FirstOrDefault() == null ? Guid.Empty : parameter.FirstOrDefault().ReportID,
                               Parameters = new ReportParameterCollect
                               {
                                   Parameters = (from i in parameter
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
                           };

            string xmlcontent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<ReportItem>(item, true, true);

            ManyResultData data = this.ConfigureService.SaveReportConfigureInfo(
             id,
             code,
             cDescription,
             eDescription,
             xmlcontent,
             reportTypeID,
             saveByID,
             updateDate);

            return data;
        }

        /// <summary>
        /// 获取公司配置列表
        /// </summary>
        /// <param name="isVaid">是否有效</param>
        /// <returns>返回公司配置列表</returns>
        public List<ConfigureList> GetConfigureList(bool? isVaid)
        {
            List<ConfigureList> list = this.ConfigureService.GetConfigureListByVaid(isVaid);
            return list;
        }

        /// <summary>
        /// 获取报表所挂公司列表
        /// </summary>
        /// <param name="reportID">报表ID</param>
        /// <returns>返回报表所挂公司列表</returns>
        public List<ReportCompanyConfigureList> GetReportCompanyConfigureList(Guid reportID)
        {
            List<ReportCompanyConfigureList> list = this.ConfigureService.GetReportCompanyConfigureList(reportID);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="solutionID">解决方案</param>
        /// <returns>返回公司配置列表</returns>
        public List<ConfigureList> GetCompanyForReportParameterIsUsed(Guid reportID, Guid reportParameterID)
        {
            List<ConfigureList> list = this.ConfigureService.GetCompanyForReportParameterIsUsed(reportID, reportParameterID);
            return list;
        }

        #endregion
    }
}
