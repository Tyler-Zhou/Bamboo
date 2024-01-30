#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/14 星期三 18:31:43
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
using ICP.FCM.OtherBusiness.UI.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public class OBBSearchPart : OBSearchPart
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
                return true;
            }
        }

        /// <summary>
        /// 选择默认操作口岸
        /// </summary>
        public override bool CheckedDefaultCompany
        {
            get { return true; }
        }

        /// <summary>
        /// 可用操作口岸列表
        /// </summary>
        public override List<OrganizationEntry> CompanyList
        {
            get
            {
                return userCompanyList ??
                       (userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == LocalOrganizationType.Company).Select((item => new OrganizationEntry { ID = item.ID, CName = item.CShortName, EName = item.EShortName })).ToList());
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public override int[] OTOperationType
        {
            get { return new[] { 1, 2, 3, 4}; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 设置揽货人相关信息
        /// </summary>
        public override void SetSalesInfo()
        {
            ICPCommUIHelper.BindDepartmentByAll(tcbSalesDepartment);
        }
        #endregion
    }
}
