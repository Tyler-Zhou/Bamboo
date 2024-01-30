using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
namespace ICP.FCM.AirExport.ServiceComponent
{
    partial class AirExportService
    {
        /// <summary>
        /// 根据业务ID获取拖车行最近业务信息
        /// </summary>
        /// <param name="orderID">业务ID</param>
        /// <returns>
        /// 该票业务对应客户的最近服务过的拖车行(TruckerID)
        /// 该票业务对应发货人的最近派车单中装货地(POLID)
        /// </returns>
        public SingleResult GetTruckRecentData(Guid orderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetTruckRecentData");
                db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "TruckerID", "ContainerDescription", "ShipperID", 
                    "CustomsBrokerID", "IsDrivingLicence", "Remark", "ReturnLocationID" });
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
        /// 获取派车信息
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <returns>返回派车信息</returns>
        public List<AirTruckInfo> GetAirTruckServiceList(Guid oceanBookingID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirTruckServiceList");

                db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirTruckInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new AirTruckInfo
                                                {
                                                    
                                                    ID = b.Field<Guid>("ID"),
                                                    AirBookingID = b.Field<Guid>("AirBookingID"),
                                                    TruckerID = b.Field<Guid>("TruckerID"),
                                                    TruckerName = b.Field<string>("TruckerName"),
                                                    TruckerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("TruckerDescription")),
                                                    PickUpAtID = b.Field<Guid?>("PickUpAtID"),
                                                    PickUpAtName = b.Field<string>("PickUpAtName"),
                                                    PickUpAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("PickUpAtDescription")),
                                                    ShipperID = b.Field<Guid>("ShipperID"),
                                                    ShipperName = b.Field<string>("ShipperName"),
                                                    ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                    LoadingTime = b.Field<DateTime>("LoadingTime"),
                                                    DeliveryAtID = b.Column<Guid>("DeliveryAtID"),
                                                    DeliveryAtName = b.Field<string>("DeliveryAtName"),
                                                    DeliveryAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("DeliveryAtDescription")),
                                                    IsDrivingLicence = b.Field<bool>("IsDrivingLicence"),
                                                    CustomsBrokerID = b.Field<Guid?>("CustomsBrokerID"),
                                                    CustomsBrokerName = b.Field<string>("CustomsBrokerName"),
                                                    CustomsBrokerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomsBrokerDescription")),
                                                    ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                                    FreigtDescription = b.Field<string>("FreigtDescription"),
                                                    Remark = b.Field<string>("Remark"),
                                                    CreateByID = b.Field<Guid>("CreateByID"),
                                                    CreateByName = b.Field<string>("CreateByName"),
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    NO = b.Field<string>("NO"),
                                                    ShippingOrderNo = b.Field<string>("SONO"),
                                                    CarrierID = b.Field<Guid>("CarrierID"),
                                                    CarrierName = b.Field<string>("CarrierName"),
                                                    VesselVoyage = b.Field<string>("VesselVoyage"),                                                    
                                                    IsDirty = false
                                                }).ToList();

                if (results == null || results.Count == 0)
                {
                    return results;
                }

                //foreach (AirTruckInfo t in results)
                //{
                //    t.Containers = (from b in ds.Tables[1].AsEnumerable()
                //                    select new AirContainerList
                //                    {
                //                        ID = b.Field<Guid>("ID"),
                //                        OwnerID = b.Field<Guid>("OwnerID"),
                //                        ContainerID = b.IsNull("AirContainerID") ? Guid.Empty : b.Field<Guid>("AirContainerID"),
                //                        ShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                //                        No = b.Field<string>("No"),
                //                        TypeID = b.Field<Guid>("TypeID"),
                //                        TypeName = b.Field<string>("TypeName"),
                //                        SealNo = b.Field<string>("SealNo"),
                //                        IsSOC = b.Field<bool>("IsSOC"),
                //                        IsPartOf = b.Field<bool>("IsPartOf"),
                //                        CreateByID = b.Field<Guid>("CreateByID"),
                //                        CreateByName = b.Field<string>("CreateByName"),
                //                        CreateDate = b.Field<DateTime>("CreateDate"),
                //                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                //                        DeliveryDate = b.Field<DateTime?>("DeliveryDate"),
                //                        ArriveDate = b.Field<DateTime?>("ArriveDate"),
                //                        ReturnDate = b.Field<DateTime?>("ReturnDate"),
                //                        DriverName = b.Field<string>("DriverName"),
                //                        CarNo = b.Field<string>("CarNo"),
                //                        IsDirty = false
                //                    }).ToList();
                //}

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
        /// 保存派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="oceanBookingID">订单ID</param>
        /// <param name="truckerID">拖车公司</param>
        /// <param name="no">拖车号</param>
        /// <param name="sono">订舱号</param>
        /// <param name="truckerDescription">拖车公司描述</param>
        /// <param name="pickUpAtID">提柜地点(关联客户)</param>
        /// <param name="pickUpAtDescription">提柜地点描述</param>
        /// <param name="shipperID">发货人</param>
        /// <param name="shipperDescription">发货人描述</param>
        /// <param name="loadTime">装货时间</param>
        /// <param name="deliveryAtID">还柜地点（关联客户）</param>
        /// <param name="deliveryAtDescription">还柜地点描述（关联客户）</param>
        /// <param name="isDrivingLicence">是否需要司机本</param>
        /// <param name="customsBrokerID">报关公司</param>
        /// <param name="customsBrokerDescription">报关公司描述</param>
        /// <param name="containerDescription">箱需求描述</param>
        /// <param name="feeDescription">费用描述</param>
        /// <param name="remark">备注</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="truckServiceContainerIDs">拖车服务箱关联ID列表</param>
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
        public HierarchyManyResult SaveAirTruckServiceInfo(TruckSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.oceanBookingID, "oceanShippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.truckerID, "truckerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.shipperID, "shipperID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirTruckServiceInfo");

                string temptruckerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.truckerDescription, true, false);
                string temppickUpAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.pickUpAtDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.shipperDescription, true, false);
                string tempdeliveryAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.deliveryAtDescription, true, false);
                string tempcustomsBrokerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.customsBrokerDescription, true, false);
                string tempcontainerDescription = SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.containerDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@NO", DbType.String, saveRequest.no);
                db.AddInParameter(dbCommand, "SONO", DbType.String, saveRequest.sono);
                db.AddInParameter(dbCommand, "@AirBookingID", DbType.Guid, saveRequest.oceanBookingID);
                db.AddInParameter(dbCommand, "@TruckerID", DbType.Guid, saveRequest.truckerID);
                db.AddInParameter(dbCommand, "@TruckerDescription", DbType.Xml, temptruckerDescription);
                db.AddInParameter(dbCommand, "@PickUpAtID", DbType.Guid, saveRequest.pickUpAtID);
                db.AddInParameter(dbCommand, "@PickUpAtDescription", DbType.Xml, temppickUpAtDescription);
                db.AddInParameter(dbCommand, "@ShipperID", DbType.Guid, saveRequest.shipperID);
                db.AddInParameter(dbCommand, "@ShipperDescription", DbType.Xml, tempshipperDescription);
                db.AddInParameter(dbCommand, "@LoadingTime", DbType.DateTime, saveRequest.loadTime);
                db.AddInParameter(dbCommand, "@DeliveryAtID", DbType.Guid, saveRequest.deliveryAtID);
                db.AddInParameter(dbCommand, "@DeliveryAtDescription", DbType.Xml, tempdeliveryAtDescription);
                db.AddInParameter(dbCommand, "@IsDrivingLicence", DbType.Boolean, saveRequest.isDrivingLicence);
                db.AddInParameter(dbCommand, "@CustomsBrokerID", DbType.Guid, saveRequest.customsBrokerID);
                db.AddInParameter(dbCommand, "@CustomsBrokerDescription", DbType.Xml, tempcustomsBrokerDescription);
                db.AddInParameter(dbCommand, "@ContainerDescription", DbType.Xml, tempcontainerDescription);
                db.AddInParameter(dbCommand, "@FreigtDescription", DbType.String, saveRequest.feeDescription);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.remark);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.updateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);                

                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "NO", "UpdateDate" }, new string[] { "ID", "AirContainerID", "AirTruckServiceID", "UpdateDate" } });
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
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveAirTruckServiceInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAirTruckServiceInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
        /// 删除派车的箱信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        public void RemoveAirTruckContainerInfo(
           Guid[] ids,
           Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAirTruckContainerInfo");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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
