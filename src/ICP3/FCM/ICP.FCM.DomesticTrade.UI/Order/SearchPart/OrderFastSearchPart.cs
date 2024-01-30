using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;

using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.DomesticTrade.UI.Order
{
    public class OrderFastSearchPart : FastSearchPart
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                companyIDs = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnClickMore()
        {
            Workitem.Commands[DTOrderCommandConstants.Command_ShowSearch].Execute();
        }

        List<Guid> companyIDs = null;
        public override Guid[] CompanyIDs
        {
            get
            {
                if (companyIDs != null) return companyIDs.ToArray();
                else
                {
                    companyIDs = new List<Guid>();
                    List<UserOrganizationTreeList> userCompanyList = userService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);

                    if (userCompanyList.Count == 0)
                    {
                        throw new Exception(LocalData.IsEnglish ? "You have no rights to query data of any company. Please contat administrator.": "您没有权限查询任何操作口岸的数据，请联系管理员！");
                    }
                    foreach (var item in userCompanyList)
                    {
                        if (item.HasPermission)
                        {
                            companyIDs.Add(item.ID);
                        }
                    }
                    return companyIDs.ToArray();
                }
            }
        }

        public override object GetData()
        {

                List<DTOrderList> list =
                    oeService.GetDTOrderListForFaster(CompanyIDs.ToArray()
                                                        , base.PartNoSearchType
                                                        , txtNo.Text.Trim()
                                                        , base.PartCustomerSearchType
                                                        , stxtCustomer.Text.Trim()
                                                        , base.PartPortSearchType
                                                        , stxtPort.Text.Trim()
                                                        , base.PartDateSearchType
                                                        , LocalData.UserInfo.LoginID
                                                        , base.From
                                                        , base.To
                                                        ,true
                                                        , 100);

                return list;
        }
    }
      
}
