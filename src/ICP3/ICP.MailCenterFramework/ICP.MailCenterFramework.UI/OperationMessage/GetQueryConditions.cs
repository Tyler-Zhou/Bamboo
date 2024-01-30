using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using ICP.Operation.Common.ServiceInterface;
using System.Windows.Forms;
namespace ICP.MailCenterFramework.UI
{
    public class GetQueryConditions
    {
        /// <summary>
        /// 服务端查询语句
        /// </summary>
       public static string ServerQueryString
       {
           get;
           set;
       }
       /// <summary>
       /// 业务信息实体类
       /// </summary>
       public BusinessQueryCriteria Criteria { get; set; }
       /// <summary>
       /// 已选择的公司列表字符串
       /// </summary>
       private static string _selectedCompanyIds;
       public static string SelectedCompanyIds
       {
           get
           {
               if (string.IsNullOrEmpty((_selectedCompanyIds)))
               {
                   _selectedCompanyIds = LocalData.IsDesignMode ? string.Empty : LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).Select(o => o.ID.ToString()).Aggregate((a, b) => a + "," + b);
               }

               return _selectedCompanyIds;
           }
           set { _selectedCompanyIds = value; }
       }
       #region 获取查询条件
       /// <summary>
        /// 根据业务号和参考号来查询业务数据
        /// </summary>
        /// <returns></returns>
        public static  string AppendAdvanceStringToSQL(string subject)
        {
            StringBuilder strSQL = new StringBuilder();
            ServerQueryString = "";
            List<string> nos = OutlookUtility.MatchArray(subject);//根据主题获取主题单号
            if (nos != null && nos.Count > 0)
            {
                int i = 0;
                foreach (var item in nos)
                {
                    strSQL.Append(GetQueryString(item, i == 0));
                    i++;
                }
            }
            return strSQL.ToString();
        }
        private static string GetQueryString(string query, bool firstCase)
        {
            //服务端查询条件语句
            GetServerQueryString(query);
            //客户端查询条件语句
            string localSQL = string.Format(" AND (No LIKE '%{0}%' OR RefNO LIKE '%{0}%') ", query);
            if (firstCase)
                return localSQL;
            else
                return string.Format("| {0}", localSQL);
        }

        private static void GetServerQueryString(string query)
        {
            //如果邮件中心快速切换邮件，这里会出现 “集合已修改；可能无法执行枚举操作”错误。
            if (string.IsNullOrEmpty(ServerQueryString))
                ServerQueryString =
                    string.Format(" 1=1 AND $@CompanyID@ in ({0}) AND ($@NO@ LIKE '%{1}%' OR $@RefNO@ LIKE '%{1}%') ", GetCompanyQueryString(), query);
            else
            {
                ServerQueryString = string.Format("{0}{1}", ServerQueryString,
                                                                        string.Format(
                                                                            " OR ($@NO@ LIKE '%{0}%' OR  $@RefNO@ LIKE '%{0}%') ",
                                                                            query));
            }
        }
        private static string GetCompanyQueryString()
        {
            string strList = string.Empty;
            string[] arrCompanyIDs = SelectedCompanyIds.Split(new char[1] { ',' });
            for (int i = 0; i < arrCompanyIDs.Length; i++)
            {
                strList += (strList.Length == 0 ? "" : ",") + "'" + arrCompanyIDs[i] + "'";
            }
            return strList;
        }
       #endregion

        #region 获取关键字查询条件
        public string EmailQuery(string queryNo)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //string queryNo = this.QueryNo;
                //如果搜索单号为空，则直接返回
                if (string.IsNullOrEmpty(queryNo))
                    return "";

                //如果此次搜索和上次搜索单号不同
                else
                {
                   return InnerQuery(queryNo);
                }
            }
        }
        /// <summary>
        /// 邮件中心-从数据源中执行查找匹配的KeyWord
        /// </summary>
        /// <param name="queryNo"></param>
        private string InnerQuery(string queryNo)
        {
            ServerQueryString = "";
            //this.AdvanceQueryString = GetQueryString(queryNo);
            //////数据绑定完成
            ////RootWorkItem.State["DataBindingComplete"] = false;
            ////如果从本地缓存数据库里没有找到数据，就需要到SQL Server 去查询
            //NeedSearchInSQLServer = true;
            ////标识是不是高级搜索
            //SearchType = SearchActionType.KeyWord;
            //IsShowLoadingForm = false;
            //this.QueryData(true);

           return GetQueryString(queryNo);

        }
        /// <summary>
        /// 将A/B/C分割，拼接SQL
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string GetQueryString(string query)
        {
            if (query.Contains("/"))
            {
                StringBuilder strBuf = new StringBuilder();
                string[] arrQuery = query.Split(new char[1] { '/' });
                int i = 0;
                foreach (var item in arrQuery)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        strBuf.Append(GetQueryString(item, i == 0));
                        i++;
                    }
                }

                return strBuf.ToString();
            }
            else
            {
                return GetQueryString(query, true);
            }
        }
        #endregion
    }
}
