using System;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using System.Runtime.InteropServices;
using ICP.EDI.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.EDI.UI
{
    public partial class CSCLWeb : Form
    {       
        public IEDIService EDIService { get; set; }
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);
        /// <summary>
        /// 登录名
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 登录URL
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// EDI类型
        /// </summary>
        public EDITypeByCarrier FEDITypeByCarrier { get; set; }
        /// <summary>
        /// 业务号或者主提单号
        /// </summary>
        public string[] OpOrMBLNos { get; set; }
        /// <summary>
        /// 发送类型0订舱/修改订舱 1装箱
        /// </summary>
        public int SendType { get; set; }

        public SendHead oSendHead;
        public Http oHttp;
        public CSCLWeb()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Load += delegate
                {
                    SetText();
                    //if (SendType == 0 && this.OpOrMBLNos.Length > 1)
                    //{
                    //    //this.menuStrip.Visible = false;
                    //    NavigateBooking(this.OpOrMBLNos[0]);
                    //}
                    //else
                    //{
                        CreateSelectPanel();
                    //}
                };
            }
        }
        private void SetText()
        {
            string text = string.Empty;

            if (FEDITypeByCarrier == EDITypeByCarrier.CSCL_Booking_NorthChina)
            {
                text = LocalData.IsEnglish ? "Check eBooking" : "检查EDI订舱";
            }
            else if (FEDITypeByCarrier == EDITypeByCarrier.CSCL_SI_NorthChina)
            {
                text = LocalData.IsEnglish ? "Check eLoading" : "检查EDI补料";
            }
            this.Text = text;
        }
        private void CreateSelectPanel()
        {
            //this.toolStripMenuItemCheck.Text = LocalData.IsEnglish ? "&Select" : "选择(&S)";
            int i = 0;
            foreach (string no in this.OpOrMBLNos)
            {
                ToolStripMenuItem edit = new ToolStripMenuItem();
                edit.Text = no;
                if (i == 0)
                {
                    edit.Checked = true;
                }

                edit.CheckOnClick = true;
                edit.Click += new EventHandler(editBooking_Click);
                this.toolBooking1.DropDownItems.Add(edit);
                if (i == 0)
                {
                    NavigateBooking(no);
                }
                if (SendType == 1)
                {
                    ToolStripMenuItem edit1 = new ToolStripMenuItem();
                    edit1.Text = no;
                    if (i == 0)
                    {
                        edit1.Checked = true;
                    }

                    edit1.CheckOnClick = true;
                    edit1.Click += new EventHandler(editLoading_Click);
                    this.toolLoading1.DropDownItems.Add(edit1);
                    if (i == 0)
                    {
                        NavigateLoading(no);
                    }
                }
                i++;
            }
        }

        void editBooking_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.toolBooking1.DropDownItems.Count; i++)
            {
                (this.toolBooking1.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
            ToolStripMenuItem edit = sender as ToolStripMenuItem;
            edit.Checked = true;
            string opOrMBlNo = edit.Text;
            NavigateBooking(opOrMBlNo);
        }
        void editLoading_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.toolLoading1.DropDownItems.Count; i++)
            {
                (this.toolLoading1.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
            ToolStripMenuItem edit = sender as ToolStripMenuItem;
            edit.Checked = true;
            string opOrMBlNo = edit.Text;
            NavigateLoading(opOrMBlNo);
        }
        private void NavigateBooking(string opOrMblNo)
        {
            if (SendType == 1)
            { 
                //获取业务号
                opOrMblNo = EDIService.GetOPNOByMBONO(opOrMblNo);
            }
            string bookingUrl = "ebooking2012/orders.do?method=show&orderno={0}&blno=&orderListPage=1";
            GotoURL(opOrMblNo, bookingUrl);
        }
        private void NavigateLoading(string opOrMblNo)
        {
            string loadingUrl = "eloading2012/loading.do?method=show&mbl_no={0}";
            string s=opOrMblNo.Substring(0,4).ToUpper();
            if (s == "CHHK" || s == "CHNJ")
            {
                opOrMblNo = opOrMblNo.Substring(4);
            }
            GotoURL(opOrMblNo, loadingUrl);
        }

        private void GotoURL(string opOrMblNo, string loadingUrl)
        {
            this.oHttp.Login(ref this.oSendHead, this.Path, this.UID, this.Pwd);
            string relativeUrl = loadingUrl;
            string url = this.Path + string.Format(relativeUrl, opOrMblNo);
            if (this.oSendHead.Cookies != null)
            {
                foreach (System.Net.Cookie cookie in this.oSendHead.Cookies)
                {
                    InternetSetCookie(this.Path, cookie.Name, cookie.Value);
                }
            }
            this.webBrowser.Navigate(url);
        }
    }
}
