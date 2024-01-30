using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.DG.DGOrderInfoVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class DGOrderInfoApiTest
    {
        private DGOrderInfoController _controller;
        private string _seed;

        public DGOrderInfoApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<DGOrderInfoController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new DGOrderInfoSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            DGOrderInfoVM vm = _controller.Wtm.CreateVM<DGOrderInfoVM>();
            DGOrderInfo v = new DGOrderInfo();
            
            v.Name = "UPVGXKq";
            v.ShipDateTime = DateTime.Parse("2024-11-03 11:26:44");
            v.DeliveryCompany = "cb4B3";
            v.DeliveryCompanyPhone = "YQQfMMV2KEB4gbOAsdzMisa4";
            v.DeliveryMan = "12vldEgSSjhRN6P7";
            v.LicensePlate = "FZqOqJgKqqNDXLlRnYTnrMdo";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DGOrderInfo>().Find(v.ID);
                
                Assert.AreEqual(data.Name, "UPVGXKq");
                Assert.AreEqual(data.ShipDateTime, DateTime.Parse("2024-11-03 11:26:44"));
                Assert.AreEqual(data.DeliveryCompany, "cb4B3");
                Assert.AreEqual(data.DeliveryCompanyPhone, "YQQfMMV2KEB4gbOAsdzMisa4");
                Assert.AreEqual(data.DeliveryMan, "12vldEgSSjhRN6P7");
                Assert.AreEqual(data.LicensePlate, "FZqOqJgKqqNDXLlRnYTnrMdo");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            DGOrderInfo v = new DGOrderInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "UPVGXKq";
                v.ShipDateTime = DateTime.Parse("2024-11-03 11:26:44");
                v.DeliveryCompany = "cb4B3";
                v.DeliveryCompanyPhone = "YQQfMMV2KEB4gbOAsdzMisa4";
                v.DeliveryMan = "12vldEgSSjhRN6P7";
                v.LicensePlate = "FZqOqJgKqqNDXLlRnYTnrMdo";
                context.Set<DGOrderInfo>().Add(v);
                context.SaveChanges();
            }

            DGOrderInfoVM vm = _controller.Wtm.CreateVM<DGOrderInfoVM>();
            var oldID = v.ID;
            v = new DGOrderInfo();
            v.ID = oldID;
       		
            v.Name = "XrFeGuV5Y1EQqfksQvq5zdPHPXiZc41D1ivNB2Us2ab";
            v.ShipDateTime = DateTime.Parse("2025-05-08 11:26:44");
            v.DeliveryCompany = "EE";
            v.DeliveryCompanyPhone = "Z2GcjbMr6542qMPIONeOqy1IWfQ2LJ9LuwO";
            v.DeliveryMan = "L94VbvV4ypybCOQUV9sV8qq042aIZyQU2fHOojNd5p";
            v.LicensePlate = "uMvKAZYYKcaTHfQMmoWFNNzh2D4Wa";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.ShipDateTime", "");
            vm.FC.Add("Entity.DeliveryCompany", "");
            vm.FC.Add("Entity.DeliveryCompanyPhone", "");
            vm.FC.Add("Entity.DeliveryMan", "");
            vm.FC.Add("Entity.LicensePlate", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DGOrderInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "XrFeGuV5Y1EQqfksQvq5zdPHPXiZc41D1ivNB2Us2ab");
                Assert.AreEqual(data.ShipDateTime, DateTime.Parse("2025-05-08 11:26:44"));
                Assert.AreEqual(data.DeliveryCompany, "EE");
                Assert.AreEqual(data.DeliveryCompanyPhone, "Z2GcjbMr6542qMPIONeOqy1IWfQ2LJ9LuwO");
                Assert.AreEqual(data.DeliveryMan, "L94VbvV4ypybCOQUV9sV8qq042aIZyQU2fHOojNd5p");
                Assert.AreEqual(data.LicensePlate, "uMvKAZYYKcaTHfQMmoWFNNzh2D4Wa");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            DGOrderInfo v = new DGOrderInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "UPVGXKq";
                v.ShipDateTime = DateTime.Parse("2024-11-03 11:26:44");
                v.DeliveryCompany = "cb4B3";
                v.DeliveryCompanyPhone = "YQQfMMV2KEB4gbOAsdzMisa4";
                v.DeliveryMan = "12vldEgSSjhRN6P7";
                v.LicensePlate = "FZqOqJgKqqNDXLlRnYTnrMdo";
                context.Set<DGOrderInfo>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DGOrderInfo v1 = new DGOrderInfo();
            DGOrderInfo v2 = new DGOrderInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "UPVGXKq";
                v1.ShipDateTime = DateTime.Parse("2024-11-03 11:26:44");
                v1.DeliveryCompany = "cb4B3";
                v1.DeliveryCompanyPhone = "YQQfMMV2KEB4gbOAsdzMisa4";
                v1.DeliveryMan = "12vldEgSSjhRN6P7";
                v1.LicensePlate = "FZqOqJgKqqNDXLlRnYTnrMdo";
                v2.Name = "XrFeGuV5Y1EQqfksQvq5zdPHPXiZc41D1ivNB2Us2ab";
                v2.ShipDateTime = DateTime.Parse("2025-05-08 11:26:44");
                v2.DeliveryCompany = "EE";
                v2.DeliveryCompanyPhone = "Z2GcjbMr6542qMPIONeOqy1IWfQ2LJ9LuwO";
                v2.DeliveryMan = "L94VbvV4ypybCOQUV9sV8qq042aIZyQU2fHOojNd5p";
                v2.LicensePlate = "uMvKAZYYKcaTHfQMmoWFNNzh2D4Wa";
                context.Set<DGOrderInfo>().Add(v1);
                context.Set<DGOrderInfo>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DGOrderInfo>().Find(v1.ID);
                var data2 = context.Set<DGOrderInfo>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
