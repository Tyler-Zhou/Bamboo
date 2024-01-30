using ICP.DataCache.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.UI.Document
{
    public partial class BindCorrelation : BasePart
    {
        public BindCorrelation()
        {
            InitializeComponent();
        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 海出公共服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        public IClientFileService BusinessFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }
        public IFileService FileService
        {
            get
            {
                return ServiceClient.GetService<IFileService>();
            }
        }

        
        #endregion

        #region 字段及属性
        public int operationType = 1; //业务类型 1 进口 2 出口

        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid OperationId { get; set; }


        /// <summary>
        ///文档列表数据源
        /// </summary>
        public List<DocumentInfo> DocumentInfoDataSource { get; set; }


        /// <summary>
        ///选择的文档主键Ids
        /// </summary>
        List<Guid?> DispatchFileIDs = new List<Guid?>();
        #endregion



        #region 事件处理

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                BusinessOperationContext context = new BusinessOperationContext();
                context.OperationID = OperationId;

                DocumentInfoDataSource = BusinessFileService.GetBusinessDocumentList(context);
                bindingSource1.DataSource = DocumentInfoDataSource;
                gridViewList.RefreshData();

                LoadControl();
                if (!LocalData.IsEnglish)
                {
                    SetCnText();
                }
            }
        }

        void Closed()
        {
            this.FindForm().Close();
        }

        #endregion

        #region 辅助方法
        private void SetCnText()
        {
            colName.Caption = "文档名称";
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建时间";
            colDocumentType.Caption = "单证类型";
            colFormId.Caption = "提单";
            btnClose.Text = "关闭";
            btnOk.Text = "确定";
        }

        private void LoadControl()
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = OperationId;

            List<OceanBLList> bllist = OceanExportService.GetOceanBLListByOperationInfo(context);
            bllist.Insert(0,new OceanBLList());
            repositoryItemLookUpEdit1.DataSource = bllist;
            repositoryItemLookUpEdit1.DisplayMember = "No";
            repositoryItemLookUpEdit1.ValueMember = "ID";
            
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            gridViewList.EndUpdate();
            bindingSource1.EndEdit();

            List<Guid> ids = new List<Guid>();
            List<Guid?> formids = new List<Guid?>();

            foreach (DocumentInfo di in DocumentInfoDataSource)
            {
                if (di.Selected)
                {
                    ids.Add(di.Id);
                    formids.Add(di.FormId);
                }
            }

            try
            {
                FileService.ChangeBind(ids.ToArray(), formids.ToArray(), LocalData.UserInfo.LoginID);
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        private void gridViewList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Column.FieldName == "FormId")
            {
                DocumentInfo doucument = gridViewList.GetFocusedRow() as DocumentInfo;
                doucument.FormId = e.Value as Guid?;
                doucument.Selected = true;
            }
        }
    }
}
