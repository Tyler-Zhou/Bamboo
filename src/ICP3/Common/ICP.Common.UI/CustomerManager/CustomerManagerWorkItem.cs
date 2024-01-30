
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerWorkItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.ClientComponents.UIFramework;
    using DevExpress.XtraBars.Docking;

    /// <summary>
    /// 客户管理场景管理WorkItem
    /// </summary>
    internal class CustomerManagerWorkItem : WorkItem
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
            bool isEnglish = LocalData.IsEnglish;
            //构造客户管理子布局
            DockPanelTabLayout childLayout = new DockPanelTabLayout
            {
                Childs = new List<BaseLayout> { 
                    new SimpleControlLayout{ //begin CustomerManagerContactListEditPart
                        ControlType=typeof(CustomerManagerContactListEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CustomerManagerContactListEditPart).Name, 
                            Text=isEnglish?"Contact":"联系人"}
                    },//end CustomerManagerContactListEditPart


                    new SimpleControlLayout{ //begin CustomerManagerPartnerListEditPart
                        ControlType=typeof(CustomerManagerPartnerListEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CustomerManagerPartnerListEditPart).Name, 
                            Text=isEnglish?"Partners":"合作伙伴"}
                    },//end CustomerManagerPartnerListEditPart

                    new SimpleControlLayout{ //begin CustomerManagerPartnerListEditPart
                        ControlType=typeof(CustomerManagerCombineListEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CustomerManagerCombineListEditPart).Name, 
                            Text=isEnglish?"Customer Combine":"客户合并"}
                        },//end CustomerManagerPartnerListEditPart

                         new SimpleControlLayout{ //begin CustomerManagerMemoListEditPart
                        ControlType=typeof(CustomerManagerMemoListEditPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CustomerManagerMemoListEditPart).Name, 
                            Text=isEnglish?"Customer Remark":"客户备注"}
                        },//end CustomerManagerMemoListEditPart

                        new SimpleControlLayout{ //begin CustomerManagerMemoListEditPart
                        ControlType=typeof(CustomerInvoiceTitleListPart), 
                        Properties=new ControlLayoutProperty{ 
                            Dock= DockStyle.Fill, 
                            Name=typeof(CustomerInvoiceTitleListPart).Name, 
                            Text=isEnglish?"InvoiceTitle":"发票抬头"}
                        }//end CustomerManagerMemoListEditPart

                },//end List<BaseLayout>

                Properties = new DockPanelTabLayoutProperty
                {
                    Height = 220f,
                    Name = "CustomerChildPanel",
                    Dock = DockStyle.Bottom,
                    TabsPosition = TabsPosition.Bottom
                }
            };

            //构造客户管理整体布局
             mainLayout = UILayoutHelper.BuilLayout<
                CustomerManagerToolBarPart,
                CustomerManagerSearchPart,
                CustomerManagerListPart,
                CustomerManagerBridge>(childLayout);

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
                //this.mainLayout.Dispose();
                this.mainLayout = null;
            }
            if (this.Status != WorkItemStatus.Terminated)
            {
                this.Terminate();
            }
        }
    }
}
