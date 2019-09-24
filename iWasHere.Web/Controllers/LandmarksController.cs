﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
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
  
        public IActionResult AddLandmark(int id)
        {
            if (id != 0)
            {
                LandmarkModel model = _dictionaryService.GetLandmarkSingle(id);
                return View(model);
            }
            else
            {
                LandmarkModel model = new LandmarkModel();
                return View(model);
            }
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
            _dictionaryService.SaveLandmark(landmarkName, landmarkTypeId, hasEntryTicket, visitIntervalId,
             ticketId, streetName, streetNumber, cityId, latitude, longitude, landmarkId);

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
                        var a = Guid.NewGuid().ToString();
                        var fileName = Path.Combine(_he.WebRootPath, a + Path.GetExtension(formFile.FileName));
                        formFile.CopyTo(new FileStream(fileName, FileMode.Create));
                        imagesPaths.Add(a + Path.GetExtension(formFile.FileName));
                    }
                }
            }
        }

        public void SaveImagesDB()
        {
            foreach (string path in imagesPaths)
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

        public ActionResult EditLandmark(string landmarkName, int landmarkTypeId, bool hasEntryTicket, int visitIntervalId,
            int ticketId, string streetName, int streetNumber, int cityId, float latitude, float longitude, int landmarkId)
        {
            Landmark newLandmark = new Landmark();
            newLandmark.LandmarkDescription = landmarkName;
            newLandmark.LandmarkTypeId = landmarkTypeId;
            newLandmark.HasEntryTicket = hasEntryTicket;
            newLandmark.VisitIntervalId = visitIntervalId;
            newLandmark.TicketId = ticketId;
            newLandmark.StreetName = streetName;
            newLandmark.StreetNumber = streetNumber;
            newLandmark.CityId = cityId;
            newLandmark.Latitude = latitude;
            newLandmark.Longitude = longitude;
            newLandmark.LandmarkId = landmarkId;

            ScarletWitchContext context = new ScarletWitchContext();
            context.Landmark.Update(newLandmark);
            return Json(context.SaveChanges());
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

        public void ExportFile([DataSourceRequest] DataSourceRequest request)
        {
            string destinationFile = Path.Combine(Environment.CurrentDirectory, "SampleDocument.docx");
            string sourceFile = Path.Combine(Environment.CurrentDirectory, "Sample.docx");
            try
            {


                System.IO.File.Copy(sourceFile, destinationFile, true);
                var logFile = System.IO.File.Create(System.IO.Path.GetTempFileName());
                using (WordprocessingDocument document = WordprocessingDocument.Open(destinationFile, true))
                {
                    document.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);

                    MainDocumentPart mainPart = document.MainDocumentPart;

                    DocumentSettingsPart documentSettingPart1 = mainPart.DocumentSettingsPart;

                    // Create a new attachedTemplate and specify a relationship ID
                    //AttachedTemplate attachedTemplate1 = new AttachedTemplate() { Id = "relationId1" };

                    // Append the attached template to the DocumentSettingsPart
                    //documentSettingPart1.Settings.Append(attachedTemplate1);

                    // Add an ExternalRelationShip of type AttachedTemplate.
                    // Specify the path of template and the relationship ID
                    //documentSettingPart1.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(sourceFile, UriKind.Absolute), "relationId1");

                    // Save the document
                    var logWriter = new System.IO.StreamWriter(logFile);
                    mainPart.Document.Save();

                    Console.WriteLine("Document generated at " + destinationFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("\nPress Enter to continue…");
                Console.ReadLine();
            }
        }

        public string parseXMLBnr([DataSourceRequest] DataSourceRequest request)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("https://www.bnr.ro/nbrfxrates.xml");
            XmlNode node = doc.GetElementsByTagName("Rate")[10];

            DictionaryCurrency newCurrency = new DictionaryCurrency();
            newCurrency.CurrencyId = 1702472;
            newCurrency.CurrencyName = "EURO";
            newCurrency.CurrencyCode = "EUR";
            newCurrency.CurrencyExchange = Convert.ToDecimal(node.InnerText);
            ScarletWitchContext context = new ScarletWitchContext();

            context.DictionaryCurrency.Update(newCurrency);
            context.SaveChanges();

            return node.InnerText;
        }
        public int Delete([DataSourceRequest] DataSourceRequest request, int id)
        {
            int sters = _dictionaryService.DeleteLandmark(id);
            return sters;
        }

        public bool VerifyLandmark([DataSourceRequest] DataSourceRequest request, String name, String street, int number, double lat, double longitud)
        {
            bool status = _dictionaryService.VerifyLandmark(name, street, number, lat, longitud);
            return status;
        }
    }
}

