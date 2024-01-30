using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;

namespace ICP.FAM.UI.BankTransaction.Finder
{
    public class BankTransactionFinder : IDataFinder, IDisposable
    {
        /// <summary>
        /// IDataFinder 成员
        /// </summary>
        public bool IsBusy { get; set; }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 财务服务
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        private const string CustomerFinderWorkspace = "BankTransactionFinderWorkspace";

        #region ChildWorkitem

        SingleFinderWorkitem singleFinderWorkitem = null;
        //CustomerMultiFinderWorkitem multiFinderWorkitem = null;

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = Workitem.Workspaces.Get<DeckWorkspace>(CustomerFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (Workitem.Workspaces.Contains(CustomerFinderWorkspace))
                {
                    Workitem.Workspaces.Remove(workspce);
                }

                workspce = Workitem.Workspaces.AddNew<DeckWorkspace>(CustomerFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, conditions, returnFields, triggerType, CustomerFinderWorkspace);
        }

        public void PickOne(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ClientConstants.MainWorkspace;

            IWorkspace workspace = Workitem.Workspaces[workspaceName];

            #region 查询数据

            string inputValue = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
                inputValue = searchValue;

            Guid companyID = Guid.Empty;
            Guid bankAccountID = Guid.Empty;
            string debitCreditFlag = string.Empty;
            if (conditions != null)
            {
                SearchCondition companyCondition = conditions.GetValue("CompanyID");
                if (companyCondition != null)
                {
                    companyID = new Guid("" + companyCondition.Value);
                }

                SearchCondition bankAccountIDCondition = conditions.GetValue("BankAccountID");
                if (bankAccountIDCondition != null)
                {
                    bankAccountID = new Guid("" + bankAccountIDCondition.Value);
                }
                SearchCondition debitCreditFlagCondition = conditions.GetValue("DebitCreditFlag");
                if (debitCreditFlagCondition != null)
                {
                    debitCreditFlag = "" + debitCreditFlagCondition.Value;
                }
            }

            List<BankTransactionInfo> list;
            if (triggerType == FinderTriggerType.ClickButton && string.IsNullOrEmpty(inputValue))
            {
                //单击选择按钮时，如果没有输入查询时，则不进行搜索。只弹出查询对话框。
                list = new List<BankTransactionInfo>();
            }
            else
            {
                BankTransactionSearchParameter requestParameter = new BankTransactionSearchParameter()
                {
                    CompanyID = companyID,
                    BankAccountID = bankAccountID,
                    BusinessNO = inputValue,
                    DebitCreditFlag = debitCreditFlag,
                    MinimumAmount = 0.00m,
                    MaximumAmount = 0.00m,
                    Queryer = LocalData.UserInfo.LoginID,
                };
                list = FinanceService.GetTransList(requestParameter);
            }

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(Framework.ClientComponents.Controls.Utility.GetSingleFinderResult(list[0], returnFields)));

                return;
            }

            #endregion

            singleFinderWorkitem = Workitem.WorkItems.Get<SingleFinderWorkitem>(GetHashCode().ToString() + "SingleFinderWorkitem");
            if (singleFinderWorkitem == null)
            {
                singleFinderWorkitem = Workitem.WorkItems.AddNew<SingleFinderWorkitem>(GetHashCode().ToString() + "SingleFinderWorkitem");
                singleFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("INPUTVALUE", inputValue);
            initValues.Add("COMPANYID", companyID);
            initValues.Add("BANKACCOUNTID", bankAccountID);
            initValues.Add("DEBITCREDITFLAG", debitCreditFlag);
            singleFinderWorkitem.Show(workspace, list, returnFields, initValues);
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions
            , string[] returnFields, FinderTriggerType triggerType
            , GetExistValueHandler getExistValueHandler, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = Workitem.Workspaces.Get<DeckWorkspace>(CustomerFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                workspce = Workitem.Workspaces.AddNew<DeckWorkspace>(CustomerFinderWorkspace);
                container.Controls.Add(workspce);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            PickMany(searchValue, property, conditions, returnFields, triggerType, getExistValueHandler, CustomerFinderWorkspace);
        }

        public void PickMany(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, string workspaceName)
        {
            
        }

        #endregion

        #endregion

        #region 属性

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(Boolean isDisposing)
        {
            if (isDisposing)
            {
                DataChoosed = null;
                singleFinderWorkitem = null;
                //this.multiFinderWorkitem = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            }
        }

        #endregion
    }
}
