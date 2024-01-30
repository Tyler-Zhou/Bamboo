using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraBars;
using System.Runtime.InteropServices;

namespace ICPMailCenter
{
    /// <summary>
    /// 邮件中心主界面
    /// </summary>
    [SmartPart]
    public partial class frmMailCenter : XtraForm, IMainForm, IStatusbar
    {
        private WorkItem rootWorkItem;
        /// <summary>
        /// 错误信息提示面板
        /// </summary>
        public ErrorInfoControl ErrorListControl
        {
            get
            {
                return this.errorInfoControl;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMailCenter()
        {
            InitializeComponent();

        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;

        //        cp.ExStyle |= 0x02000000;
        //        //WS_EX_COMPOSITED. Prevents flickering.

        //        cp.ExStyle |= 0x00080000; //WS_EX_LAYERED. Transparency key
        //        return cp;

        //    }
        //}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workItem"></param>
        [InjectionConstructor]
        public frmMailCenter(WorkItem workItem)
            : this()
        {
            this.rootWorkItem = workItem;


        }


        #region 事件处理

        event EventHandler OnApplicationExit;
        object objectLock = new Object();
        /// <summary>
        /// 应用程序退出自定义事件
        /// </summary>
        public event EventHandler ApplicationExit
        {
            add
            {
                lock (objectLock)
                {
                    OnApplicationExit += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    OnApplicationExit -= value;
                }
            }
        }


        event EventHandler OnActivated;
        public event EventHandler MainFormActivated
        {
            add
            {
                lock (objectLock)
                {
                    OnActivated += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    OnActivated -= value;
                }
            }
        }
        event EventHandler OnDeactivated;
        public event EventHandler MainFormDeactivated
        {
            add
            {
                lock (objectLock)
                {
                    OnDeactivated += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    OnDeactivated -= value;
                }
            }
        }


        private void frmMailCenter_Load(object sender, EventArgs e)
        {
            if (!LocalData.IsDesignMode)
            {
                this.Activated += new EventHandler(frmMailCenter_Activated);
                this.Deactivate += new EventHandler(frmMailCenter_Deactivated);

                this.Text = LocalData.IsEnglish ? this.Text : "邮件中心";
                this.rootWorkItem.Commands[CommandNames.EmailList].Execute();
                SetErrorPanelInfo();
                InnerHideDockPanel();
                TriggerAppStartEvent();

            }
        }

        /// <summary>
        /// 触发启动事件
        /// </summary>
        private void TriggerAppStartEvent()
        {
            if (this.OnApplicationStart != null)
            {
                this.OnApplicationStart(this, EventArgs.Empty);
            }
        }



        void frmMailCenter_Deactivated(object sender, EventArgs e)
        {
            if (this.OnDeactivated != null)
            {
                this.OnDeactivated(sender, e);
            }
        }

        void frmMailCenter_Activated(object sender, EventArgs e)
        {
            if (this.OnActivated != null)
            {
                this.OnActivated(sender, e);
            }
        }


        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                rootWorkItem.Commands["Command_DeleteSelectedMail"].Execute();
                e.Handled = true;
            }
            if (this.OnKeyDownEvent != null)
            {
                this.OnKeyDownEvent(sender, e);
            }

        }
        event KeyEventHandler OnKeyDownEvent;
        public event KeyEventHandler KeyDownEvent
        {
            add
            {
                lock (objectLock)
                {
                    OnKeyDownEvent += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    OnKeyDownEvent -= value;
                }
            }
        }
        /// <summary>
        /// 关闭窗体时保存面板位置，激发程序退出自定义事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            try
            {

                if (OnApplicationExit != null)
                {
                    OnApplicationExit(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 错误面板设置
        private delegate void SetErrorPanelInfoDelegate();
        /// <summary>
        /// 错误面板设置
        /// </summary>
        private void SetErrorPanelInfo()
        {
            if (this.InvokeRequired)
            {
                SetErrorPanelInfoDelegate setDelegate = new SetErrorPanelInfoDelegate(InnerSetErrorPanelInfo);
                this.Invoke(setDelegate);
            }
            else
            {
                InnerSetErrorPanelInfo();
            }

        }
        private void InnerSetErrorPanelInfo()
        {

            this.errorInfoControl.OnShowPanel += delegate(object sender, EventArgs e1)
            {
                if (this.InvokeRequired)
                {
                    SetErrorPanelInfoDelegate setDelegate = new SetErrorPanelInfoDelegate(InnerShowDockPanel1);
                    this.Invoke(setDelegate);
                }
                else
                {
                    InnerShowDockPanel1();
                }

            };
            this.errorInfoControl.OnHidePanel += delegate(object sender, EventArgs e2)
            {
                if (this.InvokeRequired)
                {
                    SetErrorPanelInfoDelegate setDelegate = new SetErrorPanelInfoDelegate(InnerHideDockPanel);
                    this.Invoke(setDelegate);
                }
                else
                {
                    InnerHideDockPanel();
                }

            };
        }
        private void InnerHideDockPanel()
        {
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }
        private void InnerShowDockPanel1()
        {
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            dockPanel1.Show();
        }
        #endregion
        #region IStatusbar Members

        void IStatusbar.SetStatusBarPanel(string text)
        {
            this.Invoke(new EventHandler(delegate
            {
                lblTip.Caption = text;
                clearPanel(lblTip);
            }));
        }

        void IStatusbar.SetStatusBarPanel1(string text)
        {
            ((IStatusbar)this).SetStatusBarPanel(text);
        }

        class ImageText
        {
            public string Text { get; set; }
            public Image Image { get; set; }
            public Exception AttachedException { get; set; }
        }

        Dictionary<object, ImageText> lifecycleStatusText = new Dictionary<object, ImageText>();

        void IStatusbar.SetStatusBarPanel(string text, Image image, Control lifecycleWith)
        {
            this.Invoke(new EventHandler(delegate
            {
                if (lifecycleStatusText.ContainsKey(lifecycleWith))
                {
                    lifecycleStatusText[lifecycleWith] = new ImageText { Image = image, Text = text };
                }
                else
                {
                    lifecycleStatusText.Add(lifecycleWith, new ImageText { Image = image, Text = text });
                }

                lifecycleWith.Disposed += delegate(object sender, EventArgs e)
                {
                    lifecycleStatusText.Remove(lifecycleWith);
                };

                this.lblTip.Caption = text;
                lblTip.Glyph = image;
            }));
        }

        void IStatusbar.SetStatusBarPanel(string text, StatusIconType iconType, Control lifecycleWith)
        {
            Image image;
            switch (iconType)
            {
                case StatusIconType.Warning:
                    image = global::ICPMailCenter.Properties.Resources.warning;
                    break;
                case StatusIconType.Error:
                    image = global::ICPMailCenter.Properties.Resources.warning;
                    break;
                case StatusIconType.Info:
                    image = global::ICPMailCenter.Properties.Resources.info;
                    break;
                default:
                    image = null;
                    break;
            }
            ((IStatusbar)this).SetStatusBarPanel(text, image, lifecycleWith);
        }

        void IStatusbar.SetStatusBarPanel(Exception ex, Control lifecycleWith)
        {
            Image image = global::ICPMailCenter.Properties.Resources.warning;
            string text = ex.Message;
            this.Invoke(new EventHandler(delegate
            {
                if (lifecycleStatusText.ContainsKey(lifecycleWith))
                {
                    lifecycleStatusText[lifecycleWith] = new ImageText { Text = text, Image = image, AttachedException = ex };
                }
                else
                {
                    lifecycleStatusText.Add(lifecycleWith, new ImageText { Text = text, Image = image, AttachedException = ex });
                }
                lifecycleWith.Disposed += delegate(object sender, EventArgs e)
                {
                    lifecycleStatusText.Remove(lifecycleWith);
                };

                lblTip.Caption = text;
                lblTip.Glyph = image;
            }));
        }

        void clearPanel(DevExpress.XtraBars.BarStaticItem label)
        {
            ThreadStart start = new ThreadStart(
                delegate
                {
                    Thread.Sleep(3000);
                    label.Caption = string.Empty;
                    label.Glyph = null;
                }
                );

            Thread thread = new Thread(start);
            thread.Start();
        }


        delegate void _addNotifyIconDelegate(string text, Image image, EventHandler click);
        public void AddNotifyIcon(string text, Image image, EventHandler click)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new _addNotifyIconDelegate(AddNotifyIcon), new object[] { text, image, click });
                return;
            }

            DevExpress.XtraBars.BarButtonItem item = new BarButtonItem();
            item.Caption = text;
            item.Glyph = image;
            item.ItemClick += delegate(object sender, ItemClickEventArgs e) { click(sender, EventArgs.Empty); };
            this.status.AddItem(item);
        }


        #endregion
        #region IMainForm 成员
        event EventHandler OnApplicationStart;
        public event EventHandler ApplicationStart
        {
            add
            {
                lock (objectLock)
                {
                    OnApplicationStart += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    OnApplicationStart -= value;
                }
            }
        }
        #endregion

        private void frmMailCenter_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog();
            }
        }

    }
}
