using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace ICP.WF.Activities
{
	public partial class TransactionApplicationActivity
	{
		#region Designer generated code
		
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
		private void InitializeComponent()
		{
            this.CanModifyActivities = true;
            this.transactionScopeActivity1 = new System.Workflow.ComponentModel.TransactionScopeActivity();
            // 
            // transactionScopeActivity1
            // 
            this.transactionScopeActivity1.Name = "transactionScopeActivity1";
            this.transactionScopeActivity1.TransactionOptions.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
            // 
            // TransactionApplicationActivity
            // 
            this.Activities.Add(this.transactionScopeActivity1);
            this.Name = "TransactionApplicationActivity";
            this.CanModifyActivities = false;

		}

		#endregion

        private TransactionScopeActivity transactionScopeActivity1;


    }
}
