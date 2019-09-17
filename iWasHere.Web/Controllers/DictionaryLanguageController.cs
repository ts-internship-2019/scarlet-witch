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

        public IActionResult Languages_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetLanguages().ToDataSourceResult(request));
        }

        private static IEnumerable<DictionaryLanguageModel> GetLanguages()
        {
            using (var x = new ScarletWitchContext())
            {
                return x.DictionaryLanguage.Select(language => new DictionaryLanguageModel
                {
                    LanguageId = language.LanguageId,
                    LanguageName=language.LanguageName,
                    LanguageCode=language.LanguageCode
                }).ToList();    
            }
        }

    }
}