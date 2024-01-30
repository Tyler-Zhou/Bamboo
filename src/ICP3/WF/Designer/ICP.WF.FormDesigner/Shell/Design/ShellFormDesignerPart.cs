//-----------------------------------------------------------------------
// <copyright file="ShellFormDesignerControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraTab;
    using ICP.WF.Controls;
    using Microsoft.Practices.CompositeUI;
    using System.Collections.Generic;

    /// <summary>
    /// 设计器控件
    /// </summary>
    public partial class ShellFormDesignerPart : XtraUserControl, IDesignPart
    {
        public event SelectedChangedEventHandler SelectionChanged;

        public event ExceptionEventHandler OnException;

        public IDesignerHost CurrentDesignerHost { get; set; }

        public event ActiveDesignerChangedEventHandler ActiveDesignerChanged;


        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        public ShellFormDesignerPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.SelectionChanged = null;
                this.OnException = null;
                this.CurrentDesignerHost = null;
                this.ActiveDesignerChanged = null;
                this.tabMainDesign.SelectedPageChanged -= tabMainDesign_SelectedPageChanged;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Init();

        }

        private void Init()
        {
            LWFormDesignSurfaceManager hostSurfaceManager = (LWFormDesignSurfaceManager)this.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));//new LWFormDesignSurfaceManager();
            if (hostSurfaceManager == null)
            {
                //添加LWFormDesignSurfaceManager服务
                hostSurfaceManager = this.WorkItem.Services.AddNew<LWFormDesignSurfaceManager>();
            }

            // this.tabMainDesign.KeyDown += new KeyEventHandler(tabMainDesign_KeyDown);
            this.tabMainDesign.SelectedPageChanged += new TabPageChangedEventHandler(tabMainDesign_SelectedPageChanged);
        }

        public string FormFilePath
        {
            get
            {
                LWFormDesignSurfaceManager hostSurfaceManager = (LWFormDesignSurfaceManager)this.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));
                if (hostSurfaceManager == null
                    || hostSurfaceManager.ActiveDesignSurface == null)
                {
                    return string.Empty;
                }

                XMLDesignerLoader loader = (XMLDesignerLoader)((LWFormDesignSurface)hostSurfaceManager.ActiveDesignSurface).DesignerLoader;
                if (loader == null)
                {
                    return string.Empty;
                }

                return loader.FileName;
            }
        }


        public string Title
        {
            get
            {
                if (tabMainDesign.SelectedTabPage.Tag != null)
                {
                   return this.tabMainDesign.SelectedTabPage.Text;
                }

                return string.Empty;
            }
        }

        public void ShowXMLCode()
        {
            if (this.tabMainDesign.SelectedTabPage == null|| this.tabMainDesign.SelectedTabPage.Tag == null)
            {
                return;
            }

            string tagText=this.tabMainDesign.SelectedTabPage.Text + "_Code";
            LWFormDesignSurfaceManager hostSurfaceManager = (LWFormDesignSurfaceManager)this.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));
            if (hostSurfaceManager == null
                || hostSurfaceManager.ActiveDesignSurface == null)
            {
                return;
            }
            XtraTabPage existPage = FindExistDesigner(tagText);
            if (existPage != null)
            {
                this.tabMainDesign.SelectedTabPage = existPage;
                return;
            }

            XMLDesignerLoader loader = (XMLDesignerLoader)((LWFormDesignSurface)hostSurfaceManager.ActiveDesignSurface).DesignerLoader;
            if (loader == null)
            {
                return;
            }
            XtraTabPage tabpage = new XtraTabPage();
            tabpage.Text = this.tabMainDesign.SelectedTabPage.Text + "_Code";
            tabpage.Tag = null;
            tabpage.AllowDrop = true;

            ShellCodePart codePart = new ShellCodePart();
            codePart.Parent = tabpage;
            codePart.Dock = DockStyle.Fill;
            codePart.Code = loader.GetCode();

            this.tabMainDesign.TabPages.Add(tabpage);
            this.tabMainDesign.SelectedTabPage = tabpage;

            hostSurfaceManager.ActiveDesignSurface = null;
        }


        public void NewFormDesigner(
            string title,
            string savePath,
            string templateFileName)
        {
            XtraTabPage existPage = FindExistDesigner(title);
            if (existPage != null)
            {
                this.tabMainDesign.SelectedTabPage = existPage;
                return;
            }

          

            LWFormDesignSurfaceManager hostSurfaceManager = (LWFormDesignSurfaceManager)this.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));
            LWHostControl hc = null;
            if (!string.IsNullOrEmpty(templateFileName)
                && System.IO.File.Exists(templateFileName))
            {
                hc = hostSurfaceManager.NewHost(templateFileName);
            }
            else if (!string.IsNullOrEmpty(savePath)
                && System.IO.File.Exists(savePath))
            {
                hc = hostSurfaceManager.NewHost(savePath);
            }
            else
            {
                hc = hostSurfaceManager.NewHost(
                   typeof(LWBaseForm),
                   LoaderType.XmlDesignerLoader);
            }

            XMLDesignerLoader loader = (XMLDesignerLoader)((LWFormDesignSurface)hostSurfaceManager.ActiveDesignSurface).DesignerLoader;
            if (loader != null)
            {
                loader.FileName = savePath;
            }

            XtraTabPage tabpage = new XtraTabPage();
            tabpage.Text = title;
            tabpage.Tag = LoaderType.XmlDesignerLoader;
            tabpage.AllowDrop = true;
            hc.Parent = tabpage;
            hc.Dock = DockStyle.Fill;
            this.tabMainDesign.TabPages.Add(tabpage);
            this.tabMainDesign.SelectedTabPage = tabpage;
            hostSurfaceManager.ActiveDesignSurface = hc.HostSurface;

            ISelectionService selectionService = (ISelectionService)hc.HostSurface.GetService(typeof(ISelectionService));
            if (selectionService != null)
            {
                selectionService.SelectionChanged += delegate(object sender, EventArgs arg)
                {
                    ICollection selectedComponents = selectionService.GetSelectedComponents();
                    if (this.SelectionChanged != null)
                    {
                        object[] comps = new object[selectedComponents.Count];
                        int i = 0;
                        foreach (Object o in selectedComponents)
                        {
                            comps[i] = o;
                            i++;
                        }

                        if (i > 0)
                        {
                            SelectedEventArgs args = new SelectedEventArgs(comps[i - 1]);
                            this.SelectionChanged(sender, args);
                        }
                    }
                };
            }
        }



        public void OpenForm(string filename)
        {
        }

        public void CloseActive()
        {
        }

        public void CloseAll()
        {
        }

        public void Save()
        {
            LWFormDesignSurfaceManager hostSurfaceManager = (LWFormDesignSurfaceManager)this.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));
            if (hostSurfaceManager == null
                || hostSurfaceManager.ActiveDesignSurface == null)
            {
                return;
            }

            XMLDesignerLoader loader = (XMLDesignerLoader)((LWFormDesignSurface)hostSurfaceManager.ActiveDesignSurface).DesignerLoader;
            if (loader == null)
            {
                return;
            }

            loader.Save();
        }

        public List<string> GetErrorList()
        {
            LWFormDesignSurfaceManager hostSurfaceManager = (LWFormDesignSurfaceManager)this.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));
            if (hostSurfaceManager == null
                || hostSurfaceManager.ActiveDesignSurface == null)
            {
                return null;
            }

            XMLDesignerLoader loader = (XMLDesignerLoader)((LWFormDesignSurface)hostSurfaceManager.ActiveDesignSurface).DesignerLoader;
            if (loader == null)
            {
                return null;
            }

            return loader.Validate();


        }

        /// <summary>
        /// 当前宿主控件
        /// </summary>
        public LWHostControl CurrentHostControl
        {
            get
            {
                if (this.tabMainDesign.SelectedTabPage != null
                    && this.tabMainDesign.SelectedTabPage.Tag != null
                    && this.tabMainDesign.SelectedTabPage.Controls.Count > 0
                    && this.tabMainDesign.SelectedTabPage.Controls[0] is LWHostControl)
                {
                    return (LWHostControl)this.tabMainDesign.SelectedTabPage.Controls[0];
                }

                return null;
            }
        }

        bool HasFocused(Control control)
        {
            if (control.Focused)
            {
                return true;
            }

            if (control.HasChildren)
            {
                foreach (Control c in control.Controls)
                {
                    if (HasFocused(c))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ExecuteAction(CommandID commandname)
        {
            if (commandname == null) return;
            if (this.CurrentHostControl == null)
            {
                return;
            }
            if (!HasFocused(this.CurrentHostControl)
                && commandname == StandardCommands.Delete)
            {
                return;
            }

            try
            {
                if (commandname == StandardCommands.Redo)
                {
                   IDesignSurfaceExt designSurface= (IDesignSurfaceExt)this.CurrentHostControl.HostSurface;
                   designSurface.GetUndoEngineExt().Redo();
                }
                else if (commandname == StandardCommands.Undo)
                {
                    IDesignSurfaceExt designSurface = (IDesignSurfaceExt)this.CurrentHostControl.HostSurface;
                    designSurface.GetUndoEngineExt().Undo();
                }
                else
                {
                    IMenuCommandService ims = this.CurrentHostControl.HostSurface.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
                    ims.GlobalInvoke(commandname);
                }
            }
            catch (Exception ex)
            {
                OnException(this, new ExceptionEventArgs(ex));
            }
        }


        void tabMainDesign_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (this.CurrentHostControl != null
               && this.ActiveDesignerChanged != null)
            {

                ActiveDesignerChanged(this, new ActiveDesignerChangedEventArgs(this.CurrentHostControl.DesignerHost, CurrentHostControl.HostSurface));
            }
        }

        private void barCloseCurrent_Click(object sender, EventArgs e)
        {
            if (this.tabMainDesign.SelectedTabPage != null)
            {
                this.tabMainDesign.TabPages.Remove(this.tabMainDesign.SelectedTabPage);
            }
        }

        private void barCloseAll_Click(object sender, EventArgs e)
        {
            this.tabMainDesign.SelectedPageChanged -= new TabPageChangedEventHandler(tabMainDesign_SelectedPageChanged);

            System.Collections.Generic.List<XtraTabPage> pages = new System.Collections.Generic.List<XtraTabPage>();
            foreach (XtraTabPage page in this.tabMainDesign.TabPages)
            {
                pages.Add(page);

            }

            foreach (XtraTabPage page in pages)
            {
                if (page != this.tabMainDesign.SelectedTabPage)
                {
                    this.tabMainDesign.TabPages.Remove(page);
                }
            }

            this.tabMainDesign.SelectedPageChanged += new TabPageChangedEventHandler(tabMainDesign_SelectedPageChanged);
        }


        private string GenerateFormNum(string prefix)
        {
            XtraTabPage lastPage = null;
            foreach (XtraTabPage page in this.tabMainDesign.TabPages)
            {
                if (page.Text.StartsWith(prefix))
                {
                    lastPage = page;
                }
            }

            if (lastPage != null)
            {
                string lastChar = lastPage.Text.Substring(lastPage.Text.Length - 1, 1);
                if (!Char.IsDigit(lastChar, 0))
                {
                    return prefix.Trim() + "1";
                }
                else
                {
                    return lastPage.Text.TrimEnd(lastChar.ToCharArray()) + (int.Parse(lastChar) + 1).ToString();
                }
            }

            return prefix;
        }

        private XtraTabPage FindExistDesigner(string formName)
        {
            foreach (XtraTabPage page in this.tabMainDesign.TabPages)
            {
                if (page.Text.Equals(formName))
                {
                    return page;
                }
            }

            return null;
        }

       
    }

    public class ActiveDesignerChangedEventArgs : EventArgs
    {
        public IDesignerHost DesignerHost { get; set; }
        public LWFormDesignSurface LWdesignSurface { get; set; }

        public ActiveDesignerChangedEventArgs(IDesignerHost designerHost, LWFormDesignSurface designSurface)
        {
            this.DesignerHost = designerHost;
            this.LWdesignSurface = designSurface;
        }
    }

    public delegate void ActiveDesignerChangedEventHandler(object sender, ActiveDesignerChangedEventArgs e);

}
