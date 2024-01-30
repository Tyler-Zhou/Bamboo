#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/13 星期二 14:07:02
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.FCM.OtherBusiness.UI.Common;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 虚拟页面(其他业务--订单管理）
    /// </summary>
    [ToolboxItem(false)]
    public class OBOrderSearchPart : OBSearchPart
    {
        #region 字段 & 属性
        /// <summary>
        /// 
        /// </summary>
        List<OrganizationEntry> userCompanyList = null;

        /// <summary>
        /// 业务号是否可用
        /// </summary>
        public override bool OperationNoEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 选择默认操作口岸
        /// </summary>
        public override bool CheckedDefaultCompany
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 可用操作口岸列表
        /// </summary>
        public override List<OrganizationEntry> CompanyList
        {
            get
            {
                return userCompanyList ??
                       (userCompanyList =
                           OrganizationService.GetOfficeList()
                               .Select(
                                   item =>
                                       new OrganizationEntry
                                       {
                                           ID = item.ID,
                                           CName = item.CShortName,
                                           EName = item.EShortName
                                       })
                               .ToList());
            }
        } 
        #endregion

        #region 方法
        /// <summary>
        /// 设置揽货人相关信息
        /// </summary>
        public override void SetSalesInfo()
        {
            if (LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices))
            {
                ICPCommUIHelper.BindDepartmentByAll(tcbSalesDepartment);
            }
            else
            {
                ICPCommUIHelper.BindCompanyByUser(tcbSalesDepartment, CheckState.Checked);
                mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            }
        }
        #endregion
    }
}
