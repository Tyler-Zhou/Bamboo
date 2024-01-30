using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FAM.UI.GLCode.Finder
{
    [ToolboxItem(false)]
    public partial class GLCodeMultiSelectedListPart : GLCodeListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public override event CurrentChangedHandler CurrentChanged;
        public override event SelectedHandler Selected;

        List<SolutionGLCodeList> SelectedItem
        {
            get
            {
                List<SolutionGLCodeList> list = new List<SolutionGLCodeList>();
                foreach (TreeListNode node in lwTreeGridControl1.Selection)
                {
                    SolutionGLCodeList obj = lwTreeGridControl1.GetDataRecordByNode(node) as SolutionGLCodeList;
                    if (obj != null)
                        list.Add(obj);
                }
                return list;
            }
        }

        public GLCodeMultiSelectedListPart()
            :base()
        {
            Disposed += delegate
            {
                CurrentChanged = null;
                Selected = null;
                lwTreeGridControl1.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
                CurrentChanged(this, Current);
        }

        public override object Current
        {
            get { return bsList.Current; }
        }

        private SolutionGLCodeList CurrentRow
        {
            get { return Current as SolutionGLCodeList; }
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
                if (CurrentChanged != null)
                    CurrentChanged(this, Current);
            }
        }

        #region WorkItem Common

        [CommandHandler(GLCodeCommandConstants.Command_FinderRemove)]
        public void Command_FinderRemove(object sender, EventArgs e)
        {
            if (SelectedItem == null || SelectedItem.Count == 0)
                return;
            foreach (var item in SelectedItem)
            {
                bsList.Remove(item);
            }
            bsList.ResetBindings(false);
            if (CurrentChanged != null)
                CurrentChanged(this, Current);
        }

        [CommandHandler(GLCodeCommandConstants.Command_FinderRemoveAll)]
        public void Command_FinderRemoveAll(object sender, EventArgs e)
        {
            DataSource = new List<SolutionGLCodeList>();
        }

        [CommandHandler(GLCodeCommandConstants.Command_FinderConfirm)]
        public void Command_FinderConfirm(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected(this, DataSource);
        }

        #endregion
    }
}
