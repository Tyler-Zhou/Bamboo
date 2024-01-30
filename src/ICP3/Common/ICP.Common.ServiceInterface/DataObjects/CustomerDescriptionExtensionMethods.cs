#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/15 星期五 19:53:21
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Common.ServiceInterface.CompositeObjects;

namespace ICP.Common.ServiceInterface.DataObjects
{
    using Framework.CommonLibrary.Common;

    /// <summary>
    /// CustomerDescription扩展方法
    /// </summary>
    public static class CustomerDescriptionExtensionMethods
    {
        /// <summary>
        /// 客户描述 >>> 新客户描述
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static CustomerDescriptionForNew ConvertToNew(this CustomerDescription input)
        {
            CustomerDescriptionForNew newCustomer = new CustomerDescriptionForNew
            {
                Name = input.Name,
                Address = input.Address,
                Country = input.Country,
                EnterpriseCodeType ="",
                EnterpriseCode = "",
                City = input.City,
                Tel = input.Tel,
                Fax  = input.Fax,
                Contact = input.Contact,
                Remark = input.Remark,
            };
            return newCustomer;
        }
    }

    /// <summary>
    /// CustomerDescriptionForNew扩展方法
    /// </summary>
    public static class CustomerDescriptionForNewExtensionMethods
    {
        /// <summary>
        /// 客户描述 >>> 新客户描述
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static CustomerDescription ConvertToOriginal(this CustomerDescriptionForNew input)
        {
            CustomerDescription newCustomer = new CustomerDescription
            {
                Name = input.Name,
                Address = input.Address,
                Country = input.Country,
                City = input.City,
                Tel = input.Tel,
                Fax = input.Fax,
                Contact = input.Contact,
                Remark = input.Remark,
            };
            return newCustomer;
        }

        /// <summary>
        /// 转换成保存对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static CustomerInfoSaveRequest ConvertToSaveRequest(this CustomerDescriptionForNew input)
        {
            CustomerInfoSaveRequest cSaveRequest= new CustomerInfoSaveRequest
            {
                keyword = input.Name,
                cshortname = input.Name,
                eshortname = input.Name,
                cname = input.Name,
                ename = input.Name,
                ebillname = input.Name,
                cbillname = input.Name,
                caddress = input.Address,
                eaddress = input.Address,
                countryid = input.CountryID,
                enterprisecodetype = input.EnterpriseCodeType,
                enterprisecode = input.EnterpriseCode,
                tel1 = input.Tel,
                fax = input.Fax,
            };

            return cSaveRequest;
        }
    }

    /// <summary>
    /// CustomerList扩展方法
    /// </summary>
    public static class CustomerListExtensionMethods
    {
        /// <summary>
        /// 客户对象 >>> 新客户描述
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isEnglish">中英环境</param>
        /// <returns></returns>
        public static CustomerDescriptionForNew ConvertToCustomerDescriptionNew(this CustomerList input,bool isEnglish)
        {
            CustomerDescriptionForNew newCustomer = new CustomerDescriptionForNew
            {
                Name = isEnglish ? input.EName : input.CName,
                Address = isEnglish ?input.EAddress:input.CAddress,
                Country = isEnglish ? input.CountryEName : input.CountryName,
                EnterpriseCodeType = input.EnterpriseCodeType,
                EnterpriseCode = input.EnterpriseCode,
                City = input.CityName,
                Tel = input.Tel1,
                Fax = input.Fax,
                Contact = "",
                Remark = input.Remark,
            };
            return newCustomer;
        }
    }
}
