

namespace ICP.WF.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ICP.Framework.CommonLibrary.Attributes;

    [ToolboxBitmap(typeof(LWTableLayoutPanel), "Resources.ListView.bmp")]
    [DefaultProperty("DataProperty"),
    SRDescription("TableLayoutPanelDesc"),
    SRTitle("TableLayoutPanelTitle")]
    [Serializable()]
    public partial class LWTableLayoutPanel : TableLayoutPanel
    {
        public LWTableLayoutPanel()
        {
            InitializeComponent();

            base.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        }

        public LWTableLayoutPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();


            base.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            
        }

        #region 属性


        /// <summary>
        /// 大小
        /// </summary>
        [SRDisplayName("DispSize"), ICPBrowsable(true), SRDescription("DescSize"), SRCategory("Base")]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("Name"), 
        ICPBrowsable(true), 
        SRDescription("Name"), 
        SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// 布局
        /// </summary>
        [SRDisplayName("Dock"), 
        ICPBrowsable(true),
        SRDescription("Dock")]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        /// <summary>
        /// 字体
        /// </summary>
        [SRDisplayName("DispFont"), 
        ICPBrowsable(true), 
        SRCategory("Base"), 
        SRDescription("Font")]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [SRDisplayName("DispForeColor"), 
        ICPBrowsable(true), 
        SRCategory("Base")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        /// <summary>
        /// Tab顺序
        /// </summary>
        [SRDisplayName("DispTabIndex"), 
        ICPBrowsable(true), 
        SRDescription("DispTabIndex")]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }
        /// <summary>
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"), 
        ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }

        [SRDisplayName("DispColumnCount"),
        SRCategory("Style"),
        ICPBrowsable(true), 
        SRDescription("DescColumnCount")]
        [DefaultValue(0)]
        public new int ColumnCount
        {
            get { return base.ColumnCount; }
            set { base.ColumnCount = value; }
        }

        [SRDisplayName("DispColumnStyles"), 
        ICPBrowsable(true),
         SRCategory("Style"),
        SRDescription("ColumnStyles")]
        public TableLayoutColumnStyleCollection Columns
        {
            get { return base.ColumnStyles; }
        }

        [SRDisplayName("DispRowCount"), 
        ICPBrowsable(true),
        SRCategory("Style"),
        SRDescription("DescRowCount")]
        [DefaultValue(0)]
        public new int RowCount
        {
            get { return base.RowCount; }
            set { base.RowCount = value; }
        }

        [SRDisplayName("DispRowStyles"), 
        SRCategory("Style"),
        ICPBrowsable(true), 
        SRDescription("RowStyles")]
        public TableLayoutRowStyleCollection Rows
        {
            get { return base.RowStyles; }
        }

        int columnSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ColumnSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("ColumnSpan")]
        [DefaultValue(2)]
        public int ColumnSpan
        {
            get { return columnSpan; }
            set
            {
                if (value != columnSpan)
                {
                    columnSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetColumnSpan(this, value);
                    }
                }
            }
        }

        int rowSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("RowSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("RowSpan")]
        [DefaultValue(2)]
        public int RowSpan
        {
            get { return rowSpan; }
            set
            {
                if (value != rowSpan)
                {
                    rowSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetRowSpan(this, value);
                    }
                }
            }
        }

        #endregion



    }
}
