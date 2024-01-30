using System;

namespace LongWin.DataWarehouseReport.ServiceInterface
{
    [Serializable]
    public class LedgerData
    {

        Guid id;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        Guid glId;

        public Guid GLId
        {
            get { return glId; }
            set { glId = value; }
        }
        string glCode;

        public string GLCode
        {
            get { return glCode; }
            set { glCode = value; }
        }
        string glDescription;

        public string GLDescription
        {
            get { return glDescription; }
            set { glDescription = value; }
        }


        DateTime makedate;

        public DateTime MakeDate
        {
            get { return makedate; }
            set { makedate = value; }
        }

        DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        decimal beginBalance;

        public decimal BeginBalance
        {
            get { return beginBalance; }
            set { beginBalance = value; }
        }
        decimal crAmt;

        public decimal CrAmt
        {
            get { return crAmt; }
            set { crAmt = value; }
        }
        decimal drAmt;

        public decimal DrAmt
        {
            get { return drAmt; }
            set { drAmt = value; }
        }
        decimal balance;

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        decimal orgAmt;

        public decimal OrgAmt
        {
            get { return orgAmt; }
            set { orgAmt = value; }
        }
        decimal rate;

        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }


        short billType;

        public short BillType
        {
            get { return billType; }
            set { billType = value; }
        }
        Guid billId;

        public Guid BillId
        {
            get { return billId; }
            set { billId = value; }
        }

        string billNo;

        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }

        string customerFinanceCode;

        public string CustomerFinanceCode
        {
            get { return customerFinanceCode; }
            set { customerFinanceCode = value; }
        }

        string operationNo;

        public string OperationNo
        {
            get { return operationNo; }
            set { operationNo = value; }
        }

        string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }


        string customerShortName;

        public string CustomerShortName
        {
            get { return customerShortName; }
            set { customerShortName = value; }
        }

        string departmentName;

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }
        string employeeName;

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        string voucherSeqNo;

        public string VoucherSeqNo
        {
            get { return voucherSeqNo; }
            set { voucherSeqNo = value; }
        }
    }
}
