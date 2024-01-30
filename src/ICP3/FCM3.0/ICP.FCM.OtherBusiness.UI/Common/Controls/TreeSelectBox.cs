using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList;
using System.Reflection;

namespace ICP.FCM.OtherBusiness.UI.Common.Controls
{
    public partial class TreeSelectBox : DevExpress.XtraEditors.XtraUserControl
    {
        #region init

        public TreeSelectBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控制不能改变高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = 21;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.treeMain.LayoutUpdated += new System.EventHandler(this.treeMain_LayoutUpdated);
        }


        /// <summary>
        /// 背景色
        /// </summary>
        [Category("外观"),Description("改写了的属性，修复了默认属性设置时容易丢失的bug")]
        public new Color BackColor { get { return popEdit1.BackColor; } set { popEdit1.BackColor = value; } }

        /// <summary>
        /// 背景色
        /// </summary>
        [Category("外观"), Description("指定的背景色")]
        public Color SpecifiedBackColor { get { return popEdit1.BackColor; } set { popEdit1.BackColor = value; } }

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

        void PrepareTextChanged()
        {
            if (this.DataBindings["EditValue"] != null)
            {
                this.DataBindings["EditValue"].WriteValue();
            }
        }

        Guid _editValue = Guid.Empty;
        ///<summary>
        /// 当前值
        ///</summary>
        [Bindable(true)]
        public Guid EditValue
        {
            get { return _editValue; }
            set
            {
                if (_editValue == value)
                {
                    return;
                }

                _editValue = value;

                if (_editValue == Guid.Empty)
                {
                    this.PrepareTextChanged();
                    Text = string.Empty;
                    return;
                }


                List<TreeSelectSource> source = bindingSource1.DataSource as List<TreeSelectSource>;
                if (this.DesignMode || source == null || source.Count == 0)
                {
                    return;
                }


                TreeSelectSource tager = source.Find(delegate(TreeSelectSource item) { return item.ID == value; });
                if (tager != null)
                {
                    this.PrepareTextChanged();
                    Text = tager.Name;
                }
            }
        }

        [Bindable(true)]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                if (base.Text == value) return;
                popEdit1.EditValue = popEdit1.Text=base.Text =value ;

                if (this.DataBindings["EditText"] != null)
                {
                    this.DataBindings["EditText"].WriteValue();
                }
            }
        }

        static string allText = "Selecte ALL";
        public string AllText { get { return allText; } set { allText = value; } }

        /// <summary>
        /// 单击一个CheckBox时触发
        /// </summary>
        public event EventHandler Selected;

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
        public void SetSource<T>(List<T> source, string displayMember)
        {
            SetSource<T>(source, displayMember, null);
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <typeparam name="T">数据源类型,必需有ID,ParentID属性</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="displayMember">显示的列</param>
        /// <param name="permissionKey">是否可以选中的属性</param>
        /// <param name="canCheckedProperty">判断是否显示CheckBox的属性</param>
        public void SetSource<T>(List<T> source, string displayMember, string permissionKey)
        {
            CheckSourcePropertys(typeof(T), displayMember, permissionKey);
            List<TreeSelectSource> treeSources = BulidDataSource<T>(source, displayMember, permissionKey);
            bindingSource1.DataSource = treeSources;
        }

        /// <summary>
        /// 检测传入的对象是否附合要求
        /// </summary>
        private void CheckSourcePropertys(Type t, string displayMember, string permissionKey)
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

            if (string.IsNullOrEmpty(permissionKey)==false)
            {
                property = t.GetProperty(displayMember);
                if (property == null)
                {
                    if (strBulider.Length > 0) strBulider.Append(";");
                    strBulider.Append(displayMember);
                }
            }

            if (strBulider.Length > 0) throw new ApplicationException("传入的类型缺少属性:" + strBulider.ToString());
        }

        /// <summary>
        /// 把数据源转变为DataTable
        /// </summary>
        private List<TreeSelectSource> BulidDataSource<T>(List<T> source, string displayMember, string permissionKey)
        {
            List<TreeSelectSource> treeSources = new List<TreeSelectSource>();

            foreach (T item in source)
            {
                TreeSelectSource ts = new TreeSelectSource();
                ts.ID = new Guid(Utility.GetObjectPropertyStringValue<T>(item, "ID"));
                string strParentID = Utility.GetObjectPropertyStringValue<T>(item, "ParentID");
                if (string.IsNullOrEmpty(strParentID)) ts.ParentID = Guid.Empty;
                else ts.ParentID = new Guid(strParentID);

                ts.Name = Utility.GetObjectPropertyStringValue<T>(item, displayMember);
                string permission =Utility.GetObjectPropertyStringValue<T>(item, permissionKey);
                if (string.IsNullOrEmpty(permission) == false)
                { ts.Permission = bool.Parse(permission); }

                treeSources.Add(ts);
            }

            TreeSelectSource topTs = treeSources.Find(delegate(TreeSelectSource item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
            if (topTs != null) treeSources.Remove(topTs);

            return treeSources;
        }

        #endregion

        public void ClearItems()
        {
            popEdit1.ClosePopup();
            bindingSource1.Clear();
        }

        #endregion

        #region tree event

        private void treeMain_LayoutUpdated(object sender, EventArgs e)
        {
            this.treeMain.LayoutUpdated -= new System.EventHandler(this.treeMain_LayoutUpdated);
            treeMain.ExpandAll();

        }

        private void treeMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeSelectSource ts = treeMain.GetDataRecordByNode(e.Node) as TreeSelectSource;

            if (ts == null)
            {
                return;
            }

            if (ts.Permission == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
        }

        private void treeMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeMain.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo == null || hitInfo.Node == null)
            {
                return;
            }

            TreeSelectSource ts = treeMain.GetDataRecordByNode(hitInfo.Node) as TreeSelectSource;
            if (ts == null || (ts.Permission != null && ts.Permission == false))
            {
                return;
            }

            Text = ts.Name;
            EditValue  = ts.ID;

            if (Selected != null)
            {
                Selected(this, EventArgs.Empty);
            }

            this.popEdit1.ClosePopup();
        }

        private void popEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
            List<TreeSelectSource> source = bindingSource1.DataSource as List<TreeSelectSource>;
            if (source == null || source.Count == 0)
            {
                e.Cancel = true;
            }
        }

        #endregion

        public void ShowSelectedValue(Guid guid, string p)
        {
            this.EditValue = guid;
            this.popEdit1.Text = p;

            if (this.DataBindings["EditValue"] != null)
            {
                this.DataBindings["EditValue"].DataSourceUpdateMode = System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged;
            }
        }
    }

    class TreeSelectSource
    {
        public TreeSelectSource() { }
        public TreeSelectSource(Guid id, Guid parentId, string name, bool? permission)
        {
            Name = name; ID = id; ParentID = parentId; Permission = permission;
        }
        public string Name { get; set; }
        public Guid ID { get; set; }
        public Guid ParentID { get; set; }

        public bool? Permission { get; set; }
    }
}
