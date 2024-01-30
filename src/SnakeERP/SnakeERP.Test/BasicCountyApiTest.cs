using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicCountyVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicCountyApiTest
    {
        private BasicCountyController _controller;
        private string _seed;

        public BasicCountyApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicCountyController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicCountySearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicCountyVM vm = _controller.Wtm.CreateVM<BasicCountyVM>();
            BasicCounty v = new BasicCounty();
            
            v.Code = "l2p";
            v.Name = "z";
            v.CityID = AddBasicCity();
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicCounty>().Find(v.ID);
                
                Assert.AreEqual(data.Code, "l2p");
                Assert.AreEqual(data.Name, "z");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicCounty v = new BasicCounty();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "l2p";
                v.Name = "z";
                v.CityID = AddBasicCity();
                context.Set<BasicCounty>().Add(v);
                context.SaveChanges();
            }

            BasicCountyVM vm = _controller.Wtm.CreateVM<BasicCountyVM>();
            var oldID = v.ID;
            v = new BasicCounty();
            v.ID = oldID;
       		
            v.Code = "FKQ7FoYCZZad5kLsex";
            v.Name = "X4Rm9tFZTK1sHnTsq38j";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.CityID", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicCounty>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "FKQ7FoYCZZad5kLsex");
                Assert.AreEqual(data.Name, "X4Rm9tFZTK1sHnTsq38j");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicCounty v = new BasicCounty();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "l2p";
                v.Name = "z";
                v.CityID = AddBasicCity();
                context.Set<BasicCounty>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicCounty v1 = new BasicCounty();
            BasicCounty v2 = new BasicCounty();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "l2p";
                v1.Name = "z";
                v1.CityID = AddBasicCity();
                v2.Code = "FKQ7FoYCZZad5kLsex";
                v2.Name = "X4Rm9tFZTK1sHnTsq38j";
                v2.CityID = v1.CityID; 
                context.Set<BasicCounty>().Add(v1);
                context.Set<BasicCounty>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicCounty>().Find(v1.ID);
                var data2 = context.Set<BasicCounty>().Find(v2.ID);
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

                v.Code = "ZtzSZESp4cYQmD6";
                v.Name = "OZfHRwXIyToOmNxCUJxZpE";
                context.Set<BasicProvince>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBasicCity()
        {
            BasicCity v = new BasicCity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "2M";
                v.Name = "KotfnWaYAMBBX6s1YyUtyf";
                v.ProvinceID = AddBasicProvince();
                context.Set<BasicCity>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
