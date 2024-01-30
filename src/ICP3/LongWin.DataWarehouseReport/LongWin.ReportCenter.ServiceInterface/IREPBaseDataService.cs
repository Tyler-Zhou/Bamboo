using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Agilelabs.Framework;


namespace LongWin.ReportCenter.ServiceInterface
{
    /// <summary>
    /// 该服务用来获取组织结构和用户等基础信息
    /// </summary>
    [ServiceInfomation("IREPBaseDataService", Agilelabs.Framework.ServiceType.Business)]
    public interface IREPBaseDataService
    {
        [FunctionInfomation("根据当前用户返回报表服务器的地址")]
        ReportServerInfo GetReportServerUrl();
         
        /// <summary>
        /// 根据当前用户返回可以选择的最大用户的集合
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择的最大用户的集合")]
        DataSet GetUserSetByCurrentUser();
        /// <summary>
        /// 根据当前用户返回可以选择的最大用户的集合-针对成本分析
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择的最大用户的集合-针对成本分析")]
        DataSet GetUserSetForCostByCurrentUser();
        /// <summary>
        /// 获取海外部的人员
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("获取海外部的人员")]
        DataSet GetUserSetForAGTCRM();
        
        /// <summary>
        /// 根据当前用户返回可以选择的最大用户的集合-针对代理对帐单
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择的最大用户的集合-针对代理对帐单")]
        DataSet GetUserSetForAgentDcNoteByCurrentUser();
        /// <summary>
        /// 选出所有的用户
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("选出所有的用户")]
        DataSet GetAllUserSet();
        /// <summary>
        /// 根据当前用户返回可以选择所有拓展员
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择所有拓展员")]
        DataSet GetDevelopersByCurrentUser();
        /// <summary>
        /// 根据当前用户返回可以选择所有市场部人员
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择所有市场部人员")]
        DataSet GetMKSalesByCurrentUser();

        /// <summary>
        /// 返回当前用户的组织结构的集合，用于初始化
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("返回当前用户的组织结构的集合，用于初始化")]
        DataSet GetCurrentUserStations();
        /// <summary>
        /// 根据当前用户返回可以选择组织结构
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择组织结构")]
        DataSet GetStructureNodesByCurrentUser();
        /// <summary>
        /// 获取成本分析的用户所在的公司
        /// </summary>
        /// <param name="Conditon"></param>
        /// <returns></returns>
        [FunctionInfomation("获取成本分析的用户所在的公司")]
        DataSet GetStructureNodesForCostByCurrentUser();
        /// <summary>
        /// 获取代理对帐单的用户所在的公司,如果是海外拓展,则需要返回整个公司

        /// 如果是财务,就返回该用户所在的公司，否则就返回该用户所在的部门节点
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("获取代理对帐单的用户所在的公司")]
        DataSet GetStructureNodesForAgentByCurrentUser();
        /// <summary>
        
        
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [FunctionInfomation("获取客户信息")]
        DataSet GetCustomerByCondition(string condition);

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [FunctionInfomation("获取角色信息")]
        DataSet GetRolesByCondition();
        
        /// <summary>
        /// 当前用户是否有权限查看费用的往来单位,用于业务信息详细表

        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("察看当前用户是否有权限查看费用的往来单位")]
        bool GetUserIsManange();

        /// <summary>
        /// 获取报表抬头信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("获取报表抬头信息")]
        ReportInfo.TitleInfoDataTable GetReportTitleInfo(Guid structureNodeId);

        /// <summary>
        /// 获取当前用户所能看到的所有拓展员的集合

        /// </summary>
        /// <param name="structureNodeIds"></param>
        /// <returns></returns>
        [FunctionInfomation("获取拓展员")]
        DataSet GetDeveloperSales();

        /// <summary>
        /// 获取当前用户所能看到的所有市场部人员的集合

        ///// </summary>
        ///// <param name="structureNodeIds"></param>
        ///// <returns></returns>
        //[FunctionInfomation("获取市场部人员")]
        //DataSet GetMarketSales();

        [FunctionInfomation("获取客户组")]
        DataSet GetMarketAccountByCondition(string Condition);

        /// <summary>
        /// 查看是否海外拓展,如果是经理(除了海外不经理),总经理，财务等，返回false
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("查看是否海外拓展")]
        bool IsAgentDev();

        /// <summary>
        /// 获取考试列表
        /// </summary> 
        /// <returns></returns>
        [FunctionInfomation("获取考试列表")]
        DataSet GetAllExams();

        /// <summary>
        /// 根据当前用户返回可以选择组织结构[用于考试中]
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择组织结构[用于考试中]")]
        DataSet GetStructureNodesByExamUser();

        /// <summary>
        /// 返回目标方案[用于目标完成统计报表中]
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("返回目标方案[用于目标完成统计报表中]")]
        DataSet GetAllProject();

        /// <summary>
        /// 根据当前用户返回可以选择的最大用户的集合[目标管理报表]
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation("根据当前用户返回可以选择的最大用户的集合[目标管理报表]")]
        DataSet GetUserSetByCurrentBMOUser();
    }



    public static class ExtendList
    {
        /// <summary>
        /// 把指定的列表类型对象,转换为DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable ToTable(this System.Collections.IEnumerable list, Type type)
        {
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            DataTable dt = new DataTable();
            foreach (System.Reflection.PropertyInfo p in ps)
            {
                if (p.PropertyType.IsArray) continue;

                System.Data.DataColumn dc = new DataColumn();
                dc.DataType = p.PropertyType;
                dc.ColumnName = p.Name;
                dt.Columns.Add(dc);
            }

            foreach (object v in list)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn c in dt.Columns)
                {
                    dr[c.ColumnName] = ps.FirstOrDefault(delegate(System.Reflection.PropertyInfo p) { return p.Name == c.ColumnName; }).GetValue(v, null);
                }

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

            return dt;
        }


        public static DataSet ToDataSet(this System.Collections.IList lst)
        {
            if (lst == null || lst.Count == 0) return null;

            DataTable table = new DataTable();
            PropertyInfo[] props = lst[0].GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                DataColumn col = new DataColumn(p.Name, p.PropertyType);
                table.Columns.Add(col);
            }

            table.BeginLoadData();
            foreach (object item in lst)
            {
                DataRow dr = table.NewRow();
                foreach (PropertyInfo p in props)
                {
                    dr[p.Name] = p.GetValue(item, null);
                }
                table.Rows.Add(dr);
            }
            table.EndLoadData();
            table.AcceptChanges();

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            return ds;
        }

        /// <summary>
        /// 把一个列表转换为DataTable
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static DataTable ToTable(this System.Collections.IList lst)
        {
            if (lst == null || lst.Count == 0) return null;

            DataTable table = new DataTable();
            PropertyInfo[] props = lst[0].GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                DataColumn col = new DataColumn(p.Name, p.PropertyType);
                table.Columns.Add(col);
            }

            table.BeginLoadData();
            foreach (object item in lst)
            {
                DataRow dr = table.NewRow();
                foreach (PropertyInfo p in props)
                {
                    dr[p.Name] = p.GetValue(item, null);
                }
                table.Rows.Add(dr);
            }
            table.EndLoadData();
            table.AcceptChanges();
            return table;
        }



        public static string ToCustomString(this System.Collections.IEnumerable list, string title)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (string.IsNullOrEmpty(title) == false)
            {
                sb.Append(title);
                sb.Append(System.Environment.NewLine);
            }
            int i = 0;
            foreach (string s in list)
            {
                i++;
                sb.Append(i.ToString());
                sb.Append(":");
                sb.Append(s);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
