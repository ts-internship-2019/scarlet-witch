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
            IQueryable<DictionaryCountyModel> counties = _dictionaryCountyService.GetDictionaryCounties();
            counties = counties.OrderBy(o => o.CountyId);
            var total = counties.Count();
            if (request.Page > 0)
            {
                counties = counties.Skip((request.Page - 1) * request.PageSize);
            }
            counties = counties.Take(request.PageSize);
            var result = new DataSourceResult()
            {
                Data = counties,
                Total = total
            };
            return Json(result);
        }

        public ActionResult GetDictionaryCountiesByName([DataSourceRequest] DataSourceRequest request, string countyName)
        {
            IQueryable<DictionaryCountyModel> counties = _dictionaryCountyService.GetDictionaryCountiesByName(countyName);
            counties = counties.OrderBy(o => o.CountyId);
            var total = counties.Count();
            if (request.Page > 0)
            {
                counties = counties.Skip((request.Page - 1) * request.PageSize);
            }
            counties = counties.Take(request.PageSize);
            var result = new DataSourceResult()
            {
                Data = counties,
                Total = total
            };
            return Json(result);
        }

        public ActionResult GetDictionaryCountiesByCountry([DataSourceRequest] DataSourceRequest request, string countryName)
        {
            var xc = _dictionaryCountyService.GetDictionaryCountiesByCountry(countryName).ToDataSourceResult(request);
            return Json(xc);
        }
        public IActionResult AddCounty()
        {
            return View();
        }
    }
}






