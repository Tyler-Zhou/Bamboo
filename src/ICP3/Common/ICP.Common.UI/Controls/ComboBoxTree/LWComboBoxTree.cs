//-----------------------------------------------------------------------
// <copyright file="LWComboBoxTree.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Controls
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;

    /// <summary>
    /// 下拉树控件
    /// </summary>
    public class LWComboBoxTree : PopupContainerEdit
    {
        /// <summary>
        /// 弹出树控件
        /// </summary>
        public ComboBoxTreePopupControl PopupControl { get; set; }

        public EventHandler OnFirstTimeEnter;
        private bool isFirstTimeEnter = true;
        #region 初始化

        /// <summary>
        /// 
        /// </summary>
        public LWComboBoxTree()
            : base()
        {
            Init();
            this.Disposed += delegate
            {
                this.OnFirstTimeEnter = null;
                this.PopupControl = null;

            };
        }
        protected override void OnEnter(EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                if (OnFirstTimeEnter != null)
                {
                    OnFirstTimeEnter(this, e);
                }
                isFirstTimeEnter = false;
            }
            base.OnEnter(e);
        }
        private void Init()
        {
            PopupContainerControl popupContainerControl1 = new PopupContainerControl();
            popupContainerControl1.Width = this.Width;
            popupContainerControl1.Height = 330;
            PopupControl = new ComboBoxTreePopupControl(this);
            PopupControl.Dock = DockStyle.Fill;
            popupContainerControl1.Controls.Add(PopupControl);

            this.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton( ButtonPredefines.Combo)});
            this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.Properties.PopupControl = popupContainerControl1;
            this.Properties.ShowPopupCloseButton = false;
            this.Properties.PopupSizeable = false;
        }
        #endregion

        #region 公共属性


        /// <summary>
        /// 显示成员
        /// </summary>
        [Description("获取或则设置树显示成员"), DefaultValue(""), Category("Data"),
         TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design"),
         Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public string DisplayMember
        {
            get
            {
                return PopupControl.DisplayMember;
            }
            set
            {
                this.PopupControl.DisplayMember = value;
            }
        }

        /// <summary>
        /// 父键
        /// </summary>
        [Description("获取或设置父键"), DefaultValue(""), Category("Data"),
        TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design"),
        Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public string ParentMember
        {
            get
            {
                return PopupControl.ParentMember;
            }
            set
            {
                PopupControl.ParentMember = value;
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        [Description("获取或则设置ID"), DefaultValue(""), Category("Data"),
        TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design"),
        Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public string ValueMember
        {
            get
            {
                return PopupControl.ValueMember;
            }
            set
            {
                PopupControl.ValueMember = value;
            }
        }

        /// <summary>
        /// 同级分割符
        /// </summary>
        [Bindable(false)]
        public string Separator
        {
            get
            {
                return PopupControl.Separator;
            }
            set
            {
                PopupControl.Separator = value;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [Browsable(true)]
        [Description("设置或者获取数据源"), Category("Data")]
        public object DataSource
        {
            get
            {
                return PopupControl.DataSource;
            }
            set
            {
                PopupControl.DataSource = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public object RootValue
        {
            get
            {
                return PopupControl.RootValue;
            }
            set
            {
                PopupControl.RootValue = value;
            }
        }

        /// <summary>
        /// 允许多选
        /// </summary>
        [Browsable(true)]
        public bool AllowMultSelect
        {
            get
            {
                return PopupControl.AllowMultSelect;
            }
            set
            {
                PopupControl.AllowMultSelect = value;
            }
        }


        /// <summary>
        /// 数据源
        /// </summary>
        [Browsable(false)]
        [Bindable(true)]
        public object SelectedValue
        {
            get
            {
                return this.Tag;
            }
            set
            {
                if (this.Tag != value)
                {
                    this.Tag = value;

                    object v = PopupControl.GetNodeValueById(this.DisplayMember, value);
                    if (v != null)
                    {
                        this.Text = v.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public object GetSelectedValues(string fieldName)
        {
            return PopupControl.GetCheckValues(fieldName);
        }

        public void InitSelectedNode(object nodeId)
        {
            PopupControl.InitSelectedNode(nodeId);
        }
        /// <summary>
        /// 设置树形控件展开或折叠状态
        /// </summary>
        /// <param name="expandAll"></param>
        public void SetComboBoxTreeState(bool expandAll)
        {
            PopupControl.SetComboBoxTreeState(expandAll);
        }

        #endregion
    }
}
