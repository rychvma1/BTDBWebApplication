using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class DataController : Controller
    {
        private readonly IBaseDataService _baseDataService;
        private readonly ISpecificSingletonDataService _specificSingletonDataService;
        private readonly ISpecificRelationDataService _specificRelationDataService;
        private readonly PhysicalFileProvider _provider;
        private List<ModelObject> List { get; set; }
        private const int ItemsOnPage = 50;

        public DataController(
            IBaseDataService baseDataService,
            ISpecificSingletonDataService specificSingletonDataService,
            ISpecificRelationDataService specificRelationDataService)
        {
            _baseDataService = baseDataService;
            _specificSingletonDataService = specificSingletonDataService;
            _specificRelationDataService = specificRelationDataService;
            _provider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
        }

        public IActionResult Index()
        {
            return View(GetBaseObjects());
        }

        public IActionResult DetailData(int? page, string path)
        {
            ViewBag.Path = path;
            var model = FindModelObject(path);

            var pg = page ?? 1;
            var totalItems = model.Values.Count;
            ViewBag.Pages = (int) Math.Ceiling((double) totalItems / ItemsOnPage);
            ViewBag.CurrentPage = pg;

            if (model.Type != "InlineObject" && model.Type != "relValue")
            {
                var x = (pg - 1) * ItemsOnPage;
                if ((model.Values.Count - x) < ItemsOnPage)
                {
                    model.Values = model.Values.GetRange(x, model.Values.Count - x);
                }
                else
                {
                    model.Values = model.Values.GetRange(x, ItemsOnPage);
                }
            }

            return View(model);
        }

        public IActionResult SpecificData(int? page, string name, string type)
        {
            ViewBag.Name = name;
            ViewBag.Type = type;

            List = GetSpecificData(name, type);
            var pg = page ?? 1;
            var totalItems = List.Count;
            ViewBag.Pages = (int) Math.Ceiling((double) totalItems / ItemsOnPage);
            ViewBag.CurrentPage = pg;
            if (List.Count > 1)
            {
                var x = (pg - 1) * ItemsOnPage;
                if ((List.Count - x) < ItemsOnPage)
                {
                    List = List.GetRange(x, List.Count - x);
                }
                else
                {
                    List = List.GetRange(x, ItemsOnPage);
                }
            }

            return View(List);
        }

        public IActionResult DownloadJson()
        {
            var fileInfo = _provider.GetFileInfo("JSON.json");
            var filePath = fileInfo.PhysicalPath;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/json", "JSON.json");
        }

        public JsonResult GetJson(string name, string type)
        {
            if (type.Equals("singleton"))
            {
                List = GetSingletonObject(name).ModelObjects;
            }
            else if (type.Equals("relation"))
            {
                List = GetRelationObject(name).ModelObjects;
            }

            return Json(List);
        }

        private List<ModelObject> GetDataFromJson()
        {
            var fileInfo = _provider.GetFileInfo("JSON.json");
            var filePath = fileInfo.PhysicalPath;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var str = System.Text.Encoding.Default.GetString(fileBytes);
            return JsonConvert.DeserializeObject<List<ModelObject>>(str);
        }

        private BtdbObject GetBtdbObject()
        {
            return _baseDataService.IterateDB();
        }

        private List<BaseObject> GetBaseObjects()
        {
            var list = new List<BaseObject>();

            var btdbObject = GetBtdbObject();

            list.AddRange(btdbObject.SingletonObjects.Select(singletonObject => singletonObject.BaseObj));
            list.AddRange(btdbObject.RelationObjects.Select(relationObject => relationObject.BaseObj));

            return list;
        }

        private SingletonObject GetSingletonObject(string name)
        {
            return _specificSingletonDataService.IterateDB(name);
        }

        private RelationObject GetRelationObject(string name)
        {
            return _specificRelationDataService.IterateDB(name);
        }

        private List<ModelObject> GetSpecificData(string name, string type)
        {
            if (type.Equals("singleton"))
            {
                List = GetSingletonObject(name).ModelObjects;
                var json = JsonConvert.SerializeObject(List);
                System.IO.File.WriteAllText(_provider.Root + "JSON.json", json);
            }
            else if (type.Equals("relation"))
            {
                List = GetRelationObject(name).ModelObjects;
                var json = JsonConvert.SerializeObject(List);
                System.IO.File.WriteAllText(_provider.Root + "JSON.json", json);

                List.ForEach(o =>
                {
                    if (o.IsRelKey == true)
                    {
                        o.Name += " (relKey)";
                    }
                });
            }

            return List;
        }

        private ModelObject FindModelObject(string path)
        {
            var objNames = path.Split("/").ToList();
            List = GetDataFromJson();
            var model = List.Find(x => x.Name.Contains(objNames[0]));
            if (objNames.Count > 1)
            {
                for (var i = 1; i < objNames.Count; i++)
                {
                    model = model.Values.Find(x => x.Name.Contains(objNames[i]));
                }
            }

            return model;
        }
    }
}