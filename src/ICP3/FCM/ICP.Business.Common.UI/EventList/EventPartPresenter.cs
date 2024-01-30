using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.EventList
{
    public sealed class EventPartPresenter:IDisposable
    {

        #region 服务
        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }
        #endregion

        #region 属性
        public BindingSource BSource { get; set; }
        public bool IsNew { get; set; }
        #endregion

        #region 方法

        /// <summary>
        /// 打开一个事件列表编辑界面
        /// </summary>
        /// <param name="memoParam"></param>
        /// <param name="workItem"></param>
        /// <param name="bindSourece"></param>
        /// <param name="isNew"></param>
        public void Opent(BusinessOperationContext context, WorkItem workItem, BindingSource bindingSource, bool isNew)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ICP.Framework.ClientComponents.Forms.PopupWindow form = null;
                EventEditPart frmEventEditPart = workItem.SmartParts.AddNew<EventEditPart>();
                form = new ICP.Framework.ClientComponents.Forms.PopupWindow();
                frmEventEditPart.BusinessEventObjects = context;
                frmEventEditPart.isNew = isNew;
                frmEventEditPart.bindingSource = bindingSource;
                IsNew = isNew;
                if (!isNew && bindingSource.Current != null)
                {
                    frmEventEditPart.BusinessEventDataSource = bindingSource.Current as EventObjects;
                }
                //事件 方法
                BSource = bindingSource;
                frmEventEditPart.InsertEventObjectEvent += InsertEventObject;
                form.MaximizeBox = form.MinimizeBox = false;
                form.Width = 580;
                form.Height = 400;
                form.ShowInTaskbar = true;
                form.KeyPreview = true;
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                if (isNew)
                {
                    form.Text = LocalData.IsEnglish ? "New Event" : "新增事件";
                }
                else 
                {
                    form.Text = LocalData.IsEnglish ? "Edit Event" : "编辑事件";
                }
                
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmEventEditPart.Dock = DockStyle.Fill;
                form.Controls.Add(frmEventEditPart);
                form.ShowDialog();
            }
        }


        /// <summary>
        /// 向事件列表中插入保存的对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertEventObject(object sender, CommonEventArgs<EventObjects> args)
        {
            if (args.Data != null && BSource != null)
            {
                if (!IsNew)
                {
                    if (BSource.Current != null)
                    {
                        BSource.RemoveCurrent();
                    }
                }

                EventObjects newContact = args.Data;
                BSource.Insert(0, newContact);
                BSource.MoveFirst();
                BSource.ResetBindings(false);
            }

        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.BSource != null)
            {
                this.BSource.DataSource = null;
                this.BSource = null;
            }
            this.fcmCommonService = null;
        }

        #endregion
    }
}
