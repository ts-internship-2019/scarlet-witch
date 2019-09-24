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
    public class DictionaryIntervalController
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryIntervalController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            return View();
        }
        public ActionResult Currencies_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<DictionaryInterval> intervals = new ScarletWitchContext().DictionaryInterval;

            intervals = intervals.OrderBy(o => o.VisitIntervalId);


            var total = intervals.Count();

            if (request.Page > 0)
            {
                intervals = intervals.Skip((request.Page - 1) * request.PageSize);
            }
            intervals = intervals.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = intervals,
                Total = total
            };

            return Json(result);
        }
        public ActionResult SaveInterval(string visitIntervalName)
        {
            ScarletWitchContext gf = new ScarletWitchContext();

            gf.DictionaryInterval.Add(new DictionaryInterval
            {
                VisitIntervalName = visitIntervalName
            });
            try
            {
                return Json(gf.SaveChanges());
            }
            catch (Exception e)
            {
                return Json("Empty");
            }

        }

        public ActionResult GetDictionaryIntervalFiltered([DataSourceRequest] DataSourceRequest request, String intervalName)
        {
            IQueryable<DictionaryIntervalModel> intervals = _dictionaryService.GetDictionaryIntervalFiltered(intervalName);

            intervals = intervals.OrderBy(o => o.VisitIntervalId);


            var total = intervals.Count();

            if (request.Page > 0)
            {
                intervals = intervals.Skip((request.Page - 1) * request.PageSize);
            }
            intervals = intervals.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = intervals,
                Total = total
            };

            return Json(result);
        }
        public IActionResult IntervalAdd(int id)
        {
            if (id != 0)
            {
                DictionaryIntervalModel currency = _dictionaryService.GetIntervalToEdit(id);
                return View(currency);
            }
            else
            {
                DictionaryIntervalModel currency = new DictionaryIntervalModel();
                return View(currency);
            }

        }
        public ActionResult EditInterval(int visitIntervalId, string visitIntervalName)
        {
            DictionaryInterval newInterval = new DictionaryInterval();
            newInterval.VisitIntervalId = visitIntervalId;
            newInterval.VisitIntervalName = visitIntervalName;
            ScarletWitchContext context = new ScarletWitchContext();

            context.DictionaryInterval.Update(newInterval);
            return Json(context.SaveChanges());
        }
        public ActionResult DeleteInterval([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != 0)
            {
                _dictionaryService.DeleteInterval(id);
            }
            return Json(ModelState.ToDataSourceResult());
        }
    }
}
