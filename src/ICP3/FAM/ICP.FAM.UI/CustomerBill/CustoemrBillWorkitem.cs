using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;


namespace ICP.FAM.UI.CustomerBill
{
    public class CustomerBillWorkitem : WorkItem
    {
        #region Service

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        #region Show

        public void Show(OperationCommonInfo operationCommonInfo)
        {
            CustomerBillMainWorkspace CustomerBillMainSpce = Items.Get<CustomerBillMainWorkspace>("CustomerBillMainWorkspace");
            if (CustomerBillMainSpce == null)
            {
                CustomerBillMainSpce = Items.AddNew<CustomerBillMainWorkspace>("CustomerBillMainWorkspace");

                CustomerBillListPart CustomerBillMainListPart = Items.AddNew<CustomerBillListPart>();
                CustomerBillMainListPart.OperationType = operationCommonInfo.OperationType;
                IWorkspace listWorkspace = (IWorkspace)Workspaces[CustomerBillWorkSpaceConstants.ListWorkspace];
                if (listWorkspace == null) listWorkspace = Workspaces.AddNew<DeckWorkspace>(CustomerBillWorkSpaceConstants.ListWorkspace);
                listWorkspace.Show(CustomerBillMainListPart);

                CustomerBillEditPart CustomerBillEditPart = Items.AddNew<CustomerBillEditPart>();
                CustomerBillEditPart.OperationType = operationCommonInfo.OperationType;
                CustomerBillEditPart.DocumentState = operationCommonInfo.DocumentState;

                IWorkspace editWorkspace = (IWorkspace)Workspaces[CustomerBillWorkSpaceConstants.EditWorkspace];
                if (editWorkspace == null) editWorkspace = Workspaces.AddNew<DeckWorkspace>(CustomerBillWorkSpaceConstants.EditWorkspace);
                editWorkspace.Show(CustomerBillEditPart);

                //强制耦合
                CustomerBillMainListPart.CustomerBillEditPart = CustomerBillEditPart;
                CustomerBillEditPart.CustomerBillListPart = CustomerBillMainListPart;

                BulidConnection(operationCommonInfo, CustomerBillMainListPart, CustomerBillEditPart);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();

                smartPartInfo.Title = LocalData.IsEnglish ? "Customer Bill" : "业务帐单";
                if (operationCommonInfo.OperationNo.Length >= 4)
                    smartPartInfo.Title += "-" + operationCommonInfo.OperationNo.Substring(operationCommonInfo.OperationNo.Length - 4, 4);
                else
                    smartPartInfo.Title += "-" + operationCommonInfo.OperationNo;

                //CustomerBillMainSpce.KeyDown += new System.Windows.Forms.KeyEventHandler(mainPart_KeyDown);

                CustomerBillMainSpce.Disposed += delegate { Dispose(); };
                mainWorkspace.Show(CustomerBillMainSpce, smartPartInfo);

                CustomerBillMainListPart.Focus();
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(CustomerBillMainSpce);
            }
        }

        void mainPart_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == System.Windows.Forms.Keys.F2)
            //{
            //    CustomerBillListPart listWorkspace = (IWorkspace)this.Workspaces[CustomerBillWorkSpaceConstants.ListWorkspace] as CustomerBillListPart;
            //    listWorkspace.AddData(BillType.AR);
            //}
        }
        #endregion

        private void BulidConnection(OperationCommonInfo operationCommonInfo, CustomerBillListPart CustomerBillMainListPart, CustomerBillEditPart CustomerBillEditPart)
        {
            #region initValue

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(operationCommonInfo.CompanyID);
            List<SolutionExchangeRateList> ratList = ConfigureService.GetCompanyExchangeRateList(operationCommonInfo.CompanyID, true);
            List<SolutionCurrencyList> currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);

            Dictionary<string, object> keyValue = new Dictionary<string, object>();
            keyValue.Add("BLCommonInfo", operationCommonInfo);
            keyValue.Add("ConfigureInfo", configureInfo);
            keyValue.Add("SolutionExchangeRateList", ratList);
            keyValue.Add("SolutionCurrencyList", currencyList);
            AgentBillCheckStatusEnum abcStatues = FinanceService.GetABCStatue(operationCommonInfo.OperationID, LocalData.IsEnglish);
            keyValue.Add("AgentBillCheckStatus", abcStatues);

            CustomerBillMainListPart.Init(keyValue);

            FormData defaultFromData = null;
            #region 查找默认的FormData
            if (operationCommonInfo.Forms != null && operationCommonInfo.Forms.Count > 0)
            {
                if (operationCommonInfo.OperationType == OperationType.OceanExport || operationCommonInfo.OperationType == OperationType.AirExport || operationCommonInfo.OperationType == OperationType.Truck)
                {
                    #region	针对海运出口、空运出口，则默认值=打开帐单前所选的提单号（HBL/MBL）
                    if (operationCommonInfo.CurrentFormID.IsNullOrEmpty())
                    {
                        defaultFromData = defaultFromData = operationCommonInfo.Forms[0];
                    }
                    else
                    {
                        defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.ID == operationCommonInfo.CurrentFormID; });
                    }
                    #endregion
                }
                else if (operationCommonInfo.OperationType == OperationType.OceanImport || operationCommonInfo.OperationType == OperationType.AirImport)
                {
                    //北美区解决方案，取MBL
                    if (configureInfo.SolutionID == new Guid("2a254061-0465-4b07-81cf-e18198b45802"))
                    {
                        defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.MBL; });
                    }
                    else
                    {
                        #region	针对海运进口、空运进口，则默认值=如果业务包含HBL，则用第一个HBLNO；如果只包含MBL，则用MBLNO。
                        defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.HBL; });
                        if (defaultFromData == null)
                        {
                            defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.MBL; });
                        }
                        #endregion
                    }
                }
                else if (operationCommonInfo.OperationType == OperationType.Other)
                {
                    #region 针对其他业务，默认值为第一个HBLNO,没有则用MBLNO，再没有则用业务号

                    //defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.Booking; });
                    defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.HBL; });
                    if (defaultFromData == null)
                    {
                        defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.MBL; });
                    }

                    if (defaultFromData == null)
                    {
                        defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.Booking; });
                    }
                    #endregion
                }
                else if (operationCommonInfo.OperationType == OperationType.Internal)
                {
                    defaultFromData = operationCommonInfo.Forms.Find(delegate(FormData f) { return f.Type == FormType.Booking; });
                }
            }
            #endregion
            keyValue.Add("DefaultFromData", defaultFromData);
            CustomerBillEditPart.Init(keyValue);

            #endregion

            #region CurrentChanging
            CustomerBillMainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                BillInfo info = CustomerBillEditPart.DataSource as BillInfo;
                if (info != null && (short)info.State <= (short)BillState.Created && CustomerBillEditPart.barSave.Enabled)
                {

                    UIConnectionHelper.ParentChangingForEditPart(CustomerBillMainListPart
                                                                , CustomerBillEditPart.SaveData
                                                                , (CustomerBillEditPart.DataSource as BillInfo)
                                                                , e
                                                                , LocalData.IsEnglish ? "CustomerBill Edit" : "编辑帐单");
                }
            };
            #endregion

            #region CustomerBillMainListPart.CurrentChanged
            CustomerBillMainListPart.CurrentChanged += delegate(object sender, object data)
            {
                BillList listData = data as BillList;
                BillInfo info = null;

                if (listData != null)
                {
                    if (listData.IsNew)
                    {
                        info = new BillInfo();
                        FAMUtility.CopyToValue(listData, info, typeof(BillList));
                    }
                    else
                    {
                        info = FinanceService.GetBillInfo(listData.ID);
                    }

                    info.AgentID = operationCommonInfo.AgentID;
                    info.OperationType = operationCommonInfo.OperationType;
                }
                CustomerBillEditPart.DataSource = info;
                CustomerBillEditPart.setEmai();

            };
            #endregion

            #region CustomerBillEditPart.Saved
            CustomerBillEditPart.Saved += delegate(object[] prams)
            {
                if (CustomerBillMainListPart.Current == null || prams[0] == null) return;

                Guid Id = prams[0].ToString().ToGuid();
                if (Id.IsNullOrEmpty()) return;
                BillList result = FinanceService.GetBillListByIDs(new Guid[] { Id })[0];
                if (result == null) return;
                CustomerBillMainListPart.Refresh(result);
            };

            #endregion

            List<BillList> billList = FinanceService.GetBillListByOperactioID(operationCommonInfo.OperationID);
            CustomerBillMainListPart.DataSource = billList;
        }
    }

    public class CustomerBillWorkSpaceConstants
    {
        public const string EditWorkspace = "EditWorkspace";
        public const string ListWorkspace = "ListWorkspace";
    }

    public class CustomerBillCommands
    {
        public const string Commond_PayoffWF = "Commond_PayoffWF";
        public const string Commond_Deficit = "Commond_Deficit";
        /// <summary>
        /// 额外支付
        /// </summary>
        public const string Commond_AdditionalWF = "Commond_AdditionalWF";
    }

    /// <summary>
    /// 刷新下载列表文档状态服务
    /// </summary>
    public class RefereshBillService
    {
        ///// <summary>
        ///// 刷新下载列表文档状态事件
        ///// </summary>
        //public Action Referesh;

        /// <summary>
        /// 刷新删除账单菜单状态事件
        /// </summary>
        public Action<bool> RefereshBillDelete;

        //  public Action DocumentError;
    }
}
