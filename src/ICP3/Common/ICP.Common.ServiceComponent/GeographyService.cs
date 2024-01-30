//-----------------------------------------------------------------------
// <copyright file="GeographyService.cs" company="LongWin">
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

    /// <summary>
    /// 国家，省份，地点信息维护
    /// </summary>
    public class GeographyService : IGeographyService
    {
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;

        public GeographyService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
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
                //bool isEnglish = false;
                //bool.TryParse(_sessionService.CurrentSession[ICP.Framework.CommonLibrary.Server.ServerVariables.CULTURE_ID].ToString(), out isEnglish);
                //return isEnglish;
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
        /// 获取国家列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回国家列表</returns>
        public List<CountryList> GetCountryList(
            string code,
            string name,
            bool? isValid,
            int maxRecords)
        {
            return GetCountryListByFCM(code, name, isValid, this.IsEnglish, maxRecords);
        }

        /// <summary>
        /// 获得国家列--区分中英文
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="isValid"></param>
        /// <param name="isEnglish"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        public List<CountryList> GetCountryListByFCM(
             string code,
             string name,
             bool? isValid,
             bool isEnglish,
             int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCountryList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CountryList> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new CountryList
                                             {
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
        /// 获取国家信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回国家信息</returns>
        public CountryInfo GetCountryInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCountryInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CountryInfo result = (from b in ds.Tables[0].AsEnumerable()
                                      select new CountryInfo
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
        /// 保存国家信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveCountryInfo(
            Guid? id,
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCountryInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
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
        /// 改变国家有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData ChangeCountryState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeCountryState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryId">国家</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回省份列表</returns>
        public List<CountryProvinceList> GetCountryProvinceList(
            string code,
            string name,
            Guid? countryId,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCountryProvinceList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@CountryId", DbType.Guid, countryId);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CountryProvinceList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CountryProvinceList
                                              {
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  ParentID = b.IsNull("ParentID") ? (Guid?)null : b.Field<Guid>("ParentID"),
                                                  ParentName = b.Field<string>("ParentName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  Type = (CountryProvinceType)(short)b.Field<byte>("Type"),
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
        /// 获取省份信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回省份信息</returns>
        public CountryProvinceInfo GetProvinceInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetProvinceInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CountryProvinceInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new CountryProvinceInfo
                                       {
                                           CName = b.Field<string>("CName"),
                                           Code = b.Field<string>("Code"),
                                           ParentID = b.Field<Guid>("CountryID"),
                                           ParentName = b.Field<string>("CountryName"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           Type = CountryProvinceType.Province,
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
        /// 保存省份信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="countryId">国家</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveProvinceInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid countryId,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertGuidNotEmpty(countryId, "countryId");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveProvinceInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@CountryId", DbType.Guid, countryId);
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
        /// 改变省份有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData ChangeProvinceState(
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeProvinceState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeById", DbType.Guid, changeById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
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
        /// <summary>
        /// 获取地点列表
        /// </summary>
        /// <param name="codeOrName"></param>
        /// <param name="countryId"></param>
        /// <param name="countryName"></param>
        /// <param name="provinceId"></param>
        /// <param name="isOcean"></param>
        /// <param name="isAir"></param>
        /// <param name="isOther"></param>
        /// <param name="isValid"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
       public List<LocationList> GetLocationList(string codeOrName, Guid? countryId, string countryName, Guid? provinceId, bool? isOcean, bool? isAir, bool? isOther, bool? isValid, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetLocationList");

                //db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, codeOrName);
                db.AddInParameter(dbCommand, "@CountryId", DbType.Guid, countryId);
                db.AddInParameter(dbCommand, "@CountryName", DbType.String, countryName);
                db.AddInParameter(dbCommand, "@ProvinceId", DbType.Guid, provinceId);
                db.AddInParameter(dbCommand, "@IsOcean", DbType.Boolean, isOcean);
                db.AddInParameter(dbCommand, "@IsAir", DbType.Boolean, isAir);
                db.AddInParameter(dbCommand, "@IsOther", DbType.Boolean, isOther);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<LocationList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new LocationList
                                              {
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  EName = b.Field<string>("EName"),
                                                  ID = b.Field<Guid>("ID"),
                                                  IsAir = b.Field<bool>("IsAir"),
                                                  IsOcean = b.Field<bool>("IsOcean"),
                                                  IsOther = b.Field<bool>("IsOther"),
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
        /// 获取地点列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryId">国家</param>
        /// <param name="provinceId">省份</param>
        /// <param name="isOcean">海运</param>
        /// <param name="isAir">空运</param>
        /// <param name="isOther">其它</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回地点列表</returns>
        public List<LocationList> GetLocationList(
            string codeOrName,
            Guid? countryId,
            Guid? provinceId,
            bool? isOcean,
            bool? isAir,
            bool? isOther,
            bool? isValid,
            int maxRecords)
        {
            return GetLocationList(codeOrName, countryId, null, provinceId, isOcean, isAir, isOther, isValid, maxRecords);
        }

        /// <summary>
        /// 获取地点列表
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回地点列表</returns>
        public List<LocationList> GetRecentLocationList(DateTime beginDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetRecentLocationList");

                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, beginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<LocationList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new LocationList
                                              {
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  EName = b.Field<string>("EName"),
                                                  ID = b.Field<Guid>("ID"),
                                                  IsAir = b.Field<bool>("IsAir"),
                                                  IsOcean = b.Field<bool>("IsOcean"),
                                                  IsOther = b.Field<bool>("IsOther"),
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
        /// 获取地点信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回地点列表</returns>
        public LocationInfo GetLocationInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetLocationInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                LocationInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new LocationInfo
                                       {
                                           CName = b.Field<string>("CName"),
                                           Code = b.Field<string>("Code"),
                                           CountryID = b.Field<Guid>("CountryID"),
                                           CountryName = b.Field<string>("CountryName"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           EName = b.Field<string>("EName"),
                                           ID = b.Field<Guid>("ID"),
                                           IsAir = b.Field<bool>("IsAir"),
                                           IsOcean = b.Field<bool>("IsOcean"),
                                           IsOther = b.Field<bool>("IsOther"),
                                           IsValid = b.Field<bool>("IsValid"),
                                           ProvinceID = b.IsNull("ProvinceID") ? (Guid?)null : b.Field<Guid>("ProvinceID"),
                                           ProvinceName = b.Field<string>("ProvinceName"),
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
        public List<PostalCodeInfo> GetPostalCodeList(string cityName)
        {
            List<LocationList> list = GetLocationList(cityName, null, null, true, null, null, null, 1);
            if (list == null || list.Count <= 0)
            {
                return new List<PostalCodeInfo>();
            }
            List<PostalCodeInfo> codeList = GetPostalCodeList(list[0].ID);
            if (codeList != null)
            {
                return codeList;
            }
            else
            {
                return new List<PostalCodeInfo>();
            }

        }
        public void SavePostalCodeInfo(string countryName, string cityName, string zipCode)
        {
            List<LocationList> locations = GetLocationList(cityName, null, countryName, null, null, null, null, true, 1);
            SavePostalCodeInfo(locations[0].ID, zipCode);

        }
        /// <summary>
        /// 获取街道邮编（AMS）
        /// </summary>
        /// <param name="cityId">城市地址信息</param>
        /// <returns></returns>
        public List<PostalCodeInfo> GetPostalCodeList(Guid cityId)
        {
            ArgumentHelper.AssertGuidNotEmpty(cityId, "cityId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetPostalCodeList");

                db.AddInParameter(dbCommand, "@LocationID", DbType.Guid, cityId);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<PostalCodeInfo> result = (from b in ds.Tables[0].AsEnumerable()
                                             select new PostalCodeInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           LocationID = b.Field<Guid>("LocationID"),
                                           PostalCode = b.Field<string>("Zip")
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
        /// 保存邮编（AMS）
        /// </summary>
        /// <param name="cityId">城市地址信息</param>
        /// <param name="zip">邮编</param>
        /// <returns></returns>
        public void SavePostalCodeInfo(Guid cityId, string zip)
        {
            ArgumentHelper.AssertGuidNotEmpty(cityId, "cityId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSavePostalCodeInfo");

                db.AddInParameter(dbCommand, "@LocationID", DbType.Guid, cityId);
                db.AddInParameter(dbCommand, "@PostalCode", DbType.String, zip);
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
        /// 保存地点信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="countryId">国家</param>
        /// <param name="provinceId">省份</param>
        /// <param name="isOcean">海运</param>
        /// <param name="isAir">空运</param>
        /// <param name="isOther">其它</param>
        /// <param name="saveById">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveLocationInfo(
            Guid? id,
            string code,
            string cName,
            string eName,
            Guid countryId,
            Guid? provinceId,
            bool isOcean,
            bool isAir,
            bool isOther,
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
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveLocationInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@CountryId", DbType.Guid, countryId);
                db.AddInParameter(dbCommand, "@ProvinceId", DbType.Guid, provinceId);
                db.AddInParameter(dbCommand, "@IsOcean", DbType.Boolean, isOcean);
                db.AddInParameter(dbCommand, "@IsAir", DbType.Boolean, isAir);
                db.AddInParameter(dbCommand, "@IsOther", DbType.Boolean, isOther);
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
        /// 改变地点有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeLocationState(
            Guid id,
            bool? isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeLocationState");

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
        /// 获取合并的地点列表
        /// </summary>
        /// <param name="mainId">主ID</param>
        /// <returns>返回合并的地点列表</returns>
        public List<LocationList> GetMergedLocationList(Guid mainId)
        {
            ArgumentHelper.AssertGuidNotEmpty(mainId, "mainId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetMergedLocationList");

                db.AddInParameter(dbCommand, "@MainId", DbType.Guid, mainId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<LocationList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new LocationList
                                              {
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  EName = b.Field<string>("EName"),
                                                  ID = b.Field<Guid>("ID"),
                                                  IsAir = b.Field<bool>("IsAir"),
                                                  IsOcean = b.Field<bool>("IsOcean"),
                                                  IsOther = b.Field<bool>("IsOther"),
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
        /// 合并地点
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="preservedId">保留ID</param>
        /// <param name="mergeById">合并人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData MergeLocation(
            Guid[] ids,
            Guid preservedId,
            Guid mergeById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            ArgumentHelper.AssertGuidNotEmpty(preservedId, "prserverdId");
            ArgumentHelper.AssertGuidNotEmpty(mergeById, "mergeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspMergeLocation");

                string tempIds = ids.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@PreservedId", DbType.Guid, preservedId);
                db.AddInParameter(dbCommand, "@MergeById", DbType.Guid, mergeById);
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
        /// 取消合并
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="cancelById">取消人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CancelMergeLocation(
            Guid[] ids,
            Guid cancelById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);
            ArgumentHelper.AssertGuidNotEmpty(cancelById, "cancelById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelMergeLocation");

                string tempIds = ids.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CancelById", DbType.Guid, cancelById);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
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

        /// <summary>
        /// 根据名称获取港口信息
        /// </summary>
        /// <param name="names">names</param>
        /// <returns>港口信息</returns>
        public List<PortNames> GetPortForName(string[] names)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetPortForName");

                string tempNames = names.Join();

                db.AddInParameter(dbCommand, "@Names", DbType.String, tempNames);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<PortNames> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new PortNames
                                               {
                                                   ID = b.Field<Guid?>("ID"),
                                                   CName = b.Field<string>("CName"),
                                                   EName = b.Field<string>("EName"),
                                                   OriginName = b.Field<string>("Name"),

                                               }).ToList();
                #endregion

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }

        /// <summary>
        /// 根据国家ID得到港口信息
        /// </summary>
        /// <param name="countryIDs"></param>
        /// <returns></returns>
        public List<PortNames> GetPortForCountryID(Guid[] countryIDs) 
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetPortForCountryIDs");

                string tempNames = countryIDs.Join();

                db.AddInParameter(dbCommand, "@CountryIDs", DbType.String, tempNames);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<PortNames> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new PortNames
                                           {
                                               ID = b.Field<Guid?>("ID"),
                                               CName = b.Field<string>("CName"),
                                               EName = b.Field<string>("EName"),
                                               //OriginName = b.Field<string>("Name"),
                                           }).ToList();
                #endregion

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        
        }

    }
}
