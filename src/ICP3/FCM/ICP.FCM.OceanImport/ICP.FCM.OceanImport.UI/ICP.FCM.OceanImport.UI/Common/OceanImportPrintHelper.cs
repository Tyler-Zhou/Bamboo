using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.UI.Report;
using ICP.FCM.OceanImport.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using System.Linq;
using ICP.ReportCenter.UI;
using ICP.ReportCenter.UI.FCMReports;
using System.IO;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;


namespace ICP.FCM.OceanImport.UI.Common
{
    public class OceanImportPrintHelper : WorkItem
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOIReportDataService OIReportDataService 
        {
            get 
            {
                return ServiceClient.GetService<IOIReportDataService>();
            }
        }

        public IReportViewService ReportViewService 
        {
            get 
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        public IOceanImportService OceanImportService 
        {
            get 
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public IConfigureService ConfigureService 
        {

            get 
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IFinanceService FinanceService 
        {
            get 
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        /// <summary>
        /// 获取海运进口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\
        /// </summary>
        public string GetOIReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\";
        }

        /// <summary>
        /// 打印进口操作联系单
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOIOrder(Guid orderID,Guid companyID, ICP.Message.ServiceInterface.Message message)
        {
            OIOrderReportData data = OIReportDataService.GetOIOrderReportData(orderID, companyID);
            if (data == null) return null;
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOIReportPath();
            if (LocalData.IsEnglish) fileName += "OI_OrderInfo_EN.frx";
            else fileName += "OI_OrderInfo_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", data);
            reportSource.Add("OrderFee", data.Fees);

            CustomerInfo customerinfo = GetCustomerInfo(orderID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            //邮件传真业务关联信息
            //ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = orderID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = orderID;
            viewer.BindData(fileName, reportSource, null, message);
            return viewer;
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintProfit(OceanBusinessList businessListItem, ICP.Message.ServiceInterface.Message operationInfo)
        {
            OceanBusinessInfo businessInfo = OceanImportService.GetBusinessInfo(businessListItem.ID);
            string mblString = businessListItem.MBLNo;
            string hblString = businessListItem.SubNo;
            string vesselVoyageString = businessListItem.VesselVoyage;
            if (string.IsNullOrEmpty(businessListItem.MBLNo) && !ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(businessInfo.MBLID))
            {
                businessInfo.MBLInfo = OceanImportService.GetOIMBLInfo(businessInfo.MBLID.Value);
                mblString = businessInfo.MBLInfo.MBLNo;
                vesselVoyageString = businessInfo.MBLInfo.VoyageName;
                businessInfo.HBLList = OceanImportService.GetOIBookingHBLList(businessInfo.ID);
                if (businessInfo.HBLList != null && businessInfo.HBLList.Count > 0)
                {
                    foreach (var hbl in businessInfo.HBLList)
                    {
                        if (!string.IsNullOrEmpty(hbl.HBLNo) && !string.IsNullOrEmpty(hblString))
                        {
                            hblString += ", ";
                        }


                        hblString += hbl.HBLNo;
                    }
                }
            }

            ProfitReportData reportData = new ProfitReportData();
            reportData.BaseReportData = new ProfitBaseReportData();
            reportData.BaseReportData.PrintDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            reportData.BaseReportData.DefaultCurrency = configureInfo.DefaultCurrency;
            reportData.BaseReportData.ReferenceNo = businessInfo.No;
            reportData.BaseReportData.MasterBLNo = mblString;
            reportData.BaseReportData.HouseBLNo = hblString;
            reportData.BaseReportData.AgentName = businessInfo.AgentName;
            reportData.BaseReportData.VesselVoyageNo = vesselVoyageString;
            reportData.BaseReportData.LoadPortName = businessInfo.POLName;
            reportData.BaseReportData.ETD = businessInfo.ETD == null ? string.Empty : businessInfo.ETD.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DiscPortName = businessInfo.PODName;
            reportData.BaseReportData.ETA = businessInfo.ETA == null ? string.Empty : businessInfo.ETA.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            List<BillList> billList = FinanceService.GetBillListByOperactioID(businessListItem.ID);
            if (billList != null && billList.Count > 0)
            {
                reportData.Fees = new List<ProfitReportFeeData>();
                decimal totalRevenue = 0m;
                decimal totalCost = 0m;
                decimal totalAgent = 0m;

                List<SolutionExchangeRateList> rateList = ConfigureService.GetCompanyExchangeRateList(businessInfo.CompanyID, true);
                //foreach (var item in dic)
                //{
                //    profitAmount += Utility.GetAmountByRate(item.Value, item.Key, configureInfo.DefaultCurrencyID, rateList);
                //}

                foreach (var item in billList)
                {
                    BillInfo billInfo = FinanceService.GetBillInfo(item.ID);
                    if (billInfo.Fees != null && billInfo.Fees.Count > 0)
                    {
                        foreach (var fee in billInfo.Fees)
                        {
                            ProfitReportFeeData feeItem = new ProfitReportFeeData();
                            feeItem.InvNo = item.No;
                            feeItem.PostDate = item.DueDate.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            feeItem.company = item.CustomerName;
                            feeItem.ChargeItemDescription = fee.ChargingCode;
                            feeItem.pmt = fee.PayAmount == fee.Amount ? "Y" : "N";
                            decimal money = 0m; //保存换算后的金额
                            if (item.Type == BillType.AR)
                            {
                                money = Utility.GetAmountByRate(fee.Amount, fee.CurrencyID, configureInfo.DefaultCurrencyID, rateList);
                                feeItem.Revenue = fee.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                                feeItem.Cost = 0.00m.ToString("n");
                                feeItem.agent = 0.00m.ToString("n");
                                totalRevenue += fee.Way == FeeWay.AR ? money : -money;
                            }
                            else if (item.Type == BillType.AP)
                            {
                                money = Utility.GetAmountByRate(fee.Amount, fee.CurrencyID, configureInfo.DefaultCurrencyID, rateList);
                                feeItem.Revenue = 0.00m.ToString("n");
                                feeItem.Cost = fee.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                                feeItem.agent = 0.00m.ToString("n");
                                totalCost += fee.Way == FeeWay.AR ? money : -money;
                            }
                            else if (item.Type == BillType.DC)
                            {
                                money = Utility.GetAmountByRate(fee.Amount, fee.CurrencyID, configureInfo.DefaultCurrencyID, rateList);
                                feeItem.Revenue = 0.00m.ToString("n");
                                feeItem.Cost = 0.00m.ToString("n");
                                feeItem.agent = fee.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                                totalAgent += fee.Way == FeeWay.AR ? money : -money;
                            }

                            reportData.Fees.Add(feeItem);
                        }
                    }
                }

                reportData.BaseReportData.TotalRevenue = totalRevenue.ToString("n");
                reportData.BaseReportData.TotalCost = totalCost.ToString("n");
                reportData.BaseReportData.TotalAgent = totalAgent.ToString("n");
                reportData.BaseReportData.Profit = (totalRevenue + totalCost + totalAgent).ToString("n");
            }

            string fileName = GetOIReportPath();
            fileName += "OI_Profit.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", reportData.BaseReportData);
            if (reportData.Fees != null && reportData.Fees.Count > 0)
            {
                reportSource.Add("FeeListReportSource", reportData.Fees);
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Profit Print" : "利润打印", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            CustomerInfo customerinfo = GetCustomerInfo(businessListItem.ID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            //邮件信息业务关联
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = businessListItem.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = businessListItem.ID;
            viewer.BindData(fileName, reportSource, null, message);
            return viewer;
        }

        /// <summary>
        /// 打印工作表
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintWorkSheet(OceanBusinessList businessListItem, ICP.Message.ServiceInterface.Message operationInfo)
        {
            OceanBusinessInfo businessInfo = OceanImportService.GetBusinessInfo(businessListItem.ID);
            string mblString = businessListItem.MBLNo;
            string vesselVoyageString = businessListItem.VesselVoyage;
            if (string.IsNullOrEmpty(businessListItem.MBLNo) && !ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(businessInfo.MBLID))
            {
                businessInfo.MBLInfo = OceanImportService.GetOIMBLInfo(businessInfo.MBLID.Value);
                mblString = businessInfo.MBLInfo.MBLNo;
                vesselVoyageString = businessInfo.MBLInfo.VoyageName;
            }

            WorkSheetReportData reportData = new WorkSheetReportData();
            reportData.BaseReportData = new WorkSheetBaseReportData();
            reportData.BaseReportData.PrintDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.PrintByName = LocalData.UserInfo.LoginName;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            reportData.BaseReportData.DefaultCurrency = configureInfo.DefaultCurrency;
            reportData.BaseReportData.ReferenceNO = businessInfo.No;
            reportData.BaseReportData.MasterBLNo = mblString;
            reportData.BaseReportData.AgentName = businessInfo.AgentName;
            reportData.BaseReportData.VesselVoyageNo = vesselVoyageString;
            reportData.BaseReportData.LoadPortName = businessInfo.POLName;
            reportData.BaseReportData.ETD = businessInfo.ETD == null ? string.Empty : businessInfo.ETD.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DiscPortName = businessInfo.PODName;
            reportData.BaseReportData.ETA = businessInfo.ETA == null ? string.Empty : businessInfo.ETA.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DeliveryName = businessInfo.TransportClauseName;
            reportData.BaseReportData.OperatorName = businessInfo.FilerName;
            List<BillList> billList = FinanceService.GetBillListByOperactioID(businessListItem.ID);
            if (billList != null && billList.Count > 0)
            {
                Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
                reportData.BillList = new List<BillReportData>();
                foreach (var item in billList)
                {
                    if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(item.ID) || item.CurrencyAmounts == null || item.CurrencyAmounts.Count == 0) continue;

                    foreach (var fItem in item.CurrencyAmounts)
                    {
                        if (dic.Keys.Contains(fItem.CurrencyID) == false) dic.Add(fItem.CurrencyID, 0m);
                        dic[fItem.CurrencyID] += fItem.Amount;
                    }

                    BillReportData bill = new BillReportData();
                    bill.Type = item.Type.ToString();
                    bill.BillNo = item.No;
                    bill.HouseBLNo = item.FormNo;
                    bill.PostDate = item.DueDate.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    bill.Company = item.CustomerName;
                    if (item.Type == BillType.AR)
                    {
                        bill.Revenue = item.CurrencyAmounts[0].Amount.ToString("n");
                        bill.Cost = 0.00m.ToString("n");
                        bill.Agent = 0.00m.ToString("n");
                        bill.Amount = item.CurrencyAmounts[0].Amount.ToString("n");
                    }
                    else if (item.Type == BillType.AP)
                    {
                        bill.Revenue = 0.00m.ToString("n");
                        bill.Cost = item.CurrencyAmounts[0].Amount.ToString("n");
                        bill.Agent = 0.00m.ToString("n");
                        bill.Amount = item.CurrencyAmounts[0].Amount.ToString("n");
                    }
                    else if (item.Type == BillType.DC)
                    {
                        bill.Revenue = 0.00m.ToString("n");
                        bill.Cost = 0.00m.ToString("n");
                        bill.Agent = item.CurrencyAmounts[0].Amount.ToString("n");
                        bill.Amount = item.CurrencyAmounts[0].Amount.ToString("n");
                    }

                    reportData.BillList.Add(bill);
                }

                decimal profitAmount = 0m;
                List<SolutionExchangeRateList> rateList = ConfigureService.GetCompanyExchangeRateList(businessInfo.CompanyID, true);
                foreach (var item in dic)
                {
                    profitAmount += Utility.GetAmountByRate(item.Value, item.Key, configureInfo.DefaultCurrencyID, rateList);
                }

                reportData.BaseReportData.TotalAmount = profitAmount.ToString("n");
            }

            string fileName = GetOIReportPath();
            fileName += "OI_WorkSheet.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", reportData.BaseReportData);
            if (reportData.BillList != null)
            {
                reportSource.Add("BillListReportSource", reportData.BillList);
            }
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Work Sheet Print" : "打印工作表", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            CustomerInfo customerinfo = GetCustomerInfo(businessListItem.ID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            //邮件传真业务关联
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = businessListItem.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = businessListItem.ID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        /// <summary>
        /// 打印巴西到港通知
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintArrivalNoticeReportForBrazil(OceanBusinessList businessListItem, ICP.Message.ServiceInterface.Message operationInfo)
        {
            ArrivalNoticeReportDataForBrazil data = OIReportDataService.GetOIArrivalNoticeReportDataForBrazil(businessListItem.ID);

            DirectoryInfo dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\Reports\\BrazilLOGO\\");
            FileInfo[] files = dir.GetFiles();
            data.LogoPath = files[0].FullName;

            string fileName = GetOIReportPath();
            fileName += "RptOIArrivalNoticeForBrazil.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("AGTArrivalNotice_ArrivalNotice", data);

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Arrival Notice" : "到港通知书", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            //邮件传真业务关联
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();

            if (businessListItem.SalesID != null)
            {
                Guid salesID =(Guid)businessListItem.SalesID;
                UserInfo userInfo = ServiceClient.GetService<IUserService>().GetUserInfo(salesID);
                string email = userInfo == null ? string.Empty : userInfo.EMail;
                message.CC = email;
            }

            CustomerInfo customerinfo = GetCustomerInfo(businessListItem.ID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = businessListItem.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = businessListItem.ID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        /// <summary>
        /// 打印出口业务信息报表(好像是服务器生成的，先不做)
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public void PrintExportBusinessInfo(string customerName, Guid customerID)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            WorkItem tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>();
            string spaceName = Guid.NewGuid().ToString();
            ReportMainSpace reportMainSpace = tempWorkitem.SmartParts.AddNew<ReportMainSpace>(spaceName);

            ICP.ReportCenter.UI.ReportViewBase reportViewBase = tempWorkitem.SmartParts.AddNew<ICP.ReportCenter.UI.ReportViewBase>();
            IWorkspace reportWorkspace = (IWorkspace)tempWorkitem.Workspaces[ReportWorkSpaceConstants.ReportWorkspace];
            reportWorkspace.Show(reportViewBase);

            OEBussinesInfoSearchPart searchPart = tempWorkitem.SmartParts.AddNew<OEBussinesInfoSearchPart>();
            IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[ReportWorkSpaceConstants.SearchWorkspace];
            searchWorkspace.Show(searchPart);

            IWorkspace mainWorkspace = Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
            SmartPartInfo smartPartInfo = new SmartPartInfo();
            smartPartInfo.Title = LocalData.IsEnglish ? "Operation Export Info" : "出口业务信息列表";
            mainWorkspace.Show(reportMainSpace, smartPartInfo);

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);

            //客户
            searchPart.CustomerName = customerName;

            List<Guid> guids = new List<Guid>();
            guids.Add(customerID);
            searchPart.ObjCustomerID = guids;

            //初始化分组
            searchPart.InitControls();
            //刚打开时默认搜索选择的客户
            GetDate(reportViewBase, searchPart.GetData());
            //
            searchPart.OnSearched += delegate(object sender, object results)
            {
                //搜索
                GetDate(reportViewBase, results);
            };
        }

        private void GetDate(ICP.ReportCenter.UI.ReportViewBase reportViewBase, object results)
        {

            ReportData rd = results as ReportData;
            if (rd != null)
            {
                if (rd.IsLocalReport)//本地报表
                {
                    reportViewBase.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters, rd.SubDataSource);
                }
                else
                {
                    reportViewBase.ReportName = rd.ReportName;
                    reportViewBase.ParamList = rd.Parameters;
                    if (string.IsNullOrEmpty(rd.ServiceReportPath))
                        reportViewBase.DisplayData();
                    else
                        reportViewBase.DisplayData(rd.ServiceReportPath);
                }
            }
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationID">operationID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfo(Guid operationID)
        {
            OIBusinessReportData data = OIReportDataService.GetOIBusinessReportData(operationID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BusinessInfo" : "业务信息"
                                                                    , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOIReportPath() + (LocalData.IsEnglish ? "OI_BusinessInfo_EN.frx" : "OI_BusinessInfo_CN.frx");
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("OIBusinessReportData", data);
            reportSource.Add("BLListReportData", data.blListReportDatas);

            CustomerInfo customerinfo = GetCustomerInfo(operationID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            //邮件传真业务关联
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = operationID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = operationID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="blID">blID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfoCopy(Guid blID)
        {
            #region 数据源

            OIBLReportData data = OIReportDataService.GetOIBLReportData(blID);

            BLReportClientData blReportData = new BLReportClientData();
            Utility.CopyToValue(data, blReportData, typeof(BLReportClientData));

            if (string.IsNullOrEmpty(blReportData.MBLNo) == false)
            {
                blReportData.MBLNo = "MBLNO:" + blReportData.MBLNo;
            }
            if (blReportData.ETD != null)
            {
                blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }

            #endregion

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BL Info" : "提单信息"
                                                                    , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetOIReportPath() + "OI_BL_TR_Report.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", blReportData);

            CustomerInfo customerinfo = GetCustomerInfo(blID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanImport;
            message.UserProperties.OperationId = blID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.ShippingOrder;
            message.UserProperties.FormId = blID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        private CustomerInfo GetCustomerInfo(Guid operationID)
        {
            OceanBusinessInfo info = OceanImportService.GetBusinessInfo(operationID);
            CustomerInfo customerInfo = CustomerService.GetCustomerInfo(info.CustomerID);
            return customerInfo;
        }
    }
}
