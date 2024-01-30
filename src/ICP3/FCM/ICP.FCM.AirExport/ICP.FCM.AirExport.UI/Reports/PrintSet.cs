using System;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
namespace ICP.FCM.AirExport.UI.Reports
{
    [ToolboxItem(false)]

    public partial class PrintSet : BaseEditPart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public override event SavedHandler Saved;
        public PrintSet()
        {
            InitializeComponent();
            imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(imageComboBoxEdit1_SelectedIndexChanged);
            Disposed += delegate { if (Workitem != null) { Workitem.Items.Remove(this); } };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                if (LocalData.IsEnglish == false) { SetText(); }
            }
        }
        private void SetText()
        {
            groupControl1.Text = "报表样式";
            groupControl2.Text = "标题";
            //radioGroup1..Text = "正本";
            //radioGroup1.Controls[1].Text = "副本";
            simpleButton1.Text = "确定";
            simpleButton2.Text = "取消";
        }
        void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = imageComboBoxEdit1.SelectedIndex;
            if (index == 0) { panelControl1.BackgroundImage = imageCollection1.Images[0]; }
            if (index == 1) { panelControl1.BackgroundImage = imageCollection1.Images[1]; }
            if (index == 2) { panelControl1.BackgroundImage = imageCollection1.Images[2]; }
            else { { panelControl1.BackgroundImage = imageCollection1.Images[3]; } }
        }
        //获取打印参数
        public string[] GetConfigs()
        {
            string[] configs = new string[3];
            if (radioGroup1.SelectedIndex == 0)
            {
                configs[0] = "正本";
            }
            else
            {
                configs[0] = "副本";
            }//报表样式
            if (radReport.SelectedIndex == 0)
            {
                configs[1] = "CITY OCEAN LOGISTICS CO.,LTD.";
            }
            if (radReport.SelectedIndex == 1)
            {
                configs[1] = "TOP SHIPPING LOGISTICS CO.,LTD.";
            }
            else
            {
                configs[1] = "CITY OCEAN FAR EAST OFFICE";
            }//标题
            configs[2] = imageComboBoxEdit1.SelectedIndex.ToString();//LOGO
            return configs;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string[] configs = GetConfigs();
            if (Saved != null) Saved(new object[] { configs });
            FindForm().Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string[] configs = GetConfigs();
            if (Saved != null) Saved(new object[] { configs });
            FindForm().Close();
        }
    }
}
