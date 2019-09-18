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
    public class DictionaryLandmarkTypeController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryLandmarkTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult Index()
        {
            //List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View();
        }

        public IActionResult Landmark_Types_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetLandmarkTypes().ToDataSourceResult(request));
        }

        private static IEnumerable<DictionaryLandmarkTypeModel> GetLandmarkTypes()
        {
            using (var northwind = new ScarletWitchContext())
            {
                return northwind.DictionaryLandmarkType.Select(landmark => new DictionaryLandmarkTypeModel
                {
                    LandmarkTypeCode = landmark.LandmarkTypeCode,
                    Description = landmark.Description
                 
                }).ToList();
            }
        }

        public ActionResult LandmarkType_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<DictionaryLandmarkType> landmarkTypes = new ScarletWitchContext().DictionaryLandmarkType;

            landmarkTypes = landmarkTypes.OrderBy(o => o.LandmarkTypeId);


            var total = landmarkTypes.Count();

            if (request.Page > 0)
            {
                landmarkTypes = landmarkTypes.Skip((request.Page - 1) * request.PageSize);
            }
            landmarkTypes = landmarkTypes.Take(request.PageSize);

            var result = new DataSourceResult()
            {
                Data = landmarkTypes,
                Total = total
            };

            return Json(result);
        }

        public ActionResult GetDictionaryLandmarkTypesFiltered([DataSourceRequest] DataSourceRequest request, String landmarkTypeName)
        {
            IQueryable<DictionaryLandmarkTypeModel> landmarks = _dictionaryService.GetDictionaryLandmarkTypesFiltered(landmarkTypeName);
            landmarks.ToDataSourceResult(request);
            landmarks = landmarks.OrderBy(o => o.LandmarkTypeId);
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

        public ActionResult SaveLandmarkType(string landmarkCode, string description)
        {
            ScarletWitchContext gf = new ScarletWitchContext();
            gf.DictionaryLandmarkType.Add(new DictionaryLandmarkType
            {
                LandmarkTypeCode = landmarkCode,
                Description = description
               

            });
            return Json(gf.SaveChanges());
        }


        public IActionResult LandmarkTypeAdd()
        {
            return View();
        }



    }
}