#region Comment

/*
 * 
 * FileName:    BusinessModel.cs
 * CreatedOn:   2014/5/14 星期三 17:21:31
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->业务数据DB交互类
 *      ->GetBusinessList
 *          1.获取所有业务数据
 *          2.通过传入Where查询条件获取业务数据
 *          3.传入Where查询条件、Where查询字段获取业务数据
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICP.Document
{
    /// <summary>
    /// 业务数据DB交互类
    /// </summary>
    public class BusinessModel
    {
        /// <summary>
        /// 获取所有业务数据
        /// </summary>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessList()
        {
            Dictionary<string, string> whereField = new Dictionary<string, string>();
            whereField.Add("opd", "0");
            return GetBusinessList("",whereField);
        }

        /// <summary>
        /// 获取所有业务数据
        /// </summary>
        /// <param name="strWhere">Where语句</param>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessList(string strWhere)
        {
            return GetBusinessList(strWhere,null);
        }

        /// <summary>
        /// 获取所有业务数据
        /// </summary>
        /// <param name="strWhere">Where语句</param>
        /// <param name="whereField">Where 查询字段</param>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessList(string strWhere,Dictionary<string,string> whereField)
        {
            List<BusinessInfo> busList = null;
            try
            {
                String queryString = "SELECT * FROM [OperationViewOECache]";
                if (!string.IsNullOrEmpty(strWhere))
                    queryString += strWhere;
                SqlCeHelper sqlObject = new SqlCeHelper(ClientConstants.CurrentDBPath);
                if (whereField != null)
                    sqlObject.SetWhereFields(whereField);

                DataTable dt = sqlObject.ReturnDataTable(queryString);

                if (dt == null || dt.Rows.Count <= 0)
                    return busList;

                busList = new List<BusinessInfo>();

                busList = (from entry in dt.AsEnumerable()
                           select new BusinessInfo
                           {
                               OperationID = entry.Field<Guid>("OceanBookingID"),
                               NO = entry.Field<String>("NO"),
                               SO_NO = entry.Field<String>("SONO"),
                               Carrier = entry.Field<String>("CarrierCode"),
                               POL = entry.Field<String>("POL_EName"),
                               POD = entry.Field<String>("POD_EName"),
                               Vessel = entry.Field<String>("Vessel"),
                               Voyage = entry.Field<String>("Voyage"),
                               RefNO = entry.Field<String>("RefNO"),
                               Description = entry.Field<String>("Description"),
                               ContainerDesc = entry.Field<String>("ContainerDesc"),
                               ContainerNO = entry.Field<String>("ContainerNO"),
                               ETD = entry.Field<DateTime?>("ETD"),
                               ETA = entry.Field<DateTime?>("ETA")
                           }).ToList();
            }
            catch
            {
                busList = null;
            }
            return busList;
        }
    }
}
