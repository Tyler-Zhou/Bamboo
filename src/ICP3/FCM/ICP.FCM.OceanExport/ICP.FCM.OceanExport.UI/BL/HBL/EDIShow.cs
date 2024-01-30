using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI.BL.HBL
{
    public partial class EDIShow : BasePart
    {
        public List<EDIShowValue> DataSourse;
        public EDIShow()
        {
            InitializeComponent();
        }

        private void EDIShow_Load(object sender, EventArgs e)
        {
            gcMain.DataSource = DataSourse;
            gvDetails.RefreshData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = System.Windows.Forms.DialogResult.OK;
            this.FindForm().Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void gvDetails_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            //int index = gvDetails.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            //GridGroupRowInfo.GroupText += gvDetails.GetRowCellValue(index,"BLNO");
        }

        private void gcMain_Click(object sender, EventArgs e)
        {

        }



    }
}
