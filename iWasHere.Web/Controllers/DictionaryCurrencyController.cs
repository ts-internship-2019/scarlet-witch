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



        public IActionResult Currencies_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCurrencyModels().ToDataSourceResult(request));
        }

        private static IEnumerable<DictionaryCurrencyModel> GetDictionaryCurrencyModels()
        {
            using (var northwind = new ScarletWitchContext())
            {
                return northwind.DictionaryCurrency.Select(c => new DictionaryCurrencyModel
                {
                    CurrencyId = c.CurrencyId,
                    CurrencyCode = c.CurrencyCode,
                    CurrencyName = c.CurrencyName,
                    CurrencyExchange = Convert.ToDecimal(c.CurrencyExchange)

                }).ToList();



            }
        }

        public IActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            List<DictionaryCurrencyModel> dictionaryCurrencyModels = _dictionaryService.GetDictionaryCurrencyModels();

            return View(dictionaryCurrencyModels);

            
        }

    }
}
