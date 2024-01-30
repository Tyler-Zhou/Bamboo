using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 设置消息内容
    /// </summary>
    public partial class SetMessageContentForm : DevExpress.XtraEditors.XtraForm
    {
        #region 本地变量

        ITypeDescriptorContext _typeDescriptorContext;
        SendMessageActivity _activity;
        SendEMailActivity _eMailactivity;


        #endregion

        #region 资源初始化


        public SetMessageContentForm()
		{
			InitializeComponent();

            if (this.DesignMode == false)
            {
                this.Load += new EventHandler(SetMessageContentForm_Load);
            }
		}

        public SetMessageContentForm(ITypeDescriptorContext typeDescriptorContext, string pamMessage)
            : this()
        {
            _typeDescriptorContext = typeDescriptorContext;
            message = pamMessage;
        }

        void SetMessageContentForm_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {

                InitSystemVariablesList();
                
                txtMessage.Text = message;

                this.treeSystemConstants.DoubleClick += new EventHandler(listSystemConstants_DoubleClick);
            }
        }

        void listSystemConstants_DoubleClick(object sender, EventArgs e)
        {
            if (treeSystemConstants.FocusedNode != null)
            {
                SystemVariablesList list=(SystemVariablesList)treeSystemConstants.GetDataRecordByNode(treeSystemConstants.FocusedNode) as SystemVariablesList;
                if (list.ID > 1)
                {
                    txtMessage.SuspendLayout();

                    string formatString = "{$" + list.Text + "$}";
                    string msg = txtMessage.Text;
                    txtMessage.Text = msg.Insert(txtMessage.SelectionStart, formatString);

                    txtMessage.ResumeLayout();

                    txtMessage.Focus();
                }
            }
        }

        #endregion

        #region 事件处理
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
            message = txtMessage.Text.Trim();
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

        #region 本地方法

        private bool ValidateData()
        {
            bool isSucc = true;
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
            {
                errorProvider1.SetError(txtMessage, SR.GetString("MustInput", "Must input"));
                txtMessage.Focus();
                isSucc = false;
            }

            return isSucc;
        }

        /*界面数据初始化*/
        void InitSystemVariablesList()
        {
            if (_typeDescriptorContext != null)
            {
                _activity = _typeDescriptorContext.Instance as SendMessageActivity;
                if (_activity == null)
                {
                    _eMailactivity = _typeDescriptorContext.Instance as SendEMailActivity;
                }
                if (_activity == null && _eMailactivity == null)
                {
                    return;
                }
                //初始化系统常量树
                IList<SystemVariablesList> SVList=new List<SystemVariablesList>();
                SystemVariablesList systemVariables=new SystemVariablesList();
                systemVariables.ID=1;
                systemVariables.ParentID=0;
                systemVariables.Text=SR.GetString("SystemVariable", "System variable");
                SVList.Add(systemVariables);

                List<string> systemVariablesList = GetSystemVariablesList();

                foreach (string s in systemVariablesList)
                {
                    SystemVariablesList sv = new SystemVariablesList();
                    sv.ID = SVList.Count + 2;
                    sv.ParentID = 1;
                    sv.Text = s;

                    SVList.Add(sv);
                }

                this.treeSystemConstants.DataSource = SVList;
                this.treeSystemConstants.KeyFieldName = "ID";
                this.treeSystemConstants.ParentFieldName = "ParentID";

            }
        }

        /*获取参数的数据源列表*/
        private List<string> GetSystemVariablesList()
        {
            List<string> ps = new List<string>();

            //加入全局变量
            if (SR.IsEnglish)
            {
                ps.Add(WWFConstants.ProposerDepartmentId_E);
                ps.Add(WWFConstants.ProposerCompanyId_E);

                ps.Add(WWFConstants.ProposerName_E);
                ps.Add(WWFConstants.CurrentDate_E);
                ps.Add(WWFConstants.ProposerCompanyFullName_E);
                ps.Add(WWFConstants.ProposerCompanyName_E);


                ps.Add(WWFConstants.ProposerDepartmentFullName_E);
                ps.Add(WWFConstants.ProposerDepartmentName_E);
            }
            else
            {
                ps.Add(WWFConstants.ProposerDepartmentId_C);
                ps.Add(WWFConstants.ProposerCompanyId_C);

                ps.Add(WWFConstants.ProposerName_C);
                ps.Add(WWFConstants.CurrentDate_C);
                ps.Add(WWFConstants.ProposerCompanyFullName_C);
                ps.Add(WWFConstants.ProposerCompanyName_C);


                ps.Add(WWFConstants.ProposerDepartmentFullName_C);
                ps.Add(WWFConstants.ProposerDepartmentName_C);
            }

            //初始化参数值的数据源

            DefaultSequenceActivity parentActivity =null;
            if(_activity!=null)
            {
                parentActivity = WFHelpers.GetRootActivity(_activity) as DefaultSequenceActivity;
            }
            else
            {
                parentActivity = WFHelpers.GetRootActivity(_eMailactivity) as DefaultSequenceActivity;
            }


               
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
                                if (ps.Find(delegate(string p) { return p.Equals(dc.Caption); }) == null)
                                {
                                    string tname = GetTaskNameFromActivity(a);
                                    if (string.IsNullOrEmpty(tname) == false)
                                    {
                                        ps.Add(tname + "->" + dc.Caption);
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



        #endregion

        #region 外部属性


        private string message;
        /// <summary>
        ///  消息内容
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        #endregion 

        private void treeSystemConstants_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            this.treeSystemConstants.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeSystemConstants.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
        }

        void treeSystemConstants_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.treeSystemConstants.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeSystemConstants.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
        }
    }

    /// <summary>
    ///系统变量类
    /// </summary>
    public  class SystemVariablesList
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID{get;set;}
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentID{get;set;}
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Text { get; set; }

    }
}
