using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.UI.Contact
{
    /// <summary>
    /// 联系人列表工具栏
    /// </summary>
    public partial class UCCustomerToolbar : UserControl
    {

        public WorkItem WorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        public bool ReadOnly
        {
            get
            {
                return this.barItemType.Enabled;
            }
            set
            {
                this.barItemType.Enabled = value;

            }
        }
        public ContactType ContactType
        {
            get
            {
                if (this.barItemType.EditValue == null)
                {
                    return ContactType.Customer;
                }
                else
                {
                    return (ContactType)(int)(this.barItemType.EditValue);
                }
            }
            set
            {
                int type = (int)value;
                this.barItemType.EditValue = type;
            }
        }

        bool hasFireEvent = false;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="contactType"></param>
        /// <param name="readOnly"></param>
        public void Init(ContactType contactType, bool readOnly)
        {
            this.ContactType = contactType;
            this.ReadOnly = readOnly;
            this.barItemType.Enabled = !readOnly;
            if (this.IsHandleCreated && !hasFireEvent)
            {
                FireEvent();
                hasFireEvent = true;
            }
            //this.
        }

        //this.repositoryItemCheckedComboBoxEdit.DataSource = ICP.FCM.Common.ServiceInterface.Utility.GetStageInfoSource(this.OperationType);


        public void SetStageComboBoxItems(OperationType operationType)
        {
            this.checkComb.Items.Clear();
            List<ContactStageInfo> list = ICP.FCM.Common.ServiceInterface.FCMInterfaceUtility.GetStageInfoSource(operationType);
            foreach (ContactStageInfo item in list)
            {
                checkComb.Items.Add(item.StageName, false);
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public UCCustomerToolbar()
        {
            InitializeComponent();
            if (LocalData.ApplicationType == ICP.Framework.CommonLibrary.Common.ApplicationType.ICP)
            {
                this.barItemType.Enabled = false;
                this.barItemType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barItemSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                this.barItemType.Enabled = true;
                this.barItemType.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barItemSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            this.Disposed += new EventHandler(UCCustomerToolbar_Disposed);
            if (!LocalData.IsDesignMode)
            {
                if (!LocalData.IsEnglish)
                    Locale();

                this.Disposed += delegate { DisposedComponent(); };
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Init();
        }
        private void Init()
        {
            HookEvent();
            if (!hasFireEvent)
            {
                FireEvent();
                hasFireEvent = true;
            }
        }

        private void HookEvent()
        {
            this.barItemRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barRemove_ItemClick);
            this.barItemAddNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barAddNew_ItemClick);
            this.barItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barItemSave_ItemClick);
        }

        void barItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Saved != null)
            {
                this.Saved(this, EventArgs.Empty);
            }
        }

        void UCCustomerToolbar_Disposed(object sender, EventArgs e)
        {
            this.ContactTypeChanged = null;
        }

        void barAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Added != null)
            {
                this.Added(this,EventArgs.Empty);
            }
        }

        void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Deleted != null)
            {
                this.Deleted(this, EventArgs.Empty);
            }
        }

        private void DisposedComponent()
        {
            UnHookEvent();
            if (this.WorkItem != null)
            {
                this.WorkItem.Items.Remove(this);

            }
        }

        private void UnHookEvent()
        {
            this.Saved = null;
            this.Added = null;
            this.Deleted = null;
            this.ContactTypeChanged = null;
            this.ReadOnlyChanged = null;
        }


        private void Locale()
        {
            this.barItemAddNew.Caption = "新增";
            this.barItemRemove.Caption = "删除";
            this.barItemSave.Caption = "确定";
            this.barItemType.Caption = "类型";
            this.radioRole.Items[0].Description = "客户";
            this.radioRole.Items[1].Description = "承运人";
        }

        public void InitType(bool enabled, bool checkCustomer)
        {
            this.barItemType.EditValue = checkCustomer ? 1 : 2;
            this.barItemType.Enabled = enabled;
        }

        private void repositoryItemRadioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.RadioGroup radioGroup = sender as DevExpress.XtraEditors.RadioGroup;
            ContactType = (ContactType)int.Parse(radioGroup.Text);
            FireContactTypeChange(ContactType);

        }

        private void checkComb_EditValueChanged(object sender, EventArgs e) 
        {
            if (SetStage != null)
            {
                this.SetStage(sender, EventArgs.Empty);
            }
         
            //string str = checkComb.
            //MessageBox.Show(barItemType2.EditValue.ToString());
        }

       
        private void FireContactTypeChange(ContactType contactType)
        {
            if (this.ContactTypeChanged != null)
            {
                this.ContactTypeChanged(this, new CommonEventArgs<ContactType>(contactType));
            }
        }
        private void FireReadOnlyChange(bool readOnly)
        {
            if (this.ReadOnlyChanged != null)
            {
                this.ReadOnlyChanged(this, new CommonEventArgs<bool>(readOnly));
            }
        }
        private void FireEvent()
        {
            FireContactTypeChange(this.ContactType);
            FireReadOnlyChange(this.ReadOnly);
        }
        /// <summary>
        /// 联系人类型改变事件
        /// </summary>
        public event EventHandler<CommonEventArgs<ContactType>> ContactTypeChanged;
        /// <summary>
        /// 联系人类型只读属性改变事件
        /// </summary>
        public event EventHandler<CommonEventArgs<bool>> ReadOnlyChanged;
        /// <summary>
        /// 新增联系人事件
        /// </summary>
        public event EventHandler Added;
        /// <summary>
        /// 删除联系人事件
        /// </summary>
        public event EventHandler Deleted;
        /// <summary>
        /// 保存联系人事件
        /// </summary>
        public event EventHandler Saved;

        /// <summary>
        /// 设置沟通阶段
        /// </summary>
        public event EventHandler SetStage;

        /// <summary>
        /// 显示按钮
        /// </summary>
        public void barVisibility()
        {
            this.barItemType.Enabled = true;
            this.barItemType.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barItemSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

    }
}
