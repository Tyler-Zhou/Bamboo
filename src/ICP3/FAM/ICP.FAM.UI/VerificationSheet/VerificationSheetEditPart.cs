using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.Common;

namespace ICP.FAM.UI.VerificationSheet
{
    [ToolboxItem(false)]
    public partial class VerificationSheetEditPart : BaseEditPart
    {
        #region 服务注入

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IVerificationSheetService VerificationSheetService
        {
            get
            {
                return ServiceClient.GetService<IVerificationSheetService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        #region 属性及变量

        Common.ServiceInterface.DataObjects.VerificationSheet _verificaSheet = null;
        //bool isDirty = false;
        ///// <summary>
        ///// 是否有数据发生改变
        ///// </summary>
        //private bool IsDirty
        //{
        //    get
        //    {
        //        if (_verificaSheet == null)
        //        {
        //            return false;
        //        }
        //        if (isDirty)
        //        {
        //            return true;
        //        }
        //        if (_verificaSheet.IsDirty)
        //        {
        //            return true;
        //        }
       
        //        return false;
        //    }
        //}

        #endregion

        #region init

        public VerificationSheetEditPart()
        {
            InitializeComponent();
            Disposed += delegate {

                if (operationNoFinder != null)
                {
                    operationNoFinder.Dispose();
                    operationNoFinder = null;
                }
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                _verificaSheet = null;
                Saved = null;
                bsVerifiSheet.DataSource = null;
                bsVerifiSheet.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                InitMessage();
                Closing += new EventHandler<FormClosingEventArgs>(VerificationSheetEditPart_Closing);
            }
        }

        void VerificationSheetEditPart_Closing(object sender, FormClosingEventArgs e)
        {
            Common.ServiceInterface.DataObjects.VerificationSheet sheet = bsVerifiSheet.DataSource as Common.ServiceInterface.DataObjects.VerificationSheet;
            if (sheet != null)
            {
                if (sheet.IsDirty)
                {
                    DialogResult dr = FAMUtility.EnquireIsSaveCurrentDataByUpdated();

                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else if (dr == DialogResult.Yes)
                    {
                        if (!Save())
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void InitControls()
        {
            SetDataLazyLoaders();
            SearchRegister();
        }
        
        private void InitMessage()
        {
            RegisterMessage("1108190001", LocalData.IsEnglish?"Input the consignee info":"请录入收货人信息");
        }

        #region 搜索器注册
        private IDisposable operationNoFinder, customerFinder;
        void SearchRegister()
        {
            #region RefNo

            //业务搜索器       
          operationNoFinder= DataFindClientService.Register(stxtOperationNo, FCMFinderConstants.BusinessFinderForOEAEOB, SearchFieldConstants.BusinessNo, SearchFieldConstants.BusinessResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtOperationNo.Text = _verificaSheet.OperationNo = resultData[1].ToString();
                      stxtOperationNo.Tag = _verificaSheet.OperationId = new Guid(resultData[0].ToString());

                  },
                  delegate
                  {
                      stxtOperationNo.Text = _verificaSheet.OperationNo = string.Empty;
                      stxtOperationNo.Tag = _verificaSheet.OperationId = Guid.Empty;

                  }, ClientConstants.MainWorkspace);

            #endregion

            //客户搜索器
          customerFinder=  DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          txtCustomer.Text = _verificaSheet.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtCustomer.Tag = _verificaSheet.CustomerId = new Guid(resultData[0].ToString());

                      }, delegate
                      {
                          txtCustomer.Text = _verificaSheet.CustomerName = string.Empty;
                          txtCustomer.Tag = _verificaSheet.CustomerId = Guid.Empty;

                      },
                      ClientConstants.MainWorkspace);
        }

        #endregion

        #region 下拉列表数据延迟加载

        void SetDataLazyLoaders()
        {
            //Utility.BindComboBoxByCompany(this.cmbCompany, this.UserService);
        }

        #endregion

        #endregion

        #region Save
   
        public bool Leaving()
        {
            if (_verificaSheet !=null && _verificaSheet.IsDirty)
            {
                if (FAMUtility.EnquireIsSaveCurrentDataByUpdated() == DialogResult.Yes)
                {
                    return Save();
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool Save()
        {
            Guid? customerId = null;
            if (_verificaSheet.CustomerId != null)
            {
                customerId = _verificaSheet.CustomerId.Value;
            }

            SingleResult result = VerificationSheetService.SaveVerificationSheet(_verificaSheet.ID,
                                                                                 _verificaSheet.SheetNo,
                                                                                 _verificaSheet.OperationId,
                                                                                 customerId,
                                                                                 _verificaSheet.ReceiptDate,
                                                                                _verificaSheet.ReturnDate,
                                                                                _verificaSheet.ExpressNO,
                                                                                _verificaSheet.IsFreightArrive,
                                                                                _verificaSheet.Remark,
                                                                                 LocalData.UserInfo.LoginID,
                                                                                 _verificaSheet.UpdateDate);
            if (result == null)
            {
                return false;
            }
            else
            {
                //更改当前对象版本号
                _verificaSheet.ID = result.GetValue<Guid>("ID");
                _verificaSheet.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _verificaSheet.IsDirty = false;
                return true;
            }   
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }


        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            get { return bsVerifiSheet.DataSource; }
            set { BindingData(value); }
        }

        void BindingData(object data)
        {
            if (data == null)
            {
                bsVerifiSheet.DataSource = typeof(Common.ServiceInterface.DataObjects.VerificationSheet);
                Enabled = false;
            }
            else
            {
                Enabled = true;
                _verificaSheet = data as Common.ServiceInterface.DataObjects.VerificationSheet;

                bsVerifiSheet.DataSource = _verificaSheet;
                bsVerifiSheet.ResetBindings(false);

                _verificaSheet.IsDirty = false;
              
                ((BaseDataObject)data).CancelEdit();
                ((BaseDataObject)data).BeginEdit();
            }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override event SavedHandler Saved;

        #endregion

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bsVerifiSheet.EndEdit();
                if (bsVerifiSheet.DataSource == null || bsVerifiSheet.Current == null) return;
                if (!_verificaSheet.Validate())
                {
                    return;
                }

                try
                {
                    //保存数据
                    if (SaveData())
                    {
                        ////触发保存成功事件,刷新列表？
                        //if (this.Saved != null)
                        //{
                        //    this.Saved(this.bsVerifiSheet.DataSource);
                        //}

                        //提示保存成功
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                            FindForm(),
                            "保存成功!");
                    }
                    else
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                            FindForm(),
                            "保存失败!");
                    }
                }
                catch (Exception ex)
                {
                    //设置错误信息
                    LocalCommonServices.ErrorTrace.SetErrorInfo(
                        FindForm(),
                        ex);
                }
            }
        }
    }
}
