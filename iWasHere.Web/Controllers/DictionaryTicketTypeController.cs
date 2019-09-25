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
    public class DictionaryTicketTypeController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryTicketTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
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
        public ActionResult SaveTicket(string ticketName)
        {
            ScarletWitchContext gf = new ScarletWitchContext();

            gf.DictionaryTicketType.Add(new DictionaryTicketType
            {
                TicketTypeName = ticketName
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

        public ActionResult GetDictionaryTicketFiltered([DataSourceRequest] DataSourceRequest request, String ticketName)
        {
            IQueryable<DictionaryTicketModel> tickets = _dictionaryService.GetDictionaryTicketFiltered(ticketName);

            tickets = tickets.OrderBy(o => o.TicketTypeId);


            var total = tickets.Count();

            if (request.Page > 0)
            {
                tickets = tickets.Skip((request.Page - 1) * request.PageSize);
            }
            tickets = tickets.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = tickets,
                Total = total
            };

            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TicketAdd(int id)
        {
            if (id != 0)
            {
                DictionaryTicketModel currency = _dictionaryService.GetTicketToEdit(id);
                return View(currency);
            }
            else
            {
                DictionaryTicketModel currency = new DictionaryTicketModel();
                return View(currency);
            }

        }
        public ActionResult EditTicket(int ticketId, string ticketName)
        {
            DictionaryTicketType newTicket = new DictionaryTicketType();
            newTicket.TicketTypeId = ticketId;
            newTicket.TicketTypeName = ticketName;
            ScarletWitchContext context = new ScarletWitchContext();

            context.DictionaryTicketType.Update(newTicket);
            return Json(context.SaveChanges());
        }
        public ActionResult DeleteTicket([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != 0)
            {
                _dictionaryService.DeleteTicket(id);
            }
            return Json(ModelState.ToDataSourceResult());
        }
    }
}
