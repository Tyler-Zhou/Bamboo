using ICP.Business.Common.UI.Document;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FCM.Common.UI.Common.Parts
{
    public partial class UCDocumentHistoryPartNew : BaseListPart
    {
           [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 本地文档记录
        /// </summary>
        public List<DocumentInfo> BeforeDocumentSource { get; set; }
        /// <summary>
        /// 分文档记录
        /// </summary>
        public List<DocumentInfo> AfterDocumentSource
        {
            get;
            set;
        }

        public UCDocumentHistoryPartNew()
        {
            InitializeComponent();
        }

        private DocumentListPresenter presenter;
        /// <summary>
        /// 当前文档列表呈现类
        /// </summary>
        public DocumentListPresenter CurrentDocumentListPresenter
        {
            get
            {
                if (presenter == null)
                {
                    presenter = this.Workitem.Items.AddNew<DocumentListPresenter>();
                }
                return presenter;
            }

        }
        private DocumentListPresenter historyPresenter;
        /// <summary>
        /// 历史分发文档列表呈现类
        /// </summary>
        public DocumentListPresenter HistoryDocumentListPresenter
        {
            get
            {
                if (historyPresenter == null)
                {
                    historyPresenter = this.Workitem.Items.AddNew<DocumentListPresenter>();
                }
                return historyPresenter;
            }

        }


        private UCDocumentList DocumentListHistory { get; set; }
        private UCDocumentList DocumentListCurrent { get; set; }


        protected override void OnLoad(EventArgs e)
        {

            if (!DesignMode)
            {
                DocumentListHistory = Workitem.Items.AddNew<UCDocumentList>();
                DocumentListHistory.ManageBusinessBarDisplay(false);
                DocumentListHistory.Dock = DockStyle.Fill;
                DocumentListHistory.ShowControlCheckState = false;
                pnlControlHistory.Controls.Add(DocumentListHistory);

                //修改代码，解决同时加载多个UCDocumentList时只显示一个。joe 2013-07-24
                DocumentListCurrent = Workitem.Items.AddNew<UCDocumentList>();
                DocumentListCurrent.Dock = DockStyle.Fill;
                DocumentListCurrent.ManageBusinessBarDisplay(false);
                DocumentListCurrent.ShowControlCheckState = false;// ShowCheckControl;
                DocumentListCurrent.Visible = true;
                pnlControlCurrent.Controls.Add(DocumentListCurrent);



                if (!LocalData.IsEnglish) SetCnText();
            }
        }

        private void SetCnText()
        {
            grpControlHistory.Text = "本地文档列表";
            grpControlCurrent.Text = "分发文档列表";
        }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                ;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="value"></param>
        public void BindingData(List<DocumentInfo> beforeDocumentSource, List<DocumentInfo> afterDocumentSource)
        {
            BeforeDocumentSource = beforeDocumentSource;
            AfterDocumentSource = afterDocumentSource;

            HistoryDocumentListPresenter.BusinessContext = GetContext();
            this.DocumentListHistory.Presenter = HistoryDocumentListPresenter;
            DocumentListHistory.DataSource = beforeDocumentSource;

            CurrentDocumentListPresenter.BusinessContext = GetContext();
            this.DocumentListCurrent.Presenter = CurrentDocumentListPresenter;
            DocumentListCurrent.DataSource = afterDocumentSource;

        }

        public BusinessOperationContext GetContext()
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationType = OperationType.OceanImport;
            return context;

        }

        private void UCDocumentHistoryPartNew_Resize(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = (int)(splitContainerControl1.Width / 2);
        }
    }
}
