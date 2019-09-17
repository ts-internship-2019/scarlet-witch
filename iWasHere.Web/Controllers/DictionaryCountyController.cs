using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.Models;
using Kendo.Mvc.Extensions;
using iWasHere.Domain.DTOs;

namespace iWasHere.Web.Controllers
{
    public class DictionaryCountyController : Controller
    {
        private readonly DictionaryService _dictionaryCountyService;

        public DictionaryCountyController(DictionaryService dictionaryCountyService)
        {
            _dictionaryCountyService = dictionaryCountyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetDictionaryCounties([DataSourceRequest] DataSourceRequest request)
        {
            var xc = _dictionaryCountyService.GetDictionaryCounties().ToDataSourceResult(request);
            return Json(xc);
        }
    }
}