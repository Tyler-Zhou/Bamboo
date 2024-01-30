#region Comment

/*
 * 
 * FileName:    InquireOceanRatesEmailPart.cs
 * CreatedOn:   2014-06-26 09:14
 * CreatedBy:   Taylor Zhou
 * 
 * 
 * Description：
 *      ->海运询价邮件沟通记录
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.Data;
using System.Linq;
using ICP.Business.Common.UI.Communication;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.InquireRates
{
    public partial class InquireOceanRatesEmailPart : BaseEditPart, IInquierRateDataBind
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// Root Work Item
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        /// <summary>
        /// 发送邮件服务
        /// </summary>
        [ServiceDependency]
        IInquireRateEmailService inquireRateEmailService { get; set; }
        #endregion

        #region 构造函数
        public InquireOceanRatesEmailPart(IInquireRateEmailService inquireRateEmailService)
        {
            this.inquireRateEmailService = inquireRateEmailService;
            InitializeComponent();
            EmailPart.Presenter = new CommunicationHistoryListPresenter();
            Disposed += delegate
            {
                EmailPart.Presenter = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        #endregion

        #region 成员变量
        /// <summary>
        /// 当前询价单对象
        /// </summary>
        public ClientBaseInquireRate CurrentInquierRate { get; set; }
        #endregion

        #region 窗体事件

        #region EventSubscription

        /// <summary>
        /// 发送邮件到询价人
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_SendEmailToInquireBy)]
        public void Command_SendEmailToInquireBy(object sender, DataEventArgs<List<InquierOceanRate>> e)
        {
            try
            {
                if (e.Data == null)
                    return;
                List<BaseInquireRate> transList = e.Data.Select(item => item as BaseInquireRate).ToList();
                //发送询价结果
                inquireRateEmailService.SendEmailToInquireBy(transList);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 发送邮件到询价人
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_SendEmailToRespondBy)]
        public void Command_SendEmailToRespondBy(object sender, DataEventArgs<ClientInquierOceanRate> e)
        {
            try
            {
                BaseInquireRate inquierObj = InquireRatesHelper.TransformC2S(e.Data, true);
                if (inquierObj == null)
                    return;
                //发送询价结果
                inquireRateEmailService.SendEmailToRespondBy(inquierObj);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 发送邮件到询价人
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_MailBookingConfirm)]
        public void Command_MailBookingConfirm(object sender, DataEventArgs<ClientInquierOceanRate> e)
        {
            try
            {
                DataRow dr = RootWorkItem.State["BaseCurrentRow"] as DataRow;
                if (dr == null) return;
                InquierOceanRate inquierObj = InquireRatesHelper.TransformC2S(e.Data, true);
                if (inquierObj == null)
                    return;
                BaseInquireRate baseInquireRate = InquireRatesHelper.BuildInquireOceanContent(inquierObj);
                Guid oceanBookingID = new Guid(dr["OceanBookingID"].ToString());
                //发送询价结果
                inquireRateEmailService.MailBookingConfirm(oceanBookingID, baseInquireRate);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 刷新邮件面板
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_RefreshEmailPart)]
        public void Command_RefreshEmailPart(object sender, DataEventArgs<object> e)
        {
            try
            {
                if (e.Data == null)
                    return;
                SetDataSource(e.Data);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion

        #endregion

        #region 方法定义
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据</param>
        public void DataSourceBind(object data)
        {
            ClientBaseInquireRate obj = data as ClientBaseInquireRate;
            BusinessOperationContext contex = new BusinessOperationContext();
            contex.OperationID = obj == null ? Guid.Empty : obj.ID;
            CurrentInquierRate = obj;
            EmailPart.DataBind(contex);
            Enabled = data != null;
        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="data">询价对象</param>
        public void SetDataSource(object data)
        {
            
        }
        #endregion
    }
}
