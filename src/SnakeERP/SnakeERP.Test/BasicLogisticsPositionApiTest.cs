using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using SnakeERP.Controllers;
using SnakeERP.ViewModel.Basic.BasicLogisticsPositionVMs;
using SnakeERP.Model;
using SnakeERP.DataAccess;


namespace SnakeERP.Test
{
    [TestClass]
    public class BasicLogisticsPositionApiTest
    {
        private BasicLogisticsPositionController _controller;
        private string _seed;

        public BasicLogisticsPositionApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BasicLogisticsPositionController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BasicLogisticsPositionSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BasicLogisticsPositionVM vm = _controller.Wtm.CreateVM<BasicLogisticsPositionVM>();
            BasicLogisticsPosition v = new BasicLogisticsPosition();
            
            v.LogisticsID = AddBasicLogistics();
            v.Name = "lOLZTCJ6T1uuX";
            v.Contact = "vYuRh2Lme5XExDBJyJOWVG7H6N";
            v.Phone1 = "ZdcrmB8eI";
            v.Phone2 = "7VH97YDAPMtxNRm6bv2QNrg";
            v.Phone3 = "Gnbp";
            v.ProvinceID = AddBasicProvince();
            v.CityID = AddBasicCity();
            v.CountyID = AddBasicCounty();
            v.Address = "m0fhqZLgtM1qaeDjJfO7L4MvTLAl9bIu4Rm35Ujl2FIdoxxaSwcMfEAhPlbr0NZDoJWH08wTCFxtzYx9XgColntV4gK4lb1c6oQiD2OpXjwFDGp6e1zi5x7QTglEqJIV1GZX9dJu1u6a0S0SUFE8g";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicLogisticsPosition>().Find(v.ID);
                
                Assert.AreEqual(data.Name, "lOLZTCJ6T1uuX");
                Assert.AreEqual(data.Contact, "vYuRh2Lme5XExDBJyJOWVG7H6N");
                Assert.AreEqual(data.Phone1, "ZdcrmB8eI");
                Assert.AreEqual(data.Phone2, "7VH97YDAPMtxNRm6bv2QNrg");
                Assert.AreEqual(data.Phone3, "Gnbp");
                Assert.AreEqual(data.Address, "m0fhqZLgtM1qaeDjJfO7L4MvTLAl9bIu4Rm35Ujl2FIdoxxaSwcMfEAhPlbr0NZDoJWH08wTCFxtzYx9XgColntV4gK4lb1c6oQiD2OpXjwFDGp6e1zi5x7QTglEqJIV1GZX9dJu1u6a0S0SUFE8g");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BasicLogisticsPosition v = new BasicLogisticsPosition();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LogisticsID = AddBasicLogistics();
                v.Name = "lOLZTCJ6T1uuX";
                v.Contact = "vYuRh2Lme5XExDBJyJOWVG7H6N";
                v.Phone1 = "ZdcrmB8eI";
                v.Phone2 = "7VH97YDAPMtxNRm6bv2QNrg";
                v.Phone3 = "Gnbp";
                v.ProvinceID = AddBasicProvince();
                v.CityID = AddBasicCity();
                v.CountyID = AddBasicCounty();
                v.Address = "m0fhqZLgtM1qaeDjJfO7L4MvTLAl9bIu4Rm35Ujl2FIdoxxaSwcMfEAhPlbr0NZDoJWH08wTCFxtzYx9XgColntV4gK4lb1c6oQiD2OpXjwFDGp6e1zi5x7QTglEqJIV1GZX9dJu1u6a0S0SUFE8g";
                context.Set<BasicLogisticsPosition>().Add(v);
                context.SaveChanges();
            }

            BasicLogisticsPositionVM vm = _controller.Wtm.CreateVM<BasicLogisticsPositionVM>();
            var oldID = v.ID;
            v = new BasicLogisticsPosition();
            v.ID = oldID;
       		
            v.Name = "YbroSrZMdYZbAg";
            v.Contact = "J7ube4uIQDDDJhPUGlU4XfTkg2HWztXiomOlmcPQ3fnAgQ";
            v.Phone1 = "Pu";
            v.Phone2 = "32L3Ph4tCyPHlreag4VLh3CJ95";
            v.Phone3 = "WyaZknSW82EfBDkE7L8nlgB";
            v.Address = "yt81NC3UDaXfiRSoMnbo7TTyUwD596fYlKJVdDX3oA5sQBFMjYMjZSmYK7qmNcaD5XGOchPdXIvJgWWSQFBbs0I6KxrPeDsTJnQzeA0HlBZ39itsPLbQI1vVdkq7CWj6eqvcoaIRfJ4MPmEcv1d2DuzvqWLxyx6g0OiOm5vqVqySsUdVxC1STfY2niZ5MEc0";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LogisticsID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Contact", "");
            vm.FC.Add("Entity.Phone1", "");
            vm.FC.Add("Entity.Phone2", "");
            vm.FC.Add("Entity.Phone3", "");
            vm.FC.Add("Entity.ProvinceID", "");
            vm.FC.Add("Entity.CityID", "");
            vm.FC.Add("Entity.CountyID", "");
            vm.FC.Add("Entity.Address", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BasicLogisticsPosition>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "YbroSrZMdYZbAg");
                Assert.AreEqual(data.Contact, "J7ube4uIQDDDJhPUGlU4XfTkg2HWztXiomOlmcPQ3fnAgQ");
                Assert.AreEqual(data.Phone1, "Pu");
                Assert.AreEqual(data.Phone2, "32L3Ph4tCyPHlreag4VLh3CJ95");
                Assert.AreEqual(data.Phone3, "WyaZknSW82EfBDkE7L8nlgB");
                Assert.AreEqual(data.Address, "yt81NC3UDaXfiRSoMnbo7TTyUwD596fYlKJVdDX3oA5sQBFMjYMjZSmYK7qmNcaD5XGOchPdXIvJgWWSQFBbs0I6KxrPeDsTJnQzeA0HlBZ39itsPLbQI1vVdkq7CWj6eqvcoaIRfJ4MPmEcv1d2DuzvqWLxyx6g0OiOm5vqVqySsUdVxC1STfY2niZ5MEc0");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BasicLogisticsPosition v = new BasicLogisticsPosition();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LogisticsID = AddBasicLogistics();
                v.Name = "lOLZTCJ6T1uuX";
                v.Contact = "vYuRh2Lme5XExDBJyJOWVG7H6N";
                v.Phone1 = "ZdcrmB8eI";
                v.Phone2 = "7VH97YDAPMtxNRm6bv2QNrg";
                v.Phone3 = "Gnbp";
                v.ProvinceID = AddBasicProvince();
                v.CityID = AddBasicCity();
                v.CountyID = AddBasicCounty();
                v.Address = "m0fhqZLgtM1qaeDjJfO7L4MvTLAl9bIu4Rm35Ujl2FIdoxxaSwcMfEAhPlbr0NZDoJWH08wTCFxtzYx9XgColntV4gK4lb1c6oQiD2OpXjwFDGp6e1zi5x7QTglEqJIV1GZX9dJu1u6a0S0SUFE8g";
                context.Set<BasicLogisticsPosition>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BasicLogisticsPosition v1 = new BasicLogisticsPosition();
            BasicLogisticsPosition v2 = new BasicLogisticsPosition();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LogisticsID = AddBasicLogistics();
                v1.Name = "lOLZTCJ6T1uuX";
                v1.Contact = "vYuRh2Lme5XExDBJyJOWVG7H6N";
                v1.Phone1 = "ZdcrmB8eI";
                v1.Phone2 = "7VH97YDAPMtxNRm6bv2QNrg";
                v1.Phone3 = "Gnbp";
                v1.ProvinceID = AddBasicProvince();
                v1.CityID = AddBasicCity();
                v1.CountyID = AddBasicCounty();
                v1.Address = "m0fhqZLgtM1qaeDjJfO7L4MvTLAl9bIu4Rm35Ujl2FIdoxxaSwcMfEAhPlbr0NZDoJWH08wTCFxtzYx9XgColntV4gK4lb1c6oQiD2OpXjwFDGp6e1zi5x7QTglEqJIV1GZX9dJu1u6a0S0SUFE8g";
                v2.LogisticsID = v1.LogisticsID; 
                v2.Name = "YbroSrZMdYZbAg";
                v2.Contact = "J7ube4uIQDDDJhPUGlU4XfTkg2HWztXiomOlmcPQ3fnAgQ";
                v2.Phone1 = "Pu";
                v2.Phone2 = "32L3Ph4tCyPHlreag4VLh3CJ95";
                v2.Phone3 = "WyaZknSW82EfBDkE7L8nlgB";
                v2.ProvinceID = v1.ProvinceID; 
                v2.CityID = v1.CityID; 
                v2.CountyID = v1.CountyID; 
                v2.Address = "yt81NC3UDaXfiRSoMnbo7TTyUwD596fYlKJVdDX3oA5sQBFMjYMjZSmYK7qmNcaD5XGOchPdXIvJgWWSQFBbs0I6KxrPeDsTJnQzeA0HlBZ39itsPLbQI1vVdkq7CWj6eqvcoaIRfJ4MPmEcv1d2DuzvqWLxyx6g0OiOm5vqVqySsUdVxC1STfY2niZ5MEc0";
                context.Set<BasicLogisticsPosition>().Add(v1);
                context.Set<BasicLogisticsPosition>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BasicLogisticsPosition>().Find(v1.ID);
                var data2 = context.Set<BasicLogisticsPosition>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddBasicLogistics()
        {
            BasicLogistics v = new BasicLogistics();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "p";
                v.Name = "iXYNqWl8XBmHVgqhYj2eSypDlctdYo";
                v.Alias = "3VDK1Y7djZI1E9x";
                v.CellPhone = "o9";
                v.Address = "tjIaqnuzXQ6mWN1SQcIOW63uFt4MTRVX4r1rxazSEJYLQwqNkZ62NAA30T2sIRFsChqxAVRptg";
                v.LicensePlate = "5ifxxJ50s88o";
                context.Set<BasicLogistics>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBasicProvince()
        {
            BasicProvince v = new BasicProvince();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "HL5A";
                v.Name = "nSuyFxIKnqpMAZ7MoJ0Q";
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

                v.Code = "ZlWj";
                v.Name = "aKM2IYnbW";
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

                v.Code = "zU4";
                v.Name = "abQnGbYsal242V";
                v.CityID = AddBasicCity();
                context.Set<BasicCounty>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
