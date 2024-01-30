using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicCustomerVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicCustomerApiTest
    {
        private BasicCustomerController _controller;
        private string _seed;

        public BasicCustomerApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicCustomerController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicCustomerSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicCustomerVM vm = _controller.Wtm.CreateVM<BasicCustomerVM>();
            BasicCustomer v = new BasicCustomer();
            
            v.Code = "qMfW2za0";
            v.Name = "qa20S9ZB3WUSfT";
            v.ProvinceID = AddBasicProvince();
            v.CityID = AddBasicCity();
            v.CountyID = AddBasicCounty();
            v.CellPhone = "DBCysMtyncYI";
            v.HomePhone = "VQZzuW9noLQAck00aC0T9";
            v.SalesID = AddFrameworkUser();
            v.Address = "DBOfWTe78pbidtVb7MkpyEPT0QmeLLBgKkHR0pN";
            v.Region = "EKdp32QAY0PkmE04CFzfv";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicCustomer>().Find(v.ID);
                
                Assert.AreEqual(data.Code, "qMfW2za0");
                Assert.AreEqual(data.Name, "qa20S9ZB3WUSfT");
                Assert.AreEqual(data.CellPhone, "DBCysMtyncYI");
                Assert.AreEqual(data.HomePhone, "VQZzuW9noLQAck00aC0T9");
                Assert.AreEqual(data.Address, "DBOfWTe78pbidtVb7MkpyEPT0QmeLLBgKkHR0pN");
                Assert.AreEqual(data.Region, "EKdp32QAY0PkmE04CFzfv");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicCustomer v = new BasicCustomer();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "qMfW2za0";
                v.Name = "qa20S9ZB3WUSfT";
                v.ProvinceID = AddBasicProvince();
                v.CityID = AddBasicCity();
                v.CountyID = AddBasicCounty();
                v.CellPhone = "DBCysMtyncYI";
                v.HomePhone = "VQZzuW9noLQAck00aC0T9";
                v.SalesID = AddFrameworkUser();
                v.Address = "DBOfWTe78pbidtVb7MkpyEPT0QmeLLBgKkHR0pN";
                v.Region = "EKdp32QAY0PkmE04CFzfv";
                context.Set<BasicCustomer>().Add(v);
                context.SaveChanges();
            }

            BasicCustomerVM vm = _controller.Wtm.CreateVM<BasicCustomerVM>();
            var oldID = v.ID;
            v = new BasicCustomer();
            v.ID = oldID;
       		
            v.Code = "9sNKLRB";
            v.Name = "ZeAt4nA0h9HEIgYMX62B1XA2KRuS7rczGa9rcYXkkmW";
            v.CellPhone = "vJIfEiS6hxqHpscXeK";
            v.HomePhone = "odhx3fCS0UNcu9VMVXiK07doBUU";
            v.Address = "Hofc9lSR9Cj5PTdo6KmuIuZKiJUnmxhBSBMpxIM5jTMdgcwTVLBcn5TUySQYzeAiy2VnNvs6Y2vlkwomZCl4AqLzJs1V3XHBOwws3Mc4GYsrWNAhoDw1";
            v.Region = "NgZE";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.ProvinceID", "");
            vm.FC.Add("Entity.CityID", "");
            vm.FC.Add("Entity.CountyID", "");
            vm.FC.Add("Entity.CellPhone", "");
            vm.FC.Add("Entity.HomePhone", "");
            vm.FC.Add("Entity.SalesID", "");
            vm.FC.Add("Entity.Address", "");
            vm.FC.Add("Entity.Region", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicCustomer>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "9sNKLRB");
                Assert.AreEqual(data.Name, "ZeAt4nA0h9HEIgYMX62B1XA2KRuS7rczGa9rcYXkkmW");
                Assert.AreEqual(data.CellPhone, "vJIfEiS6hxqHpscXeK");
                Assert.AreEqual(data.HomePhone, "odhx3fCS0UNcu9VMVXiK07doBUU");
                Assert.AreEqual(data.Address, "Hofc9lSR9Cj5PTdo6KmuIuZKiJUnmxhBSBMpxIM5jTMdgcwTVLBcn5TUySQYzeAiy2VnNvs6Y2vlkwomZCl4AqLzJs1V3XHBOwws3Mc4GYsrWNAhoDw1");
                Assert.AreEqual(data.Region, "NgZE");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicCustomer v = new BasicCustomer();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "qMfW2za0";
                v.Name = "qa20S9ZB3WUSfT";
                v.ProvinceID = AddBasicProvince();
                v.CityID = AddBasicCity();
                v.CountyID = AddBasicCounty();
                v.CellPhone = "DBCysMtyncYI";
                v.HomePhone = "VQZzuW9noLQAck00aC0T9";
                v.SalesID = AddFrameworkUser();
                v.Address = "DBOfWTe78pbidtVb7MkpyEPT0QmeLLBgKkHR0pN";
                v.Region = "EKdp32QAY0PkmE04CFzfv";
                context.Set<BasicCustomer>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicCustomer v1 = new BasicCustomer();
            BasicCustomer v2 = new BasicCustomer();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "qMfW2za0";
                v1.Name = "qa20S9ZB3WUSfT";
                v1.ProvinceID = AddBasicProvince();
                v1.CityID = AddBasicCity();
                v1.CountyID = AddBasicCounty();
                v1.CellPhone = "DBCysMtyncYI";
                v1.HomePhone = "VQZzuW9noLQAck00aC0T9";
                v1.SalesID = AddFrameworkUser();
                v1.Address = "DBOfWTe78pbidtVb7MkpyEPT0QmeLLBgKkHR0pN";
                v1.Region = "EKdp32QAY0PkmE04CFzfv";
                v2.Code = "9sNKLRB";
                v2.Name = "ZeAt4nA0h9HEIgYMX62B1XA2KRuS7rczGa9rcYXkkmW";
                v2.ProvinceID = v1.ProvinceID; 
                v2.CityID = v1.CityID; 
                v2.CountyID = v1.CountyID; 
                v2.CellPhone = "vJIfEiS6hxqHpscXeK";
                v2.HomePhone = "odhx3fCS0UNcu9VMVXiK07doBUU";
                v2.SalesID = v1.SalesID; 
                v2.Address = "Hofc9lSR9Cj5PTdo6KmuIuZKiJUnmxhBSBMpxIM5jTMdgcwTVLBcn5TUySQYzeAiy2VnNvs6Y2vlkwomZCl4AqLzJs1V3XHBOwws3Mc4GYsrWNAhoDw1";
                v2.Region = "NgZE";
                context.Set<BasicCustomer>().Add(v1);
                context.Set<BasicCustomer>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicCustomer>().Find(v1.ID);
                var data2 = context.Set<BasicCustomer>().Find(v2.ID);
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

                v.Code = "gIIbwSPgkLubi";
                v.Name = "Ywz6OFAU0DSn5dAGjs9WxxNkZi";
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

                v.Code = "Dts2v1d4BD";
                v.Name = "qij5kcje64kGSziUt4KPw7IUZ31IfAKdZAoSM";
                v.ProvinceID = AddBasicProvince();
                context.Set<BasicCity>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBasicCounty()
        {
            BasicCounty v = new BasicCounty();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "5bMAYbscwl6xpSml";
                v.Name = "q9OGsD4EkAGM8Me5KhrqI9LEQNvnProhOxcCUQGcVB022njS";
                v.CityID = AddBasicCity();
                context.Set<BasicCounty>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "yzZCk";
                v.FileExt = "U9";
                v.Path = "B6tLUPY9pYVWsrJ";
                v.Length = 16;
                v.UploadTime = DateTime.Parse("2023-08-20 11:25:36");
                v.SaveMode = "hn";
                v.ExtraInfo = "jy";
                v.HandlerInfo = "UghRouK4q6ZF";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFrameworkUser()
        {
            FrameworkUser v = new FrameworkUser();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Email = "7HUksZO66QwLo3XDnPn3mplgoykp8SMvfFE";
                v.Gender = WalkingTec.Mvvm.Core.GenderEnum.Male;
                v.CellPhone = "RX";
                v.HomePhone = "cj7js42fU";
                v.Address = "jLCnTb1WR2OvApFsaUXzCDZYo7pdFefBVEoEVgrMHHtKikX3zOcQ3VhfYzZZKpR4W4gvV4AI9JCCuNSUmAAM3cz78ip6ldn44039JFA2stzGRh7GDoAoL5iwCi";
                v.ZipCode = "4aAZmhbUDztZ6";
                v.ITCode = "KvTHT";
                v.Password = "DHYo6KHcSo6z1hWccQu9x7zb5Oucel";
                v.Name = "TIvCy96HdfQdK";
                v.IsValid = false;
                v.PhotoId = AddFileAttachment();
                context.Set<FrameworkUser>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
