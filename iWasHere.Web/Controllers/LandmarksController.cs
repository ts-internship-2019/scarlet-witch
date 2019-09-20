using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class LandmarksController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public LandmarksController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
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




    }
}