using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISettingsManager _settingsManager;
        private readonly Settings _settings;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration, ISettingsManager settingsManager, Settings settings)
        {
            _logger = logger;
            _configuration = configuration;
            _settingsManager = settingsManager;
            _settings = settings;
        }

        public IActionResult Index()
        {
            ViewBag.Path = _settings.DirPath;
            return View();
        }

        [HttpPost]
        public IActionResult AddPath(Settings settings)
        {
            if (ModelState.IsValid)
            {
                _settingsManager.Update(s => s.DirPath = settings.DirPath);
            }
            else
            {
                return View("Index", settings);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
