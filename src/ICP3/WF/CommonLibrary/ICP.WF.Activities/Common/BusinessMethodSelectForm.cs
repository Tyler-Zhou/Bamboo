using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.WF.Activities;
using System.Workflow.ComponentModel;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using DevExpress.Data;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Activities.Common
{
    /// <summary>
    /// 业务方法选择界面
    /// </summary>
    public partial class BusinessMethodSelectForm : DevExpress.XtraEditors.XtraForm
    {
        #region 本地变量

        ITypeDescriptorContext _typeDescriptorContext;
        string _selectedMetodName;
        LWCallExternalMethodActivity _activity;

        #endregion

        #region 资源初始化与释放

        public BusinessMethodSelectForm()
		{
			InitializeComponent();
		}

        public BusinessMethodSelectForm(ITypeDescriptorContext typeDescriptorContext, string selectedMetodName)
            : this()
        {
            _typeDescriptorContext = typeDescriptorContext;
            _selectedMetodName = selectedMetodName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode == false)
            {
                //ICP.Framework.ClientComponents.IMEControl.SetIme(this);

                InitMethodTree();
            }
        }

        #endregion


        #region 事件处理

        #region 按钮事件
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
            {
                return;
            }

            dgvParams.EndUpdate();
            bsParams.EndEdit();

            if (this.treeMetods.FocusedNode != null)
            {
                MethodData data = treeMetods.GetDataRecordByNode(this.treeMetods.FocusedNode) as MethodData;
                if (data != null)
                {
                    _selectedMethod = data;
                }
            }


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


        #region 树事件

        /// 选择树的节点发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMetods_AfterSelect(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (this.treeMetods.FocusedNode ==null)
            {
                return;
            }

            this.treeMetods.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMetods.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

            MethodData data = treeMetods.GetDataRecordByNode(this.treeMetods.FocusedNode) as MethodData;
            if (data != null)
            {
                //刷新右边的参数列表
                bsParams.DataSource = data.Parameters;
                bsParams.ResetBindings(false);
                //方法描述信息
                txtMethodDesc.Text = data.ToDescString();

            }
               
        }
        #endregion
        private void dgvParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                //dgvParams[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;
                e.Cancel = true;
            }
            else
            {
               // dgvParams[e.ColumnIndex, e.RowIndex].ErrorText = string.Empty;
            }
        }
        #endregion


        #region 本地方法

        /*验证*/
        bool ValidateData()
        {
            bool isSucc = true;

            for (int i = 0; i <= this.gridView1.RowCount; i++)
            { 
                
            }
                //foreach (DataGridViewRow row in dgvParams)
                //{
                //    row.Cells[parameterOriginalValueDataGridViewTextBoxColumn.Name].ErrorText = string.Empty;
                //    if (row.Cells[parameterOriginalValueDataGridViewTextBoxColumn.Name].Value == null
                //        || string.IsNullOrEmpty(row.Cells[parameterOriginalValueDataGridViewTextBoxColumn.Name].Value.ToString()))
                //    {
                //        row.Cells[parameterOriginalValueDataGridViewTextBoxColumn.Name].ErrorText = SR.GetString("MustInput", "必须填写!");
                //        isSucc = false;
                //    }

                //    ParameterData data = row.DataBoundItem as ParameterData;
                //    if (data != null)
                //    {
                //        ParameterSourceData item = (parameterOriginalValueDataGridViewTextBoxColumn.DataSource as List<ParameterSourceData>).Find(delegate(ParameterSourceData p) { return p.ParameterName.Equals(data.ParameterOriginalValue); });
                //        if (item != null)
                //        {
                //            if (item.ParameterTypes.Contains(data.ParameterType) == false)
                //            {
                //                row.Cells[parameterOriginalValueDataGridViewTextBoxColumn.Name].ErrorText = SR.GetString("TypeNotMatch", "类型不匹配!");
                //                isSucc = false;
                //            }
                //        }
                //    }
                //}
                return isSucc;
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        void InitMethodTree()
        {
            if (_typeDescriptorContext != null)
            {
                _activity = _typeDescriptorContext.Instance as LWCallExternalMethodActivity;
                if (_activity == null) return;

                IMethodFindProvider provider = _activity as IMethodFindProvider;
                if (provider == null)
                {
                    return;
                }
                _selectedMethod = _activity.Method;

                #region 初始化方法树

                List<MethodData> methods = provider.GetPropertyValues(_typeDescriptorContext);

                MethodData data = new MethodData();
                data.ParentID = 0;
                data.ID = 1;
                data.AliasName = SR.GetString("BusinessInterface", "业务交互接口");
                data.MetodName = "BusinessInterface";

                methods.Add(data);

            


                if (_selectedMethod != null)
                {
                    methods.Remove(methods.Find(delegate(MethodData m) { return m.MetodName == _selectedMethod.MetodName; }));
                    methods.Insert(0, _selectedMethod);
                }


                int i = 1;
                foreach (MethodData m in methods)
                {
                    if (m.ID == 1)
                    {
                        continue;
                    }
                    i++;
                    m.ID = i;
                    m.ParentID = 1;
                }

                treeMetods.DataSource = methods;
                treeMetods.KeyFieldName = "ID";
                treeMetods.ParentFieldName = "ParentID";

                #endregion

                //初始化参数值的数据源
                List<ParameterSourceData> dataList= GetParamNameList();
                foreach (ParameterSourceData psData in dataList)
                {
                    if (LocalData.IsEnglish)
                    {
                        imgCombBox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(psData.EName,psData.ID.ToString()));
                    }
                    else
                    {
                        imgCombBox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(psData.ParameterName, psData.ID.ToString()));
                    }
                }


              //选择树节点
                if (_selectedMethod != null)
                {
                    TreeListNode node = treeMetods.FindNodeByFieldValue("MetodName", _selectedMethod.MetodName);
                    if (node != null)
                    {
                        treeMetods.FocusedNode = node;
                    }
                }

            }
        }

        /*获取参数的数据源列表*/
        private List<ParameterSourceData> GetParamNameList()
        {
            List<ParameterSourceData> ps = new List<ParameterSourceData>();
            
            //加入全局变量
            ps = ParameterSourceData.GetList();

            //初始化参数值的数据源
            DefaultSequenceActivity parentActivity = WFHelpers.GetRootActivity(_activity) as DefaultSequenceActivity;
            if (parentActivity != null)
            {
                Activity[] acs = WFHelpers.GetAllNestedActivities(parentActivity);
                foreach (Activity a in acs)
                {
                    DataSet ds = GetDataSetFromActivity(a);
                    if (ds != null)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            foreach (DataColumn dc in dt.Columns)
                            {
                                if (LocalData.IsEnglish)
                                {
                                    if (ps.Find(delegate(ParameterSourceData p) { return p.EName.Equals(dc.Caption); }) == null)
                                    {
                                        string tname = GetTaskNameFromActivity(a);
                                        string strCaption = tname + "->" + dc.Caption;
                                        string strName = tname + "->" + dc.ColumnName;
                                        if (string.IsNullOrEmpty(tname) == false)
                                        {
                                            ps.Add(new ParameterSourceData(strName, strCaption, strCaption, ParameterSourceData.CanConvertType(dc.DataType)));
                                        }
                                    }
                                }
                                else
                                {
                                    if (ps.Find(delegate(ParameterSourceData p) { return p.ParameterName.Equals(dc.Caption); }) == null)
                                    {
                                        string tname = GetTaskNameFromActivity(a);
                                        string strCaption = tname + "->" + dc.Caption;
                                        string strName = tname + "->" + dc.ColumnName;
                                        if (string.IsNullOrEmpty(tname) == false)
                                        {
                                            ps.Add(new ParameterSourceData(strName, strCaption, strCaption, ParameterSourceData.CanConvertType(dc.DataType)));
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


                if (aa.FormFile == null || string.IsNullOrEmpty(aa.FormFile.ToString()))
                {
                    return null;
                }
                if (aa != null)
                {
                    IWorkFlowConfigService wService = (IWorkFlowConfigService)((IServiceProvider)_typeDescriptorContext.Container).GetService(typeof(IWorkFlowConfigService));

                    if (wService != null)
                    {
                        info = wService.GetFormProfileInfo(aa.FormFile.ToString(),LocalData.IsEnglish);
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





        /*当前行*/
        ParameterData CurrentRow
        {
            get
            {
                if (bsParams.Count > 0 && bsParams.Current != null)
                {
                    return bsParams.Current as ParameterData;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion


        #region 外部接口

        MethodData _selectedMethod;

        /// <summary>
        /// 当前选择的业务方法
        /// </summary>
        public MethodData SelectMethod
        {
            get { return _selectedMethod; }
            set { _selectedMethod = value; }
        }

        #endregion
        /// <summary>
        /// <summary>
        /// 选择的单元行发生改变时，绑定描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsParams_CurrentChanged(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                txtParameterDesc.Text = CurrentRow.ToString();
            }
            else
            {
                txtParameterDesc.Text = string.Empty;
            }
        }

        #region 设置Tree Style

        private void treeMetods_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            this.treeMetods.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMetods.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

        }
        #endregion
    }
}
