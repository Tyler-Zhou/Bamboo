using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;
using ICP.Framework.CommonLibrary;

namespace ICP.FRM.UI
{
    public partial class CheckBoxComboBox : XtraUserControl
    {
        public CheckBoxComboBox()
        {
            InitializeComponent();
        
            ICPComBox.KeyDown += new KeyEventHandler(ICPComBox_KeyDown);

            ICPComBox.TextChanged += new EventHandler(ICPComBox_TextChanged);
            Disposed += delegate {
                if (ICPComBox != null)
                {
                    ICPComBox.KeyDown -= ICPComBox_KeyDown;
                    ICPComBox.TextChanged -= ICPComBox_TextChanged;
                    ICPComBox.QueryPopUp -= ICPComBox_QueryPopUp;
                    ICPComBox = null;
                }
                EditValueChanged = null;
                KeyDownEnter = null;
                ItemCheckChanged = null;
            
            
            };
        }

        bool isTextChanged = false;

        void ICPComBox_TextChanged(object sender, EventArgs e)
        {
            if (isTextChanged)
            {
                SerachSelectText();
                isTextChanged = false;
            }
        }
        /// <summary>
        /// 选择/取消选择某一项
        /// </summary>
        public virtual event ItemCheckEventHandler ItemCheckChanged;
        /// <summary>
        /// 当前的数据发生改变时
        /// </summary>
        public virtual event EventHandler EditValueChanged;
        public event KeyEventHandler KeyDownEnter;
        void ICPComBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back &&
                e.KeyCode != Keys.Left &&
                e.KeyCode != Keys.Right &&
                e.KeyCode != Keys.Delete &&
                e.KeyCode != Keys.Enter &&
                e.KeyCode != Keys.Tab &&
                e.KeyCode != Keys.Shift &&
                e.KeyCode != Keys.Control &&
                e.KeyCode != Keys.Space)
            {
                isTextChanged = true;
            }
            else
            {
                isTextChanged = false;
            }

            if (e.KeyCode == Keys.Enter && KeyDownEnter != null)
            {
                KeyDownEnter(sender, e);
            }
        }

        /// <summary>
        /// 查询输入的值
        /// </summary>
        public void SerachSelectText()
        {
            string strText = ICPComBox.Text;

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

                    foreach (CheckedListBoxItem item in checBoxkList.Items)
                    {
                        string tmpStr = item.Description;

                        if (tmpStr.Length < currentStr.Length) continue;

                        if (tmpStr.ToUpper().Substring(0, currentStr.Length) == currentStr.ToUpper())
                        {
                            ICPComBox.Text = ICPComBox.Text + tmpStr.Substring(currentStr.Length, tmpStr.Length - currentStr.Length);

                            ICPComBox.Refresh();

                            ICPComBox.Select(strText.Length, tmpStr.Length - currentStr.Length);

                            break;
                        }
                    }
                }
            }
        }

        #region 属性

        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 21;
        }

        [Localizable(true)]
        [Category("Tooltip")]
        [Description("Gets or sets a regular tooltip's title.")]
        [DefaultValue("")]
        public virtual string ToolTip 
        {
            get
            {
                return ICPComBox.ToolTip;
            }
            set
            {
                ICPComBox.ToolTip = value;
            }
        
        }
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the string displayed in the edit box when the editor's BaseEdit.EditValue is null.")]
        public virtual string NullText
        {
            get
            {
                return ICPComBox.Properties.NullText;
            }
            set
            {
                ICPComBox.Properties.NullText = value;
            }

        }
        /// <summary>
        /// 在列表中找不到对应Valud的名称
        /// </summary>
        [Bindable(false)]
        public string[] NotFindValueToList
        {
            get
            {
                List<string> list = new List<string>();
                List<string> textlist = GetTexts();

                foreach (string str in textlist)
                {
                    int count = (from d in ItemTextList where d.Contains(str) select d).Count();
                    if(count==0)
                    {
                        list.Add(str);   
                    }
                }

                return list.ToArray();
            }
        }

        /// <summary>
        /// 选择的Values集合
        /// </summary>
        [Bindable(false)]
        public virtual string SelectValues
        {
            get
            {
                ICPComBox.Refresh();
                string strlist = string.Empty;
                foreach (CheckedListBoxItem item in checBoxkList.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (string.IsNullOrEmpty(strlist))
                        {
                            strlist = item.Value.ToString();
                        }
                        else
                        {
                            strlist = strlist + ";" + item.Value.ToString();
                        }
                    }
                }
                return strlist;
            }
        }
        [Bindable(false)]
        public Guid[] SelectValuesToGuid
        {
            get
            {
                isAllCheck = true;
                List<Guid> list = new List<Guid>();
                List<string> textlist = GetTexts();

                foreach (CheckedListBoxItem item in checBoxkList.Items)
                {
                    if (textlist.Contains(item.Description.ToUpper()))
                    {
                       
                        item.CheckState = CheckState.Checked;

                        Guid id = new Guid(item.Value.ToString());
                        if (!list.Contains(id))
                        {
                            list.Add(id);
                        }
                    }
                    else
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                } 
                isAllCheck = false;
                return list.ToArray();
            }
        }
        [Bindable(false)]
        public string[] SelectValuesToString
        {
            get
            {
                isAllCheck = true;
                List<string> list = new List<string>();
                List<string> textlist = GetTexts();

                foreach (CheckedListBoxItem item in checBoxkList.Items)
                {
                    if (textlist.Contains(item.Description.ToUpper()))
                    {
                        item.CheckState = CheckState.Checked;

                        if (!list.Contains(item.Value.ToString()))
                        {
                            list.Add(item.Value.ToString());
                        }
                    }
                    else
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                  
                }
                isAllCheck = false;
                return list.ToArray();
            }
        }

        [Bindable(false)]
        public virtual string[] SelectTextToString
        {
            get
            {
                isAllCheck = true;
                ICPComBox.Refresh();
                List<string> list = new List<string>();
                List<string> textlist = GetTexts();

                foreach (CheckedListBoxItem item in checBoxkList.Items)
                {
                    if (textlist.Contains(item.Description.ToUpper()))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                    Guid id = new Guid(item.Value.ToString());
                    if (item.CheckState == CheckState.Checked && !list.Contains(item.Description))
                    {
                        list.Add(item.Description);
                    }
                }
                isAllCheck = false;
                return list.ToArray();
            }
        }
        /// <summary>
        /// 选择的Text集合
        /// </summary>
        [Bindable(false)]
        public virtual string SelectTexts
        {
            get
            {
                isAllCheck = true;
     
                string strlist = string.Empty;
                foreach (CheckedListBoxItem item in checBoxkList.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (string.IsNullOrEmpty(strlist))
                        {
                            strlist = item.Description;
                        }
                        else
                        {
                            strlist = strlist + ";" + item.Description;
                        }
                    }
                }
                isAllCheck = false;
                return strlist;
            }
        }
        
        /// <summary>
        /// ITEM集合
        /// </summary>
        [Bindable(false)]
        public CheckedListBoxItemCollection Items
        { 
            get
            {
                return checBoxkList.Items;
            }
        }

        public List<string> ItemTextList
        {
            get
            {
                List<string> list = new List<string>();
                foreach (CheckedListBoxItem item in Items)
                {
                    if (DataTypeHelper.GetString(item.Description,string.Empty) == string.Empty)
                    {
                        continue;
                    }
                    if (!list.Contains(item.Description.ToUpper()))
                    {
                        list.Add(item.Description.ToUpper());
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [Bindable(false)]
        public object DataSource
        {
            get
            {
                return checBoxkList.DataSource;
            }
            set
            {
                checBoxkList.DataSource = value;
            }
        }
        /// <summary>
        /// 绑定值
        /// </summary>
        [Bindable(false)]
        public string ValueMember
        {
            get
            {
                return  checBoxkList.ValueMember;
            }
            set
            {
                checBoxkList.ValueMember = value;
            }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        [Bindable(false)]
        public string DisplayMember
        {
            get
            {
                return checBoxkList.DisplayMember;
            }
            set
            {
                checBoxkList.DisplayMember = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 开始编辑
        /// </summary>
        public void BeginUpdate()
        {
            checBoxkList.BeginUpdate();
        }
        /// <summary>
        /// 结束编辑
        /// </summary>
        public void EndUpdate()
        {
            checBoxkList.EndUpdate();
        }

        /// <summary>
        /// 设置选择的值
        /// </summary>
        protected  virtual void SetSelectText()
        {
            List<string> textlist = GetTexts();

            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                if (textlist.Contains(item.Description.ToUpper()))
                {
                    item.CheckState = CheckState.Checked;
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }


        public List<string> GetTexts()
        {
            string texts = ICPComBox.Text.ToUpper();
            string[] textlist = new string[0];

            if (!string.IsNullOrEmpty(texts))
            {
                textlist = texts.Split(';');
            }
            if (textlist == null || textlist.Length==0)
            {
                textlist = new string[1] { texts };
            }
            List<string> lists = textlist.ToList();
            lists.ForEach(o => o.Trim());

            return lists;
        }
        /// <summary>
        /// 获得选择的值
        /// </summary>
        protected virtual void GetSelectText()
        {
            List<string> textlist = GetTexts();

            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                if (item.CheckState == CheckState.Checked && !textlist.Contains(item.Description))
                {
                    if (ICPComBox.Text.Trim().EndsWith(";") || string.IsNullOrEmpty(ICPComBox.Text.Trim()))
                    {
                        ICPComBox.Text = ICPComBox.Text + item.Description;
                    }
                    else
                    {
                        ICPComBox.Text = ICPComBox.Text + ";" + item.Description;
                    }   
                }               
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual  void ClearItems()
        {
            checBoxkList.BeginUpdate();
            checBoxkList.Items.Clear();
            checBoxkList.EndUpdate();

            ICPComBox.Text = string.Empty;
            if (EditValueChanged != null)
            {
                EditValueChanged(null, null);
            }

        }
        /// <summary>
        /// 清空值
        /// </summary>
        public virtual void ClearEditValue()
        { 
            ICPComBox.Text = string.Empty;
            isAllCheck = true;
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                item.CheckState = CheckState.Unchecked;
            }
            isAllCheck = false;


            if (EditValueChanged != null)
            {
                EditValueChanged(null, null);
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="text"></param>
        public void SetEditText(object text)
        {
            ICPComBox.Text = string.Empty;
            isAllCheck = true;
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                item.CheckState = CheckState.Unchecked;
                if (!string.IsNullOrEmpty(ICPComBox.Text))
                {
                    continue;
                }
                else
                {
                    if (item.Description == text.ToString())
                    {
                        item.CheckState = CheckState.Checked;
                        ICPComBox.Text = item.Description;
                    }
                }
            }
            isAllCheck = false;

            if (EditValueChanged != null)
            {
                EditValueChanged(null, null);
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 添加明细项
        /// </summary>
        public virtual void AddItem(object value,string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                CheckedListBoxItem item = new CheckedListBoxItem();
                item.Value = value;
                item.Description = text;

                checBoxkList.Items.Add(item);
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
            popupContainerControl1.Width = ICPComBox.Width;
            SetSelectText();
        }
        bool isAllCheck=false;
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            ICPComBox.ClosePopup();
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            isAllCheck = true;
            AllCheck();
            GetSelectText();
            isAllCheck = false;

            if (EditValueChanged != null)
            {
                EditValueChanged(sender,e);
            }

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
            isAllCheck = true;
            UnCheck();
            isAllCheck = false;
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
            string text = string.Empty;
            foreach (CheckedListBoxItem item in checBoxkList.Items)
            {
                if (item.CheckState != CheckState.Checked)
                {
                    item.CheckState = CheckState.Checked;

                    if (text.Trim().EndsWith(";") || string.IsNullOrEmpty(text.Trim()))
                    {
                        text = text + item.Description;
                    }
                    else
                    {
                        text = text + ";" + item.Description;
                    }   
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
            ICPComBox.Text = text;
        }
        /// <summary>
        /// Item的CheckState发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checBoxkList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isAllCheck)
            {
                return;
            }
            if (checBoxkList.SelectedIndex < 0)
            {
                return;
            }
            List<string> textlist = GetTexts();
            string selectText = checBoxkList.Items[checBoxkList.SelectedIndex].Description;

            string selectTextToUpper = selectText.ToUpper();

            if (e.State == CheckState.Checked)
            {
                //选中某个时
                if (!textlist.Contains(selectTextToUpper))
                {
                    textlist.Add(selectTextToUpper);
                    if (ICPComBox.Text.Trim().EndsWith(";") || string.IsNullOrEmpty(ICPComBox.Text))
                    {
                        ICPComBox.Text = selectText;
                    }
                    else
                    {
                        ICPComBox.Text = ICPComBox.Text + ";" + selectText;
                    }
                    if (EditValueChanged != null)
                    {
                        EditValueChanged(sender, e);
                    }
                }
            }
            else
            {
                //取消选中时
                if (textlist.Contains(selectTextToUpper))
                {
                    textlist.Remove(selectTextToUpper);
                }
                string strlist = string.Empty;
                foreach (string str in textlist)
                {
                    if (string.IsNullOrEmpty(strlist))
                    {
                        strlist = str;
                    }
                    else
                    {
                        strlist = strlist + ";" + str;
                    }
                }
                ICPComBox.Text = strlist;

                if (EditValueChanged != null)
                {
                    EditValueChanged(sender, e);
                }
            }

            if (ItemCheckChanged != null)
            {
                ItemCheckChanged(sender, e);
            }

        }
    }
}
