using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using System.Text.RegularExpressions;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件搜索控件
    /// </summary>
    [SmartPart]
    public partial class UCMailSearch : UserControl, ICP.Common.ServiceInterface.IKeyboardEventHandleService
    {
        #region 属性定义

        private string tipFormat = LocalData.IsEnglish ? "Search {0} (Ctrl+E)" : "搜索 {0} (Ctrl+E)";

        #endregion
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        public string FolderEntryID
        {
            get;
            set;

        }
        public bool IsSearchAllFolder
        {
            get;
            set;
        }
        public string FolderName
        {
            get;
            set;
        }
        public EmailFolderPart FolderPart
        {
            get
            {
                return RootWorkItem.SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }
        public EmailListPart ListPart
        {
            get
            {
                return RootWorkItem.SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCMailSearch()
        {
            InitializeComponent();
            SetDefaultTip(true);
            this.GotFocus += delegate { this.txtSearchText.Focus(); };
            this.txtSearchText.GotFocus += new EventHandler(txtSearchText_GotFocus);
            this.txtSearchText.LostFocus += new EventHandler(txtSearchText_LostFocus);
            this.btnFunction.Click += new EventHandler(btnFunction_Click);
            this.txtSearchText.KeyDown += new KeyEventHandler(txtSearchText_KeyDown);

            this.Load += new EventHandler(UCMailSearch_Load);
            this.Disposed += new EventHandler(UCMailSearch_Disposed);

        }
        /// <summary>
        /// 设置搜索框的默认tooltip
        /// </summary>
        /// <param name="isSearch"></param>
        private void SetDefaultTip(bool isSearch)
        {
            this.btnFunction.ToolTip = isSearch ? (LocalData.IsEnglish ? "Search Current Folder By Subject,Sender And To" : "通过标题、发件人、收件人搜索当前文件夹") : (LocalData.IsEnglish ? "Close Search" : "关闭搜索");
        }
        /// <summary>
        /// 销毁时释放键盘事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCMailSearch_Disposed(object sender, EventArgs e)
        {
            IKeyboardEventService keyboardEventService = ServiceClient.GetClientService<IKeyboardEventService>();
            if (keyboardEventService != null)
            {
                keyboardEventService.UnRegisterHandler(this);
            }
        }

        void UCMailSearch_Load(object sender, EventArgs e)
        {
            //挂载键盘处理程序
            IKeyboardEventService keyboardEventService = ServiceClient.GetClientService<IKeyboardEventService>();
            if (keyboardEventService != null)
                keyboardEventService.RegisterHandler(this);
            // ClientProperties.folder_FullPath
            string defaultFolder = ClientProperties.ViewCtlDefaultFolderPath;
            string currentFolderName = defaultFolder;
            if (!string.IsNullOrEmpty(defaultFolder))
            {
                int index = defaultFolder.LastIndexOf("\\");
                if (index >= 0)
                {
                    currentFolderName = defaultFolder.Substring(index + 1);
                    this.FolderName = currentFolderName;
                }
                this.SearchText = string.Format(this.tipFormat, this.FolderName);
            }
        }


        void txtSearchText_LostFocus(object sender, EventArgs e)
        {
            if (this.SearchText == string.Format(this.tipFormat, this.FolderName) || string.IsNullOrEmpty(this.SearchText) || string.IsNullOrEmpty(this.SearchText.Trim()))
            {
                DrawSearchTextBorder(false);
            }
            if (string.IsNullOrEmpty(this.SearchText) || string.IsNullOrEmpty(this.SearchText.Trim()))
            {
                this.SearchText = string.Format(this.tipFormat, this.FolderName);
            }



        }

        void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && IsSearch())
            {
                StopTimer();
                this.btnFunction_Click(null, EventArgs.Empty);
            }

        }


        private void DrawSearchTextBorder(bool focused)
        {
            Color color;
            if (focused)
            {
                color = Color.FromArgb(247, 190, 87);

            }
            else
            {
                color = this.BackColor;
            }
            Pen pen = new Pen(color);
            pen.Width = 20;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.CreateGraphics().DrawRectangle(pen, this.Bounds);
            }
        }

        void btnFunction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.SearchText) || string.IsNullOrEmpty(this.SearchText.Trim()) || this.SearchText.Trim() == string.Format(this.tipFormat, this.FolderName))
            {
                return;
            }
            StopTimer();
            bool isSearch = IsSearch();
            if (isSearch)
            {
                Search();

            }
            else
            {
                CloseSearch();
            }
            SetFunctionButtonInfo(!isSearch);
        }
        private bool IsSearch()
        {
            return this.btnFunction.ImageIndex == 1;
        }
        private void SetFunctionButtonInfo(bool isSearch)
        {
            this.btnFunction.ImageIndex = isSearch ? 1 : 0;
            SetDefaultTip(isSearch);
        }
        private bool IsEnglish
        {
            get
            {
                return ClientUtility.olCultrue.Contains("en");
            }
        }
        void txtSearchText_GotFocus(object sender, EventArgs e)
        {
            string text = string.Format(this.tipFormat, this.FolderName);
            if (this.SearchText == text)
            {
                this.SearchText = string.Empty;
            }
            DrawSearchTextBorder(true);

        }

        void Search()
        {
            if (string.IsNullOrEmpty(this.SearchText) || string.IsNullOrEmpty(this.SearchText.Trim()) || this.SearchText.Trim() == string.Format(this.tipFormat, this.FolderName))
            {
                return;
            }
            string filter = GetFilter(this.SearchText);
            SetRestriction(filter, false);

        }

        private string GetFilter(string searchText)
        {
            // 查找字段:主题 发件人 邮件正文 收件人
            //中文 主题 发件人 邮件正文 收件人
            //英文 Subject From Message To
            string filterFormat = InnerGetFilterFormat();
            string filter = filterFormat.Replace("#value#", searchText);

            return filter;
        }
        private string chineseFilter;
        private string englishFilter;
        private string InnerGetFilterFormat()
        {

            if (IsEnglish)
            {

                return InnerBuildFilter(englishFilter, englishFields);
            }
            else
            {

                return InnerBuildFilter(chineseFilter, chineseFields);
            }
        }
        private List<string> englishFields = new List<string> { "Subject", "From", "To" };
        private List<string> chineseFields = new List<string> { "主题", "发件人", "收件人" };
        /// <summary>
        /// 构建过滤条件字符串
        /// </summary>
        /// <param name="existsFilter"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private string InnerBuildFilter(string existsFilter, List<string> fields)
        {
            if (!string.IsNullOrEmpty(existsFilter))
            {
                return existsFilter;
            }
            else
            {

                StringBuilder builder = new StringBuilder(200);
                fields.ForEach(field => builder.Append(string.Format("([{0}]='#value#') OR ", field)));
                existsFilter = builder.ToString().Substring(0, builder.Length - 3);
                return existsFilter;
            }
        }
        /// <summary>
        /// 关闭搜索
        /// </summary>
        private void CloseSearch()
        {
            this.SearchText = string.Format(this.tipFormat, this.FolderName);
            SetRestriction(string.Empty, false);
            DrawSearchTextBorder(false);

        }
        /// <summary>
        /// 设置邮件视图控件的过滤约束条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="disableSelectionChangeEvent">是否禁止文件夹列表当前选择文件夹改变事件</param>
        private void SetRestriction(string filter, bool disableSelectionChangeEvent)
        {
            try
            {
                if (filter == ListPart.axViewCtlEmailList.Restriction)
                {
                    return;
                }
                using (new CursorHelper())
                {
                    if (disableSelectionChangeEvent)
                    {
                        ListPart.RemoveSelectionChangedHandler();
                    }
                    ListPart.axViewCtlEmailList.Restriction = filter;
                    if (disableSelectionChangeEvent)
                    {
                        ListPart.AddSelectionChangeHandler();
                    }
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        /// <summary>
        /// 焦点文件夹改变事件处理
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(MailCenterCommandConstants.Command_CurrentFolderChanged)]
        public void CurrentFolderChanged(object sender, EventArgs e)
        {
            StopTimer();
            this.FolderEntryID = this.RootWorkItem.State[MailCenterCommandConstants.CurrentEntryID].ToString();
            string folderName = this.RootWorkItem.State[MailCenterCommandConstants.CurrentNodeText].ToString();
            Regex regex = new Regex(@"\(\d+\)$");
            Match match = regex.Match(folderName);
            if (match.Success)
            {
                this.FolderName = folderName.Replace(match.Value, "");
            }
            else
            {
                this.FolderName = folderName;
            }
            this.SearchText = string.Format(this.tipFormat, this.FolderName);
            SetRestriction(string.Empty, true);
            SetFunctionButtonInfo(true);
            DrawSearchTextBorder(false);


        }






        #region IKeyboardEventHandleService 成员
        /// <summary>
        /// 全局键盘事件捕获处理，如果按键为Ctrl+E，则使搜索框获得焦点
        /// </summary>
        /// <param name="eventInfo"></param>
        void ICP.Common.ServiceInterface.IKeyboardEventHandleService.Handle(ICP.Common.ServiceInterface.KeyboardEventInfo eventInfo)
        {
            if (eventInfo.Ctrl && eventInfo.KeyCode == Keys.E.ToString())
            {
                this.txtSearchText.Focus();
            }
        }

        #endregion
        private DateTime dtLastInput = DateTime.Now;
        private void txtSearchText_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchText) || string.IsNullOrEmpty(SearchText.Trim()) || this.SearchText == string.Format(this.tipFormat, this.FolderName))
            {
                SetFunctionButtonInfo(true);
                SetRestriction(string.Empty, false);
                StopTimer();
                return;
            }
            StopTimer();
            StartTimer();

        }
        /// <summary>
        /// 用户当前输入的搜索条件字符串
        /// </summary>
        private string SearchText
        {
            get
            {
                return this.txtSearchText.Text;
            }
            set
            {
                this.txtSearchText.Text = value;
            }
        }
        private DateTime dtLastSearch = DateTime.Now;
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            StopTimer();
            btnFunction_Click(null, EventArgs.Empty);

        }
        private void StopTimer()
        {
            this.SearchTimer.Stop();
            this.SearchTimer.Enabled = false;
        }
        private void StartTimer()
        {
            this.SearchTimer.Enabled = true;
            this.SearchTimer.Start();
        }
    }
}
