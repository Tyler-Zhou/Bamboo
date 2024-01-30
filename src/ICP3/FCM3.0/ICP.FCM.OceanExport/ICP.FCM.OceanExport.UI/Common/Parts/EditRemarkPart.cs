using System;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanExport.UI.Common.Parts
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
                this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
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
                this.txtRemark.TextChanged += new EventHandler(txtRemark_TextChanged);
                this.btnOK.Enabled = false;
            }
            InitControls();
        }

        void txtRemark_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.txtRemark.Text.Trim().Length > 0;
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
            return this.Save();
        }

        public override void EndEdit()
        {
            this.Validate();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Save())
                {
                    this.FindForm().Close();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this,ex.Message);
            }
           

            
        }

        private bool Save()
        {
            this.EndEdit();
            if (Saved != null)
            {
                Saved(new object[] { txtRemark.Text });
            }

            return true;
        }
        #endregion
    }
}
