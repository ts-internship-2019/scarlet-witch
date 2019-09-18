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
    public class DictionaryCurrencyController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCurrencyController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }


        public IActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            List<DictionaryCurrencyModel> dictionaryCurrencyModels = _dictionaryService.GetDictionaryCurrencyModels();

            return View(dictionaryCurrencyModels);


        }


        public ActionResult Currencies_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<DictionaryCurrency> currencies = new ScarletWitchContext().DictionaryCurrency;

            currencies = currencies.OrderBy(o => o.CurrencyId);


            var total = currencies.Count();

            if (request.Page > 0)
            {
                currencies = currencies.Skip((request.Page - 1) * request.PageSize);
            }
            currencies = currencies.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = currencies,
                Total = total
            };

            return Json(result);
        }

        public ActionResult GetCountry(string text)
        {
            var jk = new ScarletWitchContext();


            var cnts = jk.DictionaryCountry.Select(cnt => new DictionaryCountryModel
            {
                CountryId = cnt.CountryId,
                CountryName = cnt.CountryName
               
            });

            if (!string.IsNullOrEmpty(text))
            {
                cnts = cnts.Where(c => c.CountryName.Contains(text));
            }

            return Json(cnts);
        }

        public ActionResult SaveCurrency(int ctId,string crCode,string crName,decimal crExc)
        {
            ScarletWitchContext gf = new ScarletWitchContext();
            gf.DictionaryCurrency.Add(new DictionaryCurrency
            {
                CountryId = ctId,
                CurrencyName = crName,
                CurrencyCode = crCode,
                CurrencyExchange = crExc

            });            
            return Json(gf.SaveChanges());
        }


        public IActionResult CurrencyAdd()
        {
            return View();
        }

    }
}
