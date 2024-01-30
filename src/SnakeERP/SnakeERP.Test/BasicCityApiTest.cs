using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicCityVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicCityApiTest
    {
        private BasicCityController _controller;
        private string _seed;

        public BasicCityApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicCityController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicCitySearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicCityVM vm = _controller.Wtm.CreateVM<BasicCityVM>();
            BasicCity v = new BasicCity();
            
            v.Code = "fqTt3lyqqMD5rXXHG2";
            v.Name = "KZImNKaTulItQJIlY34RK4qdNdwJ8Hhq9uYTgtXWzkj";
            v.ProvinceID = AddBasicProvince();
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicCity>().Find(v.ID);
                
                Assert.AreEqual(data.Code, "fqTt3lyqqMD5rXXHG2");
                Assert.AreEqual(data.Name, "KZImNKaTulItQJIlY34RK4qdNdwJ8Hhq9uYTgtXWzkj");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicCity v = new BasicCity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "fqTt3lyqqMD5rXXHG2";
                v.Name = "KZImNKaTulItQJIlY34RK4qdNdwJ8Hhq9uYTgtXWzkj";
                v.ProvinceID = AddBasicProvince();
                context.Set<BasicCity>().Add(v);
                context.SaveChanges();
            }

            BasicCityVM vm = _controller.Wtm.CreateVM<BasicCityVM>();
            var oldID = v.ID;
            v = new BasicCity();
            v.ID = oldID;
       		
            v.Code = "1igTefLqbaH3MBH";
            v.Name = "U3VRH9jNPeDr8pUOjFW8iKyguuUwq";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.ProvinceID", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicCity>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "1igTefLqbaH3MBH");
                Assert.AreEqual(data.Name, "U3VRH9jNPeDr8pUOjFW8iKyguuUwq");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicCity v = new BasicCity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "fqTt3lyqqMD5rXXHG2";
                v.Name = "KZImNKaTulItQJIlY34RK4qdNdwJ8Hhq9uYTgtXWzkj";
                v.ProvinceID = AddBasicProvince();
                context.Set<BasicCity>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicCity v1 = new BasicCity();
            BasicCity v2 = new BasicCity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "fqTt3lyqqMD5rXXHG2";
                v1.Name = "KZImNKaTulItQJIlY34RK4qdNdwJ8Hhq9uYTgtXWzkj";
                v1.ProvinceID = AddBasicProvince();
                v2.Code = "1igTefLqbaH3MBH";
                v2.Name = "U3VRH9jNPeDr8pUOjFW8iKyguuUwq";
                v2.ProvinceID = v1.ProvinceID; 
                context.Set<BasicCity>().Add(v1);
                context.Set<BasicCity>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicCity>().Find(v1.ID);
                var data2 = context.Set<BasicCity>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddBasicProvince()
        {
            BasicProvince v = new BasicProvince();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "8uJHkS";
                v.Name = "m4QxBRw69lnddpA08dE7mk676DIsm9wh";
                context.Set<BasicProvince>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
