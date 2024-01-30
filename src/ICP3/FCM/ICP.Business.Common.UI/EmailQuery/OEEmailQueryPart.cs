using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.Client;
using ICP.Message.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using System.Threading;
using Exception = System.Exception;
using ICP.MailCenter.UI;

namespace ICP.Business.Common.UI
{
    public partial class OEEmailQueryPart : PopupWindow
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 业务号类型
        /// </summary>
        public int NoType { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int Types { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        public int DateType { get; set; }

        /// <summary>
        /// 时间段
        /// </summary>
        public int? Area { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 停止按钮使用的线程对象
        /// </summary>
        public Thread Thread = null;

        /// <summary>
        /// 邮件服务端接口
        /// </summary>
        public IMessageService MessageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }

        /// <summary>
        /// 邮件客户端的方法
        /// </summary>
        public IClientMessageService ClientMessageService
        {
            get { return new ClientMessageService(); }

        }

        /// <summary>
        /// OutLook服务接口
        /// </summary>
        public IOutLookService OutLookService
        {
            get
            {
                return new OutlookService();
            }
        }


        /// <summary>
        /// 返回当前选中数据行
        /// </summary>
        public Message.ServiceInterface.Message CurrentRow
        {

            get { return gridView.GetFocusedRow() as Message.ServiceInterface.Message; }

        }



        //记录LoadingForm的ID
        private int tokenID = -1;

        /// <summary>
        /// 
        /// </summary>
        public OEEmailQueryPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Load += new EventHandler(OEEmailQueryPart_Load);
            }
            if (LocalData.IsEnglish)
            {
                SetCnText();
            }
            this.Disposed += new EventHandler(OEEmailQueryPart_Disposed);
        }
        /// <summary>
        /// 关闭窗体时，注销事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OEEmailQueryPart_Disposed(object sender, EventArgs e)
        {
            this.Load -= this.OEEmailQueryPart_Load;
            this.simpleButtonFind.Click -= this.simpleButtonFind_Click;
            this.simpleButtonStop.Click -= this.simpleButtonStop_Click;
            this.simpleButtonsimpleButtonnNewSearch.Click -= simpleButtonsimpleButtonnNewSearch_Click;
            comboBoxEditDateType.SelectedIndexChanged -= comboBoxEditDateType_SelectedIndexChanged;
            gridView.RowClick -= gridView_RowClick;
            Thread = null;
            if (this.bindingSource != null)
            {
                this.bindingSource.DataSource = null;
                this.bindingSource = null;
            }

        }

        void OEEmailQueryPart_Load(object sender, EventArgs e)
        {
            //开启动画
            tokenID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading.....");
            simpleButtonStop.Enabled = false;
            AddComboBoxEdit();
            DefaultValue();
            //GetInitialFormSize();
            //GetAllCrlLocation(this);
            //GetAllCrlSize(this);
        }
        #region 方法
        /// <summary>
        /// 设置英文字符
        /// </summary>
        private void SetCnText()
        {
            labelControlcustomer.Text = "Customer";
            labelControlWords.Text = "Words";
            labelControlType.Text = "Category";
            labelControlPhas.Text = "Phase";
            labelControlNo.Text = "NO";
            labelControlMail.Text = "Mail";
            labelControlIn.Text = "In";
            labelControlDate.Text = "Time";
            simpleButtonFind.Text = "Search";
            simpleButtonStop.Text = "Stop";
            simpleButtonsimpleButtonnNewSearch.Text = "New Search";
            GroupControlConditions.Text = "The Search Criteria";
            ColumnContactStage.Caption = "ContactStage";
            ColumnSubject.Caption = "Subject";
            //ColumnSendFrom.Caption = "Send From";
            ColumnSendFromName.Caption = "Send From";
            ColumnReceivingTime.Caption = "Receiving Time";
            ColumnPriority.Caption = "Priority";
            ColumnHasAttachment.Caption = "Attachment";

            MenuItemOpen.Text = "Open";
            MenuItemrepl.Text = "Reply";
            MenuItemAllrepl.Text = "Reply All";
            MenuItemAllAttachment.Text = "Reply All(Attachment)";
            MenuItemforwardin.Text = "Forward";


        }
        /// <summary>
        /// 下拉框默认值
        /// </summary>
        public void AddComboBoxEdit()
        {
            //清空下拉框选择项
            comboBoxEditLocation.Properties.Items.Clear();
            comboBoxEditNoType.Properties.Items.Clear();
            comboBoxEditType.Properties.Items.Clear();
            comboBoxEditPhas.Properties.Items.Clear();
            comboBoxEditMailType.Properties.Items.Clear();
            comboBoxEditDateType.Properties.Items.Clear();
            comboBoxEditArea.Properties.Items.Clear();
            comboBoxEditMailType.Properties.Items.Clear();

            // 设置 comboBox的文本值不能被编辑  
            comboBoxEditLocation.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            comboBoxEditNoType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            comboBoxEditType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            comboBoxEditPhas.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            comboBoxEditDateType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            comboBoxEditArea.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            comboBoxEditMailType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //位置下拉框选择项
            comboBoxEditLocation.Properties.Items.Add(LocalData.IsEnglish == false ? "仅主题" : "Subject only");
            comboBoxEditLocation.Properties.Items.Add(LocalData.IsEnglish == false ? "主题和邮件正文" : "Subject & Mail content");
            comboBoxEditLocation.SelectedIndex = 1;

            //单号类型下拉框选择项
            comboBoxEditNoType.Properties.Items.Add(LocalData.IsEnglish == false ? "业务号" : "Shipment NO only");
            comboBoxEditNoType.Properties.Items.Add(LocalData.IsEnglish == false ? "所有单号类型" : "All type of bill");
            comboBoxEditNoType.SelectedIndex = 1;

            //分类下拉框选择项
            comboBoxEditType.Properties.Items.Add(LocalData.IsEnglish == false ? "所有" : "All");
            comboBoxEditType.Properties.Items.Add(LocalData.IsEnglish == false ? "客户 (订舱客户/收货人/发货人...)" : "Customer(Shipper/Consignee...)");
            comboBoxEditType.Properties.Items.Add(LocalData.IsEnglish == false ? "承运人(承运人/船公司...)" : "Carrier & Agent of Carrier");
            comboBoxEditType.Properties.Items.Add(LocalData.IsEnglish == false ? "代理" : "Agent");
            comboBoxEditType.SelectedIndex = 0;

            //阶段下拉框选择项
            comboBoxEditPhas.Properties.Items.Add(LocalData.IsEnglish == false ? "所有" : "All");
            //海出业务时，将加载下拉框选择项
            if (OperationType == OperationType.OceanExport)
            {
                comboBoxEditPhas.Properties.Items.Add("SO");
                comboBoxEditPhas.Properties.Items.Add("SI");
                comboBoxEditPhas.Properties.Items.Add("TRK");
            }
            comboBoxEditPhas.SelectedIndex = 0;

            //邮件类型下拉框选择项
            comboBoxEditMailType.Properties.Items.Add(LocalData.IsEnglish == false ? "发件人和收件人" : "From & Send to");
            comboBoxEditMailType.Properties.Items.Add(LocalData.IsEnglish == false ? "收件人" : "Send to");
            comboBoxEditMailType.Properties.Items.Add(LocalData.IsEnglish == false ? "发件人" : "From");
            comboBoxEditMailType.SelectedIndex = 0;

            //时间类型下拉框选择项
            comboBoxEditDateType.Properties.Items.Add(LocalData.IsEnglish == false ? "无" : "None");
            comboBoxEditDateType.Properties.Items.Add(LocalData.IsEnglish == false ? "接收时间" : "Received on");
            comboBoxEditDateType.Properties.Items.Add(LocalData.IsEnglish == false ? "发送时间" : "Sent on");
            comboBoxEditDateType.SelectedIndex = 0;

            //时间范围下拉框选择项
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "昨天" : "Last Day");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "今天 " : "Today");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "最近 7 天" : "Last 7 days");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "上周" : "Last Week");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "本周" : "This Week");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "上月" : "Last Month");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "本月" : "This Month");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "三个月" : "Three Months");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "半年" : "Half A Year");
            comboBoxEditArea.Properties.Items.Add(LocalData.IsEnglish == false ? "一年" : "A Year");
            comboBoxEditArea.Enabled = false;
        }
        /// <summary>
        /// 根据条件对控件进行赋值操作
        /// </summary>
        public void DefaultValue()
        {
            bindingSource.DataSource = null;
            bindingSource.ResetBindings(false);
            textEditNowords.EditValue = string.Empty;
            textEditMail.EditValue = string.Empty;
            comboBoxEditNoType.SelectedIndex = NoType;
            comboBoxEditType.SelectedIndex = Types;
            comboBoxEditDateType.SelectedIndex = DateType;
            if (Area != null)
            {
                comboBoxEditArea.SelectedIndex = (int)Area;
            }
            if (!string.IsNullOrEmpty(No))
            {
                textEditNowords.EditValue = No;
            }
            if (!string.IsNullOrEmpty(Mail))
            {
                textEditMail.EditValue = Mail;
            }

            if (!string.IsNullOrEmpty(textEditNowords.EditValue.ToString())
                || !string.IsNullOrEmpty(textEditMail.EditValue.ToString())
                || comboBoxEditDateType.SelectedIndex != 0)
            {
                DataBind();
            }
        }



        /// <summary>
        /// 数据绑定的方法
        /// </summary>
        public void DataBind()
        {
            simpleButtonStop.Enabled = true;
            simpleButtonFind.Enabled = false;
            simpleButtonsimpleButtonnNewSearch.Enabled = false;


            string words = textEditwords.EditValue == null ? string.Empty : textEditwords.EditValue.ToString();
            int location = comboBoxEditLocation.SelectedIndex + 1;
            string no = textEditNowords.EditValue == null ? string.Empty : textEditNowords.EditValue.ToString();
            int noType = comboBoxEditNoType.SelectedIndex + 1;
            int type = comboBoxEditType.SelectedIndex + 1;
            int phas = comboBoxEditPhas.SelectedIndex + 1;
            string customer = textEditCustomer.EditValue == null ? string.Empty : textEditCustomer.EditValue.ToString();
            string mail = textEditMail.EditValue == null ? string.Empty : textEditMail.EditValue.ToString().Replace(",", "&#;");
            int mailType = comboBoxEditMailType.SelectedIndex + 1;
            int dateType = 1;
            DateTime sendtime = DateTime.Now;
            DateTime receivingTime = DateTime.Now;
            if (comboBoxEditDateType.SelectedIndex != 0)
            {
                dateType = comboBoxEditDateType.SelectedIndex + 1;
                //基本时间为计算机本地时间
                DateTime datebasic = DateTime.Now;
                switch (comboBoxEditArea.SelectedIndex)
                {
                    //昨日
                    case 0:
                        sendtime = DateTime.Parse(datebasic.AddDays(-1).ToShortDateString());
                        receivingTime = DateTime.Parse(datebasic.AddDays(-1).ToShortDateString() + " 23:59:59");
                        break;
                    //今日
                    case 1:
                        sendtime = DateTime.Parse(datebasic.ToShortDateString());
                        receivingTime = DateTime.Parse(datebasic.ToShortDateString() + " 23:59:59");
                        break;
                    //最近 7 天
                    case 2:
                        sendtime = DateTime.Parse(DateTime.Now.AddDays(-7).ToShortDateString());
                        receivingTime = DateTime.Parse(datebasic.ToShortDateString());
                        break;
                    //上周
                    case 3:
                        string laststart = DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString();
                        string lastend = DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString();
                        sendtime = DateTime.Parse(laststart);
                        receivingTime = DateTime.Parse(lastend + " 23:59:59");
                        break;
                    //本周
                    case 4:
                        string thisstart = DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToShortDateString();
                        string thistend = DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToShortDateString();
                        sendtime = DateTime.Parse(thisstart);
                        receivingTime = DateTime.Parse(thistend + " 23:59:59");
                        break;
                    //上月
                    case 5:
                        string lastMonthstart = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1).ToShortDateString();
                        string lastMonthend = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
                        sendtime = DateTime.Parse(lastMonthstart);
                        receivingTime = DateTime.Parse(lastMonthend + " 23:59:59");
                        break;
                    //本月
                    case 6:
                        string thisMonthstart = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).ToShortDateString();
                        string thistMonthend = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(2).AddDays(-1).ToShortDateString();
                        sendtime = DateTime.Parse(thisMonthstart);
                        receivingTime = DateTime.Parse(thistMonthend + " 23:59:59");
                        break;
                    //三个月
                    case 7:
                        string thisThreeMonthsstart = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-2).ToShortDateString();
                        string thisThreeMonthsend = sendtime.ToShortDateString();
                        sendtime = DateTime.Parse(thisThreeMonthsstart);
                        receivingTime = DateTime.Parse(thisThreeMonthsend + " 23:59:59");
                        break;
                    //半年
                    case 8:
                        string thisHalfAYearstart = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-5).ToShortDateString();
                        string thisHalfAYearend = sendtime.ToShortDateString(); ;
                        sendtime = DateTime.Parse(thisHalfAYearstart);
                        receivingTime = DateTime.Parse(thisHalfAYearend + " 23:59:59");
                        break;
                    //一年
                    case 9:
                        string thisAYearstart = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddYears(-1).ToShortDateString();
                        string thisAYearend = sendtime.ToShortDateString();
                        sendtime = DateTime.Parse(thisAYearstart);
                        receivingTime = DateTime.Parse(thisAYearend + " 23:59:59");
                        break;
                }
            }
            var message = MessageService.RuturnMailList(LocalData.UserInfo.LoginID, words, location, no, noType, customer, type, phas,
                                                          mail, mailType, dateType, sendtime, receivingTime);

            //设置按钮是否可用
            simpleButtonStop.Enabled = false;
            simpleButtonFind.Enabled = true;
            simpleButtonsimpleButtonnNewSearch.Enabled = true;

            //绑定数据
            bindingSource.DataSource = null;
            bindingSource.DataSource = message;
            //关闭动画
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(tokenID);
            //隐藏图片
            if (pictureBoxLoding.Visible)
            {
                pictureBoxLoding.Visible = false;
            }
            bindingSource.ResetBindings(false);
        }


        /// <summary>
        /// 打开邮件
        /// </summary>
        public void OpenMail()
        {
            int toid = -1;
            this.TopMost = true;
            if (CurrentRow != null)
            {
                ServiceClient.GetClientService<IClientMessageService>()
                    .GetMailItemAndOpen(CurrentRow.EntryID, CurrentRow.MessageId);
                this.TopMost = false;
            }
        }


        /// <summary>
        /// 返回MailItem对象
        /// </summary>
        /// <returns></returns>
        public MailItem SetMailItem()
        {
            MailItem mailItem = null;
            try
            {
                mailItem =
                ServiceClient.GetClientService<IClientMessageService>()
                      .GetMailItemByEntryID(CurrentRow.EntryID, CurrentRow.MessageId) as MailItem;
            }
            catch (Exception ex)
            {
                Logger.Log.Info(Environment.NewLine + ex.ToString());
            }
            return mailItem;
        }
        #endregion


        #region   页面按钮
        /// <summary>
        /// 立即搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonFind_Click(object sender, EventArgs e)
        {
            bindingSource.DataSource = null;
            pictureBoxLoding.Visible = true;
            //生成新线程前，清除上次产生的线程
            if (Thread != null)
            {
                Thread.Abort();
            }
            //将方法挂入线程中执行
            Thread = new Thread(new ThreadStart(DataBind));
            //线程启动
            Thread.Start();
        }
        /// <summary>
        /// 停止按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonStop_Click(object sender, EventArgs e)
        {
            Thread.Abort();
            simpleButtonStop.Enabled = false;
            simpleButtonFind.Enabled = true;
            simpleButtonsimpleButtonnNewSearch.Enabled = true;
        }

        /// <summary>
        /// 新搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonsimpleButtonnNewSearch_Click(object sender, EventArgs e)
        {
            this.textEditCustomer.Text = string.Empty;
            this.textEditMail.Text = string.Empty;
            this.textEditNowords.Text = string.Empty;
            this.textEditwords.Text = string.Empty;
            comboBoxEditLocation.SelectedIndex = 1;
            comboBoxEditNoType.SelectedIndex = 1;
            comboBoxEditType.SelectedIndex = 0;
            comboBoxEditPhas.SelectedIndex = 0;
            comboBoxEditMailType.SelectedIndex = 0;
            comboBoxEditDateType.SelectedIndex = 0;
            bindingSource.DataSource = null;

        }

        #endregion


        /// <summary>
        /// 时间选择下拉框（如果选择的值等于无，comboBoxEditArea下拉框不可用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEditDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditDateType.SelectedIndex == 0)
            {
                comboBoxEditArea.Enabled = false;
            }
            else
            {
                comboBoxEditArea.Enabled = true;
            }
        }




        #region gridView列表事件
        /// <summary>
        /// 双击行打开邮件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                OpenMail();
            }
        }
        #endregion

        /// <summary>
        /// 页面回车执行查询方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OEEmailQueryPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bindingSource.DataSource = null;
                pictureBoxLoding.Visible = true;
                //生成新线程前，清除上次产生的线程
                if (Thread != null)
                {
                    Thread.Abort();
                }
                //将方法挂入线程中执行
                Thread = new Thread(new ThreadStart(DataBind));
                //线程启动
                Thread.Start();
            }
        }


        #region 鼠标右键菜单
        /// <summary>
        ///打开邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            OpenMail();
        }

        /// <summary>
        /// 答复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemrepl_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            OutLookService.ReplyToSender(SetMailItem());

        }

        /// <summary>
        /// 全部答复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAllrepl_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            OutLookService.ReplyToAll(SetMailItem());

        }
        /// <summary>
        /// 全部答复(含附件)
        /// </summary>
        private void MenuItemAllAttachment_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            OutLookService.ReplyToAllContainsAttachment(SetMailItem());
        }
        /// <summary>
        /// 转发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemforwardin_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            OutLookService.Forward(SetMailItem());
        }

        #endregion


        private void OEEmailQueryPart_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.gridView.OptionsView.ColumnAutoWidth = true;
                label1.Text =
                    "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                label2.Text =
                    "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            }
            else
            {
                this.gridView.OptionsView.ColumnAutoWidth = false;
                label1.Text = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                label2.Text = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            }
        }

        
    }
}
