using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Framework.ClientComponents.Controls;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.OA.UI.UserInformation
{
    public partial class UserInfoMainWorkSpace : BasePart
    {
        /// <summary>
        /// Workitem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
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
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }

        static string culture = Thread.CurrentThread.CurrentCulture.Name;
        static bool isenglish = culture.ToLower().Contains("en");
        public UserInfoMainWorkSpace(WorkItem _Workitem)
        {
            Workitem = _Workitem;
            InitializeComponent();
            this.Load += UserInfoMainWorkSpace_Load;
            this.Disposed += UserInfoMainWorkSpace_Disposed;
            gridControl1.MouseDoubleClick += gridControl1_MouseDoubleClick;
        }

        void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                GridHitInfo hi = gridView1.CalcHitInfo(new Point(e.X, e.Y));
                //单击的是列头
                if (hi.InColumn) { }
                if (hi.InRow)
                {
                    //取得选定行UserID信息
                    Guid idSelect = (Guid)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UserID");
                    ICP.Sys.UI.UserManage.UserInfoEditPart.UserInfoCustom.UserID = idSelect;
                    ICP.Sys.UI.UserManage.UserInfoEditPart ue =
                          Workitem.Items.AddNew<ICP.Sys.UI.UserManage.UserInfoEditPart>();
                    PartLoader.ShowDialog(ue, LocalData.IsEnglish ?
                        "User Info" : "个人资料", System.Windows.Forms.FormBorderStyle.Sizable);
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UserInfoMainWorkSpace:gridControl1_MouseDoubleClick" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }

        void UserInfoMainWorkSpace_Disposed(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 结果为空
        /// </summary>
        List<ShowUserInfo> ListEmpty = new List<ShowUserInfo>();
        /// 全部节点列表ID
        /// </summary>
        List<Guid> ListOrganization = new List<Guid>();
        /// <summary>
        /// 全部节点列表Guid对应Node
        /// </summary>
        Dictionary<Guid, TreeNodeInfo> DicOrganization = new Dictionary<Guid, TreeNodeInfo>();
        /// <summary>
        /// 部门下的员工信息
        /// </summary>
        Dictionary<string, List<ShowUserInfo>> DicOrganizationUser = new Dictionary<string, List<ShowUserInfo>>();
        /// <summary>
        /// 
        /// </summary>
        DataSet dsUserInfo;
        /// <summary>
        ///在数据集中新建数据表
        /// </summary>
        DataTable dtUserInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UserInfoMainWorkSpace_Load(object sender, EventArgs e)
        {
            try
            {
                ShowUserInfo temphowUserInfo = new ShowUserInfo();
                temphowUserInfo.Name = isenglish ? "No datas found!" : "无可用数据！";
                ListEmpty.Add(temphowUserInfo);

                this.gridView1.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
                this.gridView1.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
                this.gridView1.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
                this.gridView1.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效

                string strAddress = AppDomain.CurrentDomain.BaseDirectory;
                Bitmap bmpLogo = new Bitmap(strAddress + "Images\\Logo_InUserInfo.png");
                paLogo.ContentImage = bmpLogo;

                _Name.Caption = isenglish ? "Name" : "姓名";
                JobName.Caption = isenglish ? "Job name" : "岗位";
                MobilePhone.Caption = isenglish ? "MobilePhone" : "手机";
                TelePhone.Caption = isenglish ? "TelePhone" : "电话";
                Department.Caption = isenglish ? "Department" : "部门";
                Email.Caption = isenglish ? "Email" : "邮件";
                Fax.Caption = isenglish ? "Fax" : "传真";

                trvUserInfo.BackColor = Color.FromArgb(255, 207, 221, 238);

                //获取组织结构信息
                List<OrganizationList> organizationList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);

                //获取所有用户具体信息,获取所在部门ID
                List<UserDetailInfo> UserDetailInfoList = UserService.GetAllUsersDetailInfo_UserService(Guid.Empty);

                //程序过滤
                List<string> listNO = new List<string>();
                listNO.Add("F29D7D48-1D3B-4298-9615-1DA72E4A837E");//香港（巴西代理专用）
                listNO.Add("0501D29D-0EFE-E111-B376-0026551CA87B");//巴西公司
                listNO.Add("276D2B82-BF88-4D9C-B179-CC74D58A81F0");//香港公司
                //初始化节点资源
                foreach (OrganizationList objOrg in organizationList)
                {
                    if (!listNO.Contains(objOrg.ID.ToString().ToUpper()))
                        if (objOrg.Type != OrganizationType.Group)
                        {
                            TreeNodeInfo tn = new TreeNodeInfo();
                            tn.NodeID = objOrg.ID;
                            tn.Name = objOrg.ID.ToString();
                            ListOrganization.Add(objOrg.ID);
                            if (objOrg.ParentID != null)
                                tn.NodeParentID = objOrg.ParentID.Value;
                            tn.NodeName = isenglish ? objOrg.EShortName : objOrg.CShortName;
                            if (objOrg.Type == OrganizationType.Root)//结构根节点
                            {
                                tn.NodeIsRoot = true;
                            }
                            DicOrganization.Add(objOrg.ID, tn);
                        }
                }

                TreeNodeInfo nodeTest = new TreeNodeInfo();
                try
                {
                    //绘制树形节点
                    foreach (Guid obj in ListOrganization)
                    {
                        TreeNodeInfo tn = DicOrganization[obj];
                        nodeTest = tn;
                        tn.Text = tn.NodeName;
                        if (tn.NodeIsRoot)
                        {
                            trvUserInfo.Nodes.Add(tn);
                        }
                        else
                        {
                            DicOrganization[tn.NodeParentID].Nodes.Add(DicOrganization[tn.NodeID]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string name = nodeTest.NodeName;
                }
                trvUserInfo.Nodes[0].Expand();//展开第一个节点
                //trvUserInfo.ExpandAll();

                string Exception = string.Empty;

                //获取员工部门信息
                foreach (UserDetailInfo obj in UserDetailInfoList)
                {
                    //过滤离职员工及代交社保角色
                    if (obj.IsValid && obj.JobName != "代交社保")
                    {
                        List<ShowUserInfo> tempShowUserInfo = new List<ShowUserInfo>();
                        temphowUserInfo = new ShowUserInfo();
                        temphowUserInfo.Name = isenglish ? obj.EName : obj.CName;
                        temphowUserInfo.JobName = obj.JobName;
                        temphowUserInfo.MobilePhone = obj.Mobile;
                        temphowUserInfo.TelePhone = obj.Tel;
                        temphowUserInfo.DepartmentName = obj.DepartmentName;
                        temphowUserInfo.Email = obj.EMail;
                        temphowUserInfo.Fax = obj.Fax;
                        temphowUserInfo.DepartmentID = obj.DepartmentID;
                        temphowUserInfo.UserID = obj.ID;

                        if (!DicOrganizationUser.ContainsKey(obj.DepartmentID.ToString()))
                        {
                            tempShowUserInfo.Add(temphowUserInfo);
                            DicOrganizationUser.Add(obj.DepartmentID.ToString(), tempShowUserInfo);
                        }
                        else
                        {
                            DicOrganizationUser[obj.DepartmentID.ToString()].Add(temphowUserInfo);
                        }
                    }
                    trvUserInfo.AfterSelect += trvUserInfo_AfterSelect;
                }
            }
            catch (Exception ex)
            {
                string exceptionstr = "UserInfoMainWorkSpace:UserInfoMainWorkSpace_Load" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        void trvUserInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvUserInfo.SelectedNode != null)
            {
                string GuidString = trvUserInfo.SelectedNode.Name;
                if (DicOrganizationUser.ContainsKey(GuidString))
                {
                    //绑定数据
                    gridView1.OptionsView.ShowVertLines = true;
                    gridControl1.DataSource = DicOrganizationUser[GuidString];
                }
                else
                {
                    gridView1.OptionsView.ShowVertLines = false;
                    gridControl1.DataSource = ListEmpty;
                    //没数据
                }
            }
        }

        public class TreeNodeInfo : TreeNode
        {
            /// <summary>
            /// 是否是父节点
            /// </summary>
            public bool NodeIsRoot { get; set; }
            /// <summary>
            /// 节点ID
            /// </summary>
            public Guid NodeID { get; set; }
            /// <summary>
            /// 节点父ID
            /// </summary>
            public Guid NodeParentID { get; set; }
            /// <summary>
            /// 节点名称
            /// </summary>
            public string NodeName;
            /// <summary>
            /// 部门类型
            /// </summary>
            public OrganizationType OrgType { get; set; }
        }
        public delegate void NodeSelectdelegate(object sender, EventArgs e);

        /// <summary>
        /// 显示信息
        /// </summary>
        public class ShowUserInfo
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 岗位名称
            /// </summary>
            public string JobName { get; set; }
            /// <summary>
            /// 手机
            /// </summary>
            public string MobilePhone { get; set; }
            /// <summary>
            /// 电话
            /// </summary>
            public string TelePhone { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            public string DepartmentName { get; set; }
            /// <summary>
            /// 邮件
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 传真
            /// </summary>
            public string Fax { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public Guid DepartmentID { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public Guid UserID { get; set; }
        }

        private void paHeadSpace_SizeChanged(object sender, EventArgs e)
        {
            Size newsize = paHeadSpace.Size;
            int X = newsize.Width / 2 + 100;
            int Y = 10;
            labelControl1.Location = new Point(X, Y);
        }
    }
}
