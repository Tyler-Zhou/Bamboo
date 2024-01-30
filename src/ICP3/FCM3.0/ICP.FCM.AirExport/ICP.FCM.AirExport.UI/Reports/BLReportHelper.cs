using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.FCM.AirExport.ServiceInterface;

namespace ICP.FCM.AirExport.UI
{
    public class BLReportHelper
    {
       

        [ServiceDependency]
        public IAirExportService aeService { get; set; }
        /// <summary>
        /// 提单报表打印
        /// </summary>
        /// <param name="HBL">HBL对象</param>
        /// <param name="Id">ID（MBL/HBL）</param>
        /// <param name="isEnglish">是否英文环境</param>
        /// <param name="type">类型（0,代表MBL,1代表HBL）</param>
        /// <returns></returns>
        public Dictionary<string, object> Print(BLReportData_HBL HBL, Guid Id, bool isEnglish, int type, string[] reportPara)
        {
            BLReportData_HBL data = null;
            if (type == 0) { data = aeService.GetHBLReportData(Id, isEnglish); }
            else {data = aeService.GetHBLReportData(Id, isEnglish); }
            HBL.AccountInformation = data.AccountInformation;
            HBL.Agent = data.Agent;
            HBL.AgentIATACode = data.AgentIATACode;
            HBL.Airline = data.Airline;
            HBL.AmountofInsurance = data.AmountofInsurance;
            HBL.ChargeableWeight = data.ChargesAtDestination;
            HBL.CHGSCode = data.CHGSCode;
            HBL.CollectTax = data.CollectTax;
            HBL.CollectTotalotherAgentCharges = data.CollectTotalotherAgentCharges;
            HBL.CollectTotalotherCarrierCharges = data.CollectTotalotherCarrierCharges;
            HBL.CollectValuationCharge = data.CollectValuationCharge;
            HBL.CollectWeightCharge = data.CollectWeightCharge;
            HBL.CommodityItemNo = data.CommodityItemNo;
            HBL.Consignee = data.Consignee;
            HBL.ConsigneeAccountNo = data.ConsigneeAccountNo;
            HBL.Currency = data.Currency;
            HBL.CurrencyConversionRate = data.CurrencyConversionRate;
            HBL.DeclaredValueForCarriage = data.DeclaredValueForCarriage;
            HBL.DeclaredValueForCustoms = data.DeclaredValueForCustoms;
            HBL.DepartureName = data.DepartureName;
            HBL.DestinationCurrencyAmount = data.DestinationCurrencyAmount;
            HBL.DestinationName = data.DestinationName;
            HBL.FlightDate = data.FlightDate;
            HBL.GoodsDescription = data.GoodsDescription;
            HBL.GrossWeight = data.GrossWeight;
            HBL.HandlingInformation = data.HandlingInformation;
            HBL.HAWBNO = data.HAWBNO;
            HBL.IssueBy = data.IssueBy;
            HBL.IssueDate = data.IssueDate;
            HBL.IssuePlace = data.IssuePlace;
            HBL.Kglb = data.Kglb;
            HBL.MAWBNO = data.MAWBNO;
            HBL.NoofPiecesRCP = data.NoofPiecesRCP;
            HBL.NumberOfOriginal = data.NumberOfOriginal;
            HBL.OtherChargers = data.OtherChargers;
            HBL.OtherPaymentTerm = data.OtherPaymentTerm;
            HBL.PaymentTerm = data.PaymentTerm;
            HBL.PrepaidTax = data.PrepaidTax;
            HBL.PrepaidTotalotherAgentCharges = data.PrepaidTotalotherAgentCharges;
            HBL.PrepaidTotalotherCarrierCharges = data.PrepaidTotalotherCarrierCharges;
            HBL.PrepaidValuationCharge = data.PrepaidValuationCharge;
            HBL.PrepaidWeightCharge = data.PrepaidWeightCharge;
            HBL.RateCharge = data.RateCharge;
            HBL.RateClass = data.RateClass;
            HBL.Shipper = data.Shipper;
            HBL.ShipperAccountNo = data.ShipperAccountNo;
            HBL.Total = data.Total;
            HBL.TotalCollect = data.TotalCollect;
            HBL.TotalPrepaid = data.TotalPrepaid;
            HBL.TranshipmentPort1 = data.TranshipmentPort1;
            HBL.TranshipmentPort1By = data.TranshipmentPort1By;
            HBL.TranshipmentPort2 = data.TranshipmentPort2;
            HBL.TranshipmentPort2By = data.TranshipmentPort2By;
            HBL.TranshipmentPort3 = data.TranshipmentPort3;
            HBL.TranshipmentPort3By = data.TranshipmentPort3By;
            HBL.Marks = data.Marks;
            HBL.FlightNo = data.FlightNo;

            
            HBL.ReportStyle = reportPara[0];//正本//副本
            if (reportPara[0] =="副本") { HBL.Title = reportPara[1]; } else { HBL.Title = string.Empty; }
            HBL.Title = reportPara[1];
            HBL.Notify = reportPara[1];
            HBL.LogoName = reportPara[2].ToString().Trim();
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("HBL",HBL);
            return reportSource;
        }
    }
}
