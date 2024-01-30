using System;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.GoTo
{
    public partial class CommonSetting : DevExpress.XtraEditors.XtraForm
    {
        #region  服务和变量
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ICP.Common.ServiceInterface.ICommonGoTo CommonGo
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICommonGoTo>();
            }
        }
        public GoToControlReadXml ReadXml;
        /// <summary>
        /// 读取xml文件配置信息类
        /// </summary>
        public GoToControlReadXml GoToControlReadXml
        {
            get { return ReadXml ?? (ReadXml = new GoToControlReadXml()); }
            set { ReadXml = value; }
        }
        private string _type;

        GotoSetting _gotoSetting = new GotoSetting();
        #endregion
        public CommonSetting()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonSetting_Load(object sender, EventArgs e)
        {
            var settingsList = GoToControlReadXml.GetSettingScope();
            if (settingsList.Length > 1)
            {
                _type = "Update";
                foreach (var gc in groupBoxCheckBox.Controls)
                {
                    if (!(gc is CheckBox)) continue;
                    if (settingsList.Split(',').Contains(((CheckBox)gc).Text))
                    {
                        ((CheckBox)gc).Checked = true;
                    }
                }

            }
            else
            {
                _type = "Insert";
            }
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleConfirm_Click(object sender, EventArgs e)
        {
            foreach (var gc in groupBoxCheckBox.Controls)
            {
                if (!(gc is CheckBox)) continue;
                if (!((CheckBox)gc).Checked) continue;
                if (!string.IsNullOrEmpty(_gotoSetting.SettingScope))
                {
                    _gotoSetting.SettingScope =
                        _gotoSetting.SettingScope + "," + ((CheckBox)gc).Text;
                }
                else
                {
                    _gotoSetting.SettingScope = ((CheckBox)gc).Text;
                }
            }
            if (string.IsNullOrEmpty(_gotoSetting.SettingScope)) return;
            if (_type == "Insert")
            {
                GoToControlReadXml.AddQueryConditions(_gotoSetting.SettingScope);
                CloseFrom();
            }
            else if (_type == "Update")
            {
                GoToControlReadXml.UpdateQueryConditions(_gotoSetting.SettingScope);
                CloseFrom();
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleCancel_Click(object sender, EventArgs e)
        {
            CloseFrom();
        }
        /// <summary>
        /// 关闭窗体的方法
        /// </summary>
        public void CloseFrom()
        {
            this.Hide();
            var common = new CommonGotoFrom();
            //common.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            common.ShowDialog();
        }
    }
}
