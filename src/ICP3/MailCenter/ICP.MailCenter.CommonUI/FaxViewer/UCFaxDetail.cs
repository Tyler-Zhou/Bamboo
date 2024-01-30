using System;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.CommonUI
{
    public partial class UCFaxDetail : DevExpress.XtraEditors.XtraUserControl, IReadOnlyControl
    {
        private ICP.Message.ServiceInterface.Message _dataSource;
        /// <summary>
        /// 消息数据
        /// </summary>
        public ICP.Message.ServiceInterface.Message DataSource
        {
            get { return this._dataSource; }
            set
            {
                if (value == null)
                {
                    return;
                }
                this._dataSource = value;
                if (!LocalData.IsDesignMode)
                {
                    BindData();
                }
            }

        }
        private bool readOnly = false;
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly
        {
            get { return this.readOnly; }
            set
            {
                this.readOnly = value;
                SetChildControlReadOnly();

            }
        }
        /// <summary>
        /// 设置控件只读性
        /// </summary>
        public void SetChildControlReadOnly()
        {
            this.txtSubject.ReadOnly = this.txtSender.ReadOnly = this.txtSendTime.ReadOnly = this.txtCC.ReadOnly = this.txtTo.ReadOnly = this.readOnly;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCFaxDetail()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.pnlLayout.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.pnlLayout, true, null);
            }
            DevExpress.Skins.Skin currentSkin = DevExpress.Skins.CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            this.pnlLayout.BackColor = this.txtSubject.BackColor = this.txtSender.BackColor = this.txtSendTime.BackColor = this.txtTo.BackColor = this.txtCC.BackColor = this.btnFax.BackColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.Control);

            this.SizeChanged += new EventHandler(UCFaxDetail_SizeChanged);
            Load += (sender, e) =>
            {

                Locale();
            };
            this.Disposed += delegate {
                _dataSource = null;

            
            };
        }

        void UCFaxDetail_SizeChanged(object sender, EventArgs e)
        {
            this.txtSender.AutoHeight();
            this.txtTo.AutoHeight();
            this.txtCC.AutoHeight();
        }
        /// <summary>
        /// 本地化
        /// </summary>
        private void Locale()
        {
            if (LocalData.IsEnglish)
            {
                lblSubject.Text = "Subject";
                this.lblTo.Text = "To";
                this.lblCC.Text = "CC";
                this.lblSender.Text = "From";
                this.lblSendTime.Text = "Send Time";
                this.btnFax.Text = "Content";
            }
        }
        private void BindData()
        {
            this.txtSubject.Text = _dataSource.Subject;
            this.txtSender.Text = _dataSource.SendFrom;
            this.txtSendTime.Text = _dataSource.CreateDate.ToString();
            this.txtTo.Text = _dataSource.SendTo;
            this.txtCC.Text = _dataSource.CC;
            this.attachmentPanel.DataSource = _dataSource;
            if (!string.IsNullOrEmpty(_dataSource.CC))
            {

                ShowCCControl();

            }
            else
            {

                HideCCControl();

            }
            this.txtCC.Text = _dataSource.CC;
            if (this._dataSource.HasAttachment)
            {
                ShowAttachmentControl();

            }
            else
            {
                HideAttachmentControl();

            }
            this.attachmentPanel.DataSource = this._dataSource;


        }

        private void btnFax_Click(object sender, EventArgs e)
        {
            if (EventUtility.ShowBodyAction != null)
            {
                EventUtility.ShowBodyAction(btnFax);
            }
        }



        private void HideCCControl()
        {
            if (this.pnlLayout.Controls.Contains(this.lblCC))
            {
                this.pnlLayout.Controls.Remove(this.lblCC);
                pnlLayout.Controls.Remove(this.txtCC);
            }
        }
        private void ShowCCControl()
        {
            if (!this.pnlLayout.Controls.Contains(this.lblCC))
            {
                this.pnlLayout.Controls.Add(this.lblCC, 0, 4);
                this.pnlLayout.Controls.Add(this.txtCC, 1, 4);
            }
        }

        public void HideAttachmentControl()
        {
            if (this.pnlLayout.Controls.Contains(this.attachmentPanel))
            {
                pnlLayout.Controls.Remove(btnFax);
                pnlLayout.Controls.Remove(attachmentPanel);

            }
        }
        public void ShowAttachmentControl()
        {
            if (!this.pnlLayout.Controls.Contains(this.attachmentPanel))
            {
                pnlLayout.Controls.Add(this.btnFax, 0, 5);
                pnlLayout.Controls.Add(this.attachmentPanel, 1, 5);
            }
        }

        private void btnFax_MouseHover(object sender, EventArgs e)
        {
            btnFax.ToolTip = LocalData.IsEnglish ? "Click show the content" : "单击显示内容";
        }
    }
}
