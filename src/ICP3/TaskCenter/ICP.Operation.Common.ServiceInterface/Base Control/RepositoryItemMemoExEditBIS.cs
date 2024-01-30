using DevExpress.Accessibility;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using System;
using System.ComponentModel;

namespace ICP.Operation.Common.ServiceInterface
{
    #region RepositoryItemMemoExEditBIS class

    /// <summary>
    /// 
    /// </summary>
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
                typeof(PopupEditAccessible)
            ));
        }

        private void Initialize()
        {
            ConvertEmptyStringToNull = true;
        }

        #endregion

        #region overrides
        /// <summary>
        /// 
        /// </summary>
        public override string EditorTypeName
        {
            get { return EditorName; }
        }

        /// <summary>
        /// This override is used to allow custom properties in child clone editors to be properly assigned
        /// </summary>
        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            ConvertEmptyStringToNull = (item as RepositoryItemMemoExEditBIS).ConvertEmptyStringToNull;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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

            if (e.Value != null && (e.Value.ToString() == string.Empty) && ConvertEmptyStringToNull)
            {
                e.Value = DBNull.Value;
                e.Handled = true;
            }

            base.RaiseParseEditValue(e);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override bool ForceDisableButtonOnReadOnly
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether empty strings are stored in the database as null or empty strings.")]
        public bool ConvertEmptyStringToNull { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether AutoSpellChecking will be performed on the control.")]
        public bool AutoSpellCheck { get; set; }

        #endregion

    }

    #endregion

    #region MemoEditExBIS class
    /// <summary>
    /// 
    /// </summary>
    public class MemoEditExBIS : MemoExEdit
    {
        /// <summary>
        /// 
        /// </summary>
        static MemoEditExBIS()
        {
            RepositoryItemMemoExEditBIS.RegisterMyMemoExEdit();
        }
        /// <summary>
        /// 
        /// </summary>
        public override string EditorTypeName
        { get { return RepositoryItemMemoExEditBIS.EditorName; } }

        /// <summary>
        /// 
        /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public class BISBlobBaseEditPainter : BlobBaseEditPainter
    {
        /// <summary>
        /// 
        /// </summary>
        public BISBlobBaseEditPainter() : base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected override void DrawText(ControlGraphicsInfoArgs info)
        {
            //base.DrawText(info);
            info.ViewInfo.PaintAppearance.DrawString(info.Cache, info.ViewInfo.DisplayText, info.Bounds);
        }
    }

}
