using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Business.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.CommonUI
{
    public partial class ContractLabel : TextEdit
    {  
        /// <summary>
        /// 联系人EmailAddress
        /// </summary>
        public string ContractAddress { get; set; }
        /// <summary>
        /// 显示文本
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                this.ContractAddress = value;
                base.Text = string.Format("'{0}'",value);
            }
        }

        public IICPCommonOperationService ICPCommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }

        }
        public ContractLabel()
        {
            InitializeComponent();
            //屏蔽文本下划线
            this.Properties.Appearance.Options.UseFont = true;
            this.Properties.ContextMenuStrip = contextMenuStrip;
            this.Properties.ReadOnly = true;
            Locale();
         

        }

        private void Locale()
        {
            if (!LocalData.IsEnglish)
            {
                this.toolStripItemCopy.Text = "复制";
                this.toolStripItemSend.Text = "发送传真";
            }
        }

        public ContractLabel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.ContractAddress);
        }

        private void toolStripItemSend_Click(object sender, EventArgs e)
        {
          
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message { SendTo = this.ContractAddress,Type= MessageType.Fax };
            if (ICPCommonOperationService.ShowSendForm(message))
            {
                ICPCommonOperationService.SaveMessage(message);
            }

        }
       
    }
}
