//-----------------------------------------------------------------------
// <copyright file="EDIConfigureWorkItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Configure.EDIConfigure
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using DevExpress.XtraBars.Docking;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// EDI配置场景管理WorkItem
    /// </summary>
    internal class EDIConfigureWorkItem : WorkItem
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
        /// <summary>
        /// 显示当前场景
        /// </summary>
        /// <param name="workSpace">WorkSpace</param>
        public void Show(
            IWorkspace workSpace,
            string title)
        {
            //构造EDI配置子布局
            DockPanelTabLayout childLayout = new DockPanelTabLayout
            {
                Childs = new List<BaseLayout> { 

                    new SimpleControlLayout{ 
                        ControlType=typeof(EDIConfigureEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(EDIConfigureEditPart).Name, 
                            Text="编辑EDI配置"}
                    },

                    new SimpleControlLayout{ 
                        ControlType=typeof(EDICommpanyListEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(EDICommpanyListEditPart).Name, 
                            Text="配置公司"}
                    }
                },

                Properties = new DockPanelTabLayoutProperty
                {
                    Height = 300f,
                    Name = "EDIConfigureChildPanel",
                    Dock = DockStyle.Bottom,
                    TabsPosition = TabsPosition.Bottom
                }
            };
            if (mainLayout != null)
            { 
              
            }
                mainLayout = UILayoutHelper.BuilLayout<
                EDIConfigureToolBarPart,
                EDIConfigureSearchPart,
                EDIConfigureListPart,
                EDIConfigureBridge>(childLayout);             

            //生成界面
            UIBuilder.Build(
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
