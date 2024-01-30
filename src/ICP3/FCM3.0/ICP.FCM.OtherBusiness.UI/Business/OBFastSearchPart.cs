using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.OtherBusiness.UI.Common.Controls;
using ICP.Framework.ClientComponents.Controls;


namespace ICP.FCM.OtherBusiness.UI.Business
{
    public class OBFastSearchPart : FastSearchPart
    {
        protected override void OnClickMore()
        {
            Workitem.Commands[OrderCommandConstants.Command_ShowSearch].Execute();
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
            List<OtherBusinessList> list =
                OBService.GetOtherBusinessListForFaster(CompanyIDs
                                                    , base.PartNoSearchType
                                                    , txtNo.Text.Trim()
                                                    , base.PartCustomerSearchType
                                                    , stxtCustomer.Text.Trim()
                                                    , base.PartPortSearchType
                                                    , stxtPort.Text.Trim()
                                                    , base.PartDateSearchType
                                                    , SalesID
                                                    , null
                                                    , base.From
                                                    , base.To
                                                    , true
                                                    , 100
                                                    ,LocalData.IsEnglish);
            return list;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.cmbNoSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPortSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.stxtPort.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPort.Properties.Appearance.Options.UseBackColor = true;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            // 
            // OrderFastSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "OBFastSearchPart";
            ((System.ComponentModel.ISupportInitialize)(this.cmbNoSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPortSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

        }
    }
      
}
