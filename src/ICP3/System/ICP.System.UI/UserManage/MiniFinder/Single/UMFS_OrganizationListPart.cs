using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.Sys.UI.UserManage.MiniFinder
{
    [ToolboxItem(false)]
    public partial class UMFS_OrganizationListPart:ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Property

        JobList CurrentRow
        {
            get { return bsList.Current as JobList; }
            set
            {
                JobList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        #endregion

        #region init

        public UMFS_OrganizationListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.treeMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
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
            colCShortName.Caption = "名称";
            colCShortName.Visible = true;
            colEShortName.Visible = false;
        }

        #endregion

        #region tree Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if(CurrentChanged!=null)CurrentChanged(this, Current);
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get
            {
                return bsList.Current;
            }
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
                treeMain.ExpandAll();
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #endregion
    }
}