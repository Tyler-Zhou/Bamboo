using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Configure.Solution
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SoluionChargeCodeSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region serivce
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

        #region init

        public SoluionChargeCodeSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataReturned = null;
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labName.Text = "名称";
            labIsAgent.Text = "代理";
            labValid.Text = "有效";
            labMaxCount.Text = "最大行数";
            labIsCommission.Text = "佣金";


            chkIsAgent.CheckedText = "是";
            chkIsAgent.UnCheckedText = "否";
            chkIsAgent.NULLText = "全部";

            chkIsCommission.CheckedText = "是";
            chkIsCommission.UnCheckedText = "否";
            chkIsCommission.NULLText = "全部";

            ckbValid.CheckedText = "有效";
            ckbValid.UnCheckedText = "无效";
            ckbValid.NULLText = "全部";
            this.btnSearch.Text = "查询(&R)";
            this.btnClear.Text = "清空(&L)";
            navBarBase.Caption = "基本信息";
        }

        #endregion

        #region ISearchPart 成员

        Guid _SolutionID = Guid.Empty;

        public object GetData()
        {
            List<SolutionChargingCodeList> list = ConfigureService.GetSolutionChargingCodeListByList(_SolutionID, txtName.Text.Trim()
                                                                                            , chkIsAgent.Checked,chkIsCommission.Checked ,ckbValid.Checked);
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
            this.ClearControl();
        }

        private void ClearControl()
        {
            txtName.Text = string.Empty;
            chkIsAgent.Checked = null;
            chkIsCommission.Checked = null;
            ckbValid.Checked = true;
            numMaxRow.Value = 100;
        }
        #endregion


        #region ISearchPart 成员

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {

            this.btnClear.PerformClick();
            if (triggerType == FinderTriggerType.KeyEnter) this.txtName.Text = searchValue;
            if (conditions != null)
            {
                if (conditions.Contain("SolutionID"))
                {
                    _SolutionID = (Guid)conditions.GetValue("SolutionID").Value;
                }
            }
        }

        #endregion
    }
}
