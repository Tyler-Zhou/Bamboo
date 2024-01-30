using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FCMCommonService
    {
        /// <summary>
        ///  获得分文件比较详细信息
        /// </summary>
        /// <param name="OEBookingID">出口业务ID</param>
        /// <param name="OIBookingID">进口业务ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <param name="OperationType">业务类型</param>
        /// <returns></returns>
        public List<Fee> DispatchCompareBillAndCharge(Guid OEBookingID, Guid OIBookingID, Guid DispatchFileLogID, OperationType OperationType)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = null;
                if (OperationType == OperationType.OceanExport || OperationType == OperationType.AirExport)
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspDispatchCompareBillAndCharge]");
                }
                else
                {
                    dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspDispatchReviseCompareBillAndCharge]");
                }

                db.AddInParameter(dbCommand, "@OEBookingID", DbType.Guid, OEBookingID);
                db.AddInParameter(dbCommand, "@OIBookingID", DbType.Guid, OIBookingID);
                db.AddInParameter(dbCommand, "@DispatchFileLogID", DbType.Guid, DispatchFileLogID);
                db.AddInParameter(dbCommand, "@operationType", DbType.Int16, OperationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                if (OperationType == OperationType.OceanExport || OperationType == OperationType.AirExport)
                {
                    return CompareCharage(ds);
                }
                else
                {
                    return CompareCharageForOI(ds);
                }
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private List<Fee> CompareCharage(DataSet ds)
        {
            List<Fee> lstFee = (
                                  from c in ds.Tables[0].AsEnumerable()
                                      //where c.Field<Guid>("BillID").ToString() == b.ID
                                  select new Fee
                                  {
                                      BillID = c.Field<Guid>("billID").ToString(),
                                      ID = c.Field<Guid>("ID").ToString(),
                                      IsAgent = c.Field<Boolean>("Agent") ? 1 : 0,
                                      Way = int.Parse(c["Way"].ToString()),
                                      ChargeCode = c.Field<String>("Code"),
                                      ChargeName = c.Field<String>("ChargeName"),
                                      OldSumMoney = c.Field<String>("OIAmount"),
                                      NewSumMoney = c.Field<String>("OeAmount"),
                                      OldRemark = c.Field<String>("oiremark"),
                                      NewRemark = c.Field<String>("oeremark"),
                                      UpdateState = int.Parse(c.Field<int>("state").ToString()),
                                      RefFeeID = c.Field<Guid?>("RefFeeID"),
                                      DispatchFeeID = c.Field<Guid?>("DispatchFeeID"),
                                  }
                                ).ToList();
            List<Fee> OldFee = lstFee.FindAll(r => r.UpdateState == 1);
            //更改状态
            foreach (Fee feeItem in lstFee.FindAll(r => r.UpdateState == 2))
            {
                if (feeItem.DispatchFeeID.IsNullOrEmpty())
                {
                    throw new Exception("费用有误");
                }
                if (OldFee.Count(r => feeItem.DispatchFeeID.StringEquals(r.RefFeeID)) > 0)
                {
                    feeItem.UpdateState = 0;
                    OldFee.Find(r => feeItem.DispatchFeeID.StringEquals(r.RefFeeID) && r.UpdateState != 0).UpdateState = 0;
                }
                else
                {
                    feeItem.UpdateState = 1;
                }
            }
            //foreach (Fee f in lstFee.FindAll(r => r.UpdateState == 2))
            //    {
            //    if (OldFee.Count(r => r.ChargeCode == f.ChargeCode) == 0)
            //    {
            //        f.UpdateState = 1;
            //    }
            //    else if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
            //    {
            //        f.UpdateState = 0;
            //        if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
            //        {
            //            OldFee.Find(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
            //        }
            //    }
            //    else if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
            //    {
            //        if (lstFee.FindAll(r => r.UpdateState == 2).Count(s => s.ChargeCode == f.ChargeCode) > 1)
            //        {
            //            f.UpdateState = 1;
            //            if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
            //            {
            //                OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
            //            }
            //        }
            //        else
            //        {
            //            f.UpdateState = 2;
            //            if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
            //            {
            //                OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
            //            }
            //        }
            //    }
            //}
            if (OldFee.Count(r => r.UpdateState == 1) > 0)
            {
                foreach (Fee f in OldFee.FindAll(r => r.UpdateState == 1))
                {
                    f.UpdateState = 4;
                }
            }

            return lstFee;
        }

        private List<Fee> CompareCharageForOI(DataSet ds)
        {

            List<Fee> lstFee = (
                                  from c in ds.Tables[0].AsEnumerable()
                                      //where c.Field<Guid>("BillID").ToString() == b.ID
                                  select new Fee
                                  {
                                      BillID = c.Field<Guid>("billID").ToString(),
                                      ID = c.Field<Guid>("ID").ToString(),
                                      IsAgent = c.Field<Boolean>("Agent") ? 1 : 0,
                                      Way = int.Parse(c["Way"].ToString()),
                                      ChargeCode = c.Field<String>("Code"),
                                      ChargeName = c.Field<String>("ChargeName"),
                                      OldSumMoney = c.Field<String>("OiAmount"),
                                      NewSumMoney = c.Field<String>("OeAmount"),
                                      OldRemark = c.Field<String>("oiremark"),
                                      NewRemark = c.Field<String>("oeremark"),
                                      UpdateState = int.Parse(c.Field<int>("state").ToString()),
                                      RefFeeID = c.Field<Guid?>("RefFeeID").IsNullOrEmpty() ? Guid.Empty : c.Field<Guid>("RefFeeID"),
                                  }
                                ).ToList();
            List<Fee> OldFee = lstFee.FindAll(r => r.UpdateState == 1);
            //更改状态
            foreach (Fee feeItem in lstFee.FindAll(r => r.UpdateState == 2))
            {
                if (OldFee.Count(findID => findID.ID.EqualsGuid(feeItem.RefFeeID)) > 0)
                {
                    //进口引用ID和当前ID一致，为同一条费用
                    feeItem.UpdateState = 0;
                    OldFee.Find(findID => findID.ID.EqualsGuid(feeItem.RefFeeID) && findID.UpdateState != 0).UpdateState = 0;
                }
                else
                {
                    feeItem.UpdateState = 1;
                }
                //if (OldFee.Count(r => r.ChargeCode == f.ChargeCode) == 0)
                //{
                //    f.UpdateState = 1;
                //}
                //else if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
                //{
                //    f.UpdateState = 0;
                //    if (OldFee.Count(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                //    {
                //        OldFee.Find(r => r.OldSumMoney == f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                //    }
                //}
                //else if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way) > 0)
                //{
                //    if (lstFee.FindAll(r => r.UpdateState == 2).Count(s => s.ChargeCode == f.ChargeCode) > 1)
                //    {
                //        f.UpdateState = 1;
                //        if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                //        {
                //            OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                //        }
                //    }
                //    else
                //    {
                //        f.UpdateState = 2;
                //        if (OldFee.Count(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0) > 0)
                //        {
                //            OldFee.Find(r => r.OldSumMoney != f.NewSumMoney && r.ChargeCode == f.ChargeCode && r.Way == f.Way && r.UpdateState != 0).UpdateState = 0;
                //        }
                //    }
                //}
            }
            if (OldFee.Count(findID => findID.UpdateState == 1) > 0)
            {
                foreach (Fee feeItem in OldFee.FindAll(findID => findID.UpdateState == 1))
                {
                    feeItem.UpdateState = 3;
                }
            }

            return lstFee;
        }
    }
}
