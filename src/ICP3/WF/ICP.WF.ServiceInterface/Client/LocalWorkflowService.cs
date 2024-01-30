using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ICP.WF.ServiceInterface.DataObject;
using System.Xml;
using ICP.WF.ServiceInterface.Client;

namespace ICP.WF.ServiceInterface
{
    /// <summary>
    /// 本地服务
    /// </summary>
    public class LocalWorkflowService
    {
        #region 变量定义

        private string _formTemplateDir;
        /// <summary>
        /// 表单模模版路径

        /// </summary>
        public string FormTemplateDir
        {
            get { return _formTemplateDir; }
        }

        private string _workflowTemplateDir;
        /// <summary>
        /// 保存流程模版路径
        /// </summary>
        public string WorkflowTemplateDir
        {
            get { return _workflowTemplateDir; }
        }

        private string _workFlowDir;
        /// <summary>
        /// 流程设计目录
        /// </summary>
        public string WorkFlowDir
        {
            get { return _workFlowDir; }
        }


        private string _formDir;
        /// <summary>
        /// 表单设计目录
        /// </summary>
        public string FormDir
        {
            get { return _formDir; }
        }

        #endregion

        #region 构造函数


        public LocalWorkflowService()
        {
            InitDirectory();
        }

        #endregion

        #region 初始化变量


        private void InitDirectory()
        {

            _formTemplateDir = DefaultConfigMananger.Default.FormTemplateFolder.Path;// System.Windows.Forms.Application.StartupPath + @"\FormTemplates";
            if (Directory.Exists(_formTemplateDir) == false)
            {
                Directory.CreateDirectory(_formTemplateDir);
            }

            _workflowTemplateDir = DefaultConfigMananger.Default.FlowTemplateFolder.Path;// System.Windows.Forms.Application.StartupPath + @"\WorkFlowTemplates";
            if (Directory.Exists(_workflowTemplateDir) == false)
            {
                Directory.CreateDirectory(_workflowTemplateDir);
            }


            _workFlowDir =DefaultConfigMananger.Default.FolwDesignFolder.Path; //System.Windows.Forms.Application.StartupPath + @"\WorkFlows";
            if (Directory.Exists(_workFlowDir) == false)
            {
                Directory.CreateDirectory(_workFlowDir);
            }


            _formDir = DefaultConfigMananger.Default.FormDesignFolder.Path; //System.Windows.Forms.Application.StartupPath + @"\Forms";
            if (Directory.Exists(_formDir) == false)
            {
                Directory.CreateDirectory(_formDir);
            }
        }

        #endregion


        #region 外部方法

        /// <summary>
        /// 获取表单模板列表
        /// </summary>
        /// <returns></returns>
        public List<FormTemplateItem> GetFormTemplateList()
        {
            DirectoryInfo dataSourceDir = new DirectoryInfo(_formTemplateDir);
            List<FormTemplateItem> items = new List<FormTemplateItem>();
            foreach (FileInfo f in dataSourceDir.GetFiles("*.xml", SearchOption.TopDirectoryOnly))
            {
                FormTemplateItem item = new FormTemplateItem();
                item.Name = f.Name;
                item.TemplateFile = f.FullName;
                items.Add(item);
            }

            return items;
        }


        /// <summary>
        /// 获取工作流模板列表
        /// </summary>
        /// <returns></returns>
        public List<WorkFlowTemplateItem> GetWorkFlowTemplateList()
        {
            DirectoryInfo dataSourceDir = new DirectoryInfo(_workflowTemplateDir);
            List<WorkFlowTemplateItem> items = new List<WorkFlowTemplateItem>();
            foreach (FileInfo f in dataSourceDir.GetFiles("*.xoml", SearchOption.TopDirectoryOnly))
            {
                WorkFlowTemplateItem item = new WorkFlowTemplateItem();
                item.Id = Guid.NewGuid();
                item.Name = f.Name;
                item.TemplateFile = f.OpenText().ReadToEnd();
                item.Version = "1";
                items.Add(item);
            }

            return items;
        }

        #endregion
    }
}
