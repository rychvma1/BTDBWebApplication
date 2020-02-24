using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.v4;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class VisitorController : Controller
    {
        private readonly ICustomVisitor _services;
        private int _pageCount;

        public VisitorController(ICustomVisitor services)
        {
            _services = services;
        }

        public IActionResult Index(int? page)
        {
            var itemsOnPage = 5;
            var pg = page.HasValue ? page.Value : 1;
            var baseList = GetBaseObjectsPaged(pg, itemsOnPage, out var totalCount);

            ViewBag.Pages = (int) Math.Ceiling((double) totalCount / (double) itemsOnPage);
            ViewBag.CurrentPage = page;
            return View(baseList);
        }

        [HttpPost]
        public IActionResult SpecificData(int? page)
        {
            var name = Request.Form["Name"];
            var type = Request.Form["Type"];
            ViewBag.Name = name;
            ViewBag.Type = type;
            
            var itemsOnPage = 1;
            var pg = page.HasValue ? page.Value : 1;
            var baseList = GetBaseObjectsPaged(pg, itemsOnPage, out var totalCount);

            ViewBag.Pages = (int) Math.Ceiling((double) totalCount / (double) itemsOnPage);
            ViewBag.CurrentPage = page;
            
            return View(GetSpecificData(name, type));
        }

        private BtdbObject GetBtdbObject()
        {
            return _services.IterateDB();
        }

        private List<BaseObjectV4> GetBaseObjects()
        {
            var list = new List<BaseObjectV4>();

            var btdbObject = GetBtdbObject();

            list.AddRange(btdbObject.SingletonObjects.Select(singletonObject => singletonObject.BaseObj));
            list.AddRange(btdbObject.RelationObjects.Select(relationObject => relationObject.BaseObj));

            return list;
        }

        private List<ModelObjectv4> GetSpecificData(string name, string type)
        {
            var list = new List<ModelObjectv4>();
            _pageCount = 0;

            var btdbObject = GetBtdbObject();
            if (btdbObject.RelationObjects.Count >= 1)
            {
                foreach (var relationObject in btdbObject.RelationObjects.Where(relationObject =>
                    relationObject.BaseObj.Name.Equals(name) && relationObject.BaseObj.Type.Equals(type)))
                {
                    relationObject.ModelObjects.ForEach(o => list.Add(o));
                    list.ForEach(o =>
                    {
                        if (o.RelKey == true)
                        {
                            _pageCount++;
                        }
                    });
                }
            }

            if (btdbObject.SingletonObjects.Count >= 1)
            {
                foreach (var singletonObject in btdbObject.SingletonObjects.Where(singletonObject =>
                    singletonObject.BaseObj.Name.Equals(name) && singletonObject.BaseObj.Type.Equals(type)))
                {
                    singletonObject.ModelObjects.ForEach(o => list.Add(o));
                    list.ForEach(o =>
                    {
                        if (o.Name.Equals("DictionaryKey"))
                        {
                            _pageCount++;
                        }
                    });
                }
            }

            return list;
        }

        private List<BaseObjectV4> GetBaseObjectsPaged(int pg, int count, out int totalCount)
        {
            var list = GetBaseObjects();
            totalCount = list.Count;

            if( (count*2) < totalCount || pg == 1)
            {
                return list.GetRange((pg - 1) * count, count);
            }
            else
            {
                return list.GetRange((pg - 1) * count, totalCount - count); 
            }

        }

        private List<ModelObjectv4> GetSpecificDataPaged(string name, string type, int pg, int count, out int totalCount)
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
    }
}