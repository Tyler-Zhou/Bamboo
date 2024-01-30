//-----------------------------------------------------------------------
// <copyright file="OrganizationService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary;

namespace ICP.Sys.ServiceComponent
{

    /// <summary>
    /// 组织结构管理服务
    /// </summary>
    public class OrganizationService : IOrganizationService
    {
        #region 同步微信通讯录
        UserService.SyncContactsService syncContactsService = new UserService.SyncContactsService();
        #endregion

        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;
        public OrganizationService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
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
        /// 获取组织结构列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回组织结构列表</returns>
        public List<OrganizationList> GetOrganizationList(
            string code,
            string name,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetOrganizationList");

                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@Name", DbType.String, name);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<OrganizationList>();
                }

                List<OrganizationList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new OrganizationList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      CShortName = b.Field<string>("CName"),
                                                      EShortName = b.Field<string>("EName"),
                                                      Type = (OrganizationType)b.Field<byte>("Type"),
                                                      ParentID = b.Field<Guid?>("ParentID"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      CreateBy = b.Field<string>("CreateBy"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      HierarchyCode = b.Field<string>("HierarchyCode"),
                                                      FullName = b.Field<string>("FullName"),
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
        /// 获得指定组织结构的子节点 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationAndChildList(Guid[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetGetOrganizationAndChildList");

                db.AddInParameter(command, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<OrganizationList>();
                }

                List<OrganizationList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new OrganizationList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      CShortName = b.Field<string>("CName"),
                                                      EShortName = b.Field<string>("EName"),
                                                      Type = (OrganizationType)b.Field<byte>("Type"),
                                                      ParentID = b.Field<Guid?>("ParentID"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      HierarchyCode = b.Field<string>("HierarchyCode"),
                                                      FullName = b.Field<string>("FullName"),
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
        /// 指定指定组织结构的部门信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public OrganizationInfo GetCompanyInfo(Guid orgID)
        {
            ArgumentHelper.AssertGuidNotEmpty(orgID, "orgID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetCompanyInfofo");

                db.AddInParameter(command, "@OrgID", DbType.Guid, orgID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OrganizationInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new OrganizationInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               Code = b.Field<string>("Code"),
                                               CShortName = b.Field<string>("CName"),
                                               EShortName = b.Field<string>("EName"),
                                               ParentID = b.Field<Guid?>("ParentID"),
                                               ParentName = b.Field<string>("ParentName"),
                                               Type = (OrganizationType)b.Field<byte>("Type"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               CreateById = b.Field<Guid>("CreateByID"),
                                               CreateBy = b.Field<string>("CreateBy"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               HierarchyCode = b.Field<string>("HierarchyCode"),
                                               FullName = b.Field<string>("FullName")

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
        /// 获得指定组织结构上级公司ID
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public OrganizationInfo GetOrganizationParentCompanyID(Guid deptID)
        {
            ArgumentHelper.AssertGuidNotEmpty(deptID, "DeptID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetSqlStringCommand(" select sm.ufnGetCompanyIDForOrganizationID('" + deptID.ToString() + "') ");

                Guid companyID = DataTypeHelper.GetGuid(db.ExecuteScalar(command), Guid.Empty);


                OrganizationInfo orgInfo = GetOrganizationInfo(companyID);

                return orgInfo;
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
        /// 获取办事处和公司列表
        /// </summary>
        /// <returns>返回组织结构列表</returns>
        public List<OrganizationList> GetOfficeList()
        {
            List<OrganizationList> ol = this.GetOrganizationList(
                string.Empty,
                string.Empty,
                true,
                0);

            return ol.FindAll(delegate(OrganizationList o)
            {
                return o.Type == OrganizationType.Company;
            });
        }

        /// <summary>
        /// 获取组织结构信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回组织结构信息</returns>
        public OrganizationInfo GetOrganizationInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetOrganizationInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OrganizationInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new OrganizationInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               Code = b.Field<string>("Code"),
                                               CShortName = b.Field<string>("CName"),
                                               EShortName = b.Field<string>("EName"),
                                               ParentID = b.Field<Guid?>("ParentID"),
                                               ParentName = b.Field<string>("ParentName"),
                                               Type = (OrganizationType)b.Field<byte>("Type"),
                                               IsValid = b.Field<bool>("IsValid"),
                                               CreateById = b.Field<Guid>("CreateByID"),
                                               CreateBy = b.Field<string>("CreateBy"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               HierarchyCode = b.Field<string>("HierarchyCode"),
                                               FullName = b.Field<string>("FullName")

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
        /// 保存组织结构信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="parentId">父节点Id</param>
        /// <param name="type">组织结构类型</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="saveById">保存人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SaveOrganizationInfo(
            Guid? id,
            Guid? parentId,
            OrganizationType type,
            string code,
            string cname,
            string ename,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cname, "cname");
            ArgumentHelper.AssertStringNotEmpty(ename, "ename");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveOrganizationInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@ParentID", DbType.Guid, parentId);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@CShortName", DbType.String, cname);
                db.AddInParameter(command, "@EShortName", DbType.String, ename);
                db.AddInParameter(command, "@Type", DbType.Int16, (short)type);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(command, Guid.Empty);
                syncContactsService.SyncCorpContacts("organization_SaveOrganizationInfo");
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
        /// 改变组织结构状态
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData ChangeOrganizationState(
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
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeOrganizationState");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(command);
                syncContactsService.SyncCorpContacts("organization_ChangeOrganizationState");
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
        /// 设置组织结构节点的父节点
        /// </summary>
        /// <param name="childId">子节点Id</param>
        /// <param name="parentId">父节点Id</param>
        /// <param name="setById">设置人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        public ManyHierarchyResultData SetParentOrganization(
            Guid childId,
            Guid? parentId,
            Guid setById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(childId, "childId");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetParentOrganization");


                db.AddInParameter(command, "@ID", DbType.Guid, childId);
                db.AddInParameter(command, "@ParentID", DbType.Guid, parentId.HasValue ? parentId.Value : (Guid?)null);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyHierarchyResultData result = db.ManyHierarchyResult(command, Guid.Empty);
                syncContactsService.SyncCorpContacts("organization_SetParentOrganization");
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
    }
}
