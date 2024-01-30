using System;
using System.Drawing;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI
{
    public partial class CheckDateFromToEdit : DevExpress.XtraEditors.XtraUserControl
    {
        #region 属性


        public bool Checked
        {
            get
            {
                return chkEnabled.Checked;
            }
            set
            {
                chkEnabled.Checked=value;
            }
        }

        public DateTime? DateTimeFrom 
        { 
            get 
            {
                if (chkEnabled.Checked)
                    return dteFrom.DateTime.Date;
                else
                    return null;
            }
            set
            {
                dteFrom.EditValue = value;
            }
        }
        public DateTime? DateTimeTo
        {
            get
            {
                if (chkEnabled.Checked)
                    return dteTo.DateTime.Date;
                else
                    return null;
            }
            set
            {
                dteTo.EditValue = value;
            }
        }

        public string CheckBoxText 
        { 
            get { return chkEnabled.Text.Trim(); }
            set{ chkEnabled.Text = value; } 
        }
        public string LabFromText
        {
            get { return labFrom.Text.Trim(); }
            set { labFrom.Text = value; }
        }
        public string LabToText
        {
            get { return labTo.Text.Trim(); }
            set { labTo.Text = value; }
        }
        public Size DteToSize
        {
            get { return dteTo.Size; }
            set { dteTo.Size = value; }
        }
        public Size DteFromSize
        {
            get { return dteFrom.Size; }
            set { dteFrom.Size = value; }
        }
        public Point DteToLocation
        {
            get { return dteTo.Location; }
            set { dteTo.Location = value; }
        }
        public Point DteFromLocation
        {
            get { return dteFrom.Location; }
            set { dteFrom.Location = value; }
        }
        #endregion

        public CheckDateFromToEdit()
        {
            InitializeComponent();
            this.Load += new EventHandler(CheckDateFromToEdit_Load);

            if (LocalData.IsEnglish == false) SetCnText();
        }

        void CheckDateFromToEdit_Load(object sender, EventArgs e)
        {

            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date.AddMonths(-1);
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date;
        }

        void SetCnText()
        {
            this.dteFrom.Text = "从";
            this.dteTo.Text = "到";
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.dteFrom.Enabled = this.dteTo.Enabled = chkEnabled.Checked;
            if (chkEnabled.Checked)
            {
                this.dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(-1);
                this.dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            }
            else
            {
                this.dteFrom.Text = string.Empty;
                this.dteTo.Text = string.Empty;
            }

        }
    }
}
