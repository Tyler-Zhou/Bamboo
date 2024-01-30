using System;
using System.Collections.Generic;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;

using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;
namespace ICP.FCM.AirExport.UI.Booking
{
    [ToolboxItem(false)]
    public class BookingFastSearchPart : FastSearchPart
    {
        public override object GetData()
        {
            try
            {
                List<AirBookingList> list = aeService.GetAirBookingListForFaster(CompanyIDs
                                                        , base.PartNoSearchType
                                                        , txtNo.Text.Trim()
                                                        , base.PartCustomerSearchType
                                                        , stxtCustomer.Text.Trim()
                                                        , base.PartPortSearchType
                                                        , stxtPort.Text.Trim()
                                                        , base.PartDateSearchType
                                                        , OwnerID
                                                        , base.From
                                                        , base.To
                                                        , true
                                                        , 100);

                return list;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null;
            }
        }

        Guid? OwnerID
        {
            get
            {
                //if (this.Workitem.State["BookingerId"] == null)
                //{
                return LocalData.UserInfo.LoginID;
                //}
                //return this.Workitem.State["BookingerId"] as Guid?;
            }
        }

        protected override void OnClickMore()
        {
            Workitem.Commands[AEBookingCommandConstants.Command_ShowSearch].Execute();
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
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(877, 36);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(873, 32);
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
            // BookingFastSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "BookingFastSearchPart";
            this.Size = new System.Drawing.Size(877, 36);
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
