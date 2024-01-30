﻿using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ICP.FCM.DomesticTrade.ServiceComponent
{
    partial class DomesticTradeService
    {
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        public List<DTBookingPOList> GetDTOrderPOList(Guid orderID, bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalOrderPOList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DTBookingPOList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new DTBookingPOList
                                                 {
                                                     ID = b.Field<Guid>("ID"),
                                                     RelationID = b.Field<Guid>("RelationID"),
                                                     OwnerID = b.Field<Guid>("OwnerID"),
                                                     No = b.Field<string>("No"),
                                                     PODescription = b.Field<string>("PODescription"),
                                                     VendorID = b.Field<Guid?>("VendorID"),
                                                     VendorName = b.Field<string>("VendorName"),
                                                     BuyerID = b.Field<Guid?>("BuyerID"),
                                                     BuyerName = b.Field<string>("BuyerName"),
                                                     FinalDestination = b.Field<string>("FinalDestination"),
                                                     InWarehouseDate = b.Field<DateTime?>("InWarehouseDate"),
                                                     OrderDate = b.Field<DateTime?>("OrderDate"),
                                                     State = (FCMPOState)b.Field<byte>("State"),
                                                     CreateByID = b.Field<Guid>("CreateByID"),
                                                     CreateByName = b.Field<string>("CreateByName"),
                                                     CreateDate = b.Field<DateTime>("CreateDate"),
                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                     IsDirty = false,
                                                     Items = (from m in ds.Tables[1].AsEnumerable()
                                                              where m.Field<Guid>("POID") == b.Field<Guid>("ID")
                                                              select new DTPOItemList
                                                              {
                                                                  ID = m.Field<Guid>("ID"),
                                                                  RelationID = m.Field<Guid>("RelationID"),
                                                                  POID = m.Field<Guid>("POID"),
                                                                  No = m.Field<string>("No"),
                                                                  Description = m.Field<string>("Description"),
                                                                  Color = m.Field<string>("Color"),
                                                                  Size = m.Field<string>("Size"),
                                                                  Volume = m.Field<decimal>("Volume"),
                                                                  Weight = m.Field<decimal>("Weight"),
                                                                  Cartons = m.Field<int>("Cartons"),
                                                                  Units = m.Field<int>("Units"),
                                                                  HTSCode = m.Field<string>("HTSCode"),
                                                                  CreateByID = m.Field<Guid>("CreateByID"),
                                                                  CreateByName = m.Field<string>("CreateByName"),
                                                                  CreateDate = m.Field<DateTime>("CreateDate"),
                                                                  UpdateDate = m.Field<DateTime?>("UpdateDate"),
                                                                  IsDirty = false,
                                                                  // Cartons 
                                                              }).ToList(),
                                                 }).ToList();

                return results;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存PO信息
        /// </summary>
        /// <param name="request">ID</param>
        /// <param name="IsEnglish">IsEnglish</param>
        /// <returns>返回ManyResult</returns>
        public HierarchyManyResult SaveDTOrderPOInfo(POSaveRequest request, bool IsEnglish
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(request.orderID, "orderID");
            ArgumentHelper.AssertGuidNotEmpty(request.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 request.itemIDs,
                 request.itemNos,
                 request.itemDescriptions,
                 request.itemColors,
                 request.itemSizes,
                 request.itemVolumes,
                 request.itemWeights,
                 request.itemCartons,
                 request.itemUnits,
                 request.itemHTSCodes);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveInternalOrderPOInfo");

                string tempItemIDs = request.itemIDs.Join();
                string tempItemNos = request.itemNos.Join();
                string tempItemDescriptions = request.itemDescriptions.Join();
                string tempItemColors = request.itemColors.Join();
                string tempItemSizes = request.itemSizes.Join();
                string tempItemVolumes = request.itemVolumes.Join(3);
                string tempItemWeights = request.itemWeights.Join(3);
                string tempItemCartons = request.itemCartons.Join();
                string tempItemUnits = request.itemUnits.Join();
                string tempItemHTSCodes = request.itemHTSCodes.Join();
                string tempItemUpdateDates = request.itemUpdateDates.Join();

                db.AddInParameter(dbCommand, "@POID", DbType.Guid, request.id);
                db.AddInParameter(dbCommand, "@RelationID", DbType.Guid, request.relationID);
                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, request.orderID);
                db.AddInParameter(dbCommand, "@No", DbType.String, request.no);
                db.AddInParameter(dbCommand, "@Podc", DbType.String, request.podc);
                db.AddInParameter(dbCommand, "@VendorID", DbType.Guid, request.vendorID);
                db.AddInParameter(dbCommand, "@Vendor", DbType.String, request.vendor);
                db.AddInParameter(dbCommand, "@BuyerID", DbType.Guid, request.buyerID);
                db.AddInParameter(dbCommand, "@Buyer", DbType.String, request.buyer);
                db.AddInParameter(dbCommand, "@FinalDestination", DbType.String, request.finalDestination);
                db.AddInParameter(dbCommand, "@InWarehouseDate", DbType.DateTime, request.inWarehouseDate);
                db.AddInParameter(dbCommand, "@OrderDate", DbType.DateTime, request.orderDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, request.updateDate);
                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, tempItemIDs);
                db.AddInParameter(dbCommand, "@ItemNos", DbType.String, tempItemNos);
                db.AddInParameter(dbCommand, "@ItemDescriptions", DbType.String, tempItemDescriptions);
                db.AddInParameter(dbCommand, "@ItemColors", DbType.String, tempItemColors);
                db.AddInParameter(dbCommand, "@ItemSizes", DbType.String, tempItemSizes);
                db.AddInParameter(dbCommand, "@ItemVolumes", DbType.String, tempItemVolumes);
                db.AddInParameter(dbCommand, "@ItemWeights", DbType.String, tempItemWeights);
                db.AddInParameter(dbCommand, "@ItemCartons", DbType.String, tempItemCartons);
                db.AddInParameter(dbCommand, "@ItemUnits", DbType.String, tempItemUnits);
                db.AddInParameter(dbCommand, "@ItemHTSCodes", DbType.String, tempItemHTSCodes);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, request.saveByID);
                db.AddInParameter(dbCommand, "@ItemUpdateDates", DbType.String, tempItemUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "RelationID", "POID", "UpdateDate" }, new string[] { "RelationID", "POID", "ItemID", "UpdateDate" } });
                if (results == null
                    || results.Length < 2
                    || results[0].Items.Count == 0)
                {
                    return null;
                }

                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0]);
                if (results[1] != null)
                {
                    foreach (SingleResult s in results[1].Items)
                    {
                        result.Childs.Add(new HierarchyManyResult(s));
                    }
                }

                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 改变PO状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">状态（0待处理、1已确认、2全部发货、3部分发货、4取消订单）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeDTOrderPOState(
            Guid id,
            FCMPOState state,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeInternalOrderPOState");

                db.AddInParameter(dbCommand, "@POID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveDTOrderPOInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "ID");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveInternalOrderPOInfo");

                string tempUpdateDates = updateDate == null ? string.Empty : updateDate.Value.ToString();

                db.AddInParameter(dbCommand, "@ID", DbType.String, id.ToString());
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
