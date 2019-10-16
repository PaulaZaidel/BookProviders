using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookProviders.App.ViewModels;

namespace BookProviders.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var model = new ErrorViewModel();

            if (id == 500)
            {
                model.Message = "Error 500";
                model.Title = "Something is wrong :(";
                model.ErrorCode = id;
            }
            else if (id == 404)
            {
                model.Message = "Error 404 - Page not found!";
                model.Title = "Something is wrong :(";
                model.ErrorCode = id;
            }
            else if (id == 403)
            {
                model.Message = "Error 403 - You does not have permission!";
                model.Title = "Something is wrong :(";
                model.ErrorCode = id;
            } else
            {
                return StatusCode(404);
            } 
            return View("Error", model);
        }
    }
}
