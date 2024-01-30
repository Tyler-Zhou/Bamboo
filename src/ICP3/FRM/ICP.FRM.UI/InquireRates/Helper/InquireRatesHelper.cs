#region Comment

/*
 * 
 * FileName:    InquireRatesHelper.cs
 * CreatedOn:   
 * CreatedBy:    
 * 
 * 
 * Description：
 *      ->询价帮助类
 *      ->  1.海运询价客户端、服务器实体类互换 ClientInquierOceanRate 与 InquierOceanRate
 *      ->  2.空运询价客户端、服务器实体类互换 ClientInquierAirRate 与 InquierAirRate
 *      ->  3.拖车询价客户端、服务器实体类互换 ClientInquierTruckingRate 与 InquierTruckingRate
 *      ->询价UI数据帮助类
 *      ->  1.通用查询数据服务定义
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System.Linq;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 询价帮助类
    /// </summary>
    public class InquireRatesHelper
    {
        #region Sub To Base
        public static BaseInquireRate BuildInquireOceanContent(InquierOceanRate item)
        {
            BaseInquireRate baseItem = new BaseInquireRate();
            baseItem.ID = item.ID;
            baseItem.No = item.No;
            baseItem.RespondByID = item.RespondByID;
            baseItem.RespondByName = item.RespondByName;
            //Exp
            baseItem.ExpCarrierName = item.ExpCarrierName;
            baseItem.ExpCommodity = item.ExpCommodity;

            baseItem.CurrencyName = item.CurrencyName;
            baseItem.IsWillBooking = item.IsWillBooking;
            baseItem.CustomerID = item.CustomerID;
            baseItem.CustomerName = item.CustomerName;
            baseItem.POLName = item.POLName;
            baseItem.PODName = item.PODName;
            baseItem.CarrierName = item.CarrierName;
            baseItem.Commodity = item.Commodity;
            baseItem.TransportClauseName = item.TransportClauseName;
            baseItem.TotalSurcharge = item.TotalSurcharge;
            baseItem.DurationFrom = item.DurationFrom;
            baseItem.DurationTo = item.DurationTo;

            //InquireBysList
            baseItem.InquireBysList = item.InquireBysList;
            //UnitRates
            baseItem.UnitRates = item.UnitRates;
            if (baseItem.UnitRates != null && baseItem.UnitRates.Count > 0)
            {
                baseItem.UnitRateString = item.UnitRates.Aggregate("",
                                (current, itemUnit) => current + (itemUnit.UnitName + "=" + itemUnit.Rate.ToString() + " , "));
            }
            return baseItem;
        } 
        #endregion

        #region Service Object 2 Client Object

        #region Inquier Ocean Rate

        /// <summary>
        /// 构建ClientInquierOceanRate
        /// 传入InquierOceanRate、List InquireUnit 、Guid主键构建ClientInquierOceanRate
        /// </summary>
        /// <param name="orgList">原始InquierOceanRate模型</param>
        /// <param name="units">箱型集合</param>
        /// <param name="defaultCurrencyID">默认Guid</param>
        /// <returns>海运询价模型</returns>
        public static ClientInquierOceanRate TransformS2C(InquierOceanRate orgList, List<InquireUnit> units, Guid? defaultCurrencyID)
        {
            if (orgList == null) return null;

            ClientInquierOceanRate newItem = new ClientInquierOceanRate();
            newItem.ID = orgList.ID;
            newItem.MainRecordID = orgList.MainRecordID;
            newItem.No = string.IsNullOrEmpty(orgList.No) ? string.Empty : orgList.No;
            newItem.HasUnconfirmedShipment = orgList.HasUnconfirmedShipment;
            newItem.HasUnRead = orgList.HasUnRead;
            newItem.IsNoPriceAll = orgList.IsNoPriceAll;
            //if (orgList.IsNoPriceAll)
            //{
            //    newItem.Shared = orgList.Shared;
            //}
            //else
            //{             
            //    newItem.Shared = true;
            //}
            newItem.Shared = orgList.Shared;

            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.DurationFrom = orgList.DurationFrom;
            newItem.DurationTo = orgList.DurationTo;
            newItem.Commodity = orgList.Commodity;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            if (orgList.CurrencyID == null)
            {
                newItem.CurrencyID = defaultCurrencyID;
            }
            else
            {
                newItem.CurrencyID = orgList.CurrencyID;
                newItem.CurrencyName = orgList.CurrencyName;
            }

            newItem.SurCharge = orgList.SurCharge;
            newItem.Remark = orgList.Remark;
            newItem.ShippingLineID = orgList.ShippingLineID;
            newItem.ShippingLineName = orgList.ShippingLineName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            //newItem.ExpCarrierID = orgList.ExpCarrierID;
            newItem.ExpCarrierName = orgList.ExpCarrierName;
            newItem.ExpCommodity = orgList.ExpCommodity;
            newItem.ExpTransportClauseID = orgList.ExpTransportClauseID;
            newItem.ExpTransportClauseName = orgList.ExpTransportClauseName;
            newItem.CargoWeight = orgList.CargoWeight;
            newItem.Measurement = orgList.Measurement;
            newItem.CargoReady = orgList.CargoReady;
            newItem.EstimateTimeOfDelivery = orgList.EstimateTimeOfDelivery;
            newItem.IsWillBooking = orgList.IsWillBooking;
            newItem.ExpPrice = orgList.ExpPrice;
            newItem.DiscussingWhenNew = orgList.DiscussingWhenNew;
            newItem.InquireByID = orgList.InquireByID;
            newItem.InquireByName = orgList.InquireByName;
            newItem.RespondByID = orgList.RespondByID;
            newItem.RespondByName = orgList.RespondByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.DTime = orgList.DTime;
            if (units != null)
                newItem.BulidRateToZeroByOceanUints(units);
            newItem.RateUnitList = orgList.UnitRates;

            BuildRates(newItem, orgList.UnitRates);

            newItem.InquirePriceInquireBysList = orgList.InquireBysList;

            return newItem;
        }

        public static void BuildRates(ClientInquierOceanRate newItem, List<InquireUnit> unitRates)
        {
            if (unitRates != null)
            {
                foreach (var item in unitRates)
                {
                    switch (item.UnitName)
                    {
                        case "45FR": newItem.Rate_45FR = item.Rate; break;
                        case "40RF": newItem.Rate_40RF = item.Rate; break;
                        case "45HT": newItem.Rate_45HT = item.Rate; break;
                        case "20RF": newItem.Rate_20RF = item.Rate; break;
                        case "20HQ": newItem.Rate_20HQ = item.Rate; break;
                        case "20TK": newItem.Rate_20TK = item.Rate; break;
                        case "20GP": newItem.Rate_20GP = item.Rate; break;
                        case "40TK": newItem.Rate_40TK = item.Rate; break;
                        case "40OT": newItem.Rate_40OT = item.Rate; break;
                        case "20FR": newItem.Rate_20FR = item.Rate; break;
                        case "45GP": newItem.Rate_45GP = item.Rate; break;
                        case "40GP": newItem.Rate_40GP = item.Rate; break;
                        case "45RF": newItem.Rate_45RF = item.Rate; break;
                        case "20RH": newItem.Rate_20RH = item.Rate; break;
                        case "45OT": newItem.Rate_45OT = item.Rate; break;
                        case "40NOR": newItem.Rate_40NOR = item.Rate; break;
                        case "40FR": newItem.Rate_40FR = item.Rate; break;
                        case "20OT": newItem.Rate_20OT = item.Rate; break;
                        case "45TK": newItem.Rate_45TK = item.Rate; break;
                        case "20NOR": newItem.Rate_20NOR = item.Rate; break;
                        case "40HT": newItem.Rate_40HT = item.Rate; break;
                        case "40RH": newItem.Rate_40RH = item.Rate; break;
                        case "45RH": newItem.Rate_45RH = item.Rate; break;
                        case "45HQ": newItem.Rate_45HQ = item.Rate; break;
                        case "20HT": newItem.Rate_20HT = item.Rate; break;
                        case "40HQ": newItem.Rate_40HQ = item.Rate; break;
                    }
                }
            }
        }

        public static List<ClientInquierOceanRate> TransformS2C(List<InquierOceanRate> orgList, List<InquireUnit> units, Guid? defaultCurrencyID)
        {
            if (orgList == null) return null;

            List<ClientInquierOceanRate> list = new List<ClientInquierOceanRate>();
            foreach (var item in orgList)
            {
                ClientInquierOceanRate clientItem = TransformS2C(item, units, defaultCurrencyID);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }

        #endregion

        #region Inquier Air Rate

        public static ClientInquierAirRate TransformS2C(InquierAirRate orgList, List<InquireUnit> units, Guid? defaultCurrencyID)
        {
            if (orgList == null) return null;

            ClientInquierAirRate newItem = new ClientInquierAirRate();
            newItem.ID = orgList.ID;
            newItem.MainRecordID = orgList.MainRecordID;
            newItem.HasUnRead = orgList.HasUnRead;
            newItem.IsNoPriceAll = orgList.IsNoPriceAll;
            //if (orgList.IsNoPriceAll)
            //{
            //    newItem.Shared = orgList.Shared;
            //}
            //else
            //{
            //    newItem.Shared = true;
            //}
            newItem.Shared = orgList.Shared;

            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.DurationFrom = orgList.DurationFrom;
            newItem.DurationTo = orgList.DurationTo;
            newItem.Commodity = orgList.Commodity;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            if (orgList.CurrencyID == null)
            {
                newItem.CurrencyID = defaultCurrencyID;
            }
            else
            {
                newItem.CurrencyID = orgList.CurrencyID;
                newItem.CurrencyName = orgList.CurrencyName;
            }

            newItem.Schedule = orgList.Schedule;
            newItem.Routing = orgList.Routing;
            newItem.MAWB = orgList.MAWB;
            newItem.HAWB = orgList.HAWB;
            newItem.CartonsOrPallets = orgList.CartonsOrPallets;
            newItem.Remark = orgList.Remark;
            newItem.ShippingLineID = orgList.ShippingLineID;
            newItem.ShippingLineName = orgList.ShippingLineName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.ExpCarrierName = orgList.ExpCarrierName;
            newItem.ExpCommodity = orgList.ExpCommodity;
            newItem.ExpTransportClauseID = orgList.ExpTransportClauseID;
            newItem.ExpTransportClauseName = orgList.ExpTransportClauseName;
            newItem.CargoWeight = orgList.CargoWeight;
            newItem.Measurement = orgList.Measurement;
            newItem.CargoReady = orgList.CargoReady;
            newItem.EstimateTimeOfDelivery = orgList.EstimateTimeOfDelivery;
            newItem.IsWillBooking = orgList.IsWillBooking;
            newItem.ExpPrice = orgList.ExpPrice;
            newItem.DiscussingWhenNew = orgList.DiscussingWhenNew;
            newItem.InquireByID = orgList.InquireByID;
            newItem.InquireByName = orgList.InquireByName;
            newItem.RespondByID = orgList.RespondByID;
            newItem.RespondByName = orgList.RespondByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.DTime = orgList.DTime;
            newItem.BulidRateToZeroByOceanUints(units);
            newItem.RateUnitList = orgList.UnitRates;

            #region BulidRate

            if (orgList.UnitRates != null)
            {
                foreach (var item in orgList.UnitRates)
                {
                    switch (item.UnitName)
                    {
                        case "MIN": newItem.Rate_MIN = item.Rate; break;
                        case "+45": newItem.Rate_45 = item.Rate; break;
                        case "+100": newItem.Rate_100 = item.Rate; break;
                        case "+300": newItem.Rate_300 = item.Rate; break;
                        case "+500": newItem.Rate_500 = item.Rate; break;
                        case "+800": newItem.Rate_800 = item.Rate; break;
                        case "+1000": newItem.Rate_1000 = item.Rate; break;
                        case "+1300": newItem.Rate_1300 = item.Rate; break;
                    }
                }
            }

            #endregion

            newItem.InquirePriceInquireBysList = orgList.InquireBysList;
            return newItem;
        }

        public static List<ClientInquierAirRate> TransformS2C(List<InquierAirRate> orgList, List<InquireUnit> units, Guid? defaultCurrencyID)
        {
            if (orgList == null) return null;

            List<ClientInquierAirRate> list = new List<ClientInquierAirRate>();
            foreach (var item in orgList)
            {
                ClientInquierAirRate clientItem = TransformS2C(item, units, defaultCurrencyID);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }

        #endregion

        #region Inquier Trucking Rate

        public static ClientInquierTruckingRate TransformS2C(InquierTruckingRate orgList, List<InquireUnit> units, Guid? defaultCurrencyID)
        {
            if (orgList == null) return null;

            ClientInquierTruckingRate newItem = new ClientInquierTruckingRate();
            newItem.ID = orgList.ID;
            newItem.No = orgList.No;
            newItem.MainRecordID = orgList.MainRecordID;
            newItem.HasUnRead = orgList.HasUnRead;
            newItem.IsNoPriceAll = orgList.IsNoPriceAll;
            //if (orgList.IsNoPriceAll)
            //{
            //    newItem.Shared = orgList.Shared;
            //}
            //else
            //{
            //    newItem.Shared = true;
            //}
            newItem.Shared = orgList.Shared;

            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.DurationFrom = orgList.DurationFrom;
            newItem.DurationTo = orgList.DurationTo;
            newItem.Commodity = orgList.Commodity;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            if (orgList.CurrencyID == null)
            {
                newItem.CurrencyID = defaultCurrencyID;
            }
            else
            {
                newItem.CurrencyID = orgList.CurrencyID;
                newItem.CurrencyName = orgList.CurrencyName;
            }

            newItem.Rate = orgList.Rate;
            newItem.FUEL = orgList.FUEL;
            newItem.Total = orgList.Total;
            newItem.ZipCode = orgList.ZipCode;
            newItem.CartonsOrPallets = orgList.CartonsOrPallets;
            newItem.Remark = orgList.Remark;
            newItem.ShippingLineID = orgList.ShippingLineID;
            newItem.ShippingLineName = orgList.ShippingLineName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.ExpCarrierName = orgList.ExpCarrierName;
            newItem.ExpCommodity = orgList.ExpCommodity;
            newItem.ExpTransportClauseID = orgList.ExpTransportClauseID;
            newItem.ExpTransportClauseName = orgList.ExpTransportClauseName;
            newItem.CargoWeight = orgList.CargoWeight;
            newItem.Measurement = orgList.Measurement;
            newItem.CargoReady = orgList.CargoReady;
            newItem.EstimateTimeOfDelivery = orgList.EstimateTimeOfDelivery;
            newItem.IsWillBooking = orgList.IsWillBooking;
            newItem.ExpPrice = orgList.ExpPrice;
            newItem.DiscussingWhenNew = orgList.DiscussingWhenNew;
            if (orgList.InquireBysList != null && orgList.InquireBysList.Count > 0)
            {
                newItem.InquireByID = orgList.InquireBysList[0].InquireByID;
                newItem.InquireByName = orgList.InquireBysList[0].InquireByCName;
                newItem.InquirePriceInquireBysList = orgList.InquireBysList;
            }
            else
            {
                newItem.InquireByID = orgList.InquireByID;
                newItem.InquireByName = orgList.InquireByName;
            }
            newItem.RespondByID = orgList.RespondByID;
            newItem.RespondByName = orgList.RespondByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.DTime = orgList.DTime;
            //newItem.BulidRateToZeroByOceanUints(units);
            newItem.RateUnitList = orgList.UnitRates;

            #region BulidRate

            //foreach (var item in orgList.UnitRates)
            //{
            //    switch (item.UnitName)
            //    {
            //        case "45FR": newItem.Rate_45FR = item.Rate; break;
            //        case "40RF": newItem.Rate_40RF = item.Rate; break;
            //        case "45HT": newItem.Rate_45HT = item.Rate; break;
            //        case "20RF": newItem.Rate_20RF = item.Rate; break;
            //        case "20HQ": newItem.Rate_20HQ = item.Rate; break;
            //        case "20TK": newItem.Rate_20TK = item.Rate; break;
            //        case "20GP": newItem.Rate_20GP = item.Rate; break;
            //        case "40TK": newItem.Rate_40TK = item.Rate; break;
            //        case "40OT": newItem.Rate_40OT = item.Rate; break;
            //        case "20FR": newItem.Rate_20FR = item.Rate; break;
            //        case "45GP": newItem.Rate_45GP = item.Rate; break;
            //        case "40GP": newItem.Rate_40GP = item.Rate; break;
            //        case "45RF": newItem.Rate_45RF = item.Rate; break;
            //        case "20RH": newItem.Rate_20RH = item.Rate; break;
            //        case "45OT": newItem.Rate_45OT = item.Rate; break;
            //        case "40NOR": newItem.Rate_40NOR = item.Rate; break;
            //        case "40FR": newItem.Rate_40FR = item.Rate; break;
            //        case "20OT": newItem.Rate_20OT = item.Rate; break;
            //        case "45TK": newItem.Rate_45TK = item.Rate; break;
            //        case "20NOR": newItem.Rate_20NOR = item.Rate; break;
            //        case "40HT": newItem.Rate_40HT = item.Rate; break;
            //        case "40RH": newItem.Rate_40RH = item.Rate; break;
            //        case "45RH": newItem.Rate_45RH = item.Rate; break;
            //        case "45HQ": newItem.Rate_45HQ = item.Rate; break;
            //        case "20HT": newItem.Rate_20HT = item.Rate; break;
            //        case "40HQ": newItem.Rate_40HQ = item.Rate; break;
            //    }
            //}
            #endregion

            return newItem;
        }

        public static List<ClientInquierTruckingRate> TransformS2C(List<InquierTruckingRate> orgList, List<InquireUnit> units, Guid? defaultCurrencyID)
        {
            if (orgList == null) return null;

            List<ClientInquierTruckingRate> list = new List<ClientInquierTruckingRate>();
            foreach (var item in orgList)
            {
                ClientInquierTruckingRate clientItem = TransformS2C(item, units, defaultCurrencyID);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }

        #endregion

        #endregion

        #region  Client Object 2 Service Object

        #region Inquier Ocean Rate
        /// <summary>
        /// 将客户端实体类(ClientInquierOceanRate)转换为服务端实体类(InquierOceanRate)
        /// </summary>
        /// <param name="orgList">客户端实体类(ClientInquierOceanRate)</param>
        /// <returns>服务端实体类(InquierOceanRate)</returns>
        public static InquierOceanRate TransformC2S(ClientInquierOceanRate orgList)
        {
            return TransformC2S(orgList, false);
        }

        /// <summary>
        /// 将客户端实体类(ClientInquierOceanRate)转换为服务端实体类(InquierOceanRate)
        /// </summary>
        /// <param name="orgList">客户端实体类(ClientInquierOceanRate)</param>
        /// <param name="isTransSubset">是否转换子集</param>
        /// <returns>服务端实体类(InquierOceanRate)</returns>
        public static InquierOceanRate TransformC2S(ClientInquierOceanRate orgList,bool isTransSubset)
        {
            if (orgList == null) return null;

            InquierOceanRate newItem = new InquierOceanRate();
            newItem.ID = orgList.ID;
            newItem.No = orgList.No;
            newItem.MainRecordID = orgList.MainRecordID;
            newItem.HasUnRead = orgList.HasUnRead;
            newItem.IsNoPriceAll = orgList.IsNoPriceAll;
            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.DurationFrom = orgList.DurationFrom;
            newItem.DurationTo = orgList.DurationTo;
            newItem.Commodity = orgList.Commodity;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            newItem.CurrencyID = orgList.CurrencyID;
            newItem.CurrencyName = orgList.CurrencyName;
            newItem.SurCharge = orgList.SurCharge;
            newItem.Remark = orgList.Remark;
            newItem.Shared = orgList.Shared;
            newItem.ShippingLineID = orgList.ShippingLineID;
            newItem.ShippingLineName = orgList.ShippingLineName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.ExpCarrierName = orgList.ExpCarrierName;
            newItem.ExpCommodity = orgList.ExpCommodity;
            newItem.ExpTransportClauseID = orgList.ExpTransportClauseID;
            newItem.ExpTransportClauseName = orgList.ExpTransportClauseName;
            newItem.CargoWeight = orgList.CargoWeight;
            newItem.Measurement = orgList.Measurement;
            newItem.CargoReady = orgList.CargoReady;
            newItem.EstimateTimeOfDelivery = orgList.EstimateTimeOfDelivery;
            newItem.IsWillBooking = orgList.IsWillBooking;
            newItem.ExpPrice = orgList.ExpPrice;
            newItem.DiscussingWhenNew = orgList.DiscussingWhenNew;
            newItem.InquireByID = orgList.InquireByID;
            newItem.InquireByName = orgList.InquireByName;
            newItem.RespondByID = orgList.RespondByID;
            newItem.RespondByName = orgList.RespondByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.UnitRates = new List<InquireUnit>();
            newItem.TotalSurcharge = newItem.SurCharge;
            foreach (var item in orgList.RateUnitList)
            {
                InquireUnit unit = new InquireUnit();
                unit.ID = item.ID;
                unit.UnitID = item.UnitID;
                unit.UnitName = item.UnitName;
                newItem.UnitRates.Add(unit);

                #region BulidRate
                switch (item.UnitName)
                {
                    case "45FR": unit.Rate = orgList.Rate_45FR ?? 0m; break;
                    case "40RF": unit.Rate = orgList.Rate_40RF ?? 0m; break;
                    case "45HT": unit.Rate = orgList.Rate_45HT ?? 0m; break;
                    case "20RF": unit.Rate = orgList.Rate_20RF ?? 0m; break;
                    case "20HQ": unit.Rate = orgList.Rate_20HQ ?? 0m; break;
                    case "20TK": unit.Rate = orgList.Rate_20TK ?? 0m; break;
                    case "20GP": unit.Rate = orgList.Rate_20GP ?? 0m; break;
                    case "40TK": unit.Rate = orgList.Rate_40TK ?? 0m; break;
                    case "40OT": unit.Rate = orgList.Rate_40OT ?? 0m; break;
                    case "20FR": unit.Rate = orgList.Rate_20FR ?? 0m; break;
                    case "45GP": unit.Rate = orgList.Rate_45GP ?? 0m; break;
                    case "40GP": unit.Rate = orgList.Rate_40GP ?? 0m; break;
                    case "45RF": unit.Rate = orgList.Rate_45RF ?? 0m; break;
                    case "20RH": unit.Rate = orgList.Rate_20RH ?? 0m; break;
                    case "45OT": unit.Rate = orgList.Rate_45OT ?? 0m; break;
                    case "40NOR": unit.Rate = orgList.Rate_40NOR ?? 0m; break;
                    case "40FR": unit.Rate = orgList.Rate_40FR ?? 0m; break;
                    case "20OT": unit.Rate = orgList.Rate_20OT ?? 0m; break;
                    case "45TK": unit.Rate = orgList.Rate_45TK ?? 0m; break;
                    case "20NOR": unit.Rate = orgList.Rate_20NOR ?? 0m; break;
                    case "40HT": unit.Rate = orgList.Rate_40HT ?? 0m; break;
                    case "40RH": unit.Rate = orgList.Rate_40RH ?? 0m; break;
                    case "45RH": unit.Rate = orgList.Rate_45RH ?? 0m; break;
                    case "45HQ": unit.Rate = orgList.Rate_45HQ ?? 0m; break;
                    case "20HT": unit.Rate = orgList.Rate_20HT ?? 0m; break;
                    case "40HQ": unit.Rate = orgList.Rate_40HQ ?? 0m; break;
                }

                #endregion
            }
            if (isTransSubset)
            {
                newItem.InquireBysList = orgList.InquirePriceInquireBysList;
            }

            return newItem;

        }

        /// <summary>
        /// 将客户端实体集合(ClientInquierOceanRate List)转换为服务端实体集(InquierOceanRate List)
        /// </summary>
        /// <param name="orgList">客户端实体集合(ClientInquierOceanRate List)</param>
        /// <returns>服务端实体集(InquierOceanRate List)</returns>
        public static List<InquierOceanRate> TransformC2S(List<ClientInquierOceanRate> orgList)
        {
            return TransformC2S(orgList, false);
        }

        /// <summary>
        /// 将客户端实体集合(ClientInquierOceanRate List)转换为服务端实体集(InquierOceanRate List)
        /// </summary>
        /// <param name="orgList">客户端实体集合(ClientInquierOceanRate List)</param>
        /// <param name="isTransSubset">是否转换子集</param>
        /// <returns>服务端实体集(InquierOceanRate List)</returns>
        public static List<InquierOceanRate> TransformC2S(List<ClientInquierOceanRate> orgList, bool isTransSubset)
        {
            if (orgList == null) return null;

            return orgList.Select(item => TransformC2S
                (item, isTransSubset)).Where(oceanItem => oceanItem != null).ToList();
        }

        #endregion

        #region Inquier Air Rate

        public static InquierAirRate TransformC2S(ClientInquierAirRate orgList)
        {
            return TransformC2S(orgList, false);
        }

        public static InquierAirRate TransformC2S(ClientInquierAirRate orgList, bool isTransSubset)
        {
            if (orgList == null) return null;

            InquierAirRate newItem = new InquierAirRate();
            newItem.ID = orgList.ID;
            newItem.MainRecordID = orgList.MainRecordID;
            newItem.HasUnRead = orgList.HasUnRead;
            newItem.IsNoPriceAll = orgList.IsNoPriceAll;
            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.DurationFrom = orgList.DurationFrom;
            newItem.DurationTo = orgList.DurationTo;
            newItem.Commodity = orgList.Commodity;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            newItem.CurrencyID = orgList.CurrencyID;
            newItem.CurrencyName = orgList.CurrencyName;
            newItem.Schedule = orgList.Schedule;
            newItem.Routing = orgList.Routing;
            newItem.MAWB = orgList.MAWB;
            newItem.HAWB = orgList.HAWB;
            newItem.CartonsOrPallets = orgList.CartonsOrPallets;
            newItem.Remark = orgList.Remark;
            newItem.Shared = orgList.Shared;
            newItem.ShippingLineID = orgList.ShippingLineID;
            newItem.ShippingLineName = orgList.ShippingLineName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.ExpCarrierName = orgList.ExpCarrierName;
            newItem.ExpCommodity = orgList.ExpCommodity;
            newItem.ExpTransportClauseID = orgList.ExpTransportClauseID;
            newItem.ExpTransportClauseName = orgList.ExpTransportClauseName;
            newItem.CargoWeight = orgList.CargoWeight;
            newItem.Measurement = orgList.Measurement;
            newItem.CargoReady = orgList.CargoReady;
            newItem.EstimateTimeOfDelivery = orgList.EstimateTimeOfDelivery;
            newItem.IsWillBooking = orgList.IsWillBooking;
            newItem.ExpPrice = orgList.ExpPrice;
            newItem.DiscussingWhenNew = orgList.DiscussingWhenNew;
            newItem.InquireByID = orgList.InquireByID;
            newItem.InquireByName = orgList.InquireByName;
            newItem.RespondByID = orgList.RespondByID;
            newItem.RespondByName = orgList.RespondByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.UnitRates = new List<InquireUnit>();

            foreach (var item in orgList.RateUnitList)
            {
                InquireUnit unit = new InquireUnit();
                unit.ID = item.ID;
                unit.UnitID = item.UnitID;
                unit.UnitName = item.UnitName;
                newItem.UnitRates.Add(unit);

                #region BulidRate
                switch (item.UnitName)
                {
                    case "MIN": unit.Rate = orgList.Rate_MIN ?? 0m; break;
                    case "+45": unit.Rate = orgList.Rate_45 ?? 0m; break;
                    case "+100": unit.Rate = orgList.Rate_100 ?? 0m; break;
                    case "+300": unit.Rate = orgList.Rate_300 ?? 0m; break;
                    case "+500": unit.Rate = orgList.Rate_500 ?? 0m; break;
                    case "+800": unit.Rate = orgList.Rate_800 ?? 0m; break;
                    case "+1000": unit.Rate = orgList.Rate_1000 ?? 0m; break;
                    case "+1300": unit.Rate = orgList.Rate_1300 ?? 0m; break;
                }

                #endregion
            }
            if (isTransSubset)
            {
                newItem.InquireBysList = orgList.InquirePriceInquireBysList;
            }
            return newItem;

        }

        public static List<InquierAirRate> TransformC2S(List<ClientInquierAirRate> orgList)
        {
            if (orgList == null) return null;

            List<InquierAirRate> list = new List<InquierAirRate>();
            foreach (var item in orgList)
            {
                InquierAirRate AirItem = TransformC2S(item);
                if (AirItem != null) list.Add(AirItem);
            }

            return list;
        }

        #endregion

        #region Inquier Trucking Rate

        public static InquierTruckingRate TransformC2S(ClientInquierTruckingRate orgList)
        {
            if (orgList == null) return null;

            InquierTruckingRate newItem = new InquierTruckingRate();
            newItem.ID = orgList.ID;
            newItem.No = orgList.No;
            newItem.MainRecordID = orgList.MainRecordID;
            newItem.HasUnRead = orgList.HasUnRead;
            newItem.IsNoPriceAll = orgList.IsNoPriceAll;
            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.DurationFrom = orgList.DurationFrom;
            newItem.DurationTo = orgList.DurationTo;
            newItem.Commodity = orgList.Commodity;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            newItem.CurrencyID = orgList.CurrencyID;
            newItem.CurrencyName = orgList.CurrencyName;
            newItem.Rate = orgList.Rate;
            newItem.FUEL = orgList.FUEL;
            newItem.Total = orgList.Total;
            newItem.ZipCode = orgList.ZipCode;
            newItem.CartonsOrPallets = orgList.CartonsOrPallets;
            newItem.Remark = orgList.Remark;
            newItem.Shared = orgList.Shared;
            newItem.ShippingLineID = orgList.ShippingLineID;
            newItem.ShippingLineName = orgList.ShippingLineName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.ExpCarrierName = orgList.ExpCarrierName;
            newItem.ExpCommodity = orgList.ExpCommodity;
            newItem.ExpTransportClauseID = orgList.ExpTransportClauseID;
            newItem.ExpTransportClauseName = orgList.ExpTransportClauseName;
            newItem.CargoWeight = orgList.CargoWeight;
            newItem.Measurement = orgList.Measurement;
            newItem.CargoReady = orgList.CargoReady;
            newItem.EstimateTimeOfDelivery = orgList.EstimateTimeOfDelivery;
            newItem.IsWillBooking = orgList.IsWillBooking;
            newItem.ExpPrice = orgList.ExpPrice;
            newItem.DiscussingWhenNew = orgList.DiscussingWhenNew;
            if (orgList.InquirePriceInquireBysList != null && orgList.InquirePriceInquireBysList.Count > 0)
            {
                newItem.InquireByID = orgList.InquirePriceInquireBysList[0].InquireByID;
                newItem.InquireByName = orgList.InquirePriceInquireBysList[0].InquireByCName;
                newItem.InquireBysList = orgList.InquirePriceInquireBysList;
            }
            else
            {
                newItem.InquireByID = orgList.InquireByID;
                newItem.InquireByName = orgList.InquireByName;
            }
            newItem.RespondByID = orgList.RespondByID;
            newItem.RespondByName = orgList.RespondByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.UnitRates = new List<InquireUnit>();

            //foreach (var item in orgList.RateUnitList)
            //{
            //    InquireUnit unit = new InquireUnit();
            //    unit.ID = item.ID;
            //    unit.UnitID = item.UnitID;
            //    unit.UnitName = item.UnitName;
            //    newItem.UnitRates.Add(unit);               
            //}
            return newItem;

        }

        public static List<InquierTruckingRate> TransformC2S(List<ClientInquierTruckingRate> orgList)
        {
            if (orgList == null) return null;

            return orgList.Select(TransformC2S).Where(TruckingItem => TruckingItem != null).ToList();
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 询价UI数据帮助类
    /// </summary>
    public class InquireRatesUIDataHelper : Controller, IDisposable
    {
        #region Services
        /// <summary>
        /// 基础数据服务
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 公用客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        #region 属性

        List<CustomerList> _Carriers = null;
        /// <summary>
        /// 船东集合
        /// </summary>
        public List<CustomerList> Carriers
        {
            get
            {
                if (_Carriers != null) return _Carriers;
                else
                {
                    _Carriers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                       string.Empty, string.Empty, null, null, null,
                                                                       CustomerType.Carrier, null, null, null, null, null, 100);
                    return _Carriers;
                }
            }
        }

        List<CurrencyList> _Currencys = null;
        /// <summary>
        /// 币种集合
        /// </summary>
        public List<CurrencyList> Currencys
        {
            get
            {
                if (_Currencys != null) return _Currencys;
                else
                {
                    _Currencys = ConfigureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
                    return _Currencys;
                }
            }
        }

        CurrencyList _USDCurrency = null;
        /// <summary>
        /// USD币种
        /// </summary>
        public CurrencyList USDCurrency
        {
            get
            {
                if (_USDCurrency != null) return _USDCurrency;
                else
                {
                    _USDCurrency = Currencys.Find(c => c.Code.Contains("USD"));
                    return _USDCurrency;
                }
            }
        }

        List<TransportClauseList> _TransportClauses = null;
        /// <summary>
        /// 
        /// </summary>
        public List<TransportClauseList> TransportClauses
        {
            get
            {
                if (_TransportClauses != null) return _TransportClauses;
                else
                {
                    _TransportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
                    return _TransportClauses;
                }
            }
        }

        List<ShippingLineList> _ShippingLines = null;
        /// <summary>
        /// 航线集合
        /// </summary>
        public List<ShippingLineList> ShippingLines
        {
            get
            {
                if (_ShippingLines != null) return _ShippingLines;
                else
                {
                    //_ShippingLines = TFService.GetShippingLineList(string.Empty, string.Empty, true, true, 100);
                    _ShippingLines = TransportFoundationService.GetShippingLineList(null, null, true, 0);

                    _ShippingLines = (from d in _ShippingLines where d.ParentID == d.ID select d).ToList();

                    return _ShippingLines;
                }
            }
        }

        ConfigureInfo _ConfigureInfo = null;
        /// <summary>
        /// 配置信息
        /// </summary>
        public ConfigureInfo ConfigureInfo
        {
            get
            {
                if (_ConfigureInfo != null) return _ConfigureInfo;
                else
                {
                    _ConfigureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    return _ConfigureInfo;
                }
            }
        }

        List<CommodityList> _Commoditys = null;
        /// <summary>
        /// 
        /// </summary>
        public List<CommodityList> Commoditys
        {
            get
            {
                if (_Commoditys != null) return _Commoditys;
                else
                {
                    _Commoditys = TransportFoundationService.GetCommodityList(string.Empty, null, 0);
                    return _Commoditys;
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _Carriers = null;
            _Commoditys = null;
            _ConfigureInfo = null;
            _Currencys = null;
            _ShippingLines = null;
            _TransportClauses = null;
            _USDCurrency = null;

        }

        #endregion
    }
}
