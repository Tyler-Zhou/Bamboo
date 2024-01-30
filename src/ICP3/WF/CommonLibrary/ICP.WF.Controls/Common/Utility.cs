
//-----------------------------------------------------------------------
// <copyright file="Utility.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using ICP.Framework.CommonLibrary.Client;
    using System.Text.RegularExpressions;
    using ICP.WF.ServiceInterface.DataObject;

    /// <summary>
    /// 通用帮助类
    /// </summary>
    public class Utility
    {
        #region  将页面只读
        /// <summary>
        /// 页面只读
        /// </summary>
        /// <param name="parentControl"></param>
        public static void SetReadOnly(Control parentControl,bool readOnly)
        {
            ProcessPart(parentControl, readOnly);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="isReadOnly"></param>
        private static void ProcessPart(Control parentControl, bool isReadOnly)
        {
            if (parentControl.HasChildren==false)
            {
                System.Reflection.PropertyInfo pi = parentControl.GetType().GetProperty("ReadOnly");
                if (pi != null)
                {
                    pi.SetValue(parentControl, isReadOnly,null);
                }
                else
                {
                    parentControl.Enabled = false;
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    ProcessPart(ctrl, isReadOnly);
                }
            }
        }
        #endregion

        /// <summary>
        /// 设置表的Default值        /// </summary>
        /// <param name="dr"></param>
        public static void SetDefaultValue(DataTable dtb)
        {
            if (dtb == null)
            {
                return;
            }
            foreach (DataColumn col in dtb.Columns)
            {
                switch (col.DataType.Name)
                {
                    case "String":
                        col.DefaultValue = string.Empty;
                        break;
                    case "Int32":
                        col.DefaultValue = (int)0;
                        break;
                    case "Int16":
                        col.DefaultValue = (short)0;
                        break;
                    case "Boolean":
                        col.DefaultValue = false;
                        break;
                    case "Decimal":
                        col.DefaultValue = 0.0m;
                        break;
                    case "Guid":
                        col.DefaultValue = Guid.Empty;
                        break;
                    case "DateTime":
                        col.DefaultValue = DateTime.Now;
                        break;
                    default:
                        col.DefaultValue = new object();
                        break;
                }
            }
        }


        public static object GetDefaultValue(System.Type tp)
        {
            switch (tp.Name)
            {
                case "String":
                    return string.Empty;
                case "Int32":
                    return (int)0;
                case "Int16":
                    return (short)0;
                case "Boolean":
                    return false;
                case "Decimal":
                    return 0.0m;
                case "Guid":
                    return Guid.Empty;
                case "DateTime":
                    return DateTime.Now;
                default:
                    return null;
              
            }
        }


        public static bool IsDefaultValue(object val)
        {
            if (val == null || string.IsNullOrEmpty(val.ToString())) return true;

            try
            {
                if (val is System.Guid)
                {
                    return val.ToString().Equals(System.Guid.Empty.ToString());
                }
                else if (val is decimal)
                {
                    decimal v = (decimal)val;
                    return v == 0;
                }
                else if (val is short)
                {
                    short v = (short)val;
                    return v == 0;
                }
                else if (val is int)
                {
                    int v = (int)val;
                    return v == 0;
                }

                return false;
            }
            catch
            {
                return true;
            }

        }

            public static bool IsGuid(string str)
            {
                Match m = Regex.Match(str, @"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    //可以转换 
                    return true;
                }
                else
                {
                    //不可转换 
                    return false;
                }
            }
            public static bool GuidIsNullOrEmpty(Guid? id)
            {
                if (id == null || id.Value == Guid.Empty) return true;
                else return false;
            }

        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        public static bool IsEnglish
        {
            get
            {

                return LocalData.IsEnglish;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetPascalProperty(string name)
        {
            //
            char[] chars = name.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Char.IsUpper(chars[i]))
                {
                    return name.Substring(i);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据关键字查找中英文资源
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">如果在当前语言环境中，没找到，就设置为当前的默认值</param>
        /// <returns></returns>
        public static string GetString(
            string key,
            string defaultValue)
        {
            try
            {
                if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                {//查找英文资源
                    string enVal = ICP.WF.Controls.Resources.ResourceEn.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal)==false)
                    {
                        return enVal;
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.WF.Controls.Resources.ResourceCN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal)==false)
                    {
                        return cnVal;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }


        public static string GetString(
            string key, 
            string defaultValue, 
            params object[] args)
        {
            try
            {
                if (IsEnglish)
                {//查找英文资源
                    string enVal = ICP.WF.Controls.Resources.ResourceEn.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal)==false)
                    {
                        return string.Format(enVal, args);
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.WF.Controls.Resources.ResourceCN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal)==false)
                    {
                        return string.Format(cnVal, args); ;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 创建特殊列
        /// </summary>
        /// <param name="orgColumnName">原列名</param>
        /// <param name="orgColumnType">原列类型</param>
        /// <param name="type">转换目标类型</param>
        /// <returns>返回转换后的列名</returns>
        public static string BuildSpecialColumnName(
            string orgColumnName,
            Type orgColumnType,
            FieldType type)
        {
            string _name = orgColumnName;
            if (_name == null)
            {
                _name = string.Empty;
            }

            if (type == FieldType.Department)
            {
                if (!_name.ToUpper().EndsWith(WWFConstants.DepartmentID.ToUpper()) && !_name.ToUpper().EndsWith(WWFConstants.DepartmentName.ToUpper()))
                {
                    _name = _name.Replace(WWFConstants.DepartmentID, "").Replace(WWFConstants.DepartmentName, "");

                    if (orgColumnType == typeof(System.Guid)
                        || orgColumnType == typeof(System.Guid?))
                    {
                        _name = _name + WWFConstants.DepartmentID;
                    }
                    else
                    {
                        _name = _name + WWFConstants.DepartmentName;
                    }
                }
            }
            else if (type == FieldType.User)
            {
                if (!_name.ToUpper().EndsWith(WWFConstants.UserID.ToUpper()) && !_name.ToUpper().EndsWith(WWFConstants.UserName.ToUpper()))
                {
                    _name = _name.Replace(WWFConstants.UserID, "").Replace(WWFConstants.UserName, "");
                    if (orgColumnType == typeof(System.Guid)
                        || orgColumnType == typeof(System.Guid?))
                    {
                        _name = _name + WWFConstants.UserID;
                    }
                    else
                    {
                        _name = _name + WWFConstants.UserName;
                    }
                }
            }
            else if (type == FieldType.Job)
            {
                if (!_name.ToUpper().EndsWith(WWFConstants.JobID.ToUpper()) && !_name.ToUpper().EndsWith(WWFConstants.JobName))
                {
                    _name = _name.Replace(WWFConstants.JobID, "").Replace(WWFConstants.JobName, "");
                    if (orgColumnType == typeof(System.Guid)
                        || orgColumnType == typeof(System.Guid?))
                    {
                        _name = _name + WWFConstants.JobID;
                    }
                    else
                    {
                        _name = _name + WWFConstants.JobName;
                    }
                }
            }

            return _name;
        }

        /// <summary>
        /// 判断是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*[1-9][0-9]*$");
        }

        /// <summary>
        /// 判断是否为数字和字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumAndLetter(string str)
        {
            return Regex.IsMatch(str, @"^[A-Za-z0-9]+$");
        }

    }
}
