using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceComponent
{
     partial class OceanImportService
    {

        #region 根据订单ID获取PO列表
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        public List<OceanImportPOList> GetOIOrderPOList(Guid orderID)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIOrderPOList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanImportPOList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new OceanImportPOList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        RelationID = b.Field<Guid>("RelationID"),
                                                        //OwnerID = b.Field<Guid>("OwnerID"),
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
                                                                 select new OceanPOItemList
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
        #endregion

        #region 保存PO信息
        /// <summary>
        /// 保存PO信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="relationID">RelationID</param>
        /// <param name="orderID">订单ID</param>
        /// <param name="no">PO号</param>
        /// <param name="podc">订单描述</param>
        /// <param name="vendorID">卖主</param>
        /// <param name="vendor">卖主描述</param>
        /// <param name="buyerID">买主</param>
        /// <param name="buyer">买主描述</param>
        /// <param name="finalDestination">最终目的地</param>
        /// <param name="inWarehouseDate">入仓时间</param>
        /// <param name="orderDate">处理时间</param>
        /// <param name="updateDate">数据版本</param>
        /// <param name="itemIDs">ItemID</param>
        /// <param name="itemNos">Item号列表</param>
        /// <param name="itemDescriptions">Item描述列表</param>
        /// <param name="itemColors">Item颜色列表</param>
        /// <param name="itemSizes">Item尺寸列表</param>
        /// <param name="itemVolumes">Item体积列表</param>
        /// <param name="itemWeights">Item重量列表</param>
        /// <param name="itemCartons">Item装箱数量列表</param>
        /// <param name="itemUnits">tem件数列表</param>
        /// <param name="itemHTSCodes">Item海关编码列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="itemUpdateDates">更新时间</param>
        /// <returns></returns>
        public HierarchyManyResult SaveOIOrderPOInfo(POSaveRequest request)
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIOrderPOInfo");

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
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

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
        #endregion

        #region 删除PO
        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveOIOrderPOInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "ID");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOIOrderPOInfo");

                string tempUpdateDates = updateDate == null ? string.Empty : updateDate.Value.ToString();

                db.AddInParameter(dbCommand, "@ID", DbType.String, id.ToString());
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

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
        #endregion

    }
}
