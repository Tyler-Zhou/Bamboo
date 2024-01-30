using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.TaskCenter.ServiceInterface;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 操作视图基类工作区SmartPart所有视图都该继承于此处并且名称规则
    /// 为视图操作类型缩写加上"TaskItemPart" 如海出子类为OETaskItemPart，
    /// 并且实现虚拟Init方法
    /// </summary>
    [SmartPart]
    public partial class BaseTaskItemPart : XtraUserControl
    {
        /// <summary>
        /// 工作项目WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        public BaseTaskItemPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (RootWorkItem != null)
                {
                    RootWorkItem.SmartParts.Remove(this);
                    RootWorkItem = null;

                }
            };
        }
        /// <summary>
        /// 初始化工作,初始化化工具栏，订单数据表，数据表右键菜单。订单数据详情信息
        /// </summary>
        /// <param name="nodeInfo">操作视图对象</param>
        public virtual void Init(NodeInfo nodeInfo)
        {
            
        }
        public virtual void SaveCustomColumnInfo()
        {

        }
    }
}
