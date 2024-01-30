using System;
using System.Collections.Generic;
using ICP.EDI.ServiceInterface;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.EDI.UI
{
    /// <summary>
    /// EDI预览界面
    /// </summary>
    public partial class EDIPreview : BasePart
    {
        #region 成员
        /// <summary>
        /// EDI类型
        /// </summary>
        public EDIMode EDIType { get; set; }
        /// <summary>
        /// EDI发送表单ID
        /// </summary>
        public Guid[] IDs { get; set; } 
        #endregion

        #region 服务
        /// <summary>
        /// EDI服务
        /// </summary>
        IEDIService EDIService
        {
            get
            {
                return ServiceClient.GetService<IEDIService>();
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        public EDIPreview()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = System.Windows.Forms.DialogResult.OK;
            FindForm().Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        void InitControls()
        {
            List<EDIPreviewValue> list = EDIService.GetEDIPreviewValueList(EDIType, IDs);
            if (list == null)
                list = new List<EDIPreviewValue>();
            gcMain.DataSource = list;
            gvDetails.RefreshData();
        } 
        #endregion
    }
}
