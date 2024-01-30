using ICP.Common.ServiceInterface.CompositeObjects;
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
    /// CSP 订舱单
    /// </summary>
    public partial class FCMCommonService
    {
        List<BookingInfoForCSPAPI> _CacheCSPBookingDelegateList = new List<BookingInfoForCSPAPI>();
        /// <summary>
        /// 生成测试舱单数据
        /// </summary>
        public void GenerateTestBookingData()
        {
            BookingInfoForCSPAPI bookingInfo = new BookingInfoForCSPAPI()
            {
                name = "FBA Express",
                freightMethodType = CSP_FREIGHTMETHODTYPE.Unknown,
                fbaFreightMethodId = 122,
                tradeType = CSP_TRADETYPE.FBA,
                shipmentType = CSP_SHIPMENTTYPE.LCL,
                incotermsId = 324,
                originIsRequireTruck = true,
                isDeclaration = false,
                isInsurance = false,
                customerId = 32746,
                shipperCustomerId = 32746,
                consigneeCustomerId = 32747,
            };
            SaveBookingInfoForCSPAPI(bookingInfo);
        }

        /// <summary>
        /// 获取CSP订舱单业务
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        public List<BookingDelegateList> GetBookingDelegateListBySearch(SearchParameterBookingDelegate searchParameter)
        {
            try
            {
                List<BookingInfoForCSPAPI> bookings = GetBookingInfoForCSPAPIList(searchParameter);
                _CacheCSPBookingDelegateList = bookings;
                return ConvertCSPBookingAPIToDelegate(bookings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 下载CSP舱单
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns>舱单委托列表</returns>
        public List<BookingDelegate> DownloadBookingDelegate(SearchParameterDownloadBookingDelegate searchParameter)
        {
            try
            {
                if (_CacheCSPBookingDelegateList == null || _CacheCSPBookingDelegateList.Count()<=0)
                {
                    throw new Exception("缓存数据丢失，请重新查询业务数据");
                }

                List<BookingInfoForCSPAPI> bookings = new List<BookingInfoForCSPAPI>();
                foreach (int item in searchParameter.BusinessID)
                {
                    BookingInfoForCSPAPI itemBooking = _CacheCSPBookingDelegateList.SingleOrDefault(fItem => fItem.id == item);
                    if(itemBooking!=null)
                    {
                        bookings.Add(itemBooking);
                    }
                }
                return GetCSPBookingAPIMappingData(bookings);
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
        /// 获取下载业务列表
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        public List<BookingDelegate> GetBookingDelegateList(SearchParameterBookingDelegate searchParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetBookingDelegateList]");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, searchParameter.OperationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                List<BookingDelegate> result = new List<BookingDelegate>();
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return result;
                }
                result = ConvertCSPBookingForCSPByTable(ds.Tables[0]);
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
        /// 保存CSP委托列表
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveBookingDelegateList(SaveRequestBookingDelegate saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");
            try
            {
                foreach (var item in saveRequest.ItemIDs)
                {
                    
                }
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveBookingDelegate]");
                dbCommand.CommandTimeout = 60;
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@BookingMapIDs", DbType.String, saveRequest.BookingMapIDs.Join());
                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, saveRequest.ItemIDs.Join());
                db.AddInParameter(dbCommand, "@BookingNos", DbType.String, saveRequest.BookingNos.Join());
                db.AddInParameter(dbCommand, "@BookingNames", DbType.String, saveRequest.BookingNames.Join());
                db.AddInParameter(dbCommand, "@FreightMethodTypes", DbType.String, saveRequest.FreightMethodTypes.Join());
                db.AddInParameter(dbCommand, "@ShipmentTypes", DbType.String, saveRequest.ShipmentTypes.Join());
                db.AddInParameter(dbCommand, "@IncoTermIDs", DbType.String, saveRequest.IncoTermIDs.Join());
                db.AddInParameter(dbCommand, "@TradeTypes", DbType.String, saveRequest.TradeTypes.Join());
                db.AddInParameter(dbCommand, "@TransportClauseIDs", DbType.String, saveRequest.TransportClauseIDs.Join());
                db.AddInParameter(dbCommand, "@BookingDates", DbType.String, saveRequest.BookingDates.Join());
                db.AddInParameter(dbCommand, "@IsInsurances", DbType.String, saveRequest.IsInsurances.Join());
                db.AddInParameter(dbCommand, "@IsTrucks", DbType.String, saveRequest.IsTrucks.Join());
                db.AddInParameter(dbCommand, "@IsDeclarations", DbType.String, saveRequest.IsDeclarations.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, saveRequest.CustomerIDs.Join());
                db.AddInParameter(dbCommand, "@ShipperIDs", DbType.String, saveRequest.ShipperIDs.Join());
                db.AddInParameter(dbCommand, "@ConsigneeIDs", DbType.String, saveRequest.ConsigneeIDs.Join());
                db.AddInParameter(dbCommand, "@POLIDs", DbType.String, saveRequest.POLIDs.Join());
                db.AddInParameter(dbCommand, "@POLAddressMapIDs", DbType.String, saveRequest.POLAddressMapIDs.Join());
                db.AddInParameter(dbCommand, "@POLAddresss", DbType.String, saveRequest.POLAddresss.Join());
                db.AddInParameter(dbCommand, "@ETDForPOLs", DbType.String, saveRequest.ETDForPOLs.Join());
                db.AddInParameter(dbCommand, "@PODIDs", DbType.String, saveRequest.PODIDs.Join());
                db.AddInParameter(dbCommand, "@ETAForPODs", DbType.String, saveRequest.ETAForPODs.Join());
                db.AddInParameter(dbCommand, "@PODAddressMapIDs", DbType.String, saveRequest.PODAddressMapIDs.Join());
                db.AddInParameter(dbCommand, "@PODAddresss", DbType.String, saveRequest.PODAddresss.Join());
                db.AddInParameter(dbCommand, "@Containerss", DbType.String, saveRequest.Containerss.Join());
                db.AddInParameter(dbCommand, "@Quantitys", DbType.String, saveRequest.Quantitys.Join());
                db.AddInParameter(dbCommand, "@QuantityUnitIDs", DbType.String, saveRequest.QuantityUnitIDs.Join());
                db.AddInParameter(dbCommand, "@Weights", DbType.String, saveRequest.Weights.Join());
                db.AddInParameter(dbCommand, "@WeightUnitIDs", DbType.String, saveRequest.WeightUnitIDs.Join());
                db.AddInParameter(dbCommand, "@Measurements", DbType.String, saveRequest.Measurements.Join());
                db.AddInParameter(dbCommand, "@MeasurementUnitIDs", DbType.String, saveRequest.MeasurementUnitIDs.Join());
                db.AddInParameter(dbCommand, "@SalesIDs", DbType.String, saveRequest.SalesIDs.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
                Guid[] newItemID = new Guid[saveRequest.ItemIDs.Length];
                for (int i = 0; i < result.Items.Count(); i++)
                {
                    saveRequest.ItemIDs[i] = result.Items[i].GetValue<Guid>("ID");
                }
                if (!saveRequest.ItemIDs.Any(fitem => fitem.IsNullOrEmpty()))
                {
                    SaveBookingInfoForCSP(saveRequest);
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

        #region 删除CSP舱单委托
        /// <summary>
        /// 删除CSP舱单委托
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        public void RemoveBookingDelegate(SaveRequestBookingDelegate saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "removeByID");

            try
            {
                ChangeBooingStatus(saveRequest);
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspRemoveBookingDelegate]");

                db.AddInParameter(dbCommand, "@ItemIDs", DbType.String, saveRequest.ItemIDs.Join());
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteScalar(dbCommand);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingList"></param>
        /// <returns></returns>
        private List<BookingDelegateList> ConvertCSPBookingAPIToDelegate(List<BookingInfoForCSPAPI> bookingList)
        {
            try
            {
                List<BookingDelegateList> result = new List<BookingDelegateList>();
                
                if (bookingList != null && bookingList.Count() > 0)
                {
                    foreach (BookingInfoForCSPAPI itemAPI in bookingList)
                    {
                        OperationType operationType = OperationType.Unknown;
                        CSP_FBAFREIGHTMETHODTYPE fbaFreightMethodType = CSP_FBAFREIGHTMETHODTYPE.Unknown;
                        DELIVERYTYPE deliveryType = DELIVERYTYPE.Unknown;
                        if(itemAPI.tradeType == CSP_TRADETYPE.General)
                        {
                            if(itemAPI.freightMethodType == CSP_FREIGHTMETHODTYPE.Ocean)
                            {
                                operationType = OperationType.OceanExport;
                            }
                            if (itemAPI.freightMethodType == CSP_FREIGHTMETHODTYPE.Air)
                            {
                                operationType = OperationType.AirExport;
                            }
                        }else
                        {
                            if (itemAPI.fbaFreightMethodId != null)
                            {
                                switch (itemAPI.fbaFreightMethodId)
                                {
                                    case 121:
                                        fbaFreightMethodType = CSP_FBAFREIGHTMETHODTYPE.OceanTruck;
                                        deliveryType = DELIVERYTYPE.Truck;
                                        operationType = OperationType.OceanExport;
                                        break;
                                    case 122:
                                        fbaFreightMethodType = CSP_FBAFREIGHTMETHODTYPE.Express;
                                        deliveryType = DELIVERYTYPE.Express;
                                        operationType = OperationType.AirExport;
                                        break;
                                    case 123:
                                        fbaFreightMethodType = CSP_FBAFREIGHTMETHODTYPE.OceanExpress;
                                        deliveryType = DELIVERYTYPE.Express;
                                        operationType = OperationType.OceanExport;
                                        break;
                                    case 124:
                                        fbaFreightMethodType = CSP_FBAFREIGHTMETHODTYPE.AirExpress;
                                        deliveryType = DELIVERYTYPE.Express;
                                        operationType = OperationType.AirExport;
                                        break;
                                }
                            }
                        }
                        #region New Object
                        BookingDelegateList bdItem = new BookingDelegateList()
                                        {
                                            OperationType = operationType,
                                            BookingMapID = itemAPI.id,
                                            BookingDate = itemAPI.creationTime.ToDateTime(),
                                            BookingNo = itemAPI.bookingNo,
                                            BookingName = itemAPI.name,
                                            FreightMethodType = itemAPI.freightMethodType,
                                            FBAFreightMethodType = fbaFreightMethodType,
                                            DeliveryType = deliveryType,
                                            TradeType = itemAPI.tradeType,
                                            TransportClauseName = itemAPI.freightTypeString,
                                            ShipmentType = itemAPI.shipmentType,
                                            IncoTermName = itemAPI.incotermsString,
                                            IsTruck = itemAPI.originIsRequireTruck,
                                            IsDeclaration = itemAPI.isDeclaration,
                                            IsInsurance = itemAPI.isInsurance,
                                            Containers = itemAPI.Containers,
                                            CustomerName = itemAPI.customerName,
                                            ShipperName = itemAPI.shipperCustomerName,
                                            ConsigneeName = itemAPI.consigneeCustomerName,
                                            POLName = itemAPI.originPort,
                                            POLAddress = itemAPI.originAddress,
                                            ETDForPOL = itemAPI.cargoReadyDate,
                                            PODName = itemAPI.destinationPort,
                                            PODAddress = itemAPI.destinationAddress,
                                            ETAForPOD = itemAPI.deliveryDate,
                                            Quantity = itemAPI.quantity,
                                            QuantityUnitName = itemAPI.quantityUnitString,
                                            Weight = itemAPI.weight,
                                            WeightUnitName = itemAPI.weightUnitString,
                                            Measurement = itemAPI.volume,
                                            MeasurementUnitName = itemAPI.volumeUnitString,
                                            CreateByName = itemAPI.creatorUserFullName,
                                            SalesName = itemAPI.serviceBusinessUserFullName,
                                        }; 
                        #endregion
                        result.Add(bdItem);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingList"></param>
        /// <returns></returns>
        private List<BookingDelegate> GetCSPBookingAPIMappingData(List<BookingInfoForCSPAPI> bookingList)
        {
            try
            {
                if (bookingList == null || bookingList.Count() <= 0)
                    return new List<BookingDelegate>();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPBookingListForJSON]");
                dbCommand.CommandTimeout = 10 * 60;
                db.AddInParameter(dbCommand, "@Ids", DbType.String, bookingList.Select(fItem => fItem.id).ToArray().Join());
                db.AddInParameter(dbCommand, "@BookingNos", DbType.String, bookingList.Select(fItem => fItem.bookingNo).ToArray().Join());
                db.AddInParameter(dbCommand, "@BookingNames", DbType.String, bookingList.Select(fItem => fItem.name).ToArray().Join());
                db.AddInParameter(dbCommand, "@FreightMethodTypes", DbType.String, bookingList.Select(fItem => fItem.freightMethodType.GetHashCode()).ToArray().Join());
                db.AddInParameter(dbCommand, "@ShipmentTypes", DbType.String, bookingList.Select(fItem => fItem.shipmentType.GetHashCode()).ToArray().Join());
                db.AddInParameter(dbCommand, "@CreationTimes", DbType.String, bookingList.Select(fItem => fItem.creationTime).ToArray().Join());
                db.AddInParameter(dbCommand, "@Incoterms", DbType.String, bookingList.Select(fItem => fItem.incotermsId).ToArray().Join());
                db.AddInParameter(dbCommand, "@TradeTypes", DbType.String, bookingList.Select(fItem => fItem.tradeType).ToArray().Join());
                db.AddInParameter(dbCommand, "@FreightTypes", DbType.String, bookingList.Select(fItem => fItem.freightTypeString).ToArray().Join());
                db.AddInParameter(dbCommand, "@CustomerIds", DbType.String, bookingList.Select(fItem => fItem.customerId).ToArray().Join());
                db.AddInParameter(dbCommand, "@ContainerTypes", DbType.String, bookingList.Select(fItem => fItem.Containers).ToArray().Join());
                db.AddInParameter(dbCommand, "@ShipperIds", DbType.String, bookingList.Select(fItem => fItem.shipperCustomerId).ToArray().Join());
                db.AddInParameter(dbCommand, "@ConsigneeIds", DbType.String, bookingList.Select(fItem => fItem.consigneeCustomerId).ToArray().Join());
                db.AddInParameter(dbCommand, "@OriginPortIds", DbType.String, bookingList.Select(fItem => fItem.originPortId == null ? 0 : fItem.originPortId.Value).ToArray().Join());
                db.AddInParameter(dbCommand, "@OriginAddressIds", DbType.String, bookingList.Select(fItem => fItem.originAddressId == null ? 0 : fItem.originAddressId.Value).ToArray().Join());
                db.AddInParameter(dbCommand, "@OriginAddresss", DbType.String, bookingList.Select(fItem => fItem.originAddress).ToArray().Join());
                db.AddInParameter(dbCommand, "@CargoReadyDates", DbType.String, bookingList.Select(fItem => fItem.cargoReadyDate).ToArray().Join());
                db.AddInParameter(dbCommand, "@DestinationPortIds", DbType.String, bookingList.Select(fItem => fItem.destinationPortId == null ? 0 : fItem.destinationPortId).ToArray().Join());
                db.AddInParameter(dbCommand, "@DestinationAddressIds", DbType.String, bookingList.Select(fItem => fItem.destinationAddressId == null ? 0 : fItem.destinationAddressId).ToArray().Join());
                db.AddInParameter(dbCommand, "@DestinationAddresss", DbType.String, bookingList.Select(fItem => fItem.destinationAddress).ToArray().Join());
                db.AddInParameter(dbCommand, "@DeliveryDates", DbType.String, bookingList.Select(fItem => fItem.deliveryDate).ToArray().Join());
                db.AddInParameter(dbCommand, "@Quantitys", DbType.String, bookingList.Select(fItem => fItem.quantity).ToArray().Join());
                db.AddInParameter(dbCommand, "@QuantityUnits", DbType.String, bookingList.Select(fItem => fItem.quantityUnitId).ToArray().Join());
                db.AddInParameter(dbCommand, "@TotalWeights", DbType.String, bookingList.Select(fItem => fItem.weight).ToArray().Join());
                db.AddInParameter(dbCommand, "@TotalWeightUnits", DbType.String, bookingList.Select(fItem => fItem.weightUnitId).ToArray().Join());
                db.AddInParameter(dbCommand, "@TotalVolumes", DbType.String, bookingList.Select(fItem => fItem.volume).ToArray().Join());
                db.AddInParameter(dbCommand, "@TotalVolumeUnits", DbType.String, bookingList.Select(fItem => fItem.volumeUnitId).ToArray().Join());
                db.AddInParameter(dbCommand, "@ServiceBusinessUserIds", DbType.String, bookingList.Select(fItem => fItem.serviceBusinessUserId).ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                List<BookingDelegate> result = new List<BookingDelegate>();
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return result;
                }
                result = ConvertCSPBookingForCSPByTable(ds.Tables[0]);
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

        private List<BookingDelegate> ConvertCSPBookingForCSPByTable(DataTable dt)
        {
            List<BookingDelegate>  result = (from item in dt.AsEnumerable()
                      select new BookingDelegate
                      {
                          ID = item.IsNull("ID") ? Guid.Empty : item.Column<Guid>("ID"),
                          IsDefault = false,
                          BookingMapID = item.Column<int>("BookingMapID"),
                          BookingNo = item.Column<string>("BookingNo"),
                          BookingName = item.Column<string>("BookingName"),
                          OperationType = (OperationType)item.Column<byte>("OperationType"),
                          FreightMethodType = (CSP_FREIGHTMETHODTYPE)item.Column<byte>("FreightMethodType"),
                          ShipmentType = (CSP_SHIPMENTTYPE)item.Column<byte>("ShipmentType"),
                          BookingDate = item.Column<DateTime>("BookingDate"),
                          TradeType = (CSP_TRADETYPE)item.Column<byte>("TradeType"),
                          TransportClauseID = item.Column<Guid>("TransportClauseID"),
                          TransportClauseName = item.Column<string>("TransportClauseName"),
                          IncoTermID = item.Column<Guid>("IncoTermID"),
                          CustomerID = item.Column<Guid>("CustomerID"),
                          CustomerName = item.Column<string>("CustomerName"),
                          CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), item.Column<string>("CustomerDescription")),
                          Containers = item.Column<string>("Containers"),
                          ShipperID = item.Column<Guid>("ShipperID"),
                          ShipperName = item.Column<string>("ShipperName"),
                          ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), item.Column<string>("ShipperDescription")),
                          ConsigneeID = item.Column<Guid?>("ConsigneeID"),
                          ConsigneeName = item.Column<string>("ConsigneeName"),
                          ConsigneeDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), item.Column<string>("ConsigneeDescription")),
                          POLID = item.Column<Guid?>("POLID"),
                          POLName = item.Column<string>("POLName"),
                          POLAddress = item.Column<string>("POLAddress"),
                          POLAddressMapID = item.Column<int>("POLAddressMapID"),
                          ETDForPOL = item.Column<DateTime?>("ETDForPOL"),
                          PODID = item.Column<Guid?>("PODID"),
                          PODName = item.Column<string>("PODName"),
                          PODAddress = item.Column<string>("PODAddress"),
                          PODAddressMapID = item.Column<int>("PODAddressMapID"),
                          ETAForPOD = item.Column<DateTime?>("ETAForPOD"),
                          Quantity = item.Column<int>("Quantity"),
                          QuantityUnitID = item.Column<Guid?>("QuantityUnitID"),
                          Weight = item.Column<decimal>("Weight"),
                          WeightUnitID = item.Column<Guid?>("WeightUnitID"),
                          Measurement = item.Column<decimal>("Measurement"),
                          MeasurementUnitID = item.Column<Guid?>("MeasurementUnitID"),
                          SalesID = item.Column<Guid>("SalesID"),
                          SalesName =item.Column<string>("SalesName"),
                          CancelRemark = item.Column<string>("CancelRemark"),
                      }).ToList();
            return result;
        }
    }
}
