using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace ICP.FRM.ServiceComponent
{
    /// <summary>
    /// 海运运价管理服务
    /// </summary>
    public class OceanPriceService : IOceanPriceService
    {
        #region 初始化
        string _ftpHost = string.Empty;
        string _ftpUser = string.Empty;
        string _ftpPassword = string.Empty;
        string _ftpBasePath = "\\OceanPrice";
        private TimeSpan tsTimeOut = new TimeSpan(0, 10, 0);

        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }

        public OceanPriceService()
        {
        }

        //public OceanPriceService(
        //   string ftpHost,
        //   string ftpPath,
        //   string ftpUser,
        //   string ftpPassword)
        //{
        //    //_ftpHost = ftpHost;
        //    //_ftpPassword = ftpPassword;
        //    //_ftpBasePath = ftpPath;
        //    //_ftpUser = ftpUser;
        //}

        #endregion

        #region 合约管理

        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <param name="contractNo">合约号</param>
        /// <param name="contractName">合约名称</param>
        /// <param name="carrier">船东</param>
        /// <param name="shippinglineID">航线</param>
        /// <param name="pol">装货港</param>
        /// <param name="pod">卸货港</param>
        ///<param name="delivery">卸货港</param>
        ///<param name="via">中转港</param>
        /// <param name="rateType">运价类型(1:Contract,2:Market)</param>
        /// <param name="state">状态(1,Draft2.Published,3.Invalidated,4.Expired</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="publisher">发布人</param>
        /// <param name="userID">当前用户ID，用来判断是否有权限</param>
        /// <returns>返回合约列表</returns>
        public List<OceanList> GetOceanList(
            string contractNo,
            string contractName,
            string carrier,
            Guid? shippinglineID,
            string pol,
            string via,
            string pod,
            string delivery,
            RateType? rateType,
            OceanState? state,
            DateTime? fromDate,
            DateTime? toDate,
            string publisher,
            Guid userID)
        {


            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@ContractNo", DbType.String, contractNo);
                db.AddInParameter(dbCommand, "@ContractName", DbType.String, contractName);
                db.AddInParameter(dbCommand, "@Carrier", DbType.String, carrier);
                db.AddInParameter(dbCommand, "@ShippinglineID", DbType.Guid, shippinglineID);
                db.AddInParameter(dbCommand, "@Pol", DbType.String, pol);
                db.AddInParameter(dbCommand, "@Via", DbType.String, via);
                db.AddInParameter(dbCommand, "@Pod", DbType.String, pod);
                db.AddInParameter(dbCommand, "@Delivery", DbType.String, delivery);
                db.AddInParameter(dbCommand, "@RateType", DbType.Byte, rateType);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@Publisher", DbType.String, publisher);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds == null || ds.Tables.Count < 1) { return null; }
                List<OceanList> results = BulidOceanListByDataSet(ds);

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }


        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <param name="ids">ids</param>
        /// <param name="userID">当前用户ID，用来判断是否有权限</param>
        /// <returns>返回合约列表</returns>
        public List<OceanList> GetOceanListByIds(Guid[] ids, Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanListByIDs");


                string tempids = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempids);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<OceanList> results = BulidOceanListByDataSet(ds);

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }

        private static List<OceanList> BulidOceanListByDataSet(DataSet ds)
        {
            List<OceanList> results = (from b in ds.Tables[0].AsEnumerable()
                                       select new OceanList
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           ContractNo = b.Field<String>("ContractNo"),
                                           ContractName = b.Field<String>("ContractName"),
                                           ContractType = (ContractType)b.Field<Byte>("ContractType"),
                                           CarrierName = b.Field<String>("CarrierName"),
                                           CarrierID=b.Field<Guid?>("CarrierID"),
                                           ShippingLineName = b.Field<String>("ShippingLineName"),
                                           PaymentTermName = b.Field<String>("PaymentTermName").IsNullOrEmpty() ? "BOTH" : b.Field<String>("PaymentTermName"),
                                           CurrencyName = b.Field<String>("CurrencyName"),
                                           FromDate = b.Field<DateTime>("FromDate"),
                                           ToDate = b.Field<DateTime>("ToDate"),
                                           RateType = (RateType)b.Field<Byte>("RateType"),
                                           PublisherName = b.Field<String>("PublisherName"),
                                           PublishDate = b.Field<DateTime?>("PublishDate"),
                                           State = (OceanState)b.Field<Byte>("State"),
                                           PermissionMode = (OceanPermissionMode)b.Field<byte>("PermissionMode"),
                                           Permission = (OceanPermission)b.Field<byte>("Permission"),
                                           PublisherID = b.Field<Guid?>("PublisherID"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),

                                           ShipperNames = b.Field<string>("ShipperName"),
                                           ConsigneeNames = b.Field<string>("ConsigneeName"),
                                           NotifyPartyNames = b.Field<string>("NotifyPartyName"),

                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           OceanUnits = (from u in ds.Tables[1].AsEnumerable()
                                                         where u.Field<Guid>("OceanID") == b.Field<Guid>("ID")
                                                         select new OceanUnitList
                                                         {
                                                             ID = u.Field<Guid>("ID"),
                                                             OceanID = u.Field<Guid>("OceanID"),
                                                             RowPosition = u.Field<short>("RowPosition"),
                                                             UnitID = u.Field<Guid>("UnitID"),
                                                             UnitName = u.Field<string>("UnitName")
                                                         }).ToList()
                                       }).ToList();
            return results;
        }


        /// <summary>
        /// 获取合约详细信息
        /// </summary>
        /// <param name="id">合约ID</param>
        /// <returns>返回合约详细信息</returns>
        public OceanInfo GetOceanInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OceanInfo result = (from b in ds.Tables[0].AsEnumerable()
                                    select new OceanInfo
                                    {
                                        CarrierID = b.Field<Guid?>("CarrierID"),
                                        ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                        CurrencyID = b.Field<Guid>("CurrencyID"),
                                        PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                        Remark = b.Field<String>("Remark"),
                                        PublisherID = b.Field<Guid?>("PublisherID"),
                                        CreateByID = b.Field<Guid>("CreateByID"),
                                        CreateByName = b.Field<String>("CreateByName"),
                                        CreateDate = b.Field<DateTime>("CreateDate"),
                                        ID = b.Field<Guid>("ID"),
                                        PermissionMode = (OceanPermissionMode)b.Field<byte>("PermissionMode"),
                                        ContractNo = b.Field<String>("ContractNo"),
                                        ContractName = b.Field<String>("ContractName"),
                                        ContractType = (ContractType)b.Field<Byte>("ContractType"),
                                        CarrierName = b.Field<String>("CarrierName"),
                                        ShippingLineName = b.Field<String>("ShippingLineName"),
                                        PaymentTermName = b.Field<String>("PaymentTermName"),
                                        CurrencyName = b.Field<String>("CurrencyName"),
                                        FromDate = b.Field<DateTime>("FromDate"),
                                        ToDate = b.Field<DateTime>("ToDate"),
                                        RateType = (RateType)b.Field<Byte>("RateType"),
                                        PublisherName = b.Field<String>("PublisherName"),
                                        PublishDate = b.Field<DateTime?>("PublishDate"),
                                        State = (OceanState)b.Field<Byte>("State"),
                                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                        Permission = (OceanPermission)b.Field<Byte>("Permission"),
                                        OceanCustomers = (from c in ds.Tables[2].AsEnumerable()
                                                          select new OceanCustomers
                                                          {
                                                              ShipperID = c.Field<Guid?>("ShipperID"),
                                                              ShipperName = c.Field<string>("ShipperName"),
                                                              ConsigneeID = c.Field<Guid?>("ConsigneeID"),
                                                              ConsigneeName = c.Field<string>("ConsigneeName"),
                                                              NotifyID = c.Field<Guid?>("NotifyPartyID"),
                                                              NotifyName = c.Field<string>("NotifyPartyName")
                                                          }
                                                         ).ToList(),
                                        OceanUnits = (from u in ds.Tables[1].AsEnumerable()
                                                      where u.Field<Guid>("OceanID") == b.Field<Guid>("ID")
                                                      select new OceanUnitList
                                                      {
                                                          ID = u.Field<Guid>("ID"),
                                                          OceanID = u.Field<Guid>("OceanID"),
                                                          UnitID = u.Field<Guid>("UnitID"),
                                                          UnitName = u.Field<string>("UnitName")
                                                      }).ToList(),
                                        IsDirty = false,
                                    }).SingleOrDefault();
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
        /// 拷贝合约信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="copyByID">拷贝人ID</param>
        /// <returns>返回合约详细信息</returns>
        public OceanInfo CopyOceanInfo(
            Guid oceanID,
            Guid copyByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(copyByID, "copyByID");
            try
            {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspCopyOceanInfo");

                    dbCommand.CommandTimeout = 600;//设置超时时限

                    db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, copyByID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    DataSet ds = db.ExecuteDataSet(dbCommand);
                    if (ds == null || ds.Tables.Count < 1)
                    {
                        throw new ICPException(this.IsEnglish?"Copy ocean price failed.":"拷贝海运运价失败。");
                    
                    }
                    OceanInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new OceanInfo
                                        {
                                            CarrierID = b.Field<Guid?>("CarrierID"),
                                            ShippingLineID = b.Field<Guid?>("ShippingLineID"),
                                            CurrencyID = b.Field<Guid>("CurrencyID"),
                                            PaymentTermID = b.Field<Guid?>("PaymentTermID"),
                                            Remark = b.Field<String>("Remark"),
                                            PublisherID = b.Field<Guid?>("PublisherID"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<String>("CreateByName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            ID = b.Field<Guid>("ID"),
                                            PermissionMode = (OceanPermissionMode)b.Field<byte>("PermissionMode"),
                                            ContractNo = b.Field<String>("ContractNo"),
                                            ContractName = b.Field<String>("ContractName"),
                                            ContractType = (ContractType)b.Field<Byte>("ContractType"),
                                            CarrierName = b.Field<String>("CarrierName"),
                                            ShippingLineName = b.Field<String>("ShippingLineName"),
                                            PaymentTermName = b.Field<String>("PaymentTermName"),
                                            CurrencyName = b.Field<String>("CurrencyName"),
                                            FromDate = b.Field<DateTime>("FromDate"),
                                            ToDate = b.Field<DateTime>("ToDate"),
                                            RateType = (RateType)b.Field<Byte>("RateType"),
                                            PublisherName = b.Field<String>("PublisherName"),
                                            PublishDate = b.Field<DateTime?>("PublishDate"),
                                            State = (OceanState)b.Field<Byte>("State"),
                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                            Permission = (OceanPermission)b.Field<Byte>("Permission"),
                                            OceanCustomers = (from c in ds.Tables[2].AsEnumerable()
                                                              select new OceanCustomers
                                                              {
                                                                  ShipperID = c.Field<Guid?>("ShipperID"),
                                                                  ShipperName = c.Field<string>("ShipperName"),
                                                                  ConsigneeID = c.Field<Guid?>("ConsigneeID"),
                                                                  ConsigneeName = c.Field<string>("ConsigneeName"),
                                                                  NotifyID = c.Field<Guid?>("NotifyPartyID"),
                                                                  NotifyName = c.Field<string>("NotifyPartyName")
                                                              }
                                                             ).ToList(),
                                            OceanUnits = (from u in ds.Tables[1].AsEnumerable()
                                                          where u.Field<Guid>("OceanID") == b.Field<Guid>("ID")
                                                          select new OceanUnitList
                                                          {
                                                              ID = u.Field<Guid>("ID"),
                                                              OceanID = u.Field<Guid>("OceanID"),
                                                              UnitID = u.Field<Guid>("UnitID"),
                                                              UnitName = u.Field<string>("UnitName")
                                                          }).ToList(),
                                           
                                        }).SingleOrDefault();
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
        /// 保存合约信息 
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="contractNo">合约号</param>
        /// <param name="contractName">合约名</param>
        /// <param name="contractType">合约类型</param>
        /// <param name="carrierID">船东</param>
        /// <param name="shippingLineID">航线</param>
        /// <param name="paymentTermID">付款方式</param>
        /// <param name="currencyID">币种</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="rateType">运价类型</param>
        /// <param name="shipperIDs">发货人</param>
        /// <param name="consigneeIDs">收货人</param>
        /// <param name="notifyIDs">通知人</param>
        /// <param name="unitIDs">运价单位</param>
        /// <param name="remark">备注</param>
        /// <param name="updateDate">数据版本</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回SingleResult</returns>
        public SingleResultData SaveOceanInfo(
                Guid? id,
                string contractNo,
                string contractName,
                ContractType contractType,
                Guid? carrierID,
                Guid? shippingLineID,
                Guid? paymentTermID,
                Guid currencyID,
                DateTime fromDate,
                DateTime toDate,
                RateType rateType,
                Guid?[] shipperIDs,
                Guid?[] consigneeIDs,
                Guid?[] notifyIDs,
                Guid[] unitIDs,
                string remark,
                Guid saveByID,
                DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(currencyID, "currencyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveOceanInfo");

                string tempShipperIds = shipperIDs.Join();
                string tempConsigneeIds = consigneeIDs.Join();
                string tempNotifyIds = notifyIDs.Join();
                string tempUnitIDs = unitIDs.Join();

                int[] rowPositions = new int[unitIDs.Length];
                for (int i = 0; i < unitIDs.Length; i++)
                {
                    rowPositions[i] = i;
                }
                string tempRowPositions = rowPositions.Join();

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ContractNo", DbType.String, contractNo);
                db.AddInParameter(dbCommand, "@ContractName", DbType.String, contractName);
                db.AddInParameter(dbCommand, "@ContractType", DbType.Byte, contractType);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, shippingLineID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, paymentTermID);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@RateType", DbType.Byte, rateType);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@ShipperIDs", DbType.String, tempShipperIds);
                db.AddInParameter(dbCommand, "@ConsigneeIDs", DbType.String, tempConsigneeIds);
                db.AddInParameter(dbCommand, "@NotifyPartyIDs", DbType.String, tempNotifyIds);
                db.AddInParameter(dbCommand, "@UnitIDs", DbType.String, tempUnitIDs);
                db.AddInParameter(dbCommand, "@RowPositions", DbType.String, tempRowPositions);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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
        /// 删除合约
        /// </summary>
        /// <param name="id">合约ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveOceanInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            FTPClient ftp = new FTPClient();
            try
            {

                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    //移除文件
                    List<OceanFileList> files = this.GetOceanFileList(id, removeByID);
                    if (files != null && files.Count > 0)
                    {
                        RemoveOceanFileInfo(files.Select(fItem => fItem.ID).ToArray(), removeByID,
                        files.Select(fItem => fItem.UpdateDate).ToArray());
                    }
                    

                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveOceanInfo");

                    db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                    db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    db.ExecuteNonQuery(dbCommand);

                    //if (files != null && files.Count > 0)
                    //{
                    //    ftp.RemoteHost = _ftpHost;
                    //    ftp.RemotePath = _ftpBasePath;
                    //    ftp.UserName = _ftpUser;
                    //    ftp.Password = _ftpPassword;
                    //    ftp.Login();

                    //    foreach (var item in files)
                    //    {
                    //        ftp.DeleteRemoteFile(item.ID.ToString());
                    //    }
                    //}
                    scope.Complete();
                }
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
            finally { ftp.Close(); }
        }

        /// <summary>
        /// 更改合约状态
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="state">状态</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        public SingleResultData ChangeOceanState(
            Guid oceanID,
            OceanState state,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                  
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspChangeOceanState");

                dbCommand.CommandTimeout = 600;//设置超时时限

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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
        /// 更改合约状态
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="state">状态</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        SingleResultData SetOceanPermissionMode(
            Guid oceanID,
            OceanPermissionMode mode,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSetOceanPermissionMode");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@mode", DbType.Byte, mode);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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
        /// 将一个合约下所有的运价明细导出到Excel
        /// </summary>
        /// <param name="oceanID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public OceanRateToExcel ExportOceanRateToExcel(Guid oceanID, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetSearchOceanRateListByOceanID");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);

                OceanRateToExcel toExcel = new OceanRateToExcel();

                if (ds == null || ds.Tables.Count < 1)
                {
                    toExcel.DataList = new List<OceanRateDataObject>();
                    toExcel.UnitList = new List<string>();
                   return toExcel;
                }

                #region linq

                List<string> unitList = (from b in ds.Tables[0].AsEnumerable()
                                         select b.Field<string>("UnitName")).ToList();

                List<OceanRateDataObject> results = (from b in ds.Tables[1].AsEnumerable()
                                                     select new OceanRateDataObject
                                                    {

                                                        CarrierName = b.Field<String>("CarrierName"),
                                                        POLName = b.Field<String>("POLName"),
                                                        VIAName = b.Field<String>("VIAName"),
                                                        PODName = b.Field<String>("PODName"),
                                                        DeliveryName = b.Field<String>("DeliveryName"),
                                                        FinalDestinationName=b.Field<String>("FinalDestinationName"),
                                                        ItemCode = b.Field<String>("ItemCode"),
                                                        Comm = b.Field<String>("Commodity"),
                                                        Term = b.Field<String>("Term"),
                                                        SurCharge = b.Field<String>("SurCharge"),
                                                        CLS=b.Field<String>("CLS"),
                                                        TT = b.Field<String>("TT"),
                                                        DurationForm = b.Field<DateTime?>("DurationFrom"),
                                                        DurationTo = b.Field<DateTime?>("DurationTo"),
                                                        CurrencyName=b.Field<String>("Currency"),
                                                        Description = b.Field<String>("Description"),
                                                      

                                                        Rate_20FR = unitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                                        Rate_20GP = unitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                                        Rate_20HQ = unitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                                        Rate_20HT = unitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                                        Rate_20NOR = unitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                                        Rate_20OT = unitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                                        Rate_20RF = unitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                                        Rate_20RH = unitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                                        Rate_20TK = unitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                                        Rate_40FR = unitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                                        Rate_40GP = unitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                                        Rate_40HQ = unitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                                        Rate_40HT = unitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                                        Rate_40NOR = unitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                                        Rate_40OT = unitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                                        Rate_40RF = unitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                                        Rate_40RH = unitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                                        Rate_40TK = unitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                                        Rate_45FR = unitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                                        Rate_45GP = unitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                                        Rate_45HQ = unitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                                        Rate_45HT = unitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                                        Rate_45OT = unitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                                        Rate_45RF = unitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                                        Rate_45RH = unitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                                        Rate_45TK = unitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                                        Rate_53HQ = unitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,
                                                    }).ToList();
                #endregion

                toExcel.DataList = results;
                toExcel.UnitList = unitList;
                return toExcel;

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

        #region BasePort

        /// <summary>
        /// 获得Base Port数据
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>返回合约的运价列表</returns>
        public BasePortRateList GetOceanBasePorts(Guid oceanID)
        {
            BasePortRateList ratelist = new BasePortRateList();
            
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanBasePorts");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@IsRow2Col", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

               
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    List<string> a = new List<string>();
                    List<ClientBasePortList> b = new List<ClientBasePortList>();
                    List<object> c = new List<object>();
                    c.Add(a);
                    c.Add(b);
                    ratelist.Data = c;
                    return ratelist;
                }

                #region linq


                List<string> unitList = (from b in ds.Tables[0].AsEnumerable()
                                         select b.Field<string>("UnitName")).ToList();
                    
                List<ClientBasePortList> results = (from b in ds.Tables[1].AsEnumerable()
                                                    select new ClientBasePortList
                                              { 
                                                  
                                                   _ID = b.Field<Guid>("ID"),
                                                  _OceanID = b.Field<Guid>("OceanID"),
                                                  _AccountType = (AccountType)b.Field<Byte>("AccountType"),
                                                  _CarrierID = b.Field<Guid?>("CarrierID"),
                                                  _CarrierName = b.Field<String>("CarrierName"),
                                                  _FromDate = b.Field<DateTime?>("FromDate"),
                                                  _ToDate = b.Field<DateTime?>("ToDate"),
                                                  _POLID = b.Field<Guid>("POLID"),
                                                 
                                                  _POLName = b.Field<String>("POLName"),
                                                  _VIAID = b.Field<Guid?>("VIAID"),
                                                  _VIAName = b.Field<String>("VIAName"),
                                                  _PODID = b.Field<Guid>("PODID"),
                                                  _PODName = b.Field<String>("PODName"),
                                                  _PlaceOfDeliveryID = b.Field<Guid?>("PlaceOfDeliveryID"),
                                                  _PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                                  _Comm = (string.IsNullOrEmpty(b.Field<String>("Comm")) ?
                                                           string.Empty : b.Field<String>("Comm").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),
                                                  _TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                  _TransportClauseName = b.Field<String>("TransportClauseName"),
                                                  _SurCharge = b.Field<String>("SurCharge"),
                                                  _ClosingDate = b.Field<String>("ClosingDate"),
                                                  _TransitTime = b.Field<String>("TransitTime"),
                                                  _Description = b.Field<String>("Description"),
                                                  _Account = b.Field<string>("Account"),
                                                  _No = b.Field<int>("No"),
                                                  IndexNo=b.Field<int>("No"),
                                                  _DestArb = b.Field<bool>("IsDestinationArbitrary"),
                                                  _OriginArb = b.Field<bool>("IsOriginalArbitrary"),
                                                  _ItemCode = b.Field<string>("ItemCode"),
                                                  //旧值，用来解决批量替换时的问题
                                                   OLDPOLName = b.Field<String>("POLName"),
                                                   OLDVIAName = b.Field<String>("VIAName"),
                                                   OLDPODName = b.Field<String>("PODName"),
                                                   OLDDeliveryName = b.Field<String>("PlaceOfDeliveryName"),

                                                  _Rate_20FR = unitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                                  _Rate_20GP = unitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                                  _Rate_20HQ = unitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                                  _Rate_20HT = unitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                                  _Rate_20NOR = unitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                                  _Rate_20OT = unitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                                  _Rate_20RF = unitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                                  _Rate_20RH = unitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                                  _Rate_20TK = unitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                                  _Rate_40FR = unitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                                  _Rate_40GP = unitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                                  _Rate_40HQ = unitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                                  _Rate_40HT = unitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                                  _Rate_40NOR = unitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                                  _Rate_40OT = unitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                                  _Rate_40RF = unitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                                  _Rate_40RH = unitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                                  _Rate_40TK = unitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                                  _Rate_45FR = unitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                                  _Rate_45GP = unitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                                  _Rate_45HQ = unitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                                  _Rate_45HT = unitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                                  _Rate_45OT = unitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                                  _Rate_45RF = unitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                                  _Rate_45RH = unitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                                  _Rate_45TK = unitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                                  _Rate_53HQ = unitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,

                                              }).ToList();

                List<object> list = new List<object>();

                list.Add(unitList);
                list.Add(results);
                ratelist.Data = list;

                #endregion

                return ratelist;

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

        ///// <summary>
        ///// 获得Base Port数据(压缩后的)
        ///// </summary>
        ///// <param name="oceanID"></param>
        ///// <returns></returns>
       // public Byte[] GetOceanBasePortsZip(Guid oceanID)
       // {
            //List<object>  list = GetOceanBasePorts(oceanID);

          //  return DataZipStream.CompressionArrayList(list);

        //}

        /// <summary>
        /// 保存运价信息
        /// (大批量时候，需要分批传入)
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="itemCollect">运价XML内容</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="isCopyBasePort">是否包含了Copy数据</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveOceanBasePorts(
            Guid oceanID,
            BasePortCollect itemCollect,
            bool isCopyBasePort,
            Guid saveByID)
        {

            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {

                #region 转换ID
                foreach (var item in itemCollect.BasePortItem)
                {
                    if (item.ID.IsNullOrEmpty())
                    {
                        item.ID = Guid.NewGuid();
                        foreach (var unit in item.UnitRates)
                        {
                            unit.ParentID = item.ID;
                        }
                    }
                }
                #endregion

                ManyResult resultList = new ManyResult();
                resultList.Items = new List<SingleResult>();
                #region 判断数据
                if (itemCollect.BasePortItem == null)
                {
                    return null;
                }

                int scount = itemCollect.BasePortItem.Count / 1000;
                if ((itemCollect.BasePortItem.Count % 1000) > 0)
                {
                    scount++;
                }

                int min = 0;
                int max = 0;
                int num = 0;
                #endregion

                for (int i = 1; i <= scount; i++)
                {
                    #region 获得行数
                    if (i == 1)
                    {
                        min = 0;
                        max = 1000;
                    }
                    else
                    {
                        min = max + 1;
                        max = i * 1000;
                    }
                    if (max > itemCollect.BasePortItem.Count)
                    {
                        max = itemCollect.BasePortItem.Count;
                    }
                    if (min > max)
                    {
                        break;
                    }
                    else if (min == max)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = max - min;//本次取出多少行
                        if (min > 0)
                        {
                            min--;
                            num++;   
                        }
                    }
                    #endregion

                    #region 执行存储过程
                    BasePortCollect itemList = new BasePortCollect();
                    itemList.BasePortItem = itemCollect.BasePortItem.Skip(min).Take(num).ToList();


                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveOceanBasePorts");

                    string xmlcontent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<BasePortCollect>(itemList, true, true);

                    db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                    db.AddInParameter(dbCommand, "@XmlOceanItemContent", DbType.Xml, xmlcontent);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@IsAsy", DbType.Boolean, !isCopyBasePort);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                    ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate", "No" });

                    resultList.Items.AddRange(result.Items);

                    #endregion
                }

                return resultList;
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
        /// 保存运价信息(压缩后的数据)
        /// (大批量时候，需要分批传入)
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="itemCollect">运价XML内容</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveOceanBasePortsZip(
            Guid oceanID,
            byte[] itemCollects,
            Guid saveByID)
        {

            BasePortCollect itemCollect = (BasePortCollect)DataZipStream.DecompressionObject(itemCollects);

            return SaveOceanBasePorts(oceanID,itemCollect,false,saveByID);

        }



        /// <summary>
        /// 删除 BasePorts
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">更改人</param>
        /// <param name="updateDates">数据版本</param>
        public ManyResultData RemoveBasePorts(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveBasePorts");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@removeByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 将一个BasePort关联的所有AdditionalFee关联到另外一个BasePort中
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="copyIds"></param>
        /// <param name="isEnglish"></param>
        public void SaveOceanItem2AdditionalFeeByBasePort(Guid[] ids, Guid[] copyIds,Guid saveById ,bool isEnglish)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveOceanItem2AddFeeByBasePort");

                string tempIds = ids.Join();
                string tempCopyIds = copyIds.Join();

                db.AddInParameter(dbCommand, "@BaseIDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CopyIds", DbType.String,tempCopyIds );
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);
             

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        #endregion

        #region Arbitrary


        /// <summary>
        /// GetOceanArbitrarysByBasePortID
        /// </summary>
        /// <param name="basePortID">basePortID</param>
        /// <returns>ArbitraryList</returns>
        public List<ArbitraryList> GetOceanArbitrarysByBasePortID(Guid basePortID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanArbitrarysByBasePortID");

                db.AddInParameter(dbCommand, "@BasePortID", DbType.Guid, basePortID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ArbitraryList> results = BulidArbitraryByDataSet(ds, true);
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
        /// 获取合约的Arbitrarys运价列表
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>返回合约的运价列表</returns>
        public List<ArbitraryList> GetOceanArbitrarys(Guid oceanID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanArbitrarys");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ArbitraryList> results = BulidArbitraryByDataSet(ds, false);
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

        private static List<ArbitraryList> BulidArbitraryByDataSet(DataSet ds, bool bulidAssociatedType)
        {
            List<ArbitraryList> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new ArbitraryList
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               OceanID = b.Field<Guid>("OceanID"),
                                               GeographyType = (GeographyType)b.Field<Byte>("Type"),
                                               ModeOfTransport = (ModeOfTransport)b.Field<Byte>("ModeOfTransport"),
                                               No = b.Field<int>("No"),
                                               AssociatedType = bulidAssociatedType ? (GeographyType)b.Field<Byte>("AssociatedType") : GeographyType.None,
                                               POLID = b.Field<Guid>("POLID"),
                                               POLName = b.Field<String>("POLName"),
                                               PODID = b.Field<Guid>("PODID"),
                                               PODName = b.Field<String>("PODName"),
                                               TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                               TransportClauseName = b.Field<String>("TransportClauseName"),
                                               ItemCode = b.Field<string>("ItemCode"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<String>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               Remark=b.Field<string>("Remark"),
                                               UnitRates = (from u in ds.Tables[1].AsEnumerable()
                                                            where u.Field<Guid>("OceanArbitraryID") == b.Field<Guid>("ID")
                                                            select new FrmUnitRateList
                                                            {
                                                                ID = u.Field<Guid>("ID"),
                                                                ParentID = u.Field<Guid>("OceanArbitraryID"),
                                                                Rate = u.Field<decimal>("Rate"),
                                                                UnitID = u.Field<Guid>("UnitID"),
                                                                UnitName = u.Field<string>("UnitName")
                                                            }).ToList(),
                                               IsDirty = false

                                           }).ToList();
            return results;
        }

        /// <summary>
        /// 保存运价信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="itemCollect">运价XML内容</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveOceanArbitrarys(
            Guid oceanID,
            ArbitraryCollect itemCollect,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            foreach (var item in itemCollect.ArbitraryItem)
            {
                if (item.ID.IsNullOrEmpty())
                {
                    item.ID = Guid.NewGuid();
                    foreach (var unit in item.UnitRates)
                    {
                        unit.ParentID = item.ID;
                    }
                }
            }


            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveOceanArbitrarys");

                string xmlcontent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<ArbitraryCollect>(itemCollect, true, true);

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@XmlOceanItemContent", DbType.Xml, xmlcontent);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);


                DataSet ds = null;
                dbCommand.CommandTimeout = 600;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                EnumerableRowCollection<DataRow> rows = ds.Tables[0].AsEnumerable();
                if (rows.Count() == 0)
                {
                    return null;
                }
                string[] propertyNames = new string[3] { "ID","UpdateDate","No"};


                ManyResult result = new ManyResult();
                SingleResult child = null;
                foreach (DataRow row in rows)
                {
                    child = new SingleResult();
                    foreach (string p in propertyNames)
                    {
                        object val = row.Field<object>(p);
                        child.Add(p, val);
                    }
                    result.Items.Add(child);
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
        /// 删除 Arbitrarys
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">更改人</param>
        /// <param name="updateDates">数据版本</param>
        public ManyResultData RemoveArbitrarys(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveArbitrarys");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@removeByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        #endregion

        #region AdditionalFees

        /// <summary>
        /// 获取合约的AdditionalFees运价列表
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <returns>返回合约下费用列表</returns>
        public List<AdditionalFeeList> GetOceanAdditionalFees(Guid oceanID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanAdditionalFees");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<AdditionalFeeList>();
                }

                List<AdditionalFeeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new AdditionalFeeList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       OceanID = b.Field<Guid>("OceanID"),
                                                       AssociatedCount = b.Field<int>("AssociatedCount"),
                                                       ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                       ChargingCode = b.Field<String>("ChargingCode"),
                                                       ChargingCodeDescription = b.Field<String>("ChargingCodeDescription"),
                                                       CustomerID = b.Field<Guid?>("CustomerID"),
                                                       CustomerName = b.Field<String>("CustomerName"),
                                                       CurrencyID = b.Field<Guid>("CurrencyID"),
                                                       CurrencyName = b.Field<String>("CurrencyName"),
                                                       Percent = b.Field<Int16>("uPercent"),
                                                       //Remark = b.Field<String>("Remark"),
                                                       IsSpecialFee = b.Field<Boolean>("IsSpecialFee"),
                                                       CreateByID = b.Field<Guid>("CreateByID"),
                                                       CreateByName = b.Field<String>("CreateByName"),
                                                       CreateDate = b.Field<DateTime>("CreateDate"),
                                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                       FromDate = b.Field<DateTime?>("FromDate"),
                                                       ToDate = b.Field<DateTime?>("ToDate"),
                                                       Remark = b.Field<String>("DESCRIPTION"),

                                                       BaseRateIDs = (from t in ds.Tables[2].AsEnumerable()
                                                                      where t.Field<Guid>("AdditionalFeeID") == b.Field<Guid>("ID")
                                                                      select t.Field<Guid>("BaseItemID")
                                                                    ).ToList(),

                                                       UnitRates = (from u in ds.Tables[1].AsEnumerable()
                                                                    where u.Field<Guid>("AdditionalFeeID") == b.Field<Guid>("ID")
                                                                    select new FrmUnitRateList
                                                                    {
                                                                        ID = u.Field<Guid>("ID"),
                                                                        ParentID = u.Field<Guid>("AdditionalFeeID"),
                                                                        Rate = u.Field<decimal>("Rate"),
                                                                        UnitID = u.Field<Guid>("UnitID"),
                                                                        UnitName = u.Field<string>("UnitName")
                                                                    }).ToList(),
                                                       IsDirty = false,
                                                   }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 根据BasePort获得关联的AdditionalFeeList
        /// </summary>
        /// <param name="oceanPort"></param>
        /// <returns></returns>
        public List<AdditionalFeeList> GetOceanAdditionalFeesByBasePort(Guid basePortID)
        {
            ArgumentHelper.AssertGuidNotEmpty(basePortID, "basePortID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanAdditionalByBasePortID");

                db.AddInParameter(dbCommand, "@BasePortID", DbType.Guid, basePortID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<AdditionalFeeList>();
                }

                List<AdditionalFeeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new AdditionalFeeList
                                                   {
                                                       ID = b.Field<Guid>("AdditionID"),
                                                       ChargingCodeID = b.Field<Guid>("FeeID"),
                                                       ChargingCode = b.Field<String>("FeeCode"),
                                                       ChargingCodeDescription = b.Field<String>("FeeName"),
                                                       CurrencyName = b.Field<String>("CurrencyName"),
                                                       Percent = b.Field<Int16>("uPercent"),
                                                       Remark = b.Field<String>("Remark"),

                                                       UnitRates = (from u in ds.Tables[1].AsEnumerable()
                                                                    where b.Field<Guid>("AdditionID") == u.Field<Guid>("AdditionID")
                                                                    select new FrmUnitRateList
                                                                    {                                   
                                                                        Rate = u.Field<decimal>("Rate"),
                                                                        UnitID = u.Field<Guid>("UnitID"),
                                                                        UnitName = u.Field<string>("UnitName")
                                                                    }).ToList(),
                                                       IsDirty = false,
                                                   }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存合约费用信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="fees">费用列表</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回合约费用信息</returns>
        public ManyResult SaveOceanAdditionalFees(
            Guid oceanID,
            AdditionalFeeCollect fees,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            foreach (var item in fees.AdditionalFees)
            {
                if (item.ID.IsNullOrEmpty())
                {
                    item.ID = Guid.NewGuid();
                    foreach (var unit in item.UnitRates)
                    {
                        unit.ParentID = item.ID;
                    }
                }
            }


            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveOceanAdditionalFees");

                string xmlContent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<AdditionalFeeCollect>(fees, true, true);

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@XmlContent", DbType.String, xmlContent);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 删除合约费用信息
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveAdditionalFeeInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveAdditionalFeeInfo");

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

        /// <summary>
        /// SetOceanAdditionalFee2ItemInfo
        /// </summary>
        /// <param name="feeIDs">对应的费用ID列表</param>
        /// <param name="rateIDs">运价ID列表</param>
        /// <param name="isAdditionals">标识是删除还是新增</param>
        /// <param name="setByID">设置人</param>
        public ManyResult SetOceanAdditionalFee2ItemInfo(
            Guid[] feeIDs,
            Guid[] rateIDs,
            bool[] isAdditionals,
            Guid setByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(feeIDs, "feeIDs");
            ArgumentHelper.AssertGuidNotEmpty(rateIDs, "rateIDs");
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSetOceanAdditionalFee2ItemInfo");


                string tempfeeIDs = feeIDs.Join();
                string tempOceanItemIDs = rateIDs.Join();
                string tempIsAdditionals = isAdditionals.Join();
                db.AddInParameter(dbCommand, "@RateIDs", DbType.String, tempOceanItemIDs);
                db.AddInParameter(dbCommand, "@FeeIDs", DbType.String, tempfeeIDs);
                db.AddInParameter(dbCommand, "@IsAdditionals", DbType.String, tempIsAdditionals);

                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取其他费用下关联的运价列表
        /// </summary>
        /// <param name="additionalFeeIDs">其他费用ID列表</param>
        /// <returns>返回其他费用下关联的运价列表</returns>
        public List<AdditionalFee2ItemList> GetOceanAdditionalFee2ItemList(Guid[] additionalFeeIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(additionalFeeIDs, "additionalFeeIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetAdditionalFee2ItemList");

                string tempAdditionalFeeIds = additionalFeeIDs.Join();
                db.AddInParameter(dbCommand, "@AdditionalFeeIDs", DbType.String, tempAdditionalFeeIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AdditionalFee2ItemList> results = (from b in ds.Tables[0].AsEnumerable()
                                                        select new AdditionalFee2ItemList
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            AdditionalFeeID = b.Field<Guid>("AdditionalFeeID"),
                                                            OceanItemID = b.Field<Guid>("OceanItemID")
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
        /// 获取运价下关联的其他费用列表
        /// </summary>
        /// <param name="oceanItemIDs">运价ID列表</param>
        /// <returns>返回运价下关联的其他费用列表</returns>
        public List<AdditionalFee2ItemList> GetOceanItem2AdditionalFeeList(Guid[] oceanItemIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanItemIDs, "oceanItemIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanItem2AdditionalFeeList");

                string tempAdditionalFeeIds = oceanItemIDs.Join();
                db.AddInParameter(dbCommand, "@OceanItemIDs", DbType.String, tempAdditionalFeeIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AdditionalFee2ItemList> results = (from b in ds.Tables[0].AsEnumerable()
                                                        select new AdditionalFee2ItemList
                                                        {
                                                            ID = b.Field<Guid>("ID"),
                                                            AdditionalFeeID = b.Field<Guid>("AdditionalFeeID"),
                                                            OceanItemID = b.Field<Guid>("OceanItemID")
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

        #region BaseRates

        /// <summary>
        /// 获取BaseRates列表
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="pols">pols</param>
        /// <param name="isExclPols">isExclpols</param>
        /// <param name="vias">vias</param>
        /// <param name="isExclVias">isExclvias</param>
        /// <param name="pods">pods</param>
        /// <param name="isExclPods">isExclpods</param>
        /// <param name="deliverys">deliverys</param>
        /// <param name="isExclDeliverys">isExcldeliverys</param>
        /// <param name="itemCodes">itemCodes</param>
        /// <param name="isExclItemCodes">isExclitemCodes</param>
        /// <param name="carriers">carriers</param>
        /// <param name="isExclCarriers">isExclcarriers</param>
        /// <param name="comm">comm</param>
        /// <param name="isExclComm">isExclcomm</param>
        /// <param name="terms">terms</param>
        /// <param name="isExclTerms">isExclterms</param>
        /// <param name="surCharges">surCharges</param>
        /// <param name="isExclSurCharges">isExclsurCharges</param>
        /// <param name="description">description</param>
        /// <param name="isExclDescription">isExcldescription</param>
        /// <returns>BaseRatesList</returns>
        public BaseRateList GetOceanBaseRates(Guid oceanID
                                            , string pols, bool isExclPols
                                            , string vias, bool isExclVias
                                            , string pods, bool isExclPods
                                            , string deliverys, bool isExclDeliverys
                                            , string finalDestinations, bool isExclfinalDestinations
                                            , string itemCodes, bool isExclItemCodes
                                            , string carriers, bool isExclCarriers
                                            , string comm, bool isExclComm
                                            , string terms, bool isExclTerms
                                            , string surCharges, bool isExclSurCharges
                                            , string description, bool isExclDescription)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanBaseRates");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@pols", DbType.String, pols.IsNullOrEmpty()? string.Empty : pols.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@vias", DbType.String, vias.IsNullOrEmpty()? string.Empty : vias.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@pods", DbType.String, pods.IsNullOrEmpty()? string.Empty : pods.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@deliverys", DbType.String, deliverys.IsNullOrEmpty() ? string.Empty : deliverys.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@finalDestinations", DbType.String, finalDestinations.IsNullOrEmpty() ? string.Empty : finalDestinations.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@itemCodes", DbType.String, itemCodes.IsNullOrEmpty() ? string.Empty : itemCodes.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@carriers", DbType.String, carriers.IsNullOrEmpty() ? string.Empty : carriers.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@comm", DbType.String, comm);
                db.AddInParameter(dbCommand, "@terms", DbType.String, terms.IsNullOrEmpty() ? string.Empty : terms.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@surCharges", DbType.String, surCharges.IsNullOrEmpty()? string.Empty : surCharges.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));
                db.AddInParameter(dbCommand, "@description", DbType.String, description.IsNullOrEmpty()? string.Empty : description.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol));


                #region isExcl
                db.AddInParameter(dbCommand, "@isExclPols", DbType.Boolean, isExclPols);
                db.AddInParameter(dbCommand, "@isExclVias", DbType.Boolean, isExclVias);
                db.AddInParameter(dbCommand, "@isExclPods", DbType.Boolean, isExclPods);
                db.AddInParameter(dbCommand, "@isExclDeliverys", DbType.Boolean, isExclDeliverys);
                db.AddInParameter(dbCommand, "@isExclfinalDestinations", DbType.Boolean, isExclfinalDestinations);
                db.AddInParameter(dbCommand, "@isExclItemCodes", DbType.Boolean, isExclItemCodes);
                db.AddInParameter(dbCommand, "@isExclcarriers", DbType.Boolean, isExclCarriers);
                db.AddInParameter(dbCommand, "@isExclComm", DbType.Boolean, isExclComm);
                db.AddInParameter(dbCommand, "@isExclTerms", DbType.Boolean, isExclTerms);
                db.AddInParameter(dbCommand, "@isExclSurCharges", DbType.Boolean, isExclSurCharges);
                db.AddInParameter(dbCommand, "@isExclDescription", DbType.Boolean, isExclDescription);
                #endregion
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IsRow2Col", DbType.Boolean, true);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
             

                return BulidBaseRatesListByDataSet(ds);

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

        private BaseRateList BulidBaseRatesListByDataSet(DataSet ds)
        {
            BaseRateList baseList = new BaseRateList();
            List<object> list = new List<object>();

            if (ds.Tables.Count == 0)
            {
                List<string> a = new List<string>();
                List<ClientBaseRatesList> b = new List<ClientBaseRatesList>();

                list.Add(a);
                list.Add(b);


                baseList.Data = list;

            }


            List<string> unitList = (from b in ds.Tables[0].AsEnumerable()
                                     select b.Field<string>("UnitName")).ToList();


            List<ClientBaseRatesList> results = (from b in ds.Tables[1].AsEnumerable()
                                           select new ClientBaseRatesList
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               OceanID = b.Field<Guid>("OceanID"),
                                               AccountType = (AccountType)b.Field<Byte>("AccountType"),
                                               CarrierID = b.Field<Guid?>("CarrierID"),
                                               CarrierName = b.Field<String>("CarrierName"),
                                               FromDate = b.Field<DateTime?>("FromDate"),
                                               ToDate = b.Field<DateTime?>("ToDate"),
                                               POLID = b.Field<Guid>("POLID"),
                                               POLName = b.Field<String>("POLName"),
                                               VIAID = b.Field<Guid?>("VIAID"),
                                               VIAName = b.Field<String>("VIAName"),
                                               PODID = b.Field<Guid>("PODID"),
                                               PODName = b.Field<String>("PODName"),
                                               PlaceOfDeliveryID = b.Field<Guid>("PlaceOfDeliveryID"),
                                               PlaceOfDeliveryName = b.Field<String>("PlaceOfDeliveryName"),
                                               FinalDestinationID=b.Field<Guid?>("FinalDestinationID"),
                                               FinalDestinationName=b.Field<String>("FinalDestinationName"),
                                               Comm = (string.IsNullOrEmpty(b.Field<String>("Comm")) ?string.Empty : b.Field<String>("Comm").Replace(GlobalConstants.DividedSymbol, GlobalConstants.ShowDividedSymbol)),
                                               TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                               TransportClauseName = b.Field<String>("TransportClauseName"),
                                               SurCharge = b.Field<String>("SurCharge"),
                                               ClosingDate = b.Field<String>("ClosingDate"),
                                               TransitTime = b.Field<String>("TransitTime"),
                                               Description = b.Field<String>("Description"),
                                               Account = b.Field<string>("Account"),
                                               BasePortID = b.Field<Guid>("BasePortID"),
                                               DestinationArbitraryID = b.Field<Guid?>("DestinationArbitraryID"),
                                               OriginalArbitraryID = b.Field<Guid?>("OriginalArbitraryID"),
                                               BasePortNo = b.Field<int?>("BasePortNo"),
                                               DestinationArbitraryNo = b.Field<int?>("DestinationArbitraryNo"),
                                               OriginalArbitraryNo = b.Field<int?>("OriginalArbitraryNo"),
                                               ItemCode = b.Field<string>("ItemCode"),            
                                               //UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               IsDirty = false,
                                               Rate_20FR = unitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                               Rate_20GP = unitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                               Rate_20HQ = unitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                               Rate_20HT = unitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                               Rate_20NOR = unitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                               Rate_20OT = unitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                               Rate_20RF = unitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                               Rate_20RH = unitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                               Rate_20TK = unitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                               Rate_40FR = unitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                               Rate_40GP = unitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                               Rate_40HQ = unitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                               Rate_40HT = unitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                               Rate_40NOR = unitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                               Rate_40OT = unitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                               Rate_40RF = unitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                               Rate_40RH = unitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                               Rate_40TK = unitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                               Rate_45FR = unitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                               Rate_45GP = unitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                               Rate_45HQ = unitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                               Rate_45HT = unitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                               Rate_45OT = unitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                               Rate_45RF = unitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                               Rate_45RH = unitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                               Rate_45TK = unitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                               Rate_53HQ = unitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,
                                           }).ToList();

            list.Add(unitList);
            list.Add(results);

            baseList.Data = list;

            return baseList;

        }

        /// <summary>
        /// 获取BaseRates列表
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>BaseRatesList</returns>
        public BaseRateList GetOceanBaseRatesByIds(Guid[] ids)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanBaseRatesByIDs");


                string tempids = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempids);
                db.AddInParameter(dbCommand, "@IsRow2Col", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
           

                return BulidBaseRatesListByDataSet(ds);

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }

        #endregion

        #region Permission

        /// <summary>
        /// 获取文档权限列表
        /// </summary>        
        /// <returns>返回权限列表</returns>
        public List<OceanPermissionList> GetOceanPermissionList(Guid oceanID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanPermissionList");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanPermissionList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new OceanPermissionList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Type = (ICP.FRM.ServiceInterface.DataObjects.UserObjectType)b.Field<Byte>("UserObjectType"),
                                                         UserID = b.Field<Byte>("UserObjectType") == 2 ? b.Field<Guid?>("UseObjectID") : null,
                                                         UserName = b.Field<Byte>("UserObjectType") == 2 ? b.Field<String>("UseObjectName") : string.Empty,
                                                         OrganizationID = b.Field<Guid?>("OrganizationID"),
                                                         OrganizationName = b.Field<string>("OrganizationName"),
                                                         JobID = b.Field<Byte>("UserObjectType") == 1 ? b.Field<Guid?>("UseObjectID") : null,
                                                         JobName = b.Field<Byte>("UserObjectType") == 1 ? b.Field<String>("UseObjectName") : string.Empty,
                                                         Permission = (OceanPermission)b.Field<byte>("Permission"),
                                                         CreateByID = b.Field<Guid>("CreateByID"),
                                                         CreateByName = b.Field<String>("CreateByName"),
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
        /// 删除Ocean权限信息
        /// </summary>
        public void RemoveOceanPermissionInfo(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveOceanPermissionInfo");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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
        /// 设置权限
        /// </summary>
        /// <param name="oceanID">oceanID</param>
        /// <param name="permissionIDs">permissionIDs</param>
        /// <param name="organizationIDs">organizationIDs</param>
        /// <param name="userObjectIDs">userObjectIDs</param>
        /// <param name="userObjectTypes">userObjectTypes</param>
        /// <param name="permissions">permissions</param>
        /// <param name="updateDates">updateDates</param>
        /// <param name="setById">setById</param>
        /// <returns></returns>
        public ManyResult SetOceanPermissionInfo(
             Guid oceanID,
             Guid?[] permissionIDs,
             Guid?[] organizationIDs,
             Guid?[] userObjectIDs,
             ICP.FRM.ServiceInterface.DataObjects.UserObjectType?[] userObjectTypes,
             OceanPermission[] permissions,
             DateTime?[] updateDates,
             Guid setById)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSetOceanPermissionInfo");

                string tempPermissionIds = permissionIDs.Join();
                string tempOrganizationIds = organizationIDs.Join();
                string tempUserObjectIDs = userObjectIDs.Join();
                string tempUserObjectTypes = userObjectTypes.Join<ICP.FRM.ServiceInterface.DataObjects.UserObjectType?>();
                string tempPermissions = permissions.Join<OceanPermission>();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@OceanPermissionIds", DbType.String, tempPermissionIds);
                db.AddInParameter(dbCommand, "@OrganizationIds", DbType.String, tempOrganizationIds);
                db.AddInParameter(dbCommand, "@UseObjectIDs", DbType.String, tempUserObjectIDs);
                db.AddInParameter(dbCommand, "@UseObjectTypes", DbType.String, tempUserObjectTypes);
                db.AddInParameter(dbCommand, "@Permissions", DbType.String, tempPermissions);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SetById", DbType.Guid, setById);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }

        #endregion

        #region Attachment

        /// <summary>
        /// 获取合约下文件列表 
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="userID">当前用户ID</param>
        /// <returns>返回合约下文件列表 </returns>
        public List<OceanFileList> GetOceanFileList(
            Guid oceanID,
            Guid userID)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanFileList");

                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OceanFileList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new OceanFileList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   OceanID = b.Field<Guid>("OceanID"),
                                                   FileID = b.Field<Guid>("FileID"),
                                                   FileName = b.Field<String>("FileName"),
                                                   Remark = b.Field<String>("Remark"),
                                                   CreateByID = b.Field<Guid>("CreateByID"),
                                                   CreateByName = b.Field<String>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                   Permission = (OceanPermission)b.Field<byte>("Permission"),
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

        ///// <summary>
        ///// 获取合约下文件信息
        ///// </summary>
        ///// <param name="id">ID</param>
        ///// <returns>返回合约下文件信息</returns>
        //public OceanFileInfo GetOceanFileInfo(Guid id)
        //{
        //    return null;
        //}

        /// <summary>
        /// 保存合约下文件信息
        /// </summary>
        /// <param name="oceanID">合约ID</param>
        /// <param name="ids">ID</param>
        /// <param name="names">文件名</param>
        /// <param name="descriptions">文件描述</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间</param>
        /// <returns>返回SingleResult</returns>
        public ManyResult SaveOceanFileInfo(
            Guid oceanID,
            Guid?[] ids,
            string[] names,
            string[] descriptions,
            //byte[] fileContent,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(oceanID, "oceanID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspSaveOceanFileInfo");

                string tempIDs = ids.Join();
                string tempNames = names.Join();
                string tempDescriptions = descriptions.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);
                db.AddInParameter(dbCommand, "@FileNames", DbType.String, tempNames);
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, tempDescriptions);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new[] { "ID","FileID","UpdateDate" });
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
        /// 删除合约下文件信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveOceanFileInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");
            ArgumentHelper.AssertArrayLengthMatch(
                ids,
                updateDates);


            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspRemoveOceanFileInfo");

                string tempIDs = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回</returns>
        public ICP.FRM.ServiceInterface.DataObjects.FTPServerConfig GetFTPServerConfig()
        {
            return new ICP.FRM.ServiceInterface.DataObjects.FTPServerConfig(
                _ftpHost,
                _ftpUser,
                _ftpPassword,
                _ftpBasePath);
        }

        #endregion

        #region SaveOceanDatas


        /// <summary>
        /// 保存所有关于合约的信息(BasePorts,Arbitrarys,AdditionalFee,Permission
        /// </summary>
        /// <param name="oceanID">oceanID</param>
        /// <param name="basePorts">basePorts</param>
        /// <param name="isCopyBasePort">isCopyBasePort</param>
        /// <param name="arbitrarys">arbitrarys</param>
        /// <param name="fees">fees</param>
        /// <param name="permissionMode">permissionMode</param>
        /// <param name="permissions">permissions</param>
        /// <param name="saveByID">saveByID</param>
        /// <returns></returns>
        public OceanSavedResult SaveOceanDatas(
                                    Guid oceanID
                                    , BasePortCollect basePorts
                                    , bool isCopyBasePort
                                    , ArbitraryCollect arbitrarys
                                    , AdditionalFeeCollect fees
                                    , PermissionsModeCollect permissionMode
                                    , PermissionsCollect permissions
                                    , Guid saveByID)
        {

            try
            {
                TransactionOptions option = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    Timeout = tsTimeOut,
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    OceanSavedResult result = new OceanSavedResult();

                    if (basePorts != null)
                    {
                        result.BasePortResult = this.SaveOceanBasePorts(oceanID, basePorts, isCopyBasePort, saveByID);
                    }
                    if (arbitrarys != null)
                    {
                        result.ArbitraryResult = this.SaveOceanArbitrarys(oceanID, arbitrarys, saveByID);
                    }
                    if (fees != null)
                    {
                        result.AdditionalFeeResult = this.SaveOceanAdditionalFees(oceanID, fees, saveByID);
                    }
                    if (permissionMode != null)
                    {
                        result.PermissionsModeResult = SetOceanPermissionMode(oceanID, permissionMode.Mode, saveByID, permissionMode.OceanUpdateDate);
                    }

                    if (permissions != null)
                    {
                        result.PermissionsResult = SetOceanPermissionInfo(oceanID
                                                            , permissions.permissionIds.ToArray()
                                                            , permissions.organizationIds.ToArray()
                                                            , permissions.userObjectIDs.ToArray()
                                                            , permissions.types.ToArray()
                                                            , permissions.permissions.ToArray()
                                                            , permissions.updateDates.ToArray()
                                                            , saveByID);

                    }

                    scope.Complete();
                    return result;

                }
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 获得合约比较列表
        /// <summary>
        /// 获得合约比较信息列表
        /// </summary>
        /// <param name="contract1ID">合约1ID</param>
        /// <param name="contract2ID">合约2ID</param>
        /// <returns></returns>
        public OceanContractComparDataList GetComparList(Guid contract1ID, Guid contract2ID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspOceanRateCompareInfo");

                db.AddInParameter(dbCommand, "@OceanOID", DbType.Guid, contract1ID);
                db.AddInParameter(dbCommand, "@OceanTID", DbType.Guid, contract2ID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                #region 获得箱型
                List<string> unitList = new List<string>();


                List<string> unit1List = (from b in ds.Tables[0].AsEnumerable()
                                         select b.Field<string>("UnitName")).ToList();

                List<string> unit2List = (from b in ds.Tables[1].AsEnumerable()
                                         select b.Field<string>("UnitName")).ToList();

                foreach (string str in unit1List)
                {
                    if (!unitList.Contains(str))
                    {
                        unitList.Add(str);
                    }
                }

                foreach (string str in unit2List)
                {
                    if (!unitList.Contains(str))
                    {
                        unitList.Add(str);
                    }
                }
                #endregion


                List<OceanContractCompar> list = (from b in ds.Tables[2].AsEnumerable()
                                                  select new OceanContractCompar
                                                  {
                                                  

                                                      C1_ID=b.Field<Guid?>("C1_ID"),
                                                      C1_POL = b.Field<String>("C1_POLName"),
                                                      C1_VIL = b.Field<String>("C1_VIAName"),
                                                      C1_POD = b.Field<String>("C1_PODName"),
                                                      C1_Delivery = b.Field<String>("C1_PlaceOfDeliveryName"),
                                                      C1_Term = b.Field<String>("C1_TransportClauseName"),
                                                      C1_ItemCode = b.Field<String>("C1_ItemCode"),
                                                      C1_20FR = unit1List.Contains("20FR")?b.Field<decimal?>("C1_20FR"):0,
                                                      C1_20GP = unit1List.Contains("20GP") ? b.Field<decimal?>("C1_20GP") : 0,
                                                      C1_20HQ = unit1List.Contains("20HQ") ? b.Field<decimal?>("C1_20HQ") : 0,
                                                      C1_20HT = unit1List.Contains("20HT") ? b.Field<decimal?>("C1_20HT") : 0,
                                                      C1_20NOR = unit1List.Contains("20NOR") ? b.Field<decimal?>("C1_20NOR") : 0,
                                                      C1_20OT = unit1List.Contains("20OT") ? b.Field<decimal?>("C1_20OT") : 0,
                                                      C1_20RF = unit1List.Contains("20RF") ? b.Field<decimal?>("C1_20RF") : 0,
                                                      C1_20RH = unit1List.Contains("20RH") ? b.Field<decimal?>("C1_20RH") : 0,
                                                      C1_20TK = unit1List.Contains("20TK") ? b.Field<decimal?>("C1_20TK") : 0,
                                                      C1_40FR = unit1List.Contains("40FR") ? b.Field<decimal?>("C1_40FR") : 0,
                                                      C1_40GP = unit1List.Contains("40GP") ? b.Field<decimal?>("C1_40GP") : 0,
                                                      C1_40HQ = unit1List.Contains("40HQ") ? b.Field<decimal?>("C1_40HQ") : 0,
                                                      C1_40HT = unit1List.Contains("40HT") ? b.Field<decimal?>("C1_40HT") : 0,
                                                      C1_40NOR = unit1List.Contains("40NOR") ? b.Field<decimal?>("C1_40NOR") : 0,
                                                      C1_40OT = unit1List.Contains("40OT") ? b.Field<decimal?>("C1_40OT") : 0,
                                                      C1_40RF = unit1List.Contains("40RF") ? b.Field<decimal?>("C1_40RF") : 0,
                                                      C1_40RH = unit1List.Contains("40RH") ? b.Field<decimal?>("C1_40RH") : 0,
                                                      C1_40TK = unit1List.Contains("40TK") ? b.Field<decimal?>("C1_40TK") : 0,
                                                      C1_45FR = unit1List.Contains("45FR") ? b.Field<decimal?>("C1_45FR") : 0,
                                                      C1_45GP = unit1List.Contains("45GP") ? b.Field<decimal?>("C1_45GP") : 0,
                                                      C1_45HQ = unit1List.Contains("45HQ") ? b.Field<decimal?>("C1_45HQ") : 0,
                                                      C1_45HT = unit1List.Contains("45HT") ? b.Field<decimal?>("C1_45HT") : 0,
                                                      C1_45OT = unit1List.Contains("45OT") ? b.Field<decimal?>("C1_45OT") : 0,
                                                      C1_45RF = unit1List.Contains("45RF") ? b.Field<decimal?>("C1_45RF") : 0,
                                                      C1_45RH = unit1List.Contains("45RH") ? b.Field<decimal?>("C1_45RH") : 0,
                                                      C1_45TK = unit1List.Contains("45TK") ? b.Field<decimal?>("C1_45TK") : 0,
                                                      C1_53HQ = unit1List.Contains("53HQ") ? b.Field<decimal?>("C1_53HQ") : 0,

                                                      C2_ID=b.Field<Guid?>("C2_ID"),
                                                      C2_POL = b.Field<String>("C2_POLName"),
                                                      C2_VIL = b.Field<String>("C2_VIAName"),
                                                      C2_POD = b.Field<String>("C2_PODName"),
                                                      C2_Delivery = b.Field<String>("C2_PlaceOfDeliveryName"),
                                                      C2_Term = b.Field<String>("C2_TransportClauseName"),
                                                      C2_ItemCode = b.Field<String>("C2_ItemCode"),
                                                      C2_20FR = unit2List.Contains("20FR") ? b.Field<decimal?>("C2_20FR") : 0,
                                                      C2_20GP = unit2List.Contains("20GP") ? b.Field<decimal?>("C2_20GP") : 0,
                                                      C2_20HQ = unit2List.Contains("20HQ") ? b.Field<decimal?>("C2_20HQ") : 0,
                                                      C2_20HT = unit2List.Contains("20HT") ? b.Field<decimal?>("C2_20HT") : 0,
                                                      C2_20NOR = unit2List.Contains("20NOR") ? b.Field<decimal?>("C2_20NOR") : 0,
                                                      C2_20OT = unit2List.Contains("20OT") ? b.Field<decimal?>("C2_20OT") : 0,
                                                      C2_20RF = unit2List.Contains("20RF") ? b.Field<decimal?>("C2_20RF") : 0,
                                                      C2_20RH = unit2List.Contains("20RH") ? b.Field<decimal?>("C2_20RH") : 0,
                                                      C2_20TK = unit2List.Contains("20TK") ? b.Field<decimal?>("C2_20TK") : 0,
                                                      C2_40FR = unit2List.Contains("40FR") ? b.Field<decimal?>("C2_40FR") : 0,
                                                      C2_40GP = unit2List.Contains("40GP") ? b.Field<decimal?>("C2_40GP") : 0,
                                                      C2_40HQ = unit2List.Contains("40HQ") ? b.Field<decimal?>("C2_40HQ") : 0,
                                                      C2_40HT = unit2List.Contains("40HT") ? b.Field<decimal?>("C2_40HT") : 0,
                                                      C2_40NOR = unit2List.Contains("40NOR") ? b.Field<decimal?>("C2_40NOR") : 0,
                                                      C2_40OT = unit2List.Contains("40OT") ? b.Field<decimal?>("C2_40OT") : 0,
                                                      C2_40RF = unit2List.Contains("40RF") ? b.Field<decimal?>("C2_40RF") : 0,
                                                      C2_40RH = unit2List.Contains("40RH") ? b.Field<decimal?>("C2_40RH") : 0,
                                                      C2_40TK = unit2List.Contains("40TK") ? b.Field<decimal?>("C2_40TK") : 0,
                                                      C2_45FR = unit2List.Contains("45FR") ? b.Field<decimal?>("C2_45FR") : 0,
                                                      C2_45GP = unit2List.Contains("45GP") ? b.Field<decimal?>("C2_45GP") : 0,
                                                      C2_45HQ = unit2List.Contains("45HQ") ? b.Field<decimal?>("C2_45HQ") : 0,
                                                      C2_45HT = unit2List.Contains("45HT") ? b.Field<decimal?>("C2_45HT") : 0,
                                                      C2_45OT = unit2List.Contains("45OT") ? b.Field<decimal?>("C2_45OT") : 0,
                                                      C2_45RF = unit2List.Contains("45RF") ? b.Field<decimal?>("C2_45RF") : 0,
                                                      C2_45RH = unit2List.Contains("45RH") ? b.Field<decimal?>("C2_45RH") : 0,
                                                      C2_45TK = unit2List.Contains("45TK") ? b.Field<decimal?>("C2_45TK") : 0,
                                                      C2_53HQ = unit2List.Contains("53HQ") ? b.Field<decimal?>("C2_53HQ") : 0,
                                                      
                                                  }).ToList();


                OceanContractComparDataList data = new OceanContractComparDataList();
                data.DataList = list;
                data.UnitList = unitList;

                return data;

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

        public HierarchyManyResult BackUpOceanHistotry(Guid ID, Guid SaveByID, bool IsEnglish)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspBackUpOceanHistotry");

            #region 传递参数

            db.AddInParameter(dbCommand, "@id", DbType.Guid, ID);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.String, SaveByID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.DateTime, IsEnglish);

            #endregion
            ManyResult[] result = db.ManyResults(dbCommand, new string[][] { new string[] { "State" ,"OperationID" }, 
                    new string[] { "State" ,"OperationID"  } });

            if (result == null
                || result.Length < 2
                || result[0].Items.Count == 0)
            {
                return null;
            }

            HierarchyManyResult results = new HierarchyManyResult(result[0].Items[0].Copy());
            if (result[1] != null)
            {
                foreach (SingleResult s in result[1].Items)
                {
                    results.Childs.Add(new HierarchyManyResult(s));
                }
            }

            return results;
        }
        #endregion

        #region 获得一条运价的费用列表
        /// <summary>
        /// 获得一条运价的费用列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OceanRateFeeDetail GetOceanRateFeeDetail(Guid id)
        {
            OceanRateFeeDetail returnItem = new OceanRateFeeDetail();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspGetOceanRateDetails");

                db.AddInParameter(dbCommand, "@OceanBaseItemID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count <= 1)
                {
                    returnItem.UnitList = new List<string>();
                    returnItem.BasePortInfo = new ClientBasePortList();
                    returnItem.Arbitrarys = new List<ClientArbitraryList>();
                    returnItem.AdditionalFees = new List<ClientAdditionalFeeList>();
                    return returnItem;
                }

                #region linq

                #region 箱型

                returnItem.UnitList = (from b in ds.Tables[0].AsEnumerable()
                                         select b.Field<string>("UnitName")).ToList();
                    
 
                List<string> BasePortUnitList = new List<string>();
                foreach (DataColumn dc in ds.Tables[1].Columns)
                {
                    BasePortUnitList.Add(dc.ColumnName);
                }

                List<string> ArbitraryUnitList = new List<string>();
                foreach (DataColumn dc in ds.Tables[2].Columns)
                {
                    ArbitraryUnitList.Add(dc.ColumnName);
                }

                List<string> AdditionalFeeUnitList = new List<string>();
                foreach (DataColumn dc in ds.Tables[3].Columns)
                {
                    AdditionalFeeUnitList.Add(dc.ColumnName);
                }

                #endregion

                #region BasePort
                returnItem.BasePortInfo = (from b in ds.Tables[1].AsEnumerable()
                                           select new ClientBasePortList
                                           {
                                               CarrierName = b.Field<String>("Carrier"),
                                               POLName = b.Field<String>("POL"),
                                               PODName = b.Field<String>("POD"),
                                               PlaceOfDeliveryName = b.Field<String>("Delivery"),
                                               CurrencyName=b.Field<string>("Currency"),
                                               TransportClauseName=b.Field<string>("Term"),
                                               ItemCode=b.Field<string>("ItemCode"),

                                               Rate_20FR = BasePortUnitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                               Rate_20GP = BasePortUnitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                               Rate_20HQ = BasePortUnitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                               Rate_20HT = BasePortUnitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                               Rate_20NOR = BasePortUnitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                               Rate_20OT = BasePortUnitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                               Rate_20RF = BasePortUnitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                               Rate_20RH = BasePortUnitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                               Rate_20TK = BasePortUnitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                               Rate_40FR = BasePortUnitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                               Rate_40GP = BasePortUnitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                               Rate_40HQ = BasePortUnitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                               Rate_40HT = BasePortUnitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                               Rate_40NOR = BasePortUnitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                               Rate_40OT = BasePortUnitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                               Rate_40RF = BasePortUnitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                               Rate_40RH = BasePortUnitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                               Rate_40TK = BasePortUnitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                               Rate_45FR = BasePortUnitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                               Rate_45GP = BasePortUnitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                               Rate_45HQ = BasePortUnitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                               Rate_45HT = BasePortUnitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                               Rate_45OT = BasePortUnitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                               Rate_45RF = BasePortUnitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                               Rate_45RH = BasePortUnitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                               Rate_45TK = BasePortUnitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                               Rate_53HQ = BasePortUnitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,
                                               }).SingleOrDefault();
                      
                #endregion

                #region ArbitraryList
                returnItem.Arbitrarys = (from b in ds.Tables[2].AsEnumerable()
                                         select new ClientArbitraryList
                                         {
                                             POLName=b.Field<String>("POL"),
                                             PODName=b.Field<String>("POD"),
                                             TransportClauseName=b.Field<String>("Term"),

                                             Rate_20FR = ArbitraryUnitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                             Rate_20GP = ArbitraryUnitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                             Rate_20HQ = ArbitraryUnitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                             Rate_20HT = ArbitraryUnitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                             Rate_20NOR = ArbitraryUnitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                             Rate_20OT = ArbitraryUnitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                             Rate_20RF = ArbitraryUnitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                             Rate_20RH = ArbitraryUnitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                             Rate_20TK = ArbitraryUnitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                             Rate_40FR = ArbitraryUnitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                             Rate_40GP = ArbitraryUnitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                             Rate_40HQ = ArbitraryUnitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                             Rate_40HT = ArbitraryUnitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                             Rate_40NOR = ArbitraryUnitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                             Rate_40OT = ArbitraryUnitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                             Rate_40RF = ArbitraryUnitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                             Rate_40RH = ArbitraryUnitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                             Rate_40TK = ArbitraryUnitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                             Rate_45FR = ArbitraryUnitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                             Rate_45GP = ArbitraryUnitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                             Rate_45HQ = ArbitraryUnitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                             Rate_45HT = ArbitraryUnitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                             Rate_45OT = ArbitraryUnitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                             Rate_45RF = ArbitraryUnitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                             Rate_45RH = ArbitraryUnitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                             Rate_45TK = ArbitraryUnitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                             Rate_53HQ = ArbitraryUnitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,
                                         }).ToList();
                #endregion

                #region ArbitraryList
                returnItem.AdditionalFees = (from b in ds.Tables[3].AsEnumerable()
                                             select new ClientAdditionalFeeList
                                         {
                                             ChargingCodeDescription=b.Field<String>("FeeName"),
                                             CustomerName=b.Field<String>("Customer"),
                                             CurrencyName=b.Field<String>("Currency"),
                                             Remark=b.Field<String>("Remark"),

                                             Rate_20FR = AdditionalFeeUnitList.Contains("20FR") ? b.Field<Decimal?>("20FR") : 0,
                                             Rate_20GP = AdditionalFeeUnitList.Contains("20GP") ? b.Field<Decimal?>("20GP") : 0,
                                             Rate_20HQ = AdditionalFeeUnitList.Contains("20HQ") ? b.Field<Decimal?>("20HQ") : 0,
                                             Rate_20HT = AdditionalFeeUnitList.Contains("20HT") ? b.Field<Decimal?>("20HT") : 0,
                                             Rate_20NOR = AdditionalFeeUnitList.Contains("20NOR") ? b.Field<Decimal?>("20NOR") : 0,
                                             Rate_20OT = AdditionalFeeUnitList.Contains("20OT") ? b.Field<Decimal?>("20OT") : 0,
                                             Rate_20RF = AdditionalFeeUnitList.Contains("20RF") ? b.Field<Decimal?>("20RF") : 0,
                                             Rate_20RH = AdditionalFeeUnitList.Contains("20RH") ? b.Field<Decimal?>("20RH") : 0,
                                             Rate_20TK = AdditionalFeeUnitList.Contains("20TK") ? b.Field<Decimal?>("20TK") : 0,
                                             Rate_40FR = AdditionalFeeUnitList.Contains("40FR") ? b.Field<Decimal?>("40FR") : 0,
                                             Rate_40GP = AdditionalFeeUnitList.Contains("40GP") ? b.Field<Decimal?>("40GP") : 0,
                                             Rate_40HQ = AdditionalFeeUnitList.Contains("40HQ") ? b.Field<Decimal?>("40HQ") : 0,
                                             Rate_40HT = AdditionalFeeUnitList.Contains("40HT") ? b.Field<Decimal?>("40HT") : 0,
                                             Rate_40NOR = AdditionalFeeUnitList.Contains("40NOR") ? b.Field<Decimal?>("40NOR") : 0,
                                             Rate_40OT = AdditionalFeeUnitList.Contains("40OT") ? b.Field<Decimal?>("40OT") : 0,
                                             Rate_40RF = AdditionalFeeUnitList.Contains("40RF") ? b.Field<Decimal?>("40RF") : 0,
                                             Rate_40RH = AdditionalFeeUnitList.Contains("40RH") ? b.Field<Decimal?>("40RH") : 0,
                                             Rate_40TK = AdditionalFeeUnitList.Contains("40TK") ? b.Field<Decimal?>("40TK") : 0,
                                             Rate_45FR = AdditionalFeeUnitList.Contains("45FR") ? b.Field<Decimal?>("45FR") : 0,
                                             Rate_45GP = AdditionalFeeUnitList.Contains("45GP") ? b.Field<Decimal?>("45GP") : 0,
                                             Rate_45HQ = AdditionalFeeUnitList.Contains("45HQ") ? b.Field<Decimal?>("45HQ") : 0,
                                             Rate_45HT = AdditionalFeeUnitList.Contains("45HT") ? b.Field<Decimal?>("45HT") : 0,
                                             Rate_45OT = AdditionalFeeUnitList.Contains("45OT") ? b.Field<Decimal?>("45OT") : 0,
                                             Rate_45RF = AdditionalFeeUnitList.Contains("45RF") ? b.Field<Decimal?>("45RF") : 0,
                                             Rate_45RH = AdditionalFeeUnitList.Contains("45RH") ? b.Field<Decimal?>("45RH") : 0,
                                             Rate_45TK = AdditionalFeeUnitList.Contains("45TK") ? b.Field<Decimal?>("45TK") : 0,
                                             Rate_53HQ = AdditionalFeeUnitList.Contains("53HQ") ? b.Field<Decimal?>("53HQ") : 0,
                                         }).ToList();
                #endregion

                #endregion
                return returnItem;

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

        #region 生成运价

        /// <summary>
        /// 生成运价
        /// </summary>
        /// <param name="oceanID"></param>
        public void BuilerBaseItemForOceanID(Guid oceanID)
        { 
           try
           {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("frm.uspBuilerBaseItemForOceanID");
                dbCommand.CommandTimeout = (int)tsTimeOut.TotalSeconds;
                db.AddInParameter(dbCommand, "@OceanID", DbType.Guid, oceanID);

                dbCommand.CommandTimeout=1800;

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
