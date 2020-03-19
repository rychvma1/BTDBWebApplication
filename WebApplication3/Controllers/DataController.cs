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

        public IActionResult DetailData(int? page, string name, string type)
        {
            ViewBag.Name = name;
            ViewBag.Type = type;
            var m = Find(name, type);

            var itemsOnPage = 50;
            var pg = page ?? 1;
            var totalItems = m.Values.Count;
            ViewBag.Pages = (int) Math.Ceiling((double) totalItems / (double) itemsOnPage);
            ViewBag.CurrentPage = pg;
            
            if (type != "InlineObject" && type != "relValue")
            {
                var x = (pg - 1) * itemsOnPage;
                if ((m.Values.Count - x) < itemsOnPage)
                {
                    m.Values = m.Values.GetRange(x, m.Values.Count - x);
                }
                else
                {
                    m.Values = m.Values.GetRange(x, itemsOnPage);
                }
            }

            return View(m);
        }

        public IActionResult SpecificData(int? page, string name, string type)
        {
            ViewBag.Name = name;
            ViewBag.Type = type;

            List = GetSpecificData(name, type);
            var itemsOnPage = 50;
            var pg = page ?? 1;
            var totalItems = List.Count;
            ViewBag.Pages = (int) Math.Ceiling((double) totalItems / (double) itemsOnPage);
            ViewBag.CurrentPage = pg;
            if (List.Count > 1)
            {
                var x = (pg - 1) * itemsOnPage;
                if ((List.Count - x) < itemsOnPage)
                {
                    List = List.GetRange(x, List.Count - x);
                }
                else
                {
                    List = List.GetRange(x, itemsOnPage);
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

        private ModelObject Find(string name, string type)
        {
            ModelObject m = new ModelObject();
            List = GetDataFromJson();
            List.ForEach(a =>
            {
                if (a.Name == name && a.Type == type)
                {
                    m = a;
                }
                else
                {
                    a.Values.ForEach(b =>
                    {
                        if (b.Name == name && b.Type == type)
                        {
                            m = b;
                        }
                        else
                        {
                            b.Values.ForEach(c =>
                            {
                                if (c.Name == name && c.Type == type)
                                {
                                    m = c;
                                }
                                else
                                {
                                    c.Values.ForEach(d =>
                                    {
                                        if (d.Name == name && d.Type == type)
                                        {
                                            m = d;
                                        }
                                        else
                                        {
                                            d.Values.ForEach(e =>
                                            {
                                                if (e.Name == name && e.Type == type)
                                                {
                                                    m = e;
                                                }
                                                else
                                                {
                                                    e.Values.ForEach(f =>
                                                    {
                                                        if (f.Name == name && f.Type == type)
                                                        {
                                                            m = f;
                                                        }
                                                        else
                                                        {
                                                            f.Values.ForEach(g =>
                                                            {
                                                                if (g.Name == name && g.Type == type)
                                                                {
                                                                    m = g;
                                                                }
                                                            });
                                                        }
                                                    });
                                                }
                                            });
                                        }
                                    });
                                }
                            });
                        }
                    });
                }
            });
            return m;
        }
    }
}