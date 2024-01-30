using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class VoyageSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region 搜索器用到的属性
        /// <summary>
        /// 装货港id
        /// </summary>
        protected Guid? _polId = Guid.Empty;

        /// <summary>
        /// 卸货港id
        /// </summary>
        protected Guid? _podId = Guid.Empty;

        /// <summary>
        /// 装货港名
        /// </summary>
        protected string _polName = string.Empty;

        /// <summary>
        /// 卸货港名
        /// </summary>
        protected string _podName = string.Empty;
        #endregion

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

        /// <summary>
        /// 如果是Finder，在进入时就执行搜索
        /// </summary>
        protected virtual bool IsFinder { get { return false; } }

        #region init

        public VoyageSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataReturned = null;
                this.chkEta.CheckedChanged -= this.chkETA_CheckedChanged;
                this.chkEtd.CheckedChanged -= this.chkETD_CheckedChanged;
                this.btnClear.Click -= this.btnClear_Click;
                this.btnSearch.Click -= this.btnSearch_Click;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtNo, txtVesselName }, this.KeyEventHandle);
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (IsFinder == false && DataReturned != null)
            //    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtNo, txtVesselName }, this.btnSearch,this.KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void SetCnText()
        {
            labMax.Text = "最大行数";
            labNo.Text = "航次";
            labVesselName.Text = "船名/航次";
          
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            labIsValid.Text = "状态";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
            nbarBase.Caption = "基本信息";
            navDate.Caption = "日期";

            chkEta.Text = "估计到港日";
            chkEtd.Text = "估计离港日";
            labEtaFrom.Text = labEtdFrom.Text = "从";
            labEtaTo.Text = labEtdTo.Text = "到";


        }

        private void chkETA_CheckedChanged(object sender, EventArgs e)
        {
            this.dteEtaFrom.Enabled = this.dteEtaTo.Enabled = chkEta.Checked;
            if (chkEta.Checked)
            {
                this.dteEtaFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(-1);
                this.dteEtaTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            }
            else
            {
                this.dteEtaFrom.Text = string.Empty;
                this.dteEtaTo.Text = string.Empty;
            }
        }

        private void chkETD_CheckedChanged(object sender, EventArgs e)
        {
            this.dteEtdFrom.Enabled = this.dteEtdTo.Enabled = chkEtd.Checked;
            if (chkEtd.Checked)
            {
                this.dteEtdFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(-1);
                this.dteEtdTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            }
            else
            {
                this.dteEtdFrom.Text = string.Empty;
                this.dteEtdTo.Text = string.Empty;
            }
        }

        #endregion

        #region ISearchPart 成员

        public object GetData()
        {
            if (_polId == Guid.Empty)
            {
                _polId = null;
            }

            if (_podId == Guid.Empty)
            {
                _podId = null;
            }


            DateTime? etaFrom = null, etaTo = null, etdFrom = null, etdTo = null;
            if (chkEta.Checked)
            {
                etaFrom = dteEtaFrom.DateTime.Date;
                etaTo = CommonUtility.GetEndDate(dteEtaTo.DateTime);
            }
            if (chkEtd.Checked)
            {
                etdFrom = dteEtdFrom.DateTime.Date;
                etdTo = CommonUtility.GetEndDate(dteEtdTo.DateTime);
            }

            List<VoyageList> list = TransportFoundationService.GetVoyageList(null,txtVesselName.Text.Trim(),txtNo.Text.Trim(),null,null,null,lwchkIsValid.Checked,int.Parse(numMax.Value.ToString()));
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
            this.ClearControls();
        }

        protected void ClearControls()
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

            chkEta.Checked = chkEtd.Checked = false;

            txtVesselName.Focus();
        }
        #endregion

        #region ISearchPart 成员

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            this.ClearControls();
        }

        #endregion
    }

    /// <summary>
    /// Finder SerarchPart
    /// </summary>
    public class VoyageFinderSearchPart : VoyageSearchPart
    {
        protected override bool IsFinder
        {
            get
            {
                return true;
            }
        }

        public override void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            this.ClearControls();

            if (triggerType == FinderTriggerType.KeyEnter)
            {
                if (property.Contains(SearchFieldConstants.Vessel))
                    txtVesselName.Text = searchValue == null ? string.Empty : searchValue.ToString();
                else
                    txtNo.Text = searchValue == null ? string.Empty : searchValue.ToString();
            }
            if (conditions != null)
            {
                if (conditions.Contain("POLID"))
                {
                    this._polId = (Guid?)conditions.GetValue("POLID").Value;
                   // txtPol.Enabled = conditions.GetValue("POLID").CanChange;
                }

                if (conditions.Contain("POLName"))
                {
                    this._polName = conditions.GetValue("POLName").Value.ToString();
                   // txtPol.Text = _polName;
                }

                if (conditions.Contain("PODID"))
                {
                    this._podId = (Guid?)conditions.GetValue("PODID").Value;
                    //txtPod.Enabled = conditions.GetValue("PODID").CanChange;
                }

                if (conditions.Contain("PODName"))
                {
                    this._podName = conditions.GetValue("PODName").Value.ToString();
                  //  txtPod.Text = _podName;
                }
            }
        }
    }
}
