using System;
using System.Collections.Generic;
using System.Linq;
using ICP.ReportCenter.UI.Comm;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    class VoucherExport
    {
        /// <summary>
        /// 验证客户财务代码
        /// </summary>
        /// <param name="UFDataList"></param>
        /// <param name="ledgerDataList"></param>
        /// <returns></returns>
        public static List<CheckVoucherDate> CheckFinceCode(List<UFData> UFDataList,
                                                            List<UFData> ufGLCodeList,
                                                            List<VoucherLedgerData> ledgerDataList)
        {
            List<CheckVoucherDate> list = new List<CheckVoucherDate>();

            foreach (VoucherLedgerData item in ledgerDataList)
            {
                int count=0;

                if (!string.IsNullOrEmpty(item.CustomerFinanceCode))
                {
                    //客户代码不为空，并且在用友中找不到客户资料
                    count = (from d in UFDataList where d.Code == item.CustomerFinanceCode select d.Code).Count();
                    if (count == 0)
                    {
                        CheckVoucherDate newItem = list.Find(delegate(CheckVoucherDate data) { return  data.Type == "客户" && data.Code == item.CustomerFinanceCode && data.Name == item.CustomerName; });
                        if (newItem == null)
                        {
                            newItem = new CheckVoucherDate();
                            newItem.VoucherNo = item.VoucherSeqNo;
                            newItem.Type = "客户";
                            newItem.Code = item.CustomerFinanceCode;
                            newItem.Name = item.CustomerName;
                            list.Add(newItem);
                        }
                        else
                        {
                            if (!newItem.VoucherNo.Contains(item.VoucherSeqNo))
                            {
                                newItem.VoucherNo = newItem.VoucherNo + "/" + item.VoucherSeqNo;
                            }                           
                        }
                    }
                }
                count = (from d in ufGLCodeList where d.Code == item.GLCode select d.Code).Count();
                if (count == 0)
                {
                    CheckVoucherDate newItem = list.Find(delegate(CheckVoucherDate data) { return data.VoucherNo == item.VoucherSeqNo && data.Type == "科目" && data.Code == item.CustomerFinanceCode && data.Name == item.CustomerName; });
                    if (newItem == null)
                     {
                         newItem = new CheckVoucherDate();
                         newItem.VoucherNo = item.VoucherSeqNo;
                         newItem.Type = "科目";
                         newItem.Code = item.GLCode;
                         newItem.Name = item.GLDescription;
                         list.Add(newItem);
                     }
                    else
                    {
                        if (!newItem.VoucherNo.Contains(item.VoucherSeqNo))
                        {
                            newItem.VoucherNo = newItem.VoucherNo + "/" + item.VoucherSeqNo;
                        } 
                    }
                }

            }

            return list;

        }
        /// <summary>
        /// 验证管理费用的代码
        /// </summary>
        /// <param name="UFDataList"></param>
        /// <param name="ledgerDataList"></param>
        /// <param name="uf2ICPList"></param>
        /// <returns></returns>
        public static List<CheckVoucherDate> CheckUserDepartCode(List<UFData> UFDataList,
                                                                  List<UFData> ufGLCodeList,
                                                                  List<UFData> ufDeptList,
                                                                  List<UFData> ufPersonList, 
                                                                  List<VoucherLedgerData> ledgerDataList,
                                                                  List<UFCode2ICP> uf2ICPList)
        {
            List<CheckVoucherDate> list = new List<CheckVoucherDate>();

            foreach (VoucherLedgerData ledgerData in ledgerDataList)
            {
                  int count = 0;
                  //客户
                  if (!string.IsNullOrEmpty(ledgerData.CustomerFinanceCode))
                  {

                      count=UFDataList.Count(c => c.Code == ledgerData.CustomerFinanceCode);
                      if (count == 0)
                      {
                        CheckVoucherDate newItem = list.Find(delegate(CheckVoucherDate data) { return data.VoucherNo == ledgerData.VoucherSeqNo && data.Type == "客户" && data.Code == ledgerData.CustomerFinanceCode && data.Name == ledgerData.CustomerName; });
                        if (newItem == null)
                        {
                                newItem = new CheckVoucherDate();
                                newItem.VoucherNo = ledgerData.VoucherSeqNo;
                                newItem.Type = "客户";
                                newItem.Code = ledgerData.CustomerFinanceCode;
                                newItem.Name = ledgerData.CustomerName;
                                list.Add(newItem);
                        }
                        else
                        {
                            if (!newItem.VoucherNo.Contains(ledgerData.VoucherSeqNo))
                            {
                                newItem.VoucherNo = newItem.VoucherNo + "/" + ledgerData.VoucherSeqNo;
                            } 
                        }
                      }
                  }
                  //科目
                  count = (from d in ufGLCodeList where d.Code == ledgerData.GLCode select d.Code).Count();
                  if (count == 0)
                  {
                      CheckVoucherDate newItem = list.Find(delegate(CheckVoucherDate data) { return data.VoucherNo == ledgerData.VoucherSeqNo && data.Type == "科目" && data.Code == ledgerData.CustomerFinanceCode && data.Name == ledgerData.CustomerName; });
                      if (newItem == null)
                      {
                          newItem = new CheckVoucherDate();
                          newItem.VoucherNo = ledgerData.VoucherSeqNo;
                          newItem.Type = "科目";
                          newItem.Code = ledgerData.GLCode;
                          newItem.Name = ledgerData.GLDescription;
                          list.Add(newItem);
                      }
                      else
                      {
                          if (!newItem.VoucherNo.Contains(ledgerData.VoucherSeqNo))
                          {
                              newItem.VoucherNo = newItem.VoucherNo + "/" + ledgerData.VoucherSeqNo;
                          }
                      }
                  }

                 //个人
                  if (!string.IsNullOrEmpty(ledgerData.EmployeeName)&&ledgerData.EmployeeName!="办公室")
                  {
                      //string name = "";
                      string personName =ledgerData.EmployeeName;
                      UFCode2ICP personInfo=uf2ICPList.Find(d => d.ICPName.ToUpper() == ledgerData.EmployeeName.ToUpper() && d.CompanyID == ledgerData.CompanyID && d.DataType== 2);
                      if (personInfo != null)
                      {
                          //是否有关联信息
                          personName = personInfo.UFCode;
                      }

                      //ufPersonList.ForEach(p => {
                      //    name = name + ";"+p.Code;
                      //});
                      count = ufPersonList.Count(c => c.Code.ToUpper().Trim() == personName.ToUpper().Trim());
                      if (count == 0)
                      {
                          CheckVoucherDate newItem = list.Find(delegate(CheckVoucherDate data) { return data.VoucherNo == ledgerData.VoucherSeqNo && data.Type == "个人" && data.Name == ledgerData.EmployeeName; });
                          if (newItem==null)
                          {
                              newItem = new CheckVoucherDate();
                              newItem.VoucherNo = ledgerData.VoucherSeqNo;
                              newItem.Type = "个人";
                              newItem.Code = string.Empty;
                              newItem.Name = ledgerData.EmployeeName;
                              list.Add(newItem);
                          }
                          else
                          {
                              if (!newItem.VoucherNo.Contains(ledgerData.VoucherSeqNo))
                              {
                                  newItem.VoucherNo = newItem.VoucherNo + "/" + ledgerData.VoucherSeqNo;
                              }
                          }
                      }
                  }
                 //部门
                  if (string.IsNullOrEmpty(ledgerData.EmployeeName) &&
                      !string.IsNullOrEmpty(ledgerData.DepartmentName)
                    && ledgerData.DepartmentName != "办公室")
                  {
                      string deptName = ledgerData.DepartmentName;
                      UFCode2ICP deptInfo = uf2ICPList.Find(d => d.ICPName == ledgerData.DepartmentName && d.CompanyID == ledgerData.CompanyID && d.DataType == 1);
                      if (deptInfo != null)
                      {
                          //是否有关联信息
                          deptName = deptInfo.UFCode;
                      }
                      count = ufDeptList.Count(c => c.Name == deptName);
                      if (count == 0)
                      {
                          CheckVoucherDate newItem = list.Find(delegate(CheckVoucherDate data) { return data.VoucherNo == ledgerData.VoucherSeqNo && data.Type == "部门" && data.Name == ledgerData.DepartmentName; });
                          if (newItem == null)
                          {
                               newItem = new CheckVoucherDate();
                               newItem.VoucherNo = ledgerData.VoucherSeqNo;
                               newItem.Type = "部门";
                               newItem.Code = string.Empty;
                               newItem.Name = ledgerData.DepartmentName;
                               list.Add(newItem);
                           }
                           else
                           {
                               if (!newItem.VoucherNo.Contains(ledgerData.VoucherSeqNo))
                               {
                                   newItem.VoucherNo = newItem.VoucherNo + "/" + ledgerData.VoucherSeqNo;
                               }
                           }
                      }
                  }
            }
            return list;
        }
        /// <summary>
        /// 导出凭证
        /// </summary>
        /// <param name="ledgerDataList">凭证数据</param>
        /// <param name="ExportType">导出格式(0:计提 1:实收实付)</param>
        /// <returns></returns>
        public static string BuilderPlanVoucher(List<VoucherLedgerData> ledgerDataList, 
            List<UFData> UFDataList,
            List<UFData> ufPersonList,
            List<UFCode2ICP> ufCode2ICPList,
            bool isCostFee)
        {
            if (UFDataList == null) UFDataList = new List<UFData>();
            if (ufPersonList == null) ufPersonList = new List<UFData>();

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
                                  OrgAmt = ld.OrgAmt
                                ,
                                  MakeDate=ld.MakeDate
                                ,
                                  BillDate = ld.Date
                                ,
                                  CompanyID=ld.CompanyID
                                ,
                                  BillNo = ld.BillNo
                                ,
                                  Rate = ld.Rate
                                ,
                                  BillType = ld.BillType
                                ,
                                  OperationNo = ld.OperationNo
                                ,
                                  VoucherSeqNo = ld.VoucherSeqNo
                                ,
                                  FinanceCode = ld.CustomerFinanceCode
                                ,
                                  GLCode = ld.GLCode
                                ,
                                  YM = ld.Date.ToString("yyMM")
                                ,
                                  CustomerName = ld.CustomerName
                                ,
                                  Remark = ld.Remark
                                ,
                                  DepartmentName = ld.DepartmentName
                                ,
                                  EmployeeName = ld.EmployeeName
                                ,
                                  OrgAmtIsZero = ld.OrgAmtIsZero
                              };

            string vouchers = string.Empty;
            decimal drAmount = 0.00m;
            decimal crAmount = 0.00m;
            decimal orgAmt = 0.00m;
            decimal rate = 0.00m;
            string ji = string.Empty;
            string departName = string.Empty, personName = string.Empty; 
            foreach (var voucher in voucherList)
            {
                departName = string.Empty;
                personName = string.Empty; 

                if (isCostFee)
                {
                    #region 管理成本
                    if (!string.IsNullOrEmpty(voucher.EmployeeName))
                    {
                        //ICP中的用户与用友中用户不一样
                        UFCode2ICP employeeInfo = ufCode2ICPList.Find(d => d.ICPName == voucher.EmployeeName && d.CompanyID==voucher.CompanyID&&d.DataType==2);
                        if (employeeInfo != null)
                        {
                            personName = employeeInfo.UFCode;
                        }
                        else
                        {
                            personName = voucher.EmployeeName;
                        }
                        //根据用户名字，找到用户在用友中的部门
                        UFData cus = ufPersonList.Find(d => d.Code == personName);
                        if (cus != null)
                        {
                            departName = cus.Name;
                        }
                        else
                        {
                            departName= voucher.DepartmentName;
                        }
                    }
                    if (string.IsNullOrEmpty(voucher.EmployeeName) &&
                       !string.IsNullOrEmpty(voucher.DepartmentName))
                    {
                        //有部门，但没有用户
                        UFCode2ICP deptInfo = ufCode2ICPList.Find(d => d.ICPName == voucher.DepartmentName && d.CompanyID == voucher.CompanyID&&d.DataType==1);
                        if (deptInfo != null)
                        {
                            departName = deptInfo.UFCode;
                        }
                        else
                        {
                            departName=voucher.DepartmentName;
                        }
                    }
                    #endregion
                }
                else
                { 
                    //实收实付&应收应付
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
                if (string.IsNullOrEmpty(voucher.Remark))
                {
                    ji = " ";
                }
                else
                {
                    //替换摘要中的换行符
                    ji = voucher.Remark.Replace(System.Environment.NewLine," ");//摘要
                }

                drAmount = voucher.drAmt;
                crAmount = voucher.crAmt;

                if (glstr.Contains(voucher.GLCode))
                {
                    orgAmt = Math.Abs(voucher.crAmt);
                    rate = 1.00m;
                    ji = voucher.OperationNo;
                }
                if (!voucher.OrgAmtIsZero)
                {
                    //if (orgAmt == 0.00m)
                    //{ 
                    //    if(drAmount!=0.00m)
                    //    {
                    //        orgAmt = drAmount;
                    //    }
                    //    else
                    //    {
                    //        orgAmt = crAmount;
                    //    }
                    //}
                }
              
                vouchers = vouchers
                  + voucher.MakeDate.ToString("yyyy-MM-dd")
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
                  + "," + "," + "," + "," + voucher.BillDate.ToString("yyyy-MM-dd") + ","
                  + departName
                  + "," + personName
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
            glStr = glStr + "," + "510101" + ",5101";
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
        public string Year { get; set; }
        public string GLCode { get; set; }
    }


    class CheckVoucherDate
    {
        /// <summary>
        /// 凭证号
        /// </summary>
        public string VoucherNo { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
