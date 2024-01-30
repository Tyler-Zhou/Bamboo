//-----------------------------------------------------------------------
// <copyright file="ConfigureService.cs" company="LongWin">
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
    using ServiceInterface;
    using ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;
    using Framework.CommonLibrary.Server;
    using Framework.CommonLibrary.Client;
    using ICP.Common.ServiceInterface.CompositeObjects;


    /// <summary>
    /// 配置管理服务
    /// </summary>
    public sealed class ConfigureService : IConfigureService
    {
        sealed class ConfigKey : IEquatable<ConfigKey>
        {
            Guid? CompanyID { get; set; }
            Guid? SolutionID { get; set; }
            bool? IsVaid { get; set; }
            int MaxRecords { get; set; }
            public ConfigKey(Guid? companyID, Guid? solutionID, bool? isValid, int maxRecords)
            {
                CompanyID = companyID;
                SolutionID = solutionID;
                IsVaid = isValid;
                MaxRecords = maxRecords;
            }
            #region IEquatable<ConfigKey> 成员

            public bool Equals(ConfigKey other)
            {
                return CompanyID == other.CompanyID && SolutionID == other.SolutionID && IsVaid == other.IsVaid && MaxRecords == other.MaxRecords;
            }

            #endregion
        }

        ISessionService _sessionService = null;

        private static Dictionary<ConfigKey, List<ConfigureList>> cacheConfigureList = new Dictionary<ConfigKey, List<ConfigureList>>();
        private object synObj = new object();
        public ConfigureService(ISessionService sessionService)
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
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        private void ClearCache()
        {
            if (cacheConfigureList != null)
            {
                lock (synObj)
                {
                    if (cacheConfigureList != null)
                    {
                        cacheConfigureList.Clear();

                    }
                }
            }
        }

        #region 费用代码
        /// <summary>
        /// 获取费用代码列表
        /// </summary>
        /// <param name="groupID">组ID</param>
        /// <returns>返回费用代码列表</returns>
        public List<ChargingCodeList> GetChargingCodeListByGroupID(Guid groupID)
        {
            ArgumentHelper.AssertGuidNotEmpty(groupID, "groupID");

            return GetChargingCodes(
                string.Empty,
                string.Empty,
                groupID,
                null,
                null,
                1000);
        }

        /// <summary>
        /// 获取费用代码列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="groupID">组ID</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回费用代码列表</returns>
        public List<ChargingCodeList> GetChargingCodeListBySearch(
            string code,
            string name,
            Guid? groupID,
            bool? isCommission,
            bool? isValid,
            int maxRecords)
        {
            return GetChargingCodes(
                code,
                name,
                groupID,
                isCommission,
                isValid,
                maxRecords);
        }

        /// <summary>
        /// 获取公司的费用代码列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的费用代码列表</returns>
        public List<ChargingCodeList> GetCompanyChargingCodeList(
            Guid companyID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            return new List<ChargingCodeList>();
        }

        /// <summary>
        /// 获取费用代码信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回费用代码信息</returns>
        public ChargingCodeInfo GetChargingCodeInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetChargingCodeInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ChargingCodeInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new ChargingCodeInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               Code = b.Field<string>("Code"),
                                               CName = b.Field<string>("CName"),
                                               EName = b.Field<string>("EName"),
                                               IsCommission = b.Field<bool>("IsCommission"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               GroupID = b.Field<Guid>("GroupID"),
                                               GroupName = b.Field<string>("GroupName"),
                                               GroupHierarchyCode = b.Field<string>("GroupHierarchyCode"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<string>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存费用代码信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="groupID">组ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleHierarchyResultData SaveChargingCodeInfo(
            Guid? id,
            Guid groupID,
            string code,
            string cName,
            string eName,
            bool isCommission,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(groupID, "groupID");
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveChargingCodeInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@GroupID", DbType.Guid, groupID);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@IsCommission", DbType.Boolean, isCommission);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleHierarchyResultData result = db.SingleHierarchyResult(dbCommand);
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
        /// 改变费用代码状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeChargingCodeState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeChargingCodeState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 费用代码组
        /// <summary>
        /// 获取费用代码组列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="category">分类(0:管理成本,1:运输成本,2:业务费用)</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回费用代码组列表</returns>
        public List<ChargingGroupList> GetChargingGroupList(
            string code,
            string name,
            ChargeCodeCategory? category,
            int maxRecords)
        {
            return GetChargingGroups(
                code,
                name,
                category,
                maxRecords);
        }

        /// <summary>
        /// 获取费用代码组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回费用代码组信息</returns>
        public ChargingGroupInfo GetChargingGroupInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetChargingGroupInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ChargingGroupInfo result = (from b in ds.Tables[0].AsEnumerable()
                                            select new ChargingGroupInfo
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                Code = b.Field<string>("Code"),
                                                CName = b.Field<string>("CName"),
                                                EName = b.Field<string>("EName"),
                                                ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                ParentName = b.Field<string>("ParentName"),
                                                NodeType = ChargeGroupNodeType.Group,
                                                CreateByID = b.Field<Guid>("CreateByID"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存费用代码组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回费用代码组信息</returns>
        public ManyHierarchyResultData SaveChargingGroupInfo(
            Guid? id,
            Guid? parentID,
            string code,
            string cName,
            string eName,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveChargingGroupInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 删除费用代码组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveChargingGroupInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveChargingGroupInfo");

                string tempId = id.ToString();
                string tempUpdateDate = updateDate.HasValue ? updateDate.Value.ToString() : "";

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempId);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDate);
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

        /// <summary>
        /// 设置父费用代码组
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="setByID">设置人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SetChargingGroupParent(
            Guid id,
            Guid? parentID,
            Guid setByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSetParentChargingGroup");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        #endregion

        #region 币种(基础)
        /// <summary>
        /// 获取币种列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryID">国家ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回币种列表</returns>
        public List<CurrencyList> GetCurrencyList(
            string code,
            string name,
            Guid? countryID,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCurrencyList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CurrencyList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CurrencyList
                                              {
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryName = b.Field<string>("CountryName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

        ///// <summary>
        ///// 获取公司的币种列表
        ///// </summary>
        ///// <param name ="companyID">公司ID</param>
        ///// <param name="isValid">是否有效</param>
        ///// <returns>返回公司的币种列表</returns>
        //public List<CurrencyList> GetCompanyCurrencyList(
        //    Guid companyID,
        //    bool? isValid)
        //{
        //    return new List<CurrencyList>();
        //}

        /// <summary>
        /// 获取币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回币种信息</returns>
        public CurrencyInfo GetCurrencyInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCurrencyInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CurrencyInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new CurrencyInfo
                                       {
                                           CName = b.Field<string>("CName"),
                                           Code = b.Field<string>("Code"),
                                           CountryID = b.Field<Guid>("CountryID"),
                                           CountryName = b.Field<string>("CountryName"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           EName = b.Field<string>("EName"),
                                           ID = b.Field<Guid>("ID"),
                                           IsValid = b.Field<bool>("IsValid"),
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
        /// 保存币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="countryID">国家ID</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveCurrencyInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid countryID,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(countryID, "countryID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCurrencyInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变币种状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeCurrencyState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeCurrencyState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 解决方案列表
        /// <summary>
        /// 获取解决方案列表
        /// </summary>
        /// <param name="name">中文名</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回解决方案列表</returns>
        public List<SolutionList> GetSolutionList(
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionList");

                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new SolutionList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  CName = b.Field<string>("CName"),
                                                  EName = b.Field<string>("EName"),
                                                  InvoiceDateType = (InvoiceDateType)b.Field<byte>("InvoiceDateType"),
                                                  IsValid = b.Field<bool>("IsValid"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取解决方案信息
        /// </summary>
        /// <param name="id">解决方案ID</param>
        /// <returns>返回解决方案信息</returns>
        public SolutionInfo GetSolutionInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new SolutionInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           CName = b.Field<string>("CName"),
                                           EName = b.Field<string>("EName"),
                                           Remark = b.Field<string>("Remark"),
                                           InvoiceDateType = (InvoiceDateType)b.Field<byte>("InvoiceDateType"),
                                           IsAccountShare = b.Field<bool>("IsAccountingShare"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存解决方案信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="invoiceDateType">账单日期类型（0建立时间、1业务时间）</param>
        /// <param name="isAccountingShare">是否财务共享</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回解决方案信息</returns>
        public SingleResultData SaveSolutionInfo(
            Guid? id,
            string cName,
            string eName,
            InvoiceDateType invoiceDateType,
            bool isAccountingShare,
            string remark,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@InvoiceDateType", DbType.Int16, invoiceDateType);
                db.AddInParameter(dbCommand, "@IsAccountingShare", DbType.Boolean, isAccountingShare);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变解决方案状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeSolutionState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeSolutionState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 拷贝解决方案信息
        /// </summary>
        /// <param name="id">解决方案ID</param>
        /// <returns>返回解决方案信息</returns>
        public SolutionInfo CopySolutionInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCopySolutionInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new SolutionInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           CName = b.Field<string>("CName"),
                                           EName = b.Field<string>("EName"),
                                           InvoiceDateType = (InvoiceDateType)b.Field<byte>("InvoiceDateType"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
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
        #endregion

        #region 会计科目
        /// <summary>
        /// 获取公司的会计科目列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的会计科目列表</returns>
        public List<SolutionGLCodeList> GetCompanyGLCodeList(
            Guid companyID,
            bool? isValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyGLCodeList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLCodeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new SolutionGLCodeList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Code = b.Field<string>("Code"),
                                                         CName = b.Field<string>("CName"),
                                                         EName = b.Field<string>("EName"),
                                                         EfullName = b.Field<string>("FullEName"),
                                                         FullName = b.Field<string>("FullName"),
                                                         GLCodeNodeType = GLCodeNodeType.Item,
                                                         ParentID = b.Field<Guid?>("ParentID"),
                                                         ParentName = b.Field<string>("ParentName"),
                                                         IsValid = b.Field<bool>("IsValid"),
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         CreateByID = b.Field<Guid>("CreateByID"),
                                                         Description = b.Field<string>("Description"),
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
        /// 获取解决方案的会计科目列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的会计科目列表</returns>
        public List<SolutionGLCodeList> GetSolutionGLCodeList(
            Guid solutionID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLCodeList");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLCodeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new SolutionGLCodeList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        Code = b.Field<string>("Code"),
                                                        CName = b.Field<string>("CName"),
                                                        EName = b.Field<string>("EName"),
                                                        FullName = b.Field<string>("FullName"),
                                                        EfullName = b.Field<string>("FullEName"),
                                                        GLCodeNodeType = GLCodeNodeType.Item,
                                                        ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                        ParentName = b.Field<string>("ParentName"),
                                                        SolutionID = solutionID,
                                                        IsValid = b.Field<bool>("IsValid"),
                                                        CreateByName = b.Field<string>("CreateByName"),
                                                        CreateDate = b.Field<DateTime>("CreateDate"),
                                                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                        CreateByID = b.Field<Guid>("CreateByID"),
                                                        Description = b.Field<string>("Description")
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
        /// 获取解决方案的会计科目信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的会计科目信息</returns>
        public SolutionGLCodeInfo GetSolutionGLCodeInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLCodeInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionGLCodeInfo result = (from b in ds.Tables[0].AsEnumerable()
                                             select new SolutionGLCodeInfo
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 SolutionID = b.Field<Guid>("SolutionID"),
                                                 Code = b.Field<string>("Code"),
                                                 CName = b.Field<string>("CName"),
                                                 EName = b.Field<string>("EName"),
                                                 Description = b.Field<string>("Description"),
                                                 GLCodeNodeType = GLCodeNodeType.Item,
                                                 ParentID = b.Field<Guid>("ParentID"),
                                                 ParentName = b.Field<string>("ParentName"),
                                                 IsValid = b.Field<bool>("IsValid"),
                                                 CreateByID = b.Field<Guid>("CreateByID"),
                                                 CreateByName = b.Field<string>("CreateByName"),
                                                 CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存解决方案的会计科目信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="glGroupID">会计科目组ID</param>
        /// <param name="codes">代码</param>
        /// <param name="cNames">中文名</param>
        /// <param name="eNames">英文名</param>
        /// <param name="descriptions">描述</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回解决方案的会计科目信息</returns>
        public ManyResultData SaveSolutionGLCodeInfo(
            Guid solutionID,
            Guid?[] ids,
            Guid glGroupID,
            string[] codes,
            string[] cNames,
            string[] eNames,
            string[] descriptions,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");
            ArgumentHelper.AssertGuidNotEmpty(glGroupID, "glGroupID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionGLCodeInfo");

                string tempIds = ids.Join();
                string tempCodes = codes.Join();
                string tempCNames = cNames.Join();
                string tempENames = eNames.Join();
                string tempDescriptions = descriptions.Join();
                string tempDataVersions = updateDates.Join();

                Guid[] groupIds = new Guid[ids.Length];
                for (int i = 0; i < ids.Length; i++)
                {
                    groupIds[i] = glGroupID;
                }
                string tempGroupIDs = groupIds.Join();


                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@GroupIds", DbType.String, tempGroupIDs);
                db.AddInParameter(dbCommand, "@Codes", DbType.String, tempCodes);
                db.AddInParameter(dbCommand, "@CNames", DbType.String, tempCNames);
                db.AddInParameter(dbCommand, "@ENames", DbType.String, tempENames);
                db.AddInParameter(dbCommand, "@Descriptions", DbType.String, tempDescriptions);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变解决方案的会计科目状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeSolutionGLCodeState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeSolutionGLCodeState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 会计科目组
        /// <summary>
        /// 获取会计科目组列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回费用代码组列表</returns>
        public List<SolutionGLGroupList> GetSolutionGLGroupList(
            string code,
            string name,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetGLGroupList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLGroupList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new SolutionGLGroupList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Code = b.Field<string>("Code"),
                                                         CName = b.Field<string>("CName"),
                                                         EName = b.Field<string>("EName"),
                                                         //Type = (ChargingGroupType)b.Field<byte>("Type"),
                                                         HierarchyCode = b.Field<string>("HierarchyCode"),
                                                         ParentID = b.Field<Guid?>("ParentID"),
                                                         ParentName = b.Field<string>("ParentName"),
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取会计科目组信息
        /// </summary>
        /// <param name="id">父ID</param>
        /// <returns>返回会计科目组信息</returns>
        public SolutionGLGroupInfo GetSolutionGLGroupInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetGLGroupInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionGLGroupInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new SolutionGLGroupInfo
                                              {
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CreateByID = b.Field<Guid>("CreateByID"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  //Type = (ChargingGroupType)b.Field<byte>("Type"),
                                                  HierarchyCode = b.Field<string>("HierarchyCode"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  EName = b.Field<string>("EName"),
                                                  ID = b.Field<Guid>("ID"),
                                                  ParentID = b.Field<Guid?>("ParentID"),
                                                  ParentName = b.Field<string>("ParentName"),
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
        /// 保存会计科目组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleHierarchyResultData SaveSolutionGLGroupInfo(
            Guid? id,
            Guid? parentID,
            string code,
            string cName,
            string eName,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveGLGroupInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleHierarchyResultData result = db.SingleHierarchyResult(dbCommand);
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
        /// 删除会计科目组信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveSolutionGLGroupInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveGLGroupInfo");

                string tempId = id.ToString();
                string tempUpdateDate = updateDate.HasValue ? updateDate.Value.ToString() : "";

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempId);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDate);
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

        /// <summary>
        /// 设置父会计科目
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">父ID</param>
        /// <param name="setByID">设置人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetSolutionGLGroupParent(
            Guid id,
            Guid? parentID,
            Guid setByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSetParentGLGroup");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 汇率
        /// <summary>
        /// 获取解决方案的汇率列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的汇率列表</returns>
        public List<SolutionExchangeRateList> GetSolutionExchangeRateList(
            Guid solutionID,
            bool? isValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionExchangeRateList");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, ExchangeType.Bill);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<SolutionExchangeRateList>();
                }

                List<SolutionExchangeRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new SolutionExchangeRateList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              ExchangeType = (ExchangeType)b.Field<Byte>("Type"),
                                                              SourceCurrency = b.Field<string>("SourceCurrency"),
                                                              TargetCurrency = b.Field<string>("TargetCurrency"),
                                                              FromDate = b.Field<DateTime>("FromDate"),
                                                              ToDate = b.Field<DateTime>("ToDate"),
                                                              Rate = b.Field<decimal>("Rate"),
                                                              IsValid = b.Field<bool>("IsValid"),
                                                              CreateByName = b.Field<string>("CreateByName"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                              CreateByID = b.Field<Guid>("CreateByID"),
                                                              SolutionID = b.Field<Guid>("SolutionID"),
                                                              SourceCurrencyID = b.Field<Guid>("SourceCurrencyID"),
                                                              TargetCurrencyID = b.Field<Guid>("TargetCurrencyID"),
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

        /// <summary>
        /// 获取公司的汇率列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的汇率列表</returns>
        public List<SolutionExchangeRateList> GetCompanyExchangeRateList(
            Guid companyID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyExchangeRateList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, ExchangeType.Bill);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<SolutionExchangeRateList>();
                }

                List<SolutionExchangeRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new SolutionExchangeRateList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              ExchangeType = (ExchangeType)b.Field<Byte>("Type"),
                                                              SourceCurrency = b.Field<string>("SourceCurrency"),
                                                              TargetCurrency = b.Field<string>("TargetCurrency"),
                                                              SourceCurrencyID = b.Field<Guid>("SourceCurrencyID"),
                                                              TargetCurrencyID = b.Field<Guid>("TargetCurrencyID"),
                                                              FromDate = b.Field<DateTime>("FromDate"),
                                                              ToDate = b.Field<DateTime>("ToDate"),
                                                              Rate = b.Field<decimal>("Rate"),
                                                              IsValid = b.Field<bool>("IsValid"),
                                                              CreateByName = b.Field<string>("CreateByName"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取解决方案的汇率信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的汇率信息</returns>
        public SolutionExchangeRateInfo GetSolutionExchangeRateInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionExchangeRateInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionExchangeRateInfo result = (from b in ds.Tables[0].AsEnumerable()
                                                   select new SolutionExchangeRateInfo
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       SolutionID = b.Field<Guid>("SolutionID"),
                                                       ExchangeType = (ExchangeType)b.Field<Byte>("Type"),
                                                       SourceCurrencyID = b.Field<Guid>("SourceCurrencyID"),
                                                       SourceCurrency = b.Field<string>("SourceCurrency"),
                                                       TargetCurrencyID = b.Field<Guid>("TargetCurrencyID"),
                                                       TargetCurrency = b.Field<string>("TargetCurrency"),
                                                       FromDate = b.Field<DateTime>("FromDate"),
                                                       ToDate = b.Field<DateTime>("ToDate"),
                                                       Rate = b.Field<decimal>("Rate"),
                                                       IsValid = b.Field<bool>("IsValid"),
                                                       CreateByID = b.Field<Guid>("CreateByID"),
                                                       CreateByName = b.Field<string>("CreateByName"),
                                                       CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存解决方案的汇率信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="sourceCurrencyIDs">源币种ID</param>
        /// <param name="targetCurrencyIDs">目标币种ID</param>
        /// <param name="fromDates">开始时间</param>
        /// <param name="toDates">结束时间</param>
        /// <param name="rates">汇率</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData SaveSolutionExchangeRateInfo(
            Guid solutionID,
            ExchangeType exchangeType,
            Guid?[] ids,
            Guid[] sourceCurrencyIDs,
            Guid[] targetCurrencyIDs,
            DateTime[] fromDates,
            DateTime[] toDates,
            decimal[] rates,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");
            ArgumentHelper.AssertGuidNotEmpty(sourceCurrencyIDs, "sourceCurrencyID");
            ArgumentHelper.AssertGuidNotEmpty(targetCurrencyIDs, "targetCurrencyID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionExchangeRateInfo");

                string tempIds = ids.Join();
                string tempsourceCurrencyIDs = sourceCurrencyIDs.Join();
                string temptargetCurrencyIDs = targetCurrencyIDs.Join();
                string tempfromDates = fromDates.Join();
                string temptoDates = toDates.Join();
                string temprates = rates.Join(5);
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, exchangeType);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@SourceCurrencyIDs", DbType.String, tempsourceCurrencyIDs);
                db.AddInParameter(dbCommand, "@TargetCurrencyIDs", DbType.String, temptargetCurrencyIDs);
                db.AddInParameter(dbCommand, "@FromDates", DbType.String, tempfromDates);
                db.AddInParameter(dbCommand, "@ToDates", DbType.String, temptoDates);
                db.AddInParameter(dbCommand, "@Rates", DbType.String, temprates);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 更改解决方案的汇率有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeSolutionExchangeRateState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeSolutionExchangeRateState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 币种(解决方案)
        /// <summary>
        /// 获取解决方案的币种列表
        /// </summary>
        /// <param name="companyID">操作口岸ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的币种列表</returns>
        public List<SolutionCurrencyList> GetCompanyCurrencyList(
             Guid companyID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyCurrencyList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionCurrencyList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new SolutionCurrencyList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          SolutionID = b.Field<Guid>("SolutionID"),
                                                          CurrencyName = b.Field<string>("CurrencyName"),
                                                          CurrencyID = b.Field<Guid>("CurrencyID"),
                                                          StandardCurrencyID = b.Field<Guid>("StandardCurrencyID"),
                                                          DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
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
        /// 获取解决方案的币种列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的币种列表</returns>
        public List<SolutionCurrencyList> GetSolutionCurrencyList(
             Guid solutionID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionCurrencyList");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionCurrencyList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new SolutionCurrencyList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          SolutionID = b.Field<Guid>("SolutionID"),
                                                          CurrencyName = b.Field<string>("CurrencyName"),
                                                          CurrencyID = b.Field<Guid>("CurrencyID"),
                                                          CreateByName = b.Field<string>("CreateByName"),
                                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          CreateByID = b.Field<Guid>("CreateByID")
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
        /// 获取解决方案的币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的币种信息</returns>
        public SolutionCurrencyInfo GetSolutionCurrencyInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionCurrencyInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionCurrencyInfo result = (from b in ds.Tables[0].AsEnumerable()
                                               select new SolutionCurrencyInfo
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   CurrencyID = b.Field<Guid>("CurrencyID"),
                                                   CurrencyName = b.Field<string>("CurrencyName"),
                                                   CreateByID = b.Field<Guid>("CreateByID"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存解决方案的币种信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="currencyIDs">币种ID列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回解决方案的币种信息</returns>
        public ManyResultData SaveSolutionCurrencyInfo(
            Guid solutionID,
            Guid[] ids,
            Guid[] currencyIDs,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, currencyIDs, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionCurrencyInfo");

                string tempIds = ids.Join();
                string tempCurIds = currencyIDs.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, tempCurIds);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 删除解决方案下的币种信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveSolutionCurrencyInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveSolutionCurrencyInfo");


                string tempIds = id.ToString();
                string tempUpdateDates = updateDate.HasValue ? updateDate.Value.ToString() : "";

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
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
        #endregion

        #region 费用代码(解决方案)
        /// <summary>
        /// 获取解决方案的费用代码列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的费用代码列表</returns>
        public List<SolutionChargingCodeList> GetSolutionChargingCodeListBySolutionID(
            Guid solutionID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionChargingCodeList");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionChargingCodeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new SolutionChargingCodeList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              ChargingCodeName = b.Field<string>("Name"),
                                                              SolutionID = solutionID,
                                                              ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                              IsAgent = b.Field<bool>("IsAgent"),
                                                              ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                              ParentName = b.Field<string>("ParentName"),
                                                              CreateByID = b.Field<Guid>("CreateByID"),
                                                              CreateByName = b.Field<string>("CreateByName"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取解决方案的费用代码列表
        /// </summary>
        ///<param name="isAgent">是否代于理</param>
        ///<param name="Name">名称</param>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的费用代码列表</returns>
        public List<SolutionChargingCodeList> GetSolutionChargingCodeListByList(
            Guid solutionID,
            string Name,
            bool? isAgent,
            bool? isCommission,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionChargingCodeListByName");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Name", DbType.String, Name);
                db.AddInParameter(dbCommand, "@IsCommission", DbType.Boolean, isCommission);
                db.AddInParameter(dbCommand, "@IsAgent", DbType.Boolean, isAgent);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionChargingCodeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new SolutionChargingCodeList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              Code = b.Field<string>("Code"),
                                                              ChargingCodeName = b.Field<string>("Name"),
                                                              EName = b.Field<string>("EName"),
                                                              SolutionID = solutionID,
                                                              ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                              IsAgent = b.Field<bool>("IsAgent"),
                                                              ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                              ParentName = b.Field<string>("ParentName"),
                                                              CreateByID = b.Field<Guid>("CreateByID"),
                                                              CreateByName = b.Field<string>("CreateByName"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取解决方案的费用代码信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的费用代码信息</returns>
        public SolutionChargingCodeInfo GetSolutionChargeCodeInfo(Guid id)
        {
            return new SolutionChargingCodeInfo();
        }

        /// <summary>
        /// 保存解决方案的费用代码
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="chargeCodeIDs">费用代码ID列表</param>
        /// <param name="isAgents">是否代收代付列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveSolutionChargingCodeInfo(
            Guid solutionID,
            Guid?[] ids,
            Guid[] chargeCodeIDs,
            bool[] isAgents,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionChargingCodeInfo");

                string tempVersions = updateDates.Join();
                string tempChargeCodeIds = chargeCodeIDs.Join();
                string tempIds = ids.Join();
                string tempIsAgents = isAgents.Join();

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@ChargingCodeIDs", DbType.String, tempChargeCodeIds);
                db.AddInParameter(dbCommand, "@IsAgents", DbType.String, tempIsAgents);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 删除解决方案下的费用代码
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本列表</param>
        public void RemoveSolutionChargingCodeInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveSolutionChargingCodeInfo");

                string tempIds = ids.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
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
        #endregion

        #region 代理
        /// <summary>
        /// 获取公司下的代理列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司下的代理列表</returns>
        public List<CustomerList> GetCompanyAgentList(
            Guid companyID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyAgentList");

                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                List<CustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerList
                                              {
                                                  CAddress = b.Field<string>("CAddress"),
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EName = b.Field<string>("EName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  Fax = b.Field<string>("Fax"),
                                                  State = (CustomerStateType)b.Field<byte>("State"),
                                                  ID = b.Field<Guid>("ID"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Tel2 = b.Field<string>("Tel2"),
                                                  Term = b.Field<int>("Term")

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
        /// 获取解决方案下的代理列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案下的代理列表</returns>
        public List<SolutionAgentList> GetSolutionAgentList(
            Guid solutionID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionAgentList");

                db.AddInParameter(dbCommand, "@SolutionId", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionAgentList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new SolutionAgentList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       AgentName = b.Field<string>("AgentName"),
                                                       AgentID = b.Field<Guid>("AgentID"),
                                                       CreateByID = b.Field<Guid>("CreateByID"),
                                                       Remark = b.Field<string>("Remark"),
                                                       SolutionID = solutionID,
                                                       IsValid = b.Field<bool>("IsValid"),
                                                       CreateByName = b.Field<string>("CreateByName"),
                                                       CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取解决方案下的代理的详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案下的代理的详细信息</returns>
        public SolutionAgentInfo GetSolutionAgentInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionAgentInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionAgentInfo result = (from b in ds.Tables[0].AsEnumerable()
                                            select new SolutionAgentInfo
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                SolutionID = b.Field<Guid>("SolutionID"),
                                                AgentID = b.Field<Guid>("AgentID"),
                                                AgentName = b.Field<string>("AgentName"),
                                                Remark = b.Field<string>("Remark"),
                                                IsValid = b.Field<bool>("IsValid"),
                                                CreateByID = b.Field<Guid>("CreateByID"),
                                                CreateByName = b.Field<string>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存解决方下的代理信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="agentIDs">代理人ID列表</param>
        /// <param name="remarks">备注列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本列表</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData SaveSolutionAgentInfo(
            Guid solutionID,
            Guid[] ids,
            Guid[] agentIDs,
            string[] remarks,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, agentIDs, remarks, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionAgentInfo");

                string tempIds = ids.Join();
                string tempAgentIds = agentIDs.Join();
                string tempRemarks = remarks.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@AgentIDs", DbType.String, tempAgentIds);
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, tempRemarks);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变解决方案的代理状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeSolutionAgentState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeSolutionAgentState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        /// <summary>
        /// 获取解决方案的会计配置列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回解决方案的会计配置列表</returns>
        public List<SolutionGLConfigList> GetSolutionGLConfigList(
            Guid solutionID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLConfigList");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLConfigList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new SolutionGLConfigList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          GLConfigTypeID = (int)b.Field<byte>("Type"),
                                                          ChargingCodeName = b.Field<string>("ChargingCodeName"),
                                                          CurrencyName = b.Field<string>("CurrencyName"),
                                                          DRGLCodeName = b.Field<string>("DRGLCodeName"),
                                                          DRGLFullName = b.Field<string>("DRGLFullName"),
                                                          CRGLCodeName = b.Field<string>("CRGLCodeName"),
                                                          CRGLFullName = b.Field<string>("CRGLFullName"),
                                                          IsValid = b.Field<bool>("IsValid"),
                                                          CreateByName = b.Field<string>("CreateByName"),
                                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          ChargingCodeID = b.Field<Guid?>("ChargingCodeID"),
                                                          CreateByID = b.Field<Guid>("CreateByID"),
                                                          CRGLCodeID = b.Field<Guid>("CRGLCodeID"),
                                                          CurrencyID = b.Field<Guid>("CurrencyID"),
                                                          DRGLCodeID = b.Field<Guid>("DRGLCodeID"),
                                                          SolutionID = b.Field<Guid>("SolutionID"),
                                                          CompanyID = b.Field<Guid?>("CompanyID")
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
        /// 获取会计科目配置类型
        /// </summary>
        /// <returns></returns>
        public List<GLConfigType> GetGLConfigTypes()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetEnumListForType");

                db.AddInParameter(dbCommand, "@EnumTypeName", DbType.String, "GLConfigType");
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<GLConfigType>();
                }

                List<GLConfigType> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new GLConfigType
                                            {
                                                Index = (int)b.Field<byte>("Value"),
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
        /// 获取解决方案的会计配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回解决方案的会计配置信息</returns>
        public SolutionGLConfigInfo GetSolutionGLConfigInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLConfigInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionGLConfigInfo result = (from b in ds.Tables[0].AsEnumerable()
                                               select new SolutionGLConfigInfo
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   //Type = (GLConfigType)b.Field<byte>("Type"),
                                                   GLConfigTypeID = (int)b.Field<byte>("Type"),
                                                   //GLConfigTypeName = b.Field<String>("TypeName"),
                                                   ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                   ChargingCodeName = b.Field<string>("ChargingCodeName"),
                                                   CurrencyID = b.Field<Guid>("CurrencyID"),
                                                   CurrencyName = b.Field<string>("CurrencyName"),
                                                   DRGLCodeID = b.Field<Guid>("DRGLCodeID"),
                                                   DRGLCodeName = b.Field<string>("DRGLCodeName"),
                                                   CRGLCodeID = b.Field<Guid>("CRGLCodeID"),
                                                   CRGLCodeName = b.Field<string>("CRGLCodeName"),
                                                   IsValid = b.Field<bool>("IsValid"),
                                                   CreateByID = b.Field<Guid>("CreateByID"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存解决方案下的会计配置信息
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="types">类型(0费用项目、1主营业收入、2预收预付、3代收代付)</param>
        /// <param name="chargeCodeIDs">费用代码ID</param>
        /// <param name="currencyIDs">币种ID</param>
        /// <param name="drGLCodeIDs">代收会计科目ID</param>
        /// <param name="crGLCodeIDs">代付会计科目ID</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData SaveSolutionGLConfigInfo(
            Guid solutionID,
            Guid?[] ids,
            int[] types,
            Guid?[] chargeCodeIDs,
            Guid[] currencyIDs,
            Guid[] drGLCodeIDs,
            Guid[] crGLCodeIDs,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionId");
            ArgumentHelper.AssertGuidNotEmpty(currencyIDs, "currencyID");
            ArgumentHelper.AssertGuidNotEmpty(drGLCodeIDs, "drGLCodeID");
            ArgumentHelper.AssertGuidNotEmpty(crGLCodeIDs, "crGLCodeID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionGLConfigInfo");

                string tempIds = ids.Join();
                string tempTypes = types.Join<int>();
                string tempChargingCodeIDs = chargeCodeIDs.Join();
                string tempCurrencyIDs = currencyIDs.Join();
                string tempDRGLCodeIDs = drGLCodeIDs.Join();
                string tempCRGLCodeIDs = crGLCodeIDs.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@SolutionId", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@Types", DbType.String, tempTypes);
                db.AddInParameter(dbCommand, "@ChargeCodeIDs", DbType.String, tempChargingCodeIDs);
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, tempCurrencyIDs);
                db.AddInParameter(dbCommand, "@DrGLCodeIDs", DbType.String, tempDRGLCodeIDs);
                db.AddInParameter(dbCommand, "@CrGLCodeIDs", DbType.String, tempCRGLCodeIDs);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变解决方案下会计配置有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeSolutionGLConfigState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeSolutionGLConfigState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 获取公司配置列表
        /// </summary>
        /// <param name="isVaid">是否有效</param>
        /// <returns>返回公司配置列表</returns>
        public List<ConfigureList> GetConfigureListByVaid(bool? isVaid)
        {
            return InnerGetConfigureListByList(null, null, isVaid, 0);
        }

        private List<ConfigureList> InnerGetConfigureListByList(Guid? companyID,
            Guid? solutionID,
            bool? isVaid,
            int maxRecords)
        {
            ConfigKey key = new ConfigKey(companyID, solutionID, isVaid, maxRecords);
            if (cacheConfigureList.ContainsKey(new ConfigKey(companyID, solutionID, isVaid, maxRecords)))
            {
                return cacheConfigureList[key];
            }
            var result = cacheConfigureList[key] = GetConfigureListByList(companyID, solutionID, isVaid, maxRecords);
            return result;

        }

        /// <summary>
        /// 获取公司配置列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="solutionID">解决方案</param>
        /// <param name="isVaid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回公司配置列表</returns>
        public List<ConfigureList> GetConfigureListByList(
            Guid? companyID,
            Guid? solutionID,
            bool? isVaid,
            int maxRecords)
        {
            try
            {
                ConfigKey key = new ConfigKey(companyID, solutionID, isVaid, maxRecords);
                if (cacheConfigureList.ContainsKey(new ConfigKey(companyID, solutionID, isVaid, maxRecords)))
                {
                    return cacheConfigureList[key];
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isVaid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int16, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new ConfigureList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   CustomerID = b.Field<Guid>("CustomerID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   ShortCode = b.Field<string>("ShortCode"),
                                                   CompanyID = b.Field<Guid>("CompanyID"),
                                                   CompanyName = b.Field<string>("CompanyName"),
                                                   CustomerName = b.Field<string>("CustomerName"),
                                                   BusinessClosingDate = b.Field<DateTime?>("BusinessClosingDate"),
                                                   AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                                                   ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                                                   IssuePlaceID = b.IsNull("IssuePlaceID") ? Guid.Empty : b.Field<Guid>("IssuePlaceID"),
                                                   IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                                   IsValid = b.Field<bool>("IsValid"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
                                                   SolutionName = b.Field<string>("SolutionName"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               }).ToList();

                return cacheConfigureList[key] = results;
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
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns>返回公司配置信息</returns>
        /// <remarks>配置表信息+抬头+签发地详细信息</remarks>
        public ConfigureInfo GetCompanyConfigureInfo(Guid companyID)
        {
            return GetCompanyConfigureInfo(companyID, IsEnglish);
        }

        /// <summary>
        /// 根据公司对应客户获取公司配置信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns>返回公司配置信息</returns>
        /// <remarks>配置表信息+抬头+签发地详细信息</remarks>
        public ConfigureInfo GetCompanyConfigureInfoByCustomer(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyConfigureInfoByCustomer");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ConfigureInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new ConfigureInfo
                                        {
                                            ID = b.Field<Guid>("ID"),
                                            ShortCode = b.Field<string>("ShortCode"),
                                            CompanyID = b.Field<Guid>("CompanyID"),
                                            CompanyName = b.Field<string>("CompanyName"),
                                            CustomerID = b.Field<Guid>("CustomerID"),
                                            CustomerName = b.Field<string>("CustomerName"),
                                            StandardCurrencyID = b.Field<Guid>("StandardCurrencyID"),
                                            StandardCurrency = b.Field<string>("StandardCurrency"),
                                            DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                                            DefaultCurrency = b.Field<string>("DefaultCurrency"),
                                            BusinessClosingDate = b.Field<DateTime?>("BusinessClosingDate"),
                                            AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                                            ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                                            IssuePlaceID = b.IsNull("IssuePlaceID") ? Guid.Empty : b.Field<Guid>("IssuePlaceID"),
                                            IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                            IsValid = b.Field<bool>("IsValid"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<string>("CreateByName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            SolutionID = b.Field<Guid>("SolutionID"),
                                            SolutionName = b.Field<string>("SolutionName"),
                                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                            IsVATinvoice = b.IsNull("IsVATinvoice") ? false : b.Field<bool>("IsVATinvoice"),
                                            VATFeeID = b.Field<Guid?>("VATFeeID"),
                                            VATFeeName = b.Field<string>("VATFeeName"),
                                            VATrate = b.Field<Decimal?>("VATrateAP"),
                                            VATFeeCode = b.Field<string>("VATFeeCode"),
                                            ReleaserEmail = b.Field<string>("ReleaserEmail"),
                                            CMBNetComUserID = b.Field<string>("CMBNetComUserID"),
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
        /// 获取配置信息：通过操作口岸ID
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        /// <remarks>配置表信息+抬头+签发地详细信息</remarks>
        public ConfigureInfo GetCompanyConfigureInfo(Guid companyID, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyConfigureInfo");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ConfigureInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new ConfigureInfo
                                        {
                                            ID = b.Field<Guid>("ID"),
                                            ShortCode = b.Field<string>("ShortCode"),
                                            CompanyID = b.Field<Guid>("CompanyID"),
                                            CompanyName = b.Field<string>("CompanyName"),
                                            CustomerID = b.Field<Guid>("CustomerID"),
                                            CustomerName = b.Field<string>("CustomerName"),
                                            StandardCurrencyID = b.Field<Guid>("StandardCurrencyID"),
                                            StandardCurrency = b.Field<string>("StandardCurrency"),
                                            DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                                            DefaultCurrency = b.Field<string>("DefaultCurrency"),
                                            BusinessClosingDate = b.Field<DateTime?>("BusinessClosingDate"),
                                            ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                                            AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                                            IssuePlaceID = b.IsNull("IssuePlaceID") ? Guid.Empty : b.Field<Guid>("IssuePlaceID"),
                                            IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                            IsValid = b.Field<bool>("IsValid"),
                                            SolutionID = b.Field<Guid>("SolutionID"),
                                            SolutionName = b.Field<string>("SolutionName"),
                                            BLTitleID = b.IsNull("BLTitleID") ? Guid.Empty : b.Field<Guid>("BLTitleID"),
                                            BLTitleName = b.Field<string>("BLTitleName"),
                                            IsVATinvoice = b.IsNull("IsVATinvoice") ? false : b.Field<bool>("IsVATinvoice"),
                                            VATFeeID = b.Field<Guid?>("VATFeeID"),
                                            VATFeeName = b.Field<string>("VATFeeName"),
                                            VATrate = b.Field<Decimal?>("VATrateAP"),
                                            VATFeeCode = b.Field<string>("VATFeeCode"),
                                            ReleaserEmail = b.Field<string>("ReleaserEmail"),
                                            TaxControlVersion = b.Field<string>("TaxControlVersion"),
                                            CMBNetComUserID = b.Field<string>("CMBNetComUserID"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<string>("CreateByName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取配置信息：通过操作口岸ID
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        /// <remarks>仅配置表基本信息</remarks>
        public ConfigureInfo GetConfigureInfoByCompanyID(Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "CompanyID");
            ConfigureInfo result = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureInfoByCompanyID");
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                    return new ConfigureInfo();

                result = (from b in ds.Tables[0].AsEnumerable()
                          select new ConfigureInfo
                          {
                              ID = b.Field<Guid>("ID"),
                              ShortCode = b.Field<string>("ShortCode"),
                              CompanyID = b.Field<Guid>("CompanyID"),
                              CompanyName = b.Field<string>("CompanyName"),
                              CustomerID = b.Field<Guid>("CustomerID"),
                              StandardCurrencyID = b.Field<Guid>("StandardCurrencyID"),
                              DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                              BusinessClosingDate = b.Field<DateTime?>("BusinessClosingDate"),
                              AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                              ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                              IssuePlaceID = b.IsNull("IssuePlaceID") ? Guid.Empty : b.Field<Guid>("IssuePlaceID"),
                              DefaultAgentDescription = b.Field<string>("AgentDescription"),
                              IsValid = b.Field<bool>("IsValid"),
                              SolutionID = b.Field<Guid>("SolutionID"),
                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                              IsVATinvoice = b.IsNull("IsVATinvoice") ? false : b.Field<bool>("IsVATinvoice"),
                              VATrate = b.Field<decimal?>("VATrateAP"),
                              TaxControlVersion = b.Field<string>("TaxControlVersion"),
                              CMBNetComUserID = b.Field<string>("CMBNetComUserID"),
                              CreateByID = b.Field<Guid>("CreateBy"),
                              CreateDate = b.Field<DateTime>("CreateDate"),
                          }).SingleOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回公司配置信息</returns>
        public ConfigureInfo GetConfigureInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ConfigureInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new ConfigureInfo
                                        {
                                            ID = b.Field<Guid>("ID"),
                                            ShortCode = b.Field<string>("ShortCode"),
                                            CompanyID = b.Field<Guid>("CompanyID"),
                                            CompanyName = b.Field<string>("CompanyName"),
                                            CustomerID = b.Field<Guid>("CustomerID"),
                                            CustomerName = b.Field<string>("CustomerName"),
                                            StandardCurrencyID = b.Field<Guid>("StandardCurrencyID"),
                                            StandardCurrency = b.Field<string>("StandardCurrency"),
                                            DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                                            DefaultCurrency = b.Field<string>("DefaultCurrency"),
                                            BusinessClosingDate = b.Field<DateTime?>("BusinessClosingDate"),
                                            ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                                            AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                                            IssuePlaceID = b.IsNull("IssuePlaceID") ? Guid.Empty : b.Field<Guid>("IssuePlaceID"),
                                            IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                            IsValid = b.Field<bool>("IsValid"),
                                            SolutionID = b.Field<Guid>("SolutionID"),
                                            SolutionName = b.Field<string>("SolutionName"),
                                            BLTitleID = b.IsNull("BLTitleID") ? Guid.Empty : b.Field<Guid>("BLTitleID"),
                                            BLTitleName = b.Field<string>("BLTitleName"),
                                            IsVATinvoice = b.IsNull("IsVATinvoice") ? false : b.Field<bool>("IsVATinvoice"),
                                            VATFeeID = b.Field<Guid?>("VATFeeID"),
                                            VATFeeName = b.Field<string>("VATFeeName"),
                                            VATrate = b.Field<Decimal?>("VATrateAP"),
                                            VATFeeCode = b.Field<string>("VATFeeCode"),
                                            ReleaserEmail = b.Field<string>("ReleaserEmail"),
                                            TaxControlVersion = b.Field<string>("TaxControlVersion"),
                                            CMBNetComUserID = b.Field<string>("CMBNetComUserID"),
                                            CreateByID = b.Field<Guid>("CreateByID"),
                                            CreateByName = b.Field<string>("CreateByName"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存公司配置信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="standardCurrencyID">本位币</param>
        /// <param name="defaultCurrencyID">目标币</param>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="issuePlaceID">签发地ID</param>
        /// <param name="chargingClosingDate">计费关帐时间</param>
        /// <param name="accountingClosingDate">财务关帐时间</param>
        /// <param name="shortCode">简码</param>
        /// <param name="bLTitleID"></param>
        /// <param name="IsVATinvoice">是否开增值税发票</param>
        /// <param name="vATFEEID">增值税费用项目ID</param>
        /// <param name="vATrateAP">增值税率</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <param name="defaultAgentDescription"></param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveConfigureInfo(ConfigureSaveRequest configureSaveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureSaveRequest.CompanyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(configureSaveRequest.CustomerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(configureSaveRequest.StandardCurrencyID, "standardCurrencyID");
            ArgumentHelper.AssertGuidNotEmpty(configureSaveRequest.DefaultCurrencyID, "defaultCurrencyID");
            ArgumentHelper.AssertGuidNotEmpty(configureSaveRequest.SolutionID, "solutionID");
            ArgumentHelper.AssertStringNotEmpty(configureSaveRequest.ShortCode, "shortCode");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveConfigureInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, configureSaveRequest.ID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, configureSaveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, configureSaveRequest.CustomerID);
                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, configureSaveRequest.SolutionID);
                db.AddInParameter(dbCommand, "@StandardCurrencyID", DbType.Guid, configureSaveRequest.StandardCurrencyID);
                db.AddInParameter(dbCommand, "@DefaultCurrencyID", DbType.Guid, configureSaveRequest.DefaultCurrencyID);
                db.AddInParameter(dbCommand, "@IssuePlaceID", DbType.Guid, configureSaveRequest.IssuePlaceID);
                db.AddInParameter(dbCommand, "@BusinessClosingDate", DbType.DateTime, configureSaveRequest.BusinessClosingDate);
                db.AddInParameter(dbCommand, "@ChargingClosingDate", DbType.DateTime, configureSaveRequest.ChargingClosingDate);
                db.AddInParameter(dbCommand, "@AccountingClosingDate", DbType.DateTime, configureSaveRequest.AccountingClosingDate);
                db.AddInParameter(dbCommand, "@ShortCode", DbType.String, configureSaveRequest.ShortCode);
                db.AddInParameter(dbCommand, "@AgentDescription", DbType.String, configureSaveRequest.DefaultAgentDescription);
                db.AddInParameter(dbCommand, "@BLTitleID", DbType.Guid, configureSaveRequest.BLTitleID);
                db.AddInParameter(dbCommand, "@IsVATinvoice", DbType.Boolean, configureSaveRequest.IsVATinvoice);
                db.AddInParameter(dbCommand, "@VATFeeID", DbType.Guid, configureSaveRequest.VATFEEID);
                db.AddInParameter(dbCommand, "@VATrateAP", DbType.Decimal, configureSaveRequest.VATrateAP);
                db.AddInParameter(dbCommand, "@CMBNetComUserID", DbType.String, configureSaveRequest.CMBNetComUserID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, configureSaveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, configureSaveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
                ClearCache();
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
        /// 更改公司配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeConfigureState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeConfigureState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
                ClearCache();
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
        /// 获取公司配置的字典列表信息
        /// </summary>
        /// <param name="configureID">配置ID</param>
        /// <returns>返回公司配置的字典列表信息</returns>
        public List<ConfigureKeyValueList> GetConfigureKeyValueList(Guid configureID)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureKeyValueList");

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureKeyValueList> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new ConfigureKeyValueList
                                                       {
                                                           ID = b.Field<Guid>("ID"),
                                                           ConfigureKeyName = b.Field<string>("ConfigureKeyName"),
                                                           Value = b.Field<string>("Value"),
                                                           CreateByName = b.Field<string>("CreateByName"),
                                                           CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取公司配置字典信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回公司配置字典信息</returns>
        public ConfigureKeyValueInfo GetConfigureKeyValueInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureKeyValueInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ConfigureKeyValueInfo result = (from b in ds.Tables[0].AsEnumerable()
                                                select new ConfigureKeyValueInfo
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    ConfigureID = b.Field<Guid>("ConfigureID"),
                                                    ConfigureKeyID = b.Field<Guid>("ConfigureKeyID"),
                                                    ConfigureKeyName = b.Field<string>("ConfigureKeyName"),
                                                    Value = b.Field<string>("Value"),
                                                    CreateByID = b.Field<Guid>("CreateByID"),
                                                    CreateByName = b.Field<string>("CreateByName"),
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存公司配置字典信息
        /// </summary>
        /// <param name="configureID">配置ID</param>
        /// <param name="id">字典ID</param>
        /// <param name="configureKeyID">关键字ID</param>
        /// <param name="value">值</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveConfigureKeyValueInfo(
            Guid configureID,
            Guid? id,
            Guid configureKeyID,
            string value,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertGuidNotEmpty(configureKeyID, "configureKeyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveConfigureKeyValueInfo");

                string tempIds = id.HasValue ? id.Value.ToString() : string.Empty;
                string tempkeyIds = configureKeyID.ToString();
                string tempValues = value.ToString();
                string tempVersions = updateDate == null ? "" : updateDate.ToString();

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@ConfigureKeyIDs", DbType.String, tempkeyIds);
                db.AddInParameter(dbCommand, "@Values", DbType.String, tempValues);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
                ClearCache();
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
        /// 保存税控版本号
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="taxControlVersion">值</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveTaxControlVersion(
            Guid companyID,
            string taxControlVersion,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "CompanyID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "SaveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspTaxControlVersion");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@TaxControlVersion", DbType.String, taxControlVersion);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
                ClearCache();
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
        /// 删除公司配置字典信息
        /// </summary>
        /// <param name="id">字典ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveConfigureKeyValueInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveConfigureKeyValueInfo");

                string tempId = id.ToString();
                string tempUpdateDate = updateDate.HasValue ? updateDate.Value.ToString() : "";

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempId);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);
                ClearCache();
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
        /// 获取系统配置的关键字列表
        /// </summary>
        /// <returns>返回系统配置的关键字列表</returns>
        public List<ConfigureKeyList> GetConfigureKeyList()
        {
            return GetConfigureKeyListForType(ConfigureType.All);
        }
        /// <summary>
        /// 获取系统配置的关键字列表
        /// </summary>
        /// <returns>返回系统配置的关键字列表</returns>
        public List<ConfigureKeyList> GetConfigureKeyListForType(ConfigureType type)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureKeyList");

                db.AddInParameter(dbCommand, "@Type", DbType.Int32, type);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureKeyList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new ConfigureKeyList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
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
        /// 获得提单抬头（固定排序）
        /// </summary>
        /// <returns></returns>
        public List<ConfigureKeyList> GetConfigureKeyListForBLTitle()
        {
            List<ConfigureKeyList> results = GetConfigureKeyListForType(ConfigureType.BLTitle);

            results = results.OrderBy(o => o.Code).ToList();

            return results;
        }

        /// <summary>
        /// 获取公司配置字典值
        /// </summary>
        /// <param name="companyId">公司</param>
        /// <param name="key">关键字</param>
        /// <returns>返回公司配置字典值</returns>
        public string GetConfigureKeyValue(Guid companyId, string key)
        {
            return string.Empty;
        }

        /// <summary>
        /// 获取代码规则列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回代码规则列表</returns>
        public List<SolutionCodeRuleList> GetSolutionCodeRuleList(
            Guid solutionID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionCodeRuleList");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionCodeRuleList> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new SolutionCodeRuleList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          CodeMonth = b.Field<bool>("CodeMonth"),
                                                          CodePrefix = b.Field<string>("CodePrefix"),
                                                          CodeSNLength = b.Field<short>("CodeSNLength"),
                                                          CodeYear = (CodeYearFormart)b.Field<byte>("CodeYear"),
                                                          ConfigureKeyName = b.Field<string>("ConfigureName"),
                                                          CreateByID = b.Field<Guid>("CreateByID"),
                                                          CreateByName = b.Field<string>("CreateByName"),
                                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                                          Description = b.Field<string>("Description"),
                                                          IsIncludeCompanyCode = b.Field<bool>("IsIncludeCompanyCode"),
                                                          IsValid = b.Field<bool>("IsValid"),
                                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                          SolutionID = b.Field<Guid>("SolutionID"),
                                                          ConfigureKeyID = b.Field<Guid>("ConfigureKeyID")
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
        /// 获取代码规则信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回代码规则信息</returns>
        public SolutionCodeRuleInfo GetSolutionCodeRuleInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionCodeRuleInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionCodeRuleInfo result = (from b in ds.Tables[0].AsEnumerable()
                                               select new SolutionCodeRuleInfo
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   ConfigureKeyID = b.Field<Guid>("ConfigureKeyID"),
                                                   ConfigureKeyName = b.Field<string>("ConfigureName"),
                                                   Description = b.Field<string>("Description"),
                                                   IsIncludeCompanyCode = b.Field<bool>("IsIncludeCompanyCode"),
                                                   CodePrefix = b.Field<string>("CodePrefix"),
                                                   CodeYear = (CodeYearFormart)b.Field<byte>("CodeYear"),
                                                   CodeMonth = b.Field<bool>("CodeMonth"),
                                                   CodeSNLength = b.Field<short>("CodeSNLength"),
                                                   IsValid = b.Field<bool>("IsValid"),
                                                   CreateByID = b.Field<Guid>("CreateByID"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存代码规则
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="ids">ID</param>
        /// <param name="configureKeyIDs">配置字典ID列表</param>
        /// <param name="descriptions">描述</param>
        /// <param name="isIncludeCompanyCodes">包括公司代码</param>
        /// <param name="codePrefixs">代码前缀</param>
        /// <param name="codeYears">代码规则年格式</param>
        /// <param name="includeCodeMonths">在生成代码中包括月</param>
        /// <param name="codeSNLengths">序列号长度</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData SaveSolutionCodeRuleInfo(
            Guid solutionID,
            Guid?[] ids,
            Guid[] configureKeyIDs,
            string[] descriptions,
            bool[] isIncludeCompanyCodes,
            string[] codePrefixs,
            CodeYearFormart[] codeYears,
            bool[] includeCodeMonths,
            short[] codeSNLengths,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(solutionID, "solutionID");
            ArgumentHelper.AssertGuidNotEmpty(configureKeyIDs, "configureKeyID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionCodeRuleInfo");

                string tempIds = ids.Join();
                string tempConfigureKeys = configureKeyIDs.Join();
                string tempDescriptions = descriptions.Join();
                string tempIsIncludeCompanyCode = isIncludeCompanyCodes.Join();
                string tempCodePrefix = codePrefixs.Join();
                string tempCodeYears = codeYears.Join<CodeYearFormart>();
                string tempIncludeCodeMonth = includeCodeMonths.Join();
                string tempDataVersions = updateDates.Join();
                string tempCodeSNLengths = codeSNLengths.Join();

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@ConfigureKeyIDs", DbType.String, tempConfigureKeys);
                db.AddInParameter(dbCommand, "@Descriptions", DbType.String, tempDescriptions);
                db.AddInParameter(dbCommand, "@IsIncludeCompanyCodes", DbType.String, tempIsIncludeCompanyCode);
                db.AddInParameter(dbCommand, "@CodePrefixs", DbType.String, tempCodePrefix);
                db.AddInParameter(dbCommand, "@CodeYears", DbType.String, tempCodeYears);
                db.AddInParameter(dbCommand, "@CodeMonths", DbType.String, tempIncludeCodeMonth);
                db.AddInParameter(dbCommand, "@CodeSNLengths", DbType.String, tempCodeSNLengths);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变代码规则状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeSolutionCodeRuleState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeSolutionCodeRuleState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 生成单号
        /// </summary>
        /// <param name="companyID">公司</param>
        /// <param name="key">关键字</param>
        /// <param name="generateDate">日期</param>
        /// <returns>返回新生成的单号</returns>
        public string GenerateNO(
            Guid companyID,
            string key,
            DateTime generateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGenerateSN");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@Key", DbType.String, key);
                db.AddInParameter(dbCommand, "@GenerateDate", DbType.DateTime, generateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddOutParameter(dbCommand, "@No", DbType.String, 20);
                db.ExecuteNonQuery(dbCommand);

                string tempNo = (string)db.GetParameterValue(dbCommand, "@No");
                return tempNo;
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
        /// 获取费用代码组列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="category">分类(0:管理成本,1:运输成本,2:业务费用)</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回费用代码组列表</returns>
        private List<ChargingGroupList> GetChargingGroups(
            string code,
            string name,
            ChargeCodeCategory? category,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetChargingGroupList");

                db.AddInParameter(dbCommand, "@Category", DbType.Int16, (short?)category);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ChargingGroupList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new ChargingGroupList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       Code = b.Field<string>("Code"),
                                                       CName = b.Field<string>("CName"),
                                                       EName = b.Field<string>("EName"),
                                                       ParentID = b.IsNull("ParentID") ? Guid.Empty : b.Field<Guid>("ParentID"),
                                                       NodeType = (ChargeGroupNodeType)(short)b.Field<byte>("NodeType"),
                                                       HierarchyCode = b.Field<string>("HierarchyCode"),
                                                       CreateByName = b.Field<string>("CreateByName"),
                                                       CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取费用代码列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="groupID">组ID</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回费用代码列表</returns>
        private List<ChargingCodeList> GetChargingCodes(
            string code,
            string name,
            Guid? groupID,
            bool? isCommission,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetChargingCodeList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@GroupID", DbType.Guid, groupID);
                db.AddInParameter(dbCommand, "@IsCommission", DbType.Boolean, isCommission);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ChargingCodeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new ChargingCodeList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      CName = b.Field<string>("CName"),
                                                      EName = b.Field<string>("EName"),
                                                      IsCommission = b.Field<bool>("IsCommission"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      GroupID = b.Field<Guid>("GroupID"),
                                                      GroupName = b.Field<string>("GroupName"),
                                                      // GroupHierarchyCode = b.Field<string>("HierarchyCode"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
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


        public List<DataDictionaryList> GetCompanyDefaultUnitList(Guid companyID)
        {
            List<DataDictionaryList> list = new List<DataDictionaryList>();

            DataDictionaryList dl = new DataDictionaryList();
            dl.ID = new Guid("38C14B7C-9545-4BD8-84E4-FF75831A625C");
            dl.CName = "运费预付";
            dl.Code = "FKFS08";
            dl.EName = "PP";
            dl.Type = DataDictionaryType.PaymentTerm;
            list.Add(dl);

            dl = new DataDictionaryList();
            dl.ID = new Guid("0ABEF7DD-B2A7-45D3-A3A2-13272F27E418");
            dl.CName = "CTNS";
            dl.Code = "CTNS";
            dl.EName = "CTNS";
            dl.Type = DataDictionaryType.QuantityUnit;
            list.Add(dl);


            dl = new DataDictionaryList();
            dl.ID = new Guid("BB115E86-B078-4CDB-B191-6EA29C9AC782");
            dl.CName = "KGS";
            dl.Code = "KGS";
            dl.EName = "KGS";
            dl.Type = DataDictionaryType.WeightUnit;
            list.Add(dl);

            dl = new DataDictionaryList();
            dl.ID = new Guid("0F8401E6-A649-48D5-93EC-50A6AAB3497C");
            dl.CName = "CBM";
            dl.Code = "CBM";
            dl.EName = "CBM";
            dl.Type = DataDictionaryType.MeasurementUnit;
            list.Add(dl);

            return list;
        }

        /// <summary>
        /// 保存打印帐单配置信息
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="ids">id列表</param>
        /// <param name="titleCNames">中文标题</param>
        /// <param name="titleENames">英文标题</param>
        /// <param name="ctitles">中文签字</param>
        /// <param name="etitles">英文签字</param>
        /// <param name="logoFileNames">公司LOGO图标名</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveConfigureBillInfo(
            Guid configureID,
            Guid?[] ids,
            string[] titleCNames,
            string[] titleENames,
            string[] ctitles,
            string[] etitles,
            string[] logoFileNames,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            ArgumentHelper.AssertArrayLengthMatch(
                ids,
                titleCNames,
                titleENames,
                ctitles,
                etitles,
                logoFileNames,
                updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveConfigureBillInfo");

                string tempIds = ids.Join();
                string tempTitleCName = titleCNames.Join();
                string temptitleENames = titleENames.Join();
                string tempCTitles = ctitles.Join();
                string tempETitles = etitles.Join();
                string tempLogoFileNames = logoFileNames.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@TitleCNames", DbType.String, tempTitleCName);
                db.AddInParameter(dbCommand, "@TitleENames", DbType.String, temptitleENames);
                db.AddInParameter(dbCommand, "@CTitles", DbType.String, tempCTitles);
                db.AddInParameter(dbCommand, "@ETitles", DbType.String, tempETitles);
                db.AddInParameter(dbCommand, "@LogoFileNames", DbType.String, tempLogoFileNames);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData results = db.ManyResult(dbCommand);
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
        /// 获取公司配置的帐单打印配置列表
        /// </summary>
        /// <param name="configureID">公司配置</param>
        /// <returns>返回公司配置的帐单打印配置列表</returns>
        public List<ConfigureBillList> GetConfigureBillList(Guid configureID)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureBillList");

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureBillList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new ConfigureBillList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       ConfigureID = b.Field<Guid>("ConfigureID"),
                                                       TitleCName = b.Field<string>("TitleCName"),
                                                       TitleEName = b.Field<string>("TitleEName"),
                                                       CTitle = b.Field<string>("CTitle"),
                                                       ETitle = b.Field<string>("ETitle"),
                                                       LogoFileName = b.Field<string>("LogoFileName"),
                                                       IsDefault = b.Field<Boolean>("IsDefault"),
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
        /// 获取公司配置的帐单打印配置列表
        /// </summary>
        /// <param name="companyID">公司</param>
        /// <returns>返回公司配置的帐单打印配置列表</returns>
        public List<ConfigureBillList> GetCompanyConfigureBillList(Guid companyID)
        {
            ConfigureInfo info = GetCompanyConfigureInfo(companyID);
            return GetCompanyConfigureBillList(info.ID);
        }

        /// <summary>
        /// 删除公司配置的帐单打印配置
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveConfigureBillInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveConfigureBillInfo");

                string tempIds = ids.Join();
                string tempVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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

        /// <summary>
        /// 设置默认公司配置的帐单打印配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="setByID">设置人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SetDefaultConfigureBill(
            Guid id,
            Guid setByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(setByID, "setByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSetDefaultConfigureBill");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

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
        /// 保存EDI配置信息
        /// </summary>
        /// <param name="configureID">配置ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="serviceConfigureKeyIDs">配置KEY列表</param>
        /// <param name="carrierIDs">船东列表</param>
        /// <param name="uploadModes">上传模式</param>
        /// <param name="serverAddresses">服务器地址</param>
        /// <param name="userNames">用户名</param>
        /// <param name="passwords">密码</param>
        /// <param name="receiveAddresses">接收地址（如果是邮件就是收件人地址，如果是FTP就是路径）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveEDIConfigureInfo(
            Guid configureID,
            Guid?[] ids,
            Guid[] serviceConfigureKeyIDs,
            Guid?[] carrierIDs,
            EDIUploadMode[] uploadModes,
            string[] serverAddresses,
            string[] userNames,
            string[] passwords,
            string[] receiveAddresses,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            ArgumentHelper.AssertArrayLengthMatch(
                ids,
                serviceConfigureKeyIDs,
                carrierIDs,
                uploadModes,
                serverAddresses,
                userNames,
                passwords,
                receiveAddresses,
                updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveConfigureBillInfo");

                //string tempIds = TypeConvertHelper.GuidArraryToString(ids);
                //string tempTitleCName = TypeConvertHelper.StringArraryToString(titleCNames);
                //string temptitleENames = TypeConvertHelper.StringArraryToString(titleENames);
                //string tempCTitles = TypeConvertHelper.StringArraryToString(ctitles);
                //string tempETitles = TypeConvertHelper.StringArraryToString(etitles);
                //string tempLogoFileNames = TypeConvertHelper.StringArraryToString(logoFileNames);
                //string tempVersions = TypeConvertHelper.DateTimeArraryToString(updateDates);

                //db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                //db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                //db.AddInParameter(dbCommand, "@TitleCNames", DbType.String, tempTitleCName);
                //db.AddInParameter(dbCommand, "@TitleENames", DbType.String, temptitleENames);
                //db.AddInParameter(dbCommand, "@CTitles", DbType.String, tempCTitles);
                //db.AddInParameter(dbCommand, "@ETitles", DbType.String, tempETitles);
                //db.AddInParameter(dbCommand, "@LogoFileNames", DbType.String, tempLogoFileNames);
                //db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
                //db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                //db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData results = db.ManyResult(dbCommand);
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
        /// 删除公司EDI配置
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveEDIConfigureInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase();
            //    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveConfigureBillInfo");

            //    string tempIds = TypeConvertHelper.GuidArraryToString(ids);
            //    string tempVersions = TypeConvertHelper.DateTimeArraryToString(updateDates);

            //    db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
            //    db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempVersions);
            //    db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
            //    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            //    db.ExecuteNonQuery(dbCommand);
            //}
            //catch (SqlException sqlException)
            //{
            //    throw new ApplicationException(sqlException.Message);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        /// <summary>
        /// 获取EID配置列表
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回EID配置列表</returns>
        public List<EDIConfigureList> GetEDIConfigureList(
            Guid? serviceConfigureKeyID,
            Guid? carrierID,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetEDIConfigureList");

                db.AddInParameter(dbCommand, "@ServiceConfigureKeyID", DbType.Guid, serviceConfigureKeyID);
                db.AddInParameter(dbCommand, "@CarrierID", DbType.Guid, carrierID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int16, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<EDIConfigureList>();

                List<EDIConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new EDIConfigureList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      Component = b.Field<string>("Component"),
                                                      FTP = b.Field<string>("FTP"),
                                                      FileFormat = b.Field<string>("FileFormat"),
                                                      DataFormat = b.Field<string>("DataFormat"),
                                                      RegularFile = b.Field<string>("RegularFile"),
                                                      StoredProcedure = b.Field<string>("StoredProcedure"),
                                                      ServiceConfigureKeyID = b.Field<Guid>("ServiceConfigureKeyID"),
                                                      ServiceConfigureKeyName = b.Field<string>("ServiceConfigureKeyName"),
                                                      CarrierID = b.Field<Guid?>("CarrierID"),
                                                      CarrierName = b.Field<string>("CarrierName"),
                                                      UploadMode = (EDIUploadMode)b.Field<Byte>("UploadMode"),
                                                      EDIMode = (EDIMode)b.Field<Byte>("Type"),
                                                      ServerAddress = b.Field<string>("ServerAddress"),
                                                      UserName = b.Field<string>("UserName"),
                                                      Password = b.Field<string>("PASSWORD"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      ReceiveAddress = b.Field<string>("ReceiveAddress"),
                                                      ReceiverType = b.Field<byte>("ReceiverType"),
                                                      CreateByID = b.Field<Guid>("CreateByID"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存EDI配置信息
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <param name="configureKeyIDs">配置KEY</param>
        /// <param name="carrierIDs">船公司ID</param>
        /// <param name="modes">上传模式</param>
        /// <param name="serverAddresses">服务器地址</param>
        /// <param name="userNames">帐号</param>
        /// <param name="passwords">密码</param>
        /// <param name="receiveAddresses">反馈地址</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回</returns>
        public ManyResultData SaveEDIConfigureInfo(
            Guid?[] ids,
            //string[] codes,
            string[] components,
            string[] FTPs,
            string[] fileFormats,
            string[] dataFormats,
            string[] regularFiles,
            string[] storedProcedures,
            Guid[] configureKeyIDs,
            Guid?[] carrierIDs,
            EDIUploadMode[] modes,
            EDIMode[] itemType,
            string[] serverAddresses,
            string[] userNames,
            string[] passwords,
            string[] receiveAddresses,
            Guid saveByID,
            byte ReceiverType,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureKeyIDs, "configureKeyIDs");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                string tempIds = ids.Join();
                //string tempCodes = codes.Join();
                string tempComponets = components.Join();
                string tempFTPs = FTPs.Join();
                string tempFileFormats = fileFormats.Join();
                string tempDataFormats = dataFormats.Join();
                string tempRegularFiles = regularFiles.Join();
                string tempStoredProces = storedProcedures.Join();
                string tempServiceConfigureKeyIDs = configureKeyIDs.Join();
                string tempCarrierIDs = carrierIDs.Join();
                string tempUploadModes = modes.Join<EDIUploadMode>();
                string tempServerAddresses = serverAddresses.Join();
                string tempUserNames = userNames.Join();
                string tempPasswords = passwords.Join();
                string tempReceiveAddresses = receiveAddresses.Join();
                string tempUpdateDates = updateDates.Join();
                string tempItemType = itemType.Join<EDIMode>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveEDIConfigureInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                //db.AddInParameter(dbCommand, "@Codes", DbType.String, tempCodes);
                db.AddInParameter(dbCommand, "@Components", DbType.String, tempComponets);
                db.AddInParameter(dbCommand, "@FTPs", DbType.String, tempFTPs);
                db.AddInParameter(dbCommand, "@FileFormats", DbType.String, tempFileFormats);
                db.AddInParameter(dbCommand, "@DataFormats", DbType.String, tempDataFormats);
                db.AddInParameter(dbCommand, "@RegularFiles", DbType.String, tempRegularFiles);
                db.AddInParameter(dbCommand, "@StoredProcedures", DbType.String, tempStoredProces);
                db.AddInParameter(dbCommand, "@ServiceConfigureKeyIDs", DbType.String, tempServiceConfigureKeyIDs);
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, tempCarrierIDs);
                db.AddInParameter(dbCommand, "@UploadModes", DbType.String, tempUploadModes);
                db.AddInParameter(dbCommand, "@EDIType", DbType.String, tempItemType);
                db.AddInParameter(dbCommand, "@ServerAddresses", DbType.String, tempServerAddresses);
                db.AddInParameter(dbCommand, "@UserNames", DbType.String, tempUserNames);
                db.AddInParameter(dbCommand, "@Passwords", DbType.String, tempPasswords);
                db.AddInParameter(dbCommand, "@ReceiveAddresses", DbType.String, tempReceiveAddresses);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@ReceiverType", DbType.Byte, ReceiverType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变EDI配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeEDIConfigureState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeEDIConfiguresState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 获取公司配置下的EDI配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的EDI配置列表</returns>
        public List<CompanyEDIConfigureList> GetCompanyEDIConfigureList(
            Guid configureID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyEDIConfigureList");

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<CompanyEDIConfigureList>();

                List<CompanyEDIConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new CompanyEDIConfigureList
                                                         {
                                                             ConfigureID = b.Field<Guid>("ConfigureID"),
                                                             ID = b.Field<Guid>("ID"),
                                                             ServiceConfigureKeyID = b.Field<Guid>("ServiceConfigureKeyID"),
                                                             ServiceConfigureKeyName = b.Field<string>("ServiceConfigureKeyName"),
                                                             CarrierID = b.Field<Guid?>("CarrierID"),
                                                             CarrierName = b.Field<string>("CarrierName"),
                                                             UploadMode = (EDIUploadMode)b.Field<Byte>("UploadMode"),
                                                             EDIMode = (EDIMode)b.Field<Byte>("Type"),
                                                             ServerAddress = b.Field<string>("ServerAddress"),
                                                             UserName = b.Field<string>("UserName"),
                                                             Password = b.Field<string>("PASSWORD"),
                                                             ReceiveAddress = b.Field<string>("ReceiveAddress"),
                                                             CreateByID = b.Field<Guid>("CreateByID"),
                                                             CreateByName = b.Field<string>("CreateByName"),
                                                             CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 保存公司EDI配置信息
        /// </summary>
        /// <param name="ids">IDS</param>
        /// <param name="configureIDs">公司配置ID</param>
        /// <param name="ediConfigureID">EDI配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveCompanyEDIConfigureInfo(
            Guid configureID,
            Guid[] ediConfigureIDs,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");
            //ArgumentHelper.AssertGuidNotEmpty(ediConfigureIDs, "ediConfigureIDs");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCompanyEDIConfigureInfo");
                string tempEdiConfigureIDs = ediConfigureIDs.Join();
                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@EdiConfigureIDs", DbType.String, tempEdiConfigureIDs);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
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
        /// 删除公司EDI配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveCompanyEDIConfigure(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveCompanyEDIConfigure");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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

        /// <summary>
        /// 保存EDI公司配置信息
        /// </summary>
        /// <param name="ediConfigureID">EDI配置ID</param>
        /// <param name="configureIDs">公司配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveEDICompanyConfigureInfo(
            Guid ediConfigureID,
            Guid[] configureIDs,
            string[] toAddress,
            string[] csclWebURL,
            string[] csclLoginName,
            string[] csclPassword,
            string[] companyAddress,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ediConfigureID, "ediConfigureID");

            try
            {
                string tempconfigureIDs = configureIDs.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveEDICompanyConfigureInfo");

                db.AddInParameter(dbCommand, "@EdiConfigureID", DbType.Guid, ediConfigureID);
                db.AddInParameter(dbCommand, "@ConfigureIDs", DbType.String, tempconfigureIDs);
                db.AddInParameter(dbCommand, "@ToAddress", DbType.String, toAddress.Join());
                db.AddInParameter(dbCommand, "@CSCLWebURL", DbType.String, csclWebURL.Join());
                db.AddInParameter(dbCommand, "@CSCLLoginName", DbType.String, csclLoginName.Join());
                db.AddInParameter(dbCommand, "@CSCLPassword", DbType.String, csclPassword.Join());
                db.AddInParameter(dbCommand, "@CompanyAddress", DbType.String, companyAddress.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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


        public List<ConfigureListForEDI> GetEDICompanyConfigureListByConfigure(Guid ediConfigID)
        {
            ArgumentHelper.AssertGuidNotEmpty(ediConfigID, "ediConfigID");

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetEDICompanyConfigureList");

                db.AddInParameter(dbCommand, "@EDIConfigureID", DbType.Guid, ediConfigID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<ConfigureListForEDI>();
                }

                List<ConfigureListForEDI> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new ConfigureListForEDI
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   CustomerID = b.Field<Guid>("CustomerID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   ShortCode = b.Field<string>("ShortCode"),
                                                   CompanyID = b.Field<Guid>("CompanyID"),
                                                   CompanyName = b.Field<string>("CompanyName"),
                                                   CustomerName = b.Field<string>("CustomerName"),
                                                   AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                                                   ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                                                   IssuePlaceID = b.IsNull("IssuePlaceID") ? Guid.Empty : b.Field<Guid>("IssuePlaceID"),
                                                   IssuePlaceName = b.Field<string>("IssuePlaceName"),
                                                   IsValid = b.Field<bool>("IsValid"),
                                                   CreateByName = b.Field<string>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
                                                   SolutionName = b.Field<string>("SolutionName"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                   ToAddress = b.Field<string>("ToAddress"),
                                                   CSCLWebURL = b.Field<string>("CSCLWebURL"),
                                                   CSCLLoginName = b.Field<string>("CSCLLoginName"),
                                                   CSCLPassword = b.Field<string>("CSCLPassword"),
                                                   CompanyAddress = b.Field<string>("CompanyAddress")
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
        /// 获取公司EDI配置列表
        /// </summary>
        /// <param name="companyID">公司</param>
        /// <returns>返回公司EDI配置列表</returns>
        public List<CompanyEDIConfigureList> GetCompanyEDIConfigureListByCompany(Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyEDIConfigureList");

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<CompanyEDIConfigureList>();
                }

                List<CompanyEDIConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new CompanyEDIConfigureList
                                                         {
                                                             ConfigureID = b.Field<Guid>("ConfigureID"),
                                                             ID = b.Field<Guid>("ID"),
                                                             ServiceConfigureKeyID = b.Field<Guid>("ServiceConfigureKeyID"),
                                                             ServiceConfigureKeyName = b.Field<String>("ServiceConfigureKeyName"),
                                                             CarrierID = b.Field<Guid?>("CarrierID"),
                                                             CarrierName = b.Field<String>("CarrierName"),
                                                             UploadMode = (EDIUploadMode)b.Field<Byte>("UploadMode"),
                                                             EDIMode = (EDIMode)b.Field<Byte>("Type"),
                                                             ServerAddress = b.Field<String>("ServerAddress"),
                                                             UserName = b.Field<String>("UserName"),
                                                             Password = b.Field<String>("PASSWORD"),
                                                             ReceiveAddress = b.Field<String>("ReceiveAddress"),
                                                             CreateByID = b.Field<Guid>("CreateByID"),
                                                             CreateByName = b.Field<String>("CreateByName"),
                                                             CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获取报表类型列表
        /// </summary>
        /// <returns></returns>
        public List<ReportType> GetReportTypes()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetEnumListForType");

                db.AddInParameter(dbCommand, "@EnumTypeName", DbType.String, "ReportType");
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<ReportType>();
                }

                List<ReportType> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new ReportType
                                            {
                                                Index = (int)b.Field<byte>("Value"),
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
        /// 获取报表配置列表
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回报表配置列表</returns>
        public List<ReportConfigureList> GetReportConfigureList(string code,
        string cDescription,
        string eDescription,
        int? reportTypeID,
        bool? isValid,
        int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetReportList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cDescription);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eDescription);
                db.AddInParameter(dbCommand, "@ReportType", DbType.Int16, reportTypeID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<ReportConfigureList>();
                }

                List<ReportConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new ReportConfigureList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Code = b.Field<String>("Code"),
                                                         CDescription = b.Field<String>("CName"),
                                                         EDescription = b.Field<String>("EName"),
                                                         ReportTypeID = (int?)b.Field<byte>("Type"),
                                                         ReportTypeName = b.Field<String>("TypeName"),
                                                         //Type = (ReportType)b.Field<byte>("Type"),
                                                         IsValid = b.Field<bool>("IsValid"),
                                                         CreateByID = b.Field<Guid>("CreateBy"),
                                                         CreateByName = b.Field<String>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         Parameters = b.IsNull("ParametersDefine") ? new List<ReportParameterList>() : (from d in SerializerHelper.DeserializeFromString<ReportItem>(typeof(ReportItem), b.Field<string>("ParametersDefine")).Parameters.Parameters
                                                                                                                                        select new ReportParameterList
                                                                                                                                        {
                                                                                                                                            Code = d.Code,
                                                                                                                                            CDescription = d.CDescription,
                                                                                                                                            EDescription = d.EDescription,
                                                                                                                                            CreateByID = d.CreateByID,
                                                                                                                                            CreateByName = d.CreateByName,
                                                                                                                                            CreateDate = d.CreateDate,
                                                                                                                                            ID = (d.ID == null || d.ID == Guid.Empty) ? Guid.NewGuid() : d.ID,
                                                                                                                                            ParameterType = d.ParameterType,
                                                                                                                                            ParameterValue = d.ParameterValue,
                                                                                                                                            ReportID = d.ReportID
                                                                                                                                        }).ToList()
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
        /// 保存报表配置信息
        /// </summary>
        /// <param name="id">ID列表</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名称</param>
        /// <param name="eName">英文名称</param>
        /// <param name="description">描述</param>
        /// <param name="fileName">报表文件</param>
        /// <param name="parameter">参数</param>
        /// <param name="type">报表类型</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveReportConfigureInfo(
            Guid? id,
            string code,
            string cDescription,
            string eDescription,
            string reportParameters,
            int? reportTypeID,
            Guid saveByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveReportConfigureInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cDescription);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eDescription);
                db.AddInParameter(dbCommand, "@Parameter", DbType.String, reportParameters);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, reportTypeID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 改变报表配置状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeReportConfigureState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeReportConfigureState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 获取单个报表配置，根据公司ID和报表配置的Code(string型，完全匹配)
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="reportCode"></param>
        /// <returns></returns>
        public CompanyReportConfigureList GetReportConfigureList(Guid companyID, string reportCode)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            ArgumentHelper.AssertStringNotEmpty(reportCode, "reportCode");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetReportConfigure");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@ReportCode", DbType.String, reportCode);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return null;

                CompanyReportConfigureList result = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CompanyReportConfigureList
                                                            {
                                                                ConfigureID = b.Field<Guid>("ConfigureID"),
                                                                ID = b.Field<Guid>("ID"),
                                                                Code = b.Field<String>("Code"),
                                                                CDescription = b.Field<String>("CName"),
                                                                EDescription = b.Field<String>("EName"),
                                                                ReportTypeID = (int?)b.Field<byte>("Type"),
                                                                IsValid = b.Field<bool>("IsValid"),
                                                                CreateByID = b.Field<Guid>("CreateByID"),
                                                                CreateByName = b.Field<String>("CreateByName"),
                                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                Parameters = b.IsNull("ParametersValue") ? new List<ReportParameterList>() : (from d in SerializerHelper.DeserializeFromString<ReportItem>(typeof(ReportItem), b.Field<string>("ParametersValue")).Parameters.Parameters
                                                                                                                                              select new ReportParameterList
                                                                                                                                              {
                                                                                                                                                  Code = d.Code,
                                                                                                                                                  CDescription = d.CDescription,
                                                                                                                                                  EDescription = d.EDescription,
                                                                                                                                                  CreateByID = d.CreateByID,
                                                                                                                                                  CreateByName = d.CreateByName,
                                                                                                                                                  CreateDate = d.CreateDate,
                                                                                                                                                  ID = (d.ID == null || d.ID == Guid.Empty) ? Guid.NewGuid() : d.ID,
                                                                                                                                                  ParameterType = d.ParameterType,
                                                                                                                                                  ParameterValue = string.IsNullOrEmpty(d.ParameterValue) ? string.Empty : d.ParameterValue,
                                                                                                                                                  ReportID = d.ReportID
                                                                                                                                              }).ToList()
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
        /// 获取公司配置下的报表配置列表
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司配置下的报表配置列表</returns>
        public List<CompanyReportConfigureList> GetCompanyReportConfigureList(
            Guid configureID,
            bool? isValid)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyReportConfigureList");

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<CompanyReportConfigureList>();

                List<CompanyReportConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                                            select new CompanyReportConfigureList
                                                            {
                                                                ConfigureID = b.Field<Guid>("ConfigureID"),
                                                                ID = b.Field<Guid>("ID"),
                                                                Code = b.Field<String>("Code"),
                                                                CDescription = b.Field<String>("CName"),
                                                                EDescription = b.Field<String>("EName"),
                                                                ReportTypeID = (int?)b.Field<byte>("Type"),
                                                                IsValid = b.Field<bool>("IsValid"),
                                                                CreateByID = b.Field<Guid>("CreateByID"),
                                                                CreateByName = b.Field<String>("CreateByName"),
                                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                Parameters = b.IsNull("ParametersValue") ? new List<ReportParameterList>() : (from d in SerializerHelper.DeserializeFromString<ReportItem>(typeof(ReportItem), b.Field<string>("ParametersValue")).Parameters.Parameters
                                                                                                                                              select new ReportParameterList
                                                                                                                                              {
                                                                                                                                                  Code = d.Code,
                                                                                                                                                  CDescription = d.CDescription,
                                                                                                                                                  EDescription = d.EDescription,
                                                                                                                                                  CreateByID = d.CreateByID,
                                                                                                                                                  CreateByName = d.CreateByName,
                                                                                                                                                  CreateDate = d.CreateDate,
                                                                                                                                                  ID = (d.ID == null || d.ID == Guid.Empty) ? Guid.NewGuid() : d.ID,
                                                                                                                                                  ParameterType = d.ParameterType,
                                                                                                                                                  //ParameterValue = (d.ParameterType == ReportParameterType.Bool) ? (string.IsNullOrEmpty(d.ParameterValue) ? false : (d.ParameterValue.ToUpper() == "TRUE" ? true : false)) : (string.IsNullOrEmpty(d.ParameterValue) ? string.Empty : d.ParameterValue.Replace("\n", "\r\n")),
                                                                                                                                                  ParameterValue = string.IsNullOrEmpty(d.ParameterValue) ? string.Empty : d.ParameterValue,
                                                                                                                                                  ReportID = d.ReportID
                                                                                                                                              }).ToList()
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
        /// 保存公司报表配置信息
        /// </summary>
        /// <param name="configureID">公司配置ID</param>
        /// <param name="reportParameters">报表参数</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveCompanyReportConfigureInfo(
            Guid configureID,
            string reportParameters,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(configureID, "configureID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                //System.Text.StringBuilder tempReportParameters = new System.Text.StringBuilder();
                //for (int i = 0; i < reportParameters.Length; i++)
                //{
                //    for (int j = 0; j < reportParameters[i].Length; j++)
                //    {
                //        if (tempReportParameters.Length > 0)
                //        {
                //            tempReportParameters.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.DividedSymbol);
                //        }

                //        tempReportParameters.Append(ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<ReportParameterList>(reportParameters[i][j]));
                //    }
                //}



                // string xmlcontent = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<ReportParameterCollect>(reportParameters, true, true);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCompanyReportConfigureInfo");

                db.AddInParameter(dbCommand, "@ConfigureID", DbType.Guid, configureID);
                db.AddInParameter(dbCommand, "@Parameters", DbType.Xml, reportParameters);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 保存报表公司配置信息
        /// </summary>
        /// <param name="reportConfigureID">报表配置ID</param>
        /// <param name="reportParameters">公司配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回ManyResult</returns>
        public ManyResultData SaveReportCompanyConfigureInfo(
             Guid reportConfigureID,
             string reportParameters,
             Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(reportConfigureID, "reportConfigureID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveReportCompanyConfigureInfo");

                db.AddInParameter(dbCommand, "@ReportID", DbType.Guid, reportConfigureID);
                db.AddInParameter(dbCommand, "@Parameters", DbType.String, reportParameters);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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
        /// 删除公司报表配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveReportConfigure(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveReportEDIConfigure");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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

        /// <summary>
        /// 获取报表所挂公司列表
        /// </summary>
        /// <param name="reportID">报表ID</param>
        /// <returns>返回报表所挂公司列表</returns>
        public List<ReportCompanyConfigureList> GetReportCompanyConfigureList(Guid reportID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetReportCompanyConfigureList");

                db.AddInParameter(dbCommand, "@ReportID", DbType.Guid, reportID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, 0);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ReportCompanyConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                                            select new ReportCompanyConfigureList
                                                            {
                                                                ID = b.Field<Guid>("ID"),
                                                                ShortCode = b.Field<String>("ShortCode"),
                                                                CompanyID = b.Field<Guid>("CompanyID"),
                                                                CompanyName = b.Field<String>("CompanyName"),
                                                                CustomerID = b.Field<Guid>("CustomerID"),
                                                                CustomerName = b.Field<String>("CustomerName"),
                                                                StandardCurrency = b.Field<String>("StandardCurrency"),
                                                                DefaultCurrency = b.Field<String>("DefaultCurrency"),
                                                                ChargingClosingdate = b.Field<DateTime?>("ChargingClosingDate"),
                                                                AccountingClosingdate = b.Field<DateTime?>("AccountingClosingDate"),
                                                                IssuePlaceID = b.Field<Guid?>("IssuePlaceID"),
                                                                IssuePlaceName = b.Field<String>("IssuePlaceName"),
                                                                IsValid = b.Field<Boolean>("IsValid"),
                                                                CreateByName = b.Field<String>("CreateByName"),
                                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                                SolutionID = b.Field<Guid>("SolutionID"),
                                                                SolutionName = b.Field<String>("SolutionName"),
                                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                                Parameters = b.IsNull("ParametersDefine") ? new List<ReportParameterList>() : (from d in SerializerHelper.DeserializeFromString<ReportItem>(typeof(ReportItem), b.Field<string>("ParametersDefine")).Parameters.Parameters
                                                                                                                                               select new ReportParameterList
                                                                                                                                               {
                                                                                                                                                   Code = d.Code,
                                                                                                                                                   CDescription = d.CDescription,
                                                                                                                                                   EDescription = d.EDescription,
                                                                                                                                                   CreateByID = d.CreateByID,
                                                                                                                                                   CreateByName = d.CreateByName,
                                                                                                                                                   CreateDate = d.CreateDate,
                                                                                                                                                   ID = (d.ID == null || d.ID == Guid.Empty) ? Guid.NewGuid() : d.ID,
                                                                                                                                                   ParameterType = d.ParameterType,
                                                                                                                                                   ParameterValue = string.IsNullOrEmpty(d.ParameterValue) ? string.Empty : d.ParameterValue,
                                                                                                                                                   ReportID = d.ReportID
                                                                                                                                               }).ToList()
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
        /// 获取公司配置下对应的客户列表
        /// </summary>
        public List<ConfigureCustomerList> GetConfigureCustomerList()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureCustomerList");
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureCustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new ConfigureCustomerList
                                                            {
                                                                CName = b.Field<string>("CName"),
                                                                Code = b.Field<string>("Code"),
                                                                CShortName = b.Field<string>("CShortName"),
                                                                EName = b.Field<string>("EName"),
                                                                EShortName = b.Field<string>("EShortName"),
                                                                ID = b.Field<Guid>("ID"),
                                                                CompanyID = b.Field<Guid>("CompanyID"),
                                                                ConfigureID = b.Field<Guid>("ConfigureID"),
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
        /// 
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="solutionID">解决方案</param>
        /// <returns>返回公司配置列表</returns>
        public List<ConfigureList> GetCompanyForReportParameterIsUsed(
            Guid reportID,
            Guid reportParameterID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCompanyForReportParameterIsUsed");

                db.AddInParameter(dbCommand, "@ReportID", DbType.Guid, reportID);
                db.AddInParameter(dbCommand, "@ReportParameterID", DbType.Guid, reportParameterID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureList> results = (from b in ds.Tables[0].AsEnumerable()
                                               select new ConfigureList
                                               {
                                                   CompanyID = b.Field<Guid>("CompanyID"),
                                                   CompanyName = b.Field<string>("CompanyName"),
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
        /// 获得EDI配置列表
        /// </summary>
        /// <returns></returns>
        public List<EDIDictCodeList> GetEDIDictCodeList(EDIDicType? ediDicType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetEDIDictCodeList");

                db.AddInParameter(dbCommand, "@Type", DbType.Byte, ediDicType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<EDIDictCodeList> list = (from d in ds.Tables[0].AsEnumerable()
                                              select new EDIDictCodeList
                                              {
                                                  ID = d.Field<Guid>("ID"),
                                                  AMSCode = d.Field<String>("AMSCode"),
                                                  AMSName = d.Field<String>("AMSName"),
                                                  ACICode = d.Field<String>("ACICode"),
                                                  ISOCode = d.Field<String>("ISOCode"),
                                                  CreateDate = d.Field<DateTime>("CreateDate")
                                              }).ToList();

                return list;
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

        #region 会计科目_New
        /// <summary>
        /// 保存会计科目配置
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名</param>
        /// <param name="ename">英文名</param>
        /// <param name="type">类型</param>
        /// <param name="ledgerStyle">账页格式</param>
        /// <param name="gLCodeProperty">余额方向</param>
        /// <param name="isForeignCheck">是否外币核算</param>
        /// <param name="foreignCurrencyID">币种</param>
        /// <param name="isNumberCheck">是否数量核算</param>
        /// <param name="unitName">计量单位</param>
        /// <param name="isDepartmentCheck">部门核算</param>
        /// <param name="isPersonalCheck">个人核算</param>
        /// <param name="isCustomerCheck">客户往来</param>
        /// <param name="isJournal">日记帐</param>
        /// <param name="isBankAccount">银行帐</param>
        /// <param name="isFee">是否为费用</param>
        /// <param name="years">年份</param>
        /// <param name="parentID">父级ID</param>
        /// <param name="description">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <returns></returns>
        public SingleResult SaveSolutionGLCodeInfoNew(
            Guid solutionID,
            Guid id,
            string code,
            string cname,
            string ename,
            GLCodeType type,
            GLCodeLedgerStyle ledgerStyle,
            GLCodeProperty gLCodeProperty,
            bool isForeignCheck,
            Guid? foreignCurrencyID,
            bool isNumberCheck,
            string unitName,
            bool isDepartmentCheck,
            bool isPersonalCheck,
            bool isCustomerCheck,
            bool isJournal,
            bool isBankAccount,
            bool? isFee,
            Guid? parentID,
            string description,
            Guid saveByID,
            DateTime? updateDate,
            Guid? companyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveSolutionGLCodeInfonNew");


                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cname);
                db.AddInParameter(dbCommand, "@EName", DbType.String, ename);
                db.AddInParameter(dbCommand, "@GLCodetype", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@LedgerStyle", DbType.Byte, ledgerStyle);
                db.AddInParameter(dbCommand, "@GLCodeProperty", DbType.Byte, gLCodeProperty);
                db.AddInParameter(dbCommand, "@IsForeignCheck", DbType.Boolean, isForeignCheck);
                db.AddInParameter(dbCommand, "@ForeignCurrencyID", DbType.Guid, foreignCurrencyID); ;
                db.AddInParameter(dbCommand, "@IsNumberCheck", DbType.Boolean, isNumberCheck);
                db.AddInParameter(dbCommand, "@UnitName", DbType.String, unitName);
                db.AddInParameter(dbCommand, "@IsDepartmentCheck", DbType.Boolean, isDepartmentCheck);
                db.AddInParameter(dbCommand, "@IsPersonalCheck", DbType.Boolean, isPersonalCheck);
                db.AddInParameter(dbCommand, "@IsCustomerCheck", DbType.Boolean, isCustomerCheck);
                db.AddInParameter(dbCommand, "@IsJournal", DbType.Boolean, isJournal);
                db.AddInParameter(dbCommand, "@IsBankAccount", DbType.Boolean, isBankAccount);
                db.AddInParameter(dbCommand, "@IsFee", DbType.Boolean, isFee);
                db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, description);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "HierarchyCode", "LevelCode" });

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
        /// 获得会计科目详细设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public SolutionGLCodeList GetSolutionGLCodeInfoNew(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLCodeInfoNew");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                SolutionGLCodeList result = (from b in ds.Tables[0].AsEnumerable()
                                             select new SolutionGLCodeList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   Code = b.Field<String>("Code"),
                                                   CName = b.Field<String>("CName"),
                                                   EName = b.Field<String>("EName"),
                                                   FullName = b.Field<String>("FullName"),
                                                   EfullName = b.Field<String>("FullEName"),
                                                   GLCodeType = (GLCodeType)b.Field<Byte>("GLCodeType"),
                                                   LedgerStyle = (GLCodeLedgerStyle)b.Field<Byte>("LedgerStyle"),
                                                   IsForeignCheck = b.Field<Boolean>("IsForeignCheck"),
                                                   ForeignCurrencyID = b.Field<Guid?>("ForeignCurrencyID"),
                                                   IsNumberCheck = b.Field<Boolean>("IsNumberCheck"),
                                                   UnitName = b.Field<String>("UnitName"),
                                                   GLCodeProperty = (GLCodeProperty)b.Field<Byte>("GLCodeProperty"),
                                                   IsDepartmentCheck = b.Field<Boolean>("IsDepartmentCheck"),
                                                   IsPersonalCheck = b.Field<Boolean>("IsPersonalCheck"),
                                                   IsCustomerCheck = b.Field<Boolean>("IsCustomerCheck"),
                                                   IsJournal = b.Field<Boolean>("IsJournal"),
                                                   IsBankAccount = b.Field<Boolean>("IsBankAccount"),
                                                   ParentID = b.Field<Guid?>("ParentID"),
                                                   ParentName = b.Field<String>("ParentName"),
                                                   Description = b.Field<String>("Description"),
                                                   ForeignCurrencyName = b.Field<String>("ForeignCurrencyName"),
                                                   HierarchyCode = b.Field<String>("HierarchyCode"),
                                                   LevelCode = b.Field<Int32?>("LevelCode"),
                                                   IsValid = b.Field<Boolean>("IsValid"),
                                                   IsFee = b.Field<Boolean?>("IsFee"),
                                                   CreateByID = b.Field<Guid>("CreateByID"),
                                                   CreateByName = b.Field<String>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
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
        /// 获得会计科目列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="isValid">有效性</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<SolutionGLCodeList> GetSolutionGLCodeListNew(
                                    Guid solutionID,
                                    Guid[] companyIds,
                                    string code,
                                    string name,
                                    GLCodeType type,
                                    bool? isValid,
                                    bool isEnglish)
        {
            return GetSolutionGLCodeListNew(
                                    solutionID,
                                    companyIds,
                                    code,
                                    name,
                                    type,
                                    isValid,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    isEnglish);
        }
        /// <summary>
        /// 获得会计科目列表
        /// </summary>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="isValid">有效性</param>
        /// <param name="isDepartmentCheck">部门核算</param>
        /// <param name="isPersonalCheck">个人往来</param>
        /// <param name="isCustomerCheck">客户往来</param>
        /// <param name="isJournal">日记账</param>
        /// <param name="isBankAccount">银行帐</param>
        /// <param name="isFee">流程报销费用</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<SolutionGLCodeList> GetSolutionGLCodeListNew(
                                    Guid solutionID,
                                    Guid[] companyIds,
                                    string code,
                                    string name,
                                    GLCodeType type,
                                    bool? isValid,
                                    bool? isDepartmentCheck,
                                    bool? isPersonalCheck,
                                    bool? isCustomerCheck,
                                    bool? isJournal,
                                    bool? isBankAccount,
                                    bool? isFee,
                                    bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLCodeListNew");

                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsDepartmentCheck", DbType.Boolean, isDepartmentCheck);
                db.AddInParameter(dbCommand, "@IsPersonalCheck", DbType.Boolean, isPersonalCheck);
                db.AddInParameter(dbCommand, "@IsCustomerCheck", DbType.Boolean, isCustomerCheck);
                db.AddInParameter(dbCommand, "@IsJournal", DbType.Boolean, isJournal);
                db.AddInParameter(dbCommand, "@IsBankAccount", DbType.Boolean, isBankAccount);
                db.AddInParameter(dbCommand, "@IsFee", DbType.Boolean, isFee);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLCodeList> result = TransformGLCodeList(ds);

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
        private List<SolutionGLCodeList> TransformGLCodeList(DataSet ds)
        {
            List<SolutionGLCodeList> result = (from b in ds.Tables[0].AsEnumerable()
                                               select new SolutionGLCodeList
                                               {
                                                   ID = b.Field<Guid>("ID"),
                                                   SolutionID = b.Field<Guid>("SolutionID"),
                                                   Code = b.Field<String>("Code"),
                                                   CName = b.Field<String>("CName"),
                                                   EName = b.Field<String>("EName"),
                                                   FullName = b.Field<String>("FullName"),
                                                   EfullName = b.Field<String>("FullEName"),
                                                   GLCodeType = (GLCodeType)b.Field<Byte>("GLCodeType"),
                                                   LedgerStyle = (GLCodeLedgerStyle)b.Field<Byte>("LedgerStyle"),
                                                   IsForeignCheck = b.Field<Boolean>("IsForeignCheck"),
                                                   ForeignCurrencyID = b.Field<Guid?>("ForeignCurrencyID"),
                                                   IsNumberCheck = b.Field<Boolean>("IsNumberCheck"),
                                                   UnitName = b.Field<String>("UnitName"),
                                                   GLCodeProperty = (GLCodeProperty)b.Field<Byte>("GLCodeProperty"),
                                                   IsDepartmentCheck = b.Field<Boolean>("IsDepartmentCheck"),
                                                   IsPersonalCheck = b.Field<Boolean>("IsPersonalCheck"),
                                                   IsCustomerCheck = b.Field<Boolean>("IsCustomerCheck"),
                                                   IsJournal = b.Field<Boolean>("IsJournal"),
                                                   IsBankAccount = b.Field<Boolean>("IsBankAccount"),
                                                   ParentID = b.Field<Guid?>("ParentID"),
                                                   ParentName = b.Field<String>("ParentName"),
                                                   Description = b.Field<String>("Description"),
                                                   ForeignCurrencyName = b.Field<String>("ForeignCurrencyName"),
                                                   HierarchyCode = b.Field<String>("HierarchyCode"),
                                                   LevelCode = b.Field<Int32?>("LevelCode"),
                                                   IsValid = b.Field<Boolean>("IsValid"),
                                                   IsFee = b.Field<Boolean?>("IsFee"),
                                                   CreateByID = b.Field<Guid>("CreateByID"),
                                                   CreateByName = b.Field<String>("CreateByName"),
                                                   CreateDate = b.Field<DateTime>("CreateDate"),
                                                   UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                   IsLeaf = Convert.ToInt32(b.Field<Int32>("Total")) == 0 ? true : false,
                                                   CompanyID = b.Field<Guid?>("CompanyID"),
                                               }).ToList();

            return result;
        }

        private List<GL2COMPANY> TransformGLCompanyList(DataSet ds)
        {
            List<GL2COMPANY> result = (from b in ds.Tables[0].AsEnumerable()
                                       select new GL2COMPANY
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              GLID = b.Field<Guid>("GLID"),
                                              Code = b.Field<String>("Code"),
                                              CompanyID = b.Field<Guid>("CompanyID")
                                          }).ToList();

            return result;
        }

        /// <summary>
        /// 通过科目ID获得所属公司列表
        /// </summary>
        /// <param name="GLID"></param>
        /// <returns></returns>
        public List<GL2COMPANY> GetGLOrgbyId(Guid GLID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand("select * from pub.GL2COMPANY where GLID = @SolutionID");
                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, GLID);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<GL2COMPANY> result = TransformGLCompanyList(ds);

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
        /// 保存科目对应公司
        /// </summary>
        /// <param name="GLID"></param>
        /// <returns></returns>
        public void SaveGL2Company(Guid[] IDs, Guid GLID, Guid[] Coms, string[] Codes, Guid createBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveGL2Company");

                dbCommand.Parameters.Clear();
                db.AddInParameter(dbCommand, "@Id", DbType.String, IDs.Join());
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, GLID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.String, Coms.Join());
                db.AddInParameter(dbCommand, "@code", DbType.String, Codes.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, createBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, LocalData.IsEnglish);

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
        /// 删除科目对应公司
        /// </summary>
        /// <param name="GLID"></param>
        /// <returns></returns>
        public void DelGL2Company(Guid[] IDs, Guid createBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspDelGL2Company");

                dbCommand.Parameters.Clear();
                db.AddInParameter(dbCommand, "@ID", DbType.String, IDs.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, createBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, LocalData.IsEnglish);

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
        /// 根据ID集合获得科目列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<SolutionGLCodeList> GetSolutionGLCodeListByIds(Guid[] ids, Guid[] companyIds, Guid solutionID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLCodeListByIds");


                db.AddInParameter(dbCommand, "@SolutionID", DbType.Guid, solutionID);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLCodeList> result = TransformGLCodeList(ds);

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
        /// 是否为叶子节点的会计科目
        /// </summary>
        /// <param name="id">会计科目ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns>bool</returns>
        public bool IsLeafGLCode(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetSolutionGLCodeByParentID");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables[0].Rows.Count < 1)
                    return true;
                else
                    return false;
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
        /// 通过公司ID获得所有isfee=1的会计科目
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>        
        public List<SolutionGLCodeList> GetFeeGLCodeList(Guid companyID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetFeeGLCodes");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<SolutionGLCodeList> result = (from b in ds.Tables[0].AsEnumerable()
                                                   select new SolutionGLCodeList
                                                   {
                                                       ID = b.Field<Guid>("ID"),
                                                       SolutionID = b.Field<Guid>("SolutionID"),
                                                       Code = b.Field<String>("Code"),
                                                       CName = b.Field<String>("CName"),
                                                       EName = b.Field<String>("EName"),
                                                       FullName = b.Field<String>("FullName"),
                                                       EfullName = b.Field<String>("FullEName"),
                                                       GLCodeType = (GLCodeType)b.Field<Byte>("GLCodeType"),
                                                       LedgerStyle = (GLCodeLedgerStyle)b.Field<Byte>("LedgerStyle"),
                                                       IsForeignCheck = b.Field<Boolean>("IsForeignCheck"),
                                                       ForeignCurrencyID = b.Field<Guid?>("ForeignCurrencyID"),
                                                       IsNumberCheck = b.Field<Boolean>("IsNumberCheck"),
                                                       UnitName = b.Field<String>("UnitName"),
                                                       GLCodeProperty = (GLCodeProperty)b.Field<Byte>("GLCodeProperty"),
                                                       IsDepartmentCheck = b.Field<Boolean>("IsDepartmentCheck"),
                                                       IsPersonalCheck = b.Field<Boolean>("IsPersonalCheck"),
                                                       IsCustomerCheck = b.Field<Boolean>("IsCustomerCheck"),
                                                       IsJournal = b.Field<Boolean>("IsJournal"),
                                                       IsBankAccount = b.Field<Boolean>("IsBankAccount"),
                                                       ParentID = b.Field<Guid?>("ParentID"),
                                                       Description = b.Field<String>("Description"),
                                                       LevelCode = b.Field<Int32?>("LevelCode"),
                                                       IsValid = b.Field<Boolean>("IsValid"),
                                                       IsFee = b.Field<Boolean?>("IsFee"),
                                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                       GroupByID = b.Field<Guid>("GroupID"),
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

        #region 获得指定时间的汇率
        public decimal GetCompanyStandardCurrencyRate(Guid companyID, Guid sourceCurrencyID, DateTime date)
        {
            ConfigureInfo info = GetCompanyConfigureInfo(companyID);

            Guid targetCurrencyID = info.StandardCurrencyID;

            List<SolutionExchangeRateList> rateList = GetCompanyExchangeRateList(companyID, true);

            if (sourceCurrencyID == Guid.Empty || targetCurrencyID == Guid.Empty)
            {
                return 0m;
            }
            if (sourceCurrencyID == targetCurrencyID)
            {
                return 1;
            }
            SolutionExchangeRateList inRate = rateList.Find(delegate(SolutionExchangeRateList item)
            {
                return
                  item.SourceCurrencyID == sourceCurrencyID && item.TargetCurrencyID == targetCurrencyID
                  && date.Date >= item.FromDate.Date && date.Date <= item.ToDate.Date;
            });

            if (inRate != null)
            {
                return inRate.Rate;
            }

            SolutionExchangeRateList outRate = rateList.Find(delegate(SolutionExchangeRateList item)
            {
                return
                  item.SourceCurrencyID == targetCurrencyID && item.TargetCurrencyID == sourceCurrencyID
                        && date.Date >= item.FromDate && date.Date <= item.ToDate;
            });

            if (outRate != null)
            {
                //汇率只保留7位小数,跟数据库中设置一致
                return decimal.Round((1 / outRate.Rate), 7, MidpointRounding.AwayFromZero);
            }
            return 0m;
        }
        #endregion


        /// <summary>
        /// 得到前5项用户常用菜单名称
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<GetTop5MenuOfCommandNameViaUserID> GetTop5Menu(Guid UserID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[pub].[UspGetCommandNameAndFunctionNameViaUserID]");
                db.AddInParameter(dbCommand, "@CurrentUserID", DbType.Guid, UserID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<GetTop5MenuOfCommandNameViaUserID> list = (from d in ds.Tables[0].AsEnumerable()
                                                                select new GetTop5MenuOfCommandNameViaUserID
                                                                {
                                                                    FunctionName = d.Field<string>("OperationContent"),
                                                                    Count = d.Field<int>("CountRows"),
                                                                }).ToList();

                return list;
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
        /// 获取用户上一次更新密码时间
        /// ReadWrite=0 获取读取上次修改时间
        /// ReadWrite=1 更新修改时间
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ReadWrite"></param>
        /// <returns></returns>
        public List<GetUserPasswordUpdate> UserPasswordUpdate(Guid UserID, Int16 ReadWrite)
        {
            try
            {
                List<GetUserPasswordUpdate> update = null;

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[sm].[UserPasswordUpdate]");
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, UserID);
                db.AddInParameter(dbCommand, "@ReadWrite", DbType.Int16, ReadWrite);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ReadWrite == 0)//读取密上一次密码修改时间
                {
                    if (ds == null || ds.Tables.Count < 1)
                    {
                        return null;
                    }
                    update = (from d in ds.Tables[0].AsEnumerable()
                              select new GetUserPasswordUpdate
                                  {
                                      updatetime = d.Field<DateTime>("PasswordUpdate"),
                                  }).ToList();
                }
                return update;
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
