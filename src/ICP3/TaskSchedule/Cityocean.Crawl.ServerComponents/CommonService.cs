#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/5 9:52:03
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Data.SqlClient;
using System.Xml;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 公用服务
    /// </summary>
    public sealed class CommonService : BaseService, ICommonService
    {
        /// <summary>
        /// 获取船东配置
        /// </summary>
        /// <returns></returns>
        public List<CrawlConfig> GetWebsiteConfigs(CrawlType? paramCrawlType = null)
        {
            List<CrawlConfig> results = new List<CrawlConfig>();
            try
            {

                Database db = GetDefaultDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetWebsiteConfigs]");
                db.AddInParameter(dbCommand, "@CrawlType", DbType.Int16, paramCrawlType);
                DataSet ds = db.TryExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    throw new Exception("未找到船东配置");
                }

                results = (from b in ds.Tables[0].AsEnumerable()
                           select new CrawlConfig
                           {
                               ID = b.Field<Guid>("ID"),
                               WebsiteCode = b.Field<string>("URLAddress").Trim(),
                               ReturnType = (PageReturnType)b.Field<byte>("ReturnType"),
                               CrawlType = (CrawlType)b.Field<byte>("CrawlType"),
                               SortType = (DynamicSortType)b.Field<byte>("SortType"),
                               IsHistory = b.Field<bool>("IsHistory"),
                               IsNeedLogin = b.Field<bool>("IsNeedLogin"),
                               Timeout = b.Field<int>("Timeout"),
                               Browsers = (Browsers)b.Field<byte>("Browsers"),
                               EmptyPickUp = b.Field<string>("EmptyPickUp"),
                               FullPickUp = b.Field<string>("FullPickUp"),
                               LOBD = b.Field<string>("LOBD"),
                               UNLOBD = b.Field<string>("UNLOBD"),
                               REC = b.Field<string>("REC"),
                               WebsiteParams = (from m in ds.Tables[1].AsEnumerable()
                                                where b.Field<Guid>("ID") == m.Field<Guid>("ParentID")
                                                select new CrawlConfigParam
                                                {
                                                    KeyID = m.Field<Guid>("ID"),
                                                    KeyValue = m.Field<string>("KeyValue").Trim(),
                                                    KeyType = (Website_KeyType)m.Field<byte>("KeyType"),
                                                    KeyValueType = (Website_KeyValueType)m.Field<byte>("KeyValueType"),
                                                    ParamType = (Website_ParamType)m.Field<byte>("ParamType"),
                                                    ParamValue = m.Field<string>("ParamValue").Trim(),
                                                    SortIndex = m.Field<int>("SortIndex"),
                                                    Timeout = m.Field<int>("Timeout"),
                                                }).ToList()
                           }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        /// <summary>
        /// 获取船东信息
        /// </summary>
        /// <param name="paramCarrierInfo">船东信息</param>
        /// <param name="paramCOwner">船东所属</param>
        /// <returns></returns>
        public List<ECarrierInfo> GetCarrierInfos(string paramCarrierInfo, CarrierOwner paramCOwner)
        {
            List<ECarrierInfo> results = null;
            Database db = GetDefaultDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspGetCarrierList]");
            db.AddInParameter(dbCommand, "@CarrierInfo", DbType.String, paramCarrierInfo);
            db.AddInParameter(dbCommand, "@COwner", DbType.Int16, paramCOwner);
            DataSet ds = db.TryExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            results = (from b in ds.Tables[0].AsEnumerable()
                       select new ECarrierInfo
                       {
                           ID = b.Field<string>("ID"),
                           Code = b.Field<string>("Code").Trim(),
                       }).ToList();
            return results;
        }
        /// <summary>
        /// 添加船东
        /// </summary>
        public void AddCarrier(string paramOwner)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(paramOwner+"Carriers.xml");
            XmlElement root = doc.DocumentElement;   //获取根节点 
            if (root == null||root.ChildNodes.Count<=0) return;
             //遍历当前节点的所有子节点

            Database dbiw = GetDefaultDatabase();
            int cOwner = paramOwner.Equals("Inttra") ? 1 : 2;
            for (int n = 0; n < root.ChildNodes.Count; n++)
            {
                XmlNode node = root.ChildNodes[n];
                if (node == null)
                    continue;
                if (node.Attributes == null)
                    continue;
                string cCode = node.Attributes["id"].Value;
                string cName = node.Attributes["name"].Value;
                string execString = string.Format("INSERT INTO [csp].[WebCrawler_Carrier]([CCode],[CName],[COwner])VALUES(N'{0}',N'{1}','{2}')", cCode, cName, cOwner);
                execString = string.Format("UPDATE [csp].[WebCrawler_carrier] SET [CName]=N'{0}' WHERE [CCode]= N'{1}' AND [COwner] = '{2}'", cName, cCode, cOwner);
                try
                {
                    DbCommand command = dbiw.GetSqlStringCommand(execString);
                    dbiw.ExecuteNonQuery(command);
                }
                catch
                {
                    
                }
            }
        }
        /// <summary>
        /// 保存异常日志
        /// </summary>
        /// <param name="description">错误描述</param>
        /// <param name="createTime">发生日期</param>
        public void SaveErrorLog(string description, DateTime createTime)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("[sm].[uspSaveSystemErrorLog]");
                db.AddInParameter(dbCommand, "@UserId", DbType.Guid, new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"));
                db.AddInParameter(dbCommand, "@UserName", DbType.String, "System");
                db.AddInParameter(dbCommand, "@sessionId", DbType.String, GlobalVariable.SessionID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, description);
                db.AddInParameter(dbCommand, "@CreateTime", DbType.String, createTime);
                SqlParameter parameter = new SqlParameter("@ScreenCapture", SqlDbType.Image) { Value = new byte[0] };
                dbCommand.Parameters.Add(parameter);
                db.TryExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogService.Error(ModuleName, "SaveErrorLog", ex);
            }
        }
    }
}
