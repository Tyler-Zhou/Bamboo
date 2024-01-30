using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 调动类型数据库
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWDutyChangeTypes), "Resources.database.bmp")]
    [SRDescription("DutyChangeBindingSourceDesc"),
      SRTitle("DutyChangeBindingSourceTitle")]
    public partial class LWDutyChangeTypes : System.Windows.Forms.BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数


        public LWDutyChangeTypes()
        {
            this.DataSource = new List<DictCodeData>();
        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> vars)
        {
            if (this.DesignMode == false)
            {
                this.DataSource = GetDictCodes();
            }
        }

        #endregion


        #region 本地方法
        private List<DictCodeData> GetDictCodes()
        {
            List<DictCodeData> items = new List<DictCodeData>();
            DictCodeData dc = new DictCodeData();
            dc.Key = "0";
            dc.CName = "调动";
            dc.EName = "Transfer";
            items.Add(dc);

            dc = new DictCodeData();
            dc.Key = "1";
            dc.CName = "升迁";
            dc.EName = "Promotion";
            items.Add(dc);

            dc = new DictCodeData();
            dc.Key = "2";
            dc.CName = "降级";
            dc.EName = "Demotion";
            items.Add(dc);

            return items;
        }
        #endregion

        #region 屏蔽属性



        [Browsable(false)]
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
            }
        }


        [Browsable(false)]
        public new string DataMember
        {
            get
            {
                return base.DataMember;
            }
            set
            {
                base.DataMember = value;
            }
        }

        [Browsable(false)]
        public override string Filter
        {
            get
            {
                return base.Filter;
            }
            set
            {
                base.Filter = value;
            }
        }

        [Browsable(false)]
        public override bool AllowNew
        {
            get
            {
                return base.AllowNew;
            }
            set
            {
                base.AllowNew = value;
            }
        }

        [Browsable(false)]
        public new string Sort
        {
            get
            {
                return base.Sort;
            }
            set
            {
                base.Sort = value;
            }
        }
        #endregion

        #region IBindingSourceTypeService 接口实现
        [Browsable(false)]
        public System.Type DataType
        {
            get
            {
                return typeof(DictCodeData);
            }
        }

        #endregion
    }


}
