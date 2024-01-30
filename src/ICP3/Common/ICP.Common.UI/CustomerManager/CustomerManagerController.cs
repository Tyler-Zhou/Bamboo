//-----------------------------------------------------------------------
// <copyright file="CustomerManagerEditPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using sm = ICP.Sys.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 客户管理-控制器
    /// <remarks>
    /// 控制器主要有两个功能(一个服务端代理)
    /// 1:封装场景的业务逻辑.
    /// 2:封装场景的业务所有验证规则.
    /// </remarks>
    /// </summary>
    public class CustomerManagerController : Controller
    {
        #region 注入服务

        /// <summary>
        /// 客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        /// <summary>
        /// 地点管理服务
        /// </summary>
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        /// <summary>
        /// 公司配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// 基础数据服务
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        #endregion


        #region 验证逻辑

        CustomerManagerRuleManager _ruleManger = null;
        public CustomerManagerRuleManager RuleManager
        {
            get
            {
                if (_ruleManger == null)
                {
                    _ruleManger = new CustomerManagerRuleManager();
                }

                return _ruleManger;
            }
        }

        #endregion

        #region 与服务交互的逻辑

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="codeApplyState">客户审核状态</param>
        /// <param name="codeApplicantCompanyID">申请人所在公司</param>
        /// <param name="applyTimeFrom">申请时间-开始</param>
        /// <param name="applyTimeTo">申请时间-结束</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        public List<CustomerList> GetCustomerList(
            string code,
            string name,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType? customerType,
            bool? isAgentOfCarrier,
            CustomerCodeApplyState? codeApplyState,
            Guid? areaID,
            DateTime? applyTimeFrom,
            DateTime? applyTimeTo,
            int maxRecords)
        {
            return this.CustomerService.GetCustomerListByList(
                 code,
                 name,
                 address,
                 tel,
                 fax,
                 eMail,
                 countryID,
                 provinceID,
                 customerState,
                 customerType,
                 isAgentOfCarrier,
                 codeApplyState,
                 areaID,
                 applyTimeFrom,
                 applyTimeTo,
                 maxRecords);
        }

        /// <summary>
        /// 获取客户列表(客户名称检查时用)
        /// </summary>
        /// <param name="name">全称</param>       
        /// <returns>返回客户列表</returns>
        public List<CustomerList> GetCustomerListForName(string name)
        {
            return this.CustomerService.GetCustomerListForName(name);
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <returns>返回客户信息</returns>
        public CustomerInfo GetCustomerInfo(Guid id)
        {
            CustomerInfo customer = this.CustomerService.GetCustomerInfo(id);

            return customer;
        }

        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回联系人列表</returns>
        public List<CustomerContactList> GetCustomerContactList(Guid id)
        {
            List<CustomerContactList> contactList = this.CustomerService.GetCustomerContactList(id);

            return contactList;
        }

        /// <summary>
        /// 获取合作伙伴列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回合作伙伴列表</returns>
        public List<CustomerPartnerList> GetCustomerPartnerList(Guid id)
        {
            List<CustomerPartnerList> partnerList = this.CustomerService.GetCustomerPartnerList(id);
            return partnerList;
        }

        /// <summary>
        /// 获取客户合并列表
        /// </summary>
        /// <param name="mainCustomerID">主客户ID</param>
        /// <returns>返回</returns>
        public List<CustomerCombineList> GetCustomerCombineList(Guid id)
        {
            List<CustomerCombineList> list = this.CustomerService.GetCustomerCombineList(id);
            return list;
        }

        /// <summary>
        /// 获取客户备注列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回客户备注列表</returns>
        public List<CustomerMemoList> GetCustomerMemoList(Guid id)
        {
            List<CustomerMemoList> list = this.CustomerService.GetCustomerMemoList(id);
            return list;
        }     

         /// <summary>
        /// 保存客户备注
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID</param>
        /// <param name="subjects">主题</param>
        /// <param name="contents">内容</param>
        /// <param name="types">类型</param>
        /// <param name="types">类型</param>
        /// <param name="prioritys">优先级</param>
        /// <param name="isShowUsers">显示用户</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveCustomerMemoInfo(
             Guid customerID,
             Guid?[] ids,
             string[] subjects,
             string[] contents,
             MemoType[] types,
             MemoPriority[] prioritys,
             bool[] isShowCustomers,
             bool[] isShowUsers,
             Guid saveByID,
             DateTime?[] updateDates)
        {
            ManyResultData result = this.CustomerService.SaveCustomerMemoInfo(
            customerID,
            ids,
            subjects,
            contents,
            types,
            prioritys,
            isShowCustomers,
            isShowUsers,
            saveByID,
            updateDates);

            return result;
        }

        /// <summary>
        /// 删除备注
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveCustomerMemoInfo(
             Guid id,
              Guid removeByID,
             DateTime? updateDate)
        {
            this.CustomerService.RemoveCustomerMemoInfo(id, removeByID, updateDate);
        }

        /// <summary>
        /// 取消合并客户
        /// </summary>
        /// <param name="customerIDs">取消客户列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">取消客户的数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CancelCombineCustomers(
             Guid[] customerIDs,
             Guid cancelByID,
             DateTime?[] updateDates)
        {
            ManyResultData result = this.CustomerService.CancelCombineCustomers(
             customerIDs,
             cancelByID,
             updateDates);

            return result;
        }

         /// <summary>
        /// 合并客户
        /// </summary>
        /// <param name="mainCustomerID">主客户</param>
        /// <param name="customerIDs">要被合并的客户</param>
        /// <param name="combineByID">合并人</param>
        /// <param name="updateDates">被何必客户的数据版本</param>
        /// <returns>返回ManyResultData</returns>
     public ManyResultData CombineCustomers(
           Guid mainCustomerID,
           Guid[] customerIDs,
           Guid combineByID,
           DateTime?[] updateDates)
       {
           ManyResultData result = this.CustomerService.CombineCustomers(
           mainCustomerID,
           customerIDs,
           combineByID,
           updateDates);

           return result;
       }

        /// <summary>
        /// 保存合作伙伴信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID列表</param>
        /// <param name="partnerIDs">合作伙伴列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveCustomerPartnerInfo(
            Guid customerID,
            Guid?[] ids,
            Guid[] partnerIDs,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ManyResultData result = this.CustomerService.SaveCustomerPartnerInfo(
            customerID,
            ids,
            partnerIDs,
            saveByID,
            updateDates);

            return result;
        }

        /// <summary>
        /// 删除合作伙伴
        /// </summary>
        /// <param name="ids">合作伙伴列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveCustomerPartnerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            this.CustomerService.RemoveCustomerPartnerInfo(ids, removeByID, updateDates);
        }

        /// <summary>
        /// 更改客户联系人有效状态
        /// </summary>
        /// <param name="id">ID列表</param>
        /// <param name="isValid">是否有效列表</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData ChangeCustomerContactState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            ManyResultData data = this.CustomerService.ChangeCustomerContactState(id, isValid, changeByID, updateDate);

            return data;
        }

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="customer">客户对象</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveCustomerInfo(CustomerInfo customer)
        {
            //验证规则
            if (customer.Validate() == false)
            {
                return null;
            }

            //提交数据
            SingleResultData result = this.CustomerService.SaveCustomerInfo(
                 customer.ID,
                  customer.Code,
                  customer.KeyWord,
                  customer.CShortName,
                  customer.EShortName,
                  customer.CName,
                  customer.EName,
                  customer.CBillName,
                  customer.EBillName,
                  customer.CAddress,
                  customer.EAddress,
                  customer.CountryID,
                  customer.ProvinceID,
                  customer.CityID,
                  customer.EnterpriseCodeType,
                  customer.EnterpriseCode,
                  customer.PostCode,
                  customer.Tel1,
                  customer.Tel2,
                  customer.Fax,
                  customer.EMail,
                  customer.Homepage,
                  customer.TaxIdType,
                  customer.TaxIdNo,
                  customer.BankAccountNo,
                  customer.CreditLimit,
                  customer.Term,
                  customer.TradeTermID,
                  customer.PaymentTypeID,
                  customer.Type,
                  customer.IsAgentOfCarrier,
                  customer.FIRMCODE,
                  customer.Remark,
                  LocalData.UserInfo.LoginID,
                  customer.IsCompany,
                  customer.UpdateDate);

            return result;
        }

        /// <summary>
        /// 保存客户联系人信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID列表</param>
        /// <param name="cNames">中文名列表</param>
        /// <param name="eNames">英文名列表</param>
        /// <param name="departments">部门列表</param>
        /// <param name="positions">职位列表</param>
        /// <param name="tels">电话列表</param>
        /// <param name="faxs">传真列表</param>
        /// <param name="mobiles">手机列表</param>
        /// <param name="eMails">邮件列表</param>
        /// <param name="remarks">批注列表</param>
        /// <param name="types">类型列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveCustomerContactInfo(
            Guid customerID,
            Guid?[] ids,
            string[] cNames,
            string[] eNames,
            string[] departments,
            string[] positions,
            string[] tels,
            string[] faxs,
            string[] mobiles,
            string[] eMails,
            string[] remarks,
            CustomerContactType[] types,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ManyResultData result = this.CustomerService.SaveCustomerContactInfo(
            customerID,
            ids,
            cNames,
            eNames,
            departments,
            positions,
            tels,
            faxs,
            mobiles,
            eMails,
            remarks,
            types,
            saveByID,
            updateDates);

            return result;
        }

        /// <summary>
        /// 获取国家,省份列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="countryId">国家</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回省份列表</returns>
        public List<CountryProvinceList> GetCountryProvinceList(
            string code,
            string name,
            Guid? countryId,
            bool? isValid,
            int maxRecords)
        {
            List<CountryProvinceList> results = this.GeographyService.GetCountryProvinceList
                (code,
                 name,
                 countryId,
                 isValid,
                 maxRecords);

            return results;
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="countryID">国家ID</param>
        /// <param name="provinceID">省份</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回城市列表</returns>
        public List<LocationList> GetLocationList(
            Guid? countryID,
            Guid? provinceID,
            bool? isValid)
        {
            List<LocationList> results = this.GeographyService.GetLocationList(
                string.Empty,
                countryID,
                provinceID,
                false,
                false,
                true,
                isValid,
                0);

            return results;
        }


        /// <summary>
        /// 获取公司配置里的公司列表
        /// </summary>
        /// <returns></returns>
        public List<sm.OrganizationList> GetConfigureCompanyList()
        {
            List<ConfigureList> results = this.ConfigureService.GetConfigureListByVaid(true);

            List<sm.OrganizationList> companys = (from c in results
                                                  select new sm.OrganizationList
                                                  {
                                                      ID = c.CompanyID,
                                                      Code = c.ShortCode,
                                                      CShortName = c.CompanyName,
                                                      EShortName = c.CompanyName
                                                  }).ToList();
            return companys;

        }


        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回字典列表</returns>
        public List<DataDictionaryList> GetDataDictionaryList(
            DataDictionaryType? type,
            bool? isValid)
        {
            List<DataDictionaryList> results = this.TransportFoundationService.GetDataDictionaryList(
                string.Empty,
                string.Empty,
                type,
                isValid,
                0);

            return results;
        }

        /// <summary>
        /// 获取最近的客户申请信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回客户审核信息</returns>
        public CustomerConfirmInfo GetLatelyCustomerConfirmInfo(Guid customerID)
        {
            CustomerConfirmInfo data = this.CustomerService.GetLatelyCustomerConfirmInfo(customerID);
            return data;
        }

        /// <summary>
        /// 申请客户信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="applicantRemark">申请批注</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ApplyCustomerCode(
            Guid customerID,
            Guid applicantID,
            string applicantRemark,
            DateTime? updateDate)
        {
            SingleResultData data = this.CustomerService.ApplyCustomerCode(
            customerID,
            applicantID,
            applicantRemark,
            updateDate);

            return data;
        }

        /// <summary>
        /// 保存发票信息
        /// </summary>
        /// <param name="customerInvoiceTitle"></param>
        /// <returns></returns>
        public SingleResultData SaveCustomerInvoiceTitleInfo(CustomerInvoiceTitleInfo customerInvoiceTitle)
        {
            SingleResultData data = this.CustomerService.SaveCustomerInvoiceTitleInfo(
                          customerInvoiceTitle.CustomerID,
                          customerInvoiceTitle.CompanyID,
                          customerInvoiceTitle.ID,
                          customerInvoiceTitle.Code,
                          customerInvoiceTitle.InvoiceType,
                          customerInvoiceTitle.Name,
                          customerInvoiceTitle.TaxNo,
                          customerInvoiceTitle.AddressTel,
                          customerInvoiceTitle.BankAccountNo,
                          customerInvoiceTitle.IsValid,
                          LocalData.UserInfo.LoginID,
                          customerInvoiceTitle.UpdateDate );

            return data;
        }
        /// <summary>
        /// 获得客户发票抬头列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleList(Guid customerID, Guid companyID)
        {
            List<CustomerInvoiceTitleInfo> data = this.CustomerService.GetCustomerInvoiceTitleList(customerID, companyID);

            return data;
        }

        #endregion
    }
}
