using System;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.DomesticTrade.UI.Common.Parts
{
    public partial class EditRemarkPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        /// <summary>
        /// 备注必须输入，才可以点击“保存”按钮
        /// </summary>
        public bool RemartRequired { get; set; }

        #region Init

        public EditRemarkPart()
        {
            InitializeComponent();
            if (!LocalData.IsEnglish)
            {
                SetCNText();
            }
            if (!DesignMode)
            {
                Disposed += delegate {
                    Saved = null;
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
            }
        }

        private void SetCNText()
        {
            labRemark.Text = "备注";
            btnCancel.Text = "取消(&C)";
            btnOK.Text = "确定(&O)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (RemartRequired)
            {
                txtRemark.TextChanged += new EventHandler(txtRemark_TextChanged);
                btnOK.Enabled = false;
            }
            InitControls();
        }

        void txtRemark_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = txtRemark.Text.Trim().Length > 0;
        }

        private void InitControls()
        {

        }

        public string LabRemark 
        {
            get { return labRemark.Text; }
            set { labRemark.Text = value; }
        }

        public string Remark
        {
            get { return txtRemark.Text; }
            set { txtRemark.Text = value; }
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            txtRemark.Text = data == null ? string.Empty : data.ToString();
        }

        public override object DataSource
        {
            get { return txtRemark.Text; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            Validate();
        }

        public override event SavedHandler Saved;

        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();

            FindForm().Close();
        }

        private bool Save()
        {
            EndEdit();
            if (Saved != null)
            {
                Saved(new object[] { txtRemark.Text });
            }

            return true;
        }
        #endregion
    }
}
