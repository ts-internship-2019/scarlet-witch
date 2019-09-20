using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class LandmarksController : Controller
    {
        private readonly DictionaryService _dictionaryService;
        private readonly IHostingEnvironment _he;

        public LandmarksController(DictionaryService dictionaryService, IHostingEnvironment he)
        {
            _dictionaryService = dictionaryService;
            _he = he;
        }

        public IActionResult LandmarkList()
        {
            return View();
        }

        public IActionResult AddLandmark()
        {
            return View();
        }

        public IActionResult Images()
        {
            return View();
        }



        public IActionResult ViewLandmark(int landmarkId)
        {
            LandmarkModel landmark = _dictionaryLandmarkService.GetLandmarkSingle(30);
            return View(landmark);
        }



        public ActionResult GetLandmarksFiltered([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<LandmarkModel> landmarks = _dictionaryLandmarkService.GetLandmarksFiltered();
            landmarks.ToDataSourceResult(request);
            landmarks = landmarks.OrderBy(o => o.LandmarkId);
            var total = landmarks.Count();
            if (request.Page > 0)
            {
                landmarks = landmarks.Skip((request.Page - 1) * request.PageSize);
            }
            landmarks = landmarks.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = landmarks,
                Total = total
            };
            return Json(result);
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

        public void SaveImage(IFormFile pic)
        {
            if( pic!= null)
            {
            
                var fileName = Path.Combine(_he.WebRootPath, Guid.NewGuid().ToString() + Path.GetExtension(pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
            }
        }
        private IEnumerable<string> GetFileInfo(IEnumerable<IFormFile> files)
        {
            List<string> fileInfo = new List<string>();

            foreach (var file in files)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));

                fileInfo.Add(string.Format("{0} ({1} bytes)", fileName, file.Length));
            }

            return fileInfo;
        }
        public ActionResult Submit(IFormFile files)
        {
            IEnumerable<string> fileInfo = new List<string>();

            if (files != null)
            {
                var fileName = Path.Combine(_he.WebRootPath, Path.GetFileName(files.FileName));
                files.CopyTo(new FileStream(fileName, FileMode.Create));
            }

            return View();
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