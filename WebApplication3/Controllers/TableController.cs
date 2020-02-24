using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class TableController : Controller
    {
        private readonly ICustomVisitor _services;
        private readonly PhysicalFileProvider _provider;

        public TableController(ICustomVisitor services)
        {
            _services = services;
            _provider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
        }

        public IActionResult Index()
        {
            var json = JsonConvert.SerializeObject(_services.IterateDB());
            System.IO.File.WriteAllText(_provider.Root + "JSON.json", json);
            return View();
        }

        public JsonResult GetJson()
        {
            var json = _services.IterateDB();
            return Json(json);
        }

        public IActionResult DownloadJson()
        {
            var fileInfo = _provider.GetFileInfo("JSON.json");
            var filePath = fileInfo.PhysicalPath;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/json", "JSON.json");
        }
    }
}