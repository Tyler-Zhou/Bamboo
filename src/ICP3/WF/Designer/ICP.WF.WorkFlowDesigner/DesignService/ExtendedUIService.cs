using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.WorkFlowDesigner
{
	internal class ExtendedUIService : IExtendedUIService	
	{
		#region IExtendedUIService 成员

		public void AddAssemblyReference( AssemblyName assemblyName )
		{
			
		}

		public void AddDesignerActions( DesignerAction[] actions )
		{
		}

		public System.Windows.Forms.DialogResult AddWebReference( out Uri url , out Type proxyClass )
		{
			url = null;
			proxyClass = null;
			return System.Windows.Forms.DialogResult.OK;
		}

		public Type GetProxyClassForUrl( Uri url )
		{
			return base.GetType();
		}

		public ITypeDescriptorContext GetSelectedPropertyContext()
		{
			return null;
		}

		public Uri GetUrlForProxyClass( Type proxyClass )
		{
			return null;
		}

		public Dictionary<string , Type> GetXsdProjectItemsInfo()
		{
			return null;
		}

		public bool NavigateToProperty( string propName )
		{
			return true;
		}

		public void RemoveDesignerActions()
		{
		}

		public void ShowToolsOptions()
		{
		}

		#endregion
	}
}
