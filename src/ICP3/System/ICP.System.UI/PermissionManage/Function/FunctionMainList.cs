using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Sys.UI.PermissionManage.Function
{
    [ToolboxItem(false)]
    public partial class FunctionMainList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        #endregion

        #region Property

        FunctionList CurrentRow
        {
            get { return bsList.Current as FunctionList; }
            set
            {
                FunctionList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        #endregion

        #region init

        public FunctionMainList()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.treeMain.DataSource = null;
                this.treeMain.BeforeFocusNode -= this.treeMain_BeforeFocusNode;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
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
            colCName.Caption = "名称";
            colCName.Visible = true;
            colEName.Visible = false;
        }

        #endregion

        #region tree Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            FunctionList data = treeMain.GetDataRecordByNode(e.Node) as FunctionList;
            e.Node.StateImageIndex = (short)data.FunctionType;
        }

        private void treeMain_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.CanFocus = !ce.Cancel;
            }
        }

        #endregion

        #region IListPart 成员

        public override void AddItem(object item)
        {
            JobList list = item as JobList;
            bsList.Insert(0, item);
            int position = bsList.List.IndexOf(item);
            if (position >= 0) bsList.Position = position;
        }

        public override object Current
        {
            get { return bsList.Current; }
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

        public override event CancelEventHandler CurrentChanging;
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #endregion

        #region Workitem Common

        [CommandHandler(FunctionCommonConstants.Command_Refresh)]
        public void Common_DisuseData(object sender, EventArgs e)
        {
            try
            {
                List<FunctionList> list = PermissionService.GetFunctionList(null);
                this.DataSource = list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

      

    }
}
