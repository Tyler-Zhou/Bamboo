using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using System.Reflection;
using ICP.Framework.CommonLibrary.Helper;


namespace ICP.FCM.AirExport.UI.Common.Controls
{
    public partial class TreeCheckBox : XtraUserControl
    {
        #region init

        public TreeCheckBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控制不能改变高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 21;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            treeMain.LayoutUpdated += new EventHandler(treeMain_LayoutUpdated);
        }


        /// <summary>
        /// 背景色
        /// </summary>
        public override Color BackColor { get { return popEdit1.BackColor; } set { popEdit1.BackColor = value; } }

        /// <summary>
        /// 前景色
        /// </summary>
        public new Color ForeColor { get { return popEdit1.ForeColor; } set { popEdit1.ForeColor = value; } }

        /// <summary>
        /// 有效性
        /// </summary>
        public new bool Enabled { get { return popEdit1.Enabled; } set { popEdit1.Enabled = value; } }

        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get { return popEdit1.Properties.ReadOnly; } set { popEdit1.Properties.ReadOnly = value; } }

        #endregion

        #region Property


        List<Guid> _editValue = new List<Guid>();
        public List<Guid> EditValue 
        {
            get { return _editValue; }
            set
            {
                _editValue = value ?? new List<Guid>();
                BulidCheckedByEditValue();
                BulidTextByEditValue();

            } 
        }

        private void BulidCheckedByEditValue()
        {
            List<TreeCheckBoxSource> source = bindingSource1.DataSource as List<TreeCheckBoxSource>;
            if (source == null || source.Count == 0) return;

            foreach (var item in source)
            {
                if (_editValue.Contains(item.ID))
                    item.Checked = true;
                else
                    item.Checked = false;
            }
        }
        private void BulidTextByEditValue()
        {
            List<TreeCheckBoxSource> source = bindingSource1.DataSource as List<TreeCheckBoxSource>;
            if (source == null || source.Count == 0) return;
            StringBuilder strBulider = new StringBuilder();
            List<TreeCheckBoxSource> checkedItem = source.FindAll(delegate(TreeCheckBoxSource item) { return _editValue.Contains(item.ID) && item.ID != _topNode.ID; });
            foreach (var item in checkedItem)
            {
                if (strBulider.Length > 0) strBulider.Append(";");
                strBulider.Append(item.Name.ToString());
            }
            Text = strBulider.ToString();
        }

        /// <summary>
        /// 当前Text
        /// </summary>
        [Bindable(true)]
        public override string Text 
        {
            get { return base.Text; }
            set
            {
                if (base.Text == value) return;
                base.Text = popEdit1.Text = value ?? string.Empty;
            } 
        }

        static string allText = "Selecte ALL";
        public string AllText { get { return allText; } set { allText = value; } }

        TreeCheckBoxSource _topNode = null;

        /// <summary>
        /// 单击一个CheckBox时触发
        /// </summary>
        public event NodeClickEventHandler CheckedChanged;

        #endregion

        #region interface

        #region SetSource
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <typeparam name="T">数据源类型,必需有ID,ParentID属性</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="displayMember">显示的列</param>
        /// <param name="canCheckedProperty">判断是否显示CheckBox的属性</param>
        public void SetSource<T>(List<T> source, string displayMember, string canCheckedProperty)
        {
            CheckSourcePropertys(typeof(T), displayMember, canCheckedProperty);
            List<TreeCheckBoxSource> treeSources = BulidDataSource<T>(source, displayMember, canCheckedProperty);
            bindingSource1.DataSource = treeSources;
        }

        /// <summary>
        /// 检测传入的对象是否附合要求
        /// </summary>
        private void CheckSourcePropertys(Type t, string displayMember, string showCheckPropertyName)
        {
            StringBuilder strBulider = new StringBuilder();

            PropertyInfo property = t.GetProperty("ID");
            if (property == null) strBulider.Append("ID");

            property = t.GetProperty("ParentID");
            if (property == null)
            {
                if (strBulider.Length > 0) strBulider.Append(";");
                strBulider.Append("ParentID");
            }

            property = t.GetProperty(displayMember);
            if (property == null)
            {
                if (strBulider.Length > 0) strBulider.Append(";");
                strBulider.Append(displayMember);
            }

            property = t.GetProperty(showCheckPropertyName);
            if (property == null)
            {
                if (strBulider.Length > 0) strBulider.Append(";");
                strBulider.Append(showCheckPropertyName);
            }

            if (strBulider.Length > 0) throw new ApplicationException("传入的类型缺少属性:" + strBulider.ToString());
        }

        /// <summary>
        /// 把数据源转变为DataTable
        /// </summary>
        private List<TreeCheckBoxSource> BulidDataSource<T>(List<T> source, string displayMember, string canCheckedProperty)
        {
            List<TreeCheckBoxSource> treeSources = new List<TreeCheckBoxSource>();

            //Add top Node
            //_topNode = new TreeCheckBoxSource(Guid.NewGuid(), Guid.Empty, allText, true, false);
            //treeSources.Add(_topNode);

            foreach (T item in source)
            {
                TreeCheckBoxSource ts = new TreeCheckBoxSource();
                ts.ID = new Guid(Utility.GetObjectPropertyStringValue<T>(item, "ID"));
                string strParentID = Utility.GetObjectPropertyStringValue<T>(item, "ParentID");
                if(string.IsNullOrEmpty (strParentID))ts.ParentID = Guid.Empty;
                else ts.ParentID = new Guid(strParentID);

                ts.CanChecked = bool.Parse(Utility.GetObjectPropertyStringValue<T>(item, canCheckedProperty));
                ts.Name = Utility.GetObjectPropertyStringValue<T>(item, displayMember);
                ts.Checked = false;
                treeSources.Add(ts);
            }

            List<TreeCheckBoxSource> topTss = treeSources.FindAll(delegate(TreeCheckBoxSource item) { return ArgumentHelper.GuidIsNullOrEmpty(item.ParentID); });
            if (topTss == null || topTss.Count == 0)
            {
                _topNode = new TreeCheckBoxSource(Guid.NewGuid(), Guid.Empty, allText, true, false);
                foreach (var item in topTss)
                {
                    item.ParentID = _topNode.ID;
                }
                treeSources.Add(_topNode);
            }
            else if (topTss.Count == 1)
            {
                _topNode = new TreeCheckBoxSource(topTss[0].ID, Guid.Empty, allText, true, false);
                treeSources.Remove(topTss[0]);
                treeSources.Add(_topNode);
            }
            else
            {
                throw new ApplicationException("数据的顶级节点的ParentID必需为Guid.Empty");
            }

            return treeSources;
        }

        #endregion

        /// <summary>
        /// 全选
        /// </summary>
        public void SelectAll()
        {
            doSelectAll(true);
        }

        ///// <summary>
        ///// 是否选中了所有的项
        ///// </summary>
        //public bool IsAllChecked
        //{
        //    get
        //    {
        //        bool isAllSelected = true;
        //        List<TreeCheckBoxSource> tss = bindingSource1.DataSource as List<TreeCheckBoxSource>;
        //        foreach (var item in tss)
        //        {
        //            if (!item.Checked)
        //            {
        //                isAllSelected = false;
        //            }
        //        }

        //        return isAllSelected;
        //    }
        //}

        ///// <summary>
        ///// 是否一个都没选
        ///// </summary>
        //public bool IsNoneChecked
        //{
        //    get
        //    {
        //        bool isNoneChecked = true;
        //        List<TreeCheckBoxSource> tss = bindingSource1.DataSource as List<TreeCheckBoxSource>;
        //        foreach (var item in tss)
        //        {
        //            if (item.Checked)
        //            {
        //                isNoneChecked = false;
        //            }
        //        }

        //        return isNoneChecked;
        //    }
        //}

        /// <summary>
        /// 获取所有可以选择的值的列表
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetAllAvailableValues()
        {
            List<Guid> values = new List<Guid>();
            List<TreeCheckBoxSource> tss = bindingSource1.DataSource as List<TreeCheckBoxSource>;

            if (tss != null)
            {
                foreach (var item in tss)
                {
                    if (item.CanChecked)
                    {
                        values.Add(item.ID);
                    }
                }
            }

            return values;
        }

        /// <summary>
        /// 反全选
        /// </summary>
        public void UnSelectAll()
        {
            doSelectAll(false);
        }

        void doSelectAll(bool IsCheck)
        {
            List<TreeCheckBoxSource> tss = bindingSource1.DataSource as List<TreeCheckBoxSource>;
            _editValue = new List<Guid>();
            foreach (var item in tss)
            {
                item.Checked = IsCheck;
                if (item.CanChecked == false) item.Checked = false;

                if (item.Checked && item.ID != _topNode.ID) _editValue.Add(item.ID);
            }

            bindingSource1.ResetBindings(false);
            treeMain.Refresh();
            BulidTextByEditValue();
        }

        #endregion

        #region tree event

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            if (e.Node == null) return;

            TreeCheckBoxSource ts = treeMain.GetDataRecordByNode(e.Node) as TreeCheckBoxSource;
            if (ts == null) return;

            if (ts.CanChecked == false) e.Node.StateImageIndex = -1;
            else e.Node.StateImageIndex = ts.Checked ? 1 : 0;
        }

        private void treeMain_StateImageClick(object sender, NodeClickEventArgs e)
        {
            TreeCheckBoxSource ts = treeMain.GetDataRecordByNode(e.Node) as TreeCheckBoxSource;
            if (ts == null) return;
            ts.Checked = !ts.Checked;
            popEdit1.Tag = null;
            List<TreeCheckBoxSource> tss = bindingSource1.DataSource as List<TreeCheckBoxSource>;
            List<Guid> ids = GetChildIdsById(tss, ts.ID);
            _editValue = new List<Guid>();
            foreach (var item in tss)
            {
                if (item.CanChecked == false) continue;

                if (ids.Contains(item.ID)) { item.Checked = ts.Checked; }

                if (item.Checked && item.ID != _topNode.ID) _editValue.Add(item.ID);
            }

            bindingSource1.ResetBindings(false);
            treeMain.Refresh();
            BulidTextByEditValue();

            if (CheckedChanged != null) CheckedChanged(this, e);

        }

        /// <summary>
        /// 获取所有子项(包括自身)ID
        /// </summary>
        List<Guid> GetChildIdsById(List<TreeCheckBoxSource> data, Guid currentId)
        {
            List<Guid> childIds = new List<Guid>();
            childIds.Add(currentId);

            while (true)
            {
                List<TreeCheckBoxSource> childs = data.FindAll(delegate(TreeCheckBoxSource item)
                { return childIds.Contains(item.ParentID) && childIds.Contains(item.ID) == false; });

                if (childs == null || childs.Count == 0)
                    break;
                else
                {
                    foreach (TreeCheckBoxSource item in childs)
                    {
                        childIds.Add(item.ID);
                    }
                }
            }
            return childIds;
        }

        private void treeMain_LayoutUpdated(object sender, EventArgs e)
        {
            treeMain.LayoutUpdated -= new EventHandler(treeMain_LayoutUpdated);
            treeMain.ExpandAll();

        }

        #endregion
    }

    class TreeCheckBoxSource
    {
        public TreeCheckBoxSource() { }
        public TreeCheckBoxSource(Guid id, Guid parentId, string name, bool canChecked, bool isChecked)
        {
            Name = name; ID = id; ParentID = parentId; CanChecked = canChecked; Checked = isChecked;
        }
        public string Name { get; set; }
        public Guid ID { get; set; }
        public Guid ParentID { get; set; }
        public bool CanChecked { get; set; }
        public bool Checked { get; set; }
    }
}
