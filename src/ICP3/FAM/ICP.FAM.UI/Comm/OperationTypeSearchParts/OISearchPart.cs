using System;
using System.ComponentModel;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Comm.OperationTypeSearchParts
{
    [ToolboxItem(false)]
    public partial class OISearchPart : OperationTypeSearchPart
    {
        #region init

        public OISearchPart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chkETA.CheckedChanged += delegate { dteETATo.Enabled = dteETAFrom.Enabled = chkETA.Checked; };
            chkETDDate.CheckedChanged += delegate { dteETDTo.Enabled = dteETDFrom.Enabled = chkETDDate.Checked; };
            SetToolTips();
        }
        public override void Clear()
        {
            chkETDDate.Checked = chkETA.Checked = false;
            txtVessel.Text = txtVoayeNo.Text = string.Empty;
        }


        #endregion
        #region

        public DateTime? ETDFrom
        {
            get
            {
                if (chkETDDate.Checked == false || dteETDFrom.DateTime == DateTime.MinValue) return null;
                else return dteETDFrom.DateTime.Date;
            }
        }

        public DateTime? ETDTo
        {
            get
            {
                if (chkETDDate.Checked == false || dteETDTo.DateTime == DateTime.MinValue) return null;
                else return FAMUtility.GetEndDate(dteETDTo.DateTime);
            }
        }

        public DateTime? ETAFrom
        {
            get
            {
                if (chkETA.Checked == false || dteETAFrom.DateTime == DateTime.MinValue) return null;
                else return dteETAFrom.DateTime.Date;
            }
        }

        public DateTime? ETATo
        {
            get
            {
                if (chkETA.Checked == false || dteETATo.DateTime == DateTime.MinValue) return null;
                else return FAMUtility.GetEndDate(dteETATo.DateTime);
            }
        }
        #endregion

        public override OperationParameter GetOperationParameter()
        {
            OceanParameter parameter = new OceanParameter();

            parameter.IsAPCCFM = cheIsAPCCFM.Checked;
            parameter.IsARC = chkIsARC.Checked;
            parameter.IsRBLD = chkRbld.Checked;

            parameter.BlNo = BLNo;
            parameter.CtnNo = CtnNo;

            parameter.VoayeNo = txtVoayeNo.Text.Trim();
            parameter.Vessel = txtVessel.Text.Trim();
            parameter.SONo = string.Empty;

            parameter.ETDFrom = ETDFrom;
            parameter.ETDTo = ETDTo;
            parameter.ETAFrom = ETAFrom;
            parameter.ETATo = ETATo;
            return parameter;
        }

        private void chkETDDate_CheckedChanged(object sender, EventArgs e)
        {
            dteETDFrom.Enabled = dteETDTo.Enabled = chkETDDate.Checked;
        }

        private void chkETA_CheckedChanged(object sender, EventArgs e)
        {
            dteETAFrom.Enabled = dteETATo.Enabled = chkETA.Checked;
        }

        private void SetToolTips() 
        {
            string  apccfm= LocalData.IsEnglish?"A/P Fees are confirmed":"已确认应付承运人运费";
            string arc = LocalData.IsEnglish ? "Agree Release Cargo" : "同意放货";
            string rbld = LocalData.IsEnglish ? "HBL is released" : "HBL 已放单";

            cheIsAPCCFM.ToolTip = apccfm;
            chkIsARC.ToolTip = arc;
            chkRbld.ToolTip = rbld;
        }

    }
}
