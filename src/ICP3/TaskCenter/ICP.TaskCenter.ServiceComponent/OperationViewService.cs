using System;
using System.Collections.Generic;
using System.Linq;
using ICP.TaskCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ICP.Framework.CommonLibrary.Helper;
using ICP.TaskCenter.ServiceInterface.Common;

namespace ICP.TaskCenter.ServiceComponent
{
    /// <summary>
    /// 操作视图服务类
    /// </summary>
    public class OperationViewService : IOperationViewService
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public Guid UserId
        {
            get
            {
                return ApplicationContext.Current.UserId;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid AssistStaffGuid
        {
            get { return new Guid("30FC1674-0002-0000-0001-55B1BE690000"); }
        }

        /// <summary>
        /// 当前版本：中英
        /// </summary>
        public Boolean IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }

        #region IOperationViewService 成员

        /// <summary>
        /// 获取用户的顶级视图节点
        /// </summary>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetUserRootOperationViewList()
        {
            List<NodeInfo> nodes = GetUserWorkSpaceList(UserId);
            if (nodes.Count > 0)
            {
                NodeInfo tmpNode = nodes[0];
                List<NodeInfo> tmpNodes = GetWorkSpaceOperationViewList(tmpNode.SqlId, tmpNode.SearchCode, UserId);
                tmpNode.HasFetchChildrenData = true;
                nodes.AddRange(tmpNodes);
            }

            //经理： "C6265833-0902-E211-B376-0026551CA87B"
            if (GetSubordinateUserList().Any())
            {
                string strStaff = IsEnglish ? "Other Staff" : "下属同事";
                nodes.Add(new NodeInfo() { Id = UserId, Caption = strStaff, NodeType = NodeType.ParentStaff, HasChildren = true, HasFetchChildrenData = false });
            }
            if (GetSubordinateUserAssistsList().Any())
            {
                string strStaff = IsEnglish ? "Assist Staff" : "协助同事";
                nodes.Add(new NodeInfo() { Id = AssistStaffGuid, Caption = strStaff, NodeType = NodeType.AssistStaff, HasChildren = true, HasFetchChildrenData = false });
            }
            return nodes;
        }
        /// <summary>
        /// 获取用户操作视图列表
        /// </summary>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetOperationViewList(Guid? parentId)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationViewList]");

            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@Id", DbType.Guid, parentId);
            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, UserId);

            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<NodeInfo>();
                List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                        select new NodeInfo
                                        {
                                            Id = Guid.NewGuid(),
                                            SqlId = row.Field<Guid>("Id"),
                                            ParentId = row.Field<Guid?>("ParentId"),
                                            Caption = row.Field<string>("Caption"),
                                            HasChildren = row.Field<Boolean>("HasChildren"),
                                            ViewCode = row.Field<string>("ViewCode"),
                                            Keep = false
                                        }).ToList();
                return nodes;
            }
            catch (Exception ex)
            {
                return new List<NodeInfo>();
            }
        }

        /// <summary>
        /// 获取用户视图空间列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetUserWorkSpaceList(Guid? parentId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetUserWorkSpaceList]");

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@UserId", DbType.Guid, parentId);
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<NodeInfo>();
                List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                        select new NodeInfo
                                        {
                                            Id = row.Field<Guid>("WorkSpaceID"),
                                            Caption = IsEnglish ? row.Field<string>("EName") : row.Field<string>("CName"),
                                            HasChildren = true,
                                            ViewCode = row.Field<string>("Code"),
                                            SearchCode = row.Field<Guid>("WorkSpaceId"),
                                            NodeType = NodeType.OperateType,
                                            UserID = (Guid)parentId,
                                            SqlId = row.Field<Guid>("ID"),
                                            Hierarchy = 1,
                                            Keep = false
                                        }).ToList();
                return nodes;
            }
            catch (Exception ex)
            {
                return new List<NodeInfo>();
            }
        }

        /// <summary>
        /// 获取视图空间中的操作视图列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <param name="workSpaceId">视图ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetWorkSpaceOperationViewList(Guid? parentId, Guid? workSpaceId, Guid userId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetWorkSpaceOperationViewList]");
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@WorkSpaceID", DbType.Guid, workSpaceId);
            db.AddInParameter(dbCommand, "@ParentID ", DbType.Guid, parentId);
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<NodeInfo>();
                List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                        select new NodeInfo
                                        {
                                            Id = Guid.NewGuid(),
                                            ParentId = row.Field<Guid>("ParentID"),
                                            Caption = IsEnglish ? row.Field<string>("EName") : row.Field<string>("CName"),
                                            HasChildren = false,
                                            ViewCode = row.Field<string>("Code"),
                                            NodeType = NodeType.CodeLeaf,
                                            UserID = userId,
                                            SqlId = row.Field<Guid>("ID"),
                                            TooltiopCn = row.Field<string>("TooltiopCn"),
                                            TooltiopEn = row.Field<string>("TooltiopEn"),
                                            OperationType = (OperationType)row.Field<Byte>("OperationType"),
                                            BaseCriteria = row.Field<string>("BaseCriteria"),
                                            Keep = false
                                        }).ToList();

                foreach (var node in nodes)
                {
                    List<NodeInfo> SonViewList = GetWorkSpaceSonViewList(node.SqlId, null);
                    if (SonViewList.Any())
                    {
                        node.HasChildren = true;
                    }
                }


                return nodes;
            }
            catch (Exception ex)
            {
                return new List<NodeInfo>();
            }
        }

        /// <summary>
        /// 获取子节点下面的子节点
        /// </summary>
        /// <param name="parentId">上级节点GUID</param>
        /// <param name="viewparentId">上级视图Guid</param>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetWorkSpaceSonViewList(Guid parentId, Guid? viewparentId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetWorkSpaceSonViewList]");
            db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            DataSet dtSet = db.ExecuteDataSet(dbCommand);
            if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                return new List<NodeInfo>();
            List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                    select new NodeInfo
                                    {
                                        Id = Guid.NewGuid(),
                                        SqlId = row.Field<Guid>("ID"),
                                        Hierarchy = row.Field<int>("Hierarchy"),
                                        ParentId = string.IsNullOrEmpty(viewparentId.ToString()) ? row.Field<Guid>("ParentId") : viewparentId,
                                        Keep = false,
                                        Caption = IsEnglish ? row.Field<string>("EName") : row.Field<string>("CName"),
                                        TooltiopCn = row.Field<string>("TooltiopCn"),
                                        TooltiopEn = row.Field<string>("TooltiopEn"),
                                        OperationType = (OperationType)row.Field<Byte>("OperationType"),
                                        BaseCriteria = row.Field<string>("BaseCriteria"),
                                        UserID = UserId,
                                        HasChildren = false,
                                        HasFetchChildrenData = true,
                                        ViewCode = row.Field<string>("Code"),
                                        NodeType = NodeType.CodeLeaf
                                    }).ToList();
            return nodes.Where(n => n.Hierarchy > 2).ToList();
        }


        /// <summary>
        ///  获得用户下属的列表
        /// </summary>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetSubordinateUserList()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[sm].[uspGetSubordinateUserList]");

            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, UserId);
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<NodeInfo>();
                List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                        select new NodeInfo
                                        {
                                            Id = row.Field<Guid>("ID"),
                                            ParentId = UserId,
                                            Caption = IsEnglish ? row.Field<string>("EName") : row.Field<string>("CName"),
                                            HasChildren = true,
                                            ViewCode = "",
                                            NodeType = NodeType.Staff,
                                            UserID = row.Field<Guid>("ID"),
                                            SqlId = row.Field<Guid>("ID"),
                                            Keep = false
                                        }).ToList();
                var temnodes = (from info in nodes
                                where info.Id != UserId
                                select info).ToList();
                return temnodes;
            }
            catch (Exception ex)
            {
                return new List<NodeInfo>();
            }
        }


        /// <summary>
        ///  获得用户指定部门下属的列表
        /// </summary>
        /// <param name="depID">上级部门ID</param>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetDepartmentUserList(Guid depID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[sm].[uspGetSubordinateUserList]");

            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, UserId);
            db.AddInParameter(dbCommand, "@DepID", DbType.Guid, depID);
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<NodeInfo>();
                List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                        select new NodeInfo
                                        {
                                            Id = Guid.NewGuid(),
                                            ParentId = depID,
                                            Caption = IsEnglish ? row.Field<string>("EName") : row.Field<string>("CName"),
                                            HasChildren = true,
                                            ViewCode = "",
                                            NodeType = NodeType.Staff,
                                            UserID = row.Field<Guid>("ID"),
                                            SqlId = row.Field<Guid>("ID"),
                                            Keep = false
                                        }).ToList();
                var temnodes = (from info in nodes
                                where info.SqlId != UserId
                                select info).ToList();
                return temnodes;
            }
            catch (Exception ex)
            {
                return new List<NodeInfo>();
            }
        }

        /// <summary>
        ///  返回替换以后的客户名称
        /// </summary>
        /// <param name="codeName">需要替换的客户名称</param>
        /// <returns></returns>
        public string GetDeleteMarkerForInputStr(string codeName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "SELECT  [pub].[ufnGetDeleteMarkerForInputStr]('" + codeName + "')";
            return db.ExecuteScalar(CommandType.Text, sql).ToString();
        }


        /// <summary>
        /// 保存协助同事信息
        /// </summary>
        /// <param name="userAssists">协助同事实体类</param>
        /// <returns>是否保存成功</returns>
        public int UserAssistsSave(UserAssistsType userAssists)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspUserAssistsSave");
            db.AddInParameter(dbCommand, "@Id", DbType.Guid, userAssists.Id);
            db.AddInParameter(dbCommand, "@AssisterId", DbType.Guid, userAssists.AssisterId);
            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, userAssists.UserId);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, userAssists.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, userAssists.ToDate);
            db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, userAssists.CreateBy);
            db.AddInParameter(dbCommand, "@Operation", DbType.String, userAssists.Operation);
            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 返回协助同事列表信息
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="date">当前时间</param>
        /// <returns>协助同事列表信息</returns>
        public List<UserAssistsType> GetUserAssistsList(Guid userId, DateTime date)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUserAssistsList");

            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, userId);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, date);

            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<UserAssistsType>();
                List<UserAssistsType> UserAssistsType = (from row in dtSet.Tables[0].AsEnumerable()
                                                         select new UserAssistsType
                                                  {
                                                      Id = row.Field<Guid>("Id"),
                                                      AssisterId = row.Field<Guid>("AssisterId"),
                                                      UserId = row.Field<Guid>("UserId"),
                                                      FromDate = row.Field<DateTime>("FromDate"),
                                                      ToDate = row.Field<DateTime>("ToDate"),
                                                      CreateBy = row.Field<Guid>("CreateBy"),
                                                      Staff = IsEnglish ?
                                                      row.Field<string>("AssisterEName") + "=>" + row.Field<string>("UserEName") :
                                                      row.Field<string>("AssisterCName") + "=>" + row.Field<string>("UserCName")
                                                  }).ToList();
                return UserAssistsType;

            }
            catch (Exception ex)
            {
                return new List<UserAssistsType>();
            }
        }


        /// <summary>
        ///  获得协助同事列表
        /// </summary>
        /// <returns>节点集合</returns>
        public List<NodeInfo> GetSubordinateUserAssistsList()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUserAssistsList");

            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, UserId);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, DateTime.Now);
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet == null || dtSet.Tables[0].Rows.Count <= 0)
                    return new List<NodeInfo>();
                List<NodeInfo> nodes = (from row in dtSet.Tables[0].AsEnumerable()
                                        select new NodeInfo
                                        {
                                            Id = row.Field<Guid>("UserID"),
                                            ParentId = AssistStaffGuid,
                                            Caption = IsEnglish ? row.Field<string>("UserEName") : row.Field<string>("UserCName"),
                                            HasChildren = true,
                                            ViewCode = "",
                                            NodeType = NodeType.Staff,
                                            UserID = row.Field<Guid>("UserID"),
                                            SqlId = row.Field<Guid>("UserID"),
                                            Keep = false
                                        }).ToList();
                var temnodes = (from info in nodes
                                where info.Id != UserId
                                select info).ToList();
                return temnodes;
            }
            catch (Exception ex)
            {
                return new List<NodeInfo>();
            }
        }
        #endregion
    }
}
