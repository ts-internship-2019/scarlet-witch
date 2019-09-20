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
    public class LandmarksController : Controller
    {
        private readonly DictionaryService _dictionaryLandmarkService;

          public LandmarksController(DictionaryService dictionaryService)
        {
            this._dictionaryLandmarkService = dictionaryService;
        }

        public IActionResult LandmarkList()
        {
            return View();
        }

        public IActionResult AddLandmark()
        {
            return View();
        }

        public ActionResult SaveLandmark(string landmarkName, int landmarkTypeId, bool hasEntryTicket, int visitIntervalId,
            int ticketId, string streetName, int streetNumber, int cityId, float latitude, float longitude, int landmarkId)
        {
            _dictionaryLandmarkService.SaveLandmark( landmarkName,  landmarkTypeId,  hasEntryTicket,  visitIntervalId,
             ticketId,  streetName,  streetNumber,  cityId,  latitude,  longitude,  landmarkId);

            return RedirectToAction("LandmarkList");
        }
        

        public ActionResult GetAllVisitIntervals([DataSourceRequest] DataSourceRequest request)
        {
            var context = new ScarletWitchContext();
            var cnts = context.DictionaryInterval.Select(b => new DictionaryIntervalModel()
            {
                VisitIntervalName = b.VisitIntervalName,
                VisitIntervalId = b.VisitIntervalId
            }).ToList();
            return Json(cnts);
        }

        public ActionResult GetAllLandmarkTypes([DataSourceRequest] DataSourceRequest request)
        {
            var context = new ScarletWitchContext();
            var cnts = context.DictionaryLandmarkType.Select(b => new DictionaryLandmarkTypeModel()
            {
                LandmarkTypeCode = b.LandmarkTypeCode,
                LandmarkTypeId = b.LandmarkTypeId
            }).ToList();
            return Json(cnts);
        }


        public ActionResult GetAllTicketTypes([DataSourceRequest] DataSourceRequest request)
        {
            var context = new ScarletWitchContext();
            var cnts = context.Ticket.Select(b => new DictionaryTicketModel()
            {
                TicketTypeName = b.TicketName,
                TicketTypeId = b.TicketId
            }).ToList();
            return Json(cnts);
        }

        public ActionResult GetAllCities([DataSourceRequest] DataSourceRequest request)
        {
            var context = new ScarletWitchContext();
            var cnts = context.DictionaryCity.Select(b => new DictionaryCityModel()
            {
                Name = b.CityName,
                Id = b.CityId
            }).ToList();
            return Json(cnts);
        }
        
    }
}