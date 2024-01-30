using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicVehicleBrandVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicVehicleBrandApiTest
    {
        private BasicVehicleBrandController _controller;
        private string _seed;

        public BasicVehicleBrandApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicVehicleBrandController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicVehicleBrandSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicVehicleBrandVM vm = _controller.Wtm.CreateVM<BasicVehicleBrandVM>();
            BasicVehicleBrand v = new BasicVehicleBrand();
            
            v.Name = "xC3eIB";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicVehicleBrand>().Find(v.ID);
                
                Assert.AreEqual(data.Name, "xC3eIB");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicVehicleBrand v = new BasicVehicleBrand();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "xC3eIB";
                context.Set<BasicVehicleBrand>().Add(v);
                context.SaveChanges();
            }

            BasicVehicleBrandVM vm = _controller.Wtm.CreateVM<BasicVehicleBrandVM>();
            var oldID = v.ID;
            v = new BasicVehicleBrand();
            v.ID = oldID;
       		
            v.Name = "UbJ87dhm6ebfJ0mLStHCsbFbE0P26mCKhzsLGFvR";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicVehicleBrand>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "UbJ87dhm6ebfJ0mLStHCsbFbE0P26mCKhzsLGFvR");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicVehicleBrand v = new BasicVehicleBrand();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "xC3eIB";
                context.Set<BasicVehicleBrand>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicVehicleBrand v1 = new BasicVehicleBrand();
            BasicVehicleBrand v2 = new BasicVehicleBrand();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "xC3eIB";
                v2.Name = "UbJ87dhm6ebfJ0mLStHCsbFbE0P26mCKhzsLGFvR";
                context.Set<BasicVehicleBrand>().Add(v1);
                context.Set<BasicVehicleBrand>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicVehicleBrand>().Find(v1.ID);
                var data2 = context.Set<BasicVehicleBrand>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
