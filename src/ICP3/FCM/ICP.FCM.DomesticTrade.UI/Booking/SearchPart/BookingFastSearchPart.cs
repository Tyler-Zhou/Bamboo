using System;
using System.Collections.Generic;
using System.Drawing;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;

using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;
namespace ICP.FCM.DomesticTrade.UI.Booking
{
    [ToolboxItem(false)]
    public class BookingFastSearchPart : FastSearchPart
    {
        public override object GetData()
        {
            //try
            //{
                List<DTBookingList> list = oeService.GetDTBookingListForFaster(CompanyIDs
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
                                                        ,true
                                                        , 100,
                                                        LocalData.IsEnglish);

                return list;
            //}
            //catch (Exception ex) 
            //{ 
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; 
            //}
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
            Workitem.Commands[DTBookingCommandConstants.Command_ShowSearch].Execute();
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
            // panelControl2
            // 
            panelControl2.Size = new Size(877, 36);
            // 
            // panel1
            // 
            panel1.Size = new Size(873, 32);
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
            // BookingFastSearchPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "BookingFastSearchPart";
            Size = new Size(877, 36);
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
