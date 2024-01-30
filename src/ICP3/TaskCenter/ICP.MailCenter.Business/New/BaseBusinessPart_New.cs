using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using ICP.FCM.Common.ServiceInterface.Interfaces;
using ICP.Framework.CommonLibrary.Client;
using System.Data;
using System.Threading;
using System.Reflection;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Drawing;
using ICP.Common.ServiceInterface.DataObjects;
namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 业务面板基类
    /// 包含逻辑:
    /// 1.构建工具栏
    /// 2.查询及绑定数据
    /// </summary>
    public partial class BaseBusinessPart_New : XtraUserControl, IBaseBusinessPart_New
    {
        /// <summary>
        /// 已选择的公司列表字符串
        /// </summary>
        private string _selectedCompanyIds;
        private string selectedCompanyIds
        {
            get
            {
                if (string.IsNullOrEmpty((_selectedCompanyIds)))
                {
                    _selectedCompanyIds = LocalData.IsDesignMode ? string.Empty : LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).Select(o => o.ID.ToString()).Aggregate((a, b) => a + "," + b);
                }

                return _selectedCompanyIds;
            }
            set { _selectedCompanyIds = value; }
        }

        protected object Parameter
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseBusinessPart_New()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.previousParameter = null;
                this.Parameter = null;
                this._ToolBarImages = null;
            };
        }
        #region 公共属性及服务

        /// <summary>
        /// 高级查询面板传递过来的高级查询条件字符串
        /// </summary>
        public string AdvanceQueryString { get; set; }

        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        public BusinessInfoExtractorFactory BusinessInfoExtractorFactory
        {
            get
            {
                return ClientHelper.Get<BusinessInfoExtractorFactory, BusinessInfoExtractorFactory>();

            }
        }

        public IICPCommonOperationService FCMCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }
        /// <summary>
        /// 数据绑定器
        /// </summary>
        public IDataBinder DataBinder
        {
            get
            {

                return BusinessInfoExtractorFactory.GetDataBinder(this.Parameter);
            }
        }
        /// <summary>
        /// 特殊条件控制器
        /// </summary>
        public IBusinessSpecialConditioner SpecialConditioner
        {
            get { return BusinessInfoExtractorFactory.GetSpeciallyConditioner(this.Parameter); }
        }
        /// <summary>
        /// 绑定后的数据是否被编辑
        /// </summary>
        protected bool IsDataEdited
        {
            get;
            set;
        }
        #endregion

        #region IBaseBusinessPart_New 成员

        public virtual List<OperationToolbarCommand> GetToolbarCommands(string templateCode)
        {
            return ToolbarTemplateLoader.Current[templateCode];
        }
        /// <summary>
        /// 业务上下文
        /// </summary>
        public virtual BusinessOperationContext OperationContext
        { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? Updatetime
        {
            get;
            set;
        }

        public virtual string OperationNo
        {
            get;
            set;
        }

        /// <summary>
        /// 已选择的公司列表字符串
        /// </summary>
        public virtual string SelectedCompanyIds
        {

            get
            {
                return this.selectedCompanyIds;
            }
            set
            {
                this.selectedCompanyIds = value;
            }
        }

        public virtual Guid OperationID
        {
            get;
            set;
        }

        public Guid FormID
        {
            get;
            set;
        }

        /// <summary>
        /// 模板代码
        /// </summary>
        public string TemplateCode
        {
            get;
            set;
        }
        public virtual OperationType OperationType
        {
            get;
            set;
        }

        /// <summary>
        /// 获取工具栏拓展点名称，必须在参数解析后才可调用
        /// </summary>
        /// <returns></returns>
        public string GetToolbarUIExtensionSite()
        {
            return this.TemplateCode;
        }

        private ImageList _ToolBarImages;
        public ImageList ToolBarImages
        {
            get
            {
                if (_ToolBarImages == null)
                    _ToolBarImages = this.barManager.Images as ImageList;
                return _ToolBarImages;
            }
            set { _ToolBarImages = value; }
        }

        /// <summary>
        ///注册拓展点
        /// </summary>
        public virtual void RegisterExtensionSite()
        {
            string businessToolbarUISiteName = GetToolbarUIExtensionSite();
            if (!RootWorkItem.UIExtensionSites.Contains(businessToolbarUISiteName))
            {
                RootWorkItem.UIExtensionSites.RegisterSite(businessToolbarUISiteName, this.barTool);
            }
        }
        public virtual void UnRegisterExtensionSite()
        {
            string businessToolbarUISiteName = GetToolbarUIExtensionSite();
            if (RootWorkItem.UIExtensionSites.Contains(businessToolbarUISiteName))
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(businessToolbarUISiteName);
            }
            foreach (string registeredSiteName in this.registeredSiteNames)
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(registeredSiteName);
            }
        }
        /// <summary>
        /// 数据源变化后的处理
        /// </summary>
        public virtual void AfterDataSourceChanged()
        {

        }

        public virtual void Init(object parameter)
        {
            this.Parameter = parameter;
            GetInfo(parameter);
            this.RegisterExtensionSite();
            this.barManager.Images = ToolBarImages = ToobarItemFactory.ImageList;

            BuildToobar(this.TemplateCode);
            ToolbarManager.baseBarManager = this.barManager;
        }

        private List<string> registeredSiteNames = new List<string>();
        public List<string> RegisterSiteNames
        {
            get
            {
                return this.registeredSiteNames;
            }
            set
            {
                this.registeredSiteNames = value;
            }
        }
        /// <summary>
        /// 建立工具栏信息
        /// </summary>
        /// <param name="commands"></param>
        protected void BuildToobar(string templateCode)
        {
            List<OperationToolbarCommand> commands = GetToolbarCommands(templateCode);
            if (commands == null || commands.Count <= 0)
                return;
            foreach (OperationToolbarCommand command in commands)
            {
                AddToolBarElement(command);
            }
        }

        public void AddToolBarElement(OperationToolbarCommand command)
        {
            ToolbarBuilder builder = new ToolbarBuilder(command);
            builder.CurrentBaseBusinessPart = this;
            DevExpress.XtraBars.BarItem barItem = builder.BuildIn(RootWorkItem, this.barManager);
            if (!string.IsNullOrEmpty(command.ImageName))
            {
                if (ToolBarImages == null)
                    return;
                int index = ToolBarImages.Images.IndexOfKey(command.ImageName);
                barItem.ImageIndex = index;
            }
        }

        /// <summary>
        /// 上一次解析的参数
        /// </summary>
        private object previousParameter;
        /// <summary>
        /// 参数解析
        /// </summary>
        /// <param name="parameter"></param>
        public void GetInfo(object parameter)
        {
            //如果本次解析的参数和上次相同，则不解析，直接返回
            if (ReferenceEquals(parameter, previousParameter))
            {
                return;
            }
            IBusinessInfoExtractor extractor = BusinessInfoExtractorFactory.GetExtractor(parameter);
            OperationContext = extractor.Extract(parameter);
            this.OperationID = OperationContext.OperationID;
            this.OperationNo = OperationContext.OpeationNO;
            this.OperationType = OperationContext.OperationType;
            this.FormID = OperationContext.FormId;
            this.TemplateCode = OperationContext[CommonConstants.TemplateCodeKey] as string;
            if (OperationContext.ContainsKey(CommonConstants.AdvanceQueryStringKey))
            {
                this.AdvanceQueryString = OperationContext[CommonConstants.AdvanceQueryStringKey] as string;
            }
            else
            {
                this.AdvanceQueryString = this.TemplateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Unknown.ToString()) ? this.AdvanceQueryString : string.Empty;
            }
            this.previousParameter = parameter;
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="mergeAdvanceQueryString">查询时是否需要合并高级查询条件</param>
        /// <returns></returns>
        public virtual BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString)
        {
            IQueryCriteriaGetter getter = BusinessInfoExtractorFactory.GetQueryCriteriaGetter(this.Parameter);
            return getter.Get(this, this.Parameter, mergeAdvanceQueryString);
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="mergeAdvanceQueryString">查询时是否需要合并高级查询条件</param>
        public void QueryData(bool mergeAdvanceQueryString)
        {
            BusinessQueryCriteria criteria = GetQueryCriteria(mergeAdvanceQueryString);
            IBusinessQueryServiceGetter getter = BusinessInfoExtractorFactory.GetQueryService(this.Parameter);
            string tip = LocalData.IsEnglish ? "Querying Data..." : "正在查询数据。。。";
            BeginShowAnimation(tip);
            WaitCallback callback = (data) =>
            {
                ClientHelper.SetApplicationContext();
                BindParameter temp = data as BindParameter;
                try
                {
                    object result = getter.Query(criteria, temp.Parameter);
                    DataTable dt = GetInnerTable(result);
                    if (dt != null)
                    {
                        AddCustomColumn(dt);
                    }

                    InnerBindData(result, temp.Parameter);
                    temp = null;
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }

            };
            BindParameter parameter = new BindParameter { Parameter = this.Parameter, QueryCriteria = criteria, QueryServiceGetter = getter };
            ThreadPool.QueueUserWorkItem(callback, parameter);
        }

        private delegate void DataBindDelegate(object result, object parameter);

        private void InnerBindData(object result, object parameter)
        {
            DataBindDelegate bindDelegate = new DataBindDelegate(InnerBindData2);
            this.Invoke(bindDelegate, result, parameter);

        }
        private void InnerBindData2(object result, object parameter)
        {
            DataBinder.DataBind(this, result, parameter);
            EndShowAnimation();
            PostHande(result);
            IsDataEdited = false;
            AfterDataSourceChanged();
        }
        /// <summary>
        /// 数据绑定后处理
        /// </summary>
        /// <param name="result"></param>
        public void PostHande(object result)
        {
            IPostDataBindHandler postHandler = BusinessInfoExtractorFactory.GetPostHandler(this.Parameter);
            postHandler.PostHandle(this, result, this.Parameter);
        }

        /// <summary>
        /// 判断对象是否为DataTable类型，如果是，则返回对象本身，否则查找对象是否包含DataTable类型的属性成员或者字段，如果有，则返回属性的值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable GetInnerTable(object result)
        {
            if (result == null)
                return null;
            if (result.GetType() == typeof(DataTable))
                return (DataTable)result;
            PropertyInfo temp = result.GetType().GetProperties().FirstOrDefault(property => property.PropertyType == typeof(DataTable));
            if (temp != null && !string.IsNullOrEmpty(temp.Name))
            {
                return temp.GetValue(result, null) as DataTable;
            }
            else
            {
                FieldInfo fieldInfo = result.GetType().GetFields().FirstOrDefault(field => field.FieldType == typeof(DataTable));
                if (fieldInfo != null && string.IsNullOrEmpty(fieldInfo.Name))
                {
                    return fieldInfo.GetValue(result) as DataTable;
                }
            }
            return new DataTable();
        }

        /// <summary>
        /// 查询并绑定数据
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void BindData(object parameter)
        {
            this.Parameter = parameter;
            GetInfo(parameter);
            if (SpecialConditioner.QueryDataFired(this, TemplateCode, AdvanceQueryString))
                QueryData(!string.IsNullOrEmpty(this.AdvanceQueryString));
        }

        /// <summary>
        /// 向数据库返回的表结构添加自定义列
        /// </summary>
        /// <param name="dt"></param>
        public virtual void AddCustomColumn(DataTable dt)
        {


        }
        /// <summary>
        /// 设置绑定数据源
        /// </summary>
        /// <param name="data"></param>
        public void SetBaseBindSource(object data)
        {
            this.baseBindingSource.DataSource = data;
            this.baseBindingSource.ResetBindings(false);
        }
        public virtual void Refresh()
        {

        }
        public virtual void RefreshDetail()
        {

        }
        #region 工具栏提交动作的动画显示控制
        protected virtual void BeginShowAnimation(string tip)
        {

        }

        protected virtual void EndShowAnimation()
        {

        }


        #endregion
        #endregion


    }
    public class QueryResult
    {
        public object Result { get; set; }
        public object Parameter { get; set; }
    }
    /// <summary>
    /// 数据绑定参数类
    /// </summary>
    public class BindParameter
    {
        public ICP.Message.ServiceInterface.Message Message { get; set; }
        public object Parameter { get; set; }
        public List<string> Nos { get; set; }
        public DataTable Dt { get; set; }
        public bool NeedBindData { get; set; }
        public BusinessQueryCriteria QueryCriteria { get; set; }
        public IBusinessQueryServiceGetter QueryServiceGetter { get; set; }
    }
}
