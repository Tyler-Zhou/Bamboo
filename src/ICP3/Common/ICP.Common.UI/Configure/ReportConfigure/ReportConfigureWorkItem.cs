//-----------------------------------------------------------------------
// <copyright file="ReportConfigureWorkItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Configure.ReportConfigure
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using DevExpress.XtraBars.Docking;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// Report配置场景管理WorkItem
    /// </summary>
    internal class ReportConfigureWorkItem : WorkItem
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
        UILayout mainLayout;
        /// <summary>
        /// 显示当前场景
        /// </summary>
        /// <param name="workSpace">WorkSpace</param>
        public void Show(
            IWorkspace workSpace,
            string title)
        {
            //构造子布局
            DockPanelTabLayout childLayout = new DockPanelTabLayout
            {
                Childs = new List<BaseLayout> { 

                    new SimpleControlLayout{ 
                        ControlType=typeof(ReportConfigureEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(ReportConfigureEditPart).Name, 
                            Text="编辑报表配置"}
                    },

                    new SimpleControlLayout{ 
                        ControlType=typeof(ReportCommpanyListEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(ReportCommpanyListEditPart).Name, 
                            Text="配置公司"}
                    }
                },

                Properties = new DockPanelTabLayoutProperty
                {
                    Height = 245f,
                    Name = "ReportConfigureChildPanel",
                    Dock = DockStyle.Bottom,
                    TabsPosition = TabsPosition.Bottom
                }
            };

             mainLayout = UILayoutHelper.BuilLayout<
                ReportConfigureToolBarPart,
                ReportConfigureSearchPart,
                ReportConfigureListPart,
                ReportConfigureBridge>(childLayout);

            //生成界面
            UIBuilder.Build(
                this,
                workSpace,
                title,
                mainLayout);

            //UILayout layout = new UILayout();

            ////构建界面布局
            //layout.Layout = new PanelLayout
            //{
            //    Childs = new List<BaseLayout>{
            //                new SimpleControlLayout
            //                {
            //                     ControlType=typeof(ReportConfigureToolBarPart),
            //                     Properties=new ControlLayoutProperty{ 
            //                         Dock = DockStyle.Top, 
            //                         Height=22f,
            //                         Name=typeof(ReportConfigureToolBarPart).Name, 
            //                         Text="工具栏"}
            //                },
                   
            //                                                 new PanelLayout{
            //                                    Childs=new List<BaseLayout>{
            //                                          new SimpleControlLayout
            //                                                    {
            //                                                         ControlType=typeof(ReportConfigureListPart),
            //                                                         Properties=new ControlLayoutProperty{ 
            //                                                             Dock = DockStyle.Fill, 
            //                                                             Height=600f,
            //                                                             Name=typeof(ReportConfigureListPart).Name, 
            //                                                             Text="列表"}
            //                                                    },

            //                                         new DockPanelTabLayout{
            //                                                    Childs=new List<BaseLayout>{ 
            //                                                        new SimpleControlLayout {
            //                                                            ControlType=typeof(ReportConfigureEditPart),
            //                                                            Properties=new ControlLayoutProperty{ 
            //                                                                Dock= DockStyle.Fill,
            //                                                                Name=typeof(ReportConfigureEditPart).Name,
            //                                                                Text="编辑报表配置"
            //                                                            }
            //                                                        },

            //                                                         new SimpleControlLayout{ 
            //            ControlType=typeof(ReportCommpanyListEditPart), 
            //            Properties=new ControlLayoutProperty{ 
            //                Dock= DockStyle.Fill, 
            //                Name=typeof(ReportCommpanyListEditPart).Name, 
            //                Text="配置公司"}
            //        }
            //                                                    },
                                                                         
            //                                                    Properties=new DockPanelTabLayoutProperty{
            //                                                        Name=typeof(ReportConfigureEditPart).Name+"DockPanel",
            //                                                        Height=270f,
            //                                                        Dock= DockStyle.Bottom
            //                                                        //TabsPosition = TabsPosition.Bottom,
            //                                                    }
            //                                                },
                                                        
            //                                        },

            //                                        Properties=new PanelLayoutProperty{Dock= DockStyle.Fill}
            //                                    },
                                  
            //                },
            //    Properties = new PanelLayoutProperty { Dock = DockStyle.Fill }
            //};

            ////构建面版之间处理逻辑
            //layout.Relations = new List<IPartRelation>
            //{
            //     new PartRelation{ 
            //         Controller=typeof(ReportConfigureBridge), 
            //         Name="ReportConfigureBridge", 
            //         PartNames=new List<string>{
            //             typeof(ReportConfigureListPart).Name,
            //             typeof(ReportConfigureEditPart).Name,
            //             typeof(ReportCommpanyListEditPart).Name
            //         }
            //     }
            //};

            ////生成界面
            //uiBuilderService.Build(
            //    this,
            //    workSpace,
            //    title,
            //    layout);
        }

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
