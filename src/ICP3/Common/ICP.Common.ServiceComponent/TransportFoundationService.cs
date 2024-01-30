//-----------------------------------------------------------------------
// <copyright file="TransportFoundationService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 基础数据服务管理
    /// </summary>
    public class TransportFoundationService : ITransportFoundationService
    {
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;

        public TransportFoundationService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

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

        #region Container

        /// <summary>
        /// 获取箱信息列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回箱信息列表</returns>
        public List<ContainerList> GetContainerList(
            string code,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetContainerList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ContainerList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new ContainerList
                                               {
                                                   Code = b.Field<string>("Code"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
                                                   TEU = b.Field<decimal>("TEU"),
                                                   ID = b.Field<Guid>("ID"),
                                                   ISOCode = b.Field<string>("ISOCode"),
                                                   IsValid = b.Field<bool>("IsValid"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               }).OrderBy(o => o.Code).ToList();

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
        /// 获取箱详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回箱详细信息</returns>
        public ContainerInfo GetContainerInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetContainerInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ContainerInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new ContainerInfo
                                        {
                                            Code = b.Field<string>("Code"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<string>("CreateByName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            Description = b.Field<string>("Description"),
                                            ID = b.Field<Guid>("ID"),
                                            ISOCode = b.Field<string>("ISOCode"),
                                            IsValid = b.Field<bool>("IsValid"),
                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                            TEU = b.Field<decimal>("TEU"),
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
        /// 保存箱信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="code">代码</param>
        /// <param name="isoCode">标准代码</param>
        /// <param name="description">描述</param>
        /// <param name="teu">系集装箱运量统计单位</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveContainerInfo(
            Guid? id,
            string code,
            string isoCode,
            string description,
            decimal teu,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(isoCode, "isoCode");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveContainerInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@IsoCode", DbType.String, isoCode);
                db.AddInParameter(dbCommand, "@Description", DbType.String, description);
                db.AddInParameter(dbCommand, "@Teu", DbType.Decimal, teu);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveById);
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
        /// 改变箱信有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">修改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeContainerState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeContainerState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeById);
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
        #endregion

        #region TransportClause

        /// <summary>
        /// 获取运输条款列表
        /// </summary>
        /// <param name="originalCode">出发地代码</param>
        /// <param name="destinationCode">目的地代码</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回运输条款列表</returns>
        public List<TransportClauseList> GetTransportClauseList(
            string originalCode,
            string destinationCode,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetTransportClauseList");

                db.AddInParameter(dbCommand, "@OriginalCode", DbType.String, originalCode);
                db.AddInParameter(dbCommand, "@DestinationCode", DbType.String, destinationCode);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<TransportClauseList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new TransportClauseList
                                                     {
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         DestinationCode = b.Field<string>("DestinationCode"),
                                                         Code = b.Field<string>("Code"),
                                                         ID = b.Field<Guid>("ID"),
                                                         IsValid = b.Field<bool>("IsValid"),
                                                         OriginalCode = b.Field<string>("OriginalCode"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 获取运输条款明细信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回运输条款明细信息</returns>
        public TransportClauseInfo GetTransportClauseInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetTransportClauseInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                TransportClauseInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new TransportClauseInfo
                                              {
                                                  CreateByID = b.Field<Guid>("CreateByID"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  Description = b.Field<string>("Description"),
                                                  DestinationCode = b.Field<string>("DestinationCode"),
                                                  DestinationCodeID = b.Field<Guid>("DestinationCodeID"),
                                                  ID = b.Field<Guid>("ID"),
                                                  IsValid = b.Field<bool>("IsValid"),
                                                  OriginalCode = b.Field<string>("OriginalCode"),
                                                  OriginalCodeID = b.Field<Guid>("OriginalCodeID"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 保存运输条款信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="originalCodeID">出发地代码</param>
        /// <param name="destinationCodeID">目的地代码</param>
        /// <param name="description">描述</param>
        /// <param name="saveById">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveTransportClauseInfo(
            Guid? id,
            Guid originalCodeID,
            Guid destinationCodeID,
            string description,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(originalCodeID, "originalCodeID");
            ArgumentHelper.AssertGuidNotEmpty(destinationCodeID, "destinationCodeID");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveTransportClauseInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@OriginalCodeID", DbType.Guid, originalCodeID);
                db.AddInParameter(dbCommand, "@DestinationCodeID", DbType.Guid, destinationCodeID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, description);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveById);
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
        /// 改变运输条款状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeTransportClauseState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeTransportClauseState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeById);
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
        #endregion

        #region Commodity

        /// <summary>
        /// 获取品名列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回品名列表</returns>
        public List<CommodityList> GetCommodityList(
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCommodityList");

                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CommodityList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new CommodityList
                                               {
                                                   CName = b.Field<string>("CName"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
                                                   EName = b.Field<string>("EName"),
                                                   ID = b.Field<Guid>("ID"),
                                                   IsValid = b.Field<bool>("IsValid"),
                                                   ParentID = b.IsNull("ParentID") ? (Guid?)null : b.Field<Guid>("ParentID"),
                                                   ParentName = b.Field<string>("ParentName"),
                                                   HierarchyCode = b.Field<string>("HierarchyCode"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 获取品名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回品名信息</returns>
        public CommodityInfo GetCommodityInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCommodityInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CommodityInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new CommodityInfo
                                        {
                                            CName = b.Field<string>("CName"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<string>("CreateByName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            EName = b.Field<string>("EName"),
                                            ID = b.Field<Guid>("ID"),
                                            IsValid = b.Field<bool>("IsValid"),
                                            ParentID = b.IsNull("ParentID") ? (Guid?)null : b.Field<Guid>("ParentID"),
                                            HierarchyCode = b.Field<string>("HierarchyCode"),
                                            ParentName = b.Field<string>("ParentName"),
                                            Remark = b.Field<string>("Remark"),
                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 保存品名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="remark">备注</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SaveCommodityInfo(
            Guid? id,
            Guid? parentID,
            string cName,
            string eName,
            string remark,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(cName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCommodityInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(dbCommand, Guid.Empty);
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
        /// 设置品名父节点

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="setById">设置人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SetCommodityParent(
            Guid id,
            Guid? parentID,
            Guid setById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSetParentCommodity");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@SetById", DbType.Guid, setById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(dbCommand, Guid.Empty);
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
        /// 更改品名有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData ChangeCommodityState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeCommodityState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(dbCommand, id);
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

        #region ShippingLine

        /// <summary>
        /// 获取航线列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回航线列表</returns>
        public List<ShippingLineList> GetShippingLineList(
            string code,
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetShippingLineList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ShippingLineList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new ShippingLineList
                                                  {
                                                      ParentID = b.Field<Guid?>("ParentID"),
                                                      CName = b.Field<string>("CName"),
                                                      Code = b.Field<string>("Code"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      EName = b.Field<string>("EName"),
                                                      ID = b.Field<Guid>("ID"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 获取航线列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isParent">是否总航线</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回航线列表</returns>
        public List<ShippingLineList> GetShippingLineList(
            string code,
            string name,
            bool? isValid,
            bool? isParent,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetShippingLineListEx");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsParent", DbType.Boolean, isParent);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ShippingLineList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new ShippingLineList
                                                  {
                                                      ParentID = b.Field<Guid?>("ParentID"),
                                                      CName = b.Field<string>("CName"),
                                                      Code = b.Field<string>("Code"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      EName = b.Field<string>("EName"),
                                                      ID = b.Field<Guid>("ID"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取航线信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回航线信息</returns>
        public ShippingLineInfo GetShippingLineInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetShippingLineInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ShippingLineInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new ShippingLineInfo
                                           {
                                               CName = b.Field<string>("CName"),
                                               Code = b.Field<string>("Code"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<string>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               EName = b.Field<string>("EName"),
                                               ID = b.Field<Guid>("ID"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 保存航线信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveShippingLineInfo(
            Guid? id,
            Guid ParentID,
            string code,
            string cName,
            string eName,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveShippingLineInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, ParentID);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveById);
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
        /// 改变航线有效状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeShippingLineState(
            Guid id,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeShippingLineState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeById);
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
        /// 保存航线国家关联关系
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ShippingLineID"></param>
        /// <param name="CountryPortIDs"></param>
        /// <param name="Types"></param>
        /// <param name="updateDates"></param>
        /// <param name="saveByID"></param>
        /// <param name="ReturnResult"></param>
        /// <returns></returns>
        public ManyResultData SaveShiLineReationCountry
            (
            Guid?[] ids,
            Guid ShippingLineID,
            Guid[] CountryPortIDs,
            DateTime?[] updateDates,
            Guid saveByID,
            bool ReturnResult)
        {

            ArgumentHelper.AssertGuidNotEmpty(ShippingLineID, "ShippingLineID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveShiLineReationCountry");

                db.AddInParameter(dbCommand, "@ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, ShippingLineID);
                db.AddInParameter(dbCommand, "@CountryIDs", DbType.String, CountryPortIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IsReturnResult", DbType.Boolean, ReturnResult);

                ManyResultData result = db.ManyResult(dbCommand);
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


        public ManyResultData SavePortReationShippingLine
           (
           Guid?[] ids,
           Guid ShippingLineID,
           Guid[] PortIDs,
           DateTime?[] updateDates,
           Guid saveByID,
           bool ReturnResult)
        {

            ArgumentHelper.AssertGuidNotEmpty(ShippingLineID, "ShippingLineID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSavePortReationShiLine");

                db.AddInParameter(dbCommand, "@ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, ShippingLineID);
                db.AddInParameter(dbCommand, "@PortIDs", DbType.String, PortIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IsReturnResult", DbType.Boolean, ReturnResult);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 获取航线下国家港口列表
        /// </summary>
        /// <param name="shippingLineID">航线ID</param>
        /// <param name="IsEnglish">是否英文</param>
        /// <returns></returns>
        public CountryPortList GetGetShiLineReationCountryList(
            Guid shippingLineID,
            bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetShiLineReationCountryList");

                db.AddInParameter(dbCommand, "@ShippingLineID", DbType.Guid, shippingLineID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CountryPortList countryPort = new CountryPortList();

                countryPort.Country = (from b in ds.Tables[0].AsEnumerable()
                                       select new ShippingCountryInfo
                                                 {
                                                     CountryID = b.Field<Guid>("CountryID"),
                                                     ID = b.Field<Guid?>("ID"),
                                                     ShippingLineID = b.Field<Guid>("ShippingLineID"),
                                                     CountryName = b.Field<string>("CountryName"),
                                                     UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                 }).ToList();

                countryPort.Port = (from b in ds.Tables[1].AsEnumerable()
                                    select new ShippingPortInfo
                                    {
                                        PortID = b.Field<Guid>("PortID"),
                                        ID = b.Field<Guid?>("ID"),
                                        ShippingLineID = b.Field<Guid>("ShippingLineID"),
                                        PortName = b.Field<string>("PortName"),
                                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                    }).ToList();

                return countryPort;
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


        public void RemovePortReationShipping(
           Guid[] Id,  //是memo.id
           Guid removeByID,
           DateTime?[] updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(Id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemovePortReationShipping");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, Id.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDate.Join());
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


        public void RemoveCountryReationShipping(
          Guid[] Id,  //是memo.id
          Guid removeByID,
          DateTime?[] updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(Id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveCountryReationShipping");

                db.AddInParameter(dbCommand, "@IDs", DbType.String, Id.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDate.Join());
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


        #endregion

        #region  DataDictionary
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="type">字典类型</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回字典列表</returns>
        public List<DataDictionaryList> GetDataDictionaryList(
            string code,
            string name,
            DataDictionaryType? type,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetDataDictionaryList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, type);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DataDictionaryList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DataDictionaryList
                                                    {
                                                        CName = b.Field<string>("CName"),
                                                        Code = b.Field<string>("Code"),
                                                        CreateByName = b.Field<string>("CreateByName"),
                                                        Type = (DataDictionaryType)b.Field<byte>("Type"),
                                                        CreateDate = b.Field<DateTime>("CreateDate"),
                                                        EName = b.Field<string>("EName"),
                                                        ID = b.Field<Guid>("ID"),
                                                        IsValid = b.Field<bool>("IsValid"),
                                                        UpdateDate = b.Field<DateTime?>("UpdateDate")
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
        /// 获取字典信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回字典信息</returns>
        public DataDictionaryInfo GetDataDictionaryInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetDataDictionaryInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                DataDictionaryInfo result = (from b in ds.Tables[0].AsEnumerable()
                                             select new DataDictionaryInfo
                                             {
                                                 CName = b.Field<string>("CName"),
                                                 Code = b.Field<string>("Code"),
                                                 CreateByID = b.Field<Guid>("CreateByID"),
                                                 CreateByName = b.Field<string>("CreateByName"),
                                                 CreateDate = b.Field<DateTime>("CreateDate"),
                                                 EName = b.Field<string>("EName"),
                                                 Description = b.Field<string>("Description"),
                                                 ID = b.Field<Guid>("ID"),
                                                 IsValid = b.Field<bool>("IsValid"),
                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                 Type = (DataDictionaryType)b.Field<byte>("Type"),
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
        /// 保存字典信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="description">描述</param>
        /// <param name="type">字典类型</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveDataDictionaryInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
             string description,
            DataDictionaryType type,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveDataDictionaryInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, description);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, type);
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
        /// 改变字典状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeDataDictionaryState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeDataDictionaryState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
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
        /// 获取计价单位
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回计价单位列表</returns>
        public List<DataDictionaryList> GetValuationUnitList(
            string code,
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetValuationUnitList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<DataDictionaryList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DataDictionaryList
                                                    {
                                                        CName = b.Field<string>("CName"),
                                                        Code = b.Field<string>("Code"),
                                                        Type = DataDictionaryType.ValuationUnit,
                                                        EName = b.Field<string>("EName"),
                                                        ID = b.Field<Guid>("ID"),
                                                        IsValid = b.Field<bool>("IsValid"),
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

        #region Flight
        /// <summary>
        /// 获取航班列表
        /// </summary>
        /// <param name="airlineID">航空公司ID</param>
        /// <param name="no">航班号</param>
        /// <param name="polName">始发港ID</param>
        /// <param name="podName">到达港ID</param>
        /// <param name="etdFrom">估计离港日-开始</param>
        /// <param name="etdTo">估计离港日-结束</param>
        /// <param name="etaFrom">估计到港日-开始</param>
        /// <param name="etaTo">估计到港日-结束</param>
        /// <param name="closingDateFrom">截关日-开始</param>
        /// <param name="closingDateTo">截关日-结束</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回航班列表</returns>
        public List<FlightList> GetFlightList(
            Guid? airlineID,
            string no,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetFlightList");

                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, airlineID);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<FlightList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new FlightList
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                No = b.Field<string>("No"),
                                                AirlineName = b.Field<string>("AirlineName"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 获取航班信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回航班信息</returns>
        public FlightInfo GetFilghtInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetFilghtInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                FlightInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new FlightInfo
                                     {
                                         ID = b.Field<Guid>("ID"),
                                         No = b.Field<string>("No"),
                                         AirlineID = b.Field<Guid>("AirlineID"),
                                         AirlineName = b.Field<string>("AirlineName"),
                                         CreateByName = b.Field<string>("CreateByName"),
                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                         CreateByID = b.Field<Guid>("CreateByID"),
                                         IsValid = b.Field<bool>("IsValid"),
                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 保存航班信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="airlineID">航空公司ID</param>
        /// <param name="no">航班号</param>
        /// <param name="polID">始发港ID</param>
        /// <param name="etdDate">估计离港日</param>
        /// <param name="podID">到达港ID</param>
        /// <param name="etaDate">估计到港日</param>
        /// <param name="closingDate">截关日</param>
        /// <param name="docClosingDate">截文件日</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveFlightInfo(
            Guid? id,
            Guid airlineID,
            string no,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(airlineID, "airlineID");
            ArgumentHelper.AssertStringNotEmpty(no, "no");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveFlightInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@AirlineID", DbType.Guid, airlineID);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
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
        /// 改变航班有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeFlightState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeFlightState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
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
        /// 获取被合并的航班列表
        /// </summary>
        /// <param name="mainID">主航班ID</param>
        /// <returns>返回被合并的航班列表</returns>
        public List<FlightList> GetMergedFlightList(Guid mainID)
        {
            ArgumentHelper.AssertGuidNotEmpty(mainID, "mianID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetMergedFlightList");

                db.AddInParameter(dbCommand, "@MainID", DbType.Guid, mainID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<FlightList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new FlightList
                                            {
                                                AirlineName = b.Field<string>("AirlineName"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                ID = b.Field<Guid>("ID"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                No = b.Field<string>("No"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 合并航班
        /// </summary>
        /// <param name="ids">被合并的航班列表</param>
        /// <param name="preservedID">保留航班ID</param>
        /// <param name="mergeByID">合并人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData MergeFlight(
            Guid[] ids,
            Guid preservedID,
            Guid mergeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(preservedID, "preservedID");
            ArgumentHelper.AssertGuidNotEmpty(mergeByID, "mergeByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspMergeFlight");

                string tempIds = ids.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@PreservedID", DbType.Guid, preservedID);
                db.AddInParameter(dbCommand, "@MergeByID", DbType.Guid, mergeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
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
        /// 取消合并的航班
        /// </summary>
        /// <param name="ids">取消的航班列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CancelMergedFlight(
           Guid[] ids,
           Guid cancelByID,
           DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(cancelByID, "cancelByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelMergeFlight");

                string tempIds = ids.Join();
                string tempDataVersion = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CancelByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersion);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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

        #region Vessel
        /// <summary>
        /// 获取船名列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回船名列表</returns>
        public List<VesselList> GetVesselList(
            string code,
            string name,
            string carrierName,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetVesselList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@CarrierName", DbType.String, carrierName);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<VesselList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new VesselList
                                            {
                                                CarrierCode = b.Field<string>("CarrierCode"),
                                                CarrierName = b.Field<string>("CarrierName"),
                                                Code = b.Field<string>("Code"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                ID = b.Field<Guid>("ID"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                Name = b.Field<string>("Name"),
                                                Registration = b.Field<Guid?>("CountryID"),
                                                RegistrationName = b.Field<string>("CountryName"),
                                                IMO = b.Field<string>("IMONumber"),
                                                UNCode = b.Field<string>("UNCode")
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
        /// 获取船名列表
        /// </summary>
        /// <param name="carrierIDs">船东</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回船名列表</returns>
        public List<VesselList> GetRecentVesselList(DateTime beginDate,
            DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetRecentVesselList");

                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.String, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<VesselList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new VesselList
                                            {
                                                CarrierCode = b.Field<string>("CarrierCode"),
                                                CarrierName = b.Field<string>("CarrierName"),
                                                Code = b.Field<string>("Code"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                ID = b.Field<Guid>("ID"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                Name = b.Field<string>("Name"),
                                                Registration = b.Field<Guid?>("CountryID"),
                                                RegistrationName = b.Field<string>("CountryName"),
                                                IMO = b.Field<string>("IMONumber"),
                                                UNCode = b.Field<string>("UNCode")
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
        /// 获取船名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回船名信息</returns>
        public VesselInfo GetVesselInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetVesselInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                VesselInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new VesselInfo
                                     {
                                         CarrierID = b.Field<Guid>("CarrierID"),
                                         CarrierName = b.Field<string>("CarrierName"),
                                         Code = b.Field<string>("Code"),
                                         CreateByID = b.Field<Guid>("CreateByID"),
                                         CreateByName = b.Field<string>("CreateByName"),
                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                         ID = b.Field<Guid>("ID"),
                                         IsValid = b.Field<bool>("IsValid"),
                                         Name = b.Field<string>("Name"),
                                         Registration = b.Field<Guid?>("CountryID"),
                                         RegistrationName = b.Field<string>("CountryName"),
                                         IMO = b.Field<string>("IMONumber"),
                                         UNCode = b.Field<string>("UNCode")
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
        /// 保存船名信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="carrierID">船东ID</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveVesselInfo(
            Guid? id,
            string code,
            string name,
            Guid carrierID,
            Guid saveByID,
            DateTime? updateDate,
            string IMO,
            string UNCode,
            Guid? registration)
        {
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(name, "name");
            ArgumentHelper.AssertGuidNotEmpty(carrierID, "carrierID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveVesselInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IMO", DbType.String, IMO);
                db.AddInParameter(dbCommand, "@UNCode", DbType.String, UNCode);
                db.AddInParameter(dbCommand, "@Registration", DbType.Guid, registration);

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
        /// 改变船名状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeVesselState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeVesselState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
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
        /// 合并船名
        /// </summary>
        /// <param name="ids">被合并的船名列表</param>
        /// <param name="preservedID">保留航班ID</param>
        /// <param name="mergeByID">合并人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData MergeVessel(
            Guid[] ids,
            Guid preservedID,
            Guid mergeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(preservedID, "preservedID");
            ArgumentHelper.AssertGuidNotEmpty(mergeByID, "mergeByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspMergeVessel");

                string tempIds = ids.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@PreservedID", DbType.Guid, preservedID);
                db.AddInParameter(dbCommand, "@MergeByID", DbType.Guid, mergeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
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
        /// 获取被合并的船名列表
        /// </summary>
        /// <param name="mainID">主船名ID</param>
        /// <returns>返回被合并的船名列表</returns>
        public List<VesselList> GetMergedVesselList(Guid mainID)
        {
            ArgumentHelper.AssertGuidNotEmpty(mainID, "mainID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetMergedVesselList");

                db.AddInParameter(dbCommand, "@MainID", DbType.Guid, mainID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<VesselList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new VesselList
                                            {
                                                CarrierName = b.Field<string>("CarrierName"),
                                                Code = b.Field<string>("Code"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                ID = b.Field<Guid>("ID"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                Name = b.Field<string>("Name"),
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
        /// 取消合并的船名
        /// </summary>
        /// <param name="ids">取消的船名列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CancelMergedVessel(
           Guid[] ids,
           Guid cancelByID,
           DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(cancelByID, "cancelByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelMergedVessel");

                string tempIds = ids.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CancelByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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

        #region Voyage

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回船名航次列表</returns>
        public List<VoyageList> GetRecentVoyageList(DateTime beginDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetRecentVoyageList");

                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.String, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<VoyageList> result = (from b in ds.Tables[0].AsEnumerable()
                                           select new VoyageList
                                           {
                                               CreateByName = b.Field<string>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               ID = b.Field<Guid>("ID"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               No = b.Field<string>("No"),
                                               UNCode = b.Field<string>("UNCode"),
                                               VesselName = b.Field<string>("VesselName"),
                                               VesselAndNo = string.Format("{0}/{1}", b.Field<string>("VesselName"), b.Field<string>("No"))
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
        /// 获取船名航次列表
        /// </summary>
        /// <param name="vesselName">船名</param>
        /// <param name="no">航次号</param>
        /// <param name="polID">装货港ID</param>
        /// <param name="transhipmentPortID">中转港ID</param>
        /// <param name="podID">卸货港ID</param>
        /// <param name="etdFrom">估计离港日-开始</param>
        /// <param name="etdTo">估计离港日-结束</param>
        /// <param name="etaFrom">估计到港日-开始</param>
        /// <param name="etaTo">估计到港日-结束</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回船名航次列表</returns>
        public List<VoyageList> GetVoyageList(
            Guid? vesselId,
            string vesselName,
            string no,
            DateTime? createDateFrom,
            DateTime? createDateTo,
            Guid? carrierID,
            bool? isValid,
            int maxRecords)
        {
            return this.GetVoyages(
                vesselId,
                null,
                vesselName,
                no,
                createDateFrom,
                createDateTo,
                carrierID,
                isValid,
                maxRecords);
        }

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="vesselName">船名</param>
        /// <param name="no">航次号</param>
        /// <param name="polID">装货港ID</param>
        /// <param name="transhipmentPortID">中转港ID</param>
        /// <param name="podID">卸货港ID</param>
        /// <param name="etdFrom">估计离港日-开始</param>
        /// <param name="etdTo">估计离港日-结束</param>
        /// <param name="etaFrom">估计到港日-开始</param>
        /// <param name="etaTo">估计到港日-结束</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回船名航次列表</returns>
        public List<VoyageList> GetVoyageList(
            Guid? vesselId,
            Guid? companyId,
            string vesselName,
            string no,
            DateTime? createDateFrom,
            DateTime? createDateTo,
            Guid? carrierID,
            bool? isValid,
            int maxRecords)
        {
            return this.GetVoyages(
                vesselId,
                companyId,
                vesselName,
                no,
                createDateFrom,
                createDateTo,
                carrierID,
                isValid,
                maxRecords);
        }

        /// <summary>
        /// 获取船名航次列表
        /// </summary>
        /// <param name="vesselID">船名ID</param>
        /// <returns>返回船名航次列表</returns>
        public List<VoyageList> GetVoyageList(Guid vesselID)
        {
            return this.GetVoyages(
               vesselID,
               null,
               string.Empty,
               string.Empty,
               null, null, null,
               null,
               1000);
        }

        /// <summary>
        /// 获取航次信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回航次信息</returns>
        public VoyageInfo GetVoyageInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetVoyageInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                VoyageInfo result = (from b in ds.Tables[0].AsEnumerable()
                                     select new VoyageInfo
                                     {
                                         CreateByID = b.Field<Guid>("CreateByID"),
                                         CreateByName = b.Field<string>("CreateByName"),
                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                         ID = b.Field<Guid>("ID"),
                                         IsValid = b.Field<bool>("IsValid"),
                                         No = b.Field<string>("No"),
                                         VesselID = b.Field<Guid>("VesselID"),
                                         VesselName = b.Field<string>("VesselName"),
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
        /// 保存航次信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="vesselID">船名ID</param>
        /// <param name="no">航次号</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveVoyageInfo(
            Guid? id,
            Guid vesselID,
            string no,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(vesselID, "vesselID");
            ArgumentHelper.AssertStringNotEmpty(no, "no");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");


            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveVoyageInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@VesselID", DbType.Guid, vesselID);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
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
        /// 改变航次状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeVoyageState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeVoyageState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
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
        /// 合并航次
        /// </summary>
        /// <param name="ids">被合并的航次列表</param>
        /// <param name="preservedID">保留航次ID</param>
        /// <param name="mergeByID">合并人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData MergeVoyage(
            Guid[] ids,
            Guid preservedID,
            Guid mergeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(preservedID, "preservedID");
            ArgumentHelper.AssertGuidNotEmpty(mergeByID, "mergeByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspMergeVoyage");

                string tempIds = ids.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@PreservedID", DbType.Guid, preservedID);
                db.AddInParameter(dbCommand, "@MergeByID", DbType.Guid, mergeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
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
        /// 获取被合并的航次列表
        /// </summary>
        /// <param name="mainID">主航次ID</param>
        /// <returns>返回被合并的航次列表</returns>
        public List<VoyageList> GetMergedVoyageList(Guid mainID)
        {
            ArgumentHelper.AssertGuidNotEmpty(mainID, "mainID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetMergedVoyageList");

                db.AddInParameter(dbCommand, "@MainID", DbType.Guid, mainID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<VoyageList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new VoyageList
                                            {

                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                ID = b.Field<Guid>("ID"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                No = b.Field<string>("No"),

                                                VesselName = b.Field<string>("VesselName"),
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
        /// 取消合并的航次
        /// </summary>
        /// <param name="ids">取消的航次列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CancelMergedVoyage(
           Guid[] ids,
           Guid cancelByID,
           DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(cancelByID, "cancelByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelMergedVoyage");

                string tempIds = ids.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CancelByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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

        private List<VoyageList> GetVoyages(
            Guid? vesselId,
            Guid? companyId,
            string vesselName,
            string no,
            DateTime? createDateFrom,
            DateTime? createDateTo,
            Guid? carrierID,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetVoyageList");

                db.AddInParameter(dbCommand, "@VesselID", DbType.Guid, vesselId);
                db.AddInParameter(dbCommand, "@VesselName", DbType.String, vesselName);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);

                db.AddInParameter(dbCommand, "@CreateDateFrom", DbType.DateTime, createDateFrom);
                db.AddInParameter(dbCommand, "@CreateDateTo", DbType.DateTime, createDateTo);

                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<VoyageList> result = (from b in ds.Tables[0].AsEnumerable()
                                           select new VoyageList
                                           {
                                               CreateByName = b.Field<string>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               ID = b.Field<Guid>("ID"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               No = b.Field<string>("No"),
                                               UNCode = b.Field<string>("UNCode"),
                                               VesselName = b.Field<string>("VesselName"),
                                               VesselAndNo = string.Format("{0}/{1}", b.Field<string>("VesselName"), b.Field<string>("No"))
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

        #endregion

        #region Other
        /// <summary>
        /// 获取业务费用报销项目列表
        /// </summary>
        /// <returns>CostItemData</returns>
        public List<CostItemData> GetAllCostItems()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCostItems");
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<CostItemData> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CostItemData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                NodeCode = b.Field<string>("NodeCode"),
                                                CName = b.Field<string>("CName"),
                                                EName = b.Field<string>("EName"),
                                                FullName = b.Field<string>("FullName"),
                                                EFullName = b.Field<string>("EFullName"),
                                                ParentID = b.Field<Guid?>("ParentID"),
                                            }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region MAC地址管理

        public List<AuthcodeInfo> GetAuthcodeList(
         string code,
         bool? isValid,
         int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetAuthCodeList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AuthcodeInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new AuthcodeInfo
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                AuthCode = b.Field<string>("Authcode"),
                                                PhysicalID = b.Field<string>("PhysicalCode"),
                                                SenderName = b.Field<string>("SenderName"),
                                                SenderDate = b.Field<DateTime>("SenderDate"),
                                                SenderRemark = b.Field<string>("SenderRemark")
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

        public SingleResultData SaveAuthcodeInfo(
        Guid? id,
        string code,
            string physicalID,
        string remark,
        Guid savebyid
            )
        {
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveAuthCodeInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@PhysicalID", DbType.String, physicalID);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, savebyid);
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

        public void RemoveAuthcodeInfo(Guid id,
            Guid removeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "ID");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            Guid[] ids = new Guid[] { id };
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspRemoveAuthCode");

                db.AddInParameter(dbCommand, "@ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@RemoveID", DbType.Guid, removeByID);
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
        /// GetVoyageListETDETA
        /// </summary>
        /// <param name="VesselName"></param>
        /// <param name="no"></param>
        /// <param name="CarrierID"></param>
        /// <param name="POL"></param>
        /// <param name="POD"></param>
        /// <returns></returns>
        public VoyageETDETAList GetVoyageListETDETA(
        string VesselName,
            string no,
            Guid? CarrierID,
            Guid? POL,
            Guid? POD)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetVoyageListETDETA");

                db.AddInParameter(dbCommand, "@VesselName", DbType.String, VesselName);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, CarrierID);
                db.AddInParameter(dbCommand, "@PodID", DbType.Guid, POD);
                db.AddInParameter(dbCommand, "@PolID", DbType.Guid, POL);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                VoyageETDETAList results = null;

                List<VoyageETDETAList> dt1 = (from b in ds.Tables[0].AsEnumerable()
                                              select new VoyageETDETAList
                                          {
                                              ETD = b.Field<DateTime?>("ETD"),
                                          }).ToList();
                if (dt1 != null && dt1.Count > 0)
                {
                    results = new VoyageETDETAList();
                    results.ETD = dt1[0].ETD;
                }
           
                if (ds.Tables.Count > 0)
                {
                    List<VoyageETDETAList> dt2 = (from b in ds.Tables[1].AsEnumerable()
                                                    select new VoyageETDETAList
                                                    {
                                                        ETA = b.Field<DateTime?>("ETA"),
                                                    }).ToList();
                    if (dt2 != null && dt2.Count > 0)
                    {
                        if (results == null)
                            results = new VoyageETDETAList();
                        results.ETA = dt2[0].ETA;
                    }
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
        #endregion

        /// <summary>
        /// 保存船期刷新日志
        /// </summary>
        /// <param name="message"></param>
        public void SaveLogInfo(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\TransportLogs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":  " + message + System.Environment.NewLine;

            StreamWriter sw = new StreamWriter(path, true, Encoding.GetEncoding("GB2312"));
            sw.Write(str);
            sw.Close();
        }
    }
}
