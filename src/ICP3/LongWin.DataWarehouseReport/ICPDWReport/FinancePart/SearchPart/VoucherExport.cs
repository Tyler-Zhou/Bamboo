using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LongWin.DataWarehouseReport.ServiceInterface;

namespace LongWin.DataWarehouseReport.WinUI
{
    class VoucherExport
    {

        public static  List<LedgerData> CheckFinceCode(List<UFCustomer> ufCustomerList ,List<LedgerData> ledgerDataList)
        {
            var voucherList = from l in ledgerDataList
                              where ufCustomerList.Count(u => u.CustomerCode == l.CustomerFinanceCode) == 0 
                              group l by new 
                              {
                                  l.VoucherSeqNo,
                                  l.OperationNo,
                                  l.BillNo,                                  
                                  l.CustomerFinanceCode,
                                  l.CustomerName
                              } into lby
                              orderby lby.Key.VoucherSeqNo, lby.Key.OperationNo, lby.Key.BillNo, lby.Key.CustomerFinanceCode, lby.Key.CustomerName
                              select new LedgerData 
                              {
                                  Id = Guid.NewGuid(),
                                  //Balance = l.Balance,
                                  //BeginBalance = l.BeginBalance,
                                  BillId = Guid.NewGuid(),
                                  BillNo = lby.Key.BillNo,
                                  BillType = 0,
                                  CrAmt = 0,
                                  Date = DateTime.Now,
                                  DrAmt = 0,
                                  GLCode = "",
                                  GLDescription = "",
                                  GLId = Guid.NewGuid(),
                                  OrgAmt = 0,
                                  Rate = 1,
                                  CustomerFinanceCode = lby.Key.CustomerFinanceCode,
                                  CustomerName = lby.Key.CustomerName,
                                  Remark = "",
                                  OperationNo = lby.Key.OperationNo,
                                  VoucherSeqNo = lby.Key.VoucherSeqNo
                              };
            return new List<LedgerData>(voucherList);
                              
        }

        public static List<LedgerData> CheckUserDepartCode(List<UFCustomer> ufCustomerList, List<LedgerData> ledgerDataList)
        {
            var voucherList = from l in ledgerDataList
                              where ufCustomerList.Count(u => u.CustomerCode == l.EmployeeName) == 0 || ufCustomerList.Count(u =>  u.CustomerName == l.DepartmentName) == 0
                              group l by new
                              {
                                  l.VoucherSeqNo,
                                  l.OperationNo,
                                  l.BillNo,
                                  l.EmployeeName,
                                  l.DepartmentName
                              } into lby
                              orderby lby.Key.VoucherSeqNo, lby.Key.OperationNo, lby.Key.BillNo, lby.Key.EmployeeName, lby.Key.DepartmentName
                              select new LedgerData
                              {
                                  Id = Guid.NewGuid(),
                                  //Balance = l.Balance,
                                  //BeginBalance = l.BeginBalance,
                                  BillId = Guid.NewGuid(),
                                  BillNo = lby.Key.BillNo,
                                  BillType = 0,
                                  CrAmt = 0,
                                  Date = DateTime.Now,
                                  DrAmt = 0,
                                  GLCode = "",
                                  GLDescription = "",
                                  GLId = Guid.NewGuid(),
                                  OrgAmt = 0,
                                  Rate = 1,
                                  CustomerFinanceCode = lby.Key.EmployeeName,
                                  CustomerName = lby.Key.DepartmentName,
                                  Remark = "",
                                  OperationNo = lby.Key.OperationNo,
                                  VoucherSeqNo = lby.Key.VoucherSeqNo
                              };
            return new List<LedgerData>(voucherList);

        }
        /// <summary>
        /// 导出凭证
        /// </summary>
        /// <param name="ledgerDataList">凭证数据</param>
        /// <param name="ExportType">导出格式(0:计提 1:实收实付)</param>
        /// <returns></returns>
        public static string BuilderPlanVoucher(List<LedgerData> ledgerDataList, List<UFCustomer> ufCustomerList)
        {

            DateTime exportdate = System.DateTime.Now;
            if (ledgerDataList.Count > 0)
                exportdate = ledgerDataList[0].Date;
            string glstr = GetSpecialGLCodeItemstr(exportdate);

             var voucherList = from ld in ledgerDataList
                                 where ld.DrAmt != 0 || ld.CrAmt != 0
                                 select new
                                 {
                                     drAmt = ld.DrAmt
                                   ,
                                     crAmt = ld.CrAmt
                                   ,
                                     OrgAmt = ld.DrAmt > 0.00m ? ld.OrgAmt : -ld.OrgAmt
                                   ,
                                     BillDate = ld.Date
                                   ,
                                     BillNo = ld.BillNo
                                   ,
                                     Rate = ld.Rate
                                   ,
                                     BillType = ld.BillType
                                   ,
                                     OperationNo=ld.OperationNo
                                   ,
                                     VoucherSeqNo=ld.VoucherSeqNo
                                   ,
                                      FinanceCode = ld.CustomerFinanceCode
                                   ,
                                     GLCode=ld.GLCode
                                   ,
                                     YM=ld.Date.ToString("yyMM")
                                   ,
                                     CustomerName=ld.CustomerShortName
                                   ,
                                     Remark=ld.Remark
                                   ,
                                     DepartmentName=ld.DepartmentName
                                   ,
                                    EmployeeName= ld.EmployeeName
                                 };
          
            string vouchers = string.Empty;
            decimal drAmount = 0.00m;
            decimal crAmount = 0.00m;
            decimal orgAmt = 0.00m;
            decimal rate = 0.00m;
            string ji = string.Empty;
            string departName = string.Empty;
            foreach (var voucher in voucherList)
            {
                
                UFCustomer cus = ufCustomerList.Find(d => d.CustomerCode == voucher.EmployeeName );
                if (cus != null)
                {
                    departName = cus.CustomerName; //管理成本匹配用友的部门名称
                }
                else
                {
                    departName = voucher.DepartmentName;
                }

                drAmount = 0.00m;
                crAmount = 0.00m;
                orgAmt = Math.Abs(voucher.OrgAmt);
                rate = voucher.Rate;
                string VoucherSeqNo = string.Empty;
                
                if (voucher.BillType == 2
                    || voucher.BillType == 20)//收/付款凭证
                {
                    if (string.IsNullOrEmpty(voucher.VoucherSeqNo))
                    {
                        VoucherSeqNo = "凭证号为空";
                    }
                    else
                    {
                        VoucherSeqNo = voucher.VoucherSeqNo.ToString();
                    }
                }
                else
                {
                    VoucherSeqNo = voucher.YM + voucher.OperationNo;
                }
                ji = voucher.Remark;//摘要


                

                drAmount = voucher.drAmt;
                crAmount = voucher.crAmt;

                if (glstr.Contains(voucher.GLCode))
                {
                    orgAmt = Math.Abs(voucher.crAmt);
                    rate = 1.00m;
                    ji = voucher.OperationNo;
                }
                if (orgAmt == 0.00m)
                    continue;
                vouchers = vouchers
                  + voucher.BillDate.ToShortDateString()
                  + " ," + "记"
                  + "," + VoucherSeqNo
                  + "," + voucher.BillType.ToString()
                  + "," + ji
                  + "," + voucher.GLCode
                  + "," + drAmount
                  + "," + crAmount
                  + "," + "0"
                  + "," + orgAmt
                  + "," + rate
                  + "," + "," + "," + "," + voucher.BillDate.ToShortDateString() + ","
                  + departName
                  + "," + voucher.EmployeeName
                  + "," + voucher.FinanceCode + "\r\n";
            }
            vouchers = "填制凭证,V800\r\n" + vouchers;
            
            return vouchers;
        }


        public static string GetSpecialGLCodeItemstr(DateTime exportDate)
        {
            SpecifyGLCode glSet = new SpecifyGLCode();
            string path = System.Environment.CurrentDirectory + "\\SpecialGLCode.xml";
            string glStr = string.Empty;
            if (System.IO.File.Exists(path))
            {
                glSet.ReadXml(path);
            }
            else
            {
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2000", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2001", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2002", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2003", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2004", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2005", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2006", "590102");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2007", "590103");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2008", "590104");
                if (exportDate.Year == 2010)
                {
                    glSet.SpecialGLCode.AddSpecialGLCodeRow("2009", "590105");
                }
                if (exportDate.Year == 2011)
                {
                    glSet.SpecialGLCode.AddSpecialGLCodeRow("2010", "590106");
                }
                glSet.SpecialGLCode.AddSpecialGLCodeRow(exportDate.Year.ToString(), "3131");
                glSet.WriteXml(path);
            }

            List<SpecialGLCodeItem> glCodes = new List<SpecialGLCodeItem>();
            foreach (SpecifyGLCode.SpecialGLCodeRow row in glSet.SpecialGLCode.Rows)
            {
                SpecialGLCodeItem item = new SpecialGLCodeItem();
                item.GLCode = row.GLCode;
                item.Year = row.Year;
                glStr = glStr + "," + row.GLCode;
            }
            glStr = glStr + "," + "510101"+",5101";
            return glStr;
        }


        public static List<SpecialGLCodeItem> GetSpecialGLCodeItem()
        {
            SpecifyGLCode glSet = new SpecifyGLCode();
            string path = System.Environment.CurrentDirectory + "\\SpecialGLCode.xml";
            string glStr = string.Empty;
            if (System.IO.File.Exists(path))
            {
                glSet.ReadXml(path);
            }
            else
            {
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2000", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2001", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2002", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2003", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2004", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2005", "590101");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2006", "590102");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2007", "590103");
                glSet.SpecialGLCode.AddSpecialGLCodeRow("2008", "590104");
                if (System.DateTime.Now.Year == 2010)
                {
                    glSet.SpecialGLCode.AddSpecialGLCodeRow("2009", "590105");
                }
                if (System.DateTime.Now.Year == 2011)
                {
                    glSet.SpecialGLCode.AddSpecialGLCodeRow("2010", "590106");
                }
                glSet.SpecialGLCode.AddSpecialGLCodeRow(System.DateTime.Now.Year.ToString(), "3131");
                glSet.WriteXml(path);
            }

            List<SpecialGLCodeItem> glCodes = new List<SpecialGLCodeItem>();
            foreach (SpecifyGLCode.SpecialGLCodeRow row in glSet.SpecialGLCode.Rows)
            {
                SpecialGLCodeItem item = new SpecialGLCodeItem();
                item.GLCode = row.GLCode;
                item.Year = row.Year;
                glStr = glStr + row.GLCode + ",";
                glCodes.Add(item);
            }
            return glCodes;
        }
    }


    class SpecialGLCodeItem
    {

        string year;

        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        string gLCode;

        public string GLCode
        {
            get { return gLCode; }
            set { gLCode = value; }
        }
    }
}
