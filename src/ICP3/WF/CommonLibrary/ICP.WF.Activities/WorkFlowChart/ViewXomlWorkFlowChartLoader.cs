using System;
using System.ComponentModel.Design;
using System.IO;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 查看指定文件内容的流程图
    /// </summary>
    internal class ViewXomlWorkFlowChartLoader : WorkflowChartLoader
    {
        #region 本地变量

        private string _xomlContent = string.Empty;
        /// <summary>
        /// 流程XOML
        /// </summary>
        public string XomlContent
        {
            get { return _xomlContent; }
            set { _xomlContent = value; }
        }

        #endregion

        #region 构造函数

        internal ViewXomlWorkFlowChartLoader()
        {
        }

        internal ViewXomlWorkFlowChartLoader(string xomlContent)
        {
            _xomlContent = xomlContent;
        }

        #endregion

        #region 重载方法

        public override void InitWorkFlowView()
        {
            if (string.IsNullOrEmpty(_xomlContent) == false)
            {
                LoadActivitys(_xomlContent);
            }
        }

        #endregion


        #region 外部接口

        //public void Flush(string xomlContent)
        //{
        //    _xomlContent = xomlContent;
        //    base.Flush();
        //}

        #endregion


        #region 本地方法
        /*根据文件内容加载一个流程图*/
        private void LoadActivitys(string fileContent)
        {
            Activity rootActivity = null;
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
          

            // 加载活动树
            using (StringReader sr = new StringReader(fileContent))
            {
                using (XmlReader reader = new XmlTextReader(sr))
                {
                    WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                    rootActivity = xomlSerializer.Deserialize(reader) as Activity;
                }
            }
            if (rootActivity != null && designerHost != null)
            {
                AddObjectGraphToDesignerHost(designerHost, rootActivity);
                Type companionType = rootActivity.GetValue(WorkflowMarkupSerializer.XClassProperty) as Type;
                if (companionType != null)
                {
                    SetBaseComponentClassName(companionType.FullName);
                }
            }
        }

        #endregion
    }
}
