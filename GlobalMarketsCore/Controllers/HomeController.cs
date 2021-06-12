using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GlobalMarketsCore.Models;
using System.Xml;
using GlobalMarketsCore.Services;
using GlobalMarketsCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.IO;
using GlobalMarketsCore.AppHelper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarketsCore.Controllers
{

    public class HomeController : Controller
    {


        public IActionResult Index(string lang)
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
