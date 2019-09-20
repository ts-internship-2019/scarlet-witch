using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
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




    }
}