using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.CommonLibrary;
using System.Drawing;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Server;
using ICP.FAM.UI.LedgerList;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.UI.BankReceiptList
{
    [ToolboxItem(false)]
    public partial class BankReceiptListEditPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        IDataFindClientService dfService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public RateHelper RateHelperService
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        public IUserService sUserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        #endregion

        #region 变量、属性

        BankReceiptInfo bankReceiptInfo = new BankReceiptInfo();
        IDictionary<string, object> _values;
        private IDisposable customerFinder;


        #endregion


        #region 初始化
        bool isLoad = false;

        //protected override void OnLoad(EventArgs e)
        //{
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        base.OnLoad(e);
        //    }
        //}

        public BankReceiptListEditPart()
        {
            InitializeComponent();

            Disposed += delegate
            {
                SmartPartClosing -= BankReceiptEditPart_SmartPartClosing;
                bsDetails.DataSource = null;
                bsDetails.Dispose();
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                Saved = null;
            };
        }
        private void BankReceiptListEditPart_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitMessage();
                InitControls();

                SmartPartClosing += BankReceiptEditPart_SmartPartClosing;

                bankReceiptInfo.IsDirty = false;
            }
        }

        private void InitMessage()
        {
            RegisterMessage("1110100001", LocalData.IsEnglish ? "Input the detail data" : "请输入明细数据");
        }

        private void InitControls()
        {
            if (isLoad)
            {
                return;
            }
            SearchRegister();
            //Utility.BindComboBoxByCompany(cmbCompany);
            FAMUtility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
            });
            
            if (bankReceiptInfo.ID == Guid.Empty)
            {
                cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            }//默认
            else
            {
                cmbCompany.ShowSelectedValue(bankReceiptInfo.CompanyID, bankReceiptInfo.CompanyName);
                if (bankReceiptInfo.IsValid)
                {
                    barSave.Enabled = true;
                }
                else
                {
                    barSave.Enabled = false;
                }
            }
        }

        #region 关闭

        void BankReceiptEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (barSave.Enabled)
            {
                e.Cancel = !Leaving();
            }
        }

        public bool Leaving()
        {
            if (IsDirty)
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
        #endregion

        #endregion

        #region 注册搜索器
        /// <summary>
        /// 注册搜索器
        /// </summary>
        private void SearchRegister()
        {
            #region 客户搜索器

            //客户搜索器
            customerFinder = DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                  SearchFieldConstants.CustomerResultValue,
                        delegate(object inputSource, object[] resultData)
                        {
                            txtCustomer.Tag = bankReceiptInfo.CustomerID = new Guid(resultData[0].ToString());
                            txtCustomer.EditValue = bankReceiptInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                        }, delegate
                        {
                            txtCustomer.Text = string.Empty;
                            txtCustomer.Tag = Guid.Empty;
                        },
                        ClientConstants.MainWorkspace);
            #endregion
        }
        #endregion

        #region IEditPart Member

        private bool IsDirty
        {
            get
            {
                if (bankReceiptInfo.IsDirty)
                {
                    return true;
                }
                return false;
            }
        }

        public override object DataSource
        {
            get { return bsDetails.DataSource; }
            set { BindingData(value); }
        }


        void BindingData(object data)
        {
            if (data == null)
            {
                bankReceiptInfo = new BankReceiptInfo();
                bankReceiptInfo.ID = Guid.Empty;
                bankReceiptInfo.IsValid = true;
                bankReceiptInfo.Amount = 0.00M;
                bankReceiptInfo.Status = BankReceiptStatus.Created;
                bankReceiptInfo.CreateBy = LocalData.UserInfo.LoginID;
                bankReceiptInfo.CreateName = LocalData.UserInfo.LoginName;
                bankReceiptInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                bankReceiptInfo.IsDirty = false;

                bsDetails.DataSource = bankReceiptInfo;
            }
            else
            {
                bankReceiptInfo = FinanceService.GetBankReceiptInfo(((BankReceiptListInfo)data).ID, LocalData.IsEnglish);
                bankReceiptInfo.CancelEdit();
                bankReceiptInfo.BeginEdit();
                cmbCompany.Enabled = false;
                //bankReceiptInfo.IsDirty = true;
                bsDetails.DataSource = bankReceiptInfo;
                bsDetails.ResetBindings(false);
            }

        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            bsDetails.EndEdit();
            bankReceiptInfo.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        #region Save

        //public override void EndEdit()
        //{
        //    bsDetails.EndEdit();
        //}
        private bool Save()
        {
            EndEdit();
            BankReceiptInfo currentData = bsDetails.DataSource as BankReceiptInfo;
            BankReceiptSaveRequest saveRequest = new BankReceiptSaveRequest();
            //if (!IsDirty)
            //{
            //    return true;
            //}
            try
            {
                saveRequest.Id = currentData.ID;
                saveRequest.No = currentData.No;
                saveRequest.CompanyId = currentData.CompanyID;
                saveRequest.CustomerId = currentData.CustomerID;
                saveRequest.Amount = currentData.Amount;
                saveRequest.Status = currentData.Status;
                saveRequest.IsValid = currentData.IsValid;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.CreateDate = currentData.CreateDate;
                saveRequest.UpdateDate = currentData.UpdateDate;
                saveRequest.Remark = currentData.Remark;


                SingleResult result = FinanceService.SaveBankReceiptInfo(saveRequest);
                bankReceiptInfo.ID = result.GetValue<Guid>("ID");
                bankReceiptInfo.No = result.GetValue<String>("No");
                bankReceiptInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null)
                {
                    Saved(new object[] { bankReceiptInfo });
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                FindForm().Close();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region 工具栏按钮
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_Click(object sender, ItemClickEventArgs e)
        {
            Save();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion
    }
}
