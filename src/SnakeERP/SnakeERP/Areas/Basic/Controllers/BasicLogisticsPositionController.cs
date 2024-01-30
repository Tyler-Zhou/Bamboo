﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using SnakeERP.ViewModel.Basic.BasicLogisticsPositionVMs;
using SnakeERP.Model;


namespace SnakeERP.Controllers
{
    [Area("Basic")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("物流站点管理")]
    [ApiController]
    [Route("api/BasicLogisticsPosition")]
	public partial class BasicLogisticsPositionController : BaseApiController
    {
        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(BasicLogisticsPositionSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<BasicLogisticsPositionListVM>();
                vm.Searcher = searcher;
                return Content(vm.GetJson(enumToString: false));
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }

        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public BasicLogisticsPositionVM Get(string id)
        {
            var vm = Wtm.CreateVM<BasicLogisticsPositionVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(BasicLogisticsPositionVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(BasicLogisticsPositionVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

		[HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<BasicLogisticsPositionBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(BasicLogisticsPositionSearcher searcher)
        {
            var vm = Wtm.CreateVM<BasicLogisticsPositionListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<BasicLogisticsPositionListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<BasicLogisticsPositionImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(BasicLogisticsPositionImportVM vm)
        {
            if (vm!=null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm?.EntityList?.Count ?? 0);
            }
        }


        [HttpGet("GetBasicLogisticss")]
        public ActionResult GetBasicLogisticss()
        {
            return Ok(DC.Set<BasicLogistics>().GetSelectListItems(Wtm, x => x.Name));
        }

        [HttpGet("GetBasicProvinces")]
        public ActionResult GetBasicProvinces()
        {
            return Ok(DC.Set<BasicProvince>().GetSelectListItems(Wtm, x => x.Name));
        }

        [HttpGet("GetBasicCitys")]
        public ActionResult GetBasicCitys()
        {
            return Ok(DC.Set<BasicCity>().GetSelectListItems(Wtm, x => x.Name));
        }

        [HttpGet("GetBasicCountys")]
        public ActionResult GetBasicCountys()
        {
            return Ok(DC.Set<BasicCounty>().GetSelectListItems(Wtm, x => x.Name));
        }

    }
}
