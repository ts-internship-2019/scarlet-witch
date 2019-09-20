using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class LandmarksController : Controller
    {
        public IActionResult LandmarkList()
        {
            return View();
        }

        public IActionResult AddLandmark()
        {
            return View();
        }

        public void SaveLandmark()
        {

        }

        //public ActionResult GetAllVisitIntervals([DataSourceRequest] DataSourceRequest request)
        //{
        //    //IQueryable<DictionaryVisitIntervalsModel> counties = _dictionaryCountyService.GetDictionaryCounties();
        //    //counties = counties.OrderBy(o => o.CountyId);
        //    //var total = counties.Count();
        //    //if (request.Page > 0)
        //    //{
        //    //    counties = counties.Skip((request.Page - 1) * request.PageSize);
        //    //}
        //    //counties = counties.Take(request.PageSize);
        //    //var result = new DataSourceResult()
        //    //{
        //    //    Data = counties,
        //    //    Total = total
        //    //};
        //    //return Json(result);
        //}
        
    }
}