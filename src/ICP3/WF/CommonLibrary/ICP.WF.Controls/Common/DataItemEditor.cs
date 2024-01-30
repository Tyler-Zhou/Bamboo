
//-----------------------------------------------------------------------
// <copyright file="DataItemEditor.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace  ICP.WF.Controls
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using ICP.WF.ServiceInterface;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Client;
    using System.Collections.Generic;

    /// <summary>
    /// 数据源设计界面    /// </summary>
    [Serializable]
    public partial class DataItemEditor : XtraForm
    {

        #region 本地变量

        ITypeDescriptorContext _typeDescriptorContext;
        DataColumnItemCollection _selectedEvent;
        Control _control;

        #endregion

        #region 资源初始化与释放

        public DataItemEditor()
        {
            InitializeComponent();
        }

        public DataItemEditor(ITypeDescriptorContext typeDescriptorContext, DataColumnItemCollection selectedEvent)
            : this()
        {
            _typeDescriptorContext = typeDescriptorContext;
            _selectedEvent = selectedEvent;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode == false)
            {

                _control = _typeDescriptorContext.Instance as Control;
                if (_control == null)
                {
                    return;
                }


                InitControls();

                InitData(_selectedEvent);
            }
        }

        #endregion

        #region 外部接口
        /// <summary>
        /// 返回数据源
        /// </summary>
        public IList DataSource
        {
            get
            {
                if (bsItemList.Count > 0)
                {
                    bsItemList.EndEdit();
                    return bsItemList.List;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (bsItemList.DataSource != value)
                {
                    bsItemList.DataSource = value;
                    bsItemList.ResetBindings(false);
                }
            }
        }

        #endregion

        #region 本地方法

        /*控件初始化*/
        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DataItemType>> statusList = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DataItemType>(LocalData.IsEnglish);
            foreach (ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DataItemType> status in statusList)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(status.Name.ToString(), status.Value.ToString()));
            }


            btnMainDelete.Click += new EventHandler(btnMainDelete_Click);

        }

        void btnMainDelete_Click(object sender, EventArgs e)
        {
            if (this.bsItemList.Current != null)
            {
                this.bsItemList.Remove(this.bsItemList.Current);
            }
        }

        #region 外部接口

        DataColumnItemCollection _dataItemCollection;

        /// <summary>
        /// 当前选择的业务方法
        /// </summary>
        public DataColumnItemCollection DataColumnItems
        {
            get { return _dataItemCollection; }
            set { _dataItemCollection = value; }
        }

        #endregion

        #region 本地方法

        private void InitData(DataColumnItemCollection dataItemCollection)
        {
            if (dataItemCollection == null) return;

            bsItemList.DataSource = dataItemCollection;
            bsItemList.ResetBindings(false);
        }

        private DataColumnItemCollection BuildDataItemCollection()
        {
            DataColumnItemCollection dataItemCollection = new DataColumnItemCollection();
            foreach (DataColumnItem item in bsItemList.List)
            {
                dataItemCollection.Add(item);
            }

            return dataItemCollection;
        }
            #endregion

        /*验证*/
        private bool ValidateData()
        {
            bool isSuccess = true;
            //foreach (DataColumnItem item in bsItemList.List)
            //{
            //    if (!item.Validate())
            //    {
            //        isSuccess = false;
            //    }
            //}
            return isSuccess;
        }

      

      
        /*当前行*/
        private DataColumnItem CurrentDataItem
        {
            get
            {
                if (bsItemList.Count > 0 && bsItemList.Current != null)
                {
                    return bsItemList.Current as DataColumnItem;
                }
                else
                {
                    return null;
                }
            }
        }

        /*刷新工具条*/
        private void RefreshMenuEnables()
        {
            if (CurrentDataItem != null)
            {
                btnMainDelete.Enabled = true;
            }
            else
            {
                btnMainDelete.Enabled = false;
            }
        }
        #endregion

        #region 事件处理


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;

            bsItemList.EndEdit();

            _dataItemCollection = BuildDataItemCollection();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Clic(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

    }

    /// <summary>
    /// 数据列集合类
    /// </summary>
    [Serializable]
    public sealed class DataColumnItemCollection : Collection<DataColumnItem>
    {

        protected override void ClearItems()
        {
            base.ClearItems();
        }

        public DataColumnItem this[string name]
        {
            get
            {
                foreach (DataColumnItem d in this.Items)
                {
                    if (d.ColumnName.Equals(name)) return d;
                }

                return null;
            }
            set
            {
                DataColumnItem pd = null;
                foreach (DataColumnItem d in this.Items)
                {
                    if (d.ColumnName.Equals(name))
                    {
                        pd = d;
                    }
                }

                if (pd != null)
                {
                    pd = value;
                }
            }
        }

        public bool Contain(string name)
        {
            foreach (DataColumnItem d in this.Items)
            {
                if (d.ColumnName.Equals(name)) return true;
            }

            return false;
        }


        protected override void InsertItem(int index, DataColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (base.Contains(item))
            {
                this.Remove(item);
            }
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, DataColumnItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.SetItem(index, item);
        }
    }




    /// <summary>
    /// 提供可用于设计MetodData对象编辑器    /// </summary>
    [Serializable]
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    public class DataItemTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

        public override object EditValue(ITypeDescriptorContext typeDescriptorContext, IServiceProvider serviceProvider, object value)
        {
            if (typeDescriptorContext == null)
            {
                throw new ArgumentNullException("typeDescriptorContext");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            object obj2 = value;
            this.editorService = (IWindowsFormsEditorService)serviceProvider.GetService(typeof(IWindowsFormsEditorService));
            if (this.editorService != null)
            {
                DataColumnItemCollection data = null;
                if (obj2 != null)
                {
                    data = obj2 as DataColumnItemCollection;
                }
                
                DataItemEditor dialog = new DataItemEditor(typeDescriptorContext, data);
                if (DialogResult.OK != this.editorService.ShowDialog(dialog))
                {
                    return obj2;
                }

                if (typeDescriptorContext.PropertyDescriptor.PropertyType == typeof(EventData))
                {
                    return dialog.DataColumnItems;
                }
            }
            return obj2;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext typeDescriptorContext)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
