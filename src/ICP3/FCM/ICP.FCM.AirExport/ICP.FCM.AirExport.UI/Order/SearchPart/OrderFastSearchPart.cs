using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;


namespace ICP.FCM.AirExport.UI.Order
{
    public class OrderFastSearchPart : FastSearchPart
    {
        public IAirExportService AirExportService
        {
            get
            {
                return ServiceClient.GetService<IAirExportService>();
            }
        }
        protected override void OnClickMore()
        {
            Workitem.Commands[AEOrderCommandConstants.Command_ShowSearch].Execute();
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
                    List<UserOrganizationTreeList> userCompanyList = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);

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
                    return LocalData.UserInfo.LoginID;
            }
        }

        public override object GetData()
        {
            List<UserOrganizationTreeList> userOrganizationTrees = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
            UserOrganizationTreeList orginazation = userOrganizationTrees.Find(o => o.IsDefault);
            List<Guid> companyIDs = new List<Guid>();
            foreach (var item in userOrganizationTrees)
            {
                companyIDs.Add(item.ID);
            }

            List<AirOrderList> list =
                AirExportService.GetAirOrderListForFaster(companyIDs.ToArray()
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
                                                    , true
                                                    , 100);

                return list;
        }

        private void InitializeComponent()
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
            // cmbNoSearchType
            // 
            // 
            // txtNo
            // 
            // 
            // cmbCustomerSearchType
            // 
            // 
            // cmbPortSearchType
            // 
            // 
            // cmbDateSearchType
            // 
            // 
            // cmbDateType
            // 
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
            Name = "OrderFastSearchPart";
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
