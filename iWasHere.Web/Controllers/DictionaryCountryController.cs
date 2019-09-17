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
    public class DictionaryCountryController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCountryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult Countries_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCountryModels().ToDataSourceResult(request));
        }
        public IActionResult Index()
        {
            var list = _dictionaryService.GetDictionaryCountryModels();
            return View(list);
        }

        public IActionResult AddCountry()
        {   
            return View();
        }
    }
}