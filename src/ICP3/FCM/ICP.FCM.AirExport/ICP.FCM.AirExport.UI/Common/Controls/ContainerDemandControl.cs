using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirExport.UI
{
    [ToolboxItem(false)]
    public partial class ContainerDemandControl : XtraUserControl
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 用Dev的DxErrorProvider设置错误要使用这个控件
        /// </summary>
        public Control ErrorHost
        {
            get { return txtContainerDemand; }
        }

        /// <summary>
        /// 控制不能改变高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 21;
        }

        /// <summary>
        /// 如果直接用BackColor这个属性，在设计时状态指定背景色会无效。故新增此属性
        /// </summary>
        public Color SpecifiedBackColor
        {
            get
            {
                return txtContainerDemand.BackColor;
            }
            set
            {
                txtContainerDemand.BackColor = value;
            }
        }

        /// <summary>
        /// 文本框的输入有改变时触发
        /// </summary>
        public new event EventHandler TextChanged;
        
        #region 初始化


        public ContainerDemandControl()
        {
            InitializeComponent();
            Load += new EventHandler(ContainerDemandControl_Load);
            Disposed += delegate {
                TextChanged = null;
                
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                    
                }
            
            };
        }

        void ContainerDemandControl_Load(object sender, EventArgs e)
        {
            ppopMore.Location = new Point(Width - 195, 0);
        }

        #endregion

        [Bindable(true)]
        public override string Text
        {
            get { return txtContainerDemand.Text; }
            set { txtContainerDemand.Text = value; }
        }

        private void btnCtnType_Click(object sender, EventArgs e)
        {
            try
            {
                string type = (sender as Control).Tag.ToString();
                txtContainerDemand.Text = AddContainer(txtContainerDemand.Text, type);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Close)
            {
                txtContainerDemand.Text = string.Empty;
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                ppopMore.ShowPopup();
            }
            else
            {
                try
                {
                    string type = e.Button.Tag.ToString();
                    txtContainerDemand.Text = AddContainer(txtContainerDemand.Text, type);
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }

        }

        #region 正则表达式的一些匹配


        /// <summary>
        /// 生成箱需求字串

        /// </summary>
        /// <param name="containerInfo">原字符串</param>
        /// <param name="containerSize">传入如20 GP,40 HQ</param>
        public string AddContainer(string containerInfo, string containerSize)
        {
            //sapce
            string sapces = @"[\x20]*";
            //Count
            string pQty = @"(?<Qty>[\d]+)";
            //Size
            string psize = @"(?<SizeB>(?<Size>20|40|45)[\']*)";
            //Type 
            string ptype = @"(?<Type>(GP|HQ|FR|HT|OT|RF|RF|TK|TF|RH|NOR))";
            //spe
            string pspae = @"[\x20\,]*";
            //
            string patten = @"[\x20]*(?<box>" + pQty + sapces + @"[xX\xD7\*]" + sapces + psize + sapces + ptype + @")" + pspae;
            //
            MatchCollection matchs = Regex.Matches(containerInfo.ToUpper(), patten, RegexOptions.IgnoreCase);
            //

            bool added = false;
            //
            Dictionary<string, int> existCNTR = new Dictionary<string, int>();
            //for each exist container
            foreach (Match match in matchs)
            {
                Int32 qty = int.Parse(match.Groups["Qty"].Value);
                string size = match.Groups["SizeB"].Value;
                string type = match.Groups["Type"].Value;

                if (!added)
                {
                    if (containerSize.Contains(size) && containerSize.Contains(type))
                    {
                        qty++;
                        added = true;
                    }
                }

                string key = string.Format("{0} {1}", size, type);
                //
                if (existCNTR.ContainsKey(key))
                {
                    existCNTR[key] = int.Parse(existCNTR[key].ToString()) + 1;
                }
                else
                {
                    existCNTR.Add(key, qty);
                }
            }
            // if have no been added
            if (!added)
            {
                existCNTR.Add(string.Format("{0}", containerSize), 1);
            }
            // used for returning
            string result = string.Empty;
            //build string
            foreach (KeyValuePair<string, int> e in existCNTR)
            {
                if (result != string.Empty) result += ",";

                result += string.Format("{0} * {1}", e.Value, e.Key);
            }
            return result.ToUpper();
        }
        /// <summary>
        /// 验证箱需求字串,返回正确的字串

        /// </summary>
        public string ValidateContainer(string containerInfo)
        {
            //sapce
            string sapces = @"[\x20]*";
            //Count
            string pQty = @"(?<Qty>[\d]+)";
            //Size
            string psize = @"(?<SizeB>(?<Size>20|40|45)[\']*)";
            //Type 
            string ptype = @"(?<Type>(GP|HQ|FR|HT|OT|RF|RF|TK|TF|RH|NOR))";
            //spe
            string pspae = @"[\x20\,]*";
            //
            string patten = @"[\x20]*(?<box>" + pQty + sapces + @"[xX\xD7\*]" + sapces + psize + sapces + ptype + @")" + pspae;
            //
            MatchCollection matchs = Regex.Matches(containerInfo.ToUpper(), patten, RegexOptions.IgnoreCase);

            Dictionary<string, int> existCNTR = new Dictionary<string, int>();
            //for each exist container
            foreach (Match match in matchs)
            {
                Int32 qty = int.Parse(match.Groups["Qty"].Value);
                string size = match.Groups["SizeB"].Value;
                string type = match.Groups["Type"].Value;

                string key = string.Format("{0} {1}", size, type);
                //
                if (existCNTR.ContainsKey(key))
                {
                    existCNTR[key] = int.Parse(existCNTR[key].ToString()) + 1;
                }
                else
                {
                    existCNTR.Add(key, qty);
                }
            }

            // used for returning
            string result = string.Empty;
            //build string
            foreach (KeyValuePair<string, int> e in existCNTR)
            {
                if (result != string.Empty) result += ",";

                result += string.Format("{0} * {1}", e.Value, e.Key);
            }
            return result.ToUpper();
        }

        #endregion

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ppopMore.ClosePopup();
        }

        private void txtContainerDemand_Leave(object sender, EventArgs e)
        {
            ppopMore.ClosePopup();
            txtContainerDemand.Text = ValidateContainer(txtContainerDemand.Text);
        }

        private void txtContainerDemand_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged(this, null);
            }
        }
    }
}
