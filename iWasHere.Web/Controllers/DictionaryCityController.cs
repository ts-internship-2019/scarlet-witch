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

        public ActionResult GetDictionaryCityPagingFilter([DataSourceRequest]DataSourceRequest request, String cityName)
        {
            IQueryable<DictionaryCityModel> cities = _dictionaryCityService.GetDictionaryCitiesFiltered(cityName);
            cities.ToDataSourceResult(request);
            cities = cities.OrderBy(o => o.Id);
            var total = cities.Count();
            if (request.Page > 0)
            {
                cities = cities.Skip((request.Page - 1) * request.PageSize);
            }
            cities = cities.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = cities,
                Total = total
            };
            
            return Json(result);
        }

        public ActionResult Languages_ReadNoFilter([DataSourceRequest]DataSourceRequest request)
        {
            //IQueryable<DictionaryLanguage> languages = new ScarletWitchContext().DictionaryLanguage;
            IQueryable<DictionaryCityModel> cities = _dictionaryCityService.GetDictionaryCities();
            cities = cities.OrderBy(o => o.Id);
            var total = cities.Count();
            if (request.Page > 0)
            {
                cities = cities.Skip((request.Page - 1) * request.PageSize);
            }
            cities = cities.Take(request.PageSize);

            DataSourceResult result = new DataSourceResult()
            {
                Data = cities,
                Total = total
            };


            return Json(result);
        }

        public ActionResult GetDictionaryCities([DataSourceRequest] DataSourceRequest request)
        {
            var xc = _dictionaryCityService.GetDictionaryCities().ToDataSourceResult(request);
            return Json(xc);
        }

        public ActionResult GetDictionaryCitiesFiltered([DataSourceRequest] DataSourceRequest request, String cityName)
        {
            var xc = _dictionaryCityService.GetDictionaryCitiesFiltered(cityName).ToDataSourceResult(request);
            return Json(xc);

        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, int id )
        {
            if (id!= -1)
            {
                _dictionaryCityService.DeleteUsuarios(id);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult PopulateCountyCombo([DataSourceRequest] DataSourceRequest request)
        {
            var counties = _dictionaryCityService.PopulateCountyCombo();
            return Json(counties);
        }

        public IActionResult AddCity(int id)
        {
            if (id != 0)
            {
                DictionaryCityModel city = _dictionaryCityService.GetDataToEdit(id);
                return View(city);
            }
               
            else
            {
                DictionaryCityModel city = new DictionaryCityModel();
                return View(city);
            }
               
        }

 

        public ActionResult SaveCity(string cityName, int countyId)
        {
            ScarletWitchContext context= new ScarletWitchContext();
            context.DictionaryCity.Add(new DictionaryCity
            {
                CityName = cityName,
                CountyId = countyId
            });
            return Json(context.SaveChanges());
        }

        public ActionResult EditCity(string cityName, int countyId, int cityId)
        {
            DictionaryCity newCity = new DictionaryCity();
            newCity.CityName = cityName;
            newCity.CountyId = countyId;
            newCity.CityId = cityId;
            ScarletWitchContext context = new ScarletWitchContext();
            context.DictionaryCity.Update(newCity);
            return Json(context.SaveChanges());
        }

    }
} 

