

//-----------------------------------------------------------------------
// <copyright file="CommonHelper.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// 财务模块服务
/// </summary>
namespace ICP.FAM.ServiceComponent.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class UIModelHelper
    {
        /// <summary>
        /// 生成一个T类型的对象,并默认填充属性的值.
        /// </summary>
        /// <typeparam name="T">要生成的类型</typeparam>
        /// <returns></returns>
        public static T GetNormalObject<T>() where T : new()
        {
            T t = new T();

            Type sourceType = t.GetType();
            System.Reflection.PropertyInfo[] properties = sourceType.GetProperties();
            foreach (var property in properties)
            {
                System.Random random = new Random();
                if (property.CanWrite == false) continue;

                try
                {
                    if (property.PropertyType == typeof(string))
                    {
                        if (property.Name.Contains("Customer"))
                            property.SetValue(t, RandomHelper.Customer(), null);
                        else if (property.Name.Contains("Creaty"))
                            property.SetValue(t, RandomHelper.User(), null);
                        else if (property.Name.Contains("Currency"))
                            property.SetValue(t, RandomHelper.Currency(), null);
                        else
                            property.SetValue(t, property.Name, null);
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        property.SetValue(t, RandomHelper.Date(), null);
                    }
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        property.SetValue(t, random.Next(0, 5000), null);
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        property.SetValue(t, RandomHelper.Amount(), null);
                    }
                    else if (property.PropertyType == typeof(short) || property.PropertyType == typeof(short?))
                    {
                        property.SetValue(t, (short)random.Next(1, 2), null);
                    }
                    else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
                    {
                        if (property.Name.ToUpper().Contains("COMPANYID"))
                            property.SetValue(t, RandomHelper.CompanyID(), null);
                        else if (property.Name.ToUpper().Contains("SolutionID".ToUpper()))
                            property.SetValue(t, RandomHelper.SolutionID(), null);
                        else if (property.Name.ToUpper().Contains("Currency".ToUpper()))
                            property.SetValue(t, RandomHelper.CurrencyID(), null);
                        else
                            property.SetValue(t, Guid.NewGuid(), null);
                    }
                    else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                    {
                        property.SetValue(t, RandomHelper.GetBoolen(), null);
                    }
                    else if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(t, (short)random.Next(1, 2), null);
                    }
                }
                catch { continue; }

            }

            return t;
        }
    }

    public class RandomHelper
    {
        static System.Random r = null;
        public static Random Random
        {
            get
            {
                if (r == null) r = new Random();

                return r;
            }
        }

        public static Guid SolutionID()
        {
            return new Guid("B6E4DDED-4359-456A-B835-E8401C910FD0");
        }
        public static Guid CompanyID()
        {
            return new Guid("18D4697C-AA59-E011-8208-001321CC6D9F");
        }
        public static Guid CurrencyID()
        {
            return new Guid("AF34585F-3DB8-46E1-9404-B64AE9501D10");
        }

        public static string Currency()
        {
            List<string> strs = new List<string> { "USD", "RMB", "CHK", "CAD" };
            return strs[Random.Next(0, strs.Count - 1)];
        }

        public static string Customer()
        {
            List<string> strs = new List<string> { "上海爱建进出口", "恒达国贸", "嘉陵摩托", "致富帽业", " GLOBALINK IMPOR", " 广州粮油食品进", "世荣国际运输代理", " SILVA INTERNATIONAL,INC." };
            return strs[Random.Next(0, strs.Count - 1)];
        }

        public static decimal Amount()
        {
            return decimal.Parse((Random.Next(-500, 500) + r.NextDouble()).ToString("F3"));
        }

        public static string User()
        {
            List<string> strs = new List<string> { "孔海军", "周莉莉", "余英姿", "付燕", "俞虹", "曾蓉", "李旸", "雷芳", "金立", "尹佳斌" };
            return strs[Random.Next(0, strs.Count - 1)];
        }

        public static bool GetBoolen()
        {
            return Random.Next(0, 1) == 1;
        }


        public static DateTime Date()
        {
            return DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(Random.Next(-500, 500));
        }

    }

    public class ValidateHelper
    {
        public static DataTable GetDateTableColumnType(DataTable dt, Type sourceType)
        {
            DataTable tempDt= new DataTable("TempTable");
            tempDt.Columns.Add("Name");
            tempDt.Columns.Add("Type");
            tempDt.Columns.Add("PropertyName");
            tempDt.Columns.Add("PropertyType");

            List<System.Reflection.PropertyInfo> properties = sourceType.GetProperties().ToList();

            foreach (DataColumn dc in dt.Columns)
            {
                DataRow dr = tempDt.NewRow();
                dr[0] = dc.ColumnName ;
                dr[1]=dc.DataType.ToString();

                System.Reflection.PropertyInfo info = properties.Find(o=>o.Name.ToUpper().Contains(dc.ColumnName.ToUpper()) 
                    || dc.ColumnName.ToUpper().Contains(o.Name.ToUpper()));

                if (info != null)
                {
                    dr[2] = info.Name;
                    dr[3] = info.PropertyType.ToString();
                }
                tempDt.Rows.Add(dr);
            }
            return tempDt;
        }

        public static DataTable GetDateTableColumnType(DataTable dt)
        {
            DataTable tempDt = new DataTable("TempTable");
            tempDt.Columns.Add("Name");
            tempDt.Columns.Add("Type");

            foreach (DataColumn dc in dt.Columns)
            {
                DataRow dr = tempDt.NewRow();
                dr[0] = dc.ColumnName;
                dr[1] = dc.DataType.ToString();
                tempDt.Rows.Add(dr);
            }
            return tempDt;
        }
    }
}
