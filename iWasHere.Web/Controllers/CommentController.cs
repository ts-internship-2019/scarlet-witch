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


        public IActionResult AddComment(string id)
        {
            ReviewModel rm = new ReviewModel();
            rm.LandmarkId = Convert.ToInt32(id);
            return View(rm);

        }

        public IActionResult SaveComment(int id, int currentValue, bool isLogged, string userName, string title, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userN = User.FindFirstValue(ClaimTypes.Name);
            ScarletWitchContext gf = new ScarletWitchContext();

            if (isLogged == true)
            {
                gf.Review.Add(new Review
                {
                    LandmarkId = id,
                    Review1= description,
                    Title = title,
                    Grade = currentValue,
                    UserId = userId,
                    UserName = userN

                });
            }
            else
            {
                gf.Review.Add(new Review
                {
                    LandmarkId = id,
                    Review1 = description,
                    Title = title,
                    Grade = currentValue,
                    UserId = null,
                    UserName = userName

                });
            }
            

            return Json(gf.SaveChanges());

        }






    }
}