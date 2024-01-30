using System;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;

namespace ICP.Operation.Common.ServiceInterface
{
    #region RepositoryItemMemoExEditBIS class

    [UserRepositoryItem("RegisterMyMemoExEdit")]
    public class RepositoryItemMemoExEditBIS : RepositoryItemMemoExEdit
    {
        #region c'tor

        internal const string EditorName = "BISMemoExEdit";

        static RepositoryItemMemoExEditBIS()
        {
            RegisterMyMemoExEdit();
        }

        public RepositoryItemMemoExEditBIS()
        {
            Initialize();
        }

        #endregion

        #region Implementation

        public static void RegisterMyMemoExEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(
                EditorName,
                typeof(MemoEditExBIS),
                typeof(RepositoryItemMemoExEditBIS),
                typeof(RepositoryItemMemoExEditViewInfoBIS),
                new BISBlobBaseEditPainter(),
                true,
                EditImageIndexes.MemoExEdit,
                typeof(DevExpress.Accessibility.PopupEditAccessible)
            ));
        }

        private void Initialize()
        {
            ConvertEmptyStringToNull = true;
        }

        #endregion

        #region overrides

        public override string EditorTypeName
        {
            get
            { return EditorName; }
        }

        /// <summary>
        /// This override is used to allow custom properties in child clone editors to be properly assigned
        /// </summary>
        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            ConvertEmptyStringToNull = (item as RepositoryItemMemoExEditBIS).ConvertEmptyStringToNull;
        }

        protected override void RaiseParseEditValue(ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                // if a user has entered only carriage returns and tabs, clear them out
                string checkValue = e.Value.ToString();
                checkValue = checkValue.Trim();

                if (checkValue == "")
                    e.Value = "";
            }

            if (e.Value != null && (e.Value.ToString() == string.Empty) && this.ConvertEmptyStringToNull)
            {
                e.Value = DBNull.Value;
                e.Handled = true;
            }

            base.RaiseParseEditValue(e);
        }

        protected override bool ForceDisableButtonOnReadOnly
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region Properties
        [Category("Behavior")]
        [Description("Indicates whether empty strings are stored in the database as null or empty strings.")]
        public bool ConvertEmptyStringToNull { get; set; }

        [Category("Behavior")]
        [Description("Indicates whether AutoSpellChecking will be performed on the control.")]
        public bool AutoSpellCheck { get; set; }

        #endregion

    }

    #endregion

    #region MemoEditExBIS class

    public class MemoEditExBIS : DevExpress.XtraEditors.MemoExEdit
    {

        static MemoEditExBIS()
        {
            RepositoryItemMemoExEditBIS.RegisterMyMemoExEdit();
        }
       
        public override string EditorTypeName
        { get { return RepositoryItemMemoExEditBIS.EditorName; } }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMemoExEditBIS Properties
        {
            get
            {
                return base.Properties as RepositoryItemMemoExEditBIS;
            }
        }
    }

    #endregion

    public class BISBlobBaseEditPainter : BlobBaseEditPainter
    {
        public BISBlobBaseEditPainter() : base() { }
        protected override void DrawText(ControlGraphicsInfoArgs info)
        {
            //base.DrawText(info);
            info.ViewInfo.PaintAppearance.DrawString(info.Cache, info.ViewInfo.DisplayText, info.Bounds);
        }
    }

}
