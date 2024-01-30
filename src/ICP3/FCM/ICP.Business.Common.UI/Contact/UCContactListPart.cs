using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Threading;

namespace ICP.Business.Common.UI.Contact
{
    public partial class UCContactListPart : DevExpress.XtraEditors.XtraUserControl, IDataBind
    {
        #region 服务

        public WorkItem Workitem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

      
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessOperationService>();
            }
        }


        private UCCustomerList ucCustomerList;
        private UCContactStaffListPart ucContactStaffListPart;

        BusinessOperationContext operationContext = null;
        ContactObjects contactSoure = null;


        CustomerCarrierParam _customerCarrierParm
        {
            get { return new CustomerCarrierParam(); }
        }

        bool isLoad = false;


        private delegate void BindDataDelegate();

        #endregion

        public UCContactListPart()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode && !isLoad)
            {



                this.Load += (sender, e) =>
                {

                    LoadControl();

                };
                if (!LocalData.IsEnglish)
                {
                    SetCnText();
                }
            }

            this.Disposed += delegate
            {

                if (this.Workitem != null)
                {
                    if (this.ucContactStaffListPart != null)
                    {
                        this.Workitem.Items.Remove(this.ucContactStaffListPart);
                        this.ucContactStaffListPart.Dispose();
                        this.ucContactStaffListPart = null;
                    }
                    if (this.ucCustomerList != null)
                    {
                        this.Workitem.Items.Remove(this.ucCustomerList);
                        this.ucCustomerList.Dispose();
                        this.ucCustomerList = null;
                    }
                    this.Workitem.Items.Remove(this);
                }
                this.operationContext = null;
                this.contactSoure = null;
            };
        }
        private void SetCnText()
        {
            barNewCustomer.Caption = "新增客户"; //客户/承运人
            barNewStaff.Caption = "新增参与人";
            barRemove.Caption = "移除";
            barSave.Caption = "保存";
            barRemoveCustomer.Caption = "客户";  //客户/承运人
            barRemoveStaff.Caption = "参与人";
        }

        void LoadControl()
        {
            try
            {
                //加载客户列表界面
                ucCustomerList = Workitem.Items.AddNew<UCCustomerList>();
                ucCustomerList.Dock = DockStyle.Fill;
                pnlStaff.Panel1.Controls.Clear();
                pnlStaff.Panel1.Controls.Add(ucCustomerList);

                //加载员工列表面板
                ucContactStaffListPart = Workitem.Items.AddNew<UCContactStaffListPart>();
                ucContactStaffListPart.Dock = DockStyle.Fill;
                pnlStaff.Panel2.Controls.Clear();
                pnlStaff.Panel2.Controls.Add(ucContactStaffListPart);

                isLoad = true;
            }
            catch (Exception)
            {
                    
                throw;
            }
          
        }

     

        /// <summary>
        /// 数据源
        /// </summary>
        public object DataSource
        {
            set
            {
                SetDataSource(value);
                if (!isLoad)
                {
                    LoadControl();
                }
                InnerLoad();
            }
        }


        private void InnerLoad()
        {
            if (operationContext != null)
            {
                WaitCallback callback = (data) =>
                {
                    try
                    {
                        BindDataDelegate bindDelegate = new BindDataDelegate(InnerBindData);
                        this.Invoke(bindDelegate);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                    }
                };
                ThreadPool.QueueUserWorkItem(callback);
            }
        }

        private void InnerBindData()
        {
            if (ucCustomerList == null)
            {
                ucCustomerList=new UCCustomerList();
            }
            if (ucContactStaffListPart == null)
            {
                ucContactStaffListPart=new UCContactStaffListPart();
            }
            this.ucCustomerList.OperationContext = this.operationContext;
            if (operationContext.OperationID == Guid.Empty)
            {
                this.ucCustomerList.Type = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
                this.ucCustomerList.DataSource = new List<CustomerCarrierObjects>();
                this.ucContactStaffListPart.DataSource = new List<StaffObjects>();
            }

            else
            {
                contactSoure = FCMCommonService.GetContactList(operationContext.OperationID, operationContext.OperationType);
                this.ucCustomerList.Type = ICP.Framework.CommonLibrary.Common.ContactType.Customer;
                if (contactSoure != null)
                {
                    this.ucCustomerList.DataSource = contactSoure.CustomerCarrier;
                    this.ucContactStaffListPart.DataSource = contactSoure.StaffList;
                }
                else
                {
                    this.ucCustomerList.DataSource = new List<CustomerCarrierObjects>();
                }
                List<StaffObjects> dataList = FCMCommonService.GetAssistantList(Guid.Empty, operationContext.OperationID,
                                                     operationContext.OperationType);
                if (dataList != null && dataList.Count > 0)
                {
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        this.ucContactStaffListPart.Insert(dataList[i]);
                    }
                    //初始化参与者列表控件数据
                    ucContactStaffListPart.InitControls(operationContext.OperationID);
                }
            }
            SetToolEnabled(true);

            SetStageComboBoxItems(this.operationContext.OperationType);
        }

        public void SetStageComboBoxItems(OperationType operationType)
        {
            this.reSelectBox.Items.Clear();
            List<ContactStageInfo> list = ICP.FCM.Common.ServiceInterface.FCMInterfaceUtility.GetStageInfoSource(operationType);
            foreach (ContactStageInfo item in list)
            {
                reSelectBox.Items.Add(item.StageName, false);
            }
        }

        /// <summary>
        /// 设置列表数据源
        /// </summary>
        public void SetDataSource(object value)
        {
            operationContext = value as BusinessOperationContext;
        }

        private void SetToolEnabled(bool value)
        {
            barNewStaff.Enabled = barRemove.Enabled = barSave.Enabled = barNewCustomer.Enabled = value;
        }


        private void barNewStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (operationContext != null)
            {
                ucContactStaffListPart.Insert(ucContactStaffListPart.CreateStaffInfo(operationContext.OperationID, operationContext.OperationType, null, null));
                ucContactStaffListPart.InitControls(operationContext.OperationID);
            }
        }
        private void barRemoveStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            StaffObjects selectRow = ucContactStaffListPart.CurrentRow;
            if (selectRow != null)
            {
                if (selectRow.IsDirty)
                    ucContactStaffListPart.RemoveCurrent();
                else
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Can't remove the current select row." : "当前选择的参与人不能删除."));
            }
        }
        /// <summary>
        /// 移除当前项
        /// </summary>
        /// <param name="value"></param>
        private void Remove(object value)
        {
            if (value == null) return;
            UCCustomerList UCCommon = value as UCCustomerList;
            try
            {
                if (UCCommon.Current != null)
                {
                    DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dlg != DialogResult.OK)
                    {
                        return;
                    }
                    var varEvent = UCCommon.Current as CustomerCarrierObjects;
                    if (varEvent.Id != Guid.Empty)
                    {
                        FCMCommonService.RemoveContactInfo(varEvent.Id, LocalData.UserInfo.LoginID, varEvent.UpdateDate);
                    }
                    UCCommon.RemoveCurrent();
                    UCCommon.MoveFirst();
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "List Data is null." : "列表数据为空！");

                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private List<bool> CheckOperationType(OperationType operationType, int count)
        {
            bool value = false;
            if (this.operationContext.OperationType == operationType)
            {
                value = true;

            }
            List<bool> items = new List<bool>();
            for (int i = 0; i < count; i++)
            {
                items.Add(value);
            }
            return items;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ucCustomerList.ValidateData() == false)
                {
                    return;
                }
                if (this.ucCustomerList.IsChanged)
                {   

                    this.ucCustomerList.Save();
                    ClientBusinessOperationService.UpdateLocalBusinessData(this.operationContext.OperationID, this.operationContext.OperationType);
                }
                Workitem.State["CurrentMessage"] = null;
                Workitem.Commands["Command_Unknown_AssistantListSubmit"].Execute();
                //提示保存成功
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save the data successfully." : "保存数据成功!");
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }

        }

        /*验证表单数据*/
        private bool ValidateData(List<CustomerCarrierObjects> ListObject)
        {
            foreach (CustomerCarrierObjects c in ListObject)
            {
                if (c.Validate() == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 客户列表添加一列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucCustomerList.AddNewDataRecord();
        }


        /// <summary>
        /// 移除客户列表当前选择列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemoveCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Remove(ucCustomerList);
        }

        #region IDataBind 成员

        public void DataBind(BusinessOperationContext business)
        {
            //var memo = new BusinessOperationContext
            //{
            //    FormID = business.FormId,
            //    FormType = business.FormType,
            //    OperationId = business.OperationID,
            //    OperationType = business.OperationType,
            //    State = (DocumentState)business.State
            //};
            operationContext = business;
            InnerBindData();
        }
        public void ControlsReadOnly(bool flg)
        {
            SetToolEnabled(flg);
        }
        #endregion

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            if (barStage.EditValue == null)
            {
                return;
            }

            string strstage = barStage.EditValue.ToString();
            foreach (var item in ucCustomerList.DataSourceList)
            {
                item.Stage = strstage;
                item.IsDirty = true;
            }

            ucCustomerList.ResetBindings();
        }

    }
}
