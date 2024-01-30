using System;
using System.Collections.Generic;
using DevExpress.XtraTreeList.Nodes;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FAM.UI.GLCode.Finder
{
    public partial class GLCodeMultiMainListPart : GLCodeListPart
    {
        #region IListPart成员

        public override event SelectedHandler Selected;

        #endregion

        public GLCodeMultiMainListPart()
            :base()
        {
            lwTreeGridControl1.OptionsSelection.MultiSelect = true;
            //this.lwTreeGridControl1.DoubleClick += new EventHandler(lwTreeGridControl1_DoubleClick);
            Disposed += delegate
            {
                Selected = null;
                //this.lwTreeGridControl1.DoubleClick -= this.lwTreeGridControl1_DoubleClick;
            };
        }

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

        private void ConfirmSelected()
        {
            if (SelectedItem == null) return;

            if (Selected != null)
            {
                Selected(this, SelectedItem);
            }
        }

        //void lwTreeGridControl1_DoubleClick(object sender, EventArgs e)
        //{
        //    ConfirmSelected();
        //}
        public override void DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        [CommandHandler(GLCodeCommandConstants.Command_FinderSelect)]
        public void Command_FinderSelect(object sender, EventArgs e)
        {
            ConfirmSelected();
        }
    }
};