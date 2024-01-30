using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.Common.ServiceComponent
{
    partial class FCMCommonService
    {   
        public List<VoyageDateInfo> GetVoyageDateInfo(List<Guid?> voyagelIds)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[pub].[uspGetVoyageDateInfo]");
                db.AddInParameter(dbCommand, "@VoyageIDs", DbType.String, voyagelIds.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                List<VoyageDateInfo> result = new List<VoyageDateInfo>();
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return result;
                }
                result = (from item in ds.Tables[0].AsEnumerable()
                          select new VoyageDateInfo
                          {
                              ETD = item.Field<DateTime?>("ETD"),
                              ETA = item.Field<DateTime?>("ETA"),
                              ClosingDate = item.Field<DateTime?>("ClosingDate"),
                              CYClosingDate = item.Field<DateTime?>("CYClosingDate"),
                              DOCClosingDate = item.Field<DateTime?>("DOCClosingDate"),
                              PortId = item.Field<Guid?>("PortId"),
                              VoyageId = item.Field<Guid?>("VoyageId")
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
        /// 获取订舱列表
        /// </summary>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="shippingOrderNo">订舱号</param>
        /// <param name="scno">合约号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="agentOfCarrierName">承运人名</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地</param>
        /// <param name="salesID">业务员</param>
        /// <param name="bookingerID">订舱</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        public List<BusinessData> GetBusinessListForDataFinder(
            OperationType[] operationTypes, 
            Guid[] companyIDs,
            string operationNo,
            string blNo,          
            string customerName,           
            string polName,
            string podName,
            Guid? salesID,
            //Guid? bookingerID,
            //Guid? overseasFilerID,
            bool? isValid,
            OEOrderState? state,
            DateSearchTypeForDataFinder dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyIDs, "companyIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetBusinessListForWriteOff");

                string tempCompanyIDs = companyIDs.Join();              
                string operationTypesString = string.Empty;
                if (operationTypes != null)
                {
                    operationTypesString = operationTypes.Join<OperationType>();
                }

                db.AddInParameter(dbCommand, "@operationTypes", DbType.String, operationTypesString);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);          
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@FromName", DbType.String, polName);
                db.AddInParameter(dbCommand, "@ToName", DbType.String, podName);
                db.AddInParameter(dbCommand, "@SalesID", DbType.Guid, salesID);
                //db.AddInParameter(dbCommand, "@BookingerID", DbType.Guid, bookingerID);
                //db.AddInParameter(dbCommand, "@OverseasFilerID", DbType.Guid, overseasFilerID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@DateSearchType", DbType.Int16, dateSearchType);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BusinessData> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new BusinessData
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      OperationType = (OperationType)b.Field<byte>("OperationType"),
                                                      CompanyID = b.Field<Guid>("CompanyID"),
                                                      //OceanShippingOrderID = b.Field<Guid?>("OceanShippingOrderID"),
                                                      State = (OEOrderState)b.Field<byte>("State"),
                                                      No = b.Field<string>("No"),
                                                      MBLNo = b.Field<string>("MBLNo"),
                                                      HBLNo = b.Field<string>("HBLNo"),
                                                      //OceanShippingOrderNo = b.Field<string>("ShippingOrderNo"),
                                                      CustomerName = b.Field<string>("CustomerName"),
                                                      //BookingCustomerName = b.Field<string>("BookingCustomerName"),
                                                      //CarrierName = b.Field<string>("CarrierName"),
                                                      //CarrierID = b.Field<Guid?>("CarrierID"),
                                                      //AgentOfCarrierName = b.Field<string>("AgentOfCarrierName"),
                                                      //VesselVoyage = b.Field<string>("VesselVoyage"),
                                                      //PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                                      POLName = b.Field<string>("Fromlocation"),
                                                      PODName = b.Field<string>("Tolocation"),
                                                      //PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                      ETD = b.Field<DateTime?>("ETD"),
                                                      ////ShipperName = b.Field<string>("ShipperName"),
                                                      ////ConsigneeName = b.Field<string>("ConsigneeName"),
                                                      //AgentName = b.Field<string>("AgentName"),
                                                      //OEOperationType = (FCMOperationType)b.Field<byte>("OEOperationType"),
                                                      //OverSeasFilerName = b.Field<string>("OverSeasFilerName"),
                                                      //BookingerName = b.Field<string>("BookingerName"),
                                                      SalesName = b.Field<string>("SalesName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      //UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      //ContainerNo = b.Field<string>("ContainerNo"),
                                                      //BookingDate = b.Field<DateTime>("BookingDate"),
                                                      //SODate = b.Field<DateTime?>("SODate"),
                                                      //ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                                      //CreateByID = b.Field<Guid>("CreateByID"),
                                                      //CreateByName = b.Field<string>("CreateByName"),
                                                      //FilerName = b.Field<string>("FilerName"),
                                                      //PODContact = b.Field<string>("AgentFilerName"),
                                                      ETA = b.Field<DateTime?>("ETA"),
                                                      //IsValid = b.Field<bool>("IsValid"),
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
    }
}