using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeFinderSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region serivce
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region init

        public ChargingCodeFinderSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
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

            if (!DesignMode)
            {
                InitControls();
             
            }
        }
        Guid solutionID = Guid.Empty;
        protected virtual void InitControls() 
        {
            this.chkValid.Checked = true;
            this.chkValid.Enabled = false;
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            List<SolutionChargingCodeList> list = ConfigureService.GetSolutionChargingCodeListByList(
                solutionID, 
                this.txtName.Text, 
                this.chkIsAgent.Checked, 
                this.chkIsConnission.Checked, 
                this.chkValid.Checked);


            return list;
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override void Init(IDictionary<string, object> values)
        {

            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "NAME")
                {
                    this.txtName.Text = item.Value.ToString();
                }
                else if (item.Key.ToUpper() == "ISAGENT ")
                {
                    this.chkIsAgent.Enabled = false;
                    this.chkIsAgent.Checked = true;
                }
                else if (item.Key.ToUpper() == "ISCONNISSION")
                {
                    this.chkIsConnission.Enabled = false;
                    this.chkIsConnission.Checked = true;
                }
                if (item.Key.ToUpper()==("SOLUTIONID"))
                {
                    solutionID = new Guid(item.Value.ToString());
                }   
            }
            this.btnSearch.PerformClick();
        }

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            this.ClearControl();
        }

        private void ClearControl()
        {
            this.txtName.Text = string.Empty;
            if (this.chkIsAgent.Enabled)
            {
                this.chkIsAgent.Checked = null;
            }
            if (this.chkIsConnission.Enabled)
            {
                this.chkIsConnission.Checked = null;
            }
        }
        #endregion


    }
}
