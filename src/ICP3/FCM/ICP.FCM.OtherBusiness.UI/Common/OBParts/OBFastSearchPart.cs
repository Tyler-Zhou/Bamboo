using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;


namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 其他业务查询面板
    /// </summary>
    public class OBFastSearchPart : FastSearchPart
    {
        /// <summary>
        /// 添加业务类型
        /// </summary>
        public virtual AddType OTType
        {
            get { return AddType.OtherBusiness; }
        }

        /// <summary>
        /// 点击更多，显示查询面板
        /// </summary>
        protected override void OnClickMore()
        {
        }

        List<Guid> companyIDs = null;
        /// <summary>
        /// 可用口岸ID
        /// </summary>
        public override Guid[] CompanyIDs
        {
            get
            {
                if (companyIDs != null) return companyIDs.ToArray();
                companyIDs = new List<Guid>();
                List<UserOrganizationTreeList> userCompanyList = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);

                if (userCompanyList.Count == 0)
                {
                    throw new Exception(LocalData.IsEnglish ? "You have no rights to query data of any company. Please contat administrator." : "您没有权限查询任何操作口岸的数据，请联系管理员！");
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
        public override object GetData()
        {
            int[] otType = {1,2,3,4};
            if (OTType==AddType.OtherBusinessOrder)
            {
                otType = new[] { 1, 2, 3, 4, 5, 6 };
            }
            List<OtherBusinessList> list =
                OtherBusinessService.GetOtherBusinessListForFaster(CompanyIDs
                                                    , otType
                                                    , base.PartNoSearchType
                                                    , txtNo.Text.Trim()
                                                    , base.PartCustomerSearchType
                                                    , stxtCustomer.Text.Trim()
                                                    , base.PartPortSearchType
                                                    , stxtPort.Text.Trim()
                                                    , base.PartDateSearchType
                                                    , Guid.Empty
                                                    , null
                                                    , base.From
                                                    , base.To
                                                    , true
                                                    , 100);
            return list;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void InitializeComponent()
        {
            ((ISupportInitialize)(cmbNoSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(txtNo.Properties)).BeginInit();
            ((ISupportInitialize)(cmbCustomerSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(cmbPortSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(cmbDateSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(cmbDateType.Properties)).BeginInit();
            ((ISupportInitialize)(panelControl2)).BeginInit();
            panelControl2.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)(stxtPort.Properties)).BeginInit();
            ((ISupportInitialize)(stxtCustomer.Properties)).BeginInit();
            SuspendLayout();
            // 
            // stxtPort
            // 
            stxtPort.Properties.Appearance.BackColor = Color.White;
            stxtPort.Properties.Appearance.Options.UseBackColor = true;
            // 
            // stxtCustomer
            // 
            stxtCustomer.Properties.Appearance.BackColor = Color.White;
            stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            // 
            // OrderFastSearchPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "OBFastSearchPart";
            ((ISupportInitialize)(cmbNoSearchType.Properties)).EndInit();
            ((ISupportInitialize)(txtNo.Properties)).EndInit();
            ((ISupportInitialize)(cmbCustomerSearchType.Properties)).EndInit();
            ((ISupportInitialize)(cmbPortSearchType.Properties)).EndInit();
            ((ISupportInitialize)(cmbDateSearchType.Properties)).EndInit();
            ((ISupportInitialize)(cmbDateType.Properties)).EndInit();
            ((ISupportInitialize)(panelControl2)).EndInit();
            panelControl2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((ISupportInitialize)(stxtPort.Properties)).EndInit();
            ((ISupportInitialize)(stxtCustomer.Properties)).EndInit();
            ResumeLayout(false);

        }
    }
      
}
