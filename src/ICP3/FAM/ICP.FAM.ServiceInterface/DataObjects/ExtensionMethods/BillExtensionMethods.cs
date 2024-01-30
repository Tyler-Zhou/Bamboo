using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 账单扩展方法
    /// </summary>
    public static class BillExtensionMethods
    {
        /// <summary>
        /// 转换成保存对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static SaveRequestBill ConvertToSaveRequest(this BillInfo input)
        {
            SaveRequestBill cSaveRequest = new SaveRequestBill
            {
                ID = input.ID,
                BillNo = input.No,
                CompanyID = input.CompanyID,
                FormID = input.FormID,
                FormType = input.FormType,
                CustomerID = input.CustomerID,
                CustomerDescription = input.CustomerDescription,
                CustomerRefNo = input.CustomerRefNo,
                Type = input.Type,
                ShipToID = input.ShipToID,
                AccountDate = input.AccountDate,
                DueDate = input.DueDate,
                AuditorID = input.AuditorID,
                AuditorEmail = input.AuditorEmail,
                State = input.State,
                IsVATInvoiced = input.IsVATInvoiced,
                TaxRate = input.Taxrate,
                Remark = input.Remark,
                UpdateDate = input.UpdateDate,
            };

            return cSaveRequest;
        }
    }
}
