#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/24 星期二 11:02:02
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.ECommerce
{
    /// <summary>
    /// 业务编辑：选择电商业务
    /// </summary>
    public partial class ECommerceEditPart : BaseEditPart
    {
        #region Member
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 口岸ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string sOperationNo {
            get { return stxtOperationNo.Text; } 
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime? sBeginDate
        {
            get
            {
                DateTime? date = (DateTime?)stxtBeginDate.EditValue;
                if (date != null)
                {
                    date = Convert.ToDateTime(date.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                }
                return date;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        DateTime? sEndDate
        {
            get
            {
                DateTime? date= (DateTime?)stxtEndDate.EditValue;
                if (date != null)
                {
                    date =Convert.ToDateTime(date.Value.ToString("yyyy-MM-dd")+ " 23:59:59");
                }
                return date;
            }
        }

        /// <summary>
        /// 最大记录行数
        /// </summary>
        int maxRecords { get; set; }

        /// <summary>
        /// 当前行
        /// </summary>
        ECommerceList CurrentRow
        {
            get
            {
                if (bindingSource.DataSource == null || bindingSource.Current == null) return null;
                return bindingSource.Current as ECommerceList;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bindingSource.DataSource;
            }
            set
            {
                bindingSource.DataSource = value;
                bindingSource.ResetBindings(false);
            }
        }

        /// <summary>
        /// 数据源(List)
        /// </summary>
        public List<ECommerceList> ListDataSource
        {
            get { return DataSource as List<ECommerceList>; }
            set
            {
                DataSource = value;
            }
        }

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 填充选择项
        /// </summary>
        public EventHandler<CommonEventArgs<List<ECommerceList>>> FillSelectItems;

        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 选择电商业务
        /// </summary>
        public ECommerceEditPart()
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

            RegisterEvents();
        }
        
        #endregion

        #region Init
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvents()
        {
            btnSearch.Click += btnSearch_Click;
            btnSure.Click += btnSure_Click;
            btnCancel.Click += btnCancel_Click;
            gvList.RowCellClick += gvList_RowCellClick;
        }

        /// <summary>
        /// 初始化控件(设置控件显示文本)
        /// </summary>
        void InitControls()
        {
            try
            {
                stxtBeginDate.EditValue = DateTime.Now.AddDays(-7);
                stxtEndDate.EditValue = DateTime.Now;
                maxRecords = 100;
                BindData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion

        #region Delegate & Window Event
        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        /// <summary>
        /// 列点击
        /// </summary>
        void gvList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.Name.Equals("IsSelect"))
                {
                    CurrentRow.IsSelect = !CurrentRow.IsSelect;
                    bindingSource.ResetBindings(false);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                maxRecords = 0;
                BindData();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                List<ECommerceList> selectItems = ListDataSource.Where(fItem=>fItem.IsDirty).ToList();
                ECommerceSaveRequest saveRequest = new ECommerceSaveRequest
                {
                    ID = OperationID,
                    IsAssociateds = selectItems.Select(ecItem => ecItem.IsSelect).ToArray(),
                    AssociationIDs = selectItems.Select(ecItem => ecItem.ID).ToArray(),
                    SaveByID = LocalData.UserInfo.LoginID,
                    UpdateDate = DateTime.Now,
                };
                FCMCommonService.ChangeAssociationBusiness(saveRequest);
                if (FillSelectItems != null)
                {
                    FillSelectItems(this, new CommonEventArgs<List<ECommerceList>>(selectItems));
                }
                var form = FindForm();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save the data successfully." : "保存数据成功!");
                if (form != null) form.Close();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var form = FindForm();
                if (form != null) form.Close();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        } 
        #endregion

        #region Methods

        void BindData()
        {
            var qcParameter = new QueryCriteria4ECommerce
            {
                OperationID = OperationID,
                OperationNo = sOperationNo,
                CompanyID = CompanyID,
                BeginDate = sBeginDate,
                EndDate = sEndDate,
                MaxRecords = maxRecords,
            };
            List<ECommerceList> list = FCMCommonService.GetSelfSpellingECommerceList(qcParameter);
            bindingSource.DataSource = list;
            bindingSource.ResetBindings(false);
        }
        #endregion
    }
}
