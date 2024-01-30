

namespace ICP.WF.ServiceInterface.Client
{
    using ICP.Framework.CommonLibrary.Helper;

    /// <summary>
    /// 默认配置文件夹管理器
    /// </summary>
    public class DefaultConfigMananger
    {
        string toolboxConfigFile = "ToolItemsConfig.xml";
        string rootPath;
        DocumentNode root;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultConfigMananger()
        {
            rootPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (rootPath != null && rootPath.Length > 0)
            {
                rootPath = rootPath.Substring(0, rootPath.Length-1);
            }
            Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="root">根节点路径</param>
        public DefaultConfigMananger(string root)
        {
            rootPath = root;
            Init();
        }


        static DefaultConfigMananger _default;
        /// <summary>
        /// 默认配置
        /// </summary>
        public static DefaultConfigMananger Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new DefaultConfigMananger();
                }

                return _default;
            }
        }



        /// <summary>
        /// 初始化配置目录
        /// </summary>
        void Init()
        {
            if (root != null)
            {
                return;
            }

            //当前应用程序节点
            root = new DocumentNode
            {
                Name = rootPath,
            };

            //流程相关主文件夹
            DocumentNode wfFolder = new DocumentNode
            {
                Name = "WF",
                Parent = root
            };

            //流程设计父文件夹
            DocumentNode flowFolder = new DocumentNode
            {
                Name = "Flow",
                Parent = wfFolder
            };
            //流程摸版文件缓存文件夹
            DocumentNode flowTemplateFolder = new DocumentNode
            {
                Name = "Template",
                Parent = flowFolder
            };
            //流程设计文件夹
            DocumentNode flowDesignFolder = new DocumentNode
            {
                Name = "Design",
                Parent = flowFolder
            };
            //流程设计器打印文件夹
            DocumentNode flowPrintFolder = new DocumentNode
            {
                Name = "Print",
                Parent = flowFolder
            };


            //表单设计父文件夹
            DocumentNode formFolder = new DocumentNode
            {
                Name = "Form",
                Parent = wfFolder
            };
            //表单摸版文件缓存文件夹
            DocumentNode formTemplateFolder = new DocumentNode
            {
                Name = "Template",
                Parent = formFolder
            };
            // 表单设计文件缓存的文件夹
            DocumentNode formDesignFolder = new DocumentNode
            {
                Name = "Design",
                Parent = formFolder
            };

            //表单打印缓存的文件夹
            DocumentNode printFolder = new DocumentNode
            {
                Name = "Print",
                Parent = formFolder
            };

            root.Create();
        }


        #region 相关保存和取配置接口方法

        /// <summary>
        /// 保存工具项配置到指定路径
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="fileName">文件名</param>
        public void SaveToolboxConfig(
            ToolboxConfig config,
            string fileName)
        {
            string tempPath = WFFolder.Path + "\\" + fileName;
            SerializerHelper.SerializeToXMLDocument<ToolboxConfig>(config, tempPath);
        }

        /// <summary>
        /// 获取工具栏配置信息 
        /// </summary>
        /// <returns></returns>
        public ToolboxConfig GetToolboxConfig()
        {
            string tempPath = WFFolder.Path + "\\" + toolboxConfigFile;
            if (System.IO.File.Exists(tempPath))
            {
                ToolboxConfig config = SerializerHelper.DeserializeFromXMLDocument<ToolboxConfig>(tempPath);
                return config;
            }
            else
            {
                return null;
            }
        }

        #endregion


        #region 相关文件夹信息

        /// <summary>
        /// 表单设计文件缓存文件夹
        /// </summary>
        public DocumentNode WFFolder
        {
            get
            {
                return root.Childs["WF"];
            }
        }

        /// <summary>
        /// 流程设计文件缓存文件夹
        /// </summary>
        public DocumentNode FolwDesignFolder
        {
            get
            {
                return root.Childs["WF"].Childs["Flow"].Childs["Design"];
            }
        }

        /// <summary>
        /// 流程摸版文件缓存文件夹
        /// </summary>
        public DocumentNode FlowTemplateFolder
        {
            get
            {
                return root.Childs["WF"].Childs["Flow"].Childs["Template"];
            }
        }
        /// <summary>
        /// 流程打印文件夹
        /// </summary>
        public DocumentNode FlowPrintFolder
        {
            get
            {
                return root.Childs["WF"].Childs["Flow"].Childs["Print"];
            }
        }


        /// <summary>
        /// 表单摸版文件缓存文件夹
        /// </summary>
        public DocumentNode FormTemplateFolder
        {
            get
            {
                return root.Childs["WF"].Childs["Form"].Childs["Template"];
            }
        }

        /// <summary>
        /// 表单设计文件缓存的文件夹
        /// </summary>
        public DocumentNode FormDesignFolder
        {
            get
            {
                return root.Childs["WF"].Childs["Form"].Childs["Design"];
            }
        }

        /// <summary>
        /// 表单打印缓存的文件夹
        /// </summary>
        public DocumentNode FormPrintFolder
        {
            get
            {
                return root.Childs["WF"].Childs["Form"].Childs["Print"];
            }
        }

        #endregion
    }
}
