using System;
using System.Collections.Generic;
using System.Linq;
using FastReport.Export.OoXML;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using System.Windows.Forms;
using ICP.Message.ServiceInterface;

namespace ICP.Common.UI.ReportView
{
    /// <summary>
    /// 报表服务
    /// </summary>
    public class ReportViewService : IReportViewService
    {
        #region 属性及字段定义
        [ServiceDependency]
        public WorkItem workitem { get; set; }


        public IMessageService MessageService
        {
            get
            {
                return ServiceClient.GetService<IMessageService>();
            }
        }

        public IClientMessageService ClientMessageSerivice
        {
            get { return ServiceClient.GetClientService<IClientMessageService>(); }
        }

        private FastReport.Report report;
        private FastReport.Export.Pdf.PDFExport export;
        private FastReport.Export.OoXML.Excel2007Export excel;

        private object synObj = new object();
        #endregion
        #region IReportViewService Members

        public IReportViewer ShowReportViewer(string title, IWorkspace workspace)
        {
            WorkItem tempWorkitem = workitem.WorkItems.AddNew<WorkItem>();
            FastReportViewerPart viewer = tempWorkitem.SmartParts.AddNew<FastReportViewerPart>();
            viewer.Disposed += delegate(object s, EventArgs e)
            {

                tempWorkitem.Dispose();
            };

            workspace.Show(viewer, new WindowSmartPartInfo { Title = title == string.Empty ? "Print" : title });
            return viewer;
        }

        public void SendReport(ICP.Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources)
        {
            Send(message,reportTemplatePath,reportDataSources,false);
        }

        public void SendClientReport(ICP.Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources)
        {
            Send(message, reportTemplatePath, reportDataSources, true);
        }

        private void Send(ICP.Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources, bool isClientSend)
        {
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Sending email..." : "正在发送邮件...");

            //WaitCallback callback = (obj) =>
            //{
            try
            {
                ValidateParameter(message, reportTemplatePath);
                InitReportObject(reportTemplatePath, reportDataSources);
                InitExport();
                AttachmentContent attachment = GetReportAttachment(reportTemplatePath,isClientSend);
                message.Attachments.Add(attachment);
                if (isClientSend)
                {
                    ClientMessageSerivice.Send(message);
                }
                else
                {
                    MessageService.Send(message);
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.report != null)
                {
                    this.report.Dispose();
                }
                if (this.export != null)
                {
                    this.export.Dispose();
                }
            }

            //};
            try
            {
                //ThreadPool.QueueUserWorkItem(callback);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
        }
        /// <summary>
        /// 通过邮件中心发送邮件 显示邮件发送界面
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reportTemplatePath"></param>
        /// <param name="reportDataSources"></param>
        public void ShowSendForm(ICP.Message.ServiceInterface.Message message, string reportTemplatePath, Dictionary<string, object> reportDataSources)
        {
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Sending email..." : "正在发送邮件...");
            try
            {
                ValidateParameter(message, reportTemplatePath);
                InitReportObject(reportTemplatePath, reportDataSources);
                InitExport();
                AttachmentContent attachment = GetReportAttachment(reportTemplatePath,true);
                message.Attachments.Add(attachment);
                ClientMessageSerivice.ShowSendForm(message);
            }
            catch (Exception ex)
            {

                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.report != null)
                {
                    this.report.Dispose();
                }
                if (this.excel != null)
                {
                    this.excel.Dispose();
                }
            }
        }
        /// <summary>
        /// 通过邮件中心发送邮件 显示邮件发送界面（附件为EXCLE）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reportTemplatePath"></param>
        /// <param name="reportDataSources"></param>
        public void ShowExcleSendFrom(ICP.Message.ServiceInterface.Message message, string reportTemplatePath,
                                      Dictionary<string, object> reportDataSources)
        {
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Sending email..." : "正在发送邮件...");
            try
            {
                ValidateParameter(message, reportTemplatePath);
                InitReportObject(reportTemplatePath, reportDataSources);
                InitExcleExport();
                AttachmentContent attachment = GetExcelAttachment(reportTemplatePath, true);
                message.Attachments.Add(attachment);
                ClientMessageSerivice.ShowSendForm(message);
            }
            catch (Exception ex)
            {

                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.report != null)
                {
                    this.report.Dispose();
                }
                if (this.export != null)
                {
                    this.export.Dispose();
                }
            }
        }

        #endregion
        #region 辅助方法
        /// <summary>
        /// 获取报表内容的文档表示
        /// </summary>
        /// <returns></returns>
        private AttachmentContent GetReportAttachment(string reportTemplatePath,bool isClientSend)
        {
            AttachmentContent attachment = new AttachmentContent();
            using (MemoryStream stream = new MemoryStream())
            {
                export.Export(report, stream);
                string attName = attachment.DisplayName = string.IsNullOrEmpty(report.Name) ? "attachment.pdf" : report.Name;
                attachment.Name = attName;
                if (isClientSend) //客户端发送邮件附件只需要本地映射路径
                {
                    string saveDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    Directory.CreateDirectory(saveDirectory);
                    attachment.ClientPath = Path.Combine(saveDirectory, attName);
                    //保存成PDF
                    SaveAsFile(attachment.ClientPath, stream.ToArray());
                }
                else      //服务端发送邮件附件只需要文件流       
                    attachment.Content = stream.ToArray();              
                
                return attachment;
            }
        }


        private AttachmentContent GetExcelAttachment(string reportTemplatePath, bool isClientSend)
        {
            AttachmentContent attachment = new AttachmentContent();
            using (MemoryStream stream = new MemoryStream())
            {
                excel.Export(report, stream);
                string attName = attachment.DisplayName = string.IsNullOrEmpty(report.Name) ? "attachment.xlsx" : report.Name;
                attachment.Name = attName;
                if (isClientSend) //客户端发送邮件附件只需要本地映射路径
                {
                    string saveDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    Directory.CreateDirectory(saveDirectory);
                    attachment.ClientPath = Path.Combine(saveDirectory, attName);
                    //保存成PDF
                    SaveAsFile(attachment.ClientPath, stream.ToArray());
                }
                else      //服务端发送邮件附件只需要文件流       
                    attachment.Content = stream.ToArray();

                return attachment;
            }
        }

        /// <summary>
        /// 保存在本地
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public void SaveAsFile(string filePath, Byte[] content)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(content, 0, content.Length);
            }
        }

        private void InitExport()
        {
            export = new FastReport.Export.Pdf.PDFExport();
            export.ShowProgress = false;
            export.AllowAnnotate = true;
            export.AllowCopy = true;
            export.AllowModify = true;
            export.AllowOpenAfter = false;

            export.AllowPrint = true;
            export.Author = "sunny";
            export.Background = true;
            export.PageRange = FastReport.PageRange.All;
            export.HideMenubar = export.HideToolbar = export.HideWindowUI = true;



        }

        private void InitExcleExport()
        {
            excel=new Excel2007Export
                {
                    ShowProgress = false,
                    AllowOpenAfter = false,
                    PageRange = FastReport.PageRange.All
                };
        }


        private void InitReportObject(string templatePath, Dictionary<string, object> dataSources)
        {
            this.report = new FastReport.Report();

            this.report.BeginInit();
            this.report.ReportResourceString = "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Report ReportInfo.Created=\"09/27/2010 1" +
               "4:12:23\" ReportInfo.Modified=\"06/15/2012 12:08:21\" ReportInfo.CreatorVersion=\"1." +
               "2.47.0\">\r\n  <Dictionary/>\r\n</Report>\r\n";
            this.report.EndInit();
            report.Load(templatePath);

            if (dataSources != null && dataSources.Count > 0)
            {
                foreach (var item in dataSources)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = item.Value;
                    this.report.RegisterData(bs, item.Key);
                }
            }
            this.report.Refresh();
            this.report.Prepare();

        }


        private void ValidateParameter(ICP.Message.ServiceInterface.Message message, string reportTemplatePath)
        {
            List<string> illegalParameterNames = new List<string>();
            bool templateRootPath = IsReportTemplateRootPath(reportTemplatePath);

            bool reportTemplateExists = string.IsNullOrEmpty(reportTemplatePath) || (templateRootPath ? File.Exists(reportTemplatePath) : File.Exists(Path.Combine(Application.StartupPath, reportTemplatePath)));
            if (!reportTemplateExists)
            {
                illegalParameterNames.Add("未找到报表模板!");
            }
            if (illegalParameterNames.Count > 0)
            {
                throw new NullReferenceException(illegalParameterNames.Select(o => o).Aggregate((a, b) => a + "," + b));
            }
        }

        private bool IsReportTemplateRootPath(string path)
        {

            return Path.IsPathRooted(path);
        }
        #endregion


    }
}
