#region Comment

/*
 * 
 * FileName:    FormUpgradeCloud.cs
 * CreatedOn:   2015/4/16 18:00:11
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.OA.UI
{
    public partial class FormUpgradeCloud : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICP.OA.ServiceInterface.Client.IDocumentClientService DocumentClientService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.OA.ServiceInterface.Client.IDocumentClientService>();
            }
        }

        #endregion

        List<DocumentFileInfo> listDocumentInfos = null;
        public FormUpgradeCloud()
        {
            InitializeComponent();
            OperateEvent(true);
            Disposed += (sender, arg) =>
            {
                if (listDocumentInfos != null)
                {
                    listDocumentInfos.Clear();
                    listDocumentInfos = null;
                }
                OperateEvent(false);
            };
        }

        private void FormUpgradeCloud_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                barUpgradeProgress.EditValue = 0;
                listDocumentInfos = DocumentClientService.GetAllDocumentFileInfos();
                if (listDocumentInfos == null || listDocumentInfos.Count <= 0)
                {
                    DisplayUpgradeStateText("Upgrade data does not exist");
                }
                else
                {
                    barUpgradeProgress.Properties.Maximum = listDocumentInfos.Count;
                    string strState = string.Empty;
                    for (int index = 0; index < listDocumentInfos.Count; index++)
                    {
                        DocumentFileInfo item = listDocumentInfos[index];
                        strState = string.Format("Item {0} :{1} Upgrade {2}", index, item.FileName,DocumentClientService.SingleUpgradeCloud(item));
                        DisplayUpgradeStateText(strState);
                        barUpgradeProgress.EditValue = index;
                    }
                }
                DisplayUpgradeStateText("Complete");
            }
            catch (Exception ex)
            {
                DisplayUpgradeStateText(string.Format("Exception:{0}", ex.Message));
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 事件操作
        /// </summary>
        /// <param name="isAdd">是否添加</param>
        void OperateEvent(bool isAdd)
        {
            if (isAdd)
            {
                Load += FormUpgradeCloud_Load;                      //窗体加载
                btnStart.Click += btnStart_Click;
                btnClose.Click += btnClose_Click;
            }
            else
            {
                Load -= FormUpgradeCloud_Load;                      //窗体加载
                btnStart.Click -= btnStart_Click;
                btnClose.Click -= btnClose_Click;
            }
        }

        /// <summary>
        /// 在主界面的ListBox里面显示，同时在托盘区弹出气球
        /// </summary>
        /// <param name="text"></param>
        void DisplayUpgradeStateText(string text)
        {
            try
            {
                txtUpgradeState.Invoke(new EventHandler(delegate
                {
                    txtUpgradeState.AppendText(text);
                }));

            }
            catch (Exception ex)
            {

            }
        }
    }
}
