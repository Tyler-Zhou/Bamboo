using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 批量账单工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class BatchCustomerBillToolBarPart : BaseToolBar
    {
        #region Fields
        /// <summary>
        /// 按钮字典
        /// </summary>
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        #endregion
        
        #region Service

        #region WorkItem
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        } 
        #endregion

        #endregion

        #region Init
        /// <summary>
        /// 批量账单工具栏
        /// </summary>
        public BatchCustomerBillToolBarPart()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            BulidCommond();
            Disposed += (sender,arg)=>
            {
                _barItemDic.Clear();
                _barItemDic = null;
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
            InitControls();
            if (!LocalData.IsDesignMode)
            {
                if (!LocalData.IsEnglish)
                {
                    SetCnText();
                }
            }
        } 
        #endregion

        #region Custom Method

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
           
        }
        /// <summary>
        /// 设置中文
        /// </summary>
        void SetCnText()
        {
            
        }
        /// <summary>
        /// 构建按钮字典
        /// </summary>
        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        /// <summary>
        /// 构建命令行
        /// </summary>
        private void BulidCommond()
        {
            barAddBatchBill.ItemClick += delegate { Workitem.Commands[BatchBillCommandConstants.Command_AddData].Execute(); };
            barPrintBill.ItemClick += delegate { Workitem.Commands[BatchBillCommandConstants.Command_Print].Execute(); };
            barClose.ItemClick += delegate
            {
                var findForm = FindForm();
                if (findForm != null) findForm.Close();
            };
        }
        #endregion
    }
}
