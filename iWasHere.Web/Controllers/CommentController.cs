using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using iWasHere.Domain.Models;
using iWasHere.Domain.DTOs;

namespace iWasHere.Web.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
           
        }

        public IActionResult SaveCommand(string landmarkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ScarletWitchContext gf = new ScarletWitchContext();
            gf.Review.Add(new Review
            {
                //LandmarkId = 
                //LandmarkTypeCode = landmarkCode,
                //Description = description


            });

            return Json(gf.SaveChanges());

        }






    }
}