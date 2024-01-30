using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicProvinceVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicProvinceApiTest
    {
        private BasicProvinceController _controller;
        private string _seed;

        public BasicProvinceApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicProvinceController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicProvinceSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicProvinceVM vm = _controller.Wtm.CreateVM<BasicProvinceVM>();
            BasicProvince v = new BasicProvince();
            
            v.Code = "5cL6PQOQGfr2WPIs3r";
            v.Name = "jbKIzLtJCBLecBsRCCTHGVZD9pqXz";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicProvince>().Find(v.ID);
                
                Assert.AreEqual(data.Code, "5cL6PQOQGfr2WPIs3r");
                Assert.AreEqual(data.Name, "jbKIzLtJCBLecBsRCCTHGVZD9pqXz");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicProvince v = new BasicProvince();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "5cL6PQOQGfr2WPIs3r";
                v.Name = "jbKIzLtJCBLecBsRCCTHGVZD9pqXz";
                context.Set<BasicProvince>().Add(v);
                context.SaveChanges();
            }

            BasicProvinceVM vm = _controller.Wtm.CreateVM<BasicProvinceVM>();
            var oldID = v.ID;
            v = new BasicProvince();
            v.ID = oldID;
       		
            v.Code = "mrbrRBqUyNmLb5vrGof";
            v.Name = "C5KZx4vGALT6oLZbz87oHgjFOWpEL62XWl6";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Name", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicProvince>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "mrbrRBqUyNmLb5vrGof");
                Assert.AreEqual(data.Name, "C5KZx4vGALT6oLZbz87oHgjFOWpEL62XWl6");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicProvince v = new BasicProvince();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "5cL6PQOQGfr2WPIs3r";
                v.Name = "jbKIzLtJCBLecBsRCCTHGVZD9pqXz";
                context.Set<BasicProvince>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicProvince v1 = new BasicProvince();
            BasicProvince v2 = new BasicProvince();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "5cL6PQOQGfr2WPIs3r";
                v1.Name = "jbKIzLtJCBLecBsRCCTHGVZD9pqXz";
                v2.Code = "mrbrRBqUyNmLb5vrGof";
                v2.Name = "C5KZx4vGALT6oLZbz87oHgjFOWpEL62XWl6";
                context.Set<BasicProvince>().Add(v1);
                context.Set<BasicProvince>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicProvince>().Find(v1.ID);
                var data2 = context.Set<BasicProvince>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
