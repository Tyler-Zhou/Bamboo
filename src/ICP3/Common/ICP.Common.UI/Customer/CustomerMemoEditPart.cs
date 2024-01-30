using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Customer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CustomerMemoEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        //public override string Title { get { return LocalData.IsEnglish ? "CustomerMemoEdit" : "编辑备注"; } }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
 

        public CustomerMemoEditPart()
        {
            InitializeComponent();
           

            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.dxErrorProvider1.DataSource = null;
                this.DataChanged = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.btnEditAttachment.ButtonClick -= this.btnEditAttachment_ButtonClick;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

     

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<MemoType>> types = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<MemoType>(LocalData.IsEnglish);
            foreach (var item in types)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        private void SetCnText()
        {
            labAttachmentName.Text = "附件";
            labContent.Text = "内容";
            labCreateBy.Text = "创建人";
            labCreateDate.Text = "创建日期";
            labSubject.Text = "主题";
            labType.Text = "类型";

        }

        public void BindingData(object data)
        {
            this.bindingSource1.DataSource = data;

            InitControls();
        }
        #region IDataContentPart 成员
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion

        private void btnEditAttachment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string fileName = ofd.FileName;

                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    byte[] infbytes = new byte[(int)fs.Length];
                    fs.Read(infbytes, 0, infbytes.Length);
                    fs.Close();

                    CustomerMemoInfo currentData = bindingSource1.DataSource as CustomerMemoInfo;
                    //currentData.Attachment = infbytes;
                    //currentData.AttachmentName = ofd.SafeFileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
