//-----------------------------------------------------------------------
// <copyright file="ShellMediator.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.ComponentModel.Design;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using ICP.Framework.ClientComponents.UIFramework;
    using ICP.WF.FormDesigner.Common;
    using Microsoft.Practices.CompositeUI.Commands;
    using DevExpress.XtraTreeList.Nodes;
    using ICP.Framework.CommonLibrary.Client;
    using System.Collections.Generic;
using ICP.Framework.ClientComponents.Controls;

    /// <summary>
    /// 页面之间交互处理逻辑
    /// </summary>
    public class ShellMediator : IPartBridge,IDisposable
    {
        #region 本地变量
        ILayoutBuilderContext currentContext;
        IToolBar toolBarPart;               //工具栏
        IToolboxPart toolBoxPart;        //工具箱
        IFileExplorerPart fileExplorerPart; //文件夹浏览
        IDesignPart designPart;             //设计面版
        IOutputPart outputPart;             //信息输出面版
        IPropertyPart propertyPart;         //属性面版
        IErrorTraceService errorTraceService;
        LWFormDesignSurfaceManager hostSurfaceManager;

        #endregion

        #region IPartBridge接口

        /// <summary>
        /// 初始化
        /// <remarks>
        /// 把由布局生成的面板信息初始化到该处,以便在此处理各个部件之间的交互.
        /// 由布局生成类调用该接口
        /// </remarks>
        /// </summary>
        /// <param name="context">生成上下文</param>
        /// <param name="partNames">处理那几个部件之间的交互</param>
        public void Init(
            ILayoutBuilderContext context,
            string[] partNames)
        {
            //当前上下文
            currentContext = context;

            //设置要交互的面版
            toolBarPart = context.GetService<IToolBar>();
            toolBoxPart = context.GetService<IToolboxPart>();
            fileExplorerPart = context.GetService<IFileExplorerPart>();
            designPart = context.GetService<IDesignPart>();
            outputPart = context.GetService<IOutputPart>();
            propertyPart = context.GetService<IPropertyPart>();

            //添加DesignSurfaceManager服务
            hostSurfaceManager = (LWFormDesignSurfaceManager)context.WorkItem.Services.Get(typeof(LWFormDesignSurfaceManager));//new LWFormDesignSurfaceManager();
            if (hostSurfaceManager == null)
            {
                hostSurfaceManager = context.WorkItem.Services.AddNew<LWFormDesignSurfaceManager>();
            }
            hostSurfaceManager.AddService(typeof(IToolboxService), toolBoxPart);
            //hostSurfaceManager.AddService(typeof(INameCreationService), new LWNameCreationService());
            hostSurfaceManager.AddService(typeof(ITypeDiscoveryService), new LWTypeDiscoveryService());
            errorTraceService = (IErrorTraceService)currentContext.WorkItem.Services.Get<IErrorTraceService>();
            hostSurfaceManager.AddService(typeof(IErrorTraceService), errorTraceService);

            context.WorkItem.Services.Add<IOutputPart>(outputPart);

            //交互事件
            designPart.ActiveDesignerChanged += new ActiveDesignerChangedEventHandler(designPart_ActiveDesignerChanged);
            designPart.SelectionChanged += new SelectedChangedEventHandler(designPart_SelectionChanged);
            toolBoxPart.OnException += new ExceptionEventHandler(toolBoxPart_OnException);
            designPart.OnException += new ExceptionEventHandler(toolBoxPart_OnException);
            fileExplorerPart.OpentDesignFormEvent += new EventHandler(fileExplorerPart_OpentDesignFormEvent);


            this.RefreshToolbars(false, false);
        }

        /// <summary>
        /// 动态要注入交互面板
        /// </summary>
        /// <typeparam name="T">面板类型</typeparam>
        /// <param name="part">面板</param>
        /// <param name="name">面板名称</param>
        public void Register<T>(
            T part,
            string name)
        {
        }

        #endregion

        #region 面板交互逻辑处理

        void fileExplorerPart_OpentDesignFormEvent(object sender, EventArgs e)
        {
            TreeListNode node = (TreeListNode)sender;
            if (node == null)
            {
                return;
            }

            try
            {
                string path = node.GetValue("Path").ToString();
                string formName = node.GetValue("Name").ToString();
                if (!path.EndsWith(".xml"))
                {
                    return;
                }

                designPart.NewFormDesigner(
                    formName,
                    path,
                    string.Empty);
            }
            catch (Exception ex)
            {
                outputPart.Error(ex.Message);
            }
        }

        void designPart_SelectionChanged(object sender, SelectedEventArgs e)
        {
            propertyPart.SelectedObject = e.Object;
            this.RefreshToolbars(true, true);
        }

        void toolBoxPart_OnException(object sender, ExceptionEventArgs e)
        {
            outputPart.Error(e.Exception.Message);
        }

        void designPart_ActiveDesignerChanged(object sender, ActiveDesignerChangedEventArgs e)
        {
            toolBoxPart.CurrentDesignerHost = e.DesignerHost;
            propertyPart.CurrentDesignerHost = e.DesignerHost;
            hostSurfaceManager.ActiveDesignSurface = e.LWdesignSurface;

            if (e.DesignerHost != null)
            {
                this.RefreshToolbars(true, false);
            }
            else
            {
                this.RefreshToolbars(false, false);
            }

            // codePart.CurrentDesignerHost = e.DesignerHost;
        }


        void RefreshToolbars(
            bool isDesignMode,
            bool isSelectControl)
        {
            toolBarPart.SetEnable("barSave", false);
            toolBarPart.SetEnable("barCopy", false);
            toolBarPart.SetEnable("barCut", false);
            toolBarPart.SetEnable("barPaste", false);
            toolBarPart.SetEnable("barDelete", false);
            toolBarPart.SetEnable("barLeft", false);
            toolBarPart.SetEnable("barRight", false);
            toolBarPart.SetEnable("barCenter", false);
            toolBarPart.SetEnable("barTop", false);
            toolBarPart.SetEnable("barBottom", false);
            toolBarPart.SetEnable("barMiddle", false);
            toolBarPart.SetEnable("barViewCode", false);
            toolBarPart.SetEnable("barTabOrder", false);

            if (isDesignMode)
            {
                toolBarPart.SetEnable("barViewCode", true);
                toolBarPart.SetEnable("barSave", true);
                toolBarPart.SetEnable("barTabOrder",true);
                if (isSelectControl)
                {
                    toolBarPart.SetEnable("barCopy", true);
                    toolBarPart.SetEnable("barCut", true);
                    toolBarPart.SetEnable("barPaste", true);
                    toolBarPart.SetEnable("barDelete", true);
                    toolBarPart.SetEnable("barLeft", true);
                    toolBarPart.SetEnable("barRight", true);
                    toolBarPart.SetEnable("barCenter", true);
                    toolBarPart.SetEnable("barTop", true);
                    toolBarPart.SetEnable("barBottom", true);
                    toolBarPart.SetEnable("barMiddle", true);
                }
            }

        }
        #endregion





        #region 菜单事件处理

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_New)]
        public void Command_Form_New(object s, EventArgs e)
        {
            try
            {
                NewForm templateForm = new NewForm();
                DialogResult dlg = templateForm.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    designPart.NewFormDesigner(
                        templateForm.FormName,
                        templateForm.SaveAsPath,
                        templateForm.TemplateFile);
                }
            }
            catch (Exception ex)
            {
                outputPart.Error(Utility.GetString("Failly", "失败") + ex.Message);
            }
        }

        /// <summary>
        /// 打开本地
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_OpenLocal)]
        public void Command_Form_OpenLocal(object s, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = ICP.WF.ServiceInterface.Client.DefaultConfigMananger.Default.FormDesignFolder.Path;
            dlg.DefaultExt = "xml";
            dlg.Filter = "xml files|*.xml";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                string title = "Form";
                if (System.IO.File.Exists(dlg.FileName))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(dlg.FileName);
                    title = fi.Name;
                }

                designPart.NewFormDesigner(
                    title,
                    dlg.FileName,
                    string.Empty);
            }
            catch (Exception ex)
            {
                outputPart.Error(ex.Message);
            }
        }


        /// <summary>
        /// 打开服务端文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_OpenServer)]
        public void Command_Form_OpenServer(object s, EventArgs e)
        {
            FileDownForm vForm = currentContext.WorkItem.SmartParts.AddNew<FileDownForm>();
            DialogResult dlg = vForm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                try
                {
                    string fileName = vForm.FormDownFileName;
                    string title = "Form";
                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                        title = fi.Name;
                    }
                    designPart.NewFormDesigner(
                        title,
                        fileName,
                        string.Empty);
                }
                catch (Exception ex)
                {
                    outputPart.Error(Utility.GetString("Failly", "打开设计表单出错") + ex.Message);
                }
            }
        }


        /// <summary>
        /// 保存为本地文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_SaveLocal)]
        public void Command_Form_SaveLocal(object s, EventArgs e)
        {
            try
            {
                outputPart.ClearAll();

                designPart.Save();

                fileExplorerPart.AddFormFileNode(
                    designPart.Title,
                    designPart.FormFilePath);

                errorTraceService.SetSuccessfullyInfo(((Control)designPart).FindForm(), "保存成功!");
            }
            catch (Exception ex)
            {
                outputPart.Error(ex.Message);
            }
        }


        /// <summary>
        /// 保存为服务端文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_SaveServer)]
        public void Command_Form_SaveServer(object s, EventArgs e)
        {
            try
            {
                outputPart.ClearAll();

                designPart.Save();

                ///验证错误信息
                List<string> errorList = designPart.GetErrorList();
                if (errorList != null && errorList.Count > 0)
                {
                    foreach (string str in errorList)
                    {
                        outputPart.Error(Utility.GetString(str,str));
                    }
                    return;
                }

                ///保存到服务器
                DeployFormPart deployPart = currentContext.WorkItem.Items.AddNew<DeployFormPart>();
               // DeployForm vForm = currentContext.WorkItem.SmartParts.AddNew<DeployForm>();

                   deployPart.InitData(
                    new System.IO.FileInfo( designPart.FormFilePath).Name.Replace(".xml", ""),
                    designPart.FormFilePath,
                    designPart.FormFilePath.Replace(".xml", ".xsd"));

                string titel=LocalData.IsEnglish?"保存到本地":"保存到服务器";

                PartLoader.ShowDialog(deployPart, titel);

            }
            catch (Exception ex)
            {
                outputPart.Error((Utility.GetString("Failly", "部署失败!") + ex.Message));
            }
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Remove)]
        public void Command_Form_Remove(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.Delete);
        }

        /// <summary>
        /// 拷贝控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Copy)]
        public void Command_Form_Copy(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.Copy);
        }

        /// <summary>
        /// 粘贴控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Paster)]
        public void Command_Form_Paster(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.Paste);
        }

        /// <summary>
        /// 剪切控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Cut)]
        public void Command_Form_Cut(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.Cut);
        }


        /// <summary>
        /// 左对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Left)]
        public void Command_Form_Left(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.AlignLeft);
        }



        /// <summary>
        /// 粘贴控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_UnDo)]
        public void Command_Form_UnDo(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.Undo);
        }

        /// <summary>
        /// 剪切控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_ReDo)]
        public void Command_Form_ReDo(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.Redo);
        }


        /// <summary>
        /// 左对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_TabOrder)]
        public void Command_Form_TabOrder(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.TabOrder);
        }


        /// <summary>
        /// 右对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Right)]
        public void Command_Form_Right(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.AlignRight);
        }


        /// <summary>
        /// 水平居中
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Center)]
        public void Command_Form_Center(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.AlignHorizontalCenters);
        }

        /// <summary>
        /// 顶端对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Top)]
        public void Command_Form_Top(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.AlignTop);
        }

        /// <summary>
        /// 低端对齐
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Bottom)]
        public void Command_Form_Bottom(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.AlignBottom);
        }

        /// <summary>
        /// 垂直居中
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Middle)]
        public void Command_Form_Middle(object s, EventArgs e)
        {
            designPart.ExecuteAction(StandardCommands.CenterHorizontally);
        }

        /// <summary>
        /// 查看代码 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_ViewCode)]
        public void Command_Form_ViewCode(object s, EventArgs e)
        {
            try
            {
                designPart.ShowXMLCode();
            }
            catch (Exception ex)
            {
                outputPart.Error(Utility.GetString("Failly", "失败") + ex.Message);
            }

        }


        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Form_Close)]
        public void Command_Form_Close(object s, EventArgs e)
        {
            if (toolBarPart != null)
            {
                ((Control)toolBarPart).FindForm().Close();
                //currentContext.WorkItem.Terminate();
            }
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.currentContext = null;
            this.toolBarPart = null;
            this.outputPart = null;
            this.propertyPart = null;
            this.errorTraceService = null;
            this.hostSurfaceManager = null;
            if (this.designPart != null)
            {
                //交互事件
                designPart.ActiveDesignerChanged -= designPart_ActiveDesignerChanged;
                designPart.SelectionChanged -= designPart_SelectionChanged;
                designPart.OnException -= toolBoxPart_OnException;
                this.designPart = null;
            }
            if (this.toolBoxPart != null)
            {
                toolBoxPart.OnException -= toolBoxPart_OnException;
                this.toolBoxPart = null;
            }
            if (this.fileExplorerPart != null)
            {
                fileExplorerPart.OpentDesignFormEvent -=fileExplorerPart_OpentDesignFormEvent;
                this.fileExplorerPart = null;
            }
        }

        #endregion
    }
}
