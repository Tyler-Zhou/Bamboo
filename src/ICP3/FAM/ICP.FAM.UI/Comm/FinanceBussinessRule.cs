using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FAM.UI
{

    public class FinancePrintHelper
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        #endregion

        /// <summary>
        /// 获取海运出口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\FAM\\
        /// </summary>
        public string GetOEReportPath()
        {
            return Application.StartupPath + "\\Reports\\FAM\\";
        }

        #region  打印费用清单

        /// <summary>
        /// 打印费用清单
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>IReportViewer</returns>
        public IReportViewer PritnFeeList(Guid operationID)
        {
            FeeListReportData reportData = FinanceReportService.GetFeeListReportData(operationID);
            if (reportData == null) return null;

            ClientFeeListReportData data = new ClientFeeListReportData();
            FAMUtility.CopyToValue(reportData, data, typeof(FeeListReportData));

            if (data.PreETD.HasValue)
            {
                data.PreETDAndCloseData = data.PreETD.Value.ToShortDateString();

                if (data.PreClosingDate.HasValue) data.PreETDAndCloseData += "/" + data.PreClosingDate.Value.ToShortDateString();
            }

            if (data.ETD.HasValue)
            {
                data.ETDAndCloseData = data.ETD.Value.ToShortDateString();
                if (data.ClosingDate.HasValue) data.ETDAndCloseData += "/" + data.ClosingDate.Value.ToShortDateString();
            }

            


            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Fee List" : "打印费用清单", 
                (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath();
            if (LocalData.IsEnglish) fileName += "RptFeeList_EN.frx";
            else fileName += "RptFeeList_CN.frx";

            #region fees
            List<FeeListReportFee> fees = new List<FeeListReportFee>();

            //收
            List<FeeListReportFee> drfees = data.FeeListReportFees.FindAll(delegate(FeeListReportFee item){return item.Way == FeeWay.AR;});
            if(drfees!=null && drfees.Count !=0)
            {
                drfees[0].WayDescription =EnumHelper.GetDescription<FeeWay>(drfees[0].Way,LocalData.IsEnglish);
                fees.AddRange(drfees);
            }
            //付
            List<FeeListReportFee> crfees = data.FeeListReportFees.FindAll(delegate(FeeListReportFee item){return item.Way == FeeWay.AP;});
            if (crfees != null && crfees.Count != 0)
            {
                crfees[0].WayDescription =EnumHelper.GetDescription<FeeWay>(crfees[0].Way,LocalData.IsEnglish);
                fees.AddRange(crfees);
            }

            #endregion

            #region CurrencyFees
            List<FeeListReportCurrencyFee> currencyFees = new List<FeeListReportCurrencyFee>();
            #region 
            if (drfees != null && drfees.Count > 0)
            {
                FeeListReportCurrencyFee temp = new FeeListReportCurrencyFee();
                decimal usdDebit = 0m;
                decimal hkdDebit = 0m;
                decimal rmbDebit = 0m;
                foreach (var item in drfees)
                {
                    if (item.Currency == "USD") usdDebit += item.Amount;
                    else if (item.Currency == "HKD") hkdDebit += item.Amount;
                    else if (item.Currency == "RMB") rmbDebit += item.Amount;
                }
                temp.WayDescription = EnumHelper.GetDescription<FeeWay>(FeeWay.AR, LocalData.IsEnglish);
                temp.USDAmount = "USD:" + usdDebit.ToString("F2");
                temp.RMBAmount = "RMB:" + rmbDebit.ToString("F2");
                temp.HKDAmount = "HKD:" + hkdDebit.ToString("F2");
                currencyFees.Add(temp);
            }

            if (crfees != null && drfees.Count > 0)
            {
                FeeListReportCurrencyFee temp = new FeeListReportCurrencyFee();
                decimal usdCredit = 0m;
                decimal hkdCredit = 0m;
                decimal rmbCredit = 0m;
                foreach (var item in crfees)
                {
                    if (item.Currency == "USD") usdCredit += item.Amount;
                    else if (item.Currency == "HKD") hkdCredit += item.Amount;
                    else if (item.Currency == "RMB") rmbCredit += item.Amount;
                }
                temp.WayDescription = EnumHelper.GetDescription<FeeWay>(FeeWay.AP, LocalData.IsEnglish);
                temp.USDAmount = "USD:" + usdCredit.ToString("F2");
                temp.RMBAmount = "RMB:" + rmbCredit.ToString("F2");
                temp.HKDAmount = "HKD:" + hkdCredit.ToString("F2");
                currencyFees.Add(temp);
            }
            #endregion

            decimal usdProfit = 0m;
            foreach (var item in fees)
            {
                if (item.Way == FeeWay.AR) usdProfit += item.AmountOfUSD;
                else usdProfit -= item.AmountOfUSD; 
            }
            if (currencyFees!=null && currencyFees.Count > 0)
            {
                currencyFees[currencyFees.Count - 1].USDProfit = (LocalData.IsEnglish ? "Profit:USD " : "利润:USD ") + usdProfit.ToString("F2");
            }
            #endregion

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("FeeListReportData", data);
            reportSource.Add("FeeListReportFee", fees);
            reportSource.Add("FeeListReportCurrencyFee", currencyFees);
            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        class ClientFeeListReportData : FeeListReportData
        {
            public string PreETDAndCloseData{get;set;}
            public string ETDAndCloseData{get;set;}
        }


        /// <summary>
        /// 客户端报表对象
        /// </summary>
        class FeeListReportCurrencyFee
        {
            /// <summary>
            /// WayDescription
            /// </summary>
            public string WayDescription { get; set; }
            /// <summary>
            /// USDAmount(USD:5.00)
            /// </summary>
            public string USDAmount { get; set; }
            /// <summary>
            /// RMBAmount(RMB:5.00)
            /// </summary>
            public string RMBAmount { get; set; }
            /// <summary>
            /// HKDAmount(HKD:5.00)
            /// </summary>
            public string HKDAmount { get; set; }
            /// <summary>
            /// USDProfit(Profit:USD  5.00","利润:USD 5.00)
            /// </summary>
            public string USDProfit { get; set; }
        }

        #endregion

    }
}
