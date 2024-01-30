using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Office.Interop.Excel;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;


namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约传输辅助类
    /// </summary>
    public class OceanPriceTransformHelper
    {
        #region 生成新的数据
        /// <summary>
        /// BulidNewBasePortData
        /// </summary>
        /// <param name="newData">ClientBasePortList</param>
        /// <param name="parentList">OceanList</param>
        public static void BulidNewBasePortData(ClientBasePortList newData, OceanList parentList)
        {
            newData.ID = Guid.Empty;
            newData.OceanID = parentList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.UpdateDate = null;
            newData.No = null;
            newData.AccountType = AccountType.Shipper;
        }

        /// <summary>
        /// BulidNewArbitraryData
        /// </summary>
        /// <param name="newData">ClientArbitraryList</param>
        /// <param name="parentList">OceanList</param>
        public static void BulidNewArbitraryData(ClientArbitraryList newData, OceanList parentList)
        {
            newData.ID = Guid.Empty;
            newData.OceanID = parentList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.UpdateDate = null;
            newData.No = null;
            newData.Remark =string.Empty;


        }

        /// <summary>
        /// BulidNewAdditionalFeeData
        /// </summary>
        /// <param name="newData">ClientAdditionalFeeList</param>
        /// <param name="parentList">OceanList</param>
        public static void BulidNewAdditionalFeeData(ClientAdditionalFeeList newData, OceanList parentList)
        {
            newData.ID = Guid.Empty;
            newData.OceanID = parentList.ID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.UpdateDate = null;
            newData.BaseRateIDs = new List<Guid>();
        }

        #endregion

        #region Service Object 2 Client Object

        #region 主航线(BasePort)
        /// <summary>
        /// 传输数据服务端实体转客户端实体
        /// </summary>
        /// <param name="orgList">服务端实体</param>
        /// <param name="units">海运合约单位</param>
        /// <returns></returns>
        public static ClientBasePortList TransformS2C(BasePortList orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            ClientBasePortList newItem = new ClientBasePortList
            {
                Account = orgList.Account,
                ID = orgList.ID,
                OceanID = orgList.OceanID,
                AccountType = orgList.AccountType,
                CarrierID = orgList.CarrierID,
                CarrierName = orgList.CarrierName,
                POLID = orgList.POLID,
                POLName = orgList.POLName,
                VIAID = orgList.VIAID,
                VIAName = orgList.VIAName,
                PODID = orgList.PODID,
                PODName = orgList.PODName,
                PlaceOfDeliveryID = orgList.PlaceOfDeliveryID,
                PlaceOfDeliveryName = orgList.PlaceOfDeliveryName,
                FromDate = orgList.FromDate,
                ToDate = orgList.ToDate,
                OriginArb = orgList.OriginArb,
                DestArb = orgList.DestArb,
                Comm = orgList.Comm,
                ItemCode = orgList.ItemCode,
                TransportClauseID = orgList.TransportClauseID,
                TransportClauseName = orgList.TransportClauseName,
                SurCharge = orgList.SurCharge,
                ClosingDate = orgList.ClosingDate,
                TransitTime = orgList.TransitTime,
                Description = orgList.Remark,
                CreateByID = orgList.CreateByID,
                CreateByName = orgList.CreateByName,
                CreateDate = orgList.CreateDate,
                UpdateDate = orgList.UpdateDate,
                No = orgList.No
            };
            newItem.BulidRateToZeroByOceanUints(units);
            #region BulidRate
            foreach (var item in orgList.UnitRates)
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
            #endregion

            return newItem;

        }
        /// <summary>
        /// 传输数据服务端实体集合转客户端实体集合
        /// </summary>
        /// <param name="orgList">服务端实体</param>
        /// <param name="units">海运合约单位</param>
        /// <returns></returns>
        public static List<ClientBasePortList> TransformS2C(List<BasePortList> orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;
            return orgList.Select(item => TransformS2C(item, units)).Where(clientItem => clientItem != null).ToList();
        }
        #endregion

        #region 支线(Arbitrary)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgList"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public static ClientArbitraryList TransformS2C(ArbitraryList orgList,List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            ClientArbitraryList newItem = new ClientArbitraryList();
            newItem.ID = orgList.ID;
            newItem.OceanID = orgList.OceanID;
            newItem.ModeOfTransport = orgList.ModeOfTransport;
            newItem.GeographyType = orgList.GeographyType;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.ItemCode = orgList.ItemCode;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            newItem.CreateByID = orgList.CreateByID;
            newItem.CreateByName = orgList.CreateByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.No = orgList.No;
            newItem.Remark = orgList.Remark;
            newItem.AssociatedType = orgList.AssociatedType;
            newItem.BulidRateToZeroByOceanUints(units);

            #region BulidRate
            foreach (var item in orgList.UnitRates)
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
            #endregion

            return newItem;

        }

        public static List<ClientArbitraryList> TransformOceanItemsToClientObjects(List<ArbitraryList> orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            List<ClientArbitraryList> list = new List<ClientArbitraryList>();
            foreach (var item in orgList)
            {
                ClientArbitraryList clientItem = TransformS2C(item, units);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }

  
        #endregion

        #region 附加费(AdditionalFee)

        public static ClientAdditionalFeeList TransformS2C(AdditionalFeeList orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            ClientAdditionalFeeList newItem = new ClientAdditionalFeeList();
            newItem.ID = orgList.ID;
            newItem.OceanID = orgList.OceanID;
            newItem.ChargingCode = orgList.ChargingCode;
            newItem.ChargingCodeID = orgList.ChargingCodeID;
            newItem.CreateByID = orgList.CreateByID;
            newItem.CreateByName = orgList.CreateByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.CurrencyID = orgList.CurrencyID;
            newItem.CurrencyName = orgList.CurrencyName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.FromDate = orgList.FromDate;
            newItem.IsSpecialFee = orgList.IsSpecialFee;
            newItem.BaseRateIDs = orgList.BaseRateIDs;
            newItem.Percent = orgList.Percent;
            newItem.ToDate = orgList.ToDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.Remark = orgList.Remark;
            newItem.ChargingCodeDescription = orgList.ChargingCodeDescription;
            

            newItem.BulidRateToZeroByOceanUints(units);

            #region BulidRate
            foreach (var item in orgList.UnitRates)
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
            #endregion

            return newItem;

        }

        public static List<ClientAdditionalFeeList> TransformS2C(List<AdditionalFeeList> orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            List<ClientAdditionalFeeList> list = new List<ClientAdditionalFeeList>();
            foreach (var item in orgList)
            {
                ClientAdditionalFeeList clientItem = TransformS2C(item, units);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }

        #endregion

        #region BaseRates

        public static ClientBaseRatesList TransformS2C(BaseRatesList orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            ClientBaseRatesList newItem = new ClientBaseRatesList
            {
                Account = orgList.Account,
                ID = orgList.ID,
                AccountType = orgList.AccountType,
                CarrierID = orgList.CarrierID,
                CarrierName = orgList.CarrierName,
                POLID = orgList.POLID,
                POLName = orgList.POLName,
                VIAID = orgList.VIAID,
                VIAName = orgList.VIAName,
                PODID = orgList.PODID,
                PODName = orgList.PODName,
                PlaceOfDeliveryID = orgList.PlaceOfDeliveryID,
                PlaceOfDeliveryName = orgList.PlaceOfDeliveryName,
                FromDate = orgList.FromDate,
                ToDate = orgList.ToDate,
                Comm = orgList.Comm,
                ItemCode = orgList.ItemCode,
                TransportClauseID = orgList.TransportClauseID,
                TransportClauseName = orgList.TransportClauseName,
                SurCharge = orgList.SurCharge,
                ClosingDate = orgList.ClosingDate,
                TransitTime = orgList.TransitTime,
                Description = orgList.Description,
                CreateByID = orgList.CreateByID,
                CreateByName = orgList.CreateByName,
                CreateDate = orgList.CreateDate,
                UpdateDate = orgList.UpdateDate,
                OceanID = orgList.OceanID,
                BasePortNo = orgList.BasePortNo,
                OriginalArbitraryNo = orgList.OriginalArbitraryNo,
                DestinationArbitraryNo = orgList.DestinationArbitraryNo,
                BasePortID = orgList.BasePortID,
                OriginalArbitraryID = orgList.OriginalArbitraryID,
                DestinationArbitraryID = orgList.DestinationArbitraryID
            };

            newItem.BulidRateToZeroByOceanUints(units);
            #region BulidRate
            foreach (var item in orgList.UnitRates)
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
            #endregion

            return newItem;

        }

        public static List<ClientBaseRatesList> TransformS2C(List<BaseRatesList> orgList, List<OceanUnitList> units)
        {
            if (orgList == null) return null;

            List<ClientBaseRatesList> list = new List<ClientBaseRatesList>();
            foreach (var item in orgList)
            {
                ClientBaseRatesList clientItem = TransformS2C(item, units);
                if (clientItem != null) list.Add(clientItem);
            }

            return list;
        }
        #endregion

        #endregion

        #region  Client Object 2 Service Object

        #region BasePort

        public static BasePortList TransformC2S(ClientBasePortList orgList, List<OceanUnitList> unitList)
        {
            if (orgList == null) return null;

            BasePortList newItem = new BasePortList();
            newItem.Account = orgList.Account;
            newItem.ID = orgList.ID;
            newItem.OceanID = orgList.OceanID;
            newItem.AccountType = orgList.AccountType;
            newItem.CarrierID = orgList.CarrierID;
            newItem.CarrierName = orgList.CarrierName;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.VIAID = orgList.VIAID;
            newItem.VIAName = orgList.VIAName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.PlaceOfDeliveryID = orgList.PlaceOfDeliveryID;
            newItem.PlaceOfDeliveryName = orgList.PlaceOfDeliveryName;
            newItem.FromDate = orgList.FromDate;
            newItem.ToDate = orgList.ToDate;
            newItem.OriginArb = orgList.OriginArb;
            newItem.DestArb = orgList.DestArb;
            newItem.Comm = orgList.Comm;
            newItem.ItemCode = orgList.ItemCode;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            newItem.SurCharge = orgList.SurCharge;
            newItem.ClosingDate = orgList.ClosingDate;
            newItem.TransitTime = orgList.TransitTime;
            newItem.Remark = orgList.Description;
            newItem.CreateByID = orgList.CreateByID;
            newItem.CreateByName = orgList.CreateByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.No = orgList.No;

            newItem.UnitRates = new List<FrmUnitRateList>();

            foreach (var item in unitList)
            {
                FrmUnitRateList unit = new FrmUnitRateList();
                unit.ParentID = newItem.ID;
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

            return newItem;

        }

        public static List<BasePortList> TransformC2S(List<ClientBasePortList> orgList, List<OceanUnitList> unitList)
        {
            if (orgList == null) return null;

            List<BasePortList> list = new List<BasePortList>();
            foreach (var item in orgList)
            {
                BasePortList oceanItem = TransformC2S(item, unitList);
                if (oceanItem != null) list.Add(oceanItem);
            }

            return list;

        }
        #endregion

        #region Arbitrary

        public static ArbitraryList TransformC2S(ClientArbitraryList orgList, List<OceanUnitList> unitList)
        {
            if (orgList == null) return null;

            ArbitraryList newItem = new ArbitraryList();
            newItem.ID = orgList.ID;
            newItem.OceanID = orgList.OceanID;
            newItem.ModeOfTransport = orgList.ModeOfTransport;
            newItem.GeographyType = orgList.GeographyType;
            newItem.POLID = orgList.POLID;
            newItem.POLName = orgList.POLName;
            newItem.PODID = orgList.PODID;
            newItem.PODName = orgList.PODName;
            newItem.ItemCode = orgList.ItemCode;
            newItem.TransportClauseID = orgList.TransportClauseID;
            newItem.TransportClauseName = orgList.TransportClauseName;
            newItem.CreateByID = orgList.CreateByID;
            newItem.CreateByName = orgList.CreateByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.No = orgList.No;
            newItem.Remark = orgList.Remark;

            newItem.UnitRates = new List<FrmUnitRateList>();

            foreach (var item in unitList)
            {
                FrmUnitRateList unit = new FrmUnitRateList();
                unit.ParentID = newItem.ID;
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

            return newItem;

        }

        public static List<ArbitraryList> TransformC2S(List<ClientArbitraryList> orgList, List<OceanUnitList> unitList)
        {
            if (orgList == null) return null;

            List<ArbitraryList> list = new List<ArbitraryList>();
            foreach (var item in orgList)
            {
                ArbitraryList oceanItem = TransformC2S(item, unitList);
                if (oceanItem != null) list.Add(oceanItem);
            }

            return list;

        }
        #endregion

        #region AdditionalFee

        public static AdditionalFeeList TransformC2S(ClientAdditionalFeeList orgList, List<OceanUnitList> unitList)
        {
            if (orgList == null) return null;

            AdditionalFeeList newItem = new AdditionalFeeList();
            newItem.ID = orgList.ID;
            newItem.OceanID = orgList.OceanID;
            newItem.AssociatedCount = orgList.AssociatedCount;
            newItem.ChargingCode = orgList.ChargingCode;
            newItem.ChargingCodeID = orgList.ChargingCodeID;
            newItem.CreateByID = orgList.CreateByID;
            newItem.CreateByName = orgList.CreateByName;
            newItem.CreateDate = orgList.CreateDate;
            newItem.CurrencyID = orgList.CurrencyID;
            newItem.CurrencyName = orgList.CurrencyName;
            newItem.CustomerID = orgList.CustomerID;
            newItem.CustomerName = orgList.CustomerName;
            newItem.FromDate = orgList.FromDate;
            newItem.IsSpecialFee = orgList.IsSpecialFee;
            newItem.BaseRateIDs = orgList.BaseRateIDs;
            newItem.Percent = orgList.Percent;
            newItem.ToDate = orgList.ToDate;
            newItem.UpdateDate = orgList.UpdateDate;
            newItem.Remark = orgList.Remark;
            newItem.ChargingCodeDescription = orgList.ChargingCodeDescription;

            newItem.UnitRates = new List<FrmUnitRateList>();

            foreach (var item in unitList)
            {
                FrmUnitRateList unit = new FrmUnitRateList();
                unit.ParentID = newItem.ID;
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

            return newItem;

        }

        public static List<AdditionalFeeList> TransformC2S(List<ClientAdditionalFeeList> orgList, List<OceanUnitList> unitList)
        {
            if (orgList == null) return null;

            List<AdditionalFeeList> list = new List<AdditionalFeeList>();
            foreach (var item in orgList)
            {
                AdditionalFeeList oceanItem = TransformC2S(item, unitList);
                if (oceanItem != null) list.Add(oceanItem);
            }

            return list;

        }
        #endregion

        #endregion

    }
    /// <summary>
    /// 合约UI辅助类
    /// </summary>
    public class OceanPriceUIDataHelper : Controller,IDisposable
    {  
        
        #region Services
        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 配置管理服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
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

        #region 属性

        List<CustomerList> _Carriers = null;
        public List<CustomerList> Carriers
        {
            get
            {
                if (_Carriers != null) return _Carriers;
                else
                {
                    _Carriers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                       string.Empty, string.Empty, null, null, null,
                                                                       CustomerType.Carrier, null, null, null, null, null, 0);
                    return _Carriers;
                }
            }
        }


        List<CommodityList> commList =null;
        public List<CommodityList> CommList
        {
            get
            {
                if (commList != null) return commList;
                else
                {
                    commList = TransportFoundationService.GetCommodityList(string.Empty, true, 0);
                    return commList;
                }
            }
        }

        List<CustomerList> _Forwardings = null;
        public List<CustomerList> Forwardings
        {
            get
            {
                if (_Forwardings != null)
                {
                    return _Forwardings;
                }
                else
                {
                    _Forwardings = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                       string.Empty, string.Empty, null, null, null,
                                                                       CustomerType.Forwarding, null, null, null, null, null, 0);
                    return _Forwardings;
                }
            }
        }

         List<CurrencyList> _Currencys =null;
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
        public List<ShippingLineList> ShippingLines
        {
            get
            {
                if (_ShippingLines != null) return _ShippingLines;
                else
                {
                     _ShippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty,true, 0);

                     _ShippingLines = (from d in _ShippingLines where d.ParentID ==d.ID select d).ToList();

                    return _ShippingLines;
                }
            }
        }

        ConfigureInfo _ConfigureInfo = null;
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

        #region 模板文件
        public string GetOceanBasePortTemplateFileName()
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\Mode\\Ocean_BasePort_Template.xls";
        }
        public string GetOceanArbitraryTemplateFileName()
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\Mode\\Ocean_Arbitrary_Template.xls";
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _Carriers = null;
            _Commoditys = null;
            _ConfigureInfo = null;
            _Currencys = null;
            _Forwardings = null;
            _ShippingLines = null;
            _TransportClauses = null;
            _USDCurrency = null;
            commList = null;
            
        }

        #endregion
    }

    public class BasePortImportHelper
    {
        /// <summary>
        /// 读取Excel文件为DataSet
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>DataSet</returns>
        public static DataSet ReadExcelToDataSet(string fileName)
        {
            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;"
                             + "data source=" + fileName + ";"
                             + "Extended Properties=Excel 8.0";

            OleDbConnection objConn = new OleDbConnection(strConn);
            String strSql = "Select * From [Sheet1$]";
            OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(objCmd);
            DataSet ds = new DataSet();

            try
            {
                objConn.Open();
                adapter.Fill(ds);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return null; 
            }
            ds.DataSetName = "OceanRates";
            return ds;
        }

        /// <summary>
        /// 64位环境下读取Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataSet X64ReadExcelToDataSet(string fileName)
        {
            Workbook wb = null;
            Worksheet ws = null;
            bool isEqual = false;//不相等 
            var columnArr = new ArrayList();//列字段表 
            var myDs = new DataSet();
            var xlsTable = new DataTable("show");
            object missing = Missing.Value;
            var excel = new Microsoft.Office.Interop.Excel.Application();//lauch excel application
            if (excel != null)
            {
                excel.Visible = false;
                excel.UserControl = true;
                // 以只读的形式打开EXCEL文件 
                wb = excel.Workbooks.Open(fileName, missing, true, missing, missing, missing,
                 missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄 
                ws = (Worksheet)wb.Worksheets.get_Item(1);
                //取得总记录行数(包括标题列) 
                int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数 
                int columnsint = ws.UsedRange.Cells.Columns.Count;//得到列数 
                DataRow dr;
                for (int i = 1; i <= columnsint; i++)
                {
                    //判断是否有列相同 
                    if (i >= 2)
                    {
                        int r = 0;
                        for (int k = 1; k <= i - 1; k++)//列从第一列到第i-1列遍历进行比较 
                        {
                            if (((Range)ws.Cells[1, i]).Text.ToString() == ((Range)ws.Cells[1, k]).Text.ToString())
                            {
                                //如果该列的值等于前面列中某一列的值 
                                xlsTable.Columns.Add(((Range)ws.Cells[1, i]).Text.ToString(), typeof(string));
                                columnArr.Add(((Range)ws.Cells[1, i]).Text.ToString());
                                isEqual = true;
                                r++;
                                break;
                            }
                            else
                            {
                                isEqual = false;
                                continue;
                            }
                        }
                        if (!isEqual)
                        {
                            xlsTable.Columns.Add(((Range)ws.Cells[1, i]).Text.ToString(), typeof(string));
                            columnArr.Add(((Range)ws.Cells[1, i]).Text.ToString());
                        }
                    }
                    else
                    {
                        xlsTable.Columns.Add(((Range)ws.Cells[1, i]).Text.ToString(), typeof(string));
                        columnArr.Add(((Range)ws.Cells[1, i]).Text.ToString());
                    }
                }
                for (int i = 2; i <= rowsint; i++)
                {
                    dr = xlsTable.NewRow();
                    for (int j = 1; j <= columnsint; j++)
                    {
                        if (string.IsNullOrEmpty(columnArr[j - 1].ToString()))
                            break;
                        dr[columnArr[j - 1].ToString()] = ((Range)ws.Cells[i, j]).Text.ToString();
                    }
                    xlsTable.Rows.Add(dr);
                }
            }
            excel.Quit();
            excel = null;
            myDs.Tables.Add(xlsTable);

            myDs.DataSetName = "OceanRates";

            return myDs;
        }




        /// <summary>
        /// 此验证如果不通过，以throw new ApplicationException 形式提出错误
        /// </summary>
        /// <param name="dsExcel">DataSet</param>
        public static void ValidateBasePortExcelColumn(DataSet dsExcel, List<OceanUnitList> units)
        {
            if (dsExcel == null || dsExcel.Tables.Count == 0 || dsExcel.Tables[0].Rows.Count == 0)
                throw new ApplicationException("Row Not Find.");

            DataTable dt = dsExcel.Tables[0];

            #region ValidateColumn

            List<string> columns = new List<string>();
            #region 生成必需要列名
            columns.Add("Account");
            columns.Add("AccountType");
            columns.Add("Carrier");
            columns.Add("POL");
            columns.Add("VIA");
            columns.Add("POD");
            columns.Add("Delivery");
            columns.Add("OriginArb");
            columns.Add("DestArb");
            columns.Add("ItemCode");
            columns.Add("Commodity");
            columns.Add("TERM");
            columns.Add("SURCHARGE");
            columns.Add("CLS");
            columns.Add("T/T");
            columns.Add("Description");
            columns.Add("Duration(From)");
            columns.Add("Duration(To)");
            //foreach (var item in units) columns.Add(item.UnitName);

            #endregion

            #region 如果没有需要有列 报错
            List<string> notFindColumns = new List<string>();
            foreach (var item in columns)
            {
                if (dt.Columns.Contains(item) == false)
                {
                    notFindColumns.Add(item);
                }
            }
            if (notFindColumns.Count > 0)
            {
                StringBuilder errorInfo = new StringBuilder();
                foreach (var item in notFindColumns)
                {
                    if (errorInfo.Length > 0) errorInfo.Append("\r\n");

                    errorInfo.Append(string.Format("Column [{0}] Not Find.", item));
                }

                throw new ApplicationException(errorInfo.ToString());
            }
            #endregion

            #endregion
        }

        /// <summary>
        /// 先把基本信息导入到客户端对象
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="basePorts">basePorts</param>
        /// <param name="parentList">OceanList</param>
        public static void InputBaseInfoIntoBasePort(DataTable dt, List<ClientBasePortList> basePorts, OceanList parentList)
        {
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                bool hasValue = false;
                foreach (var item in dr.ItemArray)
                {
                    if (item == null || item == DBNull.Value) continue;

                    hasValue = true;
                }

                if (hasValue == false) continue;

                ClientBasePortList temp = new ClientBasePortList();
                OceanPriceTransformHelper.BulidNewBasePortData(temp, parentList);

                temp.RowIndex = i;
                temp.Account = Utility.GetStringByDataRow(dr["Account"]);
                temp.ItemCode = Utility.GetStringByDataRow(dr["ItemCode"]);
                temp.Comm = Utility.GetStringByDataRow(dr["Commodity"]);
                temp.SurCharge = Utility.GetStringByDataRow(dr["SURCHARGE"]);
                temp.ClosingDate = Utility.GetStringByDataRow(dr["CLS"]);
                temp.TransitTime = Utility.GetStringByDataRow(dr["T/T"]);
                temp.Description = Utility.GetStringByDataRow(dr["Description"]);

                temp.AccountType = GetAccountTypeByDataRow(dr);
                temp.OriginArb = Utility.GetBoolByDataRow(dr.Field<string>("OriginArb"));
                temp.DestArb = Utility.GetBoolByDataRow(dr.Field<string>("DestArb"));
                //temp.FromDate = Utility.GetDateTimeByDataRow(dr["Duration(From)"],null);
                //temp.ToDate = Utility.GetDateTimeByDataRow(dr["Duration(To)"], null);

                temp.CarrierName = dr.Field<string>("Carrier");
                temp.POLName = dr.Field<string>("POL") == null ? string.Empty : dr.Field<string>("POL").Trim();
                temp.VIAName = dr.Field<string>("VIA") == null ? string.Empty : dr.Field<string>("VIA").Trim();
                
                temp.PODName = dr.Field<string>("POD") == null ? string.Empty : dr.Field<string>("POD").Trim();
                temp.PlaceOfDeliveryName = dr.Field<string>("Delivery") == null ? string.Empty : dr.Field<string>("Delivery").Trim();
                temp.TransportClauseName = dr.Field<string>("TERM");

                temp.OLDPOLName = temp.POLName;
                temp.OLDPODName = temp.PODName;
                temp.OLDVIAName = temp.VIAName;
                temp.OLDDeliveryName = temp.PlaceOfDeliveryName;


                foreach (var item in parentList.OceanUnits)
                {
                    #region BulidRate
                    switch (item.UnitName)
                    {
                        case "45FR": temp.Rate_45FR = GetRateByDataRow(dr, item.UnitName); break;
                        case "40RF": temp.Rate_40RF = GetRateByDataRow(dr, item.UnitName); break;
                        case "45HT": temp.Rate_45HT = GetRateByDataRow(dr, item.UnitName); break;
                        case "20RF": temp.Rate_20RF = GetRateByDataRow(dr, item.UnitName); break;
                        case "20HQ": temp.Rate_20HQ = GetRateByDataRow(dr, item.UnitName); break;
                        case "20TK": temp.Rate_20TK = GetRateByDataRow(dr, item.UnitName); break;
                        case "20GP": temp.Rate_20GP = GetRateByDataRow(dr, item.UnitName); break;
                        case "40TK": temp.Rate_40TK = GetRateByDataRow(dr, item.UnitName); break;
                        case "40OT": temp.Rate_40OT = GetRateByDataRow(dr, item.UnitName); break;
                        case "20FR": temp.Rate_20FR = GetRateByDataRow(dr, item.UnitName); break;
                        case "45GP": temp.Rate_45GP = GetRateByDataRow(dr, item.UnitName); break;
                        case "40GP": temp.Rate_40GP = GetRateByDataRow(dr, item.UnitName); break;
                        case "45RF": temp.Rate_45RF = GetRateByDataRow(dr, item.UnitName); break;
                        case "20RH": temp.Rate_20RH = GetRateByDataRow(dr, item.UnitName); break;
                        case "45OT": temp.Rate_45OT = GetRateByDataRow(dr, item.UnitName); break;
                        case "40NOR": temp.Rate_40NOR = GetRateByDataRow(dr, item.UnitName); break;
                        case "40FR": temp.Rate_40FR = GetRateByDataRow(dr, item.UnitName); break;
                        case "20OT": temp.Rate_20OT = GetRateByDataRow(dr, item.UnitName); break;
                        case "45TK": temp.Rate_45TK = GetRateByDataRow(dr, item.UnitName); break;
                        case "20NOR": temp.Rate_20NOR = GetRateByDataRow(dr, item.UnitName); break;
                        case "40HT": temp.Rate_40HT = GetRateByDataRow(dr, item.UnitName); break;
                        case "40RH": temp.Rate_40RH = GetRateByDataRow(dr, item.UnitName); break;
                        case "45RH": temp.Rate_45RH = GetRateByDataRow(dr, item.UnitName); break;
                        case "45HQ": temp.Rate_45HQ = GetRateByDataRow(dr, item.UnitName); break;
                        case "20HT": temp.Rate_20HT = GetRateByDataRow(dr, item.UnitName); break;
                        case "40HQ": temp.Rate_40HQ = GetRateByDataRow(dr, item.UnitName); break;
                    }

                    #endregion
                }


                basePorts.Add(temp);
            }
        }

        #region 根据行或列转化成为数据
        /// <summary>
        /// 根据传入的行名取得Rate
        /// </summary>
        /// <param name="dr">dr</param>
        /// <param name="columnName">columnName</param>
        /// <returns></returns>
        public static decimal GetRateByDataRow(DataRow dr, string columnName)
        {
            if (dr != null && dr.Table != null && dr.Table.Columns != null && !dr.Table.Columns.Contains(columnName))
            {
                return 0;
            }
            Type typeinfo=dr[columnName].GetType();
            decimal value = 0;
            decimal tryValue;
            
            if (typeinfo == typeof(string))// .Name.ToString().ToUpper() == "STRING")
            {
                if (decimal.TryParse(dr.Field<string>(columnName), out tryValue))
                    value = tryValue;
            }
            else if (typeinfo == typeof(double)) // .Name.ToString().ToUpper() == "DOUBLE")
            {
                value = (decimal)dr.Field<double>(columnName);
            }
            else if (typeinfo == typeof(decimal)) // .Name.ToString().ToUpper() == "DECIMAL")
            {
                value = (decimal)dr.Field<decimal>(columnName);
            }
            else
            {
                if (dr.Field<object>(columnName) != null
                    && decimal.TryParse(dr.Field<object>(columnName).ToString(), out tryValue))
                {
                    value = tryValue;
                }
            }

            return value;
        }
        /// <summary>
        /// 从客户列表生成CarrierName,如果找不到则把错误信息加入到ErrorInfo
        /// </summary>
        /// <param name="item">ClientBasePortList</param>
        /// <param name="carriers">CustomerList</param>
        public static void BulidCarrierByName(ClientBasePortList item, List<CustomerList> carriers)
        {
            string value = item.CarrierName;

            if (value.IsNullOrEmpty() || value.Trim().Length == 0) return;
            value = value.Trim().ToUpper();

            CustomerList carrier = carriers.Find(c => c.Code.ToUpper() == value 
                                                 || c.CName.ToUpper() == value 
                                                 || c.EName.ToUpper() == value
                                                 || c.EShortName.ToUpper()==value
                                                 || c.CShortName.ToUpper()==value);
            if (carrier == null)
            {
                //Los Agel is incorrect!
                item.ErrorInfo += string.Format("Can not find the carrier by Name[{0}].", item.CarrierName);
                item.CarrierID = Guid.Empty;
            }
            else
            {
                item.CarrierName = carrier.Code;
                item.CarrierID = carrier.ID;
            }
        }
        /// <summary>
        /// GetAccountTypeByDataRow
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>AccountType</returns>
        public static AccountType GetAccountTypeByDataRow(DataRow dr)
        {
            string value = dr.Field<string>("AccountType");

            if (value.IsNullOrEmpty() || value.Trim().Length == 0) return AccountType.None;

            if (value.Trim().ToUpper() == AccountType.Shipper.ToString().ToUpper() || value.Trim().ToUpper()=="S")
            {
                return AccountType.Shipper;
            }
            if (value.Trim().ToUpper() == AccountType.Consignee.ToString().ToUpper() || value.Trim().ToUpper() == "C")
            {
                return AccountType.Consignee;
            }
            return AccountType.None;
        }
        /// <summary>
        /// BulidTransportClauseByName
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="transportClauses">transportClauses</param>
        public static void BulidTransportClauseByName(ClientBasePortList item, List<TransportClauseList> transportClauses)
        {
            string value = item.TransportClauseName;

            if (value.IsNullOrEmpty() || value.Trim().Length == 0) return;
            value = value.Trim().ToUpper();

            TransportClauseList term = transportClauses.Find(c => c.Code.ToUpper() == value);
            if (term == null)
            {
                item.ErrorInfo += string.Format("Can not find the Term by Name[{0}].", item.TransportClauseName);
                item.TransportClauseID = Guid.Empty;
            }
            else
            {
                item.TransportClauseName = term.Code;
                item.TransportClauseID = term.ID;
            }
        }

        #endregion

        /// <summary>
        /// 替换逗号
        /// </summary>
        /// <param name="basePorts">List ClientBasePortList</param>
        public static void ReplacePortComma(List<ClientBasePortList> basePorts)
        {
            Regex r = new Regex("\\s,\\s|\\s,|,\\s");
            foreach (var item in basePorts)
            {
                item.POLName = r.Replace(item.POLName, ",").Trim();
                item.VIAName = r.Replace(item.VIAName, ",").Trim();
                item.PODName = r.Replace(item.PODName, ",").Trim();
                item.PlaceOfDeliveryName = r.Replace(item.PlaceOfDeliveryName, ",").Trim();
            }
        }

        /// <summary>
        /// TransformPortToMutil
        /// </summary>
        /// <param name="basePorts">basePorts</param>
        public static void TransformPortToMutil(List<ClientBasePortList> basePorts)
        {
            #region POL 一转多
            List<ClientBasePortList> polBasePorts = basePorts.FindAll(b => b.POLName.Contains("/"));
            if (polBasePorts != null && polBasePorts.Count != 0)
            {
                foreach (var item in polBasePorts)
                {
                    //删除原记录
                    basePorts.Remove(item);
                    string[] portNames = item.POLName.Split(new char[] { '/' });
                    foreach (var portNameItem in portNames)
                    {
                        //增加一个副本
                        ClientBasePortList c = Utility.Clone<ClientBasePortList>(item);
                        c.OLDPOLName=c.POLName = portNameItem.Trim();

                        basePorts.Add(c);
                    }
                }
            }
            #endregion

            #region VIA 一转多
            List<ClientBasePortList> viaBasePorts = basePorts.FindAll(b => b.VIAName.Contains("/"));
            if (viaBasePorts != null && viaBasePorts.Count != 0)
            {
                foreach (var item in viaBasePorts)
                {
                    //删除原记录
                    basePorts.Remove(item);
                    string[] portNames = item.VIAName.Split(new char[] { '/' });
                    foreach (var portNameItem in portNames)
                    {
                        //增加一个副本
                        ClientBasePortList c = Utility.Clone<ClientBasePortList>(item);
                        c.OLDVIAName = c.VIAName = portNameItem.Trim();
                        basePorts.Add(c);
                    }
                }
            }
            #endregion

            #region POD 一转多
            List<ClientBasePortList> podBasePorts = basePorts.FindAll(b => b.PODName.Contains("/"));
            if (podBasePorts != null && podBasePorts.Count != 0)
            {
                foreach (var item in podBasePorts)
                {
                    //删除原记录
                    basePorts.Remove(item);
                    string[] portNames = item.PODName.Split(new char[] { '/' });
                    foreach (var portNameItem in portNames)
                    {
                        //增加一个副本
                        ClientBasePortList c = Utility.Clone<ClientBasePortList>(item);
                        c.OLDPODName=c.PODName = portNameItem.Trim();
                        basePorts.Add(c);
                    }
                }
            }
            #endregion

            #region delivery 一转多
            List<ClientBasePortList> deliveryBasePorts = basePorts.FindAll(b => b.PlaceOfDeliveryName.Contains("/"));
            if (deliveryBasePorts != null && deliveryBasePorts.Count != 0)
            {
                foreach (var item in deliveryBasePorts)
                {
                    //删除原记录
                    basePorts.Remove(item);
                    string[] portNames = item.PlaceOfDeliveryName.Split(new char[] { '/' });
                    foreach (var portNameItem in portNames)
                    {
                        //增加一个副本
                        ClientBasePortList c = Utility.Clone<ClientBasePortList>(item);
                        c.OLDDeliveryName= c.PlaceOfDeliveryName = portNameItem.Trim();
                        basePorts.Add(c);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// ValidatePortNameCarrierTransportClause
        /// </summary>
        /// <param name="basePorts">basePorts</param>
        /// <param name="geographyService">geographyService</param>
        /// <param name="carriers">carriers</param>
        /// <param name="transportClauses">transportClauses</param>
        public static void ValidatePortNameCarrierTransportClause(List<ClientBasePortList> basePorts
                                                                , IGeographyService geographyService
                                                                , List<CustomerList> carriers
                                                                , List<TransportClauseList> transportClauses)
        {
            //最终于需验证的港口名字
            List<string> validatePortNames = new List<string>();

            #region 第一轮先验证Carrier 和 TransportClause,把PortName加到validatePortNames
            foreach (var item in basePorts)
            {
                BulidCarrierByName(item, carriers);
                BulidTransportClauseByName(item, transportClauses);

                if (item.POLName.IsNullOrEmpty() == false && validatePortNames.Contains(item.POLName) == false)
                    validatePortNames.Add(item.POLName);

                if (item.VIAName.IsNullOrEmpty() == false && validatePortNames.Contains(item.VIAName) == false)
                    validatePortNames.Add(item.VIAName);

                if (item.PODName.IsNullOrEmpty() == false && validatePortNames.Contains(item.PODName) == false)
                    validatePortNames.Add(item.PODName);

                if (item.PlaceOfDeliveryName.IsNullOrEmpty() == false && validatePortNames.Contains(item.PlaceOfDeliveryName) == false)
                    validatePortNames.Add(item.PlaceOfDeliveryName);
            }
            #endregion

            List<PortNames> validatedPorts = null;
            if (validatePortNames.Count > 0) validatedPorts = geographyService.GetPortForName(validatePortNames.ToArray());

          
            #region 到数据库中验证PortName
            foreach (var item in basePorts)
            {
                #region POL
                if (item.POLName.IsNullOrEmpty() == false)
                {
                    PortNames pn = validatedPorts.Find(p => p.OriginName == item.POLName);
                    if (pn.ID.IsNullOrEmpty())
                    {
                        item.ErrorInfo += string.Format("Can not find the Port by POLName[{0}].", item.POLName) + Environment.NewLine; 
                    }
                    else
                    {
                        item.POLName = item.OLDPOLName = pn.EName;
                        item.POLID =  pn.ID.Value;
                    }
                }
                #endregion

                #region POD
                if (item.PODName.IsNullOrEmpty() == false)
                {
                    PortNames pn = validatedPorts.Find(p => p.OriginName == item.PODName);
                    if (pn.ID.IsNullOrEmpty())
                    {
                        item.ErrorInfo += string.Format("Can not find the Port by PODName[{0}].", item.PODName) + Environment.NewLine;
                       // item.PODID = Guid.Empty;
                    }
                    else
                    {
                        item.PODName = item.OLDPODName = pn.EName;
                        item.PODID = pn.ID.Value;
                    }
                }
                #endregion

                #region PlaceOfDeliveryName
                if (item.PlaceOfDeliveryName.IsNullOrEmpty() == false)
                {
                    PortNames pn = validatedPorts.Find(p => p.OriginName == item.PlaceOfDeliveryName);
                    if (pn.ID.IsNullOrEmpty())
                    {
                        item.ErrorInfo += string.Format("Can not find the Port by DeliveryName[{0}].", item.PlaceOfDeliveryName) + Environment.NewLine;
                        //item.PlaceOfDeliveryID = Guid.Empty;
                    }
                    else
                    {
                        item.PlaceOfDeliveryName = item.OLDDeliveryName = pn.EName;
                        item.PlaceOfDeliveryID = pn.ID.Value;
                    }
                }
                #endregion

                #region VIA
                if (item.VIAName.IsNullOrEmpty() == false)
                {
                    PortNames pn = validatedPorts.Find(p => p.OriginName == item.VIAName);
                    if (pn.ID.IsNullOrEmpty())
                    {
                        item.ErrorInfo += string.Format("Can not find the Port by VIAName[{0}].", item.VIAName)+Environment.NewLine; 
                        item.VIAID = Guid.Empty;
                    }
                    else
                    {
                        item.VIAName = item.OLDVIAName = pn.EName;
                        item.VIAID =  pn.ID.Value;
                    }
                }
                #endregion

                #region ItemCode
                if (item.ItemCode.IsNullOrEmpty() == false)
                {
                    if (item.ItemCode.Length > 50)
                    {
                        item.ErrorInfo += "The [ItemCode] length exceeds the limit[1,50]." + Environment.NewLine;
                    }
                }
                #endregion

                #region Comm
                //if (item.Comm.IsNullOrEmpty() == false)
                //{
                //    if (!item.Comm.Contains(";"))
                //    {
                //        #region 单个
                //        int n=(from d in commList where d.EName.ToUpper() == item.Comm.ToUpper() || 
                //                                  d.CName.ToUpper() == item.Comm.ToUpper() select d).Count();

                //        if (n == 0)
                //        {
                //            item.CommError = string.Format("Can not find the Commodities by [{0}].", item.Comm);
                //        }
                //        #endregion
                //    }
                //    else
                //    {
                //        #region 多个
                //        string[] comms= item.Comm.Split(';');
                //        if (comms != null && comms.Length > 0)
                //        {
                //            foreach (string str in comms)
                //            {
                //                int n = (from d in commList
                //                         where d.EName.ToUpper() == str.ToUpper() ||
                //                           d.CName.ToUpper() == str.ToUpper()
                //                         select d).Count();

                //                if (n == 0)
                //                {
                //                    item.CommError += string.Format("Can not find the Commodities by [{0}].", item.Comm) + System.Environment.NewLine;
                //                }
                //            }
                //        }
                //        #endregion
                //    }
                //}

                #endregion
            }
            #endregion
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ArbitraryImportHelper
    {
        /// <summary>
        /// 读取Excel文件为DataSet
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>DataSet</returns>
        public static DataSet ReadExcelToDataSet(string fileName)
        {
            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;"
                             + "data source=" + fileName + ";"
                             + "Extended Properties=Excel 8.0";

            OleDbConnection objConn = new OleDbConnection(strConn);
            String strSql = "Select * From [Sheet1$]";
            OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(objCmd);
            DataSet ds = new DataSet();

            try
            {
                objConn.Open();
                adapter.Fill(ds);
            }
            catch { return null; }
            ds.DataSetName = "OceanRates";
            return ds;
        }
        /// <summary>
        /// 64位读取Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataSet X64ReadExcelToDataSet(string fileName)
        {
            Workbook wb = null;
            Worksheet ws = null;
            bool isEqual = false;//不相等 
            var columnArr = new ArrayList();//列字段表 
            var myDs = new DataSet();
            var xlsTable = new DataTable("show");
            object missing = Missing.Value;
            var excel = new Microsoft.Office.Interop.Excel.Application();//lauch excel application
            if (excel != null)
            {
                excel.Visible = false;
                excel.UserControl = true;
                // 以只读的形式打开EXCEL文件 
                wb = excel.Workbooks.Open(fileName, missing, true, missing, missing, missing,
                 missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄 
                ws = (Worksheet)wb.Worksheets.get_Item(1);
                //取得总记录行数(包括标题列) 
                int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数 
                int columnsint = ws.UsedRange.Cells.Columns.Count;//得到列数 
                DataRow dr;
                for (int i = 1; i <= columnsint; i++)
                {
                    //判断是否有列相同 
                    if (i >= 2)
                    {
                        int r = 0;
                        for (int k = 1; k <= i - 1; k++)//列从第一列到第i-1列遍历进行比较 
                        {
                            if (((Range)ws.Cells[1, i]).Text.ToString() == ((Range)ws.Cells[1, k]).Text.ToString())
                            {
                                //如果该列的值等于前面列中某一列的值 
                                xlsTable.Columns.Add(((Range)ws.Cells[1, i]).Text.ToString(), typeof(string));
                                columnArr.Add(((Range)ws.Cells[1, i]).Text.ToString());
                                isEqual = true;
                                r++;
                                break;
                            }
                            else
                            {
                                isEqual = false;
                                continue;
                            }
                        }
                        if (!isEqual)
                        {
                            xlsTable.Columns.Add(((Range)ws.Cells[1, i]).Text.ToString(), typeof(string));
                            columnArr.Add(((Range)ws.Cells[1, i]).Text.ToString());
                        }
                    }
                    else
                    {
                        xlsTable.Columns.Add(((Range)ws.Cells[1, i]).Text.ToString(), typeof(string));
                        columnArr.Add(((Range)ws.Cells[1, i]).Text.ToString());
                    }
                }
                for (int i = 2; i <= rowsint; i++)
                {
                    dr = xlsTable.NewRow();
                    for (int j = 1; j <= columnsint; j++)
                    {
                        dr[columnArr[j - 1].ToString()] = ((Range)ws.Cells[i, j]).Text.ToString();
                    }
                    xlsTable.Rows.Add(dr);
                }
            }
            excel.Quit();
            excel = null;
            myDs.Tables.Add(xlsTable);

            myDs.DataSetName = "OceanRates";

            return myDs;
        }
        /// <summary>
        /// 此验证如果不通过，以throw new ApplicationException 形式提出错误
        /// </summary>
        /// <param name="dsExcel">DataSet</param>
        public static void ValidateArbitraryExcelColumn(DataSet dsExcel, List<OceanUnitList> units)
        {
            if (dsExcel == null || dsExcel.Tables.Count == 0 || dsExcel.Tables[0].Rows.Count == 0)
                throw new ApplicationException("Row Not Find.");

            DataTable dt = dsExcel.Tables[0];

            #region ValidateColumn

            List<string> columns = new List<string>();
            #region 生成必需要列名
            columns.Add("ItemCode");
            columns.Add("Geography");
            columns.Add("From");
            columns.Add("To");
            columns.Add("TERM");
            foreach (var item in units) columns.Add(item.UnitName);

            #endregion

            #region 如果没有需要有列 报错
            List<string> notFindColumns = new List<string>();
            foreach (var item in columns)
            {
                if (dt.Columns.Contains(item) == false)
                {
                    notFindColumns.Add(item);
                }
            }
            if (notFindColumns.Count > 0)
            {
                StringBuilder errorInfo = new StringBuilder();
                foreach (var item in notFindColumns)
                {
                    if (errorInfo.Length > 0) errorInfo.Append("\r\n");

                    errorInfo.Append(string.Format("Column [{0}] Not Find.", item));
                }

                throw new ApplicationException(errorInfo.ToString());
            }
            #endregion

            #endregion
        }

        /// <summary>
        /// 先把基本信息导入到客户端对象
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="arbitrarys">arbitrarys</param>
        /// <param name="parentList">OceanList</param>
        public static void InputBaseInfoIntoArbitrary(DataTable dt, List<ClientArbitraryList> arbitrarys, OceanList parentList)
        {
            foreach (DataRow dr in dt.Rows)
            {

                bool hasValue = false;
                foreach (var item in dr.ItemArray)
                {
                    if (item == null || item == DBNull.Value) continue;

                    hasValue = true;
                }

                if (hasValue == false) continue;

                ClientArbitraryList temp = new ClientArbitraryList();
                OceanPriceTransformHelper.BulidNewArbitraryData(temp, parentList);

                temp.ItemCode = Utility.GetStringByDataRow(dr["ItemCode"]);// dr.Field<string>("ItemCode");
                temp.GeographyType = GetGeographyTypeByDataRow(dr);
                temp.ModeOfTransport = GetTransportTypeByDataRow(dr);
                temp.POLName = Utility.GetStringByDataRow(dr["From"]);
                temp.PODName = Utility.GetStringByDataRow(dr["To"]);
                temp.TransportClauseName = dr.Field<string>("TERM");
                temp.Remark = dr.Field<string>("Description");

                foreach (var item in parentList.OceanUnits)
                {
                    #region BulidRate
                    switch (item.UnitName)
                    {
                        case "45FR": temp.Rate_45FR = GetRateByDataRow(dr, item.UnitName); break;
                        case "40RF": temp.Rate_40RF = GetRateByDataRow(dr, item.UnitName); break;
                        case "45HT": temp.Rate_45HT = GetRateByDataRow(dr, item.UnitName); break;
                        case "20RF": temp.Rate_20RF = GetRateByDataRow(dr, item.UnitName); break;
                        case "20HQ": temp.Rate_20HQ = GetRateByDataRow(dr, item.UnitName); break;
                        case "20TK": temp.Rate_20TK = GetRateByDataRow(dr, item.UnitName); break;
                        case "20GP": temp.Rate_20GP = GetRateByDataRow(dr, item.UnitName); break;
                        case "40TK": temp.Rate_40TK = GetRateByDataRow(dr, item.UnitName); break;
                        case "40OT": temp.Rate_40OT = GetRateByDataRow(dr, item.UnitName); break;
                        case "20FR": temp.Rate_20FR = GetRateByDataRow(dr, item.UnitName); break;
                        case "45GP": temp.Rate_45GP = GetRateByDataRow(dr, item.UnitName); break;
                        case "40GP": temp.Rate_40GP = GetRateByDataRow(dr, item.UnitName); break;
                        case "45RF": temp.Rate_45RF = GetRateByDataRow(dr, item.UnitName); break;
                        case "20RH": temp.Rate_20RH = GetRateByDataRow(dr, item.UnitName); break;
                        case "45OT": temp.Rate_45OT = GetRateByDataRow(dr, item.UnitName); break;
                        case "40NOR": temp.Rate_40NOR = GetRateByDataRow(dr, item.UnitName); break;
                        case "40FR": temp.Rate_40FR = GetRateByDataRow(dr, item.UnitName); break;
                        case "20OT": temp.Rate_20OT = GetRateByDataRow(dr, item.UnitName); break;
                        case "45TK": temp.Rate_45TK = GetRateByDataRow(dr, item.UnitName); break;
                        case "20NOR": temp.Rate_20NOR = GetRateByDataRow(dr, item.UnitName); break;
                        case "40HT": temp.Rate_40HT = GetRateByDataRow(dr, item.UnitName); break;
                        case "40RH": temp.Rate_40RH = GetRateByDataRow(dr, item.UnitName); break;
                        case "45RH": temp.Rate_45RH = GetRateByDataRow(dr, item.UnitName); break;
                        case "45HQ": temp.Rate_45HQ = GetRateByDataRow(dr, item.UnitName); break;
                        case "20HT": temp.Rate_20HT = GetRateByDataRow(dr, item.UnitName); break;
                        case "40HQ": temp.Rate_40HQ = GetRateByDataRow(dr, item.UnitName); break;
                    }

                    #endregion
                }


                arbitrarys.Add(temp);
            }
        }



        #region 根据行或列转化成为数据

        /// <summary>
        /// GetGeographyTypeByDataRow
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>AccountType</returns>
        public static GeographyType GetGeographyTypeByDataRow(DataRow dr)
        {
            string value = dr.Field<string>("Geography");

            if (value.IsNullOrEmpty() || value.Trim().Length == 0) return GeographyType.None;

            if ((value.Trim().ToUpper()=="O")||(value.Trim().ToUpper() == GeographyType.Original.ToString().ToUpper()))
            {
                return GeographyType.Original;
            }
            if ((value.Trim().ToUpper()=="D") || (value.Trim().ToUpper() == GeographyType.Destination.ToString().ToUpper()))
            {
                return GeographyType.Destination;
            }
            return GeographyType.None;
        }

        /// <summary>
        /// GetGeographyTypeByDataRow
        /// </summary>
        /// <param name="dr">dr</param>
        /// <returns>AccountType</returns>
        public static ModeOfTransport GetTransportTypeByDataRow(DataRow dr)
        {
            string value = dr.Field<string>("Transport");

            if (value.IsNullOrEmpty() || value.Trim().Length == 0) return ModeOfTransport.None;

            if ((value.Trim().ToUpper() == "F") || (value.Trim().ToUpper() == ModeOfTransport.Feeder.ToString().ToUpper()))
            {
                return ModeOfTransport.Feeder;
            }
            if ((value.Trim().ToUpper() == "T") || (value.Trim().ToUpper() == ModeOfTransport.Truck.ToString().ToUpper()))
            {
                return ModeOfTransport.Truck;
            }
            return ModeOfTransport.None;
        }

        /// <summary>
        /// BulidTransportClauseByName
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="transportClauses">transportClauses</param>
        public static void BulidTransportClauseByName(ClientArbitraryList item, List<TransportClauseList> transportClauses)
        {
            string value = item.TransportClauseName;

            if (value.IsNullOrEmpty() || value.Trim().Length == 0) return;
            value = value.Trim().ToUpper();

            TransportClauseList term = transportClauses.Find(c => c.Code.ToUpper() == value);
            if (term == null)
            {
                item.ErrorInfo += string.Format("Can not find the Term by Name[{0}].", item.TransportClauseName);
                item.TransportClauseID = Guid.Empty;
            }
            else
            {
                item.TransportClauseName = term.Code;
                item.TransportClauseID = term.ID;
            }
        }

        /// <summary>
        /// 根据传入的行名取得Rate
        /// </summary>
        /// <param name="dr">dr</param>
        /// <param name="columnName">columnName</param>
        /// <returns></returns>
        public static decimal GetRateByDataRow(DataRow dr, string columnName)
        {
            string value = dr.Field<string>(columnName);

            if (value.IsNullOrEmpty()) return 0m;

            decimal rate = 0m;
            bool isSrcc = decimal.TryParse(value, out rate);
            if (isSrcc) return rate;
            else return 0m;
        }

        #endregion

        /// <summary>
        /// 替换逗号
        /// </summary>
        /// <param name="arbitrarys">List ClientArbitraryList</param>
        public static void ReplacePortComma(List<ClientArbitraryList> arbitrarys)
        {
            Regex r = new Regex("\\s,\\s|\\s,|,\\s");
            foreach (var item in arbitrarys)
            {
                item.POLName = r.Replace(item.POLName, ",").Trim();
                item.PODName = r.Replace(item.PODName, ",").Trim();
            }
        }

        /// <summary>
        /// TransformPortToMutil
        /// </summary>
        /// <param name="arbitrarys">arbitrarys</param>
        public static void TransformPortToMutil(List<ClientArbitraryList> arbitrarys)
        {
            #region POL 一转多
            List<ClientArbitraryList> polArbitrarys = arbitrarys.FindAll(b => b.POLName.Contains("/"));
            if (polArbitrarys != null && polArbitrarys.Count != 0)
            {
                foreach (var item in polArbitrarys)
                {
                    //删除原记录
                    arbitrarys.Remove(item);

                    string[] portNames = item.POLName.Split(new char[] { '/' });
                    foreach (var portNameItem in portNames)
                    {
                        //增加一个副本
                        ClientArbitraryList c = Utility.Clone<ClientArbitraryList>(item);
                        c.POLName = portNameItem.Trim();
                        arbitrarys.Add(c);
                    }
                }
            }
            #endregion

            #region POD 一转多
            List<ClientArbitraryList> podArbitrarys = arbitrarys.FindAll(b => b.PODName.Contains("/"));
            if (podArbitrarys != null && podArbitrarys.Count != 0)
            {
                foreach (var item in podArbitrarys)
                {
                    //删除原记录
                    arbitrarys.Remove(item);
                    string[] portNames = item.PODName.Split(new char[] { '/' });
                    foreach (var portNameItem in portNames)
                    {
                        //增加一个副本
                        ClientArbitraryList c = Utility.Clone<ClientArbitraryList>(item);
                        c.PODName = portNameItem.Trim();
                        arbitrarys.Add(c);
                    }
                }
            }
            #endregion

        }

        /// <summary>
        /// ValidatePortNameCarrierTransportClause
        /// </summary>
        /// <param name="arbitrarys">arbitrarys</param>
        /// <param name="geographyService">geographyService</param>
        /// <param name="carriers">carriers</param>
        /// <param name="transportClauses">transportClauses</param>
        public static void ValidatePortNameTransportClause(List<ClientArbitraryList> arbitrarys
                                                                , IGeographyService geographyService
                                                                , List<TransportClauseList> transportClauses)
        {
            //最终于需验证的港口名字
            List<string> validatePortNames = new List<string>();

            #region 第一轮先验证Carrier 和 TransportClause,把PortName加到validatePortNames
            foreach (var item in arbitrarys)
            {
                BulidTransportClauseByName(item, transportClauses);

                if (item.POLName.IsNullOrEmpty() == false && validatePortNames.Contains(item.POLName) == false)
                    validatePortNames.Add(item.POLName);


                if (item.PODName.IsNullOrEmpty() == false && validatePortNames.Contains(item.PODName) == false)
                    validatePortNames.Add(item.PODName);
            }
            #endregion

            List<PortNames> validatedPorts = null;
            if (validatePortNames.Count > 0) validatedPorts = geographyService.GetPortForName(validatePortNames.ToArray());

            #region 到数据库中验证PortName
            foreach (var item in arbitrarys)
            {
                #region POL
                if (item.POLName.IsNullOrEmpty() == false)
                {
                    PortNames pn = validatedPorts.Find(p => p.OriginName == item.POLName);
                    if (pn.ID.IsNullOrEmpty())
                    {
                        item.ErrorInfo += string.Format("Can not find the Port by POLName[{0}].", item.POLName); ;
                        item.POLID = Guid.Empty;
                    }
                    else
                    {
                        item.POLName = pn.EName;
                        item.POLID = pn.ID.Value;
                    }
                }
                #endregion

                #region POD
                if (item.PODName.IsNullOrEmpty() == false)
                {
                    PortNames pn = validatedPorts.Find(p => p.OriginName == item.PODName);
                    if (pn.ID.IsNullOrEmpty())
                    {
                        item.ErrorInfo += string.Format("Can not find the Port by PODName[{0}].", item.PODName); ;
                        item.PODID = Guid.Empty;
                    }
                    else
                    {
                        item.PODName = pn.EName;
                        item.PODID = pn.ID.Value;
                    }
                }
                #endregion
            }
            #endregion
        }
    }

    public class AsyncBuilerBaseItem 
    {
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }
        public delegate void AsyncEventHandler(Guid oceanID);

        public void AsyncBuilerBaseItemForOceanID(Guid oceanID)
        {
            AsyncEventHandler asy = new AsyncEventHandler(ExecDB);

            IAsyncResult ia = asy.BeginInvoke(oceanID, new AsyncCallback(CallbackMethod), asy);
        }
        void CallbackMethod(IAsyncResult ar)
        {
            AsyncEventHandler del = (AsyncEventHandler)ar.AsyncState;
            del.EndInvoke(ar);
        }

        public void ExecDB(Guid oceanID)
        {
            try
            {
                OceanPriceService.BuilerBaseItemForOceanID(oceanID);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null,ex);
            }
        }
        
    }
}
