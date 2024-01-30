using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.Common.ServiceComponent
{
    public class CommonGoTo : ICommonGoTo
    {
        /// <summary>
        /// 返回列表查询数据
        /// </summary>
        /// <param name="type">业务类型(海出:OE 海进:OI 空出:AE 空进:AI)</param>
        /// <param name="no">查询条件</param>
        /// <param name="companyId">当前人员组织架构ID</param>
        /// <param name="fromType">当前业务的类别</param>
        /// <param name="opd">是否查询最近业务</param>
        /// <returns></returns>
        public List<GoToObject> GetGoToList(string type, string no, string companyId, int opd)
        {
          
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetGotoList");

                db.AddInParameter(dbCommand, "@Type", DbType.String, type);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.String, companyId);
                db.AddInParameter(dbCommand, "@OPD", DbType.Int16, opd);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
              
                return (from a in ds.Tables[0].AsEnumerable()
                        select new GoToObject
                        {
                          
                            Hblno = a.Field<string>("HBLNO"),
                            Mblno = a.Field<string>("MBLNO"),
                            OperationNo = a.Field<string>("OperationNO"),
                            Sono = a.Field<string>("SONO"),
                            ContainerNo = a.Field<string>("ContainerNo"),
                            Hblid = a.Field<Guid>("HBLID"),
                            Mblid = a.Field<Guid>("MBLID"),
                            OperationId = a.Field<Guid>("OperationID")
                        }).ToList();
            }
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
        /// 查询当前用户的配置文件
        /// </summary>
        /// <param name="userId">当前用户的ID</param>
        /// <returns></returns>
        public GotoSetting GotoSettingsList(Guid userId)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "UserId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetGotoSettingList");

                db.AddInParameter(dbCommand, "@UserId", DbType.Guid, userId);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return (from a in ds.Tables[0].AsEnumerable()
                        select new GotoSetting
                            {
                                SettingScope = a.Field<string>("SettingScope"),
                                UserId = a.Field<Guid>("UserId")
                            }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 添加用户设置的信息
        /// </summary>
        /// <param name="gotoSetting">实体对象</param>
        /// <param name="type">操作类型</param>
        /// <returns></returns>
        public int SettingSave(GotoSetting gotoSetting, string type)
        {
            var i = 0;
            try
            {

                ArgumentHelper.AssertGuidNotEmpty(gotoSetting.UserId, "UserId");
                if (gotoSetting != null)
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveGotoList");
                    db.AddInParameter(dbCommand, "@Id", DbType.Guid, Guid.NewGuid());
                    db.AddInParameter(dbCommand, "@SettingScope", DbType.String, gotoSetting.SettingScope);
                    db.AddInParameter(dbCommand, "@UserId", DbType.Guid, gotoSetting.UserId);
                    db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, gotoSetting.UserId);
                    db.AddInParameter(dbCommand, "@CreateDate", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "@Type", DbType.String, type);
                    db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, gotoSetting.UserId);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, DateTime.Now);
                    i = db.ExecuteNonQuery(dbCommand);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return i;
        }
    }
}
