using ICP.FCM.Common.ServiceComponent.JSONObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Platform;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// CSP 业务映射
    /// </summary>
    public partial class FCMCommonService
    {
        #region 保存映射信息
        /// <summary>
        /// 保存映射信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        public SingleResult SaveMappingInfo(SaveRequestBusinessMapping saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.ID, "ID");
            ArgumentHelper.AssertStringNotEmpty(saveRequest.MapType, "MapType");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[csp].[uspSaveBusinessMappingData]");
                //设置60秒超时
                dbCommand.CommandTimeout = 60;

                #region 构建参数
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@MapID", DbType.Int32, saveRequest.MapID);
                db.AddInParameter(dbCommand, "@MapType", DbType.String, saveRequest.MapType);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                #endregion

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID" });
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

        #region 保存CSP订舱单
        /// <summary>
        /// 保存CSP订舱单
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        public void SaveBookingInfoForCSP(SaveRequestBookingDelegate saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[csp].[uspGetCSPBookingMapping]");
                dbCommand.CommandTimeout = 60;
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, saveRequest.ItemIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                List<BookingInfoForCSPAPI> bookingInfos = (from item in ds.Tables[0].AsEnumerable()
                                                           select new BookingInfoForCSPAPI
                                                           {
                                                               ItemID = item.Column<Guid>("ItemID"),
                                                               id = item.Column<int>("BookingMapID"),
                                                               bookingNo = item.Column<string>("BookingNo"),
                                                               name = item.Column<string>("BookingName"),
                                                               freightMethodType = (CSP_FREIGHTMETHODTYPE)item.Column<byte>("FreightMethodType"),
                                                               shipmentType = (CSP_SHIPMENTTYPE)item.Column<byte>("ShipmentType"),
                                                               incotermsId = item.Column<int>("IncoTermMapID"),
                                                               tradeType = (CSP_TRADETYPE)item.Column<byte>("TradeType"),
                                                               creationTime = item.Column<DateTime>("BookingDate"),
                                                               isInsurance = item.Column<bool>("IsInsurance"),
                                                               originIsRequireTruck = item.Column<bool>("IsTruck"),
                                                               isDeclaration = item.Column<bool>("IsDeclaration"),
                                                               originPortId = item.Column<int>("POLMapID"),
                                                               originAddressId = item.Column<int?>("POLAddressMapID"),
                                                               originAddress = item.Column<string>("POLAddress"),
                                                               destinationPortId = item.Column<int>("PODMapID"),
                                                               destinationAddressId = item.Column<int?>("PODAddressMapID"),
                                                               destinationAddress = item.Column<string>("PODAddress"),
                                                               quantity = item.Column<int>("Quantity"),
                                                               quantityUnitId = item.Column<int>("QuantityUnitMapID"),
                                                               weight = item.Column<decimal>("Weight"),
                                                               weightUnitId = item.Column<int>("WeightUnitMapID"),
                                                               volume = item.Column<decimal>("Measurement"),
                                                               volumeUnitId = item.Column<int>("MeasurementUnitMapID"),
                                                           }).ToList();
                List<EventCode> CacheEventCodes = GetEventCodeList(saveRequest.OperationType);

                foreach (BookingInfoForCSPAPI bookingInfo in bookingInfos)
                {
                    #region 触发事件
                    string eventCodeStr = string.Empty;
                    if (bookingInfo.freightMethodType == CSP_FREIGHTMETHODTYPE.Ocean && bookingInfo.tradeType == CSP_TRADETYPE.General)
                    {
                        eventCodeStr = "OBKD";
                    }
                    if (bookingInfo.freightMethodType == CSP_FREIGHTMETHODTYPE.Air && bookingInfo.tradeType == CSP_TRADETYPE.General)
                    {
                        eventCodeStr = "ABKD";
                    }
                    if (bookingInfo.tradeType == CSP_TRADETYPE.FBA)
                    {
                        eventCodeStr = "FBABKD";
                    }
                    if (bookingInfo.tradeType == CSP_TRADETYPE.FBM)
                    {
                        eventCodeStr = "FBMBKD";
                    }
                    if (!eventCodeStr.IsNullOrEmpty())
                    {
                        EventCode eventCode = CacheEventCodes.Where(fItem => eventCodeStr == fItem.Code).SingleOrDefault();
                        if (eventCode != null)
                        {
                            EventObjects eventObjects = new EventObjects()
                            {
                                Id = Guid.Empty,
                                Code = eventCode.Code,
                                OperationID = saveRequest.OperationID,
                                OperationType = saveRequest.OperationType,
                                IsShowCustomer = true,
                                IsShowAgent = true,
                                Subject = eventCode.Subject,
                                Description = eventCode.Subject + ":" + bookingInfo.bookingNo,
                                FormType = FormType.Unknown,
                                FormID = bookingInfo.ItemID,
                                Priority = MemoPriority.Low,
                                Type = MemoType.Memo,
                                UpdateBy = saveRequest.SaveBy,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                OccurrenceTime = DateTime.Now,
                            };
                            SaveMemoInfo(eventObjects);
                        }
                    }
                    #endregion
                }
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

        private void ChangeBooingStatus(SaveRequestBookingDelegate saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[csp].[uspGetCSPBookingMapping]");
                dbCommand.CommandTimeout = 60;
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, saveRequest.ItemIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                List<BookingStatusForCSPAPI> saveRequestForAPIs = (from item in ds.Tables[0].AsEnumerable()
                                                                   select new BookingStatusForCSPAPI
                                                                 {
                                                                     id = item.Column<int>("BookingMapID"),
                                                                     newStatus = CSP_BOOKING_STATUS.ConfirmCancelled,
                                                                 }).ToList();
                foreach (BookingStatusForCSPAPI saveRequestForAPI in saveRequestForAPIs)
                {
                    SaveBookingStatusForCSPAPI(saveRequestForAPI);
                }
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

        #region 保存CSP运单
        /// <summary>
        /// 保存CSP运单
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        public void SaveShipmentInfoForCSP(SaveRequestShipmentInfoForCSP saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPShipmentMapping]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                ShipmentInfoForCSPAPI saveRequestForAPI = (from item in ds.Tables[0].AsEnumerable()
                                                           select new ShipmentInfoForCSPAPI
                                                           {
                                                               id = item.Column<int>("ShipmentMapID"),
                                                               soNo = item.Column<string>("SONO"),
                                                               freightMethodType = (CSP_FREIGHTMETHODTYPE)item.Column<byte>("FreightMethodType"),
                                                               shipmentType = (CSP_SHIPMENTTYPE)item.Column<byte>("ShipmentType"),
                                                               tradeType = (CSP_TRADETYPE)item.Column<byte>("TradeType"),
                                                               transportClauses = item.Column<int>("TransportClauseMapID"),
                                                               incotermsId = item.Column<int>("IncoTermMapID"),
                                                               vessel = item.Column<string>("VesselVoyage"),
                                                               carrierCustomerId = item.Column<int?>("CarrierMapID"),
                                                               truckCustomerId = item.Column<int?>("TruckCustomerMapID"),
                                                               originPortId = item.Column<int?>("POLMapID"),
                                                               destinationPortId = item.Column<int?>("PODMapID"),
                                                               siCutOffDate = item.IsNull("SICutOffDate") ? "" : item.Column<DateTime>("SICutOffDate").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               vgmCutOffDate = item.IsNull("VGMCutOffDate") ? "" : item.Column<DateTime>("VGMCutOffDate").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               cyCutOffTime = item.IsNull("CYCutOffTime") ? "" : item.Column<DateTime>("CYCutOffTime").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               estDepatureOrginPortDate = item.IsNull("ETDForPOL") ? "" : item.Column<DateTime>("ETDForPOL").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               actualDepatureOrginPortDate = item.IsNull("ATDForPOL") ? "" : item.Column<DateTime>("ATDForPOL").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               estArrivalDestinationPortDate = item.IsNull("ETAForPOD") ? "" : item.Column<DateTime>("ETAForPOD").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               actualArrivalDestinationPortDate = item.IsNull("ATAForPOD") ? "" : item.Column<DateTime>("ATAForPOD").ToString("yyyy-MM-dd HH:mm:ss"),
                                                               shipmentBookings = new List<BookingInfoForCSPAPI>(),
                                                           }).SingleOrDefault();
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    saveRequestForAPI.serviceUsers = (from item in ds.Tables[1].AsEnumerable()
                                                      select new ShipmentContactForCSPAPI
                                                      {
                                                          userId = item.Column<int>("UserMapID"),
                                                          //ContactUserID = item.Column<Guid>("UserID"),
                                                          serviceUserType = (CSP_CONTACTTYPE)item.Column<byte>("PositionType"),
                                                      }).ToList();
                }

                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    saveRequestForAPI.shipmentBookings = (from item in ds.Tables[2].AsEnumerable()
                                                          select new BookingInfoForCSPAPI
                                                          {
                                                              bookingId = item.Column<int>("BookingMapID"),
                                                              customerId = item.Column<int>("CustomerMapID"),
                                                              shipperCustomerId = item.Column<int>("ShipperMapID"),
                                                              consigneeCustomerId = item.Column<int>("ConsigneeMapID"),
                                                              quantity = item.Column<int>("Quantity"),
                                                              quantityUnitId = item.Column<int>("QuantityUnitMapID"),
                                                              weight = item.Column<decimal>("Weight"),
                                                              weightUnitId = item.Column<int>("WeightUnitMapID"),
                                                              volume = item.Column<decimal>("Measurement"),
                                                              volumeUnitId = item.Column<int>("MeasurementUnitMapID"),
                                                          }).ToList();
                }
                ShipmentInfoForCSPAPI responseResult = SaveShipmentInfoForCSPAPI(saveRequestForAPI);
                SaveMappingInfo(new SaveRequestBusinessMapping()
                {
                    ID = saveRequest.OperationID,
                    MapID = responseResult.id,
                    MapType = "fcm.OperationInfo",
                    SaveBy = saveRequest.SaveBy,
                    UpdateDate = DateTime.Now
                });
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

        #region 保存CSP箱
        /// <summary>
        /// 保存CSP箱
        /// </summary>
        /// <param name="saveRequest"></param>
        public void SaveShipmentContainerForCSP(SaveRequestShipmentContainerForCSP saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPShipmentContainerMapping]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@ContainerIDs", DbType.String, saveRequest.OperationContainerIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                List<ShipmentContainerForCSPAPI> srContainerForAPIs = (from item in ds.Tables[0].AsEnumerable()
                                                                       select new ShipmentContainerForCSPAPI
                                                                       {
                                                                           id = item.Column<int>("ShipmentContainerMapID"),
                                                                           ShipmentContainerID = item.Column<Guid>("ShipmentContainerID"),
                                                                           shipmentId = item.Column<int>("ShipmentMapID"),
                                                                           containerNo = item.Column<string>("ContainerNO"),
                                                                           containerTypeId = item.Column<int>("ContainerTypeMapID"),
                                                                           sealNo = item.Column<string>("SealNo"),
                                                                       }).ToList();


                foreach (ShipmentContainerForCSPAPI item in srContainerForAPIs)
                {
                    ShipmentContainerForCSPAPI responseResult = SaveShipmentContainerForCSPAPI(item);
                    SaveMappingInfo(new SaveRequestBusinessMapping()
                    {
                        ID = item.ShipmentContainerID,
                        MapID = responseResult.id,
                        MapType = "fcm.OperationContainer",
                        SaveBy = saveRequest.SaveBy,
                        UpdateDate = DateTime.Now
                    });
                }
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

        #region 保存CSP提单
        /// <summary>
        /// 保存CSP提单
        /// </summary>
        /// <param name="saveRequest"></param>
        public void SaveShipmentItemForCSP(SaveRequestShipmentItemForCSP saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPShipmentItemMapping]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@BLID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                ShipmentItemForCSPAPI shipmentItem = (from item in ds.Tables[0].AsEnumerable()
                                                      select new ShipmentItemForCSPAPI
                                                      {
                                                          id = item.Column<int>("BLMapID"),
                                                          shipmentId = item.Column<int>("OperationMapID"),
                                                          billOfLadingNo = item.Column<string>("BLNo"),
                                                          shipperCustomerId = item.Column<int>("ShipperMapID"),
                                                          consigneeCustomerId = item.Column<int>("ConsigneeMapID"),
                                                          totalQuantity = item.Column<int>("Quantity"),
                                                          totalQuantityUnitId = item.Column<int>("QuantityUnitMapID"),
                                                          totalWeight = item.Column<decimal>("Weight"),
                                                          totalWeightUnitId = item.Column<int>("WeightUnitMapID"),
                                                          totalVolume = item.Column<decimal>("Measurement"),
                                                          totalVolumeUnitId = item.Column<int>("MeasurementUnitMapID"),
                                                          estTruckDeliveryOrignDate = item.IsNull("ETDForPOL") ? "" : item.Column<DateTime>("ETDForPOL").ToString("yyyy-MM-dd HH:mm:ss"),
                                                          actualTruckDeliveryOrignDate = item.IsNull("ATDForPOL") ? "" : item.Column<DateTime>("ATDForPOL").ToString("yyyy-MM-dd HH:mm:ss"),
                                                          estPickUpTruckDestinationDate = item.IsNull("ETDForPOD") ? "" : item.Column<DateTime>("ETDForPOD").ToString("yyyy-MM-dd HH:mm:ss"),
                                                          actualPickUpTruckDestinationDate = item.IsNull("APTDForPOD") ? "" : item.Column<DateTime>("APTDForPOD").ToString("yyyy-MM-dd HH:mm:ss"),
                                                          estTruckDeliveryDate = item.IsNull("ETD") ? "" : item.Column<DateTime>("ETD").ToString("yyyy-MM-dd HH:mm:ss"),
                                                          actualTruckDeliveryDate = item.IsNull("ATD") ? "" : item.Column<DateTime>("ATD").ToString("yyyy-MM-dd HH:mm:ss"),
                                                          quoteEnquiryId = 0,
                                                      }).SingleOrDefault();
                ShipmentItemForCSPAPI responseResult = SaveShipmentItemForCSPAPI(shipmentItem);
                SaveMappingInfo(new SaveRequestBusinessMapping()
                {
                    ID = saveRequest.ID,
                    MapID = responseResult.id,
                    MapType = "fcm.BillOfLading",
                    SaveBy = saveRequest.SaveBy,
                    UpdateDate = DateTime.Now
                });
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

        #region 保存CSP提单箱
        /// <summary>
        /// 保存CSP提单箱
        /// </summary>
        /// <param name="saveRequest"></param>
        public void SaveShipmentItemContainerForCSP(SaveRequestShipmentItemContainerForCSP saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPShipmentItemContainerMapping]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@BLID", DbType.Guid, saveRequest.BLID);
                db.AddInParameter(dbCommand, "@BLContainerIDs", DbType.String, saveRequest.BLContainerIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }

                List<ShipmentItemContainerForCSPAPI> srItemContainerForAPIs = (from item in ds.Tables[0].AsEnumerable()
                                                                               select new ShipmentItemContainerForCSPAPI
                                                                               {
                                                                                   id = item.Column<int>("BLContainerMapID"),
                                                                                   BLContainerID = item.Column<Guid>("BLContainerID"),
                                                                                   shipmentItemId = item.Column<int>("BLMapID"),
                                                                                   shipmentContainerId = item.Column<int>("ShipmentContainerMapID"),
                                                                                   BusinessContainerID = item.Column<Guid>("ShipmentContainerID"),
                                                                                   quantity = item.Column<int>("Quantity"),
                                                                                   quantityUnitId = item.Column<int>("QuantityUnitMapID"),
                                                                                   weight = item.Column<decimal>("Weight"),
                                                                                   weightUnitId = item.Column<int>("WeightUnitMapID"),
                                                                                   volume = item.Column<decimal>("Measurement"),
                                                                                   volumeUnitId = item.Column<int>("MeasurementUnitMapID"),
                                                                               }).ToList();

                foreach (ShipmentItemContainerForCSPAPI item in srItemContainerForAPIs)
                {
                    ShipmentItemContainerForCSPAPI responseResult = SaveShipmentItemContainerForCSPAPI(item);
                    SaveMappingInfo(new SaveRequestBusinessMapping()
                    {
                        ID = item.BLContainerID,
                        MapID = responseResult.id,
                        MapType = "fcm.BillOfLadingContainer",
                        SaveBy = saveRequest.SaveBy,
                        UpdateDate = DateTime.Now
                    });
                }

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

        #region 保存CSP事件
        /// <summary>
        /// 保存CSP事件
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private void SaveShipmentEventForCSP(EventObjects saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPShipmentEventMapping]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@EventID", DbType.Guid, saveRequest.Id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                ShipmentEventInfoForCSPAPI shipmentEvent = (from b in ds.Tables[0].AsEnumerable()
                                                            select new ShipmentEventInfoForCSPAPI
                                                            {
                                                                id = b.Column<int>("EventMapID"),
                                                                businessId = b.Column<int>("OperationMapID"),
                                                                businessEventType = (CSP_EVENTFORMTYPE)b.Column<byte>("FormType"),
                                                                happenNode = b.Column<int>("HappenNode"),
                                                                isException = b.Column<bool>("IsException"),
                                                                eventCode = saveRequest.Code,
                                                                subject = saveRequest.Subject,
                                                                happenTime = saveRequest.CreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                description = saveRequest.Description,
                                                                details = saveRequest.Description,
                                                                type = ServiceInterface.CSP_SHIPMENT_EVENT_TYPE.ProcedureEvent,
                                                            }).SingleOrDefault();

                ShipmentEventInfoForCSPAPI responseResult = SaveShipmentEventForCSPAPI(shipmentEvent);
                SaveMappingInfo(new SaveRequestBusinessMapping()
                {
                    ID = saveRequest.Id,
                    MapID = responseResult.id,
                    MapType = "fcm.OperationEvent",
                    SaveBy = saveRequest.UpdateBy,
                    UpdateDate = DateTime.Now
                });
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

        #region 保存CSP采购单信息
        /// <summary>
        /// 保存CSP采购单信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private List<PurchaseOrderItemForCSPAPI> SavePurchaseOrderItemForCSP(SaveRequestPurchaseOrderItem saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPPurchaseOrderItemMapping]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Byte, saveRequest.OperationType);
                db.AddInParameter(dbCommand, "@PurchaseOrderIDs", DbType.String, saveRequest.PurchaseOrderIDs.Join());
                db.AddInParameter(dbCommand, "@PurchaseOrderItemIDs", DbType.String, saveRequest.IDs.Join());
                db.AddInParameter(dbCommand, "@PurchaseOrderNOs", DbType.String, saveRequest.PurchaseOrderNOs.Join());
                db.AddInParameter(dbCommand, "@ProductNames", DbType.String, saveRequest.ProductNames.Join());
                db.AddInParameter(dbCommand, "@BillOfLadingIDs", DbType.String, saveRequest.BillOfLadingIDs.Join());
                db.AddInParameter(dbCommand, "@BillOfLadingNOs", DbType.String, saveRequest.BillOfLadingNOs.Join());
                db.AddInParameter(dbCommand, "@ContainerIDs", DbType.String, saveRequest.ContainerIDs.Join());
                db.AddInParameter(dbCommand, "@ContainerNOs", DbType.String, saveRequest.ContainerNOs.Join());
                db.AddInParameter(dbCommand, "@StockKeepingUnits", DbType.String, saveRequest.StockKeepingUnits.Join());
                db.AddInParameter(dbCommand, "@ManufacturerPartNumbers", DbType.String, saveRequest.ManufacturerPartNumbers.Join());
                db.AddInParameter(dbCommand, "@CartonCounts", DbType.String, saveRequest.CartonCounts.Join());
                db.AddInParameter(dbCommand, "@Quantitys", DbType.String, saveRequest.Quantitys.Join());
                db.AddInParameter(dbCommand, "@UnitCostPrices", DbType.String, saveRequest.UnitCostPrices.Join());
                db.AddInParameter(dbCommand, "@Weights", DbType.String, saveRequest.Weights.Join());
                db.AddInParameter(dbCommand, "@Volumes", DbType.String, saveRequest.Volumes.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return new List<PurchaseOrderItemForCSPAPI>();
                }
                List<PurchaseOrderItemForCSPAPI> saveData = (from b in ds.Tables[0].AsEnumerable()
                                                            select new PurchaseOrderItemForCSPAPI
                                                            {
                                                                orderId = b.Column<int>("PurchaseOrderID"),
                                                                itemId = b.Column<int>("PurchaseOrderItemID"),
                                                                orderNumber = b.Column<string>("PurchaseOrderNO"),
                                                                productName = b.Column<string>("ProductName"),
                                                                shipmentItemContainerId = b.Column<int>("BillOfLadingContainerMapID"),
                                                                sku = b.Column<string>("StockKeepingUnit"),
                                                                mpn = b.Column<string>("ManufacturerPartNumber"),
                                                                cartons = b.Column<int>("CartonCount"),
                                                                units = b.Column<decimal>("Quantity"),
                                                                unitCost = b.Column<decimal>("UnitCostPrice"),
                                                                weight = b.Column<decimal>("Weight"),
                                                                volume = b.Column<decimal>("Volume"),
                                                            }).ToList();

                return SavePurchaseOrderItemForCSPAPI(saveData);
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

        #region API Method
        /// <summary>
        /// 获取舱单列表(等待下载业务)
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        private List<BookingInfoForCSPAPI> GetBookingInfoForCSPAPIList(SearchParameterBookingDelegate searchParameter)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("IsEnglish={0}", IsEnglish);
            if (searchParameter.BookingStatus != CSP_BOOKING_STATUS.Unknown)
            {
                sb.AppendFormat("&BookingStatus={0}", searchParameter.BookingStatus.GetHashCode());
            }
            if (searchParameter.FreightMethodType != CSP_FREIGHTMETHODTYPE.Unknown)
            {
                sb.AppendFormat("&CSP_FREIGHTMETHODTYPE={0}", searchParameter.FreightMethodType.GetHashCode());
            }
            if (searchParameter.ShipmentType != CSP_SHIPMENTTYPE.Unknown)
            {
                sb.AppendFormat("&ShipmentType={0}", searchParameter.ShipmentType.GetHashCode());
            }
            if (!searchParameter.SearchKey.IsNullOrEmpty())
            {
                sb.AppendFormat("&SearchKey={0}", searchParameter.SearchKey);
            }
            if (!searchParameter.Sorting.IsNullOrEmpty())
            {
                sb.AppendFormat("&Sorting={0}", searchParameter.Sorting);
            }
            if (searchParameter.MaxResultCount > 0)
            {
                sb.AppendFormat("&MaxResultCount={0}", searchParameter.MaxResultCount);
            }
            if (searchParameter.SkipCount > 0)
            {
                sb.AppendFormat("&SkipCount={0}", searchParameter.SkipCount);
            }
            string result = PlatformAPIHelper.Execute(CSPUserID, "CSP/Booking/GetAllListForIcp", HTTPMETHOD.GET, sb.ToString());
            BookingInfoForCSPAPIList resultItem = JSONSerializerHelper.DeserializeFromJson<BookingInfoForCSPAPIList>(result);
            List<BookingInfoForCSPAPI> bookings = resultItem.Result.Items;
            return bookings;
        }
        /// <summary>
        /// 更新舱单(Create)
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private BookingInfoForCSPAPI SaveBookingInfoForCSPAPI(BookingInfoForCSPAPI saveRequest)
        {
            string apiMethod = "CSP/Booking/Create";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            if (saveRequest.id != 0)
            {
                apiMethod = "CSP/Booking/UpdateForIcp";
            }
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            BookingInfoForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<BookingInfoForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }
        /// <summary>
        /// 保存舱单状态
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private void SaveBookingStatusForCSPAPI(BookingStatusForCSPAPI saveRequest)
        {
            string apiMethod = "/CSP/Booking/ChangeBookingStatus";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private ShipmentInfoForCSPAPI SaveShipmentInfoForCSPAPI(ShipmentInfoForCSPAPI saveRequest)
        {
            string apiMethod = "CSP/Shipment/Create";
            if (saveRequest.id != 0)
            {
                apiMethod = "CSP/Shipment/UpdateForIcp";
            }
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, HTTPMETHOD.POST, apiParameter);
            ShipmentForCSPItem cspItem = JSONSerializerHelper.DeserializeFromJson<ShipmentForCSPItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private ShipmentContainerForCSPAPI SaveShipmentContainerForCSPAPI(ShipmentContainerForCSPAPI saveRequest)
        {
            string apiMethod = "CSP/Shipment/CreateOrUpdateShipmentContainer";
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, HTTPMETHOD.POST, apiParameter);
            ShipmentContainerForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<ShipmentContainerForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private ShipmentItemForCSPAPI SaveShipmentItemForCSPAPI(ShipmentItemForCSPAPI saveRequest)
        {
            string apiMethod = "CSP/ShipmentItem/Create";
            if (saveRequest.id != 0)
            {
                apiMethod = "CSP/ShipmentItem/Update";
            }
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, HTTPMETHOD.POST, apiParameter);
            ShipmentItemForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<ShipmentItemForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private ShipmentItemContainerForCSPAPI SaveShipmentItemContainerForCSPAPI(ShipmentItemContainerForCSPAPI saveRequest)
        {
            string apiMethod = "CSP/ShipmentItem/CreateOrUpdateShipmentItemContainer";
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, HTTPMETHOD.POST, apiParameter);
            ShipmentItemContainerForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<ShipmentItemContainerForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }
        /// <summary>
        /// 保存CSP运单事件
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private ShipmentEventInfoForCSPAPI SaveShipmentEventForCSPAPI(ShipmentEventInfoForCSPAPI saveRequest)
        {
            string apiMethod = "CSP/ShipmentEvent/Create";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            if (saveRequest.id != 0)
            {
                apiMethod = "CSP/ShipmentEvent/Update";
                httpMethod = HTTPMETHOD.POST;
            }

            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            ShipmentEventForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<ShipmentEventForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }
        /// <summary>
        /// 保存CSP采购单明细关联
        /// </summary>
        /// <param name="saveRequests"></param>
        /// <returns></returns>
        private List<PurchaseOrderItemForCSPAPI> SavePurchaseOrderItemForCSPAPI(List<PurchaseOrderItemForCSPAPI> saveRequests)
        {
            string apiMethod = "/CSP/PurchaseOrder/CreateOrUpdateForIcp";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequests);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            PurchaseOrderItemForCSPAPIList cspItem = JSONSerializerHelper.DeserializeFromJson<PurchaseOrderItemForCSPAPIList>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result.Items;
        }
        /// <summary>
        /// 保存CSP采购单明细关联
        /// </summary>
        /// <param name="saveRequests"></param>
        /// <returns></returns>
        private List<PurchaseOrderItemForCSPAPI> SearchOrderItemByShipmentInfo(SearchParameterOrderItemByShipment searchParameter)
        {
            string apiMethod = "/CSP/ShipmentItem/GetOrderItemInContainers";
            HTTPMETHOD httpMethod = HTTPMETHOD.GET;
            string apiParameter = JSONSerializerHelper.SerializeToJson(searchParameter);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            PurchaseOrderItemForCSPAPIList cspItem = JSONSerializerHelper.DeserializeFromJson<PurchaseOrderItemForCSPAPIList>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result.Items;
        }
        #endregion

        public List<CSPPacklist> ImportCSPOrder()
        {
            throw new NotImplementedException();
        }
    }
}
