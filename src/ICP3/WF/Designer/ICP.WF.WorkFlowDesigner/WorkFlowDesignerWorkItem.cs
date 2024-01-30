using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.WF.ServiceInterface;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 流程设计器WorkItem
    /// </summary>
    public class WorkFlowDesignerWorkItem : WorkItem
    {

        #region 服务
        /// <summary>
        /// 工作流管理服务
        /// </summary>
        public IWorkflowService WorkflowService
        {
            get 
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }

        /// <summary>
        /// 工作流管理服务
        /// </summary>
        public IWorkFlowExtendService WorkFlowExtendService
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }


        /// <summary>
        /// 根据布局生成UI界面服务
        /// </summary>
        public IUIBuilder UIBuilder
        {
            get
            {
                return ServiceClient.GetClientService<IUIBuilder>();
            }
        }

        #endregion
        UILayout mainLayout;
        #region 本地方法

        /// <summary>
        /// 显示当前场景
        /// </summary>
        /// <param name="workSpace">WorkSpace</param>
        /// <param name="title">标题</param>
        public void Show( IWorkspace workSpace, string title)
        {
             mainLayout = new UILayout();

            //构建界面布局
            mainLayout.Layout = new PanelLayout
            {
                Childs = new List<BaseLayout>{
                            new SimpleControlLayout
                            {
                                 ControlType=typeof(WorkFlowMainTool),
                                 Properties=new ControlLayoutProperty{ 
                                     Dock = DockStyle.Top, 
                                     Height=22f,
                                     Name=typeof(WorkFlowMainTool).Name, 
                                     Text=LocalData.IsEnglish?"Tool":"工具栏"}
                            },
                            new PanelLayout{
                               Childs =new List<BaseLayout>{
                                          new DockPanelTabLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(WorkToolbox),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(WorkToolbox).Name,
                                                                            Text=LocalData.IsEnglish?"Tool":"工具栏"}
                                                                    },
                                                                     new SimpleControlLayout{ 
                                                                        ControlType=typeof(WorkFileExplorer), 
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill, 
                                                                            Name=typeof(WorkFileExplorer).Name, 
                                                                            Text=LocalData.IsEnglish?"Files":"文件浏览器"}
                                                                     }
                                                                },
                                                                 
                                                                Properties=new DockPanelTabLayoutProperty{
                                                                    Name=typeof(WorkToolbox).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"Tool":"工具箱",Dock= DockStyle.Left}
                                                            },
                                        new PanelLayout{
                                                Childs=new List<BaseLayout>{
                                                     new DockPanelLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(WorkProperty),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(WorkProperty).Name,}
                                                                    }
                                                                },
                                                                         
                                                                Properties=new DockPanelControlLayoutProperty{
                                                                    Name=typeof(WorkProperty).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"Property":"属性",
                                                                    Dock= DockStyle.Right}
                                                            },

                                                             new PanelLayout{
                                                Childs=new List<BaseLayout>{
                                                      new SimpleControlLayout
                                                                {
                                                                     ControlType=typeof(WorkFlowDesigner),
                                                                     Properties=new ControlLayoutProperty{ 
                                                                         Dock = DockStyle.Fill, 
                                                                         Height=600f,
                                                                         Name=typeof(WorkFlowDesigner).Name, 
                                                                         Text=LocalData.IsEnglish?"Designer":"设计器"}
                                                                },

                                                     new DockPanelLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(WorkOutput),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(WorkOutput).Name,}
                                                                    }
                                                                },
                                                                         
                                                                Properties=new DockPanelControlLayoutProperty{
                                                                    Name=typeof(WorkOutput).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"Out Info":"输出",
                                                                    Height=200f,
                                                                    Dock= DockStyle.Bottom}
                                                            },
                                                        
                                                    },

                                                    Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
                                                },
                                  
                                                     
                                                },

                                                    Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
                                             },
                                        },
                                      
                               Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
                            }},
                Properties = new PanelLayoutProperty { Dock = DockStyle.Fill }
            };

            //构建面版之间处理逻辑
            mainLayout.Relations = new List<IPartRelation>
            {
                 new PartRelation{ 
                     Controller=typeof(ShellMediator), 
                     Name="ShellMediator", 
                     PartNames=new List<string>{
                         typeof(WorkToolbox).Name,
                         typeof(WorkOutput).Name,
                         typeof(WorkFlowDesigner).Name,
                         typeof(WorkProperty).Name,
                         typeof(WorkToolbox).Name
                     }
                 }
            };


            //生成界面
            UIBuilder.Build(
                this,
                workSpace,
                title,
                mainLayout);
        }

        #endregion
        /// <summary>
        /// 关闭当前场景(在这里处理释放所有该场景创建的资源)
        /// </summary>
        public void Close()
        {
            if (this.mainLayout != null)
            {
                this.mainLayout.Dispose();
                this.mainLayout = null;
            }
            if (this.Status != WorkItemStatus.Terminated)
            {
                this.Terminate();
            }

        }
     
    }
}
