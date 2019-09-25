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

        public ActionResult Countries_Paging([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<DictionaryCountry> countries = new ScarletWitchContext().DictionaryCountry;

            countries = countries.OrderBy(o => o.CountryId);


            var total = countries.Count();

            if (request.Page > 0)
            {
                countries = countries.Skip((request.Page - 1) * request.PageSize);
            }
            countries = countries.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = countries,
                Total = total
            };

            return Json(result);
        }

        public IActionResult AddCountry()
        {   
            return View();
        }

        public IActionResult AddCountryToDb()
        {
            return View();
        }

        public ActionResult GetLanguage(string text)
        {
            var jk = new ScarletWitchContext();


            var cnts = jk.DictionaryLanguage.Select(cnt => new DictionaryLanguageModel
            {
                LanguageId = cnt.LanguageId,
                LanguageName = cnt.LanguageName

            });

            if (!string.IsNullOrEmpty(text))
            {
                cnts = cnts.Where(c => c.LanguageName.Contains(text));
            }


            return Json(cnts);
        }

        public ActionResult GetDictionaryCountriesFiltered([DataSourceRequest] DataSourceRequest request, String countryName)
        {
            IQueryable<DictionaryCountryModel> countries = _dictionaryService.GetDictionaryCountriesFiltered(countryName);

            countries = countries.OrderBy(o => o.CountryId);


            var total = countries.Count();

            if (request.Page > 0)
            {
                countries = countries.Skip((request.Page - 1) * request.PageSize);
            }
            countries = countries.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = countries,
                Total = total
            };

            return Json(result);
        }

        
        public ActionResult GetLanguageAdd(string text)
        {
            var jk = new ScarletWitchContext();

            var lngs = jk.DictionaryLanguage.Select(lng => new DictionaryLanguageModel
            {
                LanguageId = lng.LanguageId,
                LanguageName = lng.LanguageName

            });

            if (!string.IsNullOrEmpty(text))
            {
                lngs = lngs.Where(c => c.LanguageName.Contains(text));
            }

            return Json(lngs);
        }

        public int Delete([DataSourceRequest] DataSourceRequest request, int id)
        {
            int sters = _dictionaryService.DeleteCountry(id);
            return sters;
        }

        public ActionResult EditCountry(string countryName, int countryId, int languageId)
        {
            DictionaryCountry newCountry = new DictionaryCountry();
            newCountry.CountryName = countryName;
            newCountry.CountryId = countryId;
            newCountry.LanguageId = languageId;

            //CountryXlanguage cxl = new CountryXlanguage();
            //cxl.CountryId = countryId;
            //cxl.LanguageId = languageId;

            ScarletWitchContext context = new ScarletWitchContext();
            //context.CountryXlanguage.Update(cxl);
            context.DictionaryCountry.Update(newCountry);
            return Json(context.SaveChanges());
        }

        public ActionResult SaveCountry(string countryName, int languageId)
        {
            ScarletWitchContext context = new ScarletWitchContext();
            context.DictionaryCountry.Add(new DictionaryCountry
            {
                CountryName = countryName,
                LanguageId = languageId
            });
            return Json(context.SaveChanges());
        }
    }
}