using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicVehicleInfoVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicVehicleInfoApiTest
    {
        private BasicVehicleInfoController _controller;
        private string _seed;

        public BasicVehicleInfoApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicVehicleInfoController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicVehicleInfoSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicVehicleInfoVM vm = _controller.Wtm.CreateVM<BasicVehicleInfoVM>();
            BasicVehicleInfo v = new BasicVehicleInfo();
            
            v.Number = "sbPA90lw0lfhJjWFQUdnj";
            v.VehicleTypeID = AddBasicVehicleType();
            v.VehicleBrandID = AddBasicVehicleBrand();
            v.NewEnergy = true;
            v.Status = SnakeERP.Model.EnumVehicleStatus.Maintenance;
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicVehicleInfo>().Find(v.ID);
                
                Assert.AreEqual(data.Number, "sbPA90lw0lfhJjWFQUdnj");
                Assert.AreEqual(data.NewEnergy, true);
                Assert.AreEqual(data.Status, SnakeERP.Model.EnumVehicleStatus.Maintenance);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicVehicleInfo v = new BasicVehicleInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Number = "sbPA90lw0lfhJjWFQUdnj";
                v.VehicleTypeID = AddBasicVehicleType();
                v.VehicleBrandID = AddBasicVehicleBrand();
                v.NewEnergy = true;
                v.Status = SnakeERP.Model.EnumVehicleStatus.Maintenance;
                context.Set<BasicVehicleInfo>().Add(v);
                context.SaveChanges();
            }

            BasicVehicleInfoVM vm = _controller.Wtm.CreateVM<BasicVehicleInfoVM>();
            var oldID = v.ID;
            v = new BasicVehicleInfo();
            v.ID = oldID;
       		
            v.Number = "mlkqtW7enrqcaihhvEIgzrrPzJGx";
            v.NewEnergy = true;
            v.Status = SnakeERP.Model.EnumVehicleStatus.Other;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Number", "");
            vm.FC.Add("Entity.VehicleTypeID", "");
            vm.FC.Add("Entity.VehicleBrandID", "");
            vm.FC.Add("Entity.NewEnergy", "");
            vm.FC.Add("Entity.Status", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicVehicleInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.Number, "mlkqtW7enrqcaihhvEIgzrrPzJGx");
                Assert.AreEqual(data.NewEnergy, true);
                Assert.AreEqual(data.Status, SnakeERP.Model.EnumVehicleStatus.Other);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicVehicleInfo v = new BasicVehicleInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Number = "sbPA90lw0lfhJjWFQUdnj";
                v.VehicleTypeID = AddBasicVehicleType();
                v.VehicleBrandID = AddBasicVehicleBrand();
                v.NewEnergy = true;
                v.Status = SnakeERP.Model.EnumVehicleStatus.Maintenance;
                context.Set<BasicVehicleInfo>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicVehicleInfo v1 = new BasicVehicleInfo();
            BasicVehicleInfo v2 = new BasicVehicleInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Number = "sbPA90lw0lfhJjWFQUdnj";
                v1.VehicleTypeID = AddBasicVehicleType();
                v1.VehicleBrandID = AddBasicVehicleBrand();
                v1.NewEnergy = true;
                v1.Status = SnakeERP.Model.EnumVehicleStatus.Maintenance;
                v2.Number = "mlkqtW7enrqcaihhvEIgzrrPzJGx";
                v2.VehicleTypeID = v1.VehicleTypeID; 
                v2.VehicleBrandID = v1.VehicleBrandID; 
                v2.NewEnergy = true;
                v2.Status = SnakeERP.Model.EnumVehicleStatus.Other;
                context.Set<BasicVehicleInfo>().Add(v1);
                context.Set<BasicVehicleInfo>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicVehicleInfo>().Find(v1.ID);
                var data2 = context.Set<BasicVehicleInfo>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddBasicVehicleType()
        {
            BasicVehicleType v = new BasicVehicleType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "CBYhSxuJETzMjZgMvbQCSbs60vLipD9R";
                context.Set<BasicVehicleType>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBasicVehicleBrand()
        {
            BasicVehicleBrand v = new BasicVehicleBrand();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "p8Li3yCge2mnzqFpggZmvGHDLNLZA7xc5vrPmPJZxI";
                context.Set<BasicVehicleBrand>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
