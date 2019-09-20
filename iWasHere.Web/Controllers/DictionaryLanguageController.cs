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


        public IActionResult LanguageAdd(int id)
        {
            if (id != 0)
            {
                DictionaryLanguageModel language = _dictionaryService.GetDataToEditLanguage(id);
                return View(language);
            }

            else
            {
                DictionaryLanguageModel language = new DictionaryLanguageModel();
                return View(language);
            }

        }

        public ActionResult SaveLanguage(string langCode, string langName)
        {
            ScarletWitchContext gf = new ScarletWitchContext();
            gf.DictionaryLanguage.Add(new DictionaryLanguage
            {
                LanguageName = langName,
                LanguageCode = langCode
            });
            return Json(gf.SaveChanges());
        }


        public ActionResult EditLanguage(string langName, string langCode, int langId)
        {
            DictionaryLanguage newLang = new DictionaryLanguage();
            newLang.LanguageId = langId;
            newLang.LanguageCode = langCode;
            newLang.LanguageName = langName;
            ScarletWitchContext context = new ScarletWitchContext();
            context.DictionaryLanguage.Update(newLang);
            return Json(context.SaveChanges());
        }

        public ActionResult DeleteLanguage([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {

                CountryXlanguage cxl = new CountryXlanguage();
                cxl=_dictionaryService.GetDataToDeleteLang(id);
                if (cxl==null)
                {
                    _dictionaryService.DeleteLanguages(id);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

    }
}