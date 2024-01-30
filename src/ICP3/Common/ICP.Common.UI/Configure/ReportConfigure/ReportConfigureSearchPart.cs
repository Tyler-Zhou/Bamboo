using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Configure.ReportConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ReportConfigureSearchPart : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        public ReportConfigureSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.comboBoxType.OnFirstEnter -= this.OncomboBoxTypeFirstEnter;
                this.OnSearched = null;
                this.OnConditionChanged = null;
                this.bsChargingGroup.DataSource = null;
                this.bsChargingGroup.Dispose();
                if (this.WorkItem != null)
                 {
                     this.WorkItem.Items.Remove(this);
                     this.WorkItem = null;
                }
            
            };
        }

        #region 控制器

        public ReportConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<ReportConfigureController, ReportConfigureController>();
            }
        }

        #endregion  

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
        }
        private void OncomboBoxTypeFirstEnter(object sender, EventArgs e)
        {
            List<ReportType> reportTypes = this.Controller.GetReportTypes();
            this.comboBoxType.Properties.BeginUpdate();
            comboBoxType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
            foreach (var type in reportTypes)
            {
                comboBoxType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(type.Name, (object)type.Index));
            }
            this.comboBoxType.Properties.EndUpdate();
        }
        private void InitControls()
        {
            this.comboBoxType.OnFirstEnter += this.OncomboBoxTypeFirstEnter;
        }

        private void SetCnText()
        {
            labCode.Text = "代码";
            labCName.Text = "中文描述";
            labEName.Text = "英文描述";
            labReportType.Text = "类型";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";

            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";

            nbarBase.Caption = "基本信息";

            btnClear.Text = "清空(&L)";
            btnSearch.Text = "查询(&R)";
        }


        #region ISearchPart 接口

        /// <summary>
        /// 查询界面条件改变后触发该事件
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override event ConditionChangedHandler OnConditionChanged;

        /// <summary>
        /// 查询完成后,触发该事件
        /// <remarks>
        /// 查询完成后,一定要触发该事件
        /// </remarks>
        /// </summary>
        public override event SearchResultHandler OnSearched;

        public override object GetData()
        {
            int? reportTypeID = null;
            if (comboBoxType.EditValue != null)
            {
                 reportTypeID = (int?)comboBoxType.EditValue;    
            }

            try
            {
                List<ReportConfigureList> codeList = ConfigureService.GetReportConfigureList(
                    txtCode.Text.Trim(),
                    txtCDescription.Text.Trim(),
                    txtEDescription.Text.Trim(),
                    reportTypeID,
                    lwchkIsValid.Checked,
                    int.Parse(numMax.Value.ToString()));

                return codeList;
            }
            catch (Exception ex)
            {
                throw ex;//UNDONE: 错误提示处理
            }
        }   

        /// <summary>
        /// 从外界向查询面版初始化值
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="values">初始化值</param>
        public override void Init(IDictionary<string, object> values)
        {
        }

        /// <summary>
        /// 触发工具栏按钮的查询事件(列入下拉工具栏按钮,文本框按钮)
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override void RaiseSearched()
        {
            if (this.OnSearched != null)
            {
                using (new CursorHelper())
                {
                    object datas = this.GetData();
                    this.OnSearched(this, datas);
                }
            }
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.RaiseSearched();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            lwchkIsValid.Checked = true;
            txtCode.Text = string.Empty;
            txtCDescription.Text = string.Empty;
            txtEDescription.Text = string.Empty;
            comboBoxType.EditValue = null;
        }

        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(ReportConfigureConstants.CMD_Refresh)]
        public void CMD_Refresh(object s, EventArgs e)
        {
            this.RaiseSearched();
        }
    }
}
