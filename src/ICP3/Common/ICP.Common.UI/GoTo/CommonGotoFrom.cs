using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.Common.UI.GoTo
{
    public partial class CommonGotoFrom : DevExpress.XtraEditors.XtraForm
    {
        #region 服务和变量
        /// <summary>
        /// 需要跳转的窗口
        /// </summary>
        public string Wicket = string.Empty;
        /// <summary>
        /// 当前业务类别
        /// </summary>
        public string Type = string.Empty;

        /// <summary>
        /// 是否查询最近业务
        /// </summary>
        public int Opd;

        public ICP.Common.ServiceInterface.ICommonGoTo CommonGo
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICommonGoTo>();
            }
        }
        /// <summary>
        /// 是否点击的更多按钮
        /// </summary>
        public bool Verdict = false;
        /// <summary>
        /// 设置实体的对象
        /// </summary>
        GotoSetting _setting = new GotoSetting();
        /// <summary>
        /// 设置窗口
        /// </summary>
        private CommonSetting _commonSetting = null;
        /// <summary>
        /// 业务状态
        /// </summary>
        public string FormType = string.Empty;

        public GoToControlReadXml ReadXml;
        /// <summary>
        /// 读取Goto.xml文件配置信息类
        /// </summary>
        public GoToControlReadXml GoToControlReadXml
        {
            get { return ReadXml ?? (ReadXml = new GoToControlReadXml()); }
            set { ReadXml = value; }
        }

        #endregion

        public CommonGotoFrom()
        {
            InitializeComponent();
            if (!LocalData.IsEnglish)
            {
                SetCnText();
            }
        }


        #region 窗体的事件
        /// <summary>
        ///  窗体接收键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonGotoFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataBind(txtQuery.Text.Trim());
            }
            if (e.KeyCode == Keys.Escape)
            {
                CloseFrom();
            }
        }
        /// <summary>
        /// 窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonGotoFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseFrom();

        }
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonGotoFrom_Load(object sender, EventArgs e)
        {
            this.SetBounds((Screen.GetBounds(this).Width / 2) - (this.Width / 2), (Screen.GetBounds(this).Height / 2) - (this.Height / 2), this.Width, this.Height, BoundsSpecified.Location);
            this.Width = 453;
            this.Height = 170;
            txtQuery.Text = string.Empty;
            Verdict = false;
            guidControlList.Visible = false;
            txtQuery.Focus();
            StructureControls();
        }

        #endregion

        #region 根据业务的类别对列表绑定数据

        /// <summary>
        /// 根据业务的类别对列表绑定数据
        /// </summary>
        /// <param name="query">查询条件</param>
        public void DataBind(string query)
        {
            var common = new List<GoToObject>();
            //点击更多按钮以后不改变窗体的大小
            if (Verdict == false)
            {
                this.Width = 453;
                this.Height = 378;
                this.tablePanel.Location = new System.Drawing.Point(14, 3);
                //this.guidControlList.Location = new System.Drawing.Point(14, 112);
            }
            this.guidControlList.BeginUpdate();
            this.gridViewList.BeginUpdate();

            SelectWicket();
            Opd = cbRecentShipments.Checked ? 0 : 1;
            int theradID = 0;
            try
            {
                #region  对于数据的逻辑处理（是否显示最近的数据）
                string tip = LocalData.IsEnglish ? "Querying Data..." : "正在查询数据。。。";
                theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(tip);
                //获取当前用户的所有的组织架构信息
                string companyId = LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).Select(o => o.ID.ToString()).Aggregate((a, b) => a + "&#;" + b).ToString();
                common = CommonGo.GetGoToList(Type, query, companyId, Opd);
                if (common != null && common.Count > 0)
                {
                    //单条记录的处理
                    if (common.Count == 1)
                    {
                        ClientConvertform(common.FirstOrDefault());
                    }
                    else if (common.Count > 0)
                    {
                        guidControlList.Visible = true;
                        bindDataSource.DataSource = common;
                        bindDataSource.ResetBindings(false);
                        this.guidControlList.EndUpdate();
                        this.gridViewList.EndUpdate();
                    }
                }
                else
                {
                    if (common != null) common.Clear();
                    var message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                    MessageBox.Show(message);
                }
                #endregion

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
            finally
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            }
        }

        #endregion

        #region   方法
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public void CloseFrom()
        {
            txtQuery.Text = string.Empty;
            Verdict = false;
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetCnText()
        {

            ContainerNo.Caption = "箱号";
            OperationNo.Caption = "订单号";

        }
        /// <summary>
        /// 根据条件打开窗体
        /// </summary>
        /// <param name="goTo">当前实体对象</param>
        public void ClientConvertform(GoToObject goTo)
        {
            SelectWicket();
            CloseFrom();
            GoToControlReadXml.Convertform(goTo, Wicket);
        }

        /// <summary>
        /// 根据当前的选中的控件信息生成对应的搜索条件
        /// </summary>
        public void SelectWicket()
        {
            Wicket = string.Empty;
            Type = string.Empty;
            foreach (var gc in tablePanel.Controls)
            {
                if (!(gc is RadioButton)) continue;
                if (!((RadioButton)gc).Checked) continue;
                Wicket = ((RadioButton)gc).Text;
                Type = ((RadioButton)gc).Text.Split('-')[0];
                FormType = ((RadioButton)gc).Text.Split('-')[1];
            }

        }
        /// <summary>
        /// 构造选择范围
        /// </summary>
        public void StructureControls()
        {
            if (!string.IsNullOrEmpty(txtQuery.Text))
            {
                DataBind(txtQuery.Text);
            }
            if (tablePanel == null || tablePanel.IsDisposed)
            {
                tablePanel = new TableLayoutPanel();
            }
            tablePanel.Controls.Clear();
            bool flg = false;
            var getSettingScope = GoToControlReadXml.GetSettingScope().Split(',');

            if (getSettingScope.Length <= 1)
            {
                this.tablePanel.RowCount = 3;
                this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
                this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
                this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
                //获取当前配置文件中的信息
                var xml = GoToControlReadXml.GetGoToControlList().OrderBy(n => n.Order);
                // 取结果结合中最前3条信息
                var countXml = (from x in xml where x.Order <= 3 select x).ToList();
                foreach (var control in countXml)
                {
                    var radio = new RadioButton
                    {
                        Name = control.Name.Replace("-", string.Empty).Replace(" ", string.Empty),
                        Text = control.Name,
                        TabIndex = control.Order + 2,
                        UseVisualStyleBackColor = true,
                        Size = new System.Drawing.Size(96, 18)
                    };
                    if (flg == false)
                    {
                        radio.Checked = true;
                        flg = true;
                    }
                    this.tablePanel.Controls.Add(radio, 0, control.Order + 1);
                }
            }
            else
            {

                this.tablePanel.RowCount = getSettingScope.Length;
                if (getSettingScope.Length > 3)
                {
                    this.Width = 453;
                    this.Height = 580;
                    tablePanel.Location = new System.Drawing.Point(16, 29);
                    this.guidControlList.Location = new System.Drawing.Point(9, 378);
                }
                Array.Sort(getSettingScope);
                for (var j = 0; j < getSettingScope.Length; j++)
                {
                    this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
                    var radio = new RadioButton
                    {
                        Name = getSettingScope[j].Replace("-", string.Empty).Replace(" ", string.Empty),
                        Text = getSettingScope[j],
                        TabIndex = j + 2,
                        UseVisualStyleBackColor = true,
                        Size = new System.Drawing.Size(96, 18)
                    };
                    if (flg == false)
                    {
                        radio.Checked = true;

                        flg = true;
                    }

                    this.tablePanel.Controls.Add(radio, 0, j + 1);
                }
            }
        }
        #endregion

        #region 页面按钮
        /// <summary>
        /// GoTo 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGoTo_Click(object sender, EventArgs e)
        {
            DataBind(txtQuery.Text.Trim());
        }

        /// <summary>
        /// 更多按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkmore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Verdict = true;
            bool flg = false;
            if (tablePanel == null || tablePanel.IsDisposed)
            {
                tablePanel = new TableLayoutPanel();
            }
            tablePanel.Controls.Clear();
            this.Width = 453;
            this.Height = 580;
            this.guidControlList.Location = new System.Drawing.Point(9, 378);

            //获取配置文件中的信息
            var getSettingScope = GoToControlReadXml.GetSettingScope().Split(',');
            if (getSettingScope.Count() > 3)
            {
                for (int i = 0; i < getSettingScope.Count(); i++)
                {
                    this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
                    var radio = new RadioButton
                   {
                       Name = getSettingScope[i].Replace("-", string.Empty),
                       Text = getSettingScope[i],
                       TabIndex = i + 2,
                       UseVisualStyleBackColor = true,
                       Size = new System.Drawing.Size(96, 18)
                   };
                    if (flg == false)
                    {
                        radio.Checked = true;
                        flg = true;
                    }
                    this.tablePanel.Controls.Add(radio, 0, i + 1);
                }
            }
        }
        /// <summary>
        /// 设置按钮
        /// </summary>
        private void linkSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            if (_commonSetting == null || _commonSetting.IsDisposed)
            {
                _commonSetting = new CommonSetting();
            }
            _commonSetting.ShowDialog();
        }
        #endregion

        #region GridView 单击行触发事件
        /// <summary>
        /// 选中行时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var common = gridViewList.GetRow(e.RowHandle) as GoToObject;
                ClientConvertform(common);
            }
        }

        #endregion



    }
}
