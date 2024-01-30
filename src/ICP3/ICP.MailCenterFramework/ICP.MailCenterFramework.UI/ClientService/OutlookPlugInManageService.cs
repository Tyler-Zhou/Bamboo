#region Comment

/*
 * 
 * FileName:    OutlookPlugInManageService.cs
 * CreatedOn:   2014/7/11 9:24:42
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->OutLook插件管理服务
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
namespace ICP.MailCenterFramework.UI
{
    public class OutlookPlugInManageService
    {
        #region 成员变量
        /// <summary>
        /// Type Missing
        /// </summary>
        private readonly object missing = System.Reflection.Missing.Value;

        /// <summary>
        /// 答复全部(含附件)
        /// </summary>
        public static CommandBarButton btnReplyToAllContainsAttachment = null;

        /// <summary>
        /// 邮件归档
        /// </summary>
        public static CommandBarButton btnMailArchiving = null;
        /// <summary>
        /// 异步通讯录
        /// </summary>
        public static CommandBarButton btnSyncAddress = null;
        /// <summary>
        /// 批量关联
        /// </summary>
        public static CommandBarButton btnAssociatedBatch = null;

        /// <summary>
        /// 当前Inspectors 
        /// </summary>
        public static Inspectors CurrentInspectors = null;
        /// <summary>
        /// 当前 Explorer
        /// </summary>
        public static Explorer CurrentExplorer;

        /// <summary>
        /// 显示ID
        /// </summary>
        public static CommandBarButton btnShowID = null;

        /// <summary>
        /// 显示ID
        /// </summary>
        public static CommandBarButton btnAssociatedStatistics = null;
        #endregion

        #region 公有方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void InitControl()
        {
            try
            {
                if (OutlookUtility.CurrentApplication != null)
                {
                    CurrentInspectors = OutlookUtility.CurrentApplication.Inspectors;
                    if (CurrentInspectors != null)
                    {
                        //新打开窗体：查看邮件
                        CurrentInspectors.NewInspector += Inspectors_NewInspector;
                    }
                    CurrentExplorer = OutlookUtility.CurrentApplication.ActiveExplorer();

                    if (CurrentExplorer != null)
                    {
                        var commandBars = CurrentExplorer.CommandBars;

                        #region 0. Menu Bar(菜单栏)
                        //Menu Bar:37
                        //2.主菜单栏
                        var menuBar = commandBars["Menu Bar"];
                        menuBar.Reset();
                        if (menuBar != null)
                        {
                            //工具栏 Tool:30007
                            var toolBar = menuBar.FindControl(Id: 30007) as CommandBarPopup;
                            if (toolBar != null)
                            {
                                //2.0邮件归档
                                btnMailArchiving = (CommandBarButton)
                                    toolBar.Controls.Add(MsoControlType.msoControlButton
                                        , missing, missing, 4, true);
                                btnMailArchiving.Style = MsoButtonStyle.msoButtonIconAndCaption;
                                btnMailArchiving.Caption = OutlookUtility.IsEnglish ? "Mail Archiving" : "邮件归档";
                                btnMailArchiving.Click += MailArchiving_Click;
                                btnMailArchiving.FaceId = 355;
                                btnMailArchiving.Tag = "Mail Archiving";
                                //2.1 异步通讯录
                                btnSyncAddress = (CommandBarButton)
                                    toolBar.Controls.Add(MsoControlType.msoControlButton
                                        , missing, missing, 4, true);
                                btnSyncAddress.Style = MsoButtonStyle.msoButtonIconAndCaption;
                                btnSyncAddress.Caption = OutlookUtility.IsEnglish ? "Sync Contact From ICP" : "同步ICP通讯簿";
                                btnSyncAddress.Click += SyncAddress_Click;
                                btnSyncAddress.FaceId = 355;
                                btnSyncAddress.Tag = "Sync Contact";
                                //2.2 批量关联
                                btnAssociatedBatch = (CommandBarButton)
                                    toolBar.Controls.Add(MsoControlType.msoControlButton
                                        , missing, missing, 4, true);
                                btnAssociatedBatch.Style = MsoButtonStyle.msoButtonIconAndCaption;
                                btnAssociatedBatch.Caption = OutlookUtility.IsEnglish ? "Bulk Mail Association" : "批量关联邮件";
                                btnAssociatedBatch.Click += AssociatedBatch_Click;
                                btnAssociatedBatch.FaceId = 355;
                                btnAssociatedBatch.Tag = "Bulk Mail Association";

                                btnAssociatedStatistics = (CommandBarButton)
                                    toolBar.Controls.Add(MsoControlType.msoControlButton
                                        , missing, missing, 4, true);
                                btnAssociatedStatistics.Style = MsoButtonStyle.msoButtonIconAndCaption;
                                btnAssociatedStatistics.Caption = OutlookUtility.IsEnglish ? "Associated Statistics" : "关联统计";
                                btnAssociatedStatistics.Click += AssociatedStatistics_Click;
                                btnAssociatedStatistics.FaceId = 355;
                                btnAssociatedStatistics.Tag = "Associated Statistics";

                                

                                ////2.3 显示ID
                                btnShowID = (CommandBarButton)
                                        toolBar.Controls.Add(MsoControlType.msoControlButton
                                            , missing, missing, 9, true);
                                btnShowID.Style = MsoButtonStyle.msoButtonIconAndCaption;
                                btnShowID.Caption = OutlookUtility.IsEnglish ? "Show Email Property" : "显示邮件属性";
                                btnShowID.Click += btnShowID_Click;
                            }
                        }
                        #endregion

                        #region 1. Standard(标准工具栏)
                        //3.标准控件菜单栏
                        //Standard:9
                        var menuStandard = commandBars["Standard"];
                        menuStandard.Reset();
                        if (menuStandard != null)
                        {
                            //3.1 答复全部(含附件)
                            btnReplyToAllContainsAttachment = (CommandBarButton)
                                    menuStandard.Controls.Add(MsoControlType.msoControlButton
                                        , missing, missing, 9, true);
                            btnReplyToAllContainsAttachment.Style = MsoButtonStyle.msoButtonIconAndCaption;
                            btnReplyToAllContainsAttachment.Caption = OutlookUtility.IsEnglish ? "Reply To All(Attachment)" : "全部答复(含附件)";
                            btnReplyToAllContainsAttachment.Click += ReplyToAllContainsAttachment_Click;
                            btnReplyToAllContainsAttachment.FaceId = 355;
                            btnReplyToAllContainsAttachment.Tag = "Reply To All(Attachment)";
                        }
                        #endregion

                        //添加选择项改变事件
                        CurrentExplorer.SelectionChange += Explorer_SelectionChange;
                        //邮件菜单
                        OutlookUtility.CurrentApplication.ItemContextMenuDisplay += CurrentApplication_ItemContextMenuDisplay;
                    } 
                }
            }
            catch(System.Exception ex)
            {
                ToolUtility.WriteLog("Init Control",ex);
            }

        }
        /// <summary>
        /// 销毁控件
        /// </summary>
        public static void DisposedControl()
        {
            btnReplyToAllContainsAttachment = null;
            btnMailArchiving = null;
            btnSyncAddress = null;
            btnAssociatedBatch = null;
            CurrentInspectors = null;
            CurrentExplorer = null;
        }
        #endregion

        #region 私有方法

        #region Inspector
        /// <summary>
        /// New Inspector:新弹出窗体
        /// </summary>
        /// <param name="paramInspector"></param>
        void Inspectors_NewInspector(Inspector paramInspector)
        {
            try
            {
                if (paramInspector.CurrentItem is _MailItem)
                {
                    _MailItem CurrrentItem = paramInspector.CurrentItem as _MailItem;
                    //新邮件、答复、答复全部
                    if (CurrrentItem.UnRead)
                    {
                        OutlookUtility.SetMessageID(CurrrentItem);
                    }
                }

            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("New Inspector",ex);
            }
        }
        #endregion

        #region Explorer
        /// <summary>
        /// 选择项改变事件：添加判断是否选择邮件以启用、禁用'全部答复(含附件)'按钮
        /// </summary>
        void Explorer_SelectionChange()
        {
            try
            {
                if (btnReplyToAllContainsAttachment != null)
                {
                    Explorer mExplorer = OutlookUtility.CurrentApplication.ActiveExplorer();
                    if (mExplorer != null && mExplorer.Selection != null && mExplorer.Selection.Count == 1)
                        btnReplyToAllContainsAttachment.Enabled = true;
                    else
                        btnReplyToAllContainsAttachment.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Explorer Selection Change", ex);
            }
        }
        #endregion

        #region CommandBarButtonEvents
        /// <summary>
        ///回复全部(含附件) 
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        void ReplyToAllContainsAttachment_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                //1.判断是否选中邮件
                object tempObj = OutlookUtility.CurrentApplication.ActiveExplorer().Selection[1];
                if (tempObj is _MailItem)
                {
                    _MailItem item = tempObj as _MailItem;
                    OutlookOperateService olOperateService = new OutlookOperateService();
                    //2.打开邮件发送窗口，设置MessageID
                    olOperateService.ReplyEmailToAllContainsAttachment(item);
                }
                
            }
            catch(System.Exception ex)
            {
                ToolUtility.WriteLog("Reply To All Contains Attachment", ex);
            }
        }

        /// <summary>
        /// 邮件归档
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        void MailArchiving_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                OutlookOperateService olOperateService = new OutlookOperateService();
                olOperateService.EmailArchiving();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Mail Archiving", ex);
            }
        }

        /// <summary>
        /// 从ICP异步通讯录
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        void SyncAddress_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                OutlookOperateService olOperateService = new OutlookOperateService();
                olOperateService.SynchronAddressBook();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Sync AddressBook",ex);
            }
        }

        /// <summary>
        /// 批量关联
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        void AssociatedBatch_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                OutlookOperateService olOperateService = new OutlookOperateService();
                olOperateService.AssociatedBatch();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("AssociatedBatch", ex);
            }
        }

        /// <summary>
        /// 关联统计
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        void AssociatedStatistics_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                OutlookOperateService olOperateService = new OutlookOperateService();
                olOperateService.AssociatedStatistics();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("AssociatedBatch", ex);
            }
        }
        #endregion

        #region ContextMenu
        /// <summary>
        /// 右键菜单显示时添加按钮'全部答复(含附件)'
        /// </summary>
        /// <param name="CommandBar"></param>
        /// <param name="Selection"></param>
        void CurrentApplication_ItemContextMenuDisplay(CommandBar CommandBar, Selection Selection)
        {
            try
            {
                if (Selection != null && Selection.Count == 1)
                {
                    if (Selection[1] is MailItem)
                    {
                        var btnContextMenuReplyToAllContainsAttachment = (CommandBarButton)
                           CommandBar.Controls.Add(MsoControlType.msoControlButton
                               , missing, missing, 5, true);
                        btnContextMenuReplyToAllContainsAttachment.Style = MsoButtonStyle.msoButtonIconAndCaption;
                        btnContextMenuReplyToAllContainsAttachment.Caption = OutlookUtility.IsEnglish ? "Reply To All(Attachment)" : "全部答复(含附件)";
                        btnContextMenuReplyToAllContainsAttachment.FaceId = 355;
                        btnContextMenuReplyToAllContainsAttachment.Tag = "Reply To All(Attachment)";
                    }
                }
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Item Context Menu Display", ex);
            }
        }
        #endregion

        #endregion

        #region Comment Code
        void btnShowID_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                //1.判断是否选中邮件
                object tempObj = OutlookUtility.CurrentApplication.ActiveExplorer().Selection[1];
                if (tempObj is _MailItem)
                {
                    OutlookOperateService olOperateService = new OutlookOperateService();
                    olOperateService.ShowMessageBox();
                }
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Show ID",ex);
            }
        }
        #endregion
    }
}
