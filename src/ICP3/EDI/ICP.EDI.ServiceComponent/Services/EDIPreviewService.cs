using ICP.EDI.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ICP.EDI.ServiceComponent
{
    partial class EDIService
    {
        /// <summary>
        /// 获取EDI预览数据
        /// </summary>
        /// <param name="pEDIMode"></param>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public List<EDIPreviewValue> GetEDIPreviewValueList(EDIMode pEDIMode, Guid[] IDS)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetEDIPreviewData");

                dbCommand.CommandTimeout = 60;

                string tempIds = IDS.Join();


                db.AddInParameter(dbCommand, "@EdiType", DbType.Byte, pEDIMode);
                db.AddInParameter(dbCommand, "@IDS", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count == 1)
                {
                    return ConvertEDIPreviewValue(ds.Tables[0], null, pEDIMode);
                }
                else if (ds.Tables.Count == 2)
                {
                    return ConvertEDIPreviewValue(ds.Tables[0], ds.Tables[1], pEDIMode);
                }

                return null;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EDIPreviewValue> ConvertEDIPreviewValue(DataTable BLInfos, DataTable ContainerInfos, EDIMode EdiType)
        {
            if (BLInfos == null)
            {
                return null;
            }

            List<EDIPreviewValue> values = new List<EDIPreviewValue>();
            EDIPreviewValue value;

            List<EDINode> BLs = (from b in BLInfos.AsEnumerable()
                                  select new EDINode
                                  {
                                      BookingNO = b.Field<string>("BookingNO"),
                                      CagoType = b.Field<string>("CagoType"),
                                      CargoDESC = b.Field<string>("CargoDESC"),
                                      Centigrade = b.Field<string>("Centigrade"),
                                      ConsigneeAddress = b.Field<string>("ConsigneeAddress"),
                                      ConsigneeFax = b.Field<string>("ConsigneeFax"),
                                      ConsigneeName = b.Field<string>("ConsigneeName"),
                                      ConsigneeTel = b.Field<string>("ConsigneeTel"),
                                      Container = b.Field<string>("Container"),
                                      DangerousClass = b.Field<string>("DangerousClass"),
                                      DangerousNo = b.Field<string>("DangerousNo"),
                                      DeliveryPort = b.Field<string>("DeliveryPort"),
                                      DeliveryPortCode = b.Field<string>("DeliveryPortCode"),
                                      DischargePort = b.Field<string>("DischargePort"),
                                      DischargePortCode = b.Field<string>("DischargePortCode"),
                                      ETD = b.Field<DateTime>("ETD").ToShortDateString(),
                                      LoadPort = b.Field<string>("LoadPort"),
                                      LoadPortCode = b.Field<string>("LoadPortCode"),
                                      Marks = b.Field<string>("Marks"),
                                      NotifyAddress = b.Field<string>("NotifyAddress"),
                                      NotifyFax = b.Field<string>("NotifyFax"),
                                      NotifyName = b.Field<string>("NotifyName"),
                                      NotifyTel = b.Field<string>("NotifyTel"),
                                      PaymentTerm = b.Field<string>("PaymentTerm"),
                                      Qty = b.Field<decimal>("Qty").ToString("F0"),
                                      Receipt = b.Field<string>("Receipt"),
                                      ReceiptCode = b.Field<string>("ReceiptCode"),
                                      Remarks = b.Field<string>("Remarks"),
                                      SCNO = b.Field<string>("SCNO"),
                                      ReleaseType = b.Field<string>("ReleaseType"),
                                      HSCode = b.Field<string>("HSCode"),
                                      ShipperAddress = b.Field<string>("ShipperAddress"),
                                      ShipperFax = b.Field<string>("ShipperFax"),
                                      ShipperName = b.Field<string>("ShipperName"),
                                      ShipperTel = b.Field<string>("ShipperTel"),
                                      TransportClauseName = b.Field<string>("TransportClauseName"),
                                      UNCode = b.Field<string>("UNCode"),
                                      Vessel = b.Field<string>("Vessel"),
                                      Volume = b.Field<decimal>("Volume").ToString("F2"),
                                      Voyage = b.Field<string>("Voyage"),
                                      Weight = b.Field<decimal>("Weight").ToString("F2"),
                                      No = b.Field<string>("No"),
                                  }).ToList();

            List<EDIChildNode> Containers = null;
            if (ContainerInfos != null)
            {
                Containers = (from b in ContainerInfos.AsEnumerable()
                              select new EDIChildNode
                              {
                                  ContainerNo = b.Field<string>("ContainerNo"),
                                  ContainerType = b.Field<string>("ContainerType"),
                                  Qty = b.Field<decimal>("Qty").ToString("F0"),
                                  SealNo = b.Field<string>("SealNo"),
                                  Volume = b.Field<decimal>("Volume").ToString("F2"),
                                  Weight = b.Field<decimal>("Weight").ToString("F2"),
                                  No = b.Field<string>("No"),
                              }).ToList();
            }

            if (BLs != null)
            {
                foreach (EDINode BL in BLs)
                {
                    Type t = typeof(EDINode);
                    System.Reflection.PropertyInfo[] propertyInfos = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
                    {
                        value = new EDIPreviewValue();
                        value.No = BL.No;
                        value.ContainerNo = null;
                        object[] objarr = propertyInfo.GetCustomAttributes(true);
                        if (objarr != null && objarr.Length > 0)
                        {
                            GuidRequiredAttribute att = objarr[0] as GuidRequiredAttribute;
                            value.Node = att.CMessage;
                        }
                        else
                            value.Node = propertyInfo.Name;

                        value.Sourse = "";
                        value.Value = propertyInfo.GetValue(BL, null) == null ? "" : propertyInfo.GetValue(BL, null).ToString();
                        values.Add(value);
                    }

                }
            }

            if (Containers != null)
            {
                foreach (EDIChildNode Container in Containers)
                {
                    Type t = typeof(EDIChildNode);
                    System.Reflection.PropertyInfo[] propertyInfos = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
                    {
                        object[] objarr = propertyInfo.GetCustomAttributes(true);
                        if (objarr != null && objarr.Length > 0)
                        {
                            GuidRequiredAttribute att = objarr[0] as GuidRequiredAttribute;

                            value = new EDIPreviewValue();
                            value.No = Container.No;
                            value.ContainerNo = Container.ContainerNo;
                            value.Node = att.CMessage;
                            value.Sourse = "";
                            value.Value = propertyInfo.GetValue(Container, null) == null ? "" : propertyInfo.GetValue(Container, null).ToString();
                            values.Add(value);
                        }
                    }

                }
            }

            foreach (EDIPreviewValue show in values)
            {
                switch (show.Node)
                {
                    case "提单编号":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "船编号":
                        show.Sourse = "MBL";
                        break;
                    case "船名":
                        show.Sourse = "MBL";
                        break;
                    case "航次":
                        show.Sourse = "MBL";
                        break;
                    case "离港日":
                        show.Sourse = "MBL";
                        break;
                    case "收货地编号":
                        show.Sourse = "MBL";
                        break;
                    case "收货地":
                        show.Sourse = "MBL";
                        break;
                    case "装货港编号":
                        show.Sourse = "MBL";
                        break;
                    case "装货港":
                        show.Sourse = "MBL";
                        break;
                    case "卸货港编号":
                        show.Sourse = "MBL";
                        break;
                    case "卸货港":
                        show.Sourse = "MBL";
                        break;
                    case "交货地编号":
                        show.Sourse = "MBL";
                        break;
                    case "交货地":
                        show.Sourse = "MBL";
                        break;
                    case "订舱号":
                        show.Sourse = "MBL";
                        break;
                    case "付款类型":
                        show.Sourse = "MBL";
                        break;
                    case "运输条款":
                        show.Sourse = "MBL";
                        break;
                    case "合约号":
                        show.Sourse = "MBL";
                        break;
                    case "发货人名称":
                        show.Sourse = "MBL";
                        break;
                    case "发货人地址":
                        show.Sourse = "MBL";
                        break;
                    case "发货人电话":
                        show.Sourse = "MBL";
                        break;
                    case "发货人传真":
                        show.Sourse = "MBL";
                        break;
                    case "收货人名称":
                        show.Sourse = "MBL";
                        break;
                    case "收货人地址":
                        show.Sourse = "MBL";
                        break;
                    case "收货人电话":
                        show.Sourse = "MBL";
                        break;
                    case "收货人传真":
                        show.Sourse = "MBL";
                        break;
                    case "通知人名称":
                        show.Sourse = "MBL";
                        break;
                    case "通知人地址":
                        show.Sourse = "MBL";
                        break;
                    case "通知人电话":
                        show.Sourse = "MBL";
                        break;
                    case "通知人传真":
                        show.Sourse = "MBL";
                        break;
                    case "件数":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "箱号":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "封条号":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "重量":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "体积":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "品名":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "备注":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "唛头":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                    case "箱型":
                        show.Sourse = (EdiType == EDIMode.Booking || EdiType == EDIMode.SI) ? "MBL" : "报关单";
                        break;
                }
            }


            return values;
        }
    }
}
