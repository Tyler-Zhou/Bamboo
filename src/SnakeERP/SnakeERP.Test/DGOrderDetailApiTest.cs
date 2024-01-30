using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.DG.DGOrderDetailVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class DGOrderDetailApiTest
    {
        private DGOrderDetailController _controller;
        private string _seed;

        public DGOrderDetailApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<DGOrderDetailController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new DGOrderDetailSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            DGOrderDetailVM vm = _controller.Wtm.CreateVM<DGOrderDetailVM>();
            DGOrderDetail v = new DGOrderDetail();
            
            v.OrderInfoID = AddDGOrderInfo();
            v.DeliveryMan = "fT1LlEnN5GqyA4K16Fq2FZV";
            v.DeliveryPhone = "niTQ6gYL8HMZDfbFlQVfIBCewQTQBs7jV6yGquwEAcE";
            v.DeliveryAddress = "O6I2";
            v.ReceivingMan = "97IkE";
            v.ReceivingPhone = "Do048Jb8MlOsnrrb8qOC6m7kcCGndrM8Z6TNpCNG7X4";
            v.ReceivingAddress = "6lFTakknBO3pj";
            v.OrderNO = "dxdvlaFe6lEYtLCVcMOR";
            v.DoorQuantity = 83;
            v.SleeveQuantity = 45;
            v.LinesQuantity = 74;
            v.OtherQuantity = 38;
            v.Remark = "YZLNtQx44vbOcqvjjfk5qBuHmmEcR8bWsLcl869xAj1HkXjrwshQA2Hgja";
            v.DeliveryStatus = SnakeERP.Model.EnumDeliveryStatus.Delivery;
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DGOrderDetail>().Find(v.ID);
                
                Assert.AreEqual(data.DeliveryMan, "fT1LlEnN5GqyA4K16Fq2FZV");
                Assert.AreEqual(data.DeliveryPhone, "niTQ6gYL8HMZDfbFlQVfIBCewQTQBs7jV6yGquwEAcE");
                Assert.AreEqual(data.DeliveryAddress, "O6I2");
                Assert.AreEqual(data.ReceivingMan, "97IkE");
                Assert.AreEqual(data.ReceivingPhone, "Do048Jb8MlOsnrrb8qOC6m7kcCGndrM8Z6TNpCNG7X4");
                Assert.AreEqual(data.ReceivingAddress, "6lFTakknBO3pj");
                Assert.AreEqual(data.OrderNO, "dxdvlaFe6lEYtLCVcMOR");
                Assert.AreEqual(data.DoorQuantity, 83);
                Assert.AreEqual(data.SleeveQuantity, 45);
                Assert.AreEqual(data.LinesQuantity, 74);
                Assert.AreEqual(data.OtherQuantity, 38);
                Assert.AreEqual(data.Remark, "YZLNtQx44vbOcqvjjfk5qBuHmmEcR8bWsLcl869xAj1HkXjrwshQA2Hgja");
                Assert.AreEqual(data.DeliveryStatus, SnakeERP.Model.EnumDeliveryStatus.Delivery);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            DGOrderDetail v = new DGOrderDetail();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.OrderInfoID = AddDGOrderInfo();
                v.DeliveryMan = "fT1LlEnN5GqyA4K16Fq2FZV";
                v.DeliveryPhone = "niTQ6gYL8HMZDfbFlQVfIBCewQTQBs7jV6yGquwEAcE";
                v.DeliveryAddress = "O6I2";
                v.ReceivingMan = "97IkE";
                v.ReceivingPhone = "Do048Jb8MlOsnrrb8qOC6m7kcCGndrM8Z6TNpCNG7X4";
                v.ReceivingAddress = "6lFTakknBO3pj";
                v.OrderNO = "dxdvlaFe6lEYtLCVcMOR";
                v.DoorQuantity = 83;
                v.SleeveQuantity = 45;
                v.LinesQuantity = 74;
                v.OtherQuantity = 38;
                v.Remark = "YZLNtQx44vbOcqvjjfk5qBuHmmEcR8bWsLcl869xAj1HkXjrwshQA2Hgja";
                v.DeliveryStatus = SnakeERP.Model.EnumDeliveryStatus.Delivery;
                context.Set<DGOrderDetail>().Add(v);
                context.SaveChanges();
            }

            DGOrderDetailVM vm = _controller.Wtm.CreateVM<DGOrderDetailVM>();
            var oldID = v.ID;
            v = new DGOrderDetail();
            v.ID = oldID;
       		
            v.DeliveryMan = "Hd1OzNg8lLmgebC2xzG9YzOF";
            v.DeliveryPhone = "bIU19GFU6hxD64ZboUd3Ql6pqwFpYIyxVSM9r9";
            v.DeliveryAddress = "yrsrlQNPhJKgGxcnZoF";
            v.ReceivingMan = "bVM";
            v.ReceivingPhone = "p4sNDDsxiirtvTYNgoCGvlz1BEwsTV9LwEOA";
            v.ReceivingAddress = "3i7MX5kxMwy4Z5Hlu6bDq";
            v.OrderNO = "U";
            v.DoorQuantity = 40;
            v.SleeveQuantity = 77;
            v.LinesQuantity = 75;
            v.OtherQuantity = 37;
            v.Remark = "HGBkuww5rYzuhzpWp5t2BmkogRELqK12HkiVyf7ij4LvNXfUIZ43F5pc4kxARUo0csjOc6APuXgM6LTBUG1nyTRkPbKcVFdn8XBRbRNjFIx5CQpSCPkQf4NBgcjyoBcFhM7jTyUzEtC4HtmVs3";
            v.DeliveryStatus = SnakeERP.Model.EnumDeliveryStatus.Delivery;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.OrderInfoID", "");
            vm.FC.Add("Entity.DeliveryMan", "");
            vm.FC.Add("Entity.DeliveryPhone", "");
            vm.FC.Add("Entity.DeliveryAddress", "");
            vm.FC.Add("Entity.ReceivingMan", "");
            vm.FC.Add("Entity.ReceivingPhone", "");
            vm.FC.Add("Entity.ReceivingAddress", "");
            vm.FC.Add("Entity.OrderNO", "");
            vm.FC.Add("Entity.DoorQuantity", "");
            vm.FC.Add("Entity.SleeveQuantity", "");
            vm.FC.Add("Entity.LinesQuantity", "");
            vm.FC.Add("Entity.OtherQuantity", "");
            vm.FC.Add("Entity.Remark", "");
            vm.FC.Add("Entity.DeliveryStatus", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DGOrderDetail>().Find(v.ID);
 				
                Assert.AreEqual(data.DeliveryMan, "Hd1OzNg8lLmgebC2xzG9YzOF");
                Assert.AreEqual(data.DeliveryPhone, "bIU19GFU6hxD64ZboUd3Ql6pqwFpYIyxVSM9r9");
                Assert.AreEqual(data.DeliveryAddress, "yrsrlQNPhJKgGxcnZoF");
                Assert.AreEqual(data.ReceivingMan, "bVM");
                Assert.AreEqual(data.ReceivingPhone, "p4sNDDsxiirtvTYNgoCGvlz1BEwsTV9LwEOA");
                Assert.AreEqual(data.ReceivingAddress, "3i7MX5kxMwy4Z5Hlu6bDq");
                Assert.AreEqual(data.OrderNO, "U");
                Assert.AreEqual(data.DoorQuantity, 40);
                Assert.AreEqual(data.SleeveQuantity, 77);
                Assert.AreEqual(data.LinesQuantity, 75);
                Assert.AreEqual(data.OtherQuantity, 37);
                Assert.AreEqual(data.Remark, "HGBkuww5rYzuhzpWp5t2BmkogRELqK12HkiVyf7ij4LvNXfUIZ43F5pc4kxARUo0csjOc6APuXgM6LTBUG1nyTRkPbKcVFdn8XBRbRNjFIx5CQpSCPkQf4NBgcjyoBcFhM7jTyUzEtC4HtmVs3");
                Assert.AreEqual(data.DeliveryStatus, SnakeERP.Model.EnumDeliveryStatus.Delivery);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            DGOrderDetail v = new DGOrderDetail();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.OrderInfoID = AddDGOrderInfo();
                v.DeliveryMan = "fT1LlEnN5GqyA4K16Fq2FZV";
                v.DeliveryPhone = "niTQ6gYL8HMZDfbFlQVfIBCewQTQBs7jV6yGquwEAcE";
                v.DeliveryAddress = "O6I2";
                v.ReceivingMan = "97IkE";
                v.ReceivingPhone = "Do048Jb8MlOsnrrb8qOC6m7kcCGndrM8Z6TNpCNG7X4";
                v.ReceivingAddress = "6lFTakknBO3pj";
                v.OrderNO = "dxdvlaFe6lEYtLCVcMOR";
                v.DoorQuantity = 83;
                v.SleeveQuantity = 45;
                v.LinesQuantity = 74;
                v.OtherQuantity = 38;
                v.Remark = "YZLNtQx44vbOcqvjjfk5qBuHmmEcR8bWsLcl869xAj1HkXjrwshQA2Hgja";
                v.DeliveryStatus = SnakeERP.Model.EnumDeliveryStatus.Delivery;
                context.Set<DGOrderDetail>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DGOrderDetail v1 = new DGOrderDetail();
            DGOrderDetail v2 = new DGOrderDetail();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.OrderInfoID = AddDGOrderInfo();
                v1.DeliveryMan = "fT1LlEnN5GqyA4K16Fq2FZV";
                v1.DeliveryPhone = "niTQ6gYL8HMZDfbFlQVfIBCewQTQBs7jV6yGquwEAcE";
                v1.DeliveryAddress = "O6I2";
                v1.ReceivingMan = "97IkE";
                v1.ReceivingPhone = "Do048Jb8MlOsnrrb8qOC6m7kcCGndrM8Z6TNpCNG7X4";
                v1.ReceivingAddress = "6lFTakknBO3pj";
                v1.OrderNO = "dxdvlaFe6lEYtLCVcMOR";
                v1.DoorQuantity = 83;
                v1.SleeveQuantity = 45;
                v1.LinesQuantity = 74;
                v1.OtherQuantity = 38;
                v1.Remark = "YZLNtQx44vbOcqvjjfk5qBuHmmEcR8bWsLcl869xAj1HkXjrwshQA2Hgja";
                v1.DeliveryStatus = SnakeERP.Model.EnumDeliveryStatus.Delivery;
                v2.OrderInfoID = v1.OrderInfoID; 
                v2.DeliveryMan = "Hd1OzNg8lLmgebC2xzG9YzOF";
                v2.DeliveryPhone = "bIU19GFU6hxD64ZboUd3Ql6pqwFpYIyxVSM9r9";
                v2.DeliveryAddress = "yrsrlQNPhJKgGxcnZoF";
                v2.ReceivingMan = "bVM";
                v2.ReceivingPhone = "p4sNDDsxiirtvTYNgoCGvlz1BEwsTV9LwEOA";
                v2.ReceivingAddress = "3i7MX5kxMwy4Z5Hlu6bDq";
                v2.OrderNO = "U";
                v2.DoorQuantity = 40;
                v2.SleeveQuantity = 77;
                v2.LinesQuantity = 75;
                v2.OtherQuantity = 37;
                v2.Remark = "HGBkuww5rYzuhzpWp5t2BmkogRELqK12HkiVyf7ij4LvNXfUIZ43F5pc4kxARUo0csjOc6APuXgM6LTBUG1nyTRkPbKcVFdn8XBRbRNjFIx5CQpSCPkQf4NBgcjyoBcFhM7jTyUzEtC4HtmVs3";
                v2.DeliveryStatus = SnakeERP.Model.EnumDeliveryStatus.Delivery;
                context.Set<DGOrderDetail>().Add(v1);
                context.Set<DGOrderDetail>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DGOrderDetail>().Find(v1.ID);
                var data2 = context.Set<DGOrderDetail>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddDGOrderInfo()
        {
            DGOrderInfo v = new DGOrderInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "6a94hmEId0";
                v.ShipDateTime = DateTime.Parse("2023-09-08 11:27:33");
                v.DeliveryCompany = "5PK87iV4o4VAvOz6v94WHTg1Xua00PIxi0nt715P5";
                v.DeliveryCompanyPhone = "U28JWJgiSyixk";
                v.DeliveryMan = "bLXW7S3B0abMU1Nr8OYwN2AZJMslw9ueDAryBSwICC0iDs";
                v.LicensePlate = "i4f2ksTS10nPcuf7WmNouFZ5";
                context.Set<DGOrderInfo>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
