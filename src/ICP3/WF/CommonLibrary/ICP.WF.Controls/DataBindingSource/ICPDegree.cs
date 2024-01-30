using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.WF.Controls;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 学历
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWPropertys), "Resources.database.bmp")]
    [SRDescription("DegreeBindingSourceDesc"),
     SRTitle("DegreeBindingSourceTitle")]
    public class ICPDegree : BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数


        public ICPDegree()
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
            dc.Key = "大学专科";
            dc.CName = "大学专科";
            dc.EName = "大学专科";
            items.Add(dc);

            dc = new DictCodeData();
            dc.Key = "大学本科";
            dc.CName = "大学本科";
            dc.EName = "大学本科";
            items.Add(dc);

            dc = new DictCodeData();
            dc.Key = "研究生";
            dc.CName = "研究生";
            dc.EName = "研究生";
            items.Add(dc);


            dc = new DictCodeData();
            dc.Key = "博士生";
            dc.CName = "博士生";
            dc.EName = "博士生";
            items.Add(dc);


            dc = new DictCodeData();
            dc.Key = "MBA";
            dc.CName = "MBA";
            dc.EName = "MBA";
            items.Add(dc);

            dc = new DictCodeData();
            dc.Key = "其他";
            dc.CName = "其他";
            dc.EName = "其他";
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
