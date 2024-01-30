#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/5 星期四 15:12:26
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    /// <summary>
    /// 海运MBL单信息扩展方法
    /// </summary>
    public static class OceanMBLInfoExtensionMethods
    {
        /// <summary>
        /// 海运MBL单信息 >>> 事务保存MBL的参数
        /// </summary>
        /// <param name="input">海运MBL单信息</param>
        /// <param name="isSaveAs">是否另存</param>
        /// <returns>务保存MBL的参数</returns>
        public static SaveMBLInfoParameter ConvertToParameter(this OceanMBLInfo input, bool isSaveAs)
        {
            DateTime? dt = input.UpdateDate;
            if (input.UpdateDate.HasValue)
            {
                dt = DateTime.SpecifyKind(input.UpdateDate.Value, DateTimeKind.Unspecified);
            }
            SaveMBLInfoParameter parameter = new SaveMBLInfoParameter
            {
                ID = (isSaveAs || input.ID == GlobalConstants.NewRowID) ? Guid.Empty : input.ID,
                OceanBookingID = input.OceanBookingID,
                MBLNo = input.No,
                NumberOfOriginal = input.NumberOfOriginal,
                VoyageShowType = input.VoyageShowType,
                CheckerID = input.CheckerID,
                ShipperID = input.ShipperID,
                ShipperDescription = input.ShipperDescription,
                ConsigneeID = input.ConsigneeID,
                ConsigneeDescription = input.ConsigneeDescription,
                NotifyPartyID = input.NotifyPartyID,
                NotifyPartyDescription = input.NotifyPartyDescription,
                AgentID = input.AgentID,
                AgentDescription = input.AgentDescription,
                PlaceOfReceiptID = input.PlaceOfReceiptID,
                PlaceOfReceiptName = input.PlaceOfReceiptName,
                PreVoyageID = input.PreVoyageID,
                VoyageID = input.VoyageID,
                POLID = input.POLID,
                POLName = input.POLName,
                PODID = input.PODID,
                PODName = input.PODName,
                NBPODCode = input.NBPODCode,
                PlaceOfDeliveryID = input.PlaceOfDeliveryID,
                PlaceOfDeliveryName = input.PlaceOfDeliveryName,
                FinalDestinationID = input.FinalDestinationID,
                FinalDestinationName = input.FinalDestinationName,
                TransportClauseID = input.TransportClauseID,
                PaymentTermID = input.PaymentTermID,
                FreightDescription = input.FreightDescription,
                NSITBLNotes = input.NSITBLNotes,
                ReleaseType = input.ReleaseType,
                ReleaseDate = input.ReleaseDate,
                Quantity = input.Quantity,
                QuantityUnitID = input.QuantityUnitID,
                Weight = input.Weight,
                WeightUnitID = input.WeightUnitID,
                Measurement = input.Measurement,
                MeasurementUnitID = input.MeasurementUnitID,
                Marks = input.Marks,
                GoodsDescription = input.GoodsDescription,
                IsWoodPacking = input.IsWoodPacking,
                CTNQtyInfo = input.CtnQtyInfo,
                IssueType = input.IssueType,
                IssuePlaceID = input.IssuePlaceID,
                IssueByID = input.IssueByID,
                IssueDate = input.IssueDate,
                WoodPacking = input.WoodPacking,
                AgentText = input.AgentText,
                FreightRateID = input.ContractID,
                PreETD = input.PreETD,
                ETD = input.ETD,
                ETA = input.ETA,
                IsCarrierSendAMS = input.IsCarrierSendAMS,
                BookingPartyID = input.BookingPartyID,
                CollectbyAgentOrderID = input.CollectbyAgentOrderID,
                IsThirdPlacePayOrder = input.IsThirdPlacePayOrder,
                ReallyNotifyParty = input.ReallyNotifyParty,
                ReallyConsignee = input.ReallyConsignee,
                ReallyShipper = input.ReallyShipper,
                GateInDate = input.GateInDate,
                HSCODE = input.HSCODE,
                Commodity = input.Commodity,
                Container = input.Container,
                NotifyParty2 = input.NotifyParty2,
                HasFee = input.HasFee,
                CargoDescription = (input.CargoDescription == null || input.CargoDescription.Type <= 0) ? new SpclCargoDescription() : input.CargoDescription,
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = isSaveAs ? null : dt,
            };
            return parameter;
        }
    }
}
