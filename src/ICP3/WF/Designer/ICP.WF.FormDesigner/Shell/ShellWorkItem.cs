
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerWorkItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 流程设计器WorkItem
    /// </summary>
    public class ShellWorkItem : WorkItem
    {
        #region 服务

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
        private UILayout mainLayout;
        #region 本地方法

        /// <summary>
        /// 显示当前场景
        /// </summary>
        /// <param name="workSpace">WorkSpace</param>
        /// <param name="title">标题</param>
        public void Show(
            IWorkspace workSpace,
            string title)
        {
            mainLayout = new UILayout();

            //构建界面布局
            mainLayout.Layout = new PanelLayout
            {
                Childs = new List<BaseLayout>{
                            new SimpleControlLayout
                            {
                                 ControlType=typeof(ShellToolBarPart),
                                 Properties=new ControlLayoutProperty{ 
                                     Dock = DockStyle.Top, 
                                     Height=22f,
                                     Name=typeof(ShellToolBarPart).Name, 
                                     Text=LocalData.IsEnglish?"Tool":"工具栏"}
                            },
                            new PanelLayout{
                               Childs =new List<BaseLayout>{
                                          new DockPanelTabLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(ShellToolboxPart),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(ShellToolboxPart).Name,
                                                                            Text=LocalData.IsEnglish?"Tool":"工具箱"}
                                                                    },
                                                                     new SimpleControlLayout{ 
                                                                        ControlType=typeof(ShellFileExplorerPart), 
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill, 
                                                                            Name=typeof(ShellFileExplorerPart).Name, 
                                                                            Text=LocalData.IsEnglish?"Files":"文件浏览器"}
                                                                     }
                                                                },
                                                                 
                                                                Properties=new DockPanelTabLayoutProperty{
                                                                    Name=typeof(ShellToolboxPart).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"Tool":"工具箱",Dock= DockStyle.Left}
                                                            },
                                        new PanelLayout{
                                                Childs=new List<BaseLayout>{
                                                     new DockPanelLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(ShellPropertyPart),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(ShellPropertyPart).Name,}
                                                                    }
                                                                },
                                                                         
                                                                Properties=new DockPanelControlLayoutProperty{
                                                                    Name=typeof(ShellPropertyPart).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish?"Property":"属性",
                                                                    Dock= DockStyle.Right}
                                                            },

                                                             new PanelLayout{
                                                Childs=new List<BaseLayout>{
                                                      new SimpleControlLayout
                                                                {
                                                                     ControlType=typeof(ShellFormDesignerPart),
                                                                     Properties=new ControlLayoutProperty{ 
                                                                         Dock = DockStyle.Fill, 
                                                                         Height=600f,
                                                                         Name=typeof(ShellFormDesignerPart).Name, 
                                                                         Text=LocalData.IsEnglish?"Desinger":"设计器"}
                                                                },

                                                     new DockPanelLayout{
                                                                Childs=new List<BaseLayout>{ 
                                                                    new SimpleControlLayout {
                                                                        ControlType=typeof(ShellOutputPart),
                                                                        Properties=new ControlLayoutProperty{ 
                                                                            Dock= DockStyle.Fill,
                                                                            Name=typeof(ShellOutputPart).Name,}
                                                                    }
                                                                },
                                                                         
                                                                Properties=new DockPanelControlLayoutProperty{
                                                                    Name=typeof(ShellOutputPart).Name+"DockPanel",
                                                                    Text=LocalData.IsEnglish? "Out Info":"输出",
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
                         typeof(ShellToolboxPart).Name,
                         typeof(ShellOutputPart).Name,
                         typeof(ShellFormDesignerPart).Name,
                         typeof(ShellPropertyPart).Name,
                         typeof(ShellToolBarPart).Name
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
