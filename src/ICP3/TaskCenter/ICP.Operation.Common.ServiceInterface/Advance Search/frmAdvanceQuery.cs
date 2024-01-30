using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.Business.UI;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务高级查询界面
    /// </summary>
    public partial class frmAdvanceQuery : XtraForm
    {
        /// <summary>
        /// 
        /// </summary>
        public EventHandler<CommonEventArgs<QueryInfo>> AdvanceQuery;

        /// <summary>
        /// 
        /// </summary>
        private string businessTypeKey = Constants.BusinessTypeKey;
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _InitValues;

        #region 服务注入
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        /// <summary>
        /// 操作代理服务
        /// </summary>
        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }

        /// <summary>
        /// 当前查询面板
        /// </summary>
        private IBusinessQueryPart CurrentQueryPart
        {
            get
            {
                if (pnlBusinessQueryPart.Controls.Count <= 0)
                    return null;
                if (!(pnlBusinessQueryPart.Controls[0] is IBusinessQueryPart))
                    return null;
                return pnlBusinessQueryPart.Controls[0] as IBusinessQueryPart;
            }
        }

        /// <summary>
        /// 高级查询字符串
        /// </summary>
        public string AdvanceQueryString
        {
            get;
            set;
        } 
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmAdvanceQuery()
        {
            InitializeComponent();
            setLanguage(LocalData.IsEnglish);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            CurrentQueryPart.Reset();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                QueryInfo queryInfo = GetQueryInfo();
                if (AdvanceQuery != null)
                {
                    Close();
                    AdvanceQuery(null, new CommonEventArgs<QueryInfo>(queryInfo));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnQuery=>Advanced Query" + Environment.NewLine + ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                _InitValues[businessTypeKey] = "OE";
                OperationType = OperationType.OceanExport;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                _InitValues[businessTypeKey] = "OI";
                OperationType = OperationType.OceanImport;
            }

            IBusinessQueryPart queryPart = GetPart();
            queryPart.Locale();
            queryPart.LastAdvanceQueryString(_InitValues);
            UserControl uc = queryPart as UserControl;
            uc.Dock = DockStyle.Fill;
            pnlBusinessQueryPart.Controls.Clear();
            pnlBusinessQueryPart.Controls.Add(uc);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (AdvanceQuery != null)
            {
                Close();
            }
        }
        /// <summary>
        /// 窗体接收键盘按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAdvanceQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryInfo queryInfo = GetQueryInfo();
                if (AdvanceQuery != null)
                {
                    Close();
                    AdvanceQuery(null, new CommonEventArgs<QueryInfo>(queryInfo));
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        /// <summary>
        /// 设置中英文
        /// </summary>
        /// <param name="isEnglish"></param>
        private void setLanguage(bool isEnglish)
        {
            if (isEnglish)
            {
                Text = "Advenced Search";
                btnQuery.Text = "Search(&Q)";
                btnReset.Text = "&Reset";
                btnCancel.Text = "&Cancel";
                radioGroup1.Properties.Items[0].Description = "Ocean Export";
                radioGroup1.Properties.Items[1].Description = "Ocean Import";
                //this.radioGroup1.Properties.Items[2].Description = "Work Flow";
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initValues"></param>
        public void Init(Dictionary<string, object> initValues)
        {
            _InitValues = initValues;
            InnerInit();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InnerInit()
        {
            if (CurrentQueryPart != null)
            {
                CurrentQueryPart.Init(_InitValues);
                CurrentQueryPart.Reset();

            }
            else
            {

                IBusinessQueryPart queryPart = GetPart();
                queryPart.Locale();
                queryPart.LastAdvanceQueryString(_InitValues);

                UserControl uc = queryPart as UserControl;

                Height = uc.Height + 100;


                uc.Dock = DockStyle.Fill;
                pnlBusinessQueryPart.Controls.Add(uc);
                DefaultSetRadioGroupByOperationType();

              
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void DefaultSetRadioGroupByOperationType()
        {
            if (OperationType == OperationType.OceanImport)
            {
                radioGroup1.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IBusinessQueryPart GetPart()
        {
            string typeName = _InitValues[businessTypeKey].ToString();
            int IsShowOperationType = 1;

            if (_InitValues.Keys.Contains(Constants.ShowOperationType))
            {
                IsShowOperationType = (int)_InitValues[Constants.ShowOperationType];
            }
            panelControl1.Visible = IsShowOperationType > 0;

            BusinessType type = (BusinessType)Enum.Parse(typeof(BusinessType), typeName, true);

            if (IsShowOperationType > 0)
            {
                List<OperationType> userOperationTypes = OperationAgentService.GetUserOperationViewType(LocalData.UserInfo.LoginID);
                if (userOperationTypes != null)
                {
                    if (userOperationTypes.Count == 1)
                    {
                        type = (BusinessType)(int)userOperationTypes[0];
                        panelControl1.Visible = false;
                    }

                }
            }

            OperationType = (OperationType)(int)type;


            Type[] types = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICP.MailCenter.Business.UI.dll")).GetExportedTypes().Where(queryType => typeof(IBusinessQueryPart).IsAssignableFrom(queryType) && queryType.IsClass).ToArray();
            Type ucType = types.Where(t =>
            {
                PropertyInfo property = t.GetProperty("Type");
                DefaultValueAttribute attribute = property.GetCustomAttributes(typeof(DefaultValueAttribute), false).First() as DefaultValueAttribute;
                return (BusinessType)attribute.Value == type;
            }
             ).First();
            IBusinessQueryPart queryPart = null;
            if (WorkItem != null)
            {
                 queryPart = WorkItem.Items.AddNew(ucType) as IBusinessQueryPart;
            }
            else
            {
                queryPart = new OceanExportBusinessQueryPart();
            }
            return queryPart;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private QueryInfo GetQueryInfo()
        {
            AdvanceQueryString = CurrentQueryPart.GetAdvanceQueryString();
            if (radioGroup1.SelectedIndex == 0)
            {
                OperationType = OperationType.OceanExport;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                OperationType = OperationType.OceanImport;
            }

            return new QueryInfo(AdvanceQueryString, OperationType);
        }

    }

}
