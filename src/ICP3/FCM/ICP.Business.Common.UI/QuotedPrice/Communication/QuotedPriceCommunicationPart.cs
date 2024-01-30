using System.Collections.Generic;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价沟通面板
    /// </summary>
    public partial class QuotedPriceCommunicationPart :BaseEditPart
    {
        #region Service & Property

        #region Property
        #region 是否显示工具栏
        private bool _IsShowTools;
        /// <summary>
        /// 是否显示工具栏
        /// </summary>
        public bool IsShowTools
        {
            get { return _IsShowTools; }
            set
            {
                _IsShowTools = value;
                ucCommunicationPart.IsShowTools = _IsShowTools;
            }
        }  
        #endregion

        #region 是否显示复选框
        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public bool IsShowChoose
        {
            set
            {
                ucCommunicationPart.IsShowChoose = value;
            }
        }
        #endregion

        #region DataSource
        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<CommunicationHistory> ListDataSource
        {
            get { return ucCommunicationPart.DataSource; }
            set
            {
                ucCommunicationPart.DataSource = value;
            }
        }
        #endregion
        #endregion

        #endregion

        #region Inti
        /// <summary>
        /// 报价沟通面板
        /// </summary>
        public QuotedPriceCommunicationPart()
        {
            InitializeComponent();
            InitControls();
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="contex">绑定数据</param>
        public void DataBind(BusinessOperationContext contex)
        {
            if (contex == null)
            {
                Enabled = false;
                return;
            }
            Enabled = true;
            ucCommunicationPart.DataBind(contex);
        }

        private void InitControls()
        {
            ucCommunicationPart.Presenter = new Communication.CommunicationHistoryListPresenter();
        } 
        #endregion
    }
}
