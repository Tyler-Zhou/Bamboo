using DevExpress.XtraEditors;
using ICP.Business.Common.UI.Document;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价预览
    /// </summary>
    public partial class QuotedPriceView : XtraUserControl
    {
        #region Fields & Services & Propetry 
        /// <summary>
        /// 编辑面板
        /// </summary>
        private QuotedPriceEditPart editPart;
        /// <summary>
        /// 文档面板
        /// </summary>
        private UCDocumentList documentPart;
        #region Property
        #region 报价单ID

        private Guid _quotedPriceID;
        /// <summary>
        /// 报价ID
        /// </summary>
        public Guid QuotedPriceID
        {
            get { return _quotedPriceID; }
            set
            {
                _quotedPriceID = value;
                BindData();
            }
        }
        #endregion
        #endregion

        #endregion

        #region Init
        /// <summary>
        /// 报价预览
        /// </summary>
        public QuotedPriceView()
        {
            InitializeComponent();
            InitControls();
        } 
        #endregion

        #region Controls Event

        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            #region Add Edit Part
            editPart = new QuotedPriceEditPart();
            editPart.Dock = DockStyle.Fill;
            sccMain.Panel1.Controls.Add(editPart);
            #endregion

            #region Document List Part
            documentPart = new UCDocumentList();
            documentPart.Presenter = new DocumentListPresenter();
            documentPart.IsView = true;
            documentPart.Dock = DockStyle.Fill;
            sccMain.Panel2.Controls.Add(documentPart);

            
            #endregion
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            editPart.ViewQuotedPrice(QuotedPriceID);
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = QuotedPriceID;
            context.OperationType = OperationType.QuotedPrice;
            documentPart.DataBind(context);
        }

        #endregion
    }
}
