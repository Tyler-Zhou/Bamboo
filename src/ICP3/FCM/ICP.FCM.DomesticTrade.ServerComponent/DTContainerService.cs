using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;

namespace ICP.FCM.DomesticTrade.ServiceComponent
{
    partial class DomesticTradeService
    {
        public Dictionary<Guid, SaveResponse> SaveContainerCargos(List<ContainerSaveRequest> containers,
            List<CargoSaveRequest> cargos, List<POSaveRequest> pos, PoItemContainersRelationSaveRequest relations,bool IsEnglish)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                if (containers != null)
                {
                    foreach (ContainerSaveRequest saveRequest in containers)
                    {
                        ManyResult temp = this.SaveDTContainerInfo(saveRequest,IsEnglish);

                        for (int i = 0; i < temp.Items.Count; i++)
                        {
                            Guid newId = temp.Items[i].GetValue<Guid>("ID");
                            Guid oleId = saveRequest.containerIDs[i].Value;

                            if (newId != oleId)
                            {
                                relations.poContainerIds = ReplacePrimaryKeys(relations.poContainerIds, oleId, newId);
                                relations.itemContainerIds = ReplacePrimaryKeys(relations.itemContainerIds, oleId, newId);
                            }
                        }

                        result.Add(saveRequest.RequestId,
                            new SaveResponse { RequestId = saveRequest.RequestId, ManyResult = temp });
                    }
                }

                if (cargos != null)
                {
                    foreach (CargoSaveRequest saveRequest in cargos)
                    {
                        //ContainerId是使用的客户端提供的Guid.NewGuid()的值，故不用更新
                        result.Add(saveRequest.RequestId,
                            new SaveResponse { RequestId = saveRequest.RequestId, ManyResult = this.SaveDTMBLContainerCargoInfo(saveRequest, IsEnglish) });
                    }
                }

                if (pos != null)
                {
                    foreach (POSaveRequest saveRequest in pos)
                    {
                        HierarchyManyResult temp = this.SaveDTOrderPOInfo(saveRequest, IsEnglish);

                        Guid newId = temp.GetValue<Guid>("POID");

                        if (newId != saveRequest.id.Value)
                        {
                            relations.poIds = ReplacePrimaryKeys(relations.poIds, saveRequest.id.Value, newId);
                        }

                        for (int j = 0; j < temp.Childs.Count; j++)
                        {
                            HierarchyManyResult itemTemp = temp.Childs[j];
                            Guid newItemId = itemTemp.GetValue<Guid>("ItemID");
                            Guid oldItemId = saveRequest.itemIDs[j].Value;

                            if (newId != oldItemId)
                            {
                                relations.itemIds = ReplacePrimaryKeys(relations.itemIds, oldItemId, newItemId);
                            }
                        }

                        result.Add(saveRequest.RequestId,
                            new SaveResponse { RequestId = saveRequest.RequestId, HierarchyManyResult = temp });
                    }
                }

                if (relations != null)
                {
                    this.SaveDTContainerPOInfoByIds(relations.poContainerIds, relations.poIds, relations.saveByID, IsEnglish);
                    this.SaveDTContainerPOItemInfo(relations.itemContainerIds, relations.itemIds, relations.saveByID, IsEnglish);
                }

                scope.Complete();
                
                return result;
            }
        }

        Guid[] ReplacePrimaryKeys(Guid[] array, Guid old, Guid newId)
        {
            Guid[] result = new Guid[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == old)
                {
                    result[i] = newId;
                }
                else
                {
                    result[i] = array[i];
                }
            }

            return result;
        }

        #region Container

        /// <summary>
        /// 获取订舱单箱信息
        /// </summary>
        /// <param name="bookingID">订舱ID</param>
        /// <returns>返回组合结果集，包含订舱单、箱列表和货物列表</returns>
        public CompositeContainerObjects GetCompositeContainerObjects(Guid bookingID,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(bookingID, "bookingID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalLoadContainerList");

                db.AddInParameter(dbCommand, "@InternalTradeBookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <1)
                {
                    return null;
                }

                CompositeContainerObjects result = new CompositeContainerObjects();

                result.ContainerList = (from b in ds.Tables[1].AsEnumerable()
                                        select new DTContainerList
                                        {
                                            ID = b.Field<Guid>("ID"),
                                            No = b.Field<string>("No"),
                                            CarNo = b.Field<string>("CarNo"),
                                            SealNo = b.Field<string>("SealNo"),
                                            DeliveryDate = b.Field<DateTime?>("DeliveryDate"),
                                            ArriveDate = b.Field<DateTime?>("ArriveDate"),
                                            ReturnDate = b.Field<DateTime?>("ReturnDate"),
                                            DriverName = b.Field<string>("DriverName"),
                                            TypeID = b.Field<Guid>("TypeID"),
                                            TypeName = b.Field<string>("TypeName"),
                                            ShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<string>("CreateByName"),
                                            UpdateByName = b.Field<string>("UpdateByName"),
                                            UpdateByID = b.Field<Guid?>("UpdateBy"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            UpdateDate = b.Field<DateTime?>("UpdateDate")
                                        }).ToList();

               
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
        /// 获取海运箱信息
        /// </summary>
        /// <param name="bookingID">订舱单ID</param>
        /// <returns>返回海运箱信息</returns>
        public List<DTContainerList> GetDTContainerList(Guid bookingID,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(bookingID, "bookingID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalContainerList");

                db.AddInParameter(dbCommand, "@BookingID", DbType.Guid, bookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DTContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DTContainerList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        ArriveDate = b.Field<DateTime?>("ArriveDate"),
                                                        DeliveryDate = b.Field<DateTime?>("DeliveryDate"),
                                                        ReturnDate = b.Field<DateTime?>("ReturnDate"),
                                                        CarNo = b.Field<string>("CarNo"),
                                                        DriverName = b.Field<string>("DriverName"),
                                                        IsPartOf = b.Field<bool>("IsPartOf"),
                                                        IsSOC = b.Field<bool>("IsSOC"),
                                                        OceanBookingID = bookingID,
                                                        ShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                                        TypeID = b.Field<Guid>("TypeID"),
                                                        SealNo = b.Field<string>("SealNo"),
                                                        TypeName = b.Field<string>("TypeName"),
                                                        UpdateByID = b.Field<Guid?>("UpdateByID"),
                                                        UpdateByName = b.Field<string>("UpdateByName"),
                                                        No = b.Field<string>("No"),
                                                        CreateByID = b.Field<Guid>("CreateByID"),
                                                        CreateByName = b.Field<string>("CreateByName"),
                                                        CreateDate = b.Field<DateTime>("CreateDate"),
                                                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                        IsDirty = false
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
        /// 保存箱信息
        /// </summary>
        /// <param name="containerIDs">箱ID列表</param>
        /// <param name="containerNos">箱号列表</param>
        /// <param name="containerTypeIDs">箱型列表</param>
        /// <param name="containerSealNos">封条号列表</param>
        /// <param name="deliveryDates">出发日</param>
        /// <param name="arriveDates">到达日</param>
        /// <param name="returnDates">还柜日</param>
        /// <param name="driverNames">司机名</param>
        /// <param name="carNos">车牌</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="containerUpdateDates">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public ManyResult SaveDTContainerInfo(ContainerSaveRequest containerSaveRequest,bool IsEnglish
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(containerSaveRequest.oceanBookingID, "oceanBookingID");
            ArgumentHelper.AssertGuidNotEmpty(containerSaveRequest.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 containerSaveRequest.containerShippingOrderNos,
                 containerSaveRequest.containerIDs,
                 containerSaveRequest.containerNos,
                 containerSaveRequest.containerTypeIDs,
                 containerSaveRequest.containerSealNos,
                 containerSaveRequest.deliveryDates,
                 containerSaveRequest.arriveDates,
                 containerSaveRequest.returnDates,
                 containerSaveRequest.driverNames,
                 containerSaveRequest.carNos,
                 containerSaveRequest.containerUpdateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveInternalContainerInfoForTruckService]");

                string tempcontainerShippingOrderNos = containerSaveRequest.containerShippingOrderNos.Join();
                string tempcontainerIDs = containerSaveRequest.containerIDs.Join();
                string tempcontainerNos = containerSaveRequest.containerNos.Join();
                string tempcontainerTypeIDs = containerSaveRequest.containerTypeIDs.Join();
                string tempcontainerSealNos = containerSaveRequest.containerSealNos.Join();
                string tempdeliveryDates = containerSaveRequest.deliveryDates.Join();
                string temparriveDates = containerSaveRequest.arriveDates.Join();
                string tempreturnDates = containerSaveRequest.returnDates.Join();
                string tempdriverNames = containerSaveRequest.driverNames.Join();
                string tempcarNos = containerSaveRequest.carNos.Join();
                string tempcontainerUpdateDates = containerSaveRequest.containerUpdateDates.Join();

                db.AddInParameter(dbCommand, "@InternalBookingID", DbType.Guid, containerSaveRequest.oceanBookingID);

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempcontainerIDs);
                db.AddInParameter(dbCommand, "@Nos", DbType.String, tempcontainerNos);
                db.AddInParameter(dbCommand, "@ShippingOrderNos", DbType.String, tempcontainerShippingOrderNos);
                db.AddInParameter(dbCommand, "@TypeIDs", DbType.String, tempcontainerTypeIDs);
                db.AddInParameter(dbCommand, "@SealNos", DbType.String, tempcontainerSealNos);
                db.AddInParameter(dbCommand, "@DeliveryDates", DbType.String, tempdeliveryDates);
                db.AddInParameter(dbCommand, "@ArriveDates", DbType.String, temparriveDates);
                db.AddInParameter(dbCommand, "@ReturnDates", DbType.String, tempreturnDates);
                db.AddInParameter(dbCommand, "@DriverNames", DbType.String, tempdriverNames);
                db.AddInParameter(dbCommand, "@CarNos", DbType.String, tempcarNos);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempcontainerUpdateDates);

                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, containerSaveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
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
        /// 删除业务下箱信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public void RemoveDTContaierInfo(
           Guid[] ids,
           Guid removeByID,
            DateTime?[] updateDates,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveInternalContainerInfo");

                string tempIDs = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

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

        #region PO

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveDTContainerPOInfo(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanContainerPOInfo");

                string tempUpdateDates = updateDate == null ? string.Empty : updateDate.Value.ToString();

                db.AddInParameter(dbCommand, "@POID", DbType.String, id.ToString());
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

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

        /// <summary>
        /// 保存箱下的PO信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="containerID">箱ID</param>
        /// <param name="poID">POID</param>
        /// <param name="no">PO号</param>
        /// <param name="podc">订单描述</param>
        /// <param name="vendorID">卖主</param>
        /// <param name="vendor">卖主描述</param>
        /// <param name="buyerID">买主</param>
        /// <param name="buyer">买主描述</param>
        /// <param name="finalDestination">最终目的地</param>
        /// <param name="inWarehouseDate">入仓时间</param>
        /// <param name="orderDate">处理时间</param>
        /// <param name="itemIDs">ItemID列表</param>
        /// <param name="itemNos">Item号列表</param>
        /// <param name="itemDescriptions">Item描述列表</param>
        /// <param name="itemColors">Item颜色列表</param>
        /// <param name="itemSizes">Item尺寸列表</param>
        /// <param name="itemVolumes">Item体积列表</param>
        /// <param name="itemWeights">Item重量列表</param>
        /// <param name="itemCartons">Item装箱数量列表</param>
        /// <param name="itemUnits">Item件数列表</param>
        /// <param name="itemHTSCodes">Item海关编码列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public HierarchyManyResult SaveDTContainerPOInfo(ContainerPOSaveRequest containerPOSaveRequest,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(containerPOSaveRequest.bookingId, "bookingId");
            ArgumentHelper.AssertGuidNotEmpty(containerPOSaveRequest.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 containerPOSaveRequest.itemIDs,
                 containerPOSaveRequest.itemRelationIDs,
                 containerPOSaveRequest.itemNos,
                 containerPOSaveRequest.itemDescriptions,
                 containerPOSaveRequest.itemColors,
                 containerPOSaveRequest.itemSizes,
                 containerPOSaveRequest.itemVolumes,
                 containerPOSaveRequest.itemWeights,
                 containerPOSaveRequest.itemCartons,
                 containerPOSaveRequest.itemUnits,
                 containerPOSaveRequest.itemHTSCodes,
                 containerPOSaveRequest.updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanContainerPOInfo");

                string tempItemIDs = containerPOSaveRequest.itemIDs.Join();
                string tempItemRelationIds = containerPOSaveRequest.itemRelationIDs.Join();
                string tempItemNos = containerPOSaveRequest.itemNos.Join();
                string tempItemDescriptions = containerPOSaveRequest.itemDescriptions.Join();
                string tempItemColors = containerPOSaveRequest.itemColors.Join();
                string tempItemSizes = containerPOSaveRequest.itemSizes.Join();
                string tempItemVolumes = containerPOSaveRequest.itemVolumes.Join(3);
                string tempItemWeights = containerPOSaveRequest.itemWeights.Join(3);
                string tempItemCartons = containerPOSaveRequest.itemCartons.Join();
                string tempItemUnits = containerPOSaveRequest.itemUnits.Join();
                string tempItemHTSCodes = containerPOSaveRequest.itemHTSCodes.Join();
                string tempUpdateDates = containerPOSaveRequest.updateDates.Join();

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, containerPOSaveRequest.bookingId);
                db.AddInParameter(dbCommand, "@POID", DbType.Guid, containerPOSaveRequest.poId);
                db.AddInParameter(dbCommand, "@ContainerID", DbType.Guid, containerPOSaveRequest.containerID);
                db.AddInParameter(dbCommand, "@No", DbType.String, containerPOSaveRequest.no);
                db.AddInParameter(dbCommand, "@Podc", DbType.String, containerPOSaveRequest.podc);
                db.AddInParameter(dbCommand, "@VendorID", DbType.Guid, containerPOSaveRequest.vendorID);
                db.AddInParameter(dbCommand, "@Vendor", DbType.String, containerPOSaveRequest.vendor);
                db.AddInParameter(dbCommand, "@BuyerID", DbType.Guid, containerPOSaveRequest.buyerID);
                db.AddInParameter(dbCommand, "@Buyer", DbType.String, containerPOSaveRequest.buyer);
                db.AddInParameter(dbCommand, "@FinalDestination", DbType.String, containerPOSaveRequest.finalDestination);
                db.AddInParameter(dbCommand, "@InWarehouseDate", DbType.DateTime, containerPOSaveRequest.inWarehouseDate);
                db.AddInParameter(dbCommand, "@OrderDate", DbType.DateTime, containerPOSaveRequest.orderDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, containerPOSaveRequest.updateDate);
                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, tempItemIDs);
                db.AddInParameter(dbCommand, "@ItemRelationIds", DbType.String, tempItemRelationIds);
                db.AddInParameter(dbCommand, "@ItemNos", DbType.String, tempItemNos);
                db.AddInParameter(dbCommand, "@ItemDescriptions", DbType.String, tempItemDescriptions);
                db.AddInParameter(dbCommand, "@ItemColors", DbType.String, tempItemColors);
                db.AddInParameter(dbCommand, "@ItemSizes", DbType.String, tempItemSizes);
                db.AddInParameter(dbCommand, "@ItemVolumes", DbType.String, tempItemVolumes);
                db.AddInParameter(dbCommand, "@ItemWeights", DbType.String, tempItemWeights);
                db.AddInParameter(dbCommand, "@ItemCartons", DbType.String, tempItemCartons);
                db.AddInParameter(dbCommand, "@ItemUnits", DbType.String, tempItemUnits);
                db.AddInParameter(dbCommand, "@ItemHTSCodes", DbType.String, tempItemHTSCodes);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, containerPOSaveRequest.saveByID);
                db.AddInParameter(dbCommand, "@ItemUpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "RelationID","UpdateDate" }, 
                    new string[] { "ID","POID", "RelationID", "UpdateDate" } });
                
                if (results == null
                    || results.Length < 2
                    || results[0].Items.Count == 0)
                {
                    return null;
                }

                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0].Copy());
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
        /// 获取箱下的PO列表
        /// </summary>
        /// <param name="containerID">箱ID</param>
        /// <param name="bookingId"></param>
        /// <returns>返回箱下的PO列表</returns>
        public CompositePoAndItems GetDTContainerPOList(Guid bookingId,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(bookingId, "bookingId");

            CompositePoAndItems result = new CompositePoAndItems();
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanContainerPOList");

                db.AddInParameter(dbCommand, "@BookingId", DbType.Guid, bookingId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                result.PoList = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DTBookingPOList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        No = b.Field<string>("PONO"),
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
                                                        Items = (from m in ds.Tables[2].AsEnumerable()
                                                                 where m.Field<Guid>("POID") == b.Field<Guid>("ID")
                                                                 select new DTPOItemList
                                                                 {
                                                                     ID = m.Field<Guid>("ID"),
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
                                                        IsDirty = false
                                                    }).ToList();

                //result.PoItemsList= (from m in ds.Tables[2].AsEnumerable()                                                       
                //                                       select new OceanPOItemList
                //                                       {
                //                                           ID = m.Field<Guid>("ID"),
                //                                           POID = m.Field<Guid>("POID"),
                //                                           No = m.Field<string>("No"),
                //                                           Description = m.Field<string>("Description"),
                //                                           Color = m.Field<string>("Color"),
                //                                           Size = m.Field<string>("Size"),
                //                                           Volume = m.Field<decimal>("Volume"),
                //                                           Weight = m.Field<decimal>("Weight"),
                //                                           Cartons = m.Field<int>("Cartons"),
                //                                           Units = m.Field<int>("Units"),
                //                                           HTSCode = m.Field<string>("HTSCode"),
                //                                           CreateByID = m.Field<Guid>("CreateByID"),
                //                                           CreateByName = m.Field<string>("CreateByName"),
                //                                           CreateDate = m.Field<DateTime>("CreateDate"),
                //                                           UpdateDate = m.Field<DateTime?>("UpdateDate"),
                //                                           IsDirty = false,
                //                                           // Cartons 
                //                                       }).ToList();

                result.POContainerRelationsList = (from p in ds.Tables[1].AsEnumerable()
                                                         select new POContainerRelation
                                                         {
                                                             ContainerId = p.Column<Guid>("OceanContainerId"),
                                                             PoId = p.Column<Guid>("PoId")
                                                         }).ToList();

                result.PoItemContainerRelationsList = (from q in ds.Tables[3].AsEnumerable()
                                                               select new PoItemContainerRelation
                                                               {
                                                                   PoId = q.Column<Guid>("PoId"),
                                                                   ItemId = q.Column<Guid>("ItemId"),
                                                                   ContainerId = q.Column<Guid>("OceanContainerId")
                                                               }).ToList();

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
        /// 保存PO和箱子的多对多关联关系
        /// </summary>
        /// <param name="ContainerIds"></param>
        /// <param name="poIds"></param>
        /// <param name="saveById"></param>
        public void SaveDTContainerPOInfoByIds(Guid[] containerIds, Guid[] poIds, Guid saveByID, bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            //ArgumentHelper.AssertArrayLengthMatch(containerIds, poIds);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanContainerPOInfo");

                string tempcontainerIds = containerIds.Join();
                string temppoIds = poIds.Join();

                db.AddInParameter(dbCommand, "@ContainerIds", DbType.String, tempcontainerIds);
                db.AddInParameter(dbCommand, "@PoIds", DbType.String, temppoIds);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

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

        /// <summary>
        /// 保存Item和箱子的多对多关联关系
        /// </summary>
        /// <param name="containerIds"></param>
        /// <param name="itemIds"></param>
        /// <param name="saveById"></param>
        public void SaveDTContainerPOItemInfo(Guid[] containerIds, Guid[] itemIds, Guid saveByID,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            //ArgumentHelper.AssertArrayLengthMatch(containerIds, itemIds);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanContainerPOItemInfo");

                string tempcontainerIds = containerIds.Join();
                string tempitemIds = itemIds.Join();

                db.AddInParameter(dbCommand, "@ContainerIds", DbType.String, tempcontainerIds);
                db.AddInParameter(dbCommand, "@ItemIds ", DbType.String, tempitemIds);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

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

        #region Cargo

        /// <summary>
        /// 删除箱下货物信息
        /// </summary>
        /// <param name="cargoID">货物ID</param>
        /// <param name="removeByID">删除人</param>
        public void RemoveDTContainerCargoInfo(
            Guid cargoID,
            Guid removeByID,
            DateTime? updateDate,
            bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(cargoID, "cargoID");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanMBLContaierInfo");

                string tempUpdateDates = updateDate == null ? string.Empty : updateDate.Value.ToString();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, cargoID.ToString());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

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

        /// <summary>
        /// 保存箱的MBL货物信息(只在装箱中用到)
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="containerID">箱ID</param>
        /// <param name="mblIDs">MBL ID 要建立关联的MBL ID</param>
        /// <param name="mblNos">MBLNO 如果输入的主提单号不存在，那么根据订舱信息和装箱信息新建主提单</param>
        /// <param name="ids">ID </param>
        /// <param name="marks">唛头</param>
        /// <param name="commodities">品名</param>
        /// <param name="quantities">数量</param>
        /// <param name="quantityUnitIDs">数量单位</param>
        /// <param name="weights">重量</param>
        /// <param name="weightUnitIDs">重量单位</param>
        /// <param name="measurements">体积</param>
        /// <param name="measurementUnitIDs">体积单位</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveDTMBLContainerCargoInfo(CargoSaveRequest cargoSaveRequest,bool IsEnglish
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(cargoSaveRequest.containerID, "containerID");
            ArgumentHelper.AssertGuidNotEmpty(cargoSaveRequest.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                 cargoSaveRequest.ids,
                 cargoSaveRequest.marks,
                 cargoSaveRequest.commodities,
                 cargoSaveRequest.quantities,
                 cargoSaveRequest.quantityUnitIDs,
                 cargoSaveRequest.weights,
                 cargoSaveRequest.weightUnitIDs,
                 cargoSaveRequest.mblIDs,
                 cargoSaveRequest.mblNos,
                 cargoSaveRequest.measurements,
                 cargoSaveRequest.measurementUnitIDs);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanContainerCargoInfo");

                string tempBLIds = cargoSaveRequest.mblIDs.Join();
                string tempBLNos = cargoSaveRequest.mblNos.Join();
                string tempIds = cargoSaveRequest.ids.Join();
                string tempMarks = cargoSaveRequest.marks.Join();
                string tempCommodities = cargoSaveRequest.commodities.Join();
                string tempQuantities = cargoSaveRequest.quantities.Join();
                string tempQuantitiesUnits = cargoSaveRequest.quantityUnitIDs.Join();
                string tempWeights = cargoSaveRequest.weights.Join(3);
                string tempWeightUnits = cargoSaveRequest.weightUnitIDs.Join();
                string tempMeasurements = cargoSaveRequest.measurements.Join(3);
                string tempMeasurementUnits = cargoSaveRequest.measurementUnitIDs.Join();
                string tempUpdateDates = cargoSaveRequest.updateDates.Join();

                db.AddInParameter(dbCommand, "@ContainerID", DbType.Guid, cargoSaveRequest.containerID);

                db.AddInParameter(dbCommand, "@MBLIDs", DbType.String, tempBLIds);
                db.AddInParameter(dbCommand, "@MBLNOs", DbType.String, tempBLNos);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@Marks", DbType.String, tempMarks);
                db.AddInParameter(dbCommand, "@Commodities", DbType.String, tempCommodities);
                db.AddInParameter(dbCommand, "@Quantities", DbType.String, tempQuantities);
                db.AddInParameter(dbCommand, "@QuantityUnitIDs", DbType.String, tempQuantitiesUnits);
                db.AddInParameter(dbCommand, "@Weights", DbType.String, tempWeights);
                db.AddInParameter(dbCommand, "@WeightUnitIDs", DbType.String, tempWeightUnits);
                db.AddInParameter(dbCommand, "@Measurements", DbType.String, tempMeasurements);
                db.AddInParameter(dbCommand, "@MeasurementUnitIDs", DbType.String, tempMeasurementUnits);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, cargoSaveRequest.saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "OceanMBLID", "UpdateDate" });
                
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
        /// 
        /// </summary>
        /// <param name="transactionId"></param>
        public void CommitTransaction(Guid transactionId)
        {
            ICP.Framework.CommonLibrary.Helper.TransactionManager.Instance.GetTransactionScope(transactionId).Complete();
        }

        /// <summary>
        /// 获取箱下的货物列表
        /// </summary>
        /// <param name="containerID">箱ID</param>
        /// <returns>返回该提单箱下的货物列表</returns>
        public List<DTContainerCargoList> GetDTContainerCargoList(Guid containerID,bool IsEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(containerID, "containerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetInternalContainerCargoList");

                db.AddInParameter(dbCommand, "@ContainerID", DbType.Guid, containerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean,IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DTContainerCargoList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new DTContainerCargoList
                                                         {
                                                             ID = b.Field<Guid>("ID"),
                                                             //BLID = b.Field<Guid>("BLID"),
                                                             //OceanContainerID = b.Field<Guid>("OceanContainerID"),
                                                             Marks = b.Field<string>("Marks"),
                                                             Commodity = b.Field<string>("Commodity"),
                                                             Quantity = b.Field<int>("Quantity"),
                                                             QuantityUnitID = b.Field<Guid>("QuantityUnitID"),
                                                             QuantityUnitName = b.Field<string>("QuantityUnitName"),
                                                             Weight = b.Field<decimal>("Weight"),
                                                             WeightUnitID = b.Field<Guid>("WeightUnitID"),
                                                             WeightUnitName = b.Field<string>("WeightUnitName"),
                                                             Measurement = b.Field<decimal>("Measurement"),
                                                             MeasurementUnitID = b.Field<Guid>("MeasurementUnitID"),
                                                             MeasurementUnitName = b.Field<string>("MeasurementUnitName"),
                                                             CreateByID = b.Field<Guid>("CreateByID"),
                                                             CreateByName = b.Field<string>("CreateByName"),
                                                             CreateDate = b.Field<DateTime>("CreateDate"),
                                                             UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                             IsDirty = false,
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
    }
}
