using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约编辑工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPToolBar : BaseEditPart
    {
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Property
        /// <summary>
        /// 重写数据源属性
        /// </summary>
        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                BindingData(value);
            }
        } 
        #endregion

        #region Init
        /// <summary>
        /// 合约编辑工具栏
        /// </summary>
        public OPToolBar()
        {
            InitializeComponent();
            Disposed += delegate
            {

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            BulidCommand();
            if (!DesignMode) { InitMessage(); }
            InitContrls();
        } 
        #endregion

        #region Method
        #region 启用发布按钮
        /// <summary>
        /// 设置为可以发布
        /// </summary>
        public void SetPublish()
        {
            barPause.Enabled = true;
            barPause.Caption = NativeLanguageService.GetText(this, "Publish");
        }
        #endregion

        /// <summary>
        /// 注册提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("Invalidate", "In&validate");
            RegisterMessage("Resume", "Resume(&V)");
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
        }

        private void InitContrls()
        {
            SetButtonEnabled(true);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="value">数据源</param>
        private void BindingData(object value)
        {
            OceanList listData = value as OceanList;

            if (listData != null && listData.IsNew)
                barAdd.Enabled = false;
            else
                barAdd.Enabled = true;


            if (listData == null || listData.IsNew)
            {
                SetButtonEnabled(true);
            }
            else
            {
                SetButtonEnabled(false);

                if (listData.State == OceanState.Expired)
                    barInvalidate.Enabled = barPause.Enabled = false;
                else
                    barInvalidate.Enabled = barPause.Enabled = true;


                if (listData.State != OceanState.Invalidated)
                    barInvalidate.Caption = NativeLanguageService.GetText(this, "Invalidate");
                else
                    barInvalidate.Caption = NativeLanguageService.GetText(this, "Resume");


                if (listData.State == OceanState.Draft)
                    barPause.Caption = NativeLanguageService.GetText(this, "Publish");
                else
                    barPause.Caption = NativeLanguageService.GetText(this, "Pause");
            }
        }
        /// <summary>
        /// 构建按钮事件连接
        /// </summary>
        private void BulidCommand()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_AddData].Execute(); };

            barDelete.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_DeleteData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_CopyData].Execute(); };
            barPause.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute(); };
            barInvalidate.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_InvalidateResumeData].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_ShowSearch].Execute(); };

            barCompare.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_Compare].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_RefreshData].Execute(); };

            barExportToExcel.ItemClick += delegate { Workitem.Commands[OPCommonConstants.Command_ExportToExcel].Execute(); };

            barClose.ItemClick += delegate
            {
                var findForm = FindForm();
                if (findForm != null) findForm.Close();
            };
        }

        /// <summary>
        /// 设置按钮可用状态
        /// </summary>
        /// <param name="isInit">是否初始化</param>
        private void SetButtonEnabled(bool isInit)
        {
            if (isInit)
                barDelete.Enabled = barCopy.Enabled = barPause.Enabled = barInvalidate.Enabled =
                    barCompare.Enabled = barExportToExcel.Enabled = false;
            else
                barDelete.Enabled = barCopy.Enabled = barCompare.Enabled = barExportToExcel.Enabled = true;
        }
        #endregion
    }
}
