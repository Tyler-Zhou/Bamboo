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
    /// �������ݷ������
    /// </summary>
    public class TransportFoundationService : ITransportFoundationService
    {
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;

        public TransportFoundationService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// �Ƿ�Ӣ�Ļ���
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
        /// ��ȡ����Ϣ�б�
        /// </summary>
        /// <param name="code">����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>��������Ϣ�б�</returns>
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
        /// ��ȡ����ϸ��Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>��������ϸ��Ϣ</returns>
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
        /// ��������Ϣ
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="code">����</param>
        /// <param name="isoCode">��׼����</param>
        /// <param name="description">����</param>
        /// <param name="teu">ϵ��װ������ͳ�Ƶ�λ</param>
        /// <param name="saveById">������</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı�������Ч״̬
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeById">�޸���</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ��ȡ���������б�
        /// </summary>
        /// <param name="originalCode">�����ش���</param>
        /// <param name="destinationCode">Ŀ�ĵش���</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>�������������б�</returns>
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
        /// ��ȡ����������ϸ��Ϣ
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>��������������ϸ��Ϣ</returns>
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
        /// ��������������Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="originalCodeID">�����ش���</param>
        /// <param name="destinationCodeID">Ŀ�ĵش���</param>
        /// <param name="description">����</param>
        /// <param name="saveById">������ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı���������״̬

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeById">�ı���</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ��ȡƷ���б�
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>����Ʒ���б�</returns>
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
        /// ��ȡƷ����Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>����Ʒ����Ϣ</returns>
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
        /// ����Ʒ����Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">��ID</param>
        /// <param name="cName">������</param>
        /// <param name="eName">Ӣ����</param>
        /// <param name="remark">��ע</param>
        /// <param name="saveById">������</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����ManyResultData</returns>
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
        /// ����Ʒ�����ڵ�

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="parentID">��ID</param>
        /// <param name="setById">������</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����ManyResultData</returns>
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
        /// ����Ʒ����Ч״̬

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeById">������</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����ManyResultData</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="code">����</param>
        /// <param name="name">����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���غ����б�</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="code">����</param>
        /// <param name="name">����</param>
        /// <param name="isParent">�Ƿ��ܺ���</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���غ����б�</returns>
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>���غ�����Ϣ</returns>
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
        /// ���溽����Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">����</param>
        /// <param name="cName">������</param>
        /// <param name="eName">Ӣ����</param>
        /// <param name="saveById">������</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı亽����Ч״̬

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeById">�ı���</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ���溽�߹��ҹ�����ϵ
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
        /// ��ȡ�����¹��Ҹۿ��б�
        /// </summary>
        /// <param name="shippingLineID">����ID</param>
        /// <param name="IsEnglish">�Ƿ�Ӣ��</param>
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
           Guid[] Id,  //��memo.id
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
          Guid[] Id,  //��memo.id
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
        /// ��ȡ�ֵ��б�
        /// </summary>
        /// <param name="code">����</param>
        /// <param name="name">����</param>
        /// <param name="type">�ֵ�����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>�����ֵ��б�</returns>
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
        /// ��ȡ�ֵ���Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>�����ֵ���Ϣ</returns>
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
        /// �����ֵ���Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">����</param>
        /// <param name="cName">������</param>
        /// <param name="eName">Ӣ����</param>
        /// <param name="description">����</param>
        /// <param name="type">�ֵ�����</param>
        /// <param name="saveByID">������</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı��ֵ�״̬

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeByID">�ı���</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ��ȡ�Ƽ۵�λ
        /// </summary>
        /// <param name="code">����</param>
        /// <param name="name">����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���ؼƼ۵�λ�б�</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="airlineID">���չ�˾ID</param>
        /// <param name="no">�����</param>
        /// <param name="polName">ʼ����ID</param>
        /// <param name="podName">�����ID</param>
        /// <param name="etdFrom">���������-��ʼ</param>
        /// <param name="etdTo">���������-����</param>
        /// <param name="etaFrom">���Ƶ�����-��ʼ</param>
        /// <param name="etaTo">���Ƶ�����-����</param>
        /// <param name="closingDateFrom">�ع���-��ʼ</param>
        /// <param name="closingDateTo">�ع���-����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���غ����б�</returns>
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>���غ�����Ϣ</returns>
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
        /// ���溽����Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="airlineID">���չ�˾ID</param>
        /// <param name="no">�����</param>
        /// <param name="polID">ʼ����ID</param>
        /// <param name="etdDate">���������</param>
        /// <param name="podID">�����ID</param>
        /// <param name="etaDate">���Ƶ�����</param>
        /// <param name="closingDate">�ع���</param>
        /// <param name="docClosingDate">���ļ���</param>
        /// <param name="saveByID">������ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı亽����Ч״̬
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeByID">�ı���ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ��ȡ���ϲ��ĺ����б�
        /// </summary>
        /// <param name="mainID">������ID</param>
        /// <returns>���ر��ϲ��ĺ����б�</returns>
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
        /// �ϲ�����
        /// </summary>
        /// <param name="ids">���ϲ��ĺ����б�</param>
        /// <param name="preservedID">��������ID</param>
        /// <param name="mergeByID">�ϲ���ID</param>
        /// <param name="updateDates">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ȡ���ϲ��ĺ���
        /// </summary>
        /// <param name="ids">ȡ���ĺ����б�</param>
        /// <param name="cancelByID">ȡ����</param>
        /// <param name="updateDates">���ݰ汾</param>
        /// <returns>����ManyResultData</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="code">����</param>
        /// <param name="name">����</param>
        /// <param name="carrierName">������</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���ش����б�</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="carrierIDs">����</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <returns>���ش����б�</returns>
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>���ش�����Ϣ</returns>
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
        /// ���洬����Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">����</param>
        /// <param name="name">����</param>
        /// <param name="carrierID">����ID</param>
        /// <param name="saveByID">������ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı䴬��״̬
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeByID">�ı���ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ϲ�����
        /// </summary>
        /// <param name="ids">���ϲ��Ĵ����б�</param>
        /// <param name="preservedID">��������ID</param>
        /// <param name="mergeByID">�ϲ���ID</param>
        /// <param name="updateDates">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ��ȡ���ϲ��Ĵ����б�
        /// </summary>
        /// <param name="mainID">������ID</param>
        /// <returns>���ر��ϲ��Ĵ����б�</returns>
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
        /// ȡ���ϲ��Ĵ���
        /// </summary>
        /// <param name="ids">ȡ���Ĵ����б�</param>
        /// <param name="cancelByID">ȡ����</param>
        /// <param name="updateDates">���ݰ汾</param>
        /// <returns>����ManyResultData</returns>
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
        /// ��ȡ���������б�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <returns>���ش��������б�</returns>
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
        /// ��ȡ���������б�
        /// </summary>
        /// <param name="vesselName">����</param>
        /// <param name="no">���κ�</param>
        /// <param name="polID">װ����ID</param>
        /// <param name="transhipmentPortID">��ת��ID</param>
        /// <param name="podID">ж����ID</param>
        /// <param name="etdFrom">���������-��ʼ</param>
        /// <param name="etdTo">���������-����</param>
        /// <param name="etaFrom">���Ƶ�����-��ʼ</param>
        /// <param name="etaTo">���Ƶ�����-����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���ش��������б�</returns>
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
        /// ��ȡ���������б�
        /// </summary>
        /// <param name="vesselName">����</param>
        /// <param name="no">���κ�</param>
        /// <param name="polID">װ����ID</param>
        /// <param name="transhipmentPortID">��ת��ID</param>
        /// <param name="podID">ж����ID</param>
        /// <param name="etdFrom">���������-��ʼ</param>
        /// <param name="etdTo">���������-����</param>
        /// <param name="etaFrom">���Ƶ�����-��ʼ</param>
        /// <param name="etaTo">���Ƶ�����-����</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="maxRecords">����¼��</param>
        /// <returns>���ش��������б�</returns>
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
        /// ��ȡ���������б�
        /// </summary>
        /// <param name="vesselID">����ID</param>
        /// <returns>���ش��������б�</returns>
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>���غ�����Ϣ</returns>
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
        /// ���溽����Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="vesselID">����ID</param>
        /// <param name="no">���κ�</param>
        /// <param name="saveByID">������ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ı亽��״̬
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <param name="changeByID">�ı���ID</param>
        /// <param name="updateDate">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// �ϲ�����
        /// </summary>
        /// <param name="ids">���ϲ��ĺ����б�</param>
        /// <param name="preservedID">��������ID</param>
        /// <param name="mergeByID">�ϲ���ID</param>
        /// <param name="updateDates">���ݰ汾</param>
        /// <returns>����SingleResultData</returns>
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
        /// ��ȡ���ϲ��ĺ����б�
        /// </summary>
        /// <param name="mainID">������ID</param>
        /// <returns>���ر��ϲ��ĺ����б�</returns>
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
        /// ȡ���ϲ��ĺ���
        /// </summary>
        /// <param name="ids">ȡ���ĺ����б�</param>
        /// <param name="cancelByID">ȡ����</param>
        /// <param name="updateDates">���ݰ汾</param>
        /// <returns>����ManyResultData</returns>
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
        /// ��ȡҵ����ñ�����Ŀ�б�
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

        #region MAC��ַ����

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
        /// ���洬��ˢ����־
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
