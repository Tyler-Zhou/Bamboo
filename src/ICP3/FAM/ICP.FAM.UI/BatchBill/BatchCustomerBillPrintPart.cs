using ICP.Common.UI.ReportView;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 打印批量账单
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BatchCustomerBillPrintPart : CompositeReportViewPart
    {
        #region Fields
        #endregion

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        
       
        #endregion

        #region Property

        #endregion

        #region Init
        /// <summary>
        /// 打印提单
        /// </summary>
        public BatchCustomerBillPrintPart()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Locale()
        {
            base.Locale();
            if (!LocalData.IsEnglish)
            {
            }
            Disposed += (sender,arg)=>
            {
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            };
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        protected override void LoadData()
        {
            InitControls();
        } 
        #endregion

        #region Controls Event
        #endregion

        #region Custom Method
        /// <summary>
        /// 查询
        /// </summary>
        protected override void Query()
        {
            Print();
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            btnQuery.Enabled = false;
            try
            {
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            finally { btnQuery.Enabled = true; }
        }
        private void InitControls()
        {
        }

        /// <summary>
        /// 打印后
        /// 1.更改提单状态
        /// </summary>
        protected override void AfterPrint()
        {
            try
            {
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion

    }
}
