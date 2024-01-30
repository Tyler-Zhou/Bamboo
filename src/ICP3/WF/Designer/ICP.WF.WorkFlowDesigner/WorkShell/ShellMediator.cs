using System;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;
using System.ComponentModel.Design;
using System.Windows.Forms;
using ICP.WF.ServiceInterface.Client;
using System.Workflow.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.IO;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Collections.Generic;
using ICP.WF.ServiceInterface.DataObject;
using System.Drawing.Printing;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 设计器入口
    /// </summary>
    public class ShellMediator : IPartBridge,IDisposable
    {
        #region 本地变量
        ILayoutBuilderContext currentContext;
        IToolBar wftoolBarService;              // 工具栏
        IWorkToolbox wfToolBoxService;          // 工具箱
        IWorkFileExplorer wfFileService;        // 文件夹浏览
        IWorkFlowDesigner wfDesognerService;    // 设计面版
        IWorkOutput wfOutputService;            // 信息输出面版
        IWorkProperty wfPropertyService;        // 属性面版
        WorkFlowConfigInfo wfInfo = new WorkFlowConfigInfo();
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
            wftoolBarService = context.GetService<IToolBar>();
            wfToolBoxService = context.GetService<IWorkToolbox>();
            wfFileService = context.GetService<IWorkFileExplorer>();
            wfDesognerService = context.GetService<IWorkFlowDesigner>();
            wfOutputService = context.GetService<IWorkOutput>();
            wfPropertyService = context.GetService<IWorkProperty>();

            context.WorkItem.Services.Add<IWorkOutput>(wfOutputService);

            //交互事件
            wfDesognerService.ActiveDesignerChanged += new ActiveDesignerChangedEventHandler(designPart_ActiveDesignerChanged);
            wfDesognerService.SelectionChanged += new SelectedChangedEventHandler(designPart_SelectionChanged);
            wfDesognerService.OnException += new ExceptionEventHandler(toolBoxPart_OnException);
            wfToolBoxService.OnException += new ExceptionEventHandler(toolBoxPart_OnException);
            wfFileService.OpentDesignFormEvent += new EventHandler(wfFileService_OpentDesignFormEvent);

            //添加DesignSurfaceManager服务
            ICPFlwoHostSurfaceManager hostSurfaceManager = (ICPFlwoHostSurfaceManager)context.WorkItem.Services.Get(typeof(ICPFlwoHostSurfaceManager));//new LWFormDesignSurfaceManager();
            if (hostSurfaceManager == null)
            {
                hostSurfaceManager = context.WorkItem.Services.AddNew<ICPFlwoHostSurfaceManager>();
            }
            hostSurfaceManager.AddService(typeof(System.Drawing.Design.IToolboxService), wfToolBoxService);
           // hostSurfaceManager.AddService(typeof(INameCreationService), new LWNameCreationService());

            hostSurfaceManager.AddService(typeof(IWorkFlowExtendService), currentContext.WorkItem.Services.Get<IWorkFlowExtendService>());

            hostSurfaceManager.AddService(typeof(IWorkFlowConfigService), currentContext.WorkItem.Services.Get<IWorkFlowConfigService>());

            hostSurfaceManager.AddService(typeof(IFormDesignClientService), currentContext.WorkItem.Services.Get<IFormDesignClientService>());

            RefreshToolbars(false, false);
        }

        void designPart_ActiveDesignerChanged(object sender, ActiveDesignerChangedEventArgs e)
        {
            wfToolBoxService.CurrentDesignerHost = e.DesignerHost;
            wfPropertyService.CurrentDesignerHost = e.DesignerHost;
            if (e.DesignerHost != null)
            {
                RefreshToolbars(true, false);
            }
            else
            {
                RefreshToolbars(false, false);
            }
        }

        void RefreshToolbars(
          bool isDesignMode,
          bool isSelectControl)
        {
            wftoolBarService.SetEnable("barSaves", isDesignMode);
            wftoolBarService.SetEnable("barPrints", isDesignMode);
            wftoolBarService.SetEnable("barCopy", isSelectControl);
            wftoolBarService.SetEnable("barCut", isSelectControl);
            wftoolBarService.SetEnable("barPaste", isSelectControl);
            wftoolBarService.SetEnable("barDelete", isSelectControl);
            wftoolBarService.SetEnable("barZoomIn", isDesignMode);
            wftoolBarService.SetEnable("barZoomOut", isDesignMode);
            wftoolBarService.SetEnable("barCancelZaom", isDesignMode);
            wftoolBarService.SetEnable("barExpand", isSelectControl);
            wftoolBarService.SetEnable("barCollapse", isSelectControl);

        }

        /// <summary>
        /// 双击打开一个流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wfFileService_OpentDesignFormEvent(object sender, EventArgs e)
        {
            TreeList treeList = (TreeList)sender;
            DocumentNode node = (DocumentNode)treeList.GetDataRecordByNode(treeList.FocusedNode);
            if (File.Exists(node.Path))
            {

                wfDesognerService.WorkflowName = node.Name;
                wfDesognerService.WorkflowCreationType = WorkflowTypes.DefaultSequenceWorkflow;
                wfDesognerService.WorkflowSaveAs = node.Path;

                wfDesognerService.LoadWorkflow(node.Path);
            }
        }

        void designPart_SelectionChanged(object sender, SelectedEventArgs e)
        {
            wfPropertyService.SelectedObject = e.Object;
            if (e.Object != null)
            {
                RefreshToolbars(true, true);
            }
            else
            {
                RefreshToolbars(true, false);
            }
        }

        void toolBoxPart_OnException(object sender, ExceptionEventArgs e)
        {
            wfOutputService.Error(e.Exception.Message);
        }

        /// <summary>
        /// 动态要注入交互面板
        /// </summary>
        /// <typeparam name="T">面板类型</typeparam>
        /// <param name="part">面板</param>
        /// <param name="name">面板名称</param>
        public void Register<T>(T part, string name)
        {
        }

        #endregion


        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_New)]
        public void Command_Flow_New(object s, EventArgs e)
        {
            try
            {
                NewWorkflow newForm = currentContext.WorkItem.Items.AddNew<NewWorkflow>();
                if (newForm.ShowDialog() == DialogResult.OK)
                {
                    wfDesognerService.WorkflowName = newForm.WorkflowName;
                    wfDesognerService.WorkflowCreationType = newForm.WorkflowCreationType;
                    wfDesognerService.WorkflowSaveAs = newForm.WorkflowSaveAs;

                    if (string.IsNullOrEmpty(newForm.TemplateXoml))
                    {
                        wfDesognerService.ShowDefaultWorkflow();
                    }
                    else
                    {
                        wfDesognerService.LoadWorkflow(newForm.TemplateXoml);
                    }

                }
            }
            catch (Exception ex)
            {
                wfOutputService.Error(Utility.GetString("Failly", "失败") + ex.Message);
            }
        }

        /// <summary>
        /// 打开本地
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_OpenLocal)]
        public void Command_Flow_OpenLocal(object s, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = DefaultConfigMananger.Default.FolwDesignFolder.Path;
            dlg.DefaultExt = "xoml";
            dlg.Filter = "xoml files (*.xoml)|*.xoml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                wfDesognerService.WorkflowSaveAs = dlg.FileName;
                wfDesognerService.LoadWorkflow(dlg.FileName);
            }
        }


        /// <summary>
        /// 打开服务端文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_OpenServer)]
        public void Command_Flow_OpenServer(object s, EventArgs e)
        {
            WorkFlowFileDown downForm = currentContext.WorkItem.SmartParts.AddNew<WorkFlowFileDown>();
            if (downForm.ShowDialog() == DialogResult.OK)
            {
                wfInfo = downForm.WFInfo;
                try
                {
                    string fileName = string.Empty;
                    if (downForm.DownWorkFlowFileName.EndsWith(".xoml"))
                    {
                        fileName = downForm.DownWorkFlowFileName;
                    }
                    else
                    {
                        fileName = downForm.DownWorkFlowFileName + ".xoml";
                    }

                    wfDesognerService.WorkflowSaveAs = fileName;
                    wfDesognerService.WorkflowName = System.IO.Path.GetFileName(fileName);
                    wfDesognerService.LoadWorkflow(fileName);
                }
                catch (Exception ex)
                {
                    wfOutputService.Error(Utility.GetString("Failly", "打开工作流程出错") + ex.Message);
                }
            }
        }

        #region 保存
        /// <summary>
        /// 保存为本地文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_SaveLocal)]
        public void Command_Flow_SaveLocal(object s, EventArgs e)
        {
            wfDesognerService.Save(wfDesognerService.WorkflowSaveAs);

            wfFileService.AddFormFileNode(wfDesognerService.WorkflowName, wfDesognerService.WorkflowSaveAs);
        }


        /// <summary>
        /// 保存为服务端文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_SaveServer)]
        public void Command_Flow_SaveServer(object s, EventArgs e)
        {
            try
            {
                if (!ValidateControl())
                {
                    return;
                }

                wfDesognerService.Save(wfDesognerService.WorkflowSaveAs);

                ///错误信息
                List<string> errorList = wfDesognerService.GetErrorList();
                if (errorList.Count > 0)
                {
                    wfOutputService.ClearAll();
                    foreach (string error in errorList)
                    {
                        wfOutputService.Error(Utility.GetString(error, error));
                    }
                    return;
                }
                else
                {
                    //如果没有错误，清空所有的消息
                    wfOutputService.ClearAll();
                }

                WorkFlowConfigSetForm config = currentContext.WorkItem.Items.AddNew<WorkFlowConfigSetForm>();
                config.ID = wfInfo.Id;
                config._flowFile = wfDesognerService.WorkflowSaveAs;
                config._key = wfDesognerService.WorkflowName.Replace(".xoml", "");
                config._ruleFile = wfDesognerService.WorkflowSaveAs.Replace(".xoml", ".rules");
                if (config.ShowDialog() == DialogResult.OK)
                {
                   
                }

            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("Failly", "Save as failly"));

            }

        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_SaveAS)]
        public void Command_Flow_SaveAS(object s, EventArgs e)
        {
            try
            {
                if (!ValidateControl())
                {
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = DefaultConfigMananger.Default.FolwDesignFolder.Path;
                saveFileDialog.Filter = "xoml files (*.xoml)|*.xoml";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    wfDesognerService.Save(saveFileDialog.FileName);
                }
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("Failly", "Save as failly"));
            }

        }

        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Print)]
        public void Command_Flow_Print(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            WorkflowView workflowView = wfDesognerService.WorkFlowView;
            if (workflowView != null)
            {
                PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
                printDialog.AllowPrintToFile = false;
                PrintDocument printDocument = workflowView.PrintDocument;
                printDialog.Document = printDocument;

                try
                {
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("Failly", "失败!"));
                }
            }
        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_PrintPreview)]
        public void Command_Flow_PrintPreview(object s, EventArgs e)
        {
            try
            {
                if (!ValidateControl())
                {
                    return;
                }

                wfDesognerService.StartLayout();
                wfDesognerService.WorkFlowView.PrintPreviewMode = !wfDesognerService.WorkFlowView.PrintPreviewMode;
                wfDesognerService.WorkFlowView.ClientSize = wfDesognerService.GetSize;
                wfDesognerService.EndLayout();


            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("Failly", "Print Preview failly"));
            }
        }

        /// <summary>
        /// 打印设置
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_PrintSet)]
        public void Command_Flow_PrintSet(object s, EventArgs e)
        {
            try
            {
                if (!ValidateControl())
                {
                    return;
                }

                WorkflowPageSetupDialog pageSetupDialog = new WorkflowPageSetupDialog(wfDesognerService.WorkFlowView as IServiceProvider);
                if (DialogResult.OK == pageSetupDialog.ShowDialog())
                {
                    wfDesognerService.ReLayout();
                }
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("Failly", "Print Set failly"));
            }
        }

        #endregion

        #region 活动视图操作
        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Remove)]
        public void Command_Flow_Remove(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(StandardCommands.Delete);
        }

        /// <summary>
        /// 拷贝控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Copy)]
        public void Command_Flow_Copy(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(StandardCommands.Copy);
        }

        /// <summary>
        /// 粘贴控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Paster)]
        public void Command_Flow_Paster(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(StandardCommands.Paste);
        }

        /// <summary>
        /// 剪切控件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Cut)]
        public void Command_Flow_Cut(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(StandardCommands.Cut);
        }

        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_ZoomIn)]
        public void Command_Flow_ZoomIn(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(WorkflowMenuCommands.ZoomIn);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_ZoomOut)]
        public void Command_Flow_ZoomOut(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(WorkflowMenuCommands.ZoomOut);
        }
        /// <summary>
        /// 取消变焦
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_CancelZaom)]
        public void Command_Flow_CancelZaom(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            SendKeys.Send("{ESC}");
        }

        /// <summary>
        /// 选择的比例发生改变时
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [Microsoft.Practices.CompositeUI.EventBroker.EventSubscription("CmbZoomLevel_SelectValuCharge")]
        public void Command_Flow_ZoomLevelSelectedValueChanged(object s, ValueChangedEventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            if (string.IsNullOrEmpty(e.Value))
            {
                wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom100Mode);
            }
            else
            {
                string value = e.Value;
                switch (value)
                {
                    case "50%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom50Mode);
                        break;
                    case "75%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom75Mode);
                        break;
                    case "100%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom100Mode);
                        break;
                    case "150%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom150Mode);
                        break;
                    case "200%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom200Mode);
                        break;
                    case "300%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom300Mode);
                        break;
                    case "400%":
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom400Mode);
                        break;
                    default:
                        wfDesognerService.ExecuteAction(WorkflowMenuCommands.Zoom100Mode);
                        break;
                }

            }
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Expand)]
        public void Command_Flow_Expand(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(WorkflowMenuCommands.Expand);
        }

        /// <summary>
        /// 折叠
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Collapse)]
        public void Command_Flow_Collapse(object s, EventArgs e)
        {
            if (!ValidateControl())
            {
                return;
            }
            wfDesognerService.ExecuteAction(WorkflowMenuCommands.Collapse);
        }

        #endregion

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_Flow_Close)]
        public void Command_Flow_Close(object s, EventArgs e)
        {
            if (wftoolBarService != null)
            {

                ((Control)wftoolBarService).FindForm().Close();
                //currentContext.WorkItem.Terminate();
            }
        }

        /// <summary>
        /// 验证控件
        /// </summary>
        /// <returns></returns>
        private bool ValidateControl()
        {
            if (wfDesognerService == null)
            {
                return false;
            }
            if (wfDesognerService.WorkFlowView == null)
            {
                return false;
            }
            return true;
        }



        #region IDisposable 成员

        public void Dispose()
        {
            this.currentContext = null;
            this.wftoolBarService = null;
            if (this.wfToolBoxService != null)
            {
                wfToolBoxService.OnException -= this.toolBoxPart_OnException;
                this.wfToolBoxService = null;
            }
            if (this.wfFileService != null)
            {
                this.wfFileService.OpentDesignFormEvent -= this.wfFileService_OpentDesignFormEvent;
                this.wfFileService = null;
            }
            if (wfDesognerService != null)
            {
                wfDesognerService.ActiveDesignerChanged -= designPart_ActiveDesignerChanged;
                wfDesognerService.SelectionChanged -= designPart_SelectionChanged;
                wfDesognerService.OnException -= toolBoxPart_OnException;
                this.wfDesognerService = null;
            }
            this.wfOutputService = null;
            this.wfPropertyService = null;
            this.wfInfo = null;
        }

        #endregion
    }
}
