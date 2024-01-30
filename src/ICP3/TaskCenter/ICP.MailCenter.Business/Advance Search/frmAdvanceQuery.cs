using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Operation.Common.ServiceInterface;
using System.Reflection;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System.IO;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务高级查询界面
    /// </summary>
    public partial class frmAdvanceQuery : XtraForm
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public frmAdvanceQuery()
        {
            InitializeComponent();
        }
        private Dictionary<string, object> initValues;

        public string AdvanceQueryString
        {
            get;
            set;
        }
        private string businessTypeKey = Constants.BusinessTypeKey;
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        private ICP.Operation.Common.ServiceInterface.IBusinessQueryPart CurrentQueryPart
        {
            get
            {
                if (this.pnlBusinessQueryPart.Controls.Count <= 0)
                    return null;
                if (!(this.pnlBusinessQueryPart.Controls[0] is IBusinessQueryPart))
                    return null;
                return this.pnlBusinessQueryPart.Controls[0] as IBusinessQueryPart;
            }
        }
        public void Init(Dictionary<string, object> initValues)
        {
            this.initValues = initValues;
            InnerInit();
        }

        private void InnerInit()
        {
            if (CurrentQueryPart != null)
            {
                CurrentQueryPart.Init(this.initValues);
                CurrentQueryPart.Reset();

            }
            else
            {

                IBusinessQueryPart queryPart = GetPart();
                queryPart.Locale();
                UserControl uc = queryPart as UserControl;
                uc.Dock = DockStyle.Fill;
                this.pnlBusinessQueryPart.Controls.Add(uc);

            }
        }
        private IBusinessQueryPart GetPart()
        {
            string typeName = this.initValues[businessTypeKey].ToString();
            BusinessType type = (BusinessType)Enum.Parse(typeof(BusinessType), typeName, true);
            Type[] types = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICP.MailCenter.Business.UI.dll")).GetExportedTypes().Where(queryType => typeof(IBusinessQueryPart).IsAssignableFrom(queryType) && queryType.IsClass).ToArray();
            Type ucType = types.Where(t =>
            {
                PropertyInfo property = t.GetProperty("Type");
                DefaultValueAttribute attribute = property.GetCustomAttributes(typeof(DefaultValueAttribute), false).First() as DefaultValueAttribute;
                return (BusinessType)attribute.Value == type;
            }
             ).First();
            var queryPart = this.WorkItem.Items.AddNew(ucType) as ICP.Operation.Common.ServiceInterface.IBusinessQueryPart;
            return queryPart;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.CurrentQueryPart.Reset();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.AdvanceQueryString = this.CurrentQueryPart.GetAdvanceQueryString();
        }


    }


}
