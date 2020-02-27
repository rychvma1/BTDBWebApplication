using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using WebApplication3.Models.v4;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class DataController : Controller
    {
        private readonly IBaseDataService _baseDataService;
        private readonly ISpecificSingletonDataService _specificSingletonDataService;
        private readonly ISpecificRelationDataService _specificRelationDataService;
        private int _pageCount;
        private readonly PhysicalFileProvider _provider;
        private List<ModelObjectv4> List { get; set; }

        public DataController(IBaseDataService baseDataService,
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

        [HttpPost]
        public IActionResult SpecificData(int? page, string name, string type)
        {
            ViewBag.Name = name;
            ViewBag.Type = type;

//            var itemsOnPage = 1;
//            var pg = page.HasValue ? page.Value : 1;
//            var baseList = GetBaseObjectsPaged(pg, itemsOnPage, out var totalCount);

//            ViewBag.Pages = (int) Math.Ceiling((double) totalCount / (double) itemsOnPage);
//            ViewBag.CurrentPage = page;

            return View(GetSpecificData(name, type));
        }

        private BtdbObject GetBtdbObject()
        {
            return _baseDataService.IterateDB();
        }

        private List<BaseObjectV4> GetBaseObjects()
        {
            var list = new List<BaseObjectV4>();

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

        private List<ModelObjectv4> GetSpecificData(string name, string type)
        {
            _pageCount = 0;

            if (type.Equals("singleton"))
            {
                List = GetSingletonObject(name).ModelObjects;
                var json = JsonConvert.SerializeObject(List);
                System.IO.File.WriteAllText(_provider.Root + "JSON.json", json);

                List.ForEach(o =>
                {
                    if (o.Name.Equals("DictionaryKey"))
                    {
                        _pageCount++;
                    }
                });
            }
            else if (type.Equals("relation"))
            {
                List = GetRelationObject(name).ModelObjects;
                var json = JsonConvert.SerializeObject(List);
                System.IO.File.WriteAllText(_provider.Root + "JSON.json", json);

                List.ForEach(o =>
                {
                    if (o.RelKey == true)
                    {
                        _pageCount++;
                        o.Name += " (relKey)";
                    }

                    if (o.Type.Equals("List"))
                    {
                        
                    }
                });
            }

            return List;
        }

        private List<ModelObjectv4> GetSpecificDataPaged(string name, string type, int pg, int count,
            out int totalCount)
        {
            var dic = new Dictionary<int, List<ModelObjectv4>>();
            var list = GetSpecificData(name, type);
            totalCount = list.Count;

            foreach (var o in list)
            {
                if (o.Name.Equals("DictionaryKey"))
                {
                }
            }

            return list;
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

            var json = List;
            return Json(json);
        }
    }
}