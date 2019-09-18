using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryLanguageController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryLanguageController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Languages_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(GetLanguages().ToDataSourceResult(request));
        //}

        //private static IEnumerable<DictionaryLanguageModel> GetLanguages()
        //{
        //    using (var x = new ScarletWitchContext())
        //    {
        //        return x.DictionaryLanguage.Select(language => new DictionaryLanguageModel
        //        {
        //            LanguageId = language.LanguageId,
        //            LanguageName = language.LanguageName,
        //            LanguageCode = language.LanguageCode
        //        }).ToList();
        //    }
        //}



        /// ///////////////////////////////////////////////////////////////


        public ActionResult Languages_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<DictionaryLanguage> languages = new ScarletWitchContext().DictionaryLanguage;
            
            languages = languages.OrderBy(o => o.LanguageId);
            

            var total = languages.Count();

            if (request.Page > 0)
            {
                languages = languages.Skip((request.Page - 1) * request.PageSize);
            }
            languages = languages.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = languages, 
                Total = total 
            };

            return Json(result);
        }


        public ActionResult GetDictionaryLanguagesFiltered([DataSourceRequest] DataSourceRequest request, String languageName)
        {
            IQueryable<DictionaryLanguageModel> languages =_dictionaryService.GetDictionaryLanguagesFiltered(languageName);

            languages = languages.OrderBy(o => o.LanguageId);


            var total = languages.Count();

            if (request.Page > 0)
            {
                languages = languages.Skip((request.Page - 1) * request.PageSize);
            }
            languages = languages.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = languages,
                Total = total
            };

            return Json(result);
        }

    }
}