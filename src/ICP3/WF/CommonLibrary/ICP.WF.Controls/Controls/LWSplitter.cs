using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Attributes;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace ICP.WF.Controls
{
    [ToolboxBitmap(typeof(ImageCollection), "Bitmaps256.SplitterControl.bmp")]
    [DefaultProperty("DataProperty"), SRDescription("LWSplitterDesc"), SRTitle("LWSplitterTitle")]
    [Serializable()]
    public partial class LWSplitter : SplitterControl
    {
        public LWSplitter()
        {
            InitializeComponent();
        }

        public LWSplitter(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("Name"), ICPBrowsable(true), SRDescription("Name"), SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// 布局
        /// </summary>
        [SRDisplayName("Dock"), ICPBrowsable(true), SRDescription("Dock")]
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
        [SRDisplayName("DispFont"), ICPBrowsable(true), SRCategory("Base"), SRDescription("Font")]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// 文本
        /// </summary>
        [SRDisplayName("DispText"), ICPBrowsable(true), SRCategory("Base"), SRDescription("Text")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [SRDisplayName("DispForeColor"), ICPBrowsable(true), SRCategory("Base")]
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
        [SRDisplayName("DispTabIndex"), ICPBrowsable(true), SRCategory("Base"), SRDescription("DispTabIndex")]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }
        /// <summary>
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"), ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }
        /// <summary>
        /// TAG
        /// </summary>
        [SRDisplayName("Tag"), ICPBrowsable(true), SRDescription("Tag"), SRCategory("DataBinding")]
        public new object Tag
        {
            get { return base.Tag; }
            set { base.Tag = value; }
        }
        #endregion

    }
}
