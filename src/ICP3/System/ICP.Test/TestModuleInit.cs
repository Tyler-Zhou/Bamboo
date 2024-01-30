using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Test
{
    public class TestModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;

        public TestModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
        }
        public override void AddServices()
        {
            base.AddServices();

            _rootWorkItem.Services.AddNew<TestList, IAutoTestServiceInterface>();
        }

        #region Command

        #region 测试
        
        [CommandHandler("SYSTEM_TEST")]
        public void Open_SYSTEM_Test(object sender, EventArgs e)
        {
            using (new CursorHelper())
            {
                TestWorkItem roleWorkitem = _rootWorkItem.WorkItems.AddNew<TestWorkItem>();
                roleWorkitem.Run();
            }
        }
        #endregion

        #endregion

    }
}
