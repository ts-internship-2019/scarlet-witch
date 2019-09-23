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
        public static List<String> imagesPaths; 

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



        public IActionResult ViewLandmark(string id)
        {
            LandmarkModel landmark = _dictionaryService.GetLandmarkSingle(Convert.ToInt32(id));
            return View(landmark);
        }





        public ActionResult GetLandmarksFiltered([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<LandmarkModel> landmarks = _dictionaryService.GetLandmarksFiltered();
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
            _dictionaryService.SaveLandmark( landmarkName,  landmarkTypeId,  hasEntryTicket,  visitIntervalId,
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

        public void SaveImage(List<IFormFile> pic)
        {
            if (pic != null && pic.Count > 0)
            {
                imagesPaths = new List<string>();
                foreach (IFormFile formFile in pic)
                {
                    if (Path.GetExtension(formFile.FileName).ToLower() == ".jpg" || Path.GetExtension(formFile.FileName).ToLower() == ".png"
                        || Path.GetExtension(formFile.FileName).ToLower() == ".jpeg")
                    {
                        var fileName = Path.Combine(_he.WebRootPath, Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));
                        formFile.CopyTo(new FileStream(fileName, FileMode.Create));
                        imagesPaths.Add(fileName);
                    }
                    

                }
            }

        }



        public void SaveImagesDB()
        {
            foreach(string path in imagesPaths)
            {
                _dictionaryService.SaveImagesDB(path);
            }

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


    }
}