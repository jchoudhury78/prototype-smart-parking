using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartParking.Domain;
using SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartParking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Search search = new Search();
            return View(search);
        }
        [HttpPost]
        public void Search(Search search)
        {

            using(var client= new HttpClient())
            {
                //client.BaseAddress = new Uri();
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
