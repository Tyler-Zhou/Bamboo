#region Comment

/*
 * 
 * FileName:    InquireRatesBusinessQueryPart.cs
 * CreatedOn:   2014/10/22 11:58:36
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InquireRatesBusinessQueryPart : XtraUserControl, IBusinessQueryPart
    {
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region Fields
        /// <summary>
        /// Xml的文件名称
        /// </summary>
        private const string TempalteFileName = "QueryConditions.xml"; 
        #endregion

        #region Property
        /// <summary>
        /// 业务类型
        /// </summary>
        /// <remarks>询价</remarks>
        [DefaultValue(BusinessType.IR)]
        public BusinessType Type
        {
            get { return BusinessType.IR; }
        } 
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? FromDataTime
        {
            get
            {
                return fromToDateMonthControl1.From;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ToDataTime
        {
            get
            {
                return fromToDateMonthControl1.To;
            }

        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireRatesBusinessQueryPart()
        {
            InitializeComponent();
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initValues"></param>
        public void Init(Dictionary<string, object> initValues)
        {
        }
        /// <summary>
        /// 本地化
        /// </summary>
        public void Locale()
        {
        }
        /// <summary>
        /// 重置查询调减
        /// </summary>
        public void Reset()
        {
            txtSNo.Text = string.Empty;
            txtPOD.Text = string.Empty;
            txtPOL.Text = string.Empty;
            txtDelivery.Text = string.Empty;
            txtComm.Text = string.Empty;
            fromToDateMonthControl1.radioGroup1.SelectedIndex = 0;
        }
        /// <summary>
        /// 获取高级查询查询条件字符串
        /// </summary>
        /// <returns>高级查询查询条件字符串</returns>
        public string GetAdvanceQueryString()
        {
            string queryString = " 1=1";
            string strSingleWhere = string.Empty;
            strSingleWhere = SqlFilterHelper.SqlFilter(txtSNo.Text.Trim());
            if (!string.IsNullOrEmpty(strSingleWhere))
            {
                queryString += " and $@No@ like '%" + strSingleWhere + "%'";
            }
            strSingleWhere = SqlFilterHelper.SqlFilter(txtPOD.Text.Trim());
            if (!string.IsNullOrEmpty(strSingleWhere))
            {
                queryString += " and ($@PODEName@ like '%" + strSingleWhere + "%' or  $@PODCName@ like '%" + strSingleWhere + "%')";
            }
            strSingleWhere = SqlFilterHelper.SqlFilter(txtPOL.Text.Trim());
            if (!string.IsNullOrEmpty(strSingleWhere))
            {
                queryString += " and ($@POLEName@ like '%" + strSingleWhere + "%' or $@POLCName@ like '%" + strSingleWhere + "%')";
            }
            strSingleWhere = SqlFilterHelper.SqlFilter(txtDelivery.Text.Trim());
            if (!string.IsNullOrEmpty(strSingleWhere))
            {
                queryString += " and ($@DeliveryEName@ like '%" + strSingleWhere + "%' or $@DeliveryCName@ like '%" + strSingleWhere + "%')";
            }
            strSingleWhere = SqlFilterHelper.SqlFilter(txtComm.Text.Trim());
            if (!string.IsNullOrEmpty(strSingleWhere))
            {
                queryString += " and $@Commodity@ like '%" + strSingleWhere + "%'";
            }
            strSingleWhere = string.Empty;
            if (fromToDateMonthControl1.radioGroup1.SelectedIndex > 0)
            {
                if (FromDataTime!=DateTime.MinValue)
                    queryString += " and $@UpdateDate@>='" + FromDataTime + "' ";
                if (ToDataTime!=DateTime.MinValue)
                    queryString += " and $@UpdateDate@<='" + ToDataTime + "'";
            }
            return queryString;
        }
        /// <summary>
        /// 获取当前用户的上一次的查询条件
        /// </summary>
        /// <returns></returns>
        public string SetQueryConditions()
        {
            string stroriginal = string.Empty;
            string templateFilePath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.BusinessTemplates, TempalteFileName);
            if (!File.Exists(templateFilePath)) return stroriginal;

            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmldocment = from ent in document.Descendants("Item") select ent;
            if (xmldocment.Any())
            {
                foreach (var xElement in xmldocment)
                {
                    Guid Userid = new Guid(xElement.Attribute("UserId").Value);
                    if (Userid == LocalData.UserInfo.LoginID)
                    {
                        stroriginal = xElement.Attribute("Stroriginal").Value;
                    }
                }
            }
            return stroriginal;
        }
        /// <summary>
        /// 初始化上一次查询结果集合的赋值操作
        /// </summary>
        /// <param name="initValues"></param>
        public void LastAdvanceQueryString(Dictionary<string, object> initValues)
        {
            string stroriginal = SetQueryConditions();
            if (!string.IsNullOrEmpty(stroriginal))
            {
                var strReplace = stroriginal.Replace("and", "*").Replace("or", "*").Replace("1=1", string.Empty);
                var strSplit = strReplace.Split('*');
                foreach (string str in strSplit)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str.Contains("$@No@"))
                        {
                            txtSNo.Text = Strreplace(str);
                        }
                        if (str.Contains("$@POLEName@"))
                        {
                            txtPOL.Text = Strreplace(str);
                        }
                        if (str.Contains("$@POD@"))
                        {
                            txtPOD.Text = Strreplace(str);
                        }
                        if (str.Contains("$@Delivery@"))
                        {
                            txtDelivery.Text = Strreplace(str);
                        }
                        if (str.Contains("$@Comm@"))
                        {
                            txtComm.Text = Strreplace(str);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返回替换好的字符串
        /// </summary>
        /// <param name="replace">替换的字符串</param>
        /// <returns></returns>
        public string Strreplace(string replace)
        {
            if (replace.Contains("like"))
            {
                replace = replace.Replace("%", string.Empty).Replace("'", string.Empty)
                          .Replace("like", "?");

            }
            else if (replace.Contains("in"))
            {
                replace = replace.Replace("'", string.Empty).Replace("in", "?");

            }
            else
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?");

            }
            replace = replace.Replace("(", string.Empty).Replace(")", string.Empty);
            return replace.Split('?')[1].TrimStart().TrimEnd();
        }
        #endregion
        
    }
}
