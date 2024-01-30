#region Comment

/*
 * 
 * FileName:    FormStart.cs
 * CreatedOn:   2014/5/14 星期三 9:36:16
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->启动窗体，选择登陆用户
 *      ->遍历启动目录找出其中为登录用户的文件夹
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System.Windows.Forms;
using DevExpress.XtraEditors;
using System;
using System.IO;

namespace ICP.Document
{
    public partial class FormStart : FormBase
    {
        #region 成员变量
        /// <summary>
        /// 遍历路径
        /// </summary>
        private string _Path = Application.StartupPath;
        #endregion

        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public FormStart()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormStart_Load);
            this.btnStart.Click += new EventHandler(btnStart_Click);
            this.Disposed += (sender, e) =>
            {
                this.Load -= new EventHandler(FormStart_Load);
                this.btnStart.Click -= new EventHandler(btnStart_Click);
            };
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        void FormStart_Load(object sender, EventArgs e)
        {
            //1.初始化下拉框
            InitComboxItems();
            //2.默认选择第一个账户
            cmbLoginUser.SelectedIndex = 0;
            //3.验证是否单用户，是则自动登录
            if (cmbLoginUser.Properties.Items != null && cmbLoginUser.Properties.Items.Count == 1)
                btnStart_Click(null, null);
        }
        /// <summary>
        /// 打开主界面
        /// </summary>
        void btnStart_Click(object sender, EventArgs e)
        {
            //1.验证包含项
            if (cmbLoginUser.Properties.Items == null)
                return;
            //2.验证选择
            if (cmbLoginUser.EditValue == null)
            {
                MessageBox.Show("Please Select Account.", "System Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //3.设置当前选择数据库
            ClientConstants.CurrentDBPath = string.Format(_Path + @"\{0}\{1}", cmbLoginUser.EditValue.ToString(), ClientConstants.DBFullName);
            //4.设置选择状态
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 定义方法
        /// <summary>
        /// 初始化下拉框选项
        /// </summary>
        private void InitComboxItems()
        {
            //1.清空下拉框中的原始数据
            cmbLoginUser.Properties.Items.Clear();
            //2.初始化根目录对象
            DirectoryInfo rootFolder = new DirectoryInfo(_Path);
            //3.获取根目录下的子目录
            DirectoryInfo[] subFolder = rootFolder.GetDirectories();
            //4.遍历子目录中
            for (int index = 0; index < subFolder.Length; index++)
            {
                //遍历子目录中的文件
                foreach (FileInfo NextFile in subFolder[index].GetFiles())
                {
                    //判断文件是否为缓存数据库文件：验证文件扩展名
                    if (NextFile.Extension.ToLower().Equals(".sdf"))
                    {
                        //是则在下拉框中添加用户账号，跳出循环
                        cmbLoginUser.Properties.Items.Add(subFolder[index].Name);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
