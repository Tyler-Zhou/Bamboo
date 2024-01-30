using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Reporting.WinForms;

namespace ICP.ReportCenter.UI
{
    public class ReportBaseSearchPart : BaseSearchPart
    {
        /// <summary>
        /// 用于下钻报表取得数据
        /// </summary>
        /// <param name="reportPath">reportPath</param>
        /// <param name="parameters">parameters</param>
        /// <returns>ReportDataSource</returns>
        public virtual ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters) { throw new NotImplementedException(); }
    }
}
