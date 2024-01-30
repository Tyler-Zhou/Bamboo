using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.DomesticTrade.UI.Booking.Finder
{
    public class BookingS_FinderListPart : BookingListPart
    {
        protected override void GvMainDoubleClick()
        {
            if (CurrentRow != null) Workitem.Commands[DTBookingFinderCommandConstants.Command_Confirm].Execute(); 
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        [CommandHandler(DTBookingFinderCommandConstants.Command_Confirm)]
        public void Command_Confirm(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            if (CurrentRow != null && Selected != null)
            {
                Selected(this, CurrentRow);
                this.FindForm().Close();
            }
        }

        [CommandHandler(DTBookingFinderCommandConstants.Command_AddData)]
        public new void Command_AddData(object sender, EventArgs e)
        {
            AddData();
        }
        [CommandHandler(DTBookingFinderCommandConstants.Command_CopyData)]
        public new void Command_CopyData(object sender, EventArgs e)
        {
            CopyData();
        }
        [CommandHandler(DTBookingFinderCommandConstants.Command_EditData)]
        public new void Command_EditData(object sender, EventArgs e)
        {
            EditData();
        }
    }
}
