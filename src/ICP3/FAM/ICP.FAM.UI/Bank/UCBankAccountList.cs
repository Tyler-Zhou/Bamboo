using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class UCBankAccountList : BaseListPart
    {
        public UCBankAccountList()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsAccount.PositionChanged -= bsAccount_PositionChanged;
                bsAccount.DataSource = null;
                CurrentChanged = null;
                CurrentChanging = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 服务

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 窗体类型
        /// </summary>
        public string FormType
        {
            get;
            set;
        }
        /// <summary>
        /// 银行ID
        /// </summary>
        public Guid BankID
        {
            get;
            set;
        }
        #endregion
         
        #region  初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }

        private void InitMessage()
        {
            RegisterMessage("1108100001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据？");
            RegisterMessage("1108100002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据？");
        }

        private void InitControls()
        {
            if (FormType == "List")
            {
                bar2.Visible = false;
                gvMain.OptionsBehavior.Editable = false;

                colCurrency.Visible = false;
                colGLID.Visible = false;
            }
            else
            {
                colCurrency.Visible = true;
                colGLID.Visible = true;
            }
        }

        /// <summary>
        /// 绑定明细数据
        /// </summary>
        public void BindDataList()
        {
            List<BankAccountList> list = null;
            if (BankID != null && BankID != Guid.Empty)
            {
                list = FinanceService.GetBankAccountList(BankID, LocalData.IsEnglish);
            }

            bsAccount.DataSource = list;
            bsAccount.ResetBindings(false);
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsAccount.Current; }
        }

        protected BankAccountList CurrentRow
        {
            get { return Current as BankAccountList; }
        }

        public override object DataSource
        {
            get
            {
                return bsAccount.DataSource;
            }
            set
            {
                bsAccount.DataSource = value;
                bsAccount.ResetBindings(false);
            }
        }

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;


        #endregion

        /// <summary>
        /// 新增一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
                BankAccountList backAccont = new BankAccountList();
                backAccont.CreateByName = LocalData.UserInfo.LoginName;
                backAccont.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);


                bsAccount.Insert(0, backAccont);
        }
        /// <summary>
        /// 删除当前账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow != null)
            {
                if (CurrentRow.IsValid)
                {
                    if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100001")))
                    {
                        return;
                    }
                }
                else
                {
                    if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1108100002")))
                    {
                        return;
                    }
                }

                string failureMessage = string.Empty;
                if (CurrentRow.IsValid)
                    failureMessage = LocalData.IsEnglish ? "Cancel Booking State Failed." : "作废银行信息失败.";
                else
                    failureMessage = LocalData.IsEnglish ? "Available Booking State Failed." : "激活银行信息失败.";

                try
                {
                    SingleResult result = FinanceService.ChangeBankAccountValidity(CurrentRow.ID,
                        CurrentRow.IsValid,
                        LocalData.UserInfo.LoginID,
                        CurrentRow.UpdateDate,
                        LocalData.IsEnglish);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    if (CurrentRow.IsValid)
                    {
                        barDelete.Caption = LocalData.IsEnglish ? "Invalid" : "作废";
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Booking Successfully." : "账号信息已经成功作废.");
                    }
                    else
                    {
                        barDelete.Caption = LocalData.IsEnglish ? "Activation" : "激活";
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Available Booking Successfully." : "账号信息已经成功激活.");
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), failureMessage + ex.Message);
                }
            }

        }
        private void bsAccount_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                barDelete.Enabled = false;
            }
            else
            {
                barDelete.Enabled = true;
                if (CurrentRow.IsValid)
                {
                    barDelete.Caption = LocalData.IsEnglish ? "Invalid" : "作废";
                }
                else
                {
                    barDelete.Caption = LocalData.IsEnglish ? "Activation" : "激活";
                }
            }
        }

    }
}
