//-----------------------------------------------------------------------
// <copyright file="SetReportParameterForm.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    using System;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;

    public partial class SetReportParameterForm : DevExpress.XtraEditors.XtraForm
    {
        private CompanyReportConfigureListClient _currentReportConfigure;
        private string _reportValueTemp = string.Empty;

        #region 控制器
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public CompanyConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<CompanyConfigureController, CompanyConfigureController>();
            }
        }

        #endregion 

        #region 初始化

        public SetReportParameterForm()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.gridView1.RowCellStyle -= this.gridView1_RowCellStyle;
                this.repositoryItemMemoExEdit1.Closed -= this.repositoryItemMemoExEdit1_Closed;
                this.repositoryItemMemoExEdit1.Popup -= this.repositoryItemMemoExEdit1_Popup;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.gridControl1.DataSource = null;
                this._currentReportConfigure = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
            gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            ReportParameterList item = gridView1.GetRow(e.RowHandle) as ReportParameterList;
            if (item == null) return;

            if (item.ParameterType == ReportParameterType.Bool)
            {
                this.colValue.ColumnEdit = this.repositoryItemCheckEdit1;
            }
            else if (item.ParameterType == ReportParameterType.String)
            {
                this.colValue.ColumnEdit = this.repositoryItemMemoExEdit1;
            }
            else
            {
                this.colValue.ColumnEdit = this.repositoryItemSpinEdit1;
            }         
        }
    
        private void InitControls()
        {
            if (LocalData.IsEnglish)
            {
                colDescription.FieldName = "EDescription";
            }
            else
            {
                colDescription.FieldName = "CDescription";
            }

            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
            barClose.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Close_16;
            List<ReportParameterList> parameters = _currentReportConfigure.Parameters;

            foreach (var item in parameters)
            {
                if (item.ParameterType == ReportParameterType.Bool)
                {
                    if (item.ParameterValue != null && item.ParameterValue.ToUpper() == "TRUE")
                    {
                        item.ParameterValueObject = true;
                    }
                    else
                    {
                        item.ParameterValueObject = false;
                    }
                }
                else if (item.ParameterType == ReportParameterType.Digital)
                {
                    if (!string.IsNullOrEmpty(item.ParameterValue))
                    {
                        item.ParameterValueObject = decimal.Parse(item.ParameterValue);
                    }
                }
                else
                {
                    item.ParameterValueObject = item.ParameterValue;
                }

                //item.BeginEdit();
            }

            bsDataSource.DataSource = parameters;
            foreach (var paraItem in parameters)
            {
                paraItem.BeginEdit();
            }
        }

        #endregion

        #region 本地方法

        public void EndEdit()
        {
            this.Validate();
            bsDataSource.EndEdit();
        }

        /*验证*/
        private bool ValidateData()
        {
            //dtErrorInfo.ClearErrors();

            //if (string.IsNullOrEmpty(txtName.Text.Trim()))
            //{
            //    dtErrorInfo.SetError(txtName, "必须录入名称才能检查.");
            //    txtName.Focus();

            //    return false;
            //}

            return true;
        }
        #endregion

        #region 事件处理

        void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            List<ReportParameterList> parameters = bsDataSource.DataSource as List<ReportParameterList>;
            foreach (var item in parameters)
            {
                item.ReportID = _currentReportConfigure.ID;

                if (item.ParameterValueObject == null)
                {
                    item.ParameterValue = string.Empty;
                }
                else
                {
                    item.ParameterValue = item.ParameterValueObject.ToString();
                }
            }

            _currentReportConfigure.Parameters = parameters;
            this.Close();
        }

        void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void repositoryItemMemoExEdit1_Popup(object sender, System.EventArgs e)
        {
            _reportValueTemp = (sender as MemoExEdit).Text;
        }                    

        void repositoryItemMemoExEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if(e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Immediate)
            {
                (sender as MemoExEdit).Text = _reportValueTemp;
            }
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 当前报表配置
        /// </summary>
        public CompanyReportConfigureListClient CurrentReportConfigure
        {
            get
            {
                //return this.txtName.Text.Trim();
                return _currentReportConfigure;
            }

            set
            {
                _currentReportConfigure = value;
            }
        }

        #endregion
    }
}