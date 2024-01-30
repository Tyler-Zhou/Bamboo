//-----------------------------------------------------------------------
// <copyright file="CommpanyConfigureWorkItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using DevExpress.XtraBars.Docking;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 公司配置场景管理WorkItem
    /// </summary>
    internal class CommpanyConfigureWorkItem : WorkItem
    {
        #region 服务

        /// <summary>
        /// 根据布局生成UI界面服务
        /// </summary>
        public IUIBuilder UIBuilderService
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
            //构造公司配置子布局
            DockPanelTabLayout childLayout = new DockPanelTabLayout
            {
                Childs = new List<BaseLayout> { 

                    new SimpleControlLayout{ 
                        ControlType=typeof(CommpanyConfigureEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CommpanyConfigureEditPart).Name, 
                            Text="编辑公司配置"}
                    },

                    new SimpleControlLayout{ 
                        ControlType=typeof(CommpanyEDIConfigureListPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CommpanyEDIConfigureListPart).Name, 
                            Text="EDI配置"}
                    },


                    new SimpleControlLayout{ 
                        ControlType=typeof(CommpanyReportConfigureListPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CommpanyReportConfigureListPart).Name, 
                            Text="报表配置"}
                    },

                     new SimpleControlLayout{ 
                        ControlType=typeof(CompanyFaxConfigureListPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CompanyFaxConfigureListPart).Name, 
                            Text="传真配置"}
                    }
                },

                Properties = new DockPanelTabLayoutProperty
                {
                    Height = 300f,
                    Name = "CommpanyConfigureChildPanel",
                    Dock = DockStyle.Bottom,
                    TabsPosition = TabsPosition.Bottom
                }
            };

             mainLayout = UILayoutHelper.BuilLayout<
                CommpanyConfigureToolBarPart,
                CompanyConfigureSearchPart,
                CommpanyConfigureListPart,
                CommpanyConfigureBridge>(childLayout);

            //生成界面
            UIBuilderService.Build(
                this,
                workSpace,
                title,
                mainLayout);
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
