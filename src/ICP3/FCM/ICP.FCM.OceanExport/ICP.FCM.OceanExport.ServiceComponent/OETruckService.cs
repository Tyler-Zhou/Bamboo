using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;

namespace ICP.FCM.OceanExport.ServiceComponent
{
    partial class OceanExportService
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
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, orderID);
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
        public List<OceanTruckInfo> GetOceanTruckServiceList(Guid oceanBookingID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanBookingID, "oceanBookingID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOceanTruckServiceList");

                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, oceanBookingID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanTruckInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new OceanTruckInfo
                                                {
                                                    
                                                    ID = b.Field<Guid>("ID"),
                                                    OceanBookingID = b.Field<Guid>("OceanBookingID"),
                                                    TruckerID = b.Field<Guid>("TruckerID"),
                                                    TruckerName = b.Field<string>("TruckerName"),
                                                    TruckerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("TruckerDescription")),
                                                    //PickUpAtID = b.Field<Guid?>("PickUpAtID"),
                                                    //PickUpAtName = b.Field<string>("PickUpAtName"),
                                                    //PickUpAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("PickUpAtDescription")),
                                                    BillToID = b.Field<Guid?>("BillToID"),
                                                    BillToName = b.Field<string>("BillToName"),
                                                    BillToDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("BillToDescription")),
                                                    PUEmptyCNTRID = b.Field<Guid?>("PUEmptyCNTRID"),
                                                    PUEmptyCNTRName = b.Field<string>("PUEmptyCNTRName"),
                                                    PUEmptyCNTRDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("PUEmptyCNTRDescription")),
                                                    Commodity = b.Field<string>("Commodity"),
                                                    TotalPkgs = b.Field<string>("TotalPkgs"),
                                                    ShipperID = b.Field<Guid>("ShipperID"),
                                                    ShipperName = b.Field<string>("ShipperName"),
                                                    ShipperDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("ShipperDescription")),
                                                    LoadingTime = b.Field<DateTime>("LoadingTime"),
                                                    DeliveryAtID = b.Column<Guid>("DeliveryAtID"),
                                                    DeliveryAtName = b.Field<string>("DeliveryAtName"),
                                                    DeliveryAtDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("DeliveryAtDescription")),
                                                    DeliveryDate = b.Field<DateTime?>("DeliveryDate"),
                                                    IsDrivingLicence = b.Field<bool>("IsDrivingLicence"),
                                                    CustomsBrokerID = b.Field<Guid?>("CustomsBrokerID"),
                                                    CustomsBrokerName = b.Field<string>("CustomsBrokerName"),
                                                    CustomsBrokerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomsBrokerDescription")),
                                                    ContainerDescription = SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), b.Field<string>("ContainerDescription")),
                                                    FreigtDescription = b.Field<string>("FreigtDescription"),
                                                    Remark = b.Field<string>("Remark"),
                                                    TruckingCharge = b.Field<string>("TruckingCharge"),
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
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回SingleResult</returns>
        public HierarchyManyResult SaveOceanTruckServiceInfo(TruckSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.oceanBookingID, "oceanShippingOrderID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.truckerID, "truckerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.shipperID, "shipperID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanTruckServiceInfo");

                string temptruckerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.truckerDescription, true, false);
                string tempshipperDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.shipperDescription, true, false);
                string tempdeliveryAtDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.deliveryAtDescription, true, false);
                string tempcustomsBrokerDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.customsBrokerDescription, true, false);
                string tempcontainerDescription = SerializerHelper.SerializeToString<ContainerDescription>(saveRequest.containerDescription, true, false);
                string tempBillToDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.BillToDescription, true, false);
                string tempPUEmptyCNTRDescription = SerializerHelper.SerializeToString<CustomerDescription>(saveRequest.PUEmptyCNTRDescription, true, false);

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@NO", DbType.String, saveRequest.no);
                db.AddInParameter(dbCommand, "@SONO", DbType.String, saveRequest.sono);
                db.AddInParameter(dbCommand, "@OceanBookingID", DbType.Guid, saveRequest.oceanBookingID);
                db.AddInParameter(dbCommand, "@TruckerID", DbType.Guid, saveRequest.truckerID);
                db.AddInParameter(dbCommand, "@TruckerDescription", DbType.Xml, temptruckerDescription);
                db.AddInParameter(dbCommand, "@PickUpAtID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@PickUpAtDescription", DbType.Xml, null);
                db.AddInParameter(dbCommand, "@DeliveryDate", DbType.DateTime, saveRequest.DeliveryDate);
              
                db.AddInParameter(dbCommand, "@BillToID", DbType.Guid, saveRequest.BillToID);
                db.AddInParameter(dbCommand, "@BillToDescription", DbType.Xml, tempBillToDescription);
                db.AddInParameter(dbCommand, "@PUEmptyCNTRID", DbType.Guid, saveRequest.PUEmptyCNTRID);
                db.AddInParameter(dbCommand, "@PUEmptyCNTRDescription", DbType.Xml, tempPUEmptyCNTRDescription);
                db.AddInParameter(dbCommand, "@TotalPkgs", DbType.String, saveRequest.TotalPkgs);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.Commodity);
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
                db.AddInParameter(dbCommand, "@TruckingCharge", DbType.String, saveRequest.TruckingCharge);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.updateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);                

                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "NO", "UpdateDate" }, new string[] { "ID", "OceanContainerID", "OceanTruckServiceID", "UpdateDate" } });
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
        public void RemoveOceanTruckServiceInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanTruckServiceInfo");

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
        public void RemoveOceanTruckContainerInfo(
           Guid[] ids,
           Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanTruckContainerInfo");

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
