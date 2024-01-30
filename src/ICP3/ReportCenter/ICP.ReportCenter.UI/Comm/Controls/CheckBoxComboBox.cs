using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class CheckBoxComboBox : DevExpress.XtraEditors.XtraUserControl
    {
        #region init
        public CheckBoxComboBox()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.EditValueChanged = null;
            
            };
        }
        /// <summary>
        /// 当前的数据发生改变时
        /// </summary>
        public virtual event EventHandler EditValueChanged;
        /// <summary>
        /// 第一次进入时触发事件
        /// </summary>
        public event EventHandler FirstTimeEnter;
        private bool isFirstTimeEntered = true;
        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = 21;
        }

        #endregion
        protected override void OnEnter(EventArgs e)
        {
            if (isFirstTimeEntered && this.FirstTimeEnter != null)
            {
                this.FirstTimeEnter(this, e);
                isFirstTimeEntered = false;
            }
            base.OnEnter(e);
        }
        /// <summary>
        /// 查询输入的值
        /// </summary>
        public void SerachSelectText()
        {
            string strText = this.popEdit.Text;

            if (!string.IsNullOrEmpty(strText))
            {
                string[] textList = strText.Split(';');
                string currentStr = textList[textList.Length - 1];
                if (currentStr != null)
                {
                    currentStr = currentStr.Trim();
                }
                if (!string.IsNullOrEmpty(currentStr))
                {

                    foreach (CheckedListBoxItem item in this.checBoxkList.Items)
                    {
                        string tmpStr = item.Description;

                        if (tmpStr.Length < currentStr.Length) continue;

                        if (tmpStr.ToUpper().Substring(0, currentStr.Length) == currentStr.ToUpper())
                        {
                            this.popEdit.Text = this.popEdit.Text + tmpStr.Substring(currentStr.Length, tmpStr.Length - currentStr.Length);

                            this.popEdit.Refresh();

                            this.popEdit.Select(strText.Length, tmpStr.Length - currentStr.Length);

                            break;
                        }
                    }
                }
            }
        }

        #region 属性

        private string _SplitText =ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol;
        public string SplitText
        {
            get { return _SplitText; }
            set { _SplitText = value; }
        }


        [Localizable(true)]
        [Category("Tooltip")]
        [Description("Gets or sets a regular tooltip's title.")]
        [DefaultValue("")]
        public virtual string ToolTip
        {
            get
            {
                return this.popEdit.ToolTip;
            }
            set
            {
                this.popEdit.ToolTip = value;
            }

        }
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the string displayed in the edit box when the editor's BaseEdit.EditValue is null.")]
        public virtual string NullText
        {
            get
            {
                return this.popEdit.Properties.NullText;
            }
            set
            {
                this.popEdit.Properties.NullText = value;
            }

        }

        /// <summary>
        /// 选择的Values集合
        /// </summary>
        [Bindable(false)]
        public List<object> EditValue
        {
            get
            {
                List<object> values = new List<object>();
                foreach (CheckedListBoxItem item in this.checBoxkList.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        values.Add(item.Value);
                    }
                }
                return values;
            }
        }
        [Bindable(false)]
        public string EditText
        {
            get
            {
                return popEdit.Text;
            }
        }

        /// <summary>
        /// 已勾选的条目数
        /// </summary>
        public int CheckCount
        {
            get
            {
                int checkCount=0;
                foreach (CheckedListBoxItem item in checBoxkList.Items)
                {
                    if (item.CheckState == CheckState.Checked) checkCount++;
                }
                return checkCount;
            }
        }

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Description("Provides access to the item collection, when the control is not bound to a data source.")]
        public CheckedListBoxItemCollection Items { get { return checBoxkList.Items; } }
        
        #endregion

        #region 方法
        /// <summary>
        /// 开始编辑
        /// </summary>
        public void BeginUpdate()
        {
            this.checBoxkList.BeginUpdate();
        }
        /// <summary>
        /// 结束编辑
        /// </summary>
        public void EndUpdate()
        {
            this.checBoxkList.EndUpdate();
        }

        /// <summary>
        /// 刷新选择的值
        /// </summary>
        public virtual void RefreshText()
        {
            this.popEdit.Text = GetTextByCheckedItem();
        }
        /// <summary>
        /// 获得选择的值
        /// </summary>
        /// <returns></returns>
        private string GetTextByCheckedItem()
        {
            StringBuilder builder = new StringBuilder();
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    if (builder.Length > 0) builder.Append(_SplitText);

                    builder.Append(item.Description);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual void ClearItems()
        {
            this.checBoxkList.BeginUpdate();
            this.checBoxkList.Items.Clear();
            this.checBoxkList.EndUpdate();

            this.popEdit.Text = string.Empty;
            if (EditValueChanged != null) EditValueChanged(null, null);

        }
        /// <summary>
        /// 清空值
        /// </summary>
        public virtual void ClearEditValue()
        {
            this.popEdit.Text = string.Empty;
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                item.CheckState = CheckState.Unchecked;
            }
            if (EditValueChanged != null) EditValueChanged(null, null);
        }

        #endregion

        #region 事件
        /// <summary>
        /// 添加明细项
        /// </summary>
        public virtual void AddItem(object value, string text)
        {
            AddItem(value, text, false);
        }
        /// <summary>
        /// 添加明细项
        /// </summary>
        public virtual void AddItem(object value, string text,bool isChecked)
        {
            if (!string.IsNullOrEmpty(text))
            {
                CheckedListBoxItem item = new CheckedListBoxItem();
                item.Value = value;
                item.Description = text;
                item.CheckState = isChecked ? CheckState.Checked : CheckState.Unchecked;
                this.checBoxkList.Items.Add(item);
            }

        }

        #endregion

        /// <summary>
        /// 显示查询面板时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ICPComBox_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popupContainerControl1.Width = this.popEdit.Width;
        }

        #region Button事件
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.popEdit.ClosePopup();
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            AllCheck();
            RefreshText();

            if (EditValueChanged != null) EditValueChanged(sender, e);

        }
       
        /// <summary>
        /// 全选
        /// </summary>
        protected virtual void AllCheck()
        {
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                item.CheckState = CheckState.Checked;
            }
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnCheck_Click(object sender, EventArgs e)
        {
            UnCheck();
            RefreshText();
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }

        }
        /// <summary>
        /// 反选
        /// </summary>
        protected virtual void UnCheck()
        {
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                if (item.CheckState != CheckState.Checked)
                {
                    item.CheckState = CheckState.Checked;
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
         
        }

        #endregion

        /// <summary>
        /// Item的CheckState发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checBoxkList_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (this.checBoxkList.SelectedIndex < 0) return;

            if (e.State == CheckState.Checked)
            {
                if (popEdit.Text.IsNullOrEmpty() == false)
                    popEdit.Text += (_SplitText + this.checBoxkList.Items[this.checBoxkList.SelectedIndex].Description);
                else
                    popEdit.Text = this.checBoxkList.Items[this.checBoxkList.SelectedIndex].Description;
            }
            else
            {

                RefreshText();
                //List<string> textlist = popEdit.Text.Split(new string[] { _SplitText }, StringSplitOptions.None).ToList();
                //if (textlist == null) return;

                //textlist.Remove(this.checBoxkList.Items[this.checBoxkList.SelectedIndex].Description);
                //StringBuilder builder = new StringBuilder();
                //foreach (var item in textlist)
                //{
                //    if (builder.Length > 0) builder.Append(_SplitText);

                //    builder.Append(item);
                //}
                //popEdit.Text = builder.ToString();

            }

            if (this.EditValueChanged != null) this.EditValueChanged(this, EventArgs.Empty);
        }
    }
}
