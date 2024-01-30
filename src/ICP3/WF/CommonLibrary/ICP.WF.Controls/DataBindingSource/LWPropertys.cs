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
    /// 报销类型
    /// </summary>
    [DefaultProperty("ComboBoxType")]
    [ToolboxBitmap(typeof(LWPropertys), "Resources.database.bmp")]
    [SRDescription("PropertyBindingSourceDesc"),
     SRTitle("PropertyBindingSourceTitle")]
    public class LWPropertys : BindingSource, IInitiDataService, IBindingSourceTypeService
    {
        #region 构造函数


        public LWPropertys()
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
            dc.Key = "Personal";
            dc.CName = "个人报销";
            dc.EName = "Personal Expenses";
            items.Add(dc);

            dc = new DictCodeData();
            dc.Key = "Company";
            dc.CName = "公司报销";
            dc.EName = "Company Expenses";
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
