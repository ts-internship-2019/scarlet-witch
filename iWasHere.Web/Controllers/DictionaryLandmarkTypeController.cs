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
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View(dictionaryLandmarkTypeModels);
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

     
    }
}