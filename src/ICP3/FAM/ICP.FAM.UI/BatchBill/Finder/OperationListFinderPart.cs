using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 查找业务窗体
    /// </summary>
    public partial class OperationListFinderPart : BaseListPart
    {
        #region Services & Fields & Property & Delegate
        #region Fields
        List<EnumHelper.ListItem<OperationType>> _OperationTypes = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
        #endregion

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }
        
        /// <summary>
        /// 业务公共信息接口
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();

            }
        }
        
        #endregion

        #region Property
        /// <summary>
        /// 查询业务号
        /// </summary>
        public string SearchOperationNo 
        {
            set
            {
                txtOperationNo.Text = value;
            }
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }

        /// <summary>
        /// 选择业务
        /// </summary>
        public OperationCommonInfo SelectedBusiness { get; set; }

        /// <summary>
        /// 计费日
        /// </summary>
        public DateTime AccountDate
        {
            get;
            set;
        }

        public OperationType SearchOperationType
        {
            get
            {
                if (scmbOperationType.EditValue != null && scmbOperationType.EditValue != DBNull.Value) 
                    return (OperationType)scmbOperationType.EditValue;
                return OperationType.OceanExport;
            }
        }
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 查找业务窗体
        /// </summary>
        public OperationListFinderPart()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                if (RootWorkItem != null)
                {
                    RootWorkItem.Items.Remove(this);
                    RootWorkItem = null;
                }
                UnRegisterEvent();
            };
        }
        /// <summary>
        /// OnLoad
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        #endregion

        #region Controls Event
        /// <summary>
        /// 查询报价单
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? from = null, to = null;
                from = new DateTime(dteStartDate.DateTime.Year, dteStartDate.DateTime.Month, dteStartDate.DateTime.Day, 0, 0, 0);
                to = new DateTime(dtpEndDate.DateTime.Year, dtpEndDate.DateTime.Month, dtpEndDate.DateTime.Day, 23, 59, 59);
                List<OperationCommonInfo> list = FCMCommonService.GetOperationCommonInfoList(from, to, AccountDate, txtOperationNo.Text.Trim()
                    , SearchOperationType);
                bsOperationCommInfo.DataSource = list;
                bsOperationCommInfo.ResetBindings(false);

                
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        void btnOK_Click(object sender, EventArgs e)
        {
            SelectAndCloseForm();
        }
        /// <summary>
        /// 取消
        /// </summary>
        void btnCancle_Click(object sender, EventArgs e)
        {
            CloseForm(DialogResult.Cancel);
        }
        /// <summary>
        /// 双击选择
        /// </summary>
        void gvMain_DoubleClick(object sender, EventArgs e)
        {
            SelectAndCloseForm();
        }
        /// <summary>
        /// 双击
        /// </summary>
        void gvManifest_DoubleClick(object sender, EventArgs e)
        {
            SelectAndCloseForm();
        }
        /// <summary>
        /// 选择业务改变
        /// </summary>
        private void bsOperationCommInfo_PositionChanged(object sender, EventArgs e)
        {
            OperationCommonInfo ocInfo = bsOperationCommInfo.Current as OperationCommonInfo;
            bsManifest.DataSource = null; ;
            if (ocInfo != null)
            {
                bsManifest.DataSource = ocInfo.Forms;
            }
            bsManifest.ResetBindings(false);
        }
        /// <summary>
        /// 业务类型首次获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbOperationTypeFirstEnter(object sender, EventArgs e)
        {
            scmbOperationType.Properties.BeginUpdate();
            scmbOperationType.Properties.Items.Clear();
            foreach (var item in _OperationTypes.Where(item =>
                (item.Value != OperationType.Unknown
                    && item.Value != OperationType.Customs
                    && item.Value != OperationType.Internal
                    && item.Value != OperationType.Trailer
                    && item.Value != OperationType.InquireRate
                    && item.Value != OperationType.WorkFlow
                    && item.Value != OperationType.DispatchFile
                    && item.Value != OperationType.QuotedPrice
                    )))
            {
                scmbOperationType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            scmbOperationType.Properties.EndUpdate();
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControls()
        {
            dteStartDate.EditValue = DateTime.Now.AddDays(-14);
            dtpEndDate.EditValue = DateTime.SpecifyKind(AccountDate, DateTimeKind.Unspecified);

            scmbOperationType.ShowSelectedValue(OperationType.OceanExport, EnumHelper.GetDescription(OperationType.OceanExport, LocalData.IsEnglish));
            scmbOperationType.OnFirstEnter += OncmbOperationTypeFirstEnter;

            foreach (var item in _OperationTypes)
            {
                cmbOperationType.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            btnSearch.Click += btnSearch_Click;
            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancle_Click;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvManifest.DoubleClick += gvManifest_DoubleClick;
            bsOperationCommInfo.PositionChanged += bsOperationCommInfo_PositionChanged;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        void UnRegisterEvent()
        {
            btnSearch.Click -= btnSearch_Click;
            btnOK.Click -= btnOK_Click;
            btnCancel.Click -= btnCancle_Click;
            gvMain.DoubleClick -= gvMain_DoubleClick;
            gvManifest.DoubleClick -= gvManifest_DoubleClick;
            bsOperationCommInfo.PositionChanged -= bsOperationCommInfo_PositionChanged;
        }
        /// <summary>
        /// 选择数据并关闭窗体
        /// </summary>
        void SelectAndCloseForm()
        {
            OperationCommonInfo current = bsOperationCommInfo.Current as OperationCommonInfo;
            if (current == null) return;


            SelectedBusiness = new OperationCommonInfo
            {
                OperationID = current.OperationID,
                OperationNo=current.OperationNo,
                OperationType = current.OperationType,
                OperationDate=current.OperationDate,
                CompanyID = current.CompanyID,
                Forms=new List<FormData>()
            };
            FormData currentFD = bsManifest.Current as FormData;
            FormData formItem=new FormData();
            if (currentFD == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "TO DO" : "此业务没有参考单号.无法维护帐单."
                    , LocalData.IsEnglish ? " Tip" : "提示", MessageBoxButtons.OK);
                return;
            }
            formItem.ID = currentFD.ID;
            formItem.Type = currentFD.Type;
            formItem.No = currentFD.No;
            SelectedBusiness.Forms.Add(formItem);
            CloseForm(DialogResult.OK);
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="dialogResult"></param>
        void CloseForm(DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.Cancel)
            {
                SelectedBusiness = null;
            }
            var findForm = FindForm();
            if (findForm != null)
            {
                findForm.DialogResult = dialogResult;
                findForm.Close();
            }
        }
        #endregion
    }
}
