using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryCityController : Controller
    {

        private readonly DictionaryService _dictionaryCityService;

        public DictionaryCityController(DictionaryService dictionaryCityService)
        {
            _dictionaryCityService = dictionaryCityService;
        }

        public IActionResult Index()
        {

            return View();
        }
        public ActionResult GetDictionaryCities([DataSourceRequest] DataSourceRequest request)
        {
            var xc = _dictionaryCityService.GetDictionaryCities().ToDataSourceResult(request);
            return Json(xc);
        }

    }
}