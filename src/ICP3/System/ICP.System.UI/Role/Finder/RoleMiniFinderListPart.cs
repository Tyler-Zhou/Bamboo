using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.Role.Finder
{
    [ToolboxItem(false)]
    public partial class RoleMiniFinderListPart :  ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public RoleMiniFinderListPart()
        {
            InitializeComponent();
            this.Disposed += delegate 
            {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtFind }, this.OnKeyDownHandle);
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.Selected = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            colDescription.Caption = "描述";
            colCName.Caption = "中文名";
            colEName.Caption = "英文名";

            btnConfirm.Text = "确定(&O)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            txtFind.Focus();
            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtFind }, this.btnConfirm, this.OnKeyDownHandle);
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnConfirm.PerformClick();
            }

        }
        #endregion

        #region IListPart 成员

        public override  object Current
        {
            get { return bsList.Current; }
        }
        private RoleList CurrentRow
        {
            get { return Current as RoleList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override void Clear() { txtFind.Text = string.Empty; }

        #endregion

        #region Event

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            Confirm();
        }

        private void Confirm()
        {
            if (CurrentRow == null) return;

            if (this.Selected != null)
                Selected(this, CurrentRow);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Confirm();
        }

        #endregion
    }
}

