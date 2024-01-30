using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.GLCode.Finder
{
    [ToolboxItem(false)]
    public partial class GLCodeFinderListPart : GLCodeListPart
    {
        public GLCodeFinderListPart()
            : base()
        {
            //InitializeComponent();
            lwTreeGridControl1.KeyDown += lwTreeGridControl1_KeyDown;
            Disposed += delegate {

                Selected = null;
                lwTreeGridControl1.KeyDown -= lwTreeGridControl1_KeyDown;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        bool onlyLeaf = false;

        public override void Init(IDictionary<string, object> values)
        {
            foreach (var item in values)
            {
                if (item.Key == "OnlyLeaf")
                {
                    if (item.Value != null)
                    {
                        onlyLeaf = (bool)item.Value;
                    }
                }
            }
        }

        void lwTreeGridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmSelected();
            }
        }

        public override void DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelected();
        }


        private void ConfirmSelected()
        {
            if (CurrentRow == null) return;
            if (onlyLeaf&&!CurrentRow.IsLeaf)
            {
                string message ="";

                if (LocalData.IsEnglish)
                {
                    message = "[" + CurrentRow.EName + "]Not the end of the level glCode, you can not select the glCode";
                }
                else
                {
                    message = "["+CurrentRow.CName + "]不是末级科目,不能选择该科目";
                }
                XtraMessageBox.Show(message);
                return;
            }

            if (Selected != null)
            {
                Selected(this, CurrentRow);
            }

            if (FindForm() != null)
            {
                FindForm().Close();
            }
        }

        [CommandHandler(GLCodeCommandConstants.Command_FinderConfirm)]
        public void Common_FinderConfirm(object sender, EventArgs e)
        {
            ConfirmSelected();
        }

        #region IListPart成员

        public override event SelectedHandler Selected;

        #endregion
    }
}
