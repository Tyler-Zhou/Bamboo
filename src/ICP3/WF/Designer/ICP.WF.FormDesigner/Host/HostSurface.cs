
//-----------------------------------------------------------------------
// <copyright file="HostSurface.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner 
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Windows.Forms;
    using ICP.WF.Controls;


    /// <summary>
    /// �ṩ�������������û����档
    /// </summary>
    public class LWFormDesignSurface : DesignSurfaceExt
    {
        
        #region  ���캯��

        public LWFormDesignSurface() 
            : base()
		{
          
		}
		public LWFormDesignSurface(IServiceProvider parentProvider) 
            : base(parentProvider)
		{
           
        }


  
        #endregion

        XMLDesignerLoader loader;
        public void Init(XMLDesignerLoader designerLoader)
        {
            loader = designerLoader;
            this.BeginLoad(designerLoader);
        }

        public XMLDesignerLoader DesignerLoader
        {
            get
            {
                return loader;
            }
        }


        #region ��������

     

        #endregion
    }


    public class SelectedEventArgs : EventArgs
    {
        public object Object { get; set; }

        public SelectedEventArgs(object arg)
        {
            this.Object = arg;
        }
    }

    public delegate void SelectedChangedEventHandler(object sender, SelectedEventArgs e);


    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public ExceptionEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }

    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);

}
