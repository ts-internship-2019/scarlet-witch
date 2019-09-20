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
            return View();
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

        public ActionResult SaveCurrency(int countryId,string currencyCode,string currencyName,decimal currencyExcchange)
        {
            ScarletWitchContext gf = new ScarletWitchContext();
            gf.DictionaryCurrency.Add(new DictionaryCurrency
            {
                CountryId = countryId,
                CurrencyName = currencyCode,
                CurrencyCode = currencyName,
                CurrencyExchange = currencyExcchange
            });            
            return Json(gf.SaveChanges());
        }
        public ActionResult GetDictionaryCurrencyFiltered([DataSourceRequest] DataSourceRequest request, String currencyName)
        {
            IQueryable<DictionaryCurrencyModel> currency = _dictionaryService.GetDictionaryCurrencyFiltered(currencyName);

            currency = currency.OrderBy(o => o.CurrencyId);


            var total = currency.Count();

            if (request.Page > 0)
            {
                currency = currency.Skip((request.Page - 1) * request.PageSize);
            }
            currency = currency.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = currency,
                Total = total
            };

            return Json(result);
        }

        public IActionResult CurrencyAdd(int id)
        {
            if (id != 0)
            {
                DictionaryCurrencyModel currency = _dictionaryService.GetCurrencyToEdit(id);
                return View(currency);
            }
            else
            {
                DictionaryCurrencyModel currency = new DictionaryCurrencyModel();
                return View(currency);
            }
            
        }

        public ActionResult EditCurrency(int currencyId,string currencyName,string currencyCode,string currencyExchange, int countryId)
        {
            DictionaryCurrency newCurrency = new DictionaryCurrency();
            newCurrency.CurrencyId = currencyId;
            newCurrency.CurrencyName = currencyName;
            newCurrency.CurrencyCode = currencyCode;
            newCurrency.CurrencyExchange =Convert.ToDecimal(currencyExchange);           
            newCurrency.CountryId = countryId;
            ScarletWitchContext context = new ScarletWitchContext();
            context.DictionaryCurrency.Update(newCurrency);
            return Json(context.SaveChanges());
        }

        public ActionResult DeleteCurrency([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != 0)
            {
                _dictionaryService.DeleteCurrency(id);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult PopulateCountryCombo([DataSourceRequest] DataSourceRequest request)
        {
            var countries = _dictionaryService.PopulateCountryCombo();
            return Json(countries);
        }

    }
}
