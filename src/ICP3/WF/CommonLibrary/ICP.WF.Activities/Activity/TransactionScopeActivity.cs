﻿using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace ICP.WF.Activities
{
    public partial class LWTransactionScopeActivity : SequenceActivity
	{
		public LWTransactionScopeActivity()
		{
			InitializeComponent();
		}
	}
}
