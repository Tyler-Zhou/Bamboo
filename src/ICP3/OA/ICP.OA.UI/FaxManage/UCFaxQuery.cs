using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.OA.UI.FaxManage
{
    [ToolboxItem(true)]
    [SmartPart]
    public partial class UCFaxQuery : XtraUserControl
    {
        public bool IsSelectedFaxHall = false;
        #region 服务
        public EventHandler<CommonEventArgs<MailQuery>> QueryEvent;
        public EventHandler<CommonEventArgs<bool>> DisplayEvent;
        private MessageFolderList currentFolder;
        public bool Expand
        {

            get { return this.pnlQuery.Expand; }
            set
            {
                this.pnlQuery.Expand = value;
            }
        }
        #endregion

        public UCFaxQuery()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false && !LocalData.IsDesignMode)
            {
                DevExpress.Skins.Skin currentSkin = DevExpress.Skins.CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
                this.pnlQuery.ColorScheme = BSE.Windows.Forms.ColorScheme.Custom;
                Color controlColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.Control);
                this.pnlQuery.CaptionForeColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.ControlText);
                this.pnlQuery.ColorCaptionGradientBegin = this.pnlQuery.ColorCaptionGradientEnd = this.pnlQuery.ColorCaptionGradientMiddle = controlColor;

                SetCnText();
            }
            InitDateEdit();
            this.Disposed += delegate
            {
                this.currentFolder = null;
                this.QueryEvent = null;
                this.DisplayEvent = null;
            
            };
        }


        /// <summary>
        /// 中文设置
        /// </summary>
        private void SetCnText()
        {
            labSearchDateFrom.Text = "从";
            labSearchDateTo.Text = "到";
            labSearchSubject.Text = "主题";

            labSearchFrom.Text = "发件人";
            labSearchTo.Text = "收件人";
            chkDateTime.Text = "日期";
            lwchkAttachment.CheckedText = "有附件";
            lwchkAttachment.NULLText = "全部";
            lwchkAttachment.UnCheckedText = "无附件";
            btnQuery.Text = "查询(&Q)";
            btnClear.Text = "清空(&C)";
            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-1);
            dteTo.DateTime = Utility.GetEndDate(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
        }

        /// <summary>
        /// 得到查询条件
        /// </summary>
        /// <returns></returns>
        public MailQuery GetQueryCondition()
        {
            MailQuery mailquery = new MailQuery();
            mailquery.IsFaxHall = IsSelectedFaxHall;
            mailquery.To = this.txtMailTo.Text.Trim();
            mailquery.From = this.txtMailFrom.Text.Trim();
            mailquery.Subject = this.txtSubject.Text.Trim();
            mailquery.IncludeAttachment = this.lwchkAttachment.Checked;
            if (this.currentFolder != null)
            {
                mailquery.FolderId = this.currentFolder.ID;
            }
            if (chkDateTime.Checked)
            {
                mailquery.FromTime = dteFrom.DateTime.Date;
                mailquery.ToTime = Utility.GetEndDate(dteTo.DateTime);
            }
            return mailquery;
        }
        public void OnFolderChanged(object sender, CommonEventArgs<MessageFolderList> args)
        {
            this.currentFolder = args.Data;
            this.pnlQuery.Text = this.currentFolder == null ? string.Empty : this.currentFolder.Name + (LocalData.IsEnglish ? " - Search" : " - 查询");

        }

        private void chkDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dteFrom.Enabled = dteTo.Enabled = !chkDateTime.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chkDateTime.Checked = false;
            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-1);
            dteTo.DateTime = Utility.GetEndDate(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            InitDateEdit();
            lwchkAttachment.Checked = null;
        }

        /// <summary>
        /// 初始化日期控件不可用
        /// </summary>
        private void InitDateEdit()
        {
            dteFrom.Enabled = false;
            dteTo.Enabled = false;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            MailQuery queryCondition = GetQueryCondition();
            if (QueryEvent != null)
            {
                QueryEvent(this, new CommonEventArgs<MailQuery>(queryCondition));
            }
        }

        private void pnlQuery_PanelExpanding(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {
            if (DisplayEvent != null)
            {
                DisplayEvent(this, new CommonEventArgs<bool>(false));
            }
        }

        private void pnlQuery_PanelCollapsing(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {
            if (DisplayEvent != null)
            {
                DisplayEvent(this, new CommonEventArgs<bool>(true));
            }
        }

    }
}
