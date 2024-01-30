//-----------------------------------------------------------------------
// <copyright file="LWSqlWorkflowPersistenceService.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceComponent
{
    using System;
    using System.Collections.Specialized;
    using System.Data.Linq;
    using System.Workflow.Runtime.Hosting;

    /// <summary>
    /// 工作流持久化服务
    /// </summary>
    public class LWSqlWorkflowPersistenceService : SqlWorkflowPersistenceService
    {

        public LWSqlWorkflowPersistenceService(NameValueCollection parameters)
            : base(parameters)
        {
        }


        public LWSqlWorkflowPersistenceService(string connectionString)
            : base(connectionString)
        {
        }

        public LWSqlWorkflowPersistenceService(string connectionString, bool unloadOnIdle, TimeSpan instanceOwnershipDuration, TimeSpan loadingInterval)
            : base(connectionString, unloadOnIdle, instanceOwnershipDuration, loadingInterval)
        {
        }


        bool isSave = false;

        public void PreSave()
        {
            isSave = true;
        }

        public void CancelSave()
        {
            isSave = false;
        }

        protected override void SaveWorkflowInstanceState(System.Workflow.ComponentModel.Activity rootActivity, bool unlock)
        {
            //如果没掉用PreSave则不让保存数据
            if (isSave == false) return;

            base.SaveWorkflowInstanceState(rootActivity, unlock);
        }
    }
}
