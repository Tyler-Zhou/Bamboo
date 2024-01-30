using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ICP.WF.ServiceInterface;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel.DataAnnotations;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.WF.Controls
{
    public partial class EventDataEditor : XtraForm
    {
        #region 本地变量

        ITypeDescriptorContext _typeDescriptorContext;
        EventData _selectedEvent;
        Control _control;

        #endregion

        #region 资源初始化与释放

        public EventDataEditor()
        {
            InitializeComponent();
        }

        public EventDataEditor(ITypeDescriptorContext typeDescriptorContext, EventData selectedEvent)
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

        EventData _eventData;

        /// <summary>
        /// 当前选择的业务方法


        /// </summary>
        public EventData EventData
        {
            get { return _eventData; }
            set { _eventData = value; }
        }

        #endregion

        #region 本地方法

        private void InitData(EventData function)
        {
            if (function == null) return;


            cmbEventType.SelectedIndex = (int)function.EventType;

            bsEventList.DataSource = function.MappingRelationItems.ToList();

            bsEventList.ResetBindings(false);
        }

        private EventData BuildPostFunction()
        {

            EventData pf = new EventData();
            pf.EventType = (EventType)cmbEventType.EditValue;

            pf.MappingRelationItems = new MappingRelationItemCollection();
            foreach (MappingRelationItem item in bsEventList.List)
            {
                pf.MappingRelationItems.Add(item);
            }

            return pf;
        }

        /*初始化控件*/
        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<EventType>> statusList = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<EventType>(LocalData.IsEnglish);
            foreach (ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<EventType> status in statusList)
            {
                cmbEventType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(status.Name, status.Value));
            }

            List<string> sourceList = GetSourceList();
            foreach (string str in sourceList)
            {
                cmbSource.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(str,str));
            }


            List<string> targetList = GetTargetList();
            foreach (string str in sourceList)
            {
                cmbTarget.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(str, str));
            }

            btnMainDelete.Click += new EventHandler(btnMainDelete_Click);

        }

        void btnMainDelete_Click(object sender, EventArgs e)
        {
            if (this.bsEventList.Current != null)
            {
                this.bsEventList.Remove(this.bsEventList.Current);
            }
        }

     

        /*获取主标单属性列表*/
        private List<string> GetSourceList()
        {
            List<string> ps = new List<string>();

            IBindingSourceTypeService bs=null;
            if (_control is LWComBox)
            {
                LWComBox cb = _control as LWComBox;
                if (cb.DataSource == null) return ps;
                
                bs = cb.DataSource as IBindingSourceTypeService;
                if (bs == null)
                {
                    return ps;
                }
            }
            else if (_control is LWComboBoxTreeView)
            {
                LWComboBoxTreeView cb = _control as LWComboBoxTreeView;
                if (cb.DataSource == null) return ps;

                bs = cb.DataSource as IBindingSourceTypeService;
                if (bs == null)
                {
                    return ps;
                }
            }

           
            System.Reflection.PropertyInfo[] pis = bs.DataType.GetProperties();
            foreach (System.Reflection.PropertyInfo p in pis)
            {
                ps.Add(p.Name);
            }

            return ps;
        }


        LWBaseForm GetWFParentForm(Control control)
        {
            if (control.Parent is LWBaseForm)
            {
                return control.Parent as LWBaseForm;
            }
            else
            {
                return GetWFParentForm(control.Parent);
            }
        }

        /*获取自己关联表单的属性*/
        private List<string> GetTargetList()
        {
            List<string> ps = new List<string>();
            if (_control == null) return ps;

            LWBaseForm form = GetWFParentForm(_control);
            if (form == null) return ps;

            ps = form.GetTitleColumns();

            return ps;
        }







        /*验证*/
        bool ValidateData()
        {
            bool isSucc = true;

            foreach (MappingRelationItem item in bsEventList.List)
            {
                if (!item.Validate())
                {
                    isSucc = false;
                }

            }

            return isSucc;
        }

        #endregion


        #region 确认与取消
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;

            bsEventList.EndEdit();

            _eventData = BuildPostFunction();

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }


    [Serializable]
    public class MappingRelationItem : BaseDataObject
    {
        [ICP.Framework.CommonLibrary.Common.RequiredAttribute(CMessage = "源属性", EMessage = "SourceField")]
        public string SourceField { get; set; }

        [ICP.Framework.CommonLibrary.Common.RequiredAttribute(CMessage = "目标属性", EMessage = "TargetField")]
        public string TargetField { get; set; }
    }

    [Serializable]
    public class EventData
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType EventType { get; set; }


        MappingRelationItemCollection mappingRelationItems = new MappingRelationItemCollection();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MappingRelationItemCollection MappingRelationItems
        {
            get { return mappingRelationItems; }
            set { mappingRelationItems = value; }
        }


        string GetColumnNameFromCaption(DataTable table, string caption)
        {
            foreach (DataColumn dc in table.Columns)
            {
                if (string.IsNullOrEmpty(dc.Caption)) continue;

                if (dc.Caption.Equals(caption)) return dc.ColumnName;
            }

            return string.Empty;
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public void Excute(object sourceRow, DataTable targetTable)
        {
            if (sourceRow == null) return;
            if ((targetTable == null) || targetTable.Rows.Count < 1) return;

            if (mappingRelationItems == null) return;

            DataRow targetRow = targetTable.Rows[0];

            foreach (MappingRelationItem item in mappingRelationItems)
            {
                string targetCol = GetColumnNameFromCaption(targetTable, item.TargetField);
                if (string.IsNullOrEmpty(targetCol)) continue;

                targetRow[targetCol] = sourceRow.GetType().GetProperty(item.SourceField).GetValue(sourceRow,null);
            }

            targetTable.AcceptChanges();

        }

        private string ProcessMainproperty(string oldString)
        {
            string newString = oldString;
            if (oldString.Contains("->"))
            {
                newString = oldString.Substring(oldString.LastIndexOf("->") + 2);
            }


            return newString;
        }

        private string ProcessSelfProperty(Guid workflowInstanceId, IWorkflowService wService, string oldString)
        {
            Dictionary<string, object> vals = wService.GetDataCollect(workflowInstanceId).DataCollect;
            if (vals == null || vals.Count == 0) return string.Empty;

            if (vals.ContainsKey(oldString))
            {
                return vals[oldString].ToString();
            }

            return string.Empty;
        }

        public override string ToString()
        {
            return "Event" + this.EventType.ToString();
        }
    }


    [Serializable]
    public sealed class MappingRelationItemCollection : System.Collections.ObjectModel.Collection<MappingRelationItem>
    {

        protected override void ClearItems()
        {
            base.ClearItems();
        }

        public MappingRelationItem this[string name]
        {
            get
            {
                foreach (MappingRelationItem d in this.Items)
                {
                    if (d.SourceField.Equals(name)) return d;
                }

                return null;
            }
            set
            {
                MappingRelationItem pd = null;
                foreach (MappingRelationItem d in this.Items)
                {
                    if (d.SourceField.Equals(name))
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
            foreach (MappingRelationItem d in this.Items)
            {
                if (d.SourceField.Equals(name)) return true;
            }

            return false;
        }


        protected override void InsertItem(int index, MappingRelationItem item)
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

        protected override void SetItem(int index, MappingRelationItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.SetItem(index, item);
        }
    }




    /// <summary>
    /// 提供可用于设计MetodData对象编辑器
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    public class EventDataTypeEditor : UITypeEditor
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
                EventData data = null;
                if (obj2 != null)
                {
                    data = obj2 as EventData;
                }
                EventDataEditor dialog = new EventDataEditor(typeDescriptorContext, data);
                if (DialogResult.OK != this.editorService.ShowDialog(dialog))
                {
                    return obj2;
                }

                if (typeDescriptorContext.PropertyDescriptor.PropertyType == typeof(EventData))
                {
                    return dialog.EventData;
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
