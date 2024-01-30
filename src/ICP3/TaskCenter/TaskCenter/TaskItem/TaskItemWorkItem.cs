using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using ICP.TaskCenter.ServiceInterface;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Windows.Forms;

namespace ICP.TaskCenter.UI.TaskItem
{
    /// <summary>
    /// 订单项目工作区WorkItem
    /// </summary>
    public class TaskItemWorkItem : WorkItem
    {
        /// <summary>
        /// 业务工厂名称
        /// </summary>
        private string businessFactoryName = "businessFactoryName";
        /// <summary>
        /// 
        /// </summary>
        TabBaseBusinessPart tabBaseBusinessPart = null;

        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        IInquireRateClientService InquireRateClientService
        {
            get
            {
                return ServiceClient.GetClientService<IInquireRateClientService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ITaskWorkClientService TaskWorkClientService
        {
            get
            {
                return ServiceClient.GetClientService<ITaskWorkClientService>();
            }
        }

        /// <summary>
        /// 业务工厂
        /// </summary>
        public BusinessPartFactory BusinessPartFactory
        {
            get
            {
                if (RootWorkItem.Items.Contains(businessFactoryName))
                {
                    return RootWorkItem.Items.Get<BusinessPartFactory>(businessFactoryName);
                }
                else
                {
                    BusinessPartFactory businessFactroy = RootWorkItem.Items.AddNew<BusinessPartFactory>(businessFactoryName);
                    return businessFactroy;
                }

            }
        }

        /// <summary>
        /// 重写Dispose处理本地变量
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (tabBaseBusinessPart != null)
                {
                    tabBaseBusinessPart.Dispose();
                    tabBaseBusinessPart = null;
                }
                if (RootWorkItem != null)
                {
                    //this.RootWorkItem.WorkItems.Remove(this);
                    RootWorkItem = null;
                }

            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 根据业务类型加载不同窗体
        /// 现支持类型：业务(默认)、流程、询价、分发文件
        /// </summary>
        /// <param name="deckTaskItem"></param>
        internal void Show(SplitGroupPanel deckTaskItem)
        {
            RootWorkItem.State[Constants.CurrentMessageKey] = null;
            NodeInfo nodeInfo = (NodeInfo)State[ConstMember.CurrentNodeInfo];
            int tokenID = -1;
            string tip = string.Empty;

            switch (nodeInfo.OperationType)
            {
                case OperationType.WorkFlow:
                    #region 流程
                    tip = LocalData.IsEnglish ? "Searching Data..." : "正在查询数据。。。";
                    tokenID = LoadingServce.ShowLoadingForm(deckTaskItem.FindForm(), tip);
                    RootWorkItem.Commands[TaskCenterCommandConstants.CommandDisableTaskCenter].Execute();
                    try
                    {
                        string queryString = !string.IsNullOrEmpty(nodeInfo.AdvanceQueryString) ? nodeInfo.AdvanceQueryString : nodeInfo.BaseCriteria;
                        Control workFlowPart = TaskWorkClientService.TaskGetWorkList(nodeInfo.ViewCode, queryString);

                        deckTaskItem.Controls.Add(workFlowPart);
                        deckTaskItem.Controls.SetChildIndex(workFlowPart, 0);
                        workFlowPart.Dock = DockStyle.Fill;
                        workFlowPart.Visible = workFlowPart.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(deckTaskItem.FindForm(), ex.Message);
                    }
                    finally
                    {
                        LoadingServce.CloseLoadingForm(tokenID);
                        RootWorkItem.Commands[TaskCenterCommandConstants.CommandEnableTaskCenter].Execute();
                    }
                    #endregion
                    break;
                case OperationType.InquireRate:
                    #region 询价
                    tip = LocalData.IsEnglish ? "Searching Data..." : "正在查询数据。。。";
                    tokenID = LoadingServce.ShowLoadingForm(deckTaskItem.FindForm(), tip);
                    RootWorkItem.Commands[TaskCenterCommandConstants.CommandDisableTaskCenter].Execute();
                    try
                    {
                        Control inquireRatePart = InquireRateClientService.GetInquireRateWorkSpace(nodeInfo.ViewCode, nodeInfo.AdvanceQueryString);
                        deckTaskItem.Controls.Clear();
                        if (inquireRatePart != null)
                        {
                            deckTaskItem.Controls.Add(inquireRatePart);
                            deckTaskItem.Controls.SetChildIndex(inquireRatePart, 0);
                            inquireRatePart.Dock = DockStyle.Fill;
                            inquireRatePart.Visible = inquireRatePart.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(deckTaskItem.FindForm(), ex.Message);
                    }
                    finally
                    {
                        LoadingServce.CloseLoadingForm(tokenID);
                        RootWorkItem.Commands[TaskCenterCommandConstants.CommandEnableTaskCenter].Execute();
                    }
                    #endregion
                    break;
                case OperationType.DispatchFile:
                    #region 分发文件
                    tip = LocalData.IsEnglish ? "Searching Data..." : "正在查询数据。。。";
                    tokenID = LoadingServce.ShowLoadingForm(deckTaskItem.FindForm(), tip);
                    RootWorkItem.Commands[TaskCenterCommandConstants.CommandDisableTaskCenter].Execute();
                    try
                    {
                        Control DispatchFilePart = FCMCommonClientService.GetDispatchFileLogPart(nodeInfo.AdvanceQueryString);
                        deckTaskItem.Controls.Clear();
                        if (DispatchFilePart != null)
                        {
                            deckTaskItem.Controls.Add(DispatchFilePart);
                            deckTaskItem.Controls.SetChildIndex(DispatchFilePart, 0);
                            DispatchFilePart.Dock = DockStyle.Fill;
                            DispatchFilePart.Visible = DispatchFilePart.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(deckTaskItem.FindForm(), ex.Message);
                    }
                    finally
                    {
                        LoadingServce.CloseLoadingForm(tokenID);
                        RootWorkItem.Commands[TaskCenterCommandConstants.CommandEnableTaskCenter].Execute();
                    }
                    #endregion
                    break;
                case OperationType.EDI:
                    #region EDI
                    tip = LocalData.IsEnglish ? "Searching Data..." : "正在查询数据。。。";
                    tokenID = LoadingServce.ShowLoadingForm(deckTaskItem.FindForm(), tip);
                    RootWorkItem.Commands[TaskCenterCommandConstants.CommandDisableTaskCenter].Execute();
                    try
                    {
                        Control DispatchFilePart = FCMCommonClientService.GetEdiLogListPart(nodeInfo.Caption);
                        deckTaskItem.Controls.Clear();
                        if (DispatchFilePart != null)
                        {
                            deckTaskItem.Controls.Add(DispatchFilePart);
                            deckTaskItem.Controls.SetChildIndex(DispatchFilePart, 0);
                            DispatchFilePart.Dock = DockStyle.Fill;
                            DispatchFilePart.Visible = DispatchFilePart.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(deckTaskItem.FindForm(), ex.Message);
                    }
                    finally
                    {
                        LoadingServce.CloseLoadingForm(tokenID);
                        RootWorkItem.Commands[TaskCenterCommandConstants.CommandEnableTaskCenter].Execute();
                    }
                    #endregion
                    break;
                default:
                    #region Business
		            tabBaseBusinessPart = BusinessPartFactory.Get<TabBaseBusinessPart>(nodeInfo.ViewCode, nodeInfo);

                    if (deckTaskItem.Contains(tabBaseBusinessPart))
                    {
                        deckTaskItem.Controls.SetChildIndex(tabBaseBusinessPart, 0);
                        tabBaseBusinessPart.Dock = DockStyle.Fill;
                        tabBaseBusinessPart.Visible = tabBaseBusinessPart.Enabled = true;
                    }
                    else
                    {
                        deckTaskItem.Controls.Add(tabBaseBusinessPart);
                        deckTaskItem.Controls.SetChildIndex(tabBaseBusinessPart, 0);
                        tabBaseBusinessPart.Dock = DockStyle.Fill;
                        tabBaseBusinessPart.Visible = tabBaseBusinessPart.Enabled = true;
                    }
                    tabBaseBusinessPart.BindData(nodeInfo); 
	                #endregion
                    break;

            }
            Activate();

        }

        //protected override void OnDeactivated()
        //{
        //    base.OnDeactivated();

        //    if (tabBaseBusinessPart != null)
        //    {
        //        tabBaseBusinessPart.SaveCustomColumnInfo();
        //    }
        //}

    }
}
