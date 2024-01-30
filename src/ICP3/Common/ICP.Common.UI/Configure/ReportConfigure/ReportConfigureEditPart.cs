using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Common.UI.Configure.ReportConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class ReportConfigureEditPart : BaseEditPart
    {      
        #region init

        public ReportConfigureEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            this.Disposed += delegate
            {
                this.Saved = null;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.gridControl1.DataSource = null;
                this.dxErrorProvider1.DataSource = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this.parametersBindingSource.DataSource = null;
                this.parametersBindingSource.Dispose();
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        #endregion

        #region 控制器

        public ReportConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<ReportConfigureController, ReportConfigureController>();
            }
        }

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion   
 
        ///// <summary>
        ///// 报表类型ID与名称信息
        ///// </summary>
        //Dictionary<Guid, String> _reportTypeDictionary = new Dictionary<Guid, String>();

        ReportConfigureList CurrentData
        {
            get { return bsDataSource.DataSource as ReportConfigureList; }
            set { bsDataSource.DataSource = value; }
        }

        private void InitControls()
        {
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
            barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Plus_16;
            barDelete.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
           
            //Utility.SetEnterToExecuteOnec(comboBoxType, delegate
            //{
                comboBoxType.Properties.Items.Clear();
                List<ReportType> reportTypes = this.Controller.GetReportTypes();

                comboBoxType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, System.DBNull.Value));
                foreach (var type in reportTypes)
                {
                    comboBoxType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(type.Name, (object)type.Index));
                }

                this.comboBoxType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "ReportTypeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            //});

            List<EnumHelper.ListItem<ReportParameterType>> reportParameterTypes = EnumHelper.GetEnumValues<ReportParameterType>(LocalData.IsEnglish);
            foreach (var type in reportParameterTypes)
            {
                repositoryItemImageComboBox2.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(type.Name, type.Value));
            }

            if (CurrentData != null)
            {
                CurrentData.BeginEdit();
            }
         }

        #region  事件   
 
        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        /*保存*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            if (CurrentData == null) return;

            if (ValidateData() == false)
            {
                return;
            }

            try
            {
                //保存数据
                if (this.SaveData())
                {
                    //触发保存成功事件
                    if (this.Saved != null)
                    {
                        this.Saved(this.bsDataSource.DataSource);
                    }

                    CurrentData.IsDirty = false;
                    //提示保存成功
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                        this.FindForm(),
                        "保存成功!");
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                       this.FindForm(),
                       "保存失败!");
                }
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        /*添加报表参数*/
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            ReportParameterList newData = new ReportParameterList();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.ID = Guid.NewGuid();
            newData.isNewTag = true;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsDirty = false;
            parametersBindingSource.Insert(0, newData);
            parametersBindingSource.MoveFirst();
            RefreshToolBars();
        }

        /*删除报表参数*/
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {          
            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            parametersBindingSource.EndEdit();
            try
            {
                DeleteRow();
                RefreshToolBars();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        #endregion

        public object[] BeforeParentChanged()
        {
            //EndEdit();
            object[] para = new object[2];
            para[0] = true;
            if (CurrentData == null)
            {
                para[0] = false;
                return para;
            }

            if (CurrentData.IsNew)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByNew();
                if (dlg == DialogResult.Yes)
                {
                    if (ValidateData() == false)
                    {
                        para[0] = false;
                        return para;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    para[0] = false;
                }
                else if (dlg == DialogResult.No)
                {
                    para[0] = true;
                    para[1] = true;
                }
            }
            else if (CurrentData.IsDirty)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByUpdated();
                if (dlg == DialogResult.Yes)
                {
                    if (ValidateData() == false)
                    {
                        para[0] = false;
                        return para;
                    }

                    RaiseSaved();
                }
                else if (dlg == DialogResult.Cancel)
                {
                    para[0] = false;
                    return para;
                }
                else if (dlg == DialogResult.No)
                {
                    para[0] = true;
                }
            }

            return para;
        }  

        #region IEditPart接口

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return this.bsDataSource.DataSource;
            }
            set
            {
                this.bsDataSource.DataSource = value;
            }
        }

        /// <summary>
        /// 保存完成触发该事件
        /// </summary>
        public override event SavedHandler Saved;

        /// <summary>
        /// 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            this.Validate();
            bsDataSource.EndEdit();          
        }

        private ReportConfigureList _currentReportConfigure;
      
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null && values.ContainsKey("ReportConfigureList"))
            {
                _currentReportConfigure = (ReportConfigureList)values["ReportConfigureList"];
                if (_currentReportConfigure == null)
                {
                    this.Enabled = false;
                    return;
                }
                else
                {
                    if (!_currentReportConfigure.IsValid)
                    {
                        this.Enabled = false;
                    }
                    else
                    {
                        this.Enabled = true;
                    }
                }

                ReportConfigureList data = new ReportConfigureList();
                ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(_currentReportConfigure, data);
                this.bsDataSource.DataSource = data;
                //if (data != null && (comboBoxType.Properties.Items.Count == 0 || comboBoxType.Properties.Items.Count == 1))
                //{
                //    comboBoxType.Properties.Items.Clear();
                //    comboBoxType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(CurrentData.BizReportType, System.DBNull.Value));
                //    comboBoxType.SelectedIndex = 0;
                //}            

                data.BeginEdit();
                RefreshToolBars();
            }
        }

        /// <summary>
        /// 触发保存事件
        /// </summary>
        public override void RaiseSaved()
        {
            this.SaveData();

            if (this.Saved != null)
            {
                this.Saved(this.DataSource);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            var currentReportConfigure = bsDataSource.DataSource as ReportConfigureList;

            if (currentReportConfigure.ID != Guid.Empty && currentReportConfigure.Parameters != null && currentReportConfigure.Parameters.Count > 0)
            {
                foreach (var item in currentReportConfigure.Parameters)
                {
                    item.ReportID = currentReportConfigure.ID;
                }
            }    

            if (currentReportConfigure != null)
            {
                ManyResultData mans = this.Controller.SaveReportConfigureInfo(
                currentReportConfigure.ID,
                currentReportConfigure.Code,
                currentReportConfigure.CDescription,
                currentReportConfigure.EDescription,
                currentReportConfigure.Parameters.ToArray(),
                currentReportConfigure.ReportTypeID,
                LocalData.UserInfo.LoginID,
                currentReportConfigure.UpdateDate);

                if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
                {
                    return false;
                }
                else
                {
                    int i = 0;
                    foreach (ReportConfigureList cid in bsDataSource.List)
                    {
                        cid.ID = mans.ChildResults[i].ID;
                        cid.UpdateDate = mans.ChildResults[i].UpdateDate;
                        cid.IsDirty = false;
                        i++;
                    }

                    if (currentReportConfigure.Parameters != null && currentReportConfigure.Parameters.Count > 0)
                    {
                        foreach (var item in currentReportConfigure.Parameters)
                        {
                            //item.ReportID = currentReportConfigure.ID;
                            item.isNewTag = false;
                        }
                    }   
                }
            }

            return true;
        }

        #endregion

        #region 本地方法    

        /*验证表单数据*/
        private bool ValidateData()
        {
            if(CurrentData.Validate() == false)
            {
                return false;
            }

            if (CurrentData.Parameters == null || CurrentData.Parameters.Count < 1)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                       this.FindForm(),
                       "保存失败，请先定义报表参数!");
                return false;
            }

            if (CurrentData.Parameters != null && CurrentData.Parameters.Count > 0)
            {
                foreach (ReportParameterList c in CurrentData.Parameters)
                {
                    if (c.Validate() == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /*刷新工具栏*/
        private void RefreshToolBars()
        {
            if (parametersBindingSource.Current == null)
            {
                barDelete.Enabled = false;
            }
            else
            {
                barDelete.Enabled = true;
            }

            //if (bsDataSource.Count > 0)
            //{
            //    barSave.Enabled = true;
            //}
            //else
            //{
            //    barSave.Enabled = false;
            //}
        }

        /*删除当前选择的报表参数*/
        private void DeleteRow()
        {
            if (parametersBindingSource.Current != null)
            {
                var parameter = parametersBindingSource.Current as ReportParameterList;
                if (parameter != null)
                {
                    if (parameter.ID != null && parameter.ID != Guid.Empty && !parameter.isNewTag)
                    {
                        List<ConfigureList> configureListHadUsedTheParameter = Controller.GetCompanyForReportParameterIsUsed(_currentReportConfigure.ID, parameter.ID);
                        //ConfigureList con0 = new ConfigureList();
                        //con0.CompanyName = "goubao";
                        //configureListHadUsedTheParameter.Add(con0);
                        //ConfigureList con1 = new ConfigureList();
                        //con1.CompanyName = "gousun";
                        //configureListHadUsedTheParameter.Add(con1);
                        if (configureListHadUsedTheParameter == null || configureListHadUsedTheParameter.Count == 0)
                        {
                            parametersBindingSource.RemoveCurrent();
                            parametersBindingSource.MoveFirst();
                        }
                        else
                        {
                            string configureString = string.Empty;
                            foreach(var item in configureListHadUsedTheParameter)
                            {
                                if (configureString != string.Empty)
                                {
                                    configureString += ", ";
                                }

                                configureString += item.CompanyName;
                            }

                            DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Can not delect it!" : "不能删除,公司:" + configureString + "已使用该参数!", LocalData.IsEnglish ? "Tip" : "提示");
                        }
                    }
                    else
                    {
                        parametersBindingSource.RemoveCurrent();
                        parametersBindingSource.MoveFirst();
                    }
                }
            }
        } 
     
        #endregion
    }
}
