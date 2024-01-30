using DevExpress.XtraBars;
using ICP.FileSystem.ServiceInterface;
using ICP.FileSystem.UI;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FAM.UI.BankReceiptList
{
    public class CustomerDocumentList : BaseDocumentList
    {
        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        private DocumentListPresenter presenter;
        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        public DocumentListPresenter Presenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter = value;
            }
        }

        #region override
        /// <summary>
        /// 初始化上传菜单
        /// </summary>
        /// <returns></returns>
        protected override void InitUploadSubItems()
        {
            barItemUpload.ItemLinks.Clear();
            BarButtonItem btnItem = new BarButtonItem();
            btnItem.Tag = EnumHelper.GetDescription<DocumentType>(DocumentType.BR, LocalData.IsEnglish);
            btnItem.Caption = EnumHelper.GetDescription<DocumentType>(DocumentType.BR, LocalData.IsEnglish);
            btnItem.ItemClick += OnUploadItemClick;
            btnItem.ResetDropDownSuperTip();
            barItemUpload.AddItem(btnItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="business"></param>
        protected override void BindData(BusinessOperationContext business)
        {
            if (Presenter == null)
            {
                Presenter = WorkItem.Items.AddNew<DocumentListPresenter>();
            }
            Presenter.BusinessContext = business;
            Presenter.PartDocumentList = this;
            Presenter.BindData(business);
            SetResetBindings(false);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentOpen()
        {
            Presenter.Open(CurrentDocument.Id);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentOpenWith()
        {
            Presenter.OpenWith(CurrentDocument.Id);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentPreview()
        {
            Presenter.Preview(CurrentDocument.Id);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentDelete()
        {
            string message = LocalData.IsEnglish ? "Are you sure you want to delete the selected files?" : "你确定要删除所选择的文件吗?";
            DialogResult result = MessageBoxService.ShowQuestion(message, "Tips");
            if (result == DialogResult.OK)
            {
                int rowHandler = GridViewList.FocusedRowHandle;
                Guid id = CurrentDocument.Id;
                DateTime? updateDate = CurrentDocument.UpdateDate;
                GridViewList.DeleteRow(rowHandler);
                Presenter.Delete(new List<Guid> { id }, new List<DateTime?> { updateDate });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentDownload()
        {
            Presenter.Download(CurrentDocument);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="caption"></param>
        protected override void DocumentUpload(string[] filePaths, string caption)
        {
            Presenter.Upload(filePaths,caption);
            DocumentListRefresh();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentReupload()
        {
            Presenter.Reupload(new List<Guid> { CurrentDocument.Id });
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void DocumentListRefresh()
        {
            Presenter.BindData(Presenter.BusinessContext);
            SetResetBindings(false);
        }
        #endregion

        
    }
}
