

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
    /// ���� DesignSurface ���󼯺ϡ�
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

        #region ���� 

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
        /// ��ӷ���
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="serviceInstance">����ʵ��</param>
        public void AddService(
            Type type,
            object serviceInstance)
        {
            this.ServiceContainer.AddService(type, serviceInstance);
        }
        #endregion

       
        #region �ⲿ�ӿ�

        

  
       

        #endregion


    }
}
