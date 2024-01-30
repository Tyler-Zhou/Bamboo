using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ICP.MailCenter.Business.UI
{
    public partial class WorkFlowBusinessQueryPart : XtraUserControl,IBusinessQueryPart
    {
        public WorkFlowBusinessQueryPart()
        {
            InitializeComponent();
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 工作流配置管理服务
        /// </summary>
        public IWorkFlowConfigService WorkFlowConfigService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowConfigService>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
             }
        }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }

        }
        #endregion

        #region 重写
        private ICP.Framework.CommonLibrary.Common.BusinessType type = ICP.Framework.CommonLibrary.Common.BusinessType.WF;
        [DefaultValue(ICP.Framework.CommonLibrary.Common.BusinessType.WF)]
        public  BusinessType Type
        {
            get
            {
                return type;
            }
        }

        // 摘要: 
        //     获取查询结果
        public  string GetAdvanceQueryString()
        {
            string queryString = " 1 = 1  ";
            if (DataTypeHelper.GetGuid(stxtOrganization.EditValue, Guid.Empty) != Guid.Empty)
            {
                queryString += " and $@Department@ = '" + DataTypeHelper.GetGuid(stxtOrganization.EditValue, Guid.Empty).ToString() + "'";
            }
            if (DataTypeHelper.GetString(this.cmbWorkFlowCode.EditValue, string.Empty) != string.Empty)
            { 
                queryString+=" and $@WorkFlow@='"+DataTypeHelper.GetString(this.cmbWorkFlowCode.EditValue)+"'";
            }
            if (this.txtNo.Text != string.Empty)
            {
                queryString += " and $@No@ = '"+this.txtNo.Text.Trim()+"'";
            }
            if (this.txtWorkName.Text != string.Empty)
            {
                queryString += " and $@WorkName@ ='" + this.txtWorkName.Text.Trim() + "'";
            }
            if (this.grxType.SelectedIndex >= 0)
            {
                queryString += " and $@Type@='"+this.grxType.SelectedIndex.ToString()+"'";
            }
            if (this.dateMonthControl1.radioGroup1.SelectedIndex > 0)
            {
                queryString += " and $@StartDate@>=''" + this.dateMonthControl1.dteFrom.ToString() + "' and $@EndDate@<='" + this.dateMonthControl1.dteTo.ToString() + "'";
            }
            if (DataTypeHelper.GetString(this.txtApplyName.EditValue,string.Empty)!= string.Empty)
            {
                queryString += " and $@ApplyBy@='" + DataTypeHelper.GetString(this.txtApplyName.EditValue, string.Empty) + "'";
            }
           string stateList=string.Empty;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chcState.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                { 
                    if(stateList==string.Empty)
                    {
                        stateList="'"+item.Value.ToString()+"'";
                    }
                    else
                    {
                          stateList=stateList+",'"+item.Value.ToString()+"'";
                    }
                }
            }
            if (stateList!=string.Empty)
            {
                queryString += " and $@State@ in(" + stateList + ")";
            }


            return queryString;
        }
        public void Init(Dictionary<string, object> initValues)
        { 
            
        }
        public  void Locale()
        {
            if (LocalData.IsEnglish)
            {
                this.labStatus.Text = "Status" ;
                this.labApplyName.Text = "ApplyBy" ;
                this.labWorkName.Text = "WorkName";
                this.labNo.Text = "No";
                this.labDepartment.Text = "Department";
                this.labWorkFlow.Text = "WorkFlow";
                this.labTo.Text = "To";
                this.labBegin.Text = "Start";
                this.gcDate.Text = "Date";
                this.gcType.Text = "Type";
            }
        }
        public  void Reset()
        {
            this.txtApplyName.EditValue = null;
            this.txtNo.Text = string.Empty;
            this.txtWorkName.Text = string.Empty;
            
            this.cmbWorkFlowCode.EditValue = null;
            this.cmbWorkFlowCode.Text = string.Empty;

            this.stxtOrganization.EditValue = null;
            this.stxtOrganization.Text = string.Empty;

            this.grxType.SelectedIndex = 0;

            this.dateMonthControl1.radioGroup1.SelectedIndex = 0;

            this.chcState.SelectAll();

        }
        #endregion

        #region 加载
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                Locale();
            }
        }
        private void InitControls()
        {
            #region 初始化流程下拉框
            this.cmbWorkFlowCode.OnFirstEnter += this.OncmbWorkFlowCodeFirstEnter;

            #endregion


            #region 初始化部门下拉框

             DataFindClientService.RegisterMiniFinder(stxtOrganization, SystemFinderConstants.OrganizationFinder, @"Code/Name"
                            , LocalData.IsEnglish ? "EShortName" : "CShortName", "ID", new string[] { "ID", "EShortName", "CShortName" },
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtOrganization.Text = LocalData.IsEnglish ? resultData[1].ToString() : resultData[2].ToString();
                      stxtOrganization.Tag = new Guid(resultData[0].ToString());
                  }, null);

            #endregion

            #region  绑定用户
            this.txtApplyName.OnFirstEnter += this.OntxtApplyNameFirstEnter;

            #endregion

            #region 状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<WorkItemSearchStatus>> masterStatus = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<WorkItemSearchStatus>(LocalData.IsEnglish);
            this.chcState.Properties.BeginUpdate();
            this.chcState.Properties.Items.Clear();
            foreach (var item in masterStatus)
            {
                if (item.Value != WorkItemSearchStatus.All)
                {
                        chcState.Properties.Items.Add(item.Value, item.Name, CheckState.Checked, true);
                }
            }
            this.chcState.Properties.EndUpdate();

            #endregion
        }
        public Guid NewID = new Guid("E4765E59-453D-4C0E-B457-BD6E28341339");
        private void OncmbWorkFlowCodeFirstEnter(object sender, EventArgs e)
        {
            List<WorkFlowConfigInfo> wfList = new List<WorkFlowConfigInfo>();
            byte[] wfitems = WorkFlowConfigService.GetWorkFlowConfigListZip(null, LocalData.IsEnglish);
            wfList = (List<WorkFlowConfigInfo>)DataZipStream.DecompressionArrayList(wfitems);
            if (wfList == null)
            {
                wfList = new List<WorkFlowConfigInfo>();
            }
            List<WorkFlowTypeCS> dataList = new List<WorkFlowTypeCS>();

            var infoList = from c in wfList

                           group c by c.CategoryName into g
                           select new { Key = g.Key, Info = g };

            foreach (var s in infoList)
            {
                WorkFlowTypeCS cs = new WorkFlowTypeCS();
                cs.ID = Guid.NewGuid();
                cs.ParentID = NewID ;
                cs.Name = s.Key;
                dataList.Add(cs);

                foreach (var t in s.Info)
                {
                    WorkFlowTypeCS csDetail = new WorkFlowTypeCS();
                    csDetail.ID = t.Id;
                    csDetail.ParentID = cs.ID;
                    csDetail.Name = LocalData.IsEnglish ? t.EDescription : t.CDescription;
                    dataList.Add(csDetail);
                }
            }

            WorkFlowTypeCS allCS = new WorkFlowTypeCS();
            allCS.ID = NewID;
            allCS.Name = LocalData.IsEnglish ? "All" : "全部";
            allCS.ParentID = null;
            dataList.Add(allCS);

            cmbWorkFlowCode.DataSource = dataList;
            cmbWorkFlowCode.ParentMember = "ParentID";
            cmbWorkFlowCode.ValueMember = "ID";
            cmbWorkFlowCode.DisplayMember = "Name";
        }
        private void OntxtApplyNameFirstEnter(object sender, EventArgs e)
        {
            List<UserList> userList = UserService.GetUserListByList(null, null, null, null, null, null, true, true, 0);

            Dictionary<string, string> col = new Dictionary<string, string>();
            if (LocalData.IsEnglish)
            {
                col.Add("EName", "Name");
                col.Add("Code", "Code");
            }
            else
            {
                col.Add("CName", "名称");
                col.Add("Code", "代码");
            }

            txtApplyName.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "Code");
        }
        #endregion

        #region 上次查询
        //读取模版的路径
        private readonly string _fileRootDirectory = Path.Combine(LocalData.MainPath, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "QueryConditions.xml";
        /// <summary>
        /// 获取当前用户的上一次的查询条件
        /// </summary>
        /// <returns></returns>
        public string SetQueryConditions()
        {
            string stroriginal = string.Empty;
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmldocment = from ent in document.Descendants("Item") select ent;
            if (xmldocment.Any())
            {
                foreach (var xElement in xmldocment)
                {
                    Guid Userid = new Guid(xElement.Attribute("UserId").Value);
                    if (Userid == LocalData.UserInfo.LoginID)
                    {
                        stroriginal = xElement.Attribute("Stroriginal").Value;
                    }
                }
            }
            return stroriginal;
        }
        /// <summary>
        /// 初始化上一次查询结果集合的赋值操作
        /// </summary>
        /// <param name="initValues"></param>
        public void LastAdvanceQueryString(Dictionary<string, object> initValues)
        {
            string stroriginal = SetQueryConditions();
            if (!string.IsNullOrEmpty(stroriginal))
            {
                var strReplace = stroriginal.Replace("and", "*").Replace("1=1", string.Empty);
                var strSplit = strReplace.Split('*');
                foreach (string str in strSplit)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str.Contains("$@Department@"))
                        {
                            this.stxtOrganization.EditValue = Strreplace(str);
                        }
                        if (str.Contains("$@WorkFlow@"))
                        {
                            this.cmbWorkFlowCode.EditValue = Strreplace(str);
                        }
                        if (str.Contains("$@No@"))
                        {
                            this.txtNo.Text = Strreplace(str);
                        }
                        if (str.Contains("$@WorkName@"))
                        {
                            this.txtWorkName.Text = Strreplace(str);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返回替换好的字符串
        /// </summary>
        /// <param name="replace">替换的字符串</param>
        /// <returns></returns>
        public string Strreplace(string replace)
        {
            if (replace.Contains("like"))
            {
                replace = replace.Replace("%", string.Empty).Replace("'", string.Empty)
                          .Replace("like", "?");

            }
            else if (replace.Contains("in"))
            {
                replace = replace.Replace("(", string.Empty).Replace("'", string.Empty)
                                 .Replace(")", string.Empty).Replace("in", "?");

            }
            else
            {
                replace = replace.Replace("'", string.Empty).Replace("=", "?");

            }
            string str = replace.Split('?')[1];
            if (str != null)
            {
                str = str.Trim();
            }
            return str;
        }

        #endregion
    }
}
