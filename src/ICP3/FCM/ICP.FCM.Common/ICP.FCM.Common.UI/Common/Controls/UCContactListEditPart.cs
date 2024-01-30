

/*************
 *页面说明：    弹出的用户联系人添加界面
 *实现功能：    实现业务联系人的添加
 *创建时间：    2013-11-11 
 *创建人:       wlj
 * *********************/

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.Business.Common.UI.Contact;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FCM.Common.UI
{
    /// <summary>
    /// 联系人列表编辑框
    /// </summary>
    public partial class UCContactListEditPart : XtraUserControl
    {
        #region  服务和参数

        public WorkItem Workitem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        private UCCustomerList ucCustomerList;
        /// <summary>
        /// 业务上下文
        /// </summary>
        public BusinessOperationContext operationContext
        {
            get;
            set;
        }
        ContactObjects contactSoure = null;
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 需要获取的客户所属的沟通阶段
        /// </summary>
        public ContactStage TargetStage { get; set; }
        /// <summary>
        /// 回传的信息集合
        /// </summary>
        public List<CustomerCarrierObjects> Customer { get; set; }

        /// <summary>
        /// 是否需要返回数据
        /// </summary>
        public bool Isreturnvalue { get; set; }

        /// <summary>
        /// 海出方法类
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        /// <summary>
        /// 海进方法类
        /// </summary>
        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }
        }

        /// <summary>
        /// 需要执行的方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 执行参数集合
        /// </summary>
        public object[] ParameterCollection { get; set; }

        #endregion

        /// <summary>
        /// 联系人列表编辑框
        /// </summary>
        public UCContactListEditPart()
        {
            InitializeComponent();
            Load += (sender, e) =>
                {
                    LoadControl();
                    SetCnText();
                    InnerBindData();
                };
            Disposed += (sender, e) =>
                {
                    if (Workitem != null)
                    {
                        if (ucCustomerList != null)
                        {
                            Workitem.Items.Remove(ucCustomerList);
                            ucCustomerList.Dispose();
                            ucCustomerList = null;
                        }
                        Workitem.Items.Remove(this);
                    }
                    operationContext = null;
                };
        }
        /// <summary>
        /// 加载控件
        /// </summary>
        private void LoadControl()
        {
            //加载客户列表界面
            ucCustomerList = Workitem.Items.AddNew<UCCustomerList>();
            ucCustomerList.OperationContext = operationContext;
            ucCustomerList.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(ucCustomerList);
        }
        /// <summary>
        ///  本地化
        /// </summary>
        private void SetCnText()
        {
            if (!LocalData.IsEnglish)
            {
                barAdd.Caption = "新增";
                barRemove.Caption = "删除";
                barSave.Caption = "保存";
            }
        }



        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            ucCustomerList.ContactStage = TargetStage;
            ucCustomerList.AddNewDataRecord();
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ucCustomerList.Current != null)
            {
                DialogResult dlg =
                    XtraMessageBox.Show(
                        LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?",
                        LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlg != DialogResult.OK)
                {
                    return;
                }
                var varEvent = ucCustomerList.Current as CustomerCarrierObjects;
                if (varEvent.Id != Guid.Empty)
                {
                    FCMCommonService.RemoveContactInfo(varEvent.Id, LocalData.UserInfo.LoginID, varEvent.UpdateDate);
                }
                ucCustomerList.RemoveCurrent();
                ucCustomerList.MoveFirst();
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ucCustomerList.ValidateData() == false)
            {
                return;
            }
            ucCustomerList.ContactStage = TargetStage;
            ucCustomerList.Save();
            ServiceInterface.Utility.GetStageName(TargetStage);
            FindForm().Close();
            ExecutionMethod();
        }
        /// <summary>
        /// 执行对应的方法(可使用反射的方式来调用方法，但是由于参数的不统一，目前并不好解决)
        /// </summary>
        public void ExecutionMethod()
        {
            switch (MethodName)
            {
                case "MailCustomerForCancelBooking":
                    ClientOceanExportService.MailCustomerForCancelBooking(bool.Parse(ParameterCollection[0].ToString()), (OceanBookingInfo)ParameterCollection[1]);
                    break;
                case "MailCustomerAskForConfirmSI":
                    ClientOceanExportService.MailCustomerAskForConfirmSI(bool.Parse(ParameterCollection[0].ToString()), new Guid(ParameterCollection[1].ToString()), (OceanHBLInfo)ParameterCollection[2], (OceanMBLInfo)ParameterCollection[3]);
                    break;
                case "MailCustomerAskForSi":
                    ClientOceanExportService.MailCustomerAskForSi(bool.Parse(ParameterCollection[0].ToString()), (OceanBookingInfo)ParameterCollection[1]);
                    break;
                case "MailCustomerForSoFailure":
                    ClientOceanExportService.MailCustomerForSoFailure(bool.Parse(ParameterCollection[0].ToString()), (OceanBookingInfo)ParameterCollection[1]);
                    break;
                case "MailSoCopyToCustomer":
                    ClientOceanExportService.MailSoCopyToCustomer(bool.Parse(ParameterCollection[0].ToString()), (OceanBookingInfo)ParameterCollection[1]);
                    break;
                case "MailSoConfirmationToCustomer":
                    OceanBookingInfo oceanBooking = (OceanBookingInfo)ParameterCollection[1];
                    ClientOceanExportService.MailSoConfirmationToCustomer(oceanBooking.ID, bool.Parse(ParameterCollection[0].ToString()));
                    break;
                case "MailSoConfirmationToAgent":
                    ClientOceanExportService.MailSoConfirmationToAgent(new Guid(ParameterCollection[0].ToString()));
                    break;
                case "MailAnToCustomer":
                    ClientOceanImportService.MailAnToCustomer(new Guid(ParameterCollection[0].ToString()), bool.Parse(ParameterCollection[1].ToString()));
                    break;
                case "MailPickUpToCustomer":
                    ClientOceanImportService.MailPickUpToCustomer(new Guid(ParameterCollection[0].ToString()), bool.Parse(ParameterCollection[1].ToString()));
                    break;
                case "PayNtMailSend":
                    ClientOceanImportService.PayNtMail(new Guid(ParameterCollection[0].ToString()));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void InnerBindData()
        {
            ucCustomerList.Type = ContactType.Customer;
            ucCustomerList.OperationContext = operationContext;
            if (operationContext.OperationID == Guid.Empty)
            {
                
                ucCustomerList.DataSource = new List<CustomerCarrierObjects>();
            }
            else
            {
                contactSoure = FCMCommonService.GetContactList(operationContext.OperationID,
                                                               operationContext.OperationType);
                if (contactSoure != null)
                {
                    ucCustomerList.DataSource = contactSoure.CustomerCarrier;
                }
                else
                {
                    ucCustomerList.DataSource = new List<CustomerCarrierObjects>();
                }
            }
        }
    }
}
