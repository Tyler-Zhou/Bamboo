using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using System.Windows.Forms;

namespace ICP.FCM.AirExport.UI.Booking.Finder
{
    public class BookingS_FinderListPart : BookingListPart
    {
        public BookingS_FinderListPart()
            : base()
        {
           gvMain.KeyDown += new KeyEventHandler(gvMain_KeyDown);
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsDisposed) return;

                if (CurrentRow != null && Selected != null)
                {
                    Selected(this, CurrentRow);
                    FindForm().Close();
                }
            }
        }

        protected override void GvMainDoubleClick()
        {
            if (CurrentRow != null) Workitem.Commands[OEBookingFinderCommandConstants.Command_Confirm].Execute(); 
        }

        public override event SelectedHandler Selected;

        [CommandHandler(OEBookingFinderCommandConstants.Command_Confirm)]
        public void Command_Confirm(object sender, EventArgs e)
        {
            if (IsDisposed) return;

            if (CurrentRow != null && Selected != null)
            {
                Selected(this, CurrentRow);
                FindForm().Close();
            }
        }

        [CommandHandler(OEBookingFinderCommandConstants.Command_AddData)]
        public new void Command_AddData(object sender, EventArgs e)
        {
            AddData();
        }
        [CommandHandler(OEBookingFinderCommandConstants.Command_CopyData)]
        public new void Command_CopyData(object sender, EventArgs e)
        {
            CopyData();
        }
        [CommandHandler(OEBookingFinderCommandConstants.Command_EditData)]
        public new void Command_EditData(object sender, EventArgs e)
        {
            EditData();
        }
    }
}
