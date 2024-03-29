﻿using System;
using System.ComponentModel;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.Comm.OperationTypeSearchParts
{
    [ToolboxItem(false)]
    public partial class OESearchPart : OperationTypeSearchPart
    {
        #region init
        public OESearchPart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chkETA.CheckedChanged += delegate { dteETATo.Enabled = dteETAFrom.Enabled = chkETA.Checked ; };
            chkETDDate.CheckedChanged += delegate { dteETDTo.Enabled = dteETDFrom.Enabled = chkETDDate.Checked; };
        }
        public override void Clear()
        {
            chkETDDate.Checked = chkETA.Checked = false;
            txtSONo.Text = txtVessel.Text = txtVoayeNo.Text = string.Empty;
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

            parameter.BlNo = BLNo;
            parameter.CtnNo = CtnNo;

            parameter.VoayeNo = txtVoayeNo.Text.Trim();
            parameter.Vessel = txtVessel.Text.Trim();
            parameter.SONo = txtSONo.Text.Trim();

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


    }
}
