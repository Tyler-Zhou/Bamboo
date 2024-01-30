using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Service;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.UI.WriteOff
{
    public partial class PaidSetExpenses : BaseListPart
    {
        public PaidSetExpenses()
        {
            InitializeComponent();
            Disposed += delegate {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (chargeIdFinder != null)
                {
                    chargeIdFinder.Dispose();
                    chargeIdFinder = null;
                }
                _rateList = null;
                dxErrorProvider1.DataSource = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 公司
        /// </summary>
        public Guid CompnayID
        {
            get;
            set;
        }


        /// <summary>
        /// 数据对象列表
        /// </summary>
        public List<WriteOffCharge> DataSourceList
        {
            get;
            set;
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        public SaveExpenseList ExpenseList
        {
            get;
            set;
        }
        #endregion

        #region 私有字段
        List<SolutionExchangeRateList> _rateList;
   
        #endregion

        #region 初始化事件

        private void PaidSetExpenses_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }

        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1110250001", LocalData.IsEnglish ? "Receive / give / pay the amount receivable does not agree, please input exchange or advance / pay" : "实收/付与应收/付金额不符,请录入汇兑损益或预收/付");

        }
        private IDisposable customerFinder, chargeIdFinder;
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
           labMessage.Text = NativeLanguageService.GetText(this, "1110250001");

            #region 绑定方向
           cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
           cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));
           #endregion

            #region 绑定币种
            List<SolutionCurrencyList> _currencyList = new List<SolutionCurrencyList>();
            _rateList = ConfigureService.GetCompanyExchangeRateList(CompnayID, true);

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(CompnayID);
            //找到解决方案
            if (configureInfo != null)
            {
                _currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
            }
            else
            {
                return;
            }
            //填充下拉框与币种信息
            foreach (SolutionCurrencyList currency in _currencyList)
            {
                cmbCurrencyID.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
            }
            #endregion

            #region 绑定会计科目 
            SolutionID = configureInfo.SolutionID;
            //List<SolutionGLCodeList> _gLList = ConfigureService.GetSolutionGLCodeList(configureInfo.SolutionID, true);

            //foreach (SolutionGLCodeList item in _gLList)
            //{
            //    string name = LocalData.IsEnglish ? item.EName : item.CName;
            //    this.cmbGL.Properties.Items.Add(new ImageComboBoxItem(name, item.ID));
            //}
            #endregion

            #region 客户搜索器

          customerFinder=  DataFindClientService.RegisterGridColumnFinder(colCustomerName
            , CommonFinderConstants.CustoemrFinder
            , "CustomerID"
            , "CustomerName"
            , "ID"
            , LocalData.IsEnglish ? "EName" : "CName");

            #endregion

           #region 会计科目搜索器
          chargeIdFinder = DataFindClientService.RegisterGridColumnFinder(colChargeID, FAMFinderConstants.GLCodeFinder,
           "GLID", "GLFullName", "ID", "GLCodeName", GetSolutionIDSearchCondition);
           #endregion

          #region 绑定列表数据

          bsList.DataSource = DataSourceList;
            bsList.ResetBindings(false);
            
            #endregion


        }
        public Guid SolutionID;
        SearchConditionCollection GetSolutionIDSearchCondition()
        {
            List<Guid> idList = new List<Guid>();
            idList.Add(CompnayID);

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", SolutionID, false);
            conditions.AddWithValue("CompanyIDs", idList, false);
            return conditions;
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
            gvMain.CloseEditor();

            if (!ValidateData())
            {
                return;
            }

            GetData();

            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();

        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            foreach (WriteOffCharge item in DataSourceList)
            {
                if (!item.Validate())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        private void GetData()
        {

            List<Guid> checkIDList = new List<Guid>();
            List<Guid?> customerIDList = new List<Guid?>();
            List<Int32> wayList = new List<int>();
            List<String> billNoList = new List<string>();
            List<Guid> glIDList = new List<Guid>();
            List<Guid> currencyIDList = new List<Guid>();
            List<Decimal> amountList = new List<decimal>();
            List<Decimal> rateList = new List<decimal>();
            List<String> remarkList = new List<string>();


            foreach (WriteOffCharge item in DataSourceList)
            {
                checkIDList.Add(item.CheckID);
                customerIDList.Add(item.CustomerID);
                wayList.Add(item.Way.GetHashCode());
                billNoList.Add(item.BillNo);
                glIDList.Add(item.GLID);
                currencyIDList.Add(item.CurrencyID);
                amountList.Add(item.Amount);
                rateList.Add(item.ExchangeRate);
                remarkList.Add(item.Remark);

            }

            ExpenseList = new SaveExpenseList();

            ExpenseList.CheckIDs = checkIDList.ToArray();
            ExpenseList.CustomerIDs = customerIDList.ToArray();
            ExpenseList.Ways = wayList.ToArray();
            ExpenseList.BillNos = billNoList.ToArray();
            ExpenseList.GLIDs = glIDList.ToArray();
            ExpenseList.CurrencyIDs = currencyIDList.ToArray();
            ExpenseList.Amounts = amountList.ToArray();
            ExpenseList.Rates = rateList.ToArray();
            ExpenseList.Remarks = remarkList.ToArray();
        }

        #endregion

        #region 关闭
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }
        #endregion

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            WriteOffCharge item = new WriteOffCharge();
            bsList.Add(item);
            bsList.ResetBindings(false);
        }


    }
}
