using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI;
using System.ComponentModel;

namespace ICP.Common.ServiceInterface.Client
{
    /// <summary>
    /// 报表服务
    /// </summary>
    public interface IReportViewService
    {
        /// <summary>
        /// 显示一个报表预览界面
        /// </summary>
        /// <param name="title">抬头</param>
        /// <param name="workspace">所在的工作空间</param>
        /// <returns></returns>
        IReportViewer ShowReportViewer(string title, IWorkspace workspace);
        /// <summary>
        /// 将报表内容作为邮件或传真附件发送
        /// </summary>
        ///<param name="message">消息；如果需要保存关联操作日志，请将关联属性赋值给UserProperties属性</param>
        /// <param name="reportTemplatePath">报表模板路径</param>
        /// <param name="reportDataSources">报表数据源</param>
        void SendReport(ICP.Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources);
        /// <summary>
        /// 通过邮件中心去发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reportTemplatePath"></param>
        /// <param name="reportDataSources"></param>
        void SendClientReport(Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources);
        /// <summary>
        /// 通过邮件中心发送邮件 显示邮件发送界面
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reportTemplatePath"></param>
        /// <param name="reportDataSources"></param>
        void ShowSendForm(ICP.Message.ServiceInterface.Message message, string reportTemplatePath,Dictionary<string, object> reportDataSources);
        /// <summary>
        /// 通过邮件中心发送邮件 显示邮件发送界面（附件为EXCLE）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reportTemplatePath"></param>
        /// <param name="reportDataSources"></param>
        void ShowExcleSendFrom(ICP.Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources);

    }

    /// <summary>
    ///报表接口
    /// </summary>
    public interface IReportViewer
    {
        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="reportParth">报表路径</param>
        /// <param name="datasources">数据源</param>
        /// <param name="parms">参数</param>
        void BindData(string reportParth, Dictionary<string, object> datasources, List<FastReport.Data.Parameter> parms);
        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="reportPath"></param>
        /// <param name="datasources"></param>
        /// <param name="parms"></param>
        ///<param name="message">消息</param>
        void BindData(string reportPath, Dictionary<string, object> datasources, List<FastReport.Data.Parameter> parms, ICP.Message.ServiceInterface.Message message);

        #region  自定义事件

        /// <summary>
        /// 编辑之前事件,可以设置e.Cancel=true以取消编辑
        /// </summary>
        event CancelEventHandler BeforeEdit;

        /// <summary>
        /// 点击打印事件前
        /// </summary>
        event CancelEventHandler BeforePrint;

        /// <summary>
        /// 点击打印事件后
        /// </summary>
        event EventHandler AfterPrint;

        /// <summary>
        /// 点击输出Email前
        /// </summary>
        event CancelEventHandler BeforeSendEmail;

        /// <summary>
        /// 点击输出Email后
        /// </summary>
        // event EventHandler AfterSendEmail;

        /// <summary>
        /// 点击发送传真前
        /// </summary>
        event CancelEventHandler BeforeSendFax;
        /// <summary>
        /// 发送邮件
        /// </summary>
        event EventHandler SendEmail;
        /// <summary>
        /// 点击发送传真
        /// </summary>
        event EventHandler SendFax;
        /// <summary>
        /// 点击发送传真后
        /// </summary>
        event EventHandler AfterSendFax;

        /// <summary>
        /// 输出为文件后
        /// </summary>
        event EventHandler AfterExport;

        /// <summary>
        /// 点击刷新事件
        /// </summary>
        event EventHandler ClickRefresh;


        /// <summary>
        /// 由报表发出的指令,用于和报表交互
        /// </summary>
        event EventHandler ReportMessage;

        /// <summary>
        /// 点击Email前
        /// </summary>
        event CancelEventHandler BeforeClickEmail;
        /// <summary>
        /// 点击报表存档
        /// </summary>
        event EventHandler Archive;

        #endregion

        /// <summary>
        /// 所在的Workitem
        /// </summary>
        WorkItem Workitem { get; }
        /// <summary>
        /// 业务报表发送邮件或传真时，报表生成文件的绝对路径
        /// </summary>
        string ExportFilePath { get; }
    }

}
