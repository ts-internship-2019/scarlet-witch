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
        private readonly DictionaryCountryController _dictionaryCountryController;

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

        public ActionResult GetAllCountries(string countyName)
        {
            var context = new ScarletWitchContext();
            var cnts = context.DictionaryCountry.Select(cnt => new DictionaryCountryModel
            {
                CountryId = cnt.CountryId,
                CountryName = cnt.CountryName
            });
            if (!string.IsNullOrEmpty(countyName))
            {
                cnts = cnts.Where(c => c.CountryName.Contains(countyName));
            }
            return Json(cnts);
        }

        public IActionResult AddCounty()
        {
            return View();
        }

        public ActionResult SaveCounty(int ctId, string cyName)
        {
            ScarletWitchContext context = new ScarletWitchContext();
            context.DictionaryCounty.Add(new DictionaryCounty
            {
                CountryId = ctId,
                CountyName = cyName,
            });
            return Json(context.SaveChanges());
        }

        public int Verify([DataSourceRequest] DataSourceRequest request, int id)
        {
            int nrOrase= _dictionaryCountyService.VerifyCitiesInCounty(id);
            return nrOrase;
        }
        public int Delete([DataSourceRequest] DataSourceRequest request, int id)
        {
            int sters = _dictionaryCountyService.DeleteCounty(id);
            //if (id != -1)
            //{
            //    sters = _dictionaryCountyService.DeleteCounty(id);
            //}
            return sters;
        }
    }
}






