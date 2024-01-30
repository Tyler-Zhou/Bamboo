using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;

using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.WriteOff
{
    [ToolboxItem(false)]
    public partial class CurrencyRateEditor : BaseEditPart
    {


        public CurrencyRateEditor()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        #region 属性
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID
        {
            get;
            set;
        }

        /// <summary>
        /// 币种名称
        /// </summary>
        public String CurrencyName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public List<SolutionExchangeRateList> DataSourceList
        {
            get
            {
                List<SolutionExchangeRateList> list = bsList.DataSource as List<SolutionExchangeRateList>;
                if (list == null)
                {
                    list = new List<SolutionExchangeRateList>();
                }
                return list;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
            }
        }


        #endregion

        #region 初始化
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
            RegisterMessage("1108040001",LocalData.IsEnglish?"The Rate could not less than zero": "汇率必须不能小于等于0");
        }
        private void InitControls()
        {
            txtCurrencyName.Text = CurrencyName;
        }

        #endregion

        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region 确定
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }



            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            foreach (SolutionExchangeRateList item in DataSourceList)
            {
                if (item.Rate == 0)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108040001"));
                    return false;
                }
            }

            return true;
        }
        #endregion


    }
}
