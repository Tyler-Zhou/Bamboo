using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.WF.Activities;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common; 
using System.ComponentModel.DataAnnotations;
using System.Workflow.ComponentModel;
using System.ComponentModel;
namespace ICP.WF.Activities.Common
{
    public partial class SetMainWorkItemDataForm : DevExpress.XtraEditors.XtraForm
	{
        #region 本地变量

        ITypeDescriptorContext _typeDescriptorContext;
        FunctionData _selectedEvent;
        Activity _activity;

        #endregion

        #region 资源初始化与释放

        public SetMainWorkItemDataForm()
		{
			InitializeComponent();
		}

        public SetMainWorkItemDataForm(ITypeDescriptorContext typeDescriptorContext, FunctionData selectedEvent)
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
                _activity = _typeDescriptorContext.Instance as Activity;
                if (_activity == null)
                {
                    return;
                }

                InitControls();

                InitData(_selectedEvent);
            }
        }

        #endregion


        #region 事件处理

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;

            dataGridView1.EndUpdate();
            bindingSource1.EndEdit();

            bsConstants.EndEdit();

            _postFunction = BuildPostFunction();
          
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    dataGridView1[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;
            //    e.Cancel = true;
            //}
            //else
            //{
            //    dataGridView1[e.ColumnIndex, e.RowIndex].ErrorText = string.Empty;
            //}
        }
        #endregion

        #region 外部接口

        FunctionData _postFunction;

        /// <summary>
        /// 当前选择的业务方法
        /// </summary>
        public FunctionData PostFunction
        {
            get { return _postFunction; }
            set { _postFunction = value; }
        }

        #endregion

        #region 本地方法

        private void InitData(FunctionData function)
        {
            if (function == null)
            {
                bindingSource1.DataSource = new MappingRelationItemCollection();
                bsConstants.DataSource = new MappingRelationItemCollection();
                return;
            }

            bindingSource1.DataSource = function.MappingRelationItems.ToList();
            bindingSource1.ResetBindings(false);


            bsConstants.DataSource = function.ConstantMappingRelationItems.ToList();
            bsConstants.ResetBindings(false);
        }

        private FunctionData BuildPostFunction()
        {

            FunctionData pf = new FunctionData();
            pf.MetodName = "PostFunction";

            pf.MappingRelationItems = new MappingRelationItemCollection();
            foreach(MappingRelationItem item in bindingSource1.List)
            {
                pf.MappingRelationItems.Add(item);
            }

            pf.ConstantMappingRelationItems = new MappingRelationItemCollection();
            foreach (MappingRelationItem item in bsConstants.List)
            {
                pf.ConstantMappingRelationItems.Add(item);
            }

            return pf;
        }

        /*初始化控件*/
        private void InitControls()
        {
            //mainWorkItemFieldDataGridViewTextBoxColumn---主表单属性
            Dictionary<string, string> mainPropertyList = GetMainPropertyList();
            foreach (KeyValuePair<string,string>  mainProperty in mainPropertyList)
            {
                MainWorkItemFieldColumn.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(mainProperty.Value, mainProperty.Key));
                colConstantComBox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(mainProperty.Value, mainProperty.Key));
            }
            //selfItemFieldDataGridViewTextBoxColumn---当前表单属性
            Dictionary<string, string> selfPropertyList = GetSelfPropertyList();
            foreach (KeyValuePair<string, string> selfProperty in selfPropertyList)
            {
                SelfItemFieldColumn.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(selfProperty.Value, selfProperty.Key));
            }
            //rewriteModeComBoxColumn---重与属性
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<RewriteMode>> statusList = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<RewriteMode>(LocalData.IsEnglish);
            foreach (ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<RewriteMode> mode in statusList)
            {
                RewriteModeColumn.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(mode.Name, mode.Value.ToString()));
                colConstantRewriteMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(mode.Name, mode.Value.ToString()));
            }          

            //定义删除按钮方法

            colConstantDel.Click += new EventHandler(colConstantDel_Click);

            btnMainDelete.Click += new EventHandler(btnMainDelete_Click);

        }

        void btnMainDelete_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.bindingSource1.Remove(this.bindingSource1.Current);
            }
        }

        /// <summary>
        /// 删除明细行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void colConstantDel_Click(object sender, EventArgs e)
        {
            if (this.bsConstants.Current != null)
            {
                this.bsConstants.Remove(this.bsConstants.Current);
            }
        }


        /*获取主标单属性列表*/
        private  Dictionary<string,string>  GetMainPropertyList()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();

            //初始化参数值的数据源
            DefaultSequenceActivity parentActivity = WFHelpers.GetRootActivity(_activity) as DefaultSequenceActivity;
            if (parentActivity != null)
            {
                Activity[] acs = WFHelpers.GetAllNestedActivities(parentActivity);
                foreach (Activity a in acs)
                {
                    if (a is ApplicationActivity)
                    {
                        string tname = GetTaskNameFromActivity(a);
                        DataSet ds = GetDataSetFromActivity(a);
                        if (ds != null)
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    if (dc.DataType == typeof(string)
                                        || dc.DataType == typeof(Guid)
                                        || dc.DataType == typeof(int)
                                        || dc.DataType == typeof(int?)
                                        || dc.DataType == typeof(decimal)
                                        || dc.DataType == typeof(decimal?)
                                        || dc.DataType == typeof(DateTime)
                                        || dc.DataType == typeof(DateTime?)
                                        || dc.DataType == typeof(Guid?)
                                        || dc.DataType == typeof(Boolean))
                                    {
                                        string tName = this.Name + "->" + dc.ColumnName;
                                        string tval = tname + "->" + dc.Caption;
                                        if (string.IsNullOrEmpty(tval) == false && !ps.ContainsKey(tName))
                                        {
                                            ps.Add(tName, tval);

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ps;
        }

        /*获取自己关联表单的属性*/
        private Dictionary<string, string> GetSelfPropertyList()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            string tname = GetTaskNameFromActivity(_activity);
            if (SR.IsEnglish)
            {
                ps.Add(WWFConstants.WorkflowNoCode, WWFConstants.WorkflowNo_E);
                ps.Add(WWFConstants.ProposerName_E, WWFConstants.ProposerName_E);
                ps.Add(WWFConstants.CurrentDate_E, WWFConstants.CurrentDate_E);
                ps.Add(WWFConstants.ProposerDepartmentName_C, WWFConstants.ProposerDepartmentName_C);
                ps.Add(WWFConstants.ProposerDepartmentName_E, WWFConstants.ProposerDepartmentName_E);
                ps.Add(WWFConstants.ProposerCompanyName_E, WWFConstants.ProposerCompanyName_E);
                ps.Add(WWFConstants.ProposerCompanyName_E, WWFConstants.ProposerCompanyName_E);


                ps.Add(tname + "->" + WWFConstants.CurrentExcutor_E, tname + "->" + WWFConstants.CurrentExcutor_E);
                ps.Add(tname + "->" + WWFConstants.CurrentFinishDate_E, tname + "->" + WWFConstants.CurrentFinishDate_E);

                ps.Add(WWFConstants.ProposerDepartmentId_E, WWFConstants.ProposerDepartmentId_E);
                ps.Add(WWFConstants.ProposerCompanyId_E, WWFConstants.ProposerCompanyId_E);
            }
            else
            {
                ps.Add(WWFConstants.WorkflowNoCode, WWFConstants.WorkflowNo_C);
                ps.Add(WWFConstants.ProposerName_C, WWFConstants.ProposerName_C);
                ps.Add(WWFConstants.CurrentDate_C, WWFConstants.CurrentDate_C);
                ps.Add(WWFConstants.ProposerDepartmentName_C, WWFConstants.ProposerDepartmentName_C);
                ps.Add(WWFConstants.ProposerDepartmentName_E, WWFConstants.ProposerDepartmentName_E);
                ps.Add(WWFConstants.ProposerCompanyName_C, WWFConstants.ProposerCompanyName_C);
                ps.Add(WWFConstants.ProposerCompanyName_E, WWFConstants.ProposerCompanyName_E);

                ps.Add(tname + "->" + WWFConstants.CurrentExcutorID_C, tname + "->" + WWFConstants.CurrentExcutorID_C);
                ps.Add(tname + "->" + WWFConstants.CurrentExcutor_C, tname + "->" + WWFConstants.CurrentExcutor_C);
                ps.Add(tname + "->" + WWFConstants.CurrentFinishDate_C, tname + "->" + WWFConstants.CurrentFinishDate_C);
                ps.Add(WWFConstants.ProposerDepartmentId_C, WWFConstants.ProposerDepartmentId_C);
                ps.Add(WWFConstants.ProposerCompanyId_C, WWFConstants.ProposerCompanyId_C);
            }


            DataSet ds = GetDataSetFromActivity(_activity);
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {

                        string dcName = tname + "->" + dc.ColumnName;
                        string tval = tname + "->" + dc.Caption;
                        if (string.IsNullOrEmpty(tval) == false && !ps.ContainsKey(dcName))
                        {
                            ps.Add(dcName, tval);
                        }
                    }
                }
            }

            return ps;
        }

        /*获取活动的任务名称*/
        private string GetTaskNameFromActivity(Activity activity)
        {
            if (activity is ApplicationActivity)
            {
                ApplicationActivity aa = activity as ApplicationActivity;
                if (aa != null)
                {
                    if (SR.IsEnglish)
                    {
                        return aa.EName;
                    }
                    else
                    {
                        return aa.CName;
                    }
                }
            }
            else if (activity is ApproveActivity)
            {
                ApproveActivity aa = activity as ApproveActivity;
                if (aa != null)
                {
                    if (SR.IsEnglish)
                    {
                        return aa.EName;
                    }
                    else
                    {
                        return aa.CName;
                    }

                }
            }

            return string.Empty;
        }

      

        /*获取活动对应表单的数据源*/
        private DataSet GetDataSetFromActivity(Activity activity)
        {
            DataSet ds = null;
            FormProfileInfo info = null;

       
            if (activity is ApplicationActivity)
            {
                ApplicationActivity aa = activity as ApplicationActivity;

                if (aa.FormFile==null||string.IsNullOrEmpty(aa.FormFile.ToString()))
                {
                    return null;
                }


                if (aa != null)
                {
                    IWorkFlowConfigService wService = (IWorkFlowConfigService)((IServiceProvider)_typeDescriptorContext.Container).GetService(typeof(IWorkFlowConfigService));
                 
                    if (wService != null)
                    {
                        info = wService.GetFormProfileInfo(aa.FormFile.ToString(), LocalData.IsEnglish);

                        if (info != null)
                        {
                            ds = info.Data;
                        }
                    }
                }
            }
            else if (activity is ApproveActivity)
            {
                ApproveActivity aa = activity as ApproveActivity;
                if (aa != null)
                {

                    if (aa.FormFile == null || string.IsNullOrEmpty(aa.FormFile.ToString()))
                    {
                        return null;
                    }


                    IWorkFlowConfigService wService = (IWorkFlowConfigService)((IServiceProvider)_typeDescriptorContext.Container).GetService(typeof(IWorkFlowConfigService));
                   
                    
                    if (wService != null)
                    {
                        info = wService.GetFormProfileInfo(aa.FormFile.ToString(), LocalData.IsEnglish);

                        if (info != null)
                        {
                            ds = info.Data;
                        } 
                    }

                }
            }

            return ds;
        }



        /*验证*/
        bool ValidateData()
        {
            bool isSucc = true;

            foreach (MappingRelationItem item in bindingSource1.List)
            {
                if (!item.Validate())
                {
                    isSucc=false;
                }
            }


            return isSucc;
           
        }

        #endregion

        private void SetMainWorkItemDataForm_Load(object sender, EventArgs e)
        {

        }
    }

    [Serializable]
    public class MappingRelationItem : BaseDataObject
    {

        [ICP.Framework.CommonLibrary.Common.RequiredAttribute(CMessage = "主表单属性", EMessage = "MainWorkItemField")]
        public string MainWorkItemField { get; set; }

        [ICP.Framework.CommonLibrary.Common.RequiredAttribute(CMessage = "当前表单属性", EMessage = "SelfItemField")]
        public string SelfItemField { get; set; }

        [ICP.Framework.CommonLibrary.Common.RequiredAttribute(CMessage = "模式", EMessage = "Mode")]
        public string RewriteMode { get; set; }
    }

    [Serializable]
    public class FunctionData
    {
        string metodName;
        /// <summary>
        /// 方法名
        /// </summary>
        public string MetodName
        {
            get { return metodName; }
            set { metodName = value; }
        }


        MappingRelationItemCollection mappingRelationItems = new MappingRelationItemCollection();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MappingRelationItemCollection MappingRelationItems
        {
            get { return mappingRelationItems; }
            set { mappingRelationItems = value; }
        }


        MappingRelationItemCollection constantMappingRelationItems = new MappingRelationItemCollection();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MappingRelationItemCollection ConstantMappingRelationItems
        {
            get { return constantMappingRelationItems; }
            set { constantMappingRelationItems = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public void Excute(Guid workflowInstanceId, IWorkflowService wService)
        {


            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            int count = (mappingRelationItems == null ? 0 : mappingRelationItems.Count) + (constantMappingRelationItems == null ? 0 : constantMappingRelationItems.Count);
            string[] mainPropertys = new string[count];
            string[] selfValues = new string[count];
            RewriteMode[] rewritemodes = new RewriteMode[count];

            int i = 0;
            if (mappingRelationItems == null) mappingRelationItems = new MappingRelationItemCollection();
            foreach (MappingRelationItem item in mappingRelationItems)
            {
                mainPropertys[i] = ProcessMainproperty(item.MainWorkItemField);
                selfValues[i] = ProcessSelfProperty(workflowInstanceId, wService, item.SelfItemField);
                rewritemodes[i] = (RewriteMode)Enum.Parse(typeof(RewriteMode), item.RewriteMode);

                i++;
            }

            if (constantMappingRelationItems == null) constantMappingRelationItems = new MappingRelationItemCollection();
            foreach (MappingRelationItem item in constantMappingRelationItems)
            {
                mainPropertys[i] = ProcessMainproperty(item.MainWorkItemField);
                selfValues[i] = item.SelfItemField;
                rewritemodes[i] = (RewriteMode)Enum.Parse(typeof(RewriteMode), item.RewriteMode);

                i++;
            }

          
             wService.SaveMainWorkItemData(workflowInstanceId, mainPropertys, selfValues, rewritemodes, ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId);

  

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

        private string ProcessSelfProperty(Guid workflowInstanceId,IWorkflowService wService,string oldString)
        {
  
            Dictionary<string, object> vals = wService.GetDataCollect(workflowInstanceId).DataCollect;
      

            if (vals == null || vals.Count == 0) return oldString;

            if (vals.ContainsKey(oldString))
            {
                return vals[oldString].ToString();
            }
            else
            {
                return oldString;
            }
         
        }

        public override string ToString()
        {
            return "Function";
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
                    if (d.MainWorkItemField.Equals(name)) return d;
                }

                return null;
            }
            set
            {
                MappingRelationItem pd = null;
                foreach (MappingRelationItem d in this.Items)
                {
                    if (d.MainWorkItemField.Equals(name))
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
                if (d.MainWorkItemField.Equals(name)) return true;
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

    
   
}
