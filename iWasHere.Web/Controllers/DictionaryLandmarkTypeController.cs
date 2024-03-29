﻿using System;
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
                    LandmarkTypeId = landmark.LandmarkTypeId,
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

            try
            {
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
            catch
            {
               
                return Json(0);
                
            }
           

        }


        public IActionResult SaveLandmarkType(string landmarkCode, string description)
        {
           
                ScarletWitchContext gf = new ScarletWitchContext();
                gf.DictionaryLandmarkType.Add(new DictionaryLandmarkType
                {
                    
                    LandmarkTypeCode = landmarkCode,
                    Description = description


                });

               return Json(gf.SaveChanges());
                                     
        }

        public IActionResult EditLandmarkType(int landmarkTypeId, string landmarkCode, string description)
        {
            
                DictionaryLandmarkType newLandmarkType = new DictionaryLandmarkType();
                newLandmarkType.LandmarkTypeId = landmarkTypeId;
                newLandmarkType.LandmarkTypeCode = landmarkCode;
                newLandmarkType.Description = description;
                ScarletWitchContext context = new ScarletWitchContext();
                context.DictionaryLandmarkType.Update(newLandmarkType);
                return Json(context.SaveChanges());
            
        }


        public IActionResult LandmarkTypeAdd(int id)
        {
            
                if (id != 0)
            {
                DictionaryLandmarkTypeModel landmarkTypeModel = _dictionaryService.GetDataToEditLandmarkType(id);
                return View(landmarkTypeModel);
            }

            else
            {
                DictionaryLandmarkTypeModel landmarkTypeModel = new DictionaryLandmarkTypeModel();
                return View(landmarkTypeModel);
            }
        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteLandmarkType(id);
            }

            return Json(ModelState.ToDataSourceResult());
        }



    }
}