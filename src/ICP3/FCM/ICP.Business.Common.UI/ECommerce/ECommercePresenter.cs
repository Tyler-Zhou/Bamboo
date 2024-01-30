#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/20 星期五 17:35:27
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Threading;
using System.Windows.Forms;
using ICP.Business.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.ECommerce
{
    /// <summary>
    /// 电商代理类
    /// </summary>
    public sealed class ECommercePresenter : IDisposable
    {
        #region Member
        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BindingSource BSource { get; set; }
        /// <summary>
        /// 电商列表面板
        /// </summary>
        public ECommerceListPart ViewPart
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定委托
        /// </summary>
        /// <param name="context"></param>
        delegate void BindDataDelegate(BusinessOperationContext context);
        #endregion

        #region Delegate & Window Event

        #endregion

        #region Implemented Delegate & Method
        /// <summary>
        /// 根据业务操作上下文绑定数据
        /// 获取文档列表
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        public void BindData(BusinessOperationContext context)
        {
            if (context == null)
            {
                return;
            }
            WaitCallback callback = (data) =>
            {
                try
                {
                    if (ViewPart.IsHandleCreated)
                    {
                        BusinessOperationContext parameter = data as BusinessOperationContext;
                        var bindDelegate = new BindDataDelegate(InnerBindData);
                        ViewPart.BeginInvoke(bindDelegate, parameter);
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(ViewPart, ex);
                }
            };
            ThreadPool.QueueUserWorkItem(callback, context);
        }

        /// <summary>
        /// 关联业务
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="context"></param>
        public void AssociationBusiness(WorkItem workItem, BusinessOperationContext context)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Framework.ClientComponents.Forms.PopupWindow form = null;
                ECommerceEditPart selectPart = workItem.SmartParts.AddNew<ECommerceEditPart>();
                form = new Framework.ClientComponents.Forms.PopupWindow();
                form.MaximizeBox = form.MinimizeBox = false;
                form.Width = 900;
                form.Height = 400;
                form.ShowInTaskbar = true;
                form.KeyPreview = true;
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.Text = LocalData.IsEnglish ? "Association Business" : "关联业务";
                form.StartPosition = FormStartPosition.CenterScreen;
                selectPart.OperationID = context.OperationID;
                selectPart.CompanyID = (Guid)context["CompanyID"];
                selectPart.Dock = DockStyle.Fill;
                form.Controls.Add(selectPart);
                form.ShowDialog();
            }
        }
        /// <summary>
        /// 添加业务
        /// </summary>
        public void AddBusiness()
        {
            ICPCommonOperationService.AddBusiness(null, OperationType.Other, FormType.ECommerceOrder);
        }

        /// <summary>
        /// 编辑业务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyID"></param>
        /// <param name="no"></param>
        public void EdiBusiness(Guid id,Guid companyID,string no)
        {
            var editPartShowCriteria = new EditPartShowCriteria
            {
                OperationID = id,
                OperationNo = no,
                CompanyID = companyID,
            };
            ICPCommonOperationService.EditBusiness(editPartShowCriteria, null, OperationType.Other, FormType.ECommerceOrder);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        void InnerBindData(BusinessOperationContext context)
        {
            var qcParameter = new QueryCriteria4ECommerce
            {
                OperationID = context.OperationID,
            };
            ViewPart.ListDataSource = FCMCommonService.GetAssociatedECommerceList(qcParameter);
        }

        #endregion

        #region IDisposable
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (BSource != null)
            {
                BSource.DataSource = null;
                BSource = null;
            }
        }

        #endregion
    }
}
