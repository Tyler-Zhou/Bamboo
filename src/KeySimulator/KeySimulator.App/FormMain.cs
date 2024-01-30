using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KeySimulator.App
{
    public partial class FormMain : Form
    {
        #region 成员(Member)
        /// <summary>
        /// 命令列表
        /// </summary>
        private IList<string> _CommandList = null;
        /// <summary>
        /// 命令索引
        /// </summary>
        private int _CommandIndex = 0;
        /// <summary>
        /// 默认间隔时间(ms)
        /// </summary>
        private const int _DefaultInterval = 100;
        /// <summary>
        /// 默认等待时间
        /// </summary>
        private const int _DefaultWaitTime = 3;
        /// <summary>
        /// 等待时间(s)
        /// </summary>
        private int _WaitTime = _DefaultWaitTime;
        /// <summary>
        /// 缓存待执行内容
        /// </summary>
        private string _CacheContent = "";

        HotKey h = new HotKey();

        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr ext);
        #endregion

        #region 构造函数(Constructor)
        public FormMain()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
            };
        }
        #endregion

        #region 事件(Event)
        private void FormMain_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 9; i++)
            {
                if (RegistHotKey($"Ctrl + Alt + {i}", ButtonExecute))
                    break;
            }
            TimerPosition.Start();
        }

        private void TimerExecute_Tick(object sender, EventArgs e)
        {
            TimerExecute.Stop();
            TimerExecute.Interval = _DefaultInterval;
            lblInfo.Text = $"{_CommandIndex + 1} / {_CommandList.Count} {_CommandList[_CommandIndex]} ";
            string currentCommand = _CommandList[_CommandIndex];
            int split = currentCommand.IndexOf(':');
            string commandTypeText = "";
            string codeContent = "";
            if (split > 0)
            {
                commandTypeText = currentCommand.Substring(0, split);
                codeContent = currentCommand.Substring(split + 1);
            }
            else
            {
                commandTypeText = currentCommand;
            }
            commandTypeText = commandTypeText.Trim().ToUpper();
            if (Enum.IsDefined(typeof(CommandType), commandTypeText))
            {
                try
                {
                    CommandType cmdType = (CommandType)Enum.Parse(typeof(CommandType), commandTypeText);
                    switch (cmdType)
                    {
                        case CommandType.INPUT:
                            ExecuteInput(codeContent);
                            break;
                        case CommandType.RUN:
                            ExecuteRun(codeContent);
                            break;
                        case CommandType.KEY:
                            ExecuteKey(codeContent);
                            break;
                        case CommandType.SLEEP:
                            ExecuteSleep(codeContent);
                            break;
                        case CommandType.MOUSE_MOVE:
                            ExecuteMouseMove(codeContent);
                            break;
                        case CommandType.MOUSE_CLICK:
                            ExecuteMouseClick();
                            break;
                        case CommandType.MOUSE_DBCLICK:
                            ExecuteMouseDBClick();
                            break;
                        case CommandType.SCREEN:
                            ExecuteScreen();
                            break;
                        case CommandType.ALL_SCREEN:
                            ExecuteAllScreen();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    btnExecute.PerformClick();
                    MessageBox.Show("运行[" + currentCommand + "]时失败！\r\n\r\n错误原因：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            _CommandIndex++;
            if (_CommandIndex == _CommandList.Count)
            {
                btnExecute.PerformClick();
            }
            else
            {
                TimerExecute.Start();
            }
        }

        private void TimerTips_Tick(object sender, EventArgs e)
        {
            _WaitTime--;
            lblInfo.Text = $"{_WaitTime} 秒后开始！";
            if (_WaitTime == 0)
            {
                TimerTips.Stop();
                TimerExecute.Interval = _DefaultInterval;
                TimerExecute.Start();
            }
        }

        private void TimerPosition_Tick(object sender, EventArgs e)
        {
            TimerPosition.Stop();
            int x = MousePosition.X;
            int y = MousePosition.Y;
            lblPosition.Text = $"鼠标坐标: {x},{y}";

            Rectangle rect = Screen.PrimaryScreen.Bounds;
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(x, y, 0, 0, new Size(1, 1));
                }
                lblColor.BackColor = bmp.GetPixel(0, 0);
            }

            TimerPosition.Start();
        }

        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkHelp.Text == "帮助")
            {
                _CacheContent = txtContent.Text;
                linkHelp.Text = "关闭";
                txtContent.Text = @"========================
按键工具 Key Simulator v1.0 

输入格式为：命令:内容 每行一条命令

INPUT         输入文本
RUN           运行程序
KEY           模拟按键
SLEEP         暂停
MOUSE_MOVE    鼠标移动
MOUSE_CLICK   鼠标单击
MOUSE_DBCLICK 鼠标双击
SCREEN        窗口截屏
ALL_SCREEN    全屏截屏

KEY 辅助说明 按照C# SendKeys.Send 函数要求

字母或数字 a-z A-Z 0-9
Alt    %
Ctrl   ^
Shift  +
向上键 {UP} 
向下键 {DOWN}  
向左键 {LEFT}  
向右键 {RIGHT}  
Enter  {ENTER} 或 ~  
Backspace {BACKSPACE}、{BS} 或 {BKSP}  
Break     {BREAK}  
Caps Lock {CAPSLOCK}  
Scroll Lock   {SCROLLLOCK}  
Print Screen  {PRTSC}（保留供将来使用）  
Del 或 Delete {DELETE} 或 {DEL}  
End   {END}  
Esc   {ESC}  
Help  {HELP}  
Home  {HOME}  
Ins 或 Insert  {INSERT} 或 {INS}  
Num Lock   {NUMLOCK}  
Page Down  {PGDN}  
Page Up    {PGUP}  
Tab        {TAB}  
F1-F16     {F1-F16} 
数字键盘加号 {ADD}  
数字键盘减号 {SUBTRACT} 
数字键盘乘号 {MULTIPLY}  
数字键盘除号 {DIVIDE} 
特殊键 {{} {%}
重复键 {h 10}
组合键 ^(AC)
";
            }
            else
            {
                txtContent.Text = _CacheContent;
                linkHelp.Text = "帮助";
                txtContent.Focus();
            }
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            ExecuteCommand();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            h.UnRegist(Handle, ButtonExecute);
        }
        #endregion

        #region 重写方法(Override Method)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {

            //窗口消息处理函数
            h.ProcessHotKey(m);
            base.WndProc(ref m);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 
        /// </summary>
        private void RegisterEvent()
        {
            Load += FormMain_Load;
            TimerExecute.Tick += TimerExecute_Tick;
            TimerTips.Tick += TimerTips_Tick;
            TimerPosition.Tick += TimerPosition_Tick;
            linkHelp.LinkClicked += linkHelp_LinkClicked;
            btnExecute.Click += BtnExecute_Click;
            FormClosing += FormMain_FormClosing;
        }
        /// <summary>
        /// 
        /// </summary>
        private void UnRegisterEvent()
        {
            Load -= FormMain_Load;
            TimerExecute.Tick -= TimerExecute_Tick;
            TimerTips.Tick -= TimerTips_Tick;
            TimerPosition.Tick -= TimerPosition_Tick;
            linkHelp.LinkClicked -= linkHelp_LinkClicked;
            btnExecute.Click -= BtnExecute_Click;
        }
        private bool RegistHotKey(string str, HotKeyCallBackHanlder hotKeyCallBackHanlder)
        {
            try
            {
                if (str == "")
                    return false;
                int modifiers = 0;
                Keys vk = Keys.None;
                foreach (string value in str.Split('+'))
                {
                    if (value.Trim() == "Ctrl")
                        modifiers = modifiers + (int)KeyModifiers.Ctrl;
                    else if (value.Trim() == "Alt")
                        modifiers = modifiers + (int)KeyModifiers.Alt;
                    else if (value.Trim() == "Shift")
                        modifiers = modifiers + (int)KeyModifiers.Shift;
                    else
                    {
                        if (Regex.IsMatch(value, @"[0-9]"))
                        {
                            vk = (Keys)Enum.Parse(typeof(Keys), "D" + value.Trim());
                        }
                        else
                        {
                            vk = (Keys)Enum.Parse(typeof(Keys), value.Trim());
                        }
                    }
                }
                h.Regist(Handle, modifiers, vk, hotKeyCallBackHanlder);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 按下快捷键时被调用的方法
        /// </summary>
        public void ButtonExecute()
        {
            btnExecute.PerformClick();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteCommand()
        {
            if (linkHelp.Text == "关闭")
            {
                txtContent.Text = _CacheContent;
                linkHelp.Text = "帮助";
            }
            if (btnExecute.Text == "停 止")
            {
                TimerTips.Stop();
                TimerExecute.Stop();
                lblInfo.Text = "";
                btnExecute.Text = "开 始";
            }
            else
            {
                if (txtContent.Text == "")
                {
                    return;
                }
                _CommandList = new List<string>();
                _CommandIndex = 0;
                foreach (string r in txtContent.Text.Split('\r'))
                {
                    if (r.Trim() != "")
                    {
                        _CommandList.Add(r.Trim());
                    }
                }
                _WaitTime = _DefaultWaitTime;
                btnExecute.Text = "停 止";
                lblInfo.Text = $" {_WaitTime} 秒后开始！";
                TimerTips.Start();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private void ExecuteInput(string str)
        {
            Clipboard.Clear();
            Clipboard.SetText(str);
            SendKeys.SendWait("^v");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private void ExecuteRun(string str)
        {
            Process.Start(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private void ExecuteKey(string str)
        {
            SendKeys.SendWait(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private void ExecuteSleep(string str)
        {
            int t = 0;
            if (int.TryParse(str, out t))
            {
                if (t > _DefaultInterval)
                {
                    TimerExecute.Interval = t;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ExecuteScreen()
        {
            Clipboard.Clear();
            SendKeys.SendWait("{PRTSC}");
            if (Clipboard.ContainsImage())
            {
                Image img = Clipboard.GetImage();
                SavePic(img);
                img.Dispose();
                Clipboard.Clear();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ExecuteAllScreen()
        {
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, new Size(rect.Width, rect.Height));
                }
                SavePic(bmp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        private void SavePic(Image img)
        {
            string strScreenPath = Application.StartupPath + "\\SCREEN\\";
            if (!System.IO.Directory.Exists(strScreenPath))
            {
                System.IO.Directory.CreateDirectory(strScreenPath);
            }
            img.Save(strScreenPath + Guid.NewGuid().ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private void ExecuteMouseMove(string str)
        {
            string[] strs = str.Split(',');
            if (strs.Length == 2)
            {
                int x = int.Parse(strs[0]);
                int y = int.Parse(strs[1]);
                Cursor.Position = new Point(x, y);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ExecuteMouseClick()
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }
        /// <summary>
        /// 
        /// </summary>
        private void ExecuteMouseDBClick()
        {
            ExecuteMouseClick();
            ExecuteMouseClick();
        }
        #endregion
    }
}
