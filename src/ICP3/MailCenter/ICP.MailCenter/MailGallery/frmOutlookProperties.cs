using System;
using System.Windows.Forms;

namespace ICP.MailCenter.UI
{
    public partial class frmOutlookProperties : Form
    {
        public frmOutlookProperties(string nickName, string mailAdd, string type)
            : this()
        {
            this.txtNickName.Text = nickName;
            this.txtMail.Text = mailAdd;
            this.txtType.SelectedText = type;
        }

        public frmOutlookProperties()
        {
            InitializeComponent();
            InitControls();
            RegisterEvents();
            this.Disposed += delegate { DisposedComponent(); };
        }

        private void DisposedComponent()
        {
            this.FormClosing -= new FormClosingEventHandler(frmOutlookProperties_FormClosing);
            this.btnClose.Click -= new System.EventHandler(this.btnClose_Click);
            this.btnAddContact.Click -= new EventHandler(btnAddContact_Click);
        }

        void InitControls()
        {
            string isEnglish = ClientUtility.olCultrue;
            if (isEnglish == "en-us")
            {
                this.Text = "E-mail Properties";
                this.lblNick.Text = "(&D)isplay Name:";
                this.lblMail.Text = "E-(&m)ail Address:";
                this.lblType.Text = "E-mail (&T)ype:";
                this.btnAddContact.Text = "Add to AddressBok(&A)";
                this.btnClose.Text = "Close(&C)";
            }
        }

        void RegisterEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frmOutlookProperties_FormClosing);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnAddContact.Click += new EventHandler(btnAddContact_Click);
        }


        void btnAddContact_Click(object sender, EventArgs e)
        {
            MailListPresenter.AddToContact(this.txtNickName.Text, this.txtMail.Text);
        }

        void frmOutlookProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOutlookProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
