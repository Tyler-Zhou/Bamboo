using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.UserManage.MiniFinder
{
    /// <summary>
    /// UMFS= UserMiniFinderSingle
    /// </summary>
    [ToolboxItem(false)]
    public partial class UMFS_MainListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public UMFS_MainListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtFinder }, this.OnKeyDownHandle);
                this.gvMain.DoubleClick -= this.gvMain_DoubleClick;
                this.gvMain.KeyDown -= this.gvMain_KeyDown;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.txtFinder.TextChanged -= this.txtFinder_TextChanged;
                this.btnConfirm.Click -= this.btnConfirm_Click;
                this.Selected = null;
                this._initSource = null;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
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
            colCode.Caption = "代码";
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
            txtFinder.Focus();
            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtFinder }, this.btnConfirm,this.OnKeyDownHandle);
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnConfirm.PerformClick();
            }

        }
        #endregion

        #region IListPart成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        private UserList CurrentRow
        {
            get { return Current as UserList; }
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

        public override void Clear() { txtFinder.Text = string.Empty; }

        #region IPart 成员
        List<UserList> _initSource = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "InitSource")
                {
                    _initSource = item.Value as List<UserList>;
                    if (_initSource == null) _initSource = new List<UserList>();

                    txtFinder.Text = string.Empty;
                }
                
            }
        }
        #endregion

        #endregion

        #region Event

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            UserList list = gvMain.GetRow(e.RowHandle) as UserList;
            if (list == null) return;

            if (list.IsValid == false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        private void txtFinder_TextChanged(object sender, EventArgs e)
        {

            string stringFinder = txtFinder.Text.Trim().ToUpper();
            if (stringFinder.Length ==0)
            {
                this.DataSource = _initSource;
                return;
            }

            List<UserList> tager = _initSource.FindAll(delegate(UserList item)
                                        {
                                            return item.Code.ToUpper().Contains(stringFinder)
                                                   || item.CName.ToUpper().Contains(stringFinder)
                                                   || item.EName.ToUpper().Contains(stringFinder);
                                        });
            this.DataSource = tager;
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            Confirm();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }


        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Confirm();
        }

        private void Confirm()
        {
            if (CurrentRow == null) return;

            if (this.Selected != null)
                Selected(this, CurrentRow);
        }

        #endregion

     
    }
}

