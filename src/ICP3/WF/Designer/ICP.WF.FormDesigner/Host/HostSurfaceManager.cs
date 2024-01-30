

//-----------------------------------------------------------------------
// <copyright file="HostSurfaceManager.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Windows.Forms;
    using ICP.WF.Controls;


    /// <summary>
    /// 管理 DesignSurface 对象集合。
    /// </summary>
	public class LWFormDesignSurfaceManager : DesignSurfaceManager
    {
        public LWFormDesignSurfaceManager()
        {
            this.ActiveDesignSurfaceChanged+=new ActiveDesignSurfaceChangedEventHandler(LWFormDesignSurfaceManager_ActiveDesignSurfaceChanged);
        }

        void  LWFormDesignSurfaceManager_ActiveDesignSurfaceChanged(object sender, ActiveDesignSurfaceChangedEventArgs e)
        {
 	        this.ActiveDesignSurface=e.NewSurface;
        }

        #region 重载 

        protected override DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
		{
            return new LWFormDesignSurface(parentProvider);
        }


        public override DesignSurface ActiveDesignSurface
        {
            get
            {
                return base.ActiveDesignSurface;
            }
            set
            {
                base.ActiveDesignSurface = value;
            }
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <param name="serviceInstance">服务实例</param>
        public void AddService(
            Type type,
            object serviceInstance)
        {
            this.ServiceContainer.AddService(type, serviceInstance);
        }
        #endregion

        //#region 本地方法

        //private HostControl GetNewHost(Type rootComponentType)
        //{
        //    HostSurface hostSurface = (HostSurface)this.CreateDesignSurface(this.ServiceContainer);
        //    if (rootComponentType == typeof(LWBaseForm))
        //    {
        //        hostSurface.BeginLoad(typeof(LWBaseForm));
        //    }
        //    else if (rootComponentType == typeof(UserControl))
        //    {
        //        hostSurface.BeginLoad(typeof(UserControl));
        //    }
        //    else if (rootComponentType == typeof(Component))
        //    {
        //        hostSurface.BeginLoad(typeof(Component));
        //    }
        //    else
        //    {
        //        throw new Exception("Undefined Host Type: " + rootComponentType.ToString());
        //    }

        //    hostSurface.Initialize();
        //    //this.ActiveDesignSurface = hostSurface;
        //    return new HostControl(hostSurface);
        //}

        //#endregion

        #region 外部接口

        public LWHostControl NewHost(
            Type rootComponentType,
            LoaderType loaderType)
        {
            LWFormDesignSurface hostSurface = (LWFormDesignSurface)this.CreateDesignSurface(this.ServiceContainer);
            IDesignerHost host = (IDesignerHost)hostSurface.GetService(typeof(IDesignerHost));
            switch (loaderType)
            {
                case LoaderType.XmlDesignerLoader:
                    XMLDesignerLoader basicHostLoader = new XMLDesignerLoader(rootComponentType);
                    hostSurface.Init(basicHostLoader);
                  
                    break;

                default:
                    throw new Exception("ICP.WF.FormDesigner is not defined: " + loaderType.ToString());
            }

            //hostSurface.Initialize();

            this.ActiveDesignSurface = hostSurface;

            return new LWHostControl(hostSurface);
        }

        /// <summary>
        /// 从制定表单文件创建界面控件 
        /// </summary>
        public LWHostControl NewHost(string fileName)
        {
            if (fileName == null
                || !File.Exists(fileName))
            {
                throw new Exception(Utility.GetString("WrongNameOrFileNotExisted", "文件名错误或则该路径不存在该文件: ") + fileName);
            }

            if (fileName.EndsWith("xml"))
            {
                LWFormDesignSurface hostSurface = (LWFormDesignSurface)this.CreateDesignSurface(this.ServiceContainer);
                IDesignerHost host = (IDesignerHost)hostSurface.GetService(typeof(IDesignerHost));

                XMLDesignerLoader basicHostLoader = new XMLDesignerLoader(fileName);
                hostSurface.Init(basicHostLoader);
                // hostSurface.Initialize();

                this.ActiveDesignSurface = hostSurface;

                return new LWHostControl(hostSurface);
            }
            else
            {
                throw new Exception(Utility.GetString("NotSupportTheFileFormat", "不支持该文件格式: ") + fileName);
            }
        }

  
       

        #endregion



    }
}
