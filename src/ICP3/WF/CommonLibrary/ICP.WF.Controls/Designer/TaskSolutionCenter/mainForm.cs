#region Copyright (c) 2000-2010 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{       XtraGrid                                                    }
{                                                                   }
{       Copyright (c) 2000-2010 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2010 Developer Express Inc.

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Frames;
using DevExpress.XtraGrid.Frames;
using DevExpress.XtraEditors.Design.TasksSolution;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class TasksSolutionFormGrid : TasksSolutionFormBase {
		string[] resources =  new string[] {"Events.xml", "Steps.xml", "Tasks.xml" };
		public TasksSolutionFormGrid() {
		}
		protected override void AddStepFrames() {
			base.AddStepFrames();
			this.AddStepFrame("Columns", typeof(ICP.WF.Controls.Design.ColumnDesigner));
			//this.AddStepFrame("Bands", typeof(BandDesigner));
		}
		protected override string[] GetXmlFiles() {
			string[] xmlResources = new string[resources.Length];
			for(int i = 0; i < resources.Length; i ++)
				xmlResources[i] = ResourcePath + resources[i];
			return xmlResources;
		}
		protected override string ShortProductName { get { return "XtraGrid"; } }
		public string ResourcePath { get { return "DevExpress.XtraGrid.Design.TaskSolutionCenter."; } }
	}
}
