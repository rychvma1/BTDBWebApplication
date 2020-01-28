using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.v4;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class VisitorController : Controller
    {
        private readonly ICustomVisitor _services;

        public VisitorController(ICustomVisitor services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View(GetBaseObjects());
        }

        public IActionResult SpecificData(string name, string type)
        {
            ViewBag.Name = name;
            ViewBag.Type = type;
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

            var btdbObject = GetBtdbObject();
            if (btdbObject.RelationObjects.Count >= 1)
            {
                foreach (var relationObject in btdbObject.RelationObjects.Where(relationObject => relationObject.BaseObj.Name.Equals(name) && relationObject.BaseObj.Type.Equals(type)))
                {
                    relationObject.ModelObjects.ForEach(o => list.Add(o));
                }
            }

            if (btdbObject.SingletonObjects.Count >= 1)
            {
                foreach (var singletonObject in btdbObject.SingletonObjects.Where(singletonObject => singletonObject.BaseObj.Name.Equals(name) && singletonObject.BaseObj.Type.Equals(type)))
                {
                    singletonObject.ModelObjects.ForEach(o => list.Add(o));
                }
            }
                
            return list;
        }
        
    }
}