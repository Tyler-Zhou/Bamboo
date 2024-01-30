using System;
using ICP.Business.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
namespace ICP.MailCenter.CommonUI.FaxViewer
{  
    /// <summary>
    /// Email地址文本框
    /// </summary>
   public class EmailAddressTextBox:AutoHeightTextBox
    {  
       /// <summary>
       /// 构造函数
       /// </summary>
       public EmailAddressTextBox()
       {
           this.Click += new EventHandler(EmailAddressTextBox_Click);
           //屏蔽系统默认右键菜单
           this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
          // this.AddressDoubleClick += OnAddressDoubleClick;
           //设置背景色
           DevExpress.Skins.Skin currentSkin = DevExpress.Skins.CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
           this.BackColor  = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.Control);
           this.Disposed += delegate {
               this.AddressDoubleClick = null;
               
           
           };
       }
       /// <summary>
       /// 点击时设置选择文本为当前点击所在位置的Email地址
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       void EmailAddressTextBox_Click(object sender, EventArgs e)
       {
           SetSelection();
       }
        const int WM_LBUTTONDBLCLK = 0x0203;
       /// <summary>
       /// 地址双击事件
       /// </summary>
        public EventHandler<CommonEventArgs<string>> AddressDoubleClick;
        public IICPCommonOperationService ICPCommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }

        }
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                if (AddressDoubleClick != null)
                {
                    AddressDoubleClick(this, new CommonEventArgs<string>(this.SelectedText));
                }
                return;
            }
        
               base.WndProc(ref m);
            
        }
        private void SetSelection()
        {
            if (string.IsNullOrEmpty(this.Text))
                return;
            int nextSpaceIndex = this.Text.Substring(this.SelectionStart).IndexOf(';');
            int firstSpaceIndex = this.Text.Substring(0, this.SelectionStart).LastIndexOf(';');
            nextSpaceIndex = nextSpaceIndex == -1 ? this.Text.Length : nextSpaceIndex + this.SelectionStart;
            firstSpaceIndex = firstSpaceIndex == -1 ? 0 : firstSpaceIndex;
            if (firstSpaceIndex > 0)
            {
                this.SelectionStart = firstSpaceIndex + 1;
                this.SelectionLength = nextSpaceIndex - firstSpaceIndex - 1;
            }
            else
            {
                this.SelectionStart = firstSpaceIndex;
                this.SelectionLength = nextSpaceIndex - firstSpaceIndex;
            }
        }
        private void OnAddressDoubleClick(object sender, CommonEventArgs<string> args)
        {
         
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message { SendTo = args.Data, Type = MessageType.Fax };
            if (ICPCommonOperationService.ShowSendForm(message))
            {
                ICPCommonOperationService.SaveMessage(message);
            }
        }
      
        
    }
    
}
