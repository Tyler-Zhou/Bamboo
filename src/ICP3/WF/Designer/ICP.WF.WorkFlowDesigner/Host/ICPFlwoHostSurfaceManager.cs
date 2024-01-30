

//-----------------------------------------------------------------------
// <copyright file="HostSurfaceManager.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
{
    using System;
    using System.ComponentModel.Design;

    /// <summary>
    /// 管理 DesignSurface 对象集合。
    /// </summary>
	public class ICPFlwoHostSurfaceManager : DesignSurfaceManager
    {
        public ICPFlwoHostSurfaceManager()
        {
            this.ActiveDesignSurfaceChanged+=LWFormDesignSurfaceManager_ActiveDesignSurfaceChanged;
            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ActiveDesignSurfaceChanged -= LWFormDesignSurfaceManager_ActiveDesignSurfaceChanged;
            }
            base.Dispose(disposing);
        }

        void  LWFormDesignSurfaceManager_ActiveDesignSurfaceChanged(object sender, ActiveDesignSurfaceChangedEventArgs e)
        {
 	        this.ActiveDesignSurface=e.NewSurface;
        }

        #region 重载 

        protected override DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
		{
			return new ICPFlowDesignSurface(parentProvider);
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

       
        #region 外部接口

        

  
       

        #endregion


    }
}
