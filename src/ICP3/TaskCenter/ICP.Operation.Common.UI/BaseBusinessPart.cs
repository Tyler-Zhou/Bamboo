using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.Business.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CommonConstants = ICP.Operation.Common.ServiceInterface.CommonConstants;

namespace ICP.Operation.Common.UI
{
    /// <summary>
    /// 业务面板基类
    /// 包含逻辑:
    /// 1.构建工具栏
    /// 2.查询及绑定数据
    /// </summary>
    public partial class BaseBusinessPart : XtraUserControl, IBaseBusinessPart_New
    {
        #region Definition

        #region Fields
        /// <summary>
        /// 上一次解析的参数
        /// </summary>
        private object previousParameter;
        /// <summary>
        /// 数据绑定参数
        /// </summary>
        public BindParameter Temp = null;
        /// <summary>
        /// 重新搜索的条件
        /// </summary>
        public BusinessQueryCriteria Requerys;
        #endregion

        #region Property
        /// <summary>
        /// 已选择的公司列表字符串
        /// </summary>
        private string _selectedCompanyIds;
        /// <summary>
        /// 已选择的公司列表字符串
        /// </summary>
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
        /// <summary>
        /// 参数
        /// </summary>
        protected object Parameter
        {
            get;
            set;
        }
        /// <summary>
        /// 高级查询面板传递过来的高级查询条件字符串
        /// </summary>
        public string AdvanceQueryString
        {
            get;
            set;
        }
        /// <summary>
        /// 参数全名
        /// </summary>
        public string ParameterFullName
        {
            get { return Parameter.GetType().FullName; }
        }
        /// <summary>
        /// 绑定后的数据是否被编辑
        /// </summary>
        protected bool IsDataEdited
        {
            get;
            set;
        }
        /// <summary>
        /// 选择的范围
        /// </summary>
        public string SelectedScope { get; set; }
        /// <summary>
        /// 获取工具栏拓展点名称，必须在参数解析后才可调用
        /// </summary>
        /// <returns></returns>
        public string GetToolbarUIExtensionSite()
        {
            return TemplateCode;
        }
        /// <summary>
        /// 模板代码
        /// </summary>
        public virtual string TemplateCode
        {
            get;
            set;
        }
        /// <summary>
        /// 业务号
        /// </summary>
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
                return selectedCompanyIds;
            }
            set
            {
                selectedCompanyIds = value;
            }
        }
        /// <summary>
        /// 服务器查询字符串
        /// </summary>
        public virtual string ServerQueryString
        {
            get;
            set;
        }
        /// <summary>
        /// 业务上下文
        /// </summary>
        public virtual BusinessOperationContext OperationContext { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? Updatetime
        {
            get;
            set;
        }
        /// <summary>
        /// 业务的ID
        /// </summary>
        public virtual Guid OperationID
        {
            get;
            set;
        }
        /// <summary>
        /// 业务口岸ID
        /// </summary>
        public virtual Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 表单ID
        /// </summary>
        public virtual Guid FormID
        {
            get;
            set;
        }
        /// <summary>
        /// 选择记录的业务ID
        /// </summary>
        public Guid[] SelctedOperationIDs { get; set; }
        /// <summary>
        /// 选择记录的业务单号
        /// </summary>
        public string[] SelectedOperationNos { get; set; }
        /// <summary>
        /// 选择记录的口岸ID
        /// </summary>
        public Guid[] SelctedCompanyIDs { get; set; }
        /// <summary>
        /// 选择记录的业务类型
        /// </summary>
        public OperationType[] SelectedOperationTypes { get; set; }
        /// <summary>
        /// 选择记录的修改时间
        /// </summary>
        public DateTime?[] SelectedUpdateDates { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public virtual OperationType OperationType
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件中心搜索类型
        /// </summary>
        public virtual SearchActionType SearchType
        { get; set; }
        /// <summary>
        /// 需要搜索SQLServer
        /// </summary>
        public virtual bool NeedSearchInSQLServer { get; set; }
        /// <summary>
        /// 图片集合
        /// </summary>
        private ImageList _ToolBarImages;
        /// <summary>
        /// 图片集合
        /// </summary>
        public ImageList ToolBarImages
        {
            get { return _ToolBarImages ?? (_ToolBarImages = barManager.Images as ImageList); }
            set { _ToolBarImages = value; }
        }
        /// <summary>
        /// 注册站点名称
        /// </summary>
        private List<string> registeredSiteNames = new List<string>();
        /// <summary>
        /// 注册点名称
        /// </summary>
        public List<string> RegisterSiteNames
        {
            get
            {
                return registeredSiteNames;
            }
            set
            {
                registeredSiteNames = value;
            }
        }
        /// <summary>
        /// 业务信息实体类
        /// </summary>
        public BusinessQueryCriteria Criteria
        {
            get;
            set;
        }
        #endregion

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        #region 业务信息抽取(BusinessInfoExtractorFactory)
        /// <summary>
        /// 业务信息抽取
        /// </summary>
        private BusinessInfoExtractorFactory _BusinessInfoExtractorFactory;
        /// <summary>
        /// 业务信息抽取
        /// </summary>
        public BusinessInfoExtractorFactory BusinessInfoExtractorFactory
        {
            get
            {
                return _BusinessInfoExtractorFactory ??
                       (_BusinessInfoExtractorFactory =
                           ClientHelper.Get<BusinessInfoExtractorFactory, BusinessInfoExtractorFactory>());
            }
        }
        #endregion

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryServiceGetter Getter
        {
            get
            {
                return BusinessInfoExtractorFactory.GetQueryService(ParameterFullName);
            }
        }
        /// <summary>
        /// FCM 通用接口
        /// </summary>
        public IICPCommonOperationService FCMCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }
        /// <summary>
        /// 业务回调接口
        /// </summary>
        public IBusinessOperationHandleService BusinessOperationHandleService
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessOperationHandleService>();
            }
        }
        /// <summary>
        /// 数据绑定器
        /// </summary>
        private IDataBinder _DataBinder;
        /// <summary>
        /// 数据绑定器
        /// </summary>
        public IDataBinder DataBinder
        {
            get
            {
                if (_DataBinder == null)
                    _DataBinder = BusinessInfoExtractorFactory.GetDataBinder(ParameterFullName);

                return _DataBinder;
            }
        }
        /// <summary>
        /// 特殊条件控制器
        /// </summary>
        private IBusinessSpecialConditioner _SpecialConditioner;
        /// <summary>
        /// 特殊条件控制器
        /// </summary>
        public IBusinessSpecialConditioner SpecialConditioner
        {
            get
            {
                if (_SpecialConditioner == null)
                    _SpecialConditioner = BusinessInfoExtractorFactory.GetSpeciallyConditioner(ParameterFullName);

                return _SpecialConditioner;
            }
        }
        /// <summary>
        /// 数据绑定后处理
        /// </summary>
        public IPostDataBindHandler PostHandler
        {
            get
            {
                return BusinessInfoExtractorFactory.GetPostHandler(ParameterFullName);
            }
        } 
        #endregion

        #region Delegate
        private delegate void DataBindDelegate(object result, object parameter);
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseBusinessPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                EndShowAnimation();
                previousParameter = null;
                Parameter = null;
                _ToolBarImages = null;
                Requerys = null;
            };
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void Init(object parameter)
        {
            Parameter = parameter;
            GetInfo(parameter);
            RegisterExtensionSite();
            barManager.Images = ToolBarImages = ToobarItemFactory.ImageList;
            BuildToobar(TemplateCode);
            ToolbarManager.baseBarManager = barManager;
            HookEvent();
        }
        private void HookEvent()
        {
            BusinessOperationHandleService.BusinessOperationCompleted += OnBusinessOperationCompleted;
        }
        #endregion

        #region Event
        /// <summary>
        /// 邮件中心回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnBusinessOperationCompleted(object sender, CommonEventArgs<BusinessOperationParameter> e)
        {
            if (LocalData.ApplicationType == ApplicationType.EmailCenter && e.Data.Relation != null)
            {
                UIHelper.CurrentMessageRelation =
                    new List<OperationMessageRelation> { e.Data.Relation };
            }
            BusinessOperationParameter businessOperationParameter = e.Data;
            if (string.IsNullOrEmpty(businessOperationParameter.TemplateCode))
            {
                businessOperationParameter.TemplateCode = businessOperationParameter.Context["TemplateCode"].ToString();
            }
            if (businessOperationParameter.TemplateCode != TemplateCode)
            {
                return;
            }
            if (!UIHelper.IsCancelOperation()) return;
            RootWorkItem.State["CancelOperation"] = false;
        } 
        #endregion

        #region Method

        #region 工具栏提交动作的动画显示控制
        /// <summary>
        /// 显示Loading窗体
        /// </summary>
        /// <param name="tip"></param>
        public virtual void BeginShowAnimation(string tip)
        {

        }
        /// <summary>
        /// 关闭Loading窗体
        /// </summary>
        public virtual void EndShowAnimation()
        {

        }
        #endregion

        #region 更多查询面板
        /// <summary>
        /// panelMore 面板是否显示出来
        /// </summary>
        /// <param name="show">是否显示</param>
        public virtual void PanelMoreShow(bool show)
        {

        }
        #endregion

        /// <summary>
        /// 工具栏命令
        /// </summary>
        /// <param name="templateCode">模版CODE</param>
        /// <returns></returns>
        public virtual List<OperationToolbarCommand> GetToolbarCommands(string templateCode)
        {
            return ToolbarTemplateLoader.Current[templateCode];
        }
        /// <summary>
        ///注册拓展点
        /// </summary>
        public virtual void RegisterExtensionSite()
        {
            string businessToolbarUISiteName = GetToolbarUIExtensionSite();
            if (!RootWorkItem.UIExtensionSites.Contains(businessToolbarUISiteName))
            {
                RootWorkItem.UIExtensionSites.RegisterSite(businessToolbarUISiteName, barTool);
            }
        }
        /// <summary>
        /// 注销拓展点
        /// </summary>
        public virtual void UnRegisterExtensionSite()
        {
            string businessToolbarUISiteName = GetToolbarUIExtensionSite();
            if (RootWorkItem.UIExtensionSites.Contains(businessToolbarUISiteName))
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(businessToolbarUISiteName);
            }
            foreach (string registeredSiteName in registeredSiteNames)
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
        /// <summary>
        /// F5 查询
        /// </summary>
        public virtual void F5Query()
        {
        }
        /// <summary>
        /// 建立工具栏信息
        /// </summary>
        /// <param name="templateCode"></param>
        protected void BuildToobar(string templateCode)
        {
            List<OperationToolbarCommand> commands = GetToolbarCommands(templateCode);
            if (commands == null || commands.Count <= 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? string.Format("Could not find node [{0}] toolbar configuration items，Please contact the system administrator!", templateCode) : string.Format("未能找到节点[{0}]工具栏配置项，请联系系统管理员!", templateCode));
                return;
            }
            foreach (OperationToolbarCommand command in commands)
            {
                AddToolBarElement(command);
            }
        }
        /// <summary>
        /// 添加工具栏元素
        /// </summary>
        /// <param name="command"></param>
        public void AddToolBarElement(OperationToolbarCommand command)
        {
            ToolbarBuilder builder = new ToolbarBuilder(command) {CurrentBaseBusinessPart = this};
            BarItem barItem = builder.BuildIn(RootWorkItem, barManager);
            if (!string.IsNullOrEmpty(command.ImageName))
            {
                if (ToolBarImages == null)
                    return;
                int index = ToolBarImages.Images.IndexOfKey(command.ImageName);
                barItem.ImageIndex = index;
            }
        }
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
            IBusinessInfoExtractor extractor = BusinessInfoExtractorFactory.GetExtractor(parameter.GetType().FullName);
            OperationContext = extractor.Extract(parameter, false);
            OperationID = OperationContext.OperationID;
            OperationNo = OperationContext.OperationNO;
            OperationType = OperationContext.OperationType;
            FormID = OperationContext.FormId;
            TemplateCode = OperationContext[CommonConstants.TemplateCodeKey] as string;
            if (OperationContext.ContainsKey(CommonConstants.AdvanceQueryStringKey))
            {
                AdvanceQueryString = OperationContext[CommonConstants.AdvanceQueryStringKey] as string;
            }

            previousParameter = parameter;
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="mergeAdvanceQueryString">查询时是否需要合并高级查询条件</param>
        /// <returns></returns>
        public virtual BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString)
        {
            IQueryCriteriaGetter getter = BusinessInfoExtractorFactory.GetQueryCriteriaGetter(ParameterFullName);
            return getter.Get(this, Parameter, mergeAdvanceQueryString);
        }
        /// <summary>
        /// 得到查询条件
        /// </summary>
        /// <param name="megeAdvanceQueryString"></param>
        /// <param name="businessOperationParameter"></param>
        /// <returns></returns>
        public virtual BusinessQueryCriteria GetQueryCriteria(bool megeAdvanceQueryString, BusinessOperationParameter businessOperationParameter)
        {
            IQueryCriteriaGetter getter = BusinessInfoExtractorFactory.GetQueryCriteriaGetter(ParameterFullName);
            return getter.GetEntity(this, Parameter, megeAdvanceQueryString, businessOperationParameter);
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="mergeAdvanceQueryString">查询时是否需要合并高级查询条件</param>
        public void QueryData(bool mergeAdvanceQueryString)
        {
            Criteria = GetQueryCriteria(mergeAdvanceQueryString);
            if (Criteria.SearchType != SearchActionType.Auto)
            {
                Criteria.OperationType = RootWorkItem.State["OperationType"] == null ? Criteria.OperationType : (OperationType)(int)RootWorkItem.State["OperationType"];
                if (Criteria.OperationType != null)
                {
                    OperationType = Criteria.OperationType.Value;
                }
            }
            IBusinessQueryServiceGetter getter = BusinessInfoExtractorFactory.GetQueryService(ParameterFullName);
            string tip = LocalData.IsEnglish ? "Querying Data..." : "正在查询数据。。。";
            BeginShowAnimation(tip);//不执行
            if (LocalData.ApplicationType == ApplicationType.ICP)
            {
                ServiceClient.GetClientService<WorkItem>().Commands["Command_DisableTaskCenter"].Execute();
            }
            InnerQueryData(Criteria);
        }
        /// <summary>
        /// 执行查询数据
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        private void InnerQueryData(BusinessQueryCriteria criteria)
        {
            WaitCallback callback = (data) =>
            {
                ClientHelper.SetApplicationContext();
                Temp = data as BindParameter;
                try
                {
                    PanelMoreShow(false);
                    object result;
                    if (Temp == null) return;
                    result = Temp.QueryServiceGetter.Query(Temp.QueryCriteria, Temp.Parameter);
                    DataTable dt = GetInnerTable(result);
                    if (dt != null)
                    {
                        AddCustomColumn(dt);
                    }
                    if (LocalData.ApplicationType == ApplicationType.ICP)
                    {
                        //有高级查找的条件但是集合为空的情况下，查找历史记录信息
                        if (!string.IsNullOrEmpty(Temp.QueryCriteria.AdvanceQueryString) && dt != null && dt.Rows.Count == 0)
                        {
                            EndShowAnimation();
                            result = Requery();
                        }
                        else if (!string.IsNullOrEmpty(Temp.QueryCriteria.AdvanceQueryString))
                        {
                            if (Temp.QueryCriteria.AdvanceQueryString.Contains("$@OPD@=0"))
                            {
                                PanelMoreShow(true);
                            }
                        }
                    }
                    InnerBindData(result, Temp.Parameter);
                }
                catch (Exception ex)
                {
                    EndShowAnimation();
                    Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
                finally
                {
                    if (LocalData.ApplicationType == ApplicationType.ICP)
                    {
                        EndShowAnimation();
                        ServiceClient.GetClientService<WorkItem>().Commands["Command_EnableTaskCenter"].Execute();
                    }
                }

            };
            BindParameter parameter = new BindParameter { Parameter = Parameter, QueryCriteria = criteria, QueryServiceGetter = Getter };
            ThreadPool.QueueUserWorkItem(callback, parameter);
        }
        /// <summary>
        /// 在任务中心输入搜索条件没找到数据时会重新检索
        /// </summary>
        public object Requery()
        {
            if (Temp.QueryCriteria.AdvanceQueryString.Contains("$@OPD@=0") == false) return null;
            PanelMoreShow(false);
            Stopwatch stopwatch = StopwatchHelper.StartStopwatch();
            Requerys = new BusinessQueryCriteria();
            object result = null;
            if (!string.IsNullOrEmpty(Temp.QueryCriteria.AdvanceQueryString))
            {
                Requerys.AdvanceQueryString = Temp.QueryCriteria.AdvanceQueryString.Replace("and $@OPD@=0",
                                                                                     string.Empty);
                #region  加载用户默认所有口岸
                var copany = LocalData.UserInfo.UserOrganizationList.Where(o => o.Type == LocalOrganizationType.Company).ToList();
                if (copany.Count > 1)
                {
                    string[] advanceQueryString = Requerys.AdvanceQueryString.Replace("and", "/").Split('/');
                    Requerys.AdvanceQueryString = string.Empty;

                    for (int i = 0; i < advanceQueryString.Count(); i++)
                    {
                        if (string.IsNullOrEmpty(Requerys.AdvanceQueryString))
                        {
                            Requerys.AdvanceQueryString = advanceQueryString[i];
                        }
                        else if (advanceQueryString[i].Contains("CompanyID"))
                        {
                            string companyId = string.Empty;
                            foreach (var c in copany)
                            {
                                if (string.IsNullOrEmpty(companyId))
                                {
                                    companyId = "'" + c.ID + "'";
                                }
                                else
                                {
                                    companyId = companyId + "," + "'" + c.ID + "'";
                                }
                            }
                            Requerys.AdvanceQueryString += " and $@CompanyID@ in (" + companyId + ")";
                        }
                        else
                        {
                            Requerys.AdvanceQueryString += " and " + advanceQueryString[i];
                        }
                    }
                }
                #endregion
                Requerys.TemplateCode = Temp.QueryCriteria.TemplateCode;

                string tip = LocalData.IsEnglish ? "Querying Data..." : "正在查询数据。。。";
                Guid OperationLogsID = Guid.NewGuid();

                try
                {
                    BeginShowAnimation(tip);

                    MethodBase method = MethodBase.GetCurrentMethod();
                    StopwatchHelper.CustomRecordStopwatch(stopwatch, OperationLogsID, DateTime.Now,
                        method.DeclaringType.FullName, "SEARCH", "任务中心快速查找|通过搜索条件未找到数据,重新检索|");
                    result = Getter.Query(Requerys, Temp.Parameter);
                    if (ServiceClient.GetClientService<WorkItem>().State["Logsquery"] != null)
                    {
                        string logsquery = ServiceClient.GetClientService<WorkItem>().State["Logsquery"].ToString();
                        StopwatchHelper.CustomUpdateStopwatchLog(stopwatch, OperationLogsID, false, "任务中心快速查找|全局|搜索条件为" + logsquery);
                    }
                }
                catch
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(stopwatch, OperationLogsID, false, string.Format("查找出现异常 Session Id[{0}]",LocalData.SessionId));
                    throw;
                }
                finally
                {
                    EndShowAnimation();
                }
            }
            else
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Check no results, please re-enter the query conditions." : "查询无结果,请重新输入查询条件.", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OK);
                ServiceClient.GetClientService<WorkItem>().Commands["Command_EnableTaskCenter"].Execute();
            }
            return result;
        }
        /// <summary>
        /// 执行绑定
        /// </summary>
        /// <param name="result"></param>
        /// <param name="parameter"></param>
        private void InnerBindData(object result, object parameter)
        {
            DataBindDelegate bindDelegate = InnerBindData2;
            Invoke(bindDelegate, result, parameter);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="result"></param>
        /// <param name="parameter"></param>
        private void InnerBindData2(object result, object parameter)
        {
            DataBinder.DataBind(this, result, parameter);//调用ListBaseBusinessPart.cs的SetDataSource(object data)
            EndShowAnimation();//邮件中心不执行
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
            PostHandler.PostHandle(this, result, Parameter);
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
            FieldInfo fieldInfo = result.GetType().GetFields().FirstOrDefault(field => field.FieldType == typeof(DataTable));
            if (fieldInfo != null && string.IsNullOrEmpty(fieldInfo.Name))
            {
                return fieldInfo.GetValue(result) as DataTable;
            }
            return new DataTable();
        }
        /// <summary>
        /// 查询并绑定数据
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void BindData(object parameter)
        {
            Parameter = parameter;
            GetInfo(parameter);//参数解析(现在没用)
            QueryData(!string.IsNullOrEmpty(AdvanceQueryString));//数据查询
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
            baseBindingSource.DataSource = data;
            baseBindingSource.ResetBindings(false);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public virtual void Refresh()
        {

        }
        /// <summary>
        /// 刷新
        /// </summary>
        public virtual void RefreshDetail()
        {

        }
        #endregion

    }

    /// <summary>
    /// 查询结果
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public object Result { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public object Parameter { get; set; }
    }
    /// <summary>
    /// 数据绑定参数类
    /// </summary>
    public class BindParameter
    {
        /// <summary>
        /// 需要绑定数据
        /// </summary>
        public bool NeedBindData { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public object Parameter { get; set; }
        /// <summary>
        /// Message 对象
        /// </summary>
        public Message.ServiceInterface.Message Message { get; set; }
        /// <summary>
        /// NO
        /// </summary>
        public List<string> Nos { get; set; }
        /// <summary>
        /// DataTable
        /// </summary>
        public DataTable Dt { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public BusinessQueryCriteria QueryCriteria { get; set; }
        /// <summary>
        /// 查询服务
        /// </summary>
        public IBusinessQueryServiceGetter QueryServiceGetter { get; set; }
    }

}
