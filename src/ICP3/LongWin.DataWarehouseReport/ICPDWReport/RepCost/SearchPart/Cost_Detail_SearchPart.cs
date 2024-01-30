﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using LongWin.Framework.Client;
using Agilelabs.Framework.Service;

using LongWin.DataWarehouseReport.ServiceInterface;
using LongWin.CommonData.ServiceInterface;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class Cost_Detail_SearchPart : UserControl
    {
        #region 服务注入
        IREPBaseDataService _repBaseDataService;
        [ServiceDependency]
        public IREPBaseDataService RepBaseDataService
        {
            set { this._repBaseDataService = value; }
        }

        string finderType = string.Empty;

        IDataFinder dataFinder = null;

        IDataFinderFactory _finderFactory;
        [ServiceDependency]
        public IDataFinderFactory FinderFactory
        {
            get { return _finderFactory; }
            set { _finderFactory = value; }
        }

        WorkItem _workItem;
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get { return _workItem; }
            set { _workItem = value; }
        }

        ITransportFoundationService _transportFoundationService;
        [ServiceDependency]
        public ITransportFoundationService TransportFoundationService
        {
            get { return _transportFoundationService; }
            set { _transportFoundationService = value; }
        }
        IInitializeService _initializeService;
        [ServiceDependency]
        public IInitializeService InitializeService
        {
            get { return _initializeService; }
            set { _initializeService = value; }
        }
        
        #endregion

        #region 初始化
        public Cost_Detail_SearchPart()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(Cost_Detail_SearchPart_Disposed);
        }

        void Cost_Detail_SearchPart_Disposed(object sender, EventArgs e)
        {
            this._workItem.Items.Remove(this);
        }

        void Cost_Detail_SearchPart_Load(object sender, System.EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (_repBaseDataService == null || _transportFoundationService == null)
                {
                    //MessageBox.Show("服务为空,请检查");
                    _repBaseDataService = REPModuleInit.RepService;
                    _transportFoundationService = REPModuleInit.FoundationService;
                    //return;
                }
                this.ucSelectCompany.DataSource = this._repBaseDataService.GetStructureNodesByCurrentUser();
                this.ucSelectCostItem.DataSource = Utility.GetCostItem(this._transportFoundationService.GetAllCostItems());

                if (!this._repBaseDataService.GetUserIsManange())
                {
                    this.txtSales.Enabled = false;
                    this.txtSales.SelectedText = _initializeService.GetUserInfo().DispalyName;
                    txtSales.SelectedValue = new Guid[] { new Guid(_initializeService.GetUserInfo().UserId) };
                }


                if (Utility.IsEnglish)
                {
                    this.ultraExplorerBarContainerControl1.Size = new Size(225, 206);
                }
                else
                {
                    this.ultraExplorerBarContainerControl1.Size = new Size(225, 130);
                }

                //this.ultraExplorerBar1
                this.ultraExplorerBar1.Groups[0].Text = Utility.GetValueString("日期", "日期");
                this.ultraExplorerBar1.Groups[1].Text = Utility.GetValueString("常规", "常规");
                                
            }
        }
        
        #endregion 

        #region 事件
        public event EventHandler SearchResult;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ucSelectCompany.Value == null)
            {
                MessageBox.Show("请输入组织结构！");
                return;
            }
            else if (this.SearchResult != null)
            {
                this.SearchResult(sender, e);
            }
        }
        #endregion

        #region 属性

        #region 时间范围
        /// <summary>
        /// 开始时间        /// </summary>
        public DateTime BeginTime
        {
            get { return this.dateTimeCostCtl1.DateTimeFrom; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return this.dateTimeCostCtl1.DateTimeTo; }
        }
        #endregion

        #region 组织结构
        /// <summary>
        /// 组织结构的Id
        /// </summary>
        public string StructNodeId
        { get { return this.ucSelectCompany.Value.ToString(); } }
        /// <summary>
        /// 组织结构的名字
        /// </summary>
        public string StructureNodeName
        {
            get { return this.ucSelectCompany.Text; }
        }
        #endregion

        #region 费用项目
        /// <summary>
        /// 费用项目的Id
        /// </summary>
        public Guid CostItemID
        {
            get
            {
                if (this.ucSelectCostItem.Value != null)
                {
                    return new Guid(this.ucSelectCostItem.Value.ToString());
                }
                return Guid.Empty;
            }
        }
        /// <summary>
        /// 费用项目的名称        /// </summary>
        public string CostItemName
        {
            get
            {
                return this.ucSelectCostItem.Text.Trim();
            }
        }
        #endregion

        #region 业务员
        private string _employeeIds;
        /// <summary>
        /// 业务员
        /// </summary>
        public string EmployeeIDs
        {
            get
            {
                _employeeIds = string.Empty;
                if (this.txtSales.SelectedValue != null && !string.IsNullOrEmpty(this.txtSales.SelectedValue.ToString()))
                {
                    Guid[] customerIds = (Guid[])this.txtSales.SelectedValue;
                    foreach (Guid id in customerIds)
                    {
                        _employeeIds += "'" + id.ToString() + "',";
                    }
                }
                if (_employeeIds.EndsWith(","))
                {
                    _employeeIds = _employeeIds.Substring(0, _employeeIds.Length - 1);
                }
                return _employeeIds;
            }
        }
        /// <summary>
        /// 业务员姓名
        /// </summary>
        public string EmployeeNames
        {
            get { return txtSales.SelectedText; }
        }
        #endregion

        #region 附加信息
        /// <summary>
        /// 期间
        /// </summary>
        public string Peried
        {
            get { return this.dateTimeCostCtl1.DateTimeFrom.ToShortDateString() + "-" + this.dateTimeCostCtl1.DateTimeTo.ToShortDateString(); }
        }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string ConditionRemark
        {
            get
            {
                string condition = "";
                condition +=Utility.GetValueString("部门", "部门") +"  : "+this.ucSelectCompany.Text+";";
                
                if (this.ucSelectCostItem.Text != string.Empty)
                {
                    condition += "费用项目：" + this.ucSelectCostItem.Text + ";";
                }
                if (condition.EndsWith(";"))
                {
                    condition = condition.Substring(0, condition.Length - 1);
                }
                return condition;

            }
        }
        #endregion 


        private void txtSales_DoSearching(object sender, SearchEventArgs e)
        {
            finderType = RepConst.FindUserKey;
            Guid[] guids = new Guid[] { };
            if (e.SelectedValue != null)
            {
                guids = (Guid[])e.SelectedValue;
            }
            this.DoSearching("Name", guids);
        }

       

        void DoSearching(string searchField, Guid[] exitsValues)
        {
            this.dataFinder = this._finderFactory.GetDataFinder(finderType);

            dataFinder.DataChoosed += delegate(object sender, DataFindEventArgs arg)
            {
                this.BindSelectData(arg.Data);
                this.dataFinder.Unwrap.FindForm().Close();
            };
            if (this.finderType == RepConst.FindCustomerKey)
            {
                string[] returnFields = new string[] { "Id", "CName", "EName" };
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
                string info = Utility.IsEnglish == false ? "客户查找" : "Customer Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info,info));
            }
            else
            {
                string[] returnFields = new string[] { "Id", "Name" };
                Control ctl = this.dataFinder.PickMany(returnFields, exitsValues);
                string info = Utility.IsEnglish == false ? "用户查找" : "User Search";
                this._workItem.Workspaces[RepConst.mainWorkspace].Show(ctl, new SmartPartInfo(info,info));
            }
        }

       

        void BindSelectData(object[] result)
        {
            if (result != null)
            {
                if (this.finderType == RepConst.FindUserKey)
                {
                    string userText = string.Empty;
                    Guid[] userIds = new Guid[result.Length];
                    for (int i = 0; i < result.Length; i++)
                    {
                        userText += (result[i] as object[])[1].ToString() + ",";

                        userIds[i] = (Guid)((result[i] as object[])[0]);
                    }

                    this.txtSales.SelectedText = userText;
                    this.txtSales.SelectedValue = userIds;
                }
            }
        }
        
        #endregion      
    }
}
