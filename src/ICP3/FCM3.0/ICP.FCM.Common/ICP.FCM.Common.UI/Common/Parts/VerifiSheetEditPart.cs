using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;


namespace ICP.FCM.Common.UI.Common.Parts
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class VerifiSheetEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public IVerificationSheetService verificationSheetService { get; set; }

        #endregion

        #region 属性

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                //base.DataSource = value;
                //_currentOceanBLList = value as OceanBLList;
               
                object[] data = value as object[];
                _currentOperationId = new Guid(data[0].ToString());
                _currentOperationNo = data[1].ToString();
            }
        }

        //OceanBLList _currentOceanBLList;
        //object[] _currentSource = null;

        /// <summary>
        /// 当前业务ID
        /// </summary>
        Guid _currentOperationId;
       
        /// <summary>
        /// 当前业务号
        /// </summary>
        string _currentOperationNo = string.Empty;

        /// <summary>
        /// 列表当前行
        /// </summary>
        public VerificationSheet ListCurrentData
        {
            get
            {
                return this.bsList.Current as VerificationSheet;
            }
        }

        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<VerificationSheet> ListDataSource
        {
            get
            {
                return this.bsList.DataSource as List<VerificationSheet>;
            }
        }  

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 窗体变量

        VerificationSheet _curruntEditDataSource = null;  

        ///// <summary>
        ///// 是否有数据发生改变
        ///// </summary>
        //private bool IsChanged
        //{
        //    get
        //    {            
        //        return _curruntEditDataSource.IsDirty;
        //    }
        //}

        #endregion

        #region 初始化数据

        public VerifiSheetEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };          
        }
       
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitContols();
            }

            this.SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(VerifiSheetEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitContols()
        {
            List<VerificationSheet> sheetList = verificationSheetService.GetVerificationSheetListByIds(_currentOperationId);
            if (sheetList == null || sheetList.Count == 0)
            {
                bsList.DataSource = typeof(VerificationSheet);
            }
            else
            {
                bsList.DataSource = sheetList;              
            }

            bsList.ResetBindings(false);
            gvMain.BestFitColumns();
          
            if (sheetList.Count == 0)
            {
                SetButtonEnabled(false);
            }

            //注册搜索器
            SearchRegister();
        }

      
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            //客户搜索器
            dfService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          txtCustomer.Text = _curruntEditDataSource.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtCustomer.Tag = _curruntEditDataSource.CustomerId = new Guid(resultData[0].ToString());

                      }, delegate
                      {
                          txtCustomer.Text = _curruntEditDataSource.CustomerName = string.Empty;
                          txtCustomer.Tag = _curruntEditDataSource.CustomerId = Guid.Empty;

                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        #endregion

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region 选择行发生改变

        /// <summary>
        /// 当前行发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsList_CurrentChanged(object sender, EventArgs e)
        {
            if (bsList.Current != null)
            {
                //绑定编辑界面的数据
                VerificationSheet sheet = bsList.Current as VerificationSheet;
                //sheet.IsDirty = false;
                _curruntEditDataSource = sheet;
                bsSheetInfo.DataSource = _curruntEditDataSource;
                bsSheetInfo.ResetBindings(false);
                SetButtonEnabled(true);
            }
            else
            {
                VerificationSheet nullData = new VerificationSheet();
                _curruntEditDataSource = nullData;
                bsSheetInfo.DataSource = _curruntEditDataSource;
                bsSheetInfo.ResetBindings(false);
                SetButtonEnabled(false);
            }
        }

        /// <summary>
        /// 禁用/启用工具栏按钮与面板
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetButtonEnabled(bool isEnabled)
        {
            groupBase.Enabled = isEnabled;        
            this.barDelete.Enabled = isEnabled;
            this.barSave.Enabled = isEnabled;          
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            VerificationSheet newSheet = new VerificationSheet();
            newSheet.OperationId = _currentOperationId;
            newSheet.OperationNo = _currentOperationNo;
            newSheet.CreateByID = LocalData.UserInfo.LoginID;
            newSheet.CreateByName = LocalData.UserInfo.LoginName;
            newSheet.CreateDate = DateTime.Now;
            newSheet.IsDirty = true;
            newSheet.BeginEdit();
            bsList.Insert(0, newSheet);
            gvMain.FocusedRowHandle = 0;
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (bsList.Current == null) return;

            if (PartLoader.EnquireIsDeleteCurrentData())
            {
                VerificationSheet currentData = ListCurrentData;
                try
                {
                    if (!currentData.IsNew)
                    {
                        verificationSheetService.RemoveVerificationSheet(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                    }

                    bsList.RemoveCurrent();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Delete Successfully." : "删除成功!");

                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }

        #endregion

        #region 保存

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            bsSheetInfo.EndEdit();
            if (_curruntEditDataSource == null)
            {
                return false;
            }

            if (this.ValidateData() == false)
            {
                return false;
            }    

            Guid? customerId = null;
            if (_curruntEditDataSource.CustomerId != null)
            {
                customerId = _curruntEditDataSource.CustomerId.Value;
            }

            try
            {
                SingleResult result = this.verificationSheetService.SaveVerificationSheet(_curruntEditDataSource.ID,
                                                                                 _curruntEditDataSource.SheetNo,
                                                                                 _curruntEditDataSource.OperationId,
                                                                                 customerId,
                                                                                 _curruntEditDataSource.ReceiptDate,
                                                                                _curruntEditDataSource.ReturnDate,
                                                                                _curruntEditDataSource.ExpressNO,
                                                                                _curruntEditDataSource.IsFreightArrive,
                                                                                _curruntEditDataSource.Remark,
                                                                                 LocalData.UserInfo.LoginID,
                                                                                 _curruntEditDataSource.UpdateDate);
                if (result == null)
                {
                    return false;
                }
                else
                {
                    //更改当前对象版本号
                    _curruntEditDataSource.ID = result.GetValue<Guid>("ID");
                    _curruntEditDataSource.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _curruntEditDataSource.IsDirty = false;
                }

                VerificationSheet listCurrentData = ListCurrentData;
                Utility.CopyToValue(_curruntEditDataSource, listCurrentData, typeof(VerificationSheet));
                this.bsList.ResetBindings(false);
                this.gvMain.RefreshData();
                this.gvMain.BestFitColumns();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully." : "保存成功");             
                return true;

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        #endregion

        #region 验证数据

        /// <summary>
        /// 验证数据    
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            bsSheetInfo.EndEdit();
            if (!_curruntEditDataSource.Validate())
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 退出时验证保存
        /// <summary>
        /// 换行时验证是否保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (_curruntEditDataSource.IsDirty)
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Allow = false;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.Save())
                    {
                        e.Allow = false;
                    }
                }
            }
        }
        #endregion

        void VerifiSheetEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (_curruntEditDataSource.IsDirty)
            {
                DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!this.Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BaseDataObject list = gvMain.GetRow(e.RowHandle) as BaseDataObject;
            if (list == null)
            {
                return;
            }

            if (list.IsNew || list.IsDirty)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
        }
    }
}
