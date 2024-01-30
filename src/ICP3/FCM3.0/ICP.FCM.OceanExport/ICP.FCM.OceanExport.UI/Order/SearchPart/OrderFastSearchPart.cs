using System;
using System.Collections.Generic;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;

using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.Order
{
    public class OrderFastSearchPart : FastSearchPart
    {
        protected override void OnClickMore()
        {
            Workitem.Commands[OEOrderCommandConstants.Command_ShowSearch].Execute();
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

        Guid SalesID
        {
            get
            {
                //try
                //{
                //    return (Guid)this.Workitem.State["SalesId"];
                //}
                //catch
                //{
                    return LocalData.UserInfo.LoginID;
                //}
            }
        }

        public override object GetData()
        {
            //try
            //{
            int i = 0;
            if (string.IsNullOrEmpty(txtNo.Text.Trim()) && string.IsNullOrEmpty(stxtCustomer.Text.Trim()) && string.IsNullOrEmpty(stxtPort.Text.Trim()) && base.cmbDateType.SelectedIndex <= 0)
            {
                i = 100;
            }
                List<OceanOrderList> list =
                    oeService.GetOceanOrderListForFaster(null
                                                        , base.PartNoSearchType
                                                        , txtNo.Text.Trim()
                                                        , base.PartCustomerSearchType
                                                        , stxtCustomer.Text.Trim()
                                                        , base.PartPortSearchType
                                                        , stxtPort.Text.Trim()
                                                        , base.PartDateSearchType
                                                        , SalesID
                                                        , base.From
                                                        , base.To
                                                        ,true
                                                        , i);

                return list;
            //}
            //catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
        }
    }
      
}
