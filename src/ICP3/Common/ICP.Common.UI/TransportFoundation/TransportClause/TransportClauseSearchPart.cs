using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.TransportFoundation.TransportClause
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TransportClauseSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }


        #endregion

        public TransportClauseSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataReturned = null;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.stxtDestinationCode, this.stxtOriginalCode }, this.KeyEventHandle);
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labDestinationCode.Text = "目的地";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labOriginalCode.Text = "起始地";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (DataReturned != null)
            //    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.stxtDestinationCode, this.stxtOriginalCode }, this.btnSearch,this.KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        #region ISearchPart 成员

        public object GetData()
        {
            List<TransportClauseList> list = TransportFoundationService.GetTransportClauseList(stxtOriginalCode.Text.Trim(),
                                                                 stxtDestinationCode.Text.Trim(),
                                                                 lwchkIsValid.Checked,
                                                                 int.Parse(numMax.Value.ToString()));
            return list;
        }

        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
            {
                using (new CursorHelper())
                {
                    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
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
            stxtOriginalCode.Focus();
        }

        #endregion

        #region ISearchPart 成员

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            this.ClearControls();
        }

        #endregion
    }
}
