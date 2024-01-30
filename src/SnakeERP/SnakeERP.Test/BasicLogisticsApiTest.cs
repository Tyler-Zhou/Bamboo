using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicLogisticsVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicLogisticsApiTest
    {
        private BasicLogisticsController _controller;
        private string _seed;

        public BasicLogisticsApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicLogisticsController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicLogisticsSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicLogisticsVM vm = _controller.Wtm.CreateVM<BasicLogisticsVM>();
            BasicLogistics v = new BasicLogistics();
            
            v.Code = "Rt1zJLV";
            v.Name = "6zWu3P9NkwbvCe8j5W6xbt6UTRh71JBTEl5r5O0DKM";
            v.Alias = "DEdnrXXPIOvMaRSW4YjehFIPdMlAmonSTVlnwjkEg8Ng";
            v.CellPhone = "W1Aw2GVbVMabmD";
            v.Address = "rfGXVf0riJYaAGC4RGHruBaJcoKy3cFlw";
            v.LicensePlate = "SjDlH1rb7KxflW1nz1aPRISix9RgTQ8d7bmlzB4WD3iw95DY";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicLogistics>().Find(v.ID);
                
                Assert.AreEqual(data.Code, "Rt1zJLV");
                Assert.AreEqual(data.Name, "6zWu3P9NkwbvCe8j5W6xbt6UTRh71JBTEl5r5O0DKM");
                Assert.AreEqual(data.Alias, "DEdnrXXPIOvMaRSW4YjehFIPdMlAmonSTVlnwjkEg8Ng");
                Assert.AreEqual(data.CellPhone, "W1Aw2GVbVMabmD");
                Assert.AreEqual(data.Address, "rfGXVf0riJYaAGC4RGHruBaJcoKy3cFlw");
                Assert.AreEqual(data.LicensePlate, "SjDlH1rb7KxflW1nz1aPRISix9RgTQ8d7bmlzB4WD3iw95DY");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicLogistics v = new BasicLogistics();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "Rt1zJLV";
                v.Name = "6zWu3P9NkwbvCe8j5W6xbt6UTRh71JBTEl5r5O0DKM";
                v.Alias = "DEdnrXXPIOvMaRSW4YjehFIPdMlAmonSTVlnwjkEg8Ng";
                v.CellPhone = "W1Aw2GVbVMabmD";
                v.Address = "rfGXVf0riJYaAGC4RGHruBaJcoKy3cFlw";
                v.LicensePlate = "SjDlH1rb7KxflW1nz1aPRISix9RgTQ8d7bmlzB4WD3iw95DY";
                context.Set<BasicLogistics>().Add(v);
                context.SaveChanges();
            }

            BasicLogisticsVM vm = _controller.Wtm.CreateVM<BasicLogisticsVM>();
            var oldID = v.ID;
            v = new BasicLogistics();
            v.ID = oldID;
       		
            v.Code = "4";
            v.Name = "IUIQ";
            v.Alias = "iLUso3DxPmHovKcxyEC0FUP6EqksEM0b3zZ2OKjUMKkzPJ";
            v.CellPhone = "ukOtWUJFrCgH";
            v.Address = "JznHI9BCNlAxUkyZbFqK90TXL6430CNoCHUINUO7FnU8EeUdP4GyB55LSz643Grdd1d4lWHTb2ZRpGmKvMpTezrPdFvST";
            v.LicensePlate = "nZfaMHTV2m3BYzhKplCvuJqiUTUU2lOZpznN0xeW4Ql";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Alias", "");
            vm.FC.Add("Entity.CellPhone", "");
            vm.FC.Add("Entity.Address", "");
            vm.FC.Add("Entity.LicensePlate", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicLogistics>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "4");
                Assert.AreEqual(data.Name, "IUIQ");
                Assert.AreEqual(data.Alias, "iLUso3DxPmHovKcxyEC0FUP6EqksEM0b3zZ2OKjUMKkzPJ");
                Assert.AreEqual(data.CellPhone, "ukOtWUJFrCgH");
                Assert.AreEqual(data.Address, "JznHI9BCNlAxUkyZbFqK90TXL6430CNoCHUINUO7FnU8EeUdP4GyB55LSz643Grdd1d4lWHTb2ZRpGmKvMpTezrPdFvST");
                Assert.AreEqual(data.LicensePlate, "nZfaMHTV2m3BYzhKplCvuJqiUTUU2lOZpznN0xeW4Ql");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicLogistics v = new BasicLogistics();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "Rt1zJLV";
                v.Name = "6zWu3P9NkwbvCe8j5W6xbt6UTRh71JBTEl5r5O0DKM";
                v.Alias = "DEdnrXXPIOvMaRSW4YjehFIPdMlAmonSTVlnwjkEg8Ng";
                v.CellPhone = "W1Aw2GVbVMabmD";
                v.Address = "rfGXVf0riJYaAGC4RGHruBaJcoKy3cFlw";
                v.LicensePlate = "SjDlH1rb7KxflW1nz1aPRISix9RgTQ8d7bmlzB4WD3iw95DY";
                context.Set<BasicLogistics>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicLogistics v1 = new BasicLogistics();
            BasicLogistics v2 = new BasicLogistics();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "Rt1zJLV";
                v1.Name = "6zWu3P9NkwbvCe8j5W6xbt6UTRh71JBTEl5r5O0DKM";
                v1.Alias = "DEdnrXXPIOvMaRSW4YjehFIPdMlAmonSTVlnwjkEg8Ng";
                v1.CellPhone = "W1Aw2GVbVMabmD";
                v1.Address = "rfGXVf0riJYaAGC4RGHruBaJcoKy3cFlw";
                v1.LicensePlate = "SjDlH1rb7KxflW1nz1aPRISix9RgTQ8d7bmlzB4WD3iw95DY";
                v2.Code = "4";
                v2.Name = "IUIQ";
                v2.Alias = "iLUso3DxPmHovKcxyEC0FUP6EqksEM0b3zZ2OKjUMKkzPJ";
                v2.CellPhone = "ukOtWUJFrCgH";
                v2.Address = "JznHI9BCNlAxUkyZbFqK90TXL6430CNoCHUINUO7FnU8EeUdP4GyB55LSz643Grdd1d4lWHTb2ZRpGmKvMpTezrPdFvST";
                v2.LicensePlate = "nZfaMHTV2m3BYzhKplCvuJqiUTUU2lOZpznN0xeW4Ql";
                context.Set<BasicLogistics>().Add(v1);
                context.Set<BasicLogistics>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicLogistics>().Find(v1.ID);
                var data2 = context.Set<BasicLogistics>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
