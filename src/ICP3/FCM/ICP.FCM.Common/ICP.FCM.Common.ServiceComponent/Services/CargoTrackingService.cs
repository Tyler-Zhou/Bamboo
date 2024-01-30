using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using System.Transactions;

namespace ICP.FCM.Common.ServiceComponent
{
    public partial class FCMCommonService
    {
        /// <summary>
        /// 获取进口箱子面板
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        public CargoTrackingInfo GetOperationContainersInfo(Guid operationID, OperationType operationType)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationContainersInfo");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int32, operationType);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, ApplicationContext.Current.IsEnglish);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
                return new CargoTrackingInfo();
            CargoTrackingInfo result = null;
            if (operationType == OperationType.OceanImport)
            {
                result = (from b in ds.Tables[0].AsEnumerable()
                          select new CargoTrackingInfo
                          {
                              OperationNO = b.Field<string>("OperationNO"),
                              OperationID = b.Field<Guid>("OperationID"),
                              MBLNO = b.Field<string>("MBLNO"),
                              Carrier = b.Field<string>("Carrier"),
                              CarrierID = b.Field<Guid?>("CarrierID"),
                              Voyage = b.Field<string>("Voyage"),
                              VoyageID = b.Field<Guid?>("VoyageID"),
                              POLID = b.Field<Guid>("POLID"),
                              PODID = b.Field<Guid>("PODID"),
                              POLCode = b.Field<string>("POLCode"),
                              PODCode = b.Field<string>("PODCode"),
                              POL = b.Field<string>("POL"),
                              POD = b.Field<string>("POD"),
                              PlaceOfDelivery = b.Field<string>("PlaceOfDelivery"),
                              FinalDestination = b.Field<string>("FinalDestination"),
                              ETD = b.Field<DateTime?>("ETD"),
                              ETA = b.Field<DateTime?>("ETA"),
                              DETA = b.Field<DateTime?>("DETA"),
                              FETA = b.Field<DateTime?>("FETA"),
                              PickUpID = b.Field<Guid?>("PickUpID"),
                              PickUpPlace = b.Field<string>("PickUpPlace"),
                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                              Containers = (from h in ds.Tables[1].AsEnumerable()
                                            select new CargoTrackingContainerInfo
                                            {
                                                ContainerID = h.Field<Guid>("ContainerID"),
                                                ContainerNO = h.Field<string>("ContainerNO"),
                                                PickUpNo = h.Field<string>("PickUpNo"),
                                                LastFreeDate = h.Field<DateTime?>("LastFreeDate"),
                                                AvailableDate = h.Field<DateTime?>("AvailableDate"),
                                                DeliveryTime = h.Field<DateTime?>("DeliveryTime"),
                                                PickUpDate = h.Field<DateTime?>("PickUpDate"),
                                                ReturnDate = h.Field<DateTime?>("ReturnDate"),
                                                Status = h.Field<string>("Status"),
                                            }).ToList(),
                          }).SingleOrDefault();
            }
            else if (operationType == OperationType.OceanExport)
            {
                result = (from b in ds.Tables[0].AsEnumerable()
                          select new CargoTrackingInfo
                          {
                              OperationNO = b.Field<string>("OperationNO"),
                              SONO = b.Field<string>("SONO"),
                              Carrier = b.Field<string>("Carrier"),
                              PreVoyage = b.Field<string>("PreVoyage"),
                              PreETD = b.Field<DateTime?>("PreETD"),
                              Voyage = b.Field<string>("Voyage"),
                              ETD = b.Field<DateTime>("ETD"),
                              ETA = b.Field<DateTime>("ETA"),
                              DETA = b.Field<DateTime?>("CY_RailCUT"),
                              FETA = b.Field<DateTime?>("AESCUT"),
                              DOC_SICUT = b.Field<DateTime?>("DOC_SICUT"),
                              AMSClose = b.Field<DateTime?>("AMSClose"),
                              ReturnLoc = b.Field<string>("ReturnLoc"),
                              Containers = (from h in ds.Tables[1].AsEnumerable()
                                            select new CargoTrackingContainerInfo
                                            {
                                                MBLNO = h.Field<string>("MBLNO"),
                                                ContainerNO = h.Field<string>("ContainerNO"),
                                                Status = h.Field<string>("Status"),
                                            }).ToList(),
                          }).SingleOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cargoTrackingSaveRequest"></param>
        /// <returns></returns>
        public SingleResult SaveOperationContainersInfo(CargoTrackingSaveRequest cargoTrackingSaveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOperationContainersInfoForOI");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, cargoTrackingSaveRequest.OperationID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, cargoTrackingSaveRequest.CarrierID);
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, cargoTrackingSaveRequest.VoyageID);
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, cargoTrackingSaveRequest.ETD);
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, cargoTrackingSaveRequest.ETA);
                db.AddInParameter(dbCommand, "@DETA", DbType.DateTime, cargoTrackingSaveRequest.DETA);
                db.AddInParameter(dbCommand, "@FETA", DbType.DateTime, cargoTrackingSaveRequest.FETA);
                db.AddInParameter(dbCommand, "@PickUpID", DbType.Guid, cargoTrackingSaveRequest.PickUpID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, cargoTrackingSaveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, ApplicationContext.Current.UserId);

                db.AddInParameter(dbCommand, "@ContainerIDs", DbType.String, cargoTrackingSaveRequest.ContainerIds.ToArray().Join());
                db.AddInParameter(dbCommand, "@LastFreeDates", DbType.String, cargoTrackingSaveRequest.LastFreeDates.ToArray().Join());
                db.AddInParameter(dbCommand, "@AvailableDates", DbType.String, cargoTrackingSaveRequest.AvailableDates.ToArray().Join());

                //db.AddInParameter(dbCommand, "@AvailableTimes", DbType.String, cargoTrackingSaveRequest.AvailableTimes.ToArray().Join());
                db.AddInParameter(dbCommand, "@DeliveryTimes", DbType.String, cargoTrackingSaveRequest.DeliveryTimes.ToArray().Join());

                db.AddInParameter(dbCommand, "@PickUpDates", DbType.String, cargoTrackingSaveRequest.PickUpDates.ToArray().Join());
                db.AddInParameter(dbCommand, "@PickUpNos", DbType.String, cargoTrackingSaveRequest.PickUpNos.ToArray().Join());
                db.AddInParameter(dbCommand, "@ReturnDates", DbType.String, cargoTrackingSaveRequest.ReturnDates.ToArray().Join());

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
        /// 获取货物跟踪信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns></returns>
        public CargoTrackingInfo GetCargoTrackingInfo(Guid operationID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetCargoTrackingInfo");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, ApplicationContext.Current.IsEnglish);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
                return new CargoTrackingInfo();
            CargoTrackingInfo result = null;
            result = (from b in ds.Tables[0].AsEnumerable()
                      select new CargoTrackingInfo
                      {
                          OperationID = b.Field<Guid>("OperationID"),
                          OperationNO = b.Field<string>("OperationNO"),
                          SONO = b.Field<string>("SONO"),
                          Carrier = b.Field<string>("Carrier"),
                          CarrierID = b.Field<Guid?>("CarrierID"),
                          PreVoyage = b.Field<string>("PreVoyage"),
                          PreVoyageID = b.Field<Guid?>("PreVoyageID"),
                          PreETD = b.Field<DateTime?>("PreETD"),
                          Voyage = b.Field<string>("Voyage"),
                          VoyageID = b.Field<Guid?>("VoyageID"),
                          ETD = b.Field<DateTime?>("ETD"),
                          ETA = b.Field<DateTime?>("ETA"),
                          CY_RailCUT = b.Field<DateTime?>("CY_RailCUT"),
                          AESCUT = b.Field<DateTime?>("AESCUT"),
                          DOC_SICUT = b.Field<DateTime?>("DOC_SICUT"),
                          AMSClose = b.Field<DateTime?>("AMSClose"),
                          ReturnLoc = b.Field<string>("ReturnLoc"),
                          ReturnLocID = b.Field<Guid?>("ReturnLocID"),
                          OpenClosePortId = b.Field<Guid?>("OpenClosePortId"),
                          OpenPort = b.Field<DateTime?>("OpenPortDate"),
                          ClosePort = b.Field<DateTime?>("ClosePortDate"),
                          OpenAndClosePortDesc = b.Field<string>("OpenAndClosePortDesc"),
                          OpenAndClosePortUpdateDate = b.Field<DateTime?>("OpenAndClosePortUpdateDate"),
                          NetETD = b.Field<DateTime?>("NetETD"),
                          DOCK = b.Field<string>("Dock"),
                          IsSearchDock = b.Field<bool>("IsSearchDock"),
                          POLID = b.Field<Guid>("POLID"),
                          POLCode = b.Field<string>("POLCode"),
                          POL = b.Field<string>("POL"),
                          Containers = (from h in ds.Tables[1].AsEnumerable()
                                        select new CargoTrackingContainerInfo
                                        {
                                            Id = h.Field<Guid?>("Id"),
                                            MBLNO = h.Field<string>("MBLNO"),
                                            MBLId = h.Field<Guid>("MBLId"),
                                            ContainerID = h.Field<Guid>("ContainerID"),
                                            ContainerNO = h.Field<string>("ContainerNO"),
                                            Status = h.Field<string>("Status"),
                                            StatusALL = h.Field<string>("StatusALL"),
                                            CustomsRelease = h.Field<string>("CustomsRelease"),
                                            CustomsReleaseDesc = h.Field<string>("CustomsReleaseDesc"),
                                            GateIn = h.Field<string>("GateIn"),
                                            GateInDesc = h.Field<string>("GateInDesc"),
                                            Loadship = h.Field<string>("Loadship"),
                                            LoadshipDesc = h.Field<string>("LoadshipDesc"),
                                            IsUpdate = h.Field<bool>("IsUpdate"),
                                            UpdateDate = h.Field<DateTime?>("UpdateDate")
                                        }).ToList(),
                      }).SingleOrDefault();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cargoTracking"></param>
        /// <returns></returns>
        public SingleResult SaveCargoTrackingInfo(CargoTrackingInfo cargoTracking)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveCargoTracking");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, cargoTracking.OperationID);
                db.AddInParameter(dbCommand, "@SONO", DbType.String, cargoTracking.SONO);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, cargoTracking.CarrierID);//
                db.AddInParameter(dbCommand, "@VoyageID", DbType.Guid, cargoTracking.VoyageID);//
                db.AddInParameter(dbCommand, "@ETD", DbType.DateTime, cargoTracking.ETD);//
                db.AddInParameter(dbCommand, "@ETA", DbType.DateTime, cargoTracking.ETA);//
                db.AddInParameter(dbCommand, "@AESCUT", DbType.DateTime, cargoTracking.AESCUT);//
                db.AddInParameter(dbCommand, "@AMSClose", DbType.DateTime, cargoTracking.AMSClose);
                db.AddInParameter(dbCommand, "@CY_RailCUT", DbType.DateTime, cargoTracking.CY_RailCUT);//
                db.AddInParameter(dbCommand, "@DOC_SICUT", DbType.DateTime, cargoTracking.DOC_SICUT);//
                db.AddInParameter(dbCommand, "@PreETD", DbType.DateTime, cargoTracking.PreETD);//
                db.AddInParameter(dbCommand, "@PreVoyageID", DbType.Guid, cargoTracking.PreVoyageID);//
                db.AddInParameter(dbCommand, "@ReturnLocID", DbType.Guid, cargoTracking.ReturnLocID);//
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, cargoTracking.UpdateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, ApplicationContext.Current.UserId);

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
        /// 保存海关放行/进场日期
        /// </summary>
        public void SaveCustomsAndGateIn(List<CargoTrackingContainerInfo> saveList, Guid saveBy)
        {
            int c = saveList.Count;
            if (c == 0) return;
            Guid?[] Id = new Guid?[c];
            Guid[] MBLId = new Guid[c];
            string[] MBLNO = new string[c];
            string[] ContainerNO = new string[c];
            string[] CustomsRelease = new string[c];
            string[] CustomsReleaseDesc = new string[c];
            string[] GateIn = new string[c];
            string[] GateInDesc = new string[c];
            string[] Loadship = new string[c];
            string[] LoadshipDesc = new string[c];
            string[] IsUpdate = new string[c];
            DateTime? UpdateDate = saveList[0].UpdateDate;
            int i = 0;
            foreach (CargoTrackingContainerInfo cc in saveList)
            {
                Id[i] = cc.Id;
                MBLId[i] = cc.MBLId;
                MBLNO[i] = cc.MBLNO;
                ContainerNO[i] = cc.ContainerNO;
                CustomsRelease[i] = cc.CustomsRelease;
                if (!string.IsNullOrEmpty(cc.CustomsRelease.Trim()))
                    CustomsReleaseDesc[i] = cc.CustomsReleaseDesc;
                else
                    CustomsReleaseDesc[i] = string.Empty;
                GateIn[i] = cc.GateIn;
                if (!string.IsNullOrEmpty(cc.GateIn))
                    GateInDesc[i] = cc.GateInDesc;
                else
                    GateInDesc[i] = string.Empty;
                Loadship[i] = cc.Loadship;
                if (!string.IsNullOrEmpty(cc.Loadship))
                    LoadshipDesc[i] = cc.LoadshipDesc;
                else
                    LoadshipDesc[i] = string.Empty;
                IsUpdate[i] = cc.IsUpdate ? "1" : "0";
                i++;
            }
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveFCMOceanCustomsAndLoadship");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, Id.Join());
                db.AddInParameter(dbCommand, "@MBLId", DbType.String, MBLId.Join());
                db.AddInParameter(dbCommand, "@MBLNo", DbType.String, MBLNO.Join());
                db.AddInParameter(dbCommand, "@No", DbType.String, ContainerNO.Join());
                db.AddInParameter(dbCommand, "@CustomsRelease", DbType.String, CustomsRelease.Join());
                db.AddInParameter(dbCommand, "@CustomsReleaseDesc", DbType.String, CustomsReleaseDesc.Join());
                db.AddInParameter(dbCommand, "@GateIn", DbType.String, GateIn.Join());
                db.AddInParameter(dbCommand, "@GateInDesc", DbType.String, GateInDesc.Join());
                db.AddInParameter(dbCommand, "@Loadship", DbType.String, Loadship.Join());
                db.AddInParameter(dbCommand, "@LoadshipDesc", DbType.String, LoadshipDesc.Join());
                db.AddInParameter(dbCommand, "@IsUpdate", DbType.String, IsUpdate.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, UpdateDate);
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
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="openPort"></param>
        /// <param name="closePort"></param>
        /// <param name="portDesc"></param>
        /// <param name="isEnglish"></param>
        /// <param name="saveBy"></param>
        /// <param name="updateDate"></param>
        /// <param name="netETD"></param>
        /// <param name="Dock"></param>
        public void SaveOpenAndClosePort(Guid? Id, DateTime? openPort, DateTime? closePort, string portDesc, bool isEnglish, Guid saveBy, DateTime? updateDate,DateTime? netETD,string Dock)
        {
            try
            {
                if (Id == null || Id == Guid.Empty) return;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspUpdateOceanOpenAndClosePort");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@OpenPortDate", DbType.DateTime, openPort);
                db.AddInParameter(dbCommand, "@ClosePortDate", DbType.DateTime, closePort);
                db.AddInParameter(dbCommand, "@OpenAndClosePortDesc", DbType.String, portDesc);
                db.AddInParameter(dbCommand, "@NetETD", DbType.DateTime, netETD);
                db.AddInParameter(dbCommand, "@Dock", DbType.String, Dock);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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
        ///  保存/更新箱动态的事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SingleResult SaveCarrierContainerEvent(List<SaveCarrierContainerEventInput> input)
        {
            try
            {
                var dt = DataTableHelper.ToDataTable(input, this.IsEnglish);
                dt.Columns.Remove("EventCode");
                SingleResult result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                option.Timeout = TimeSpan.FromMinutes(10);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOceanContainerTracking");
                    SqlParameter[] sqlParameters = new SqlParameter[] {
                     new SqlParameter { ParameterName = "@SaveOceanContainerTracking"
                     , Direction = ParameterDirection.Input
                     ,TypeName = "dbo.SaveOceanContainerTrackingType",
                         SqlDbType =SqlDbType.Structured, Value = dt }
                    };
                    dbCommand.Parameters.AddRange(sqlParameters);

                    result = db.SingleResult(dbCommand, new string[] { "ID", "ContainerNO", "UpdateDate", "IsNew", "OperationID", "OperationType", "EventCode" });
                    #region 触发事件
                    string eventCodeStr = string.Empty;
                    List<EventCode> CacheEventCodes = GetEventCodeList(OperationType.OceanExport);
                    if (result != null)
                    {
                        eventCodeStr = input.FirstOrDefault().IsManual ? input.FirstOrDefault().EventCode : result.GetValue<string>("EVENTCODE");
                        if (result.GetValue<bool>("ISNEW") && !string.IsNullOrEmpty(eventCodeStr))
                        {
                            EventCode eventCode = CacheEventCodes.Where(fItem => eventCodeStr == fItem.Code).SingleOrDefault();
                            if (eventCode != null)
                            {
                                EventObjects eventObjects = new EventObjects()
                                {
                                    Id = Guid.Empty,
                                    Code = eventCode.Code,
                                    OperationID = result.GetValue<Guid>("OperationID"),
                                    OperationType = (OperationType)result.GetValue<byte>("OperationType"),
                                    Subject = eventCode.Subject,
                                    IsShowCustomer = true,
                                    IsShowAgent = true,
                                    Description = eventCode.Subject + ":" + result.GetValue<string>("ContainerNO"),
                                    FormType = FormType.Container,
                                    FormID = input.FirstOrDefault().ContainerID,
                                    Priority = MemoPriority.Low,
                                    Type = MemoType.Memo,
                                    UpdateBy = input.FirstOrDefault().SaveByID,
                                    CreateDate = DateTime.Now,
                                    UpdateDate = DateTime.Now,
                                    OccurrenceTime = DateTime.Now,
                                };
                                SaveMemoInfo(eventObjects);
                            }
                        }
                    }
                    #endregion
                    scope.Complete();
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
        /// 删除箱动态事件
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RemoveByID"></param>
        /// <param name="IsEnglish"></param>
        public void DeleteCarrierContainerEvent(Guid Id,Guid RemoveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOceanContainerTracking");
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, Id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, RemoveByID);
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