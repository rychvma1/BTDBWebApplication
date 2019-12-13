using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class TableController : Controller
    {
        private readonly ICustomDataServices _services;

        public TableController(ICustomDataServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            var baseObjects = getBaseObjects();

//            if (baseObjects == null)
//            {
//                return View();
//            }

            return View(baseObjects);
        }

        public IActionResult SpecificData(string name, string type)
        {
            var modelObject = getModelObject(name, type);

//            if (type.Equals("relation"))
//            {
//                foreach (var obj in modelObject.Value)
//                {
//                    if (obj.Type.Equals("relKey"))
//                    {
//
//                    }
//                }
//            }

            ViewBag.Name = name;
            ViewBag.Type = type;
            return View(modelObject);
        }

        private List<ModelObject> getDataFromBTDB()
        {
            return _services.IterateDb();
        }

        private List<BaseObject> getBaseObjects()
        {
//            var baseObjs = new List<BaseObject>();
//            getDataFromBTDB()[0].Value.ForEach(o =>
//            {
//                baseObjs.Add(new BaseObject { Name = o.Name, Type = o.Type });
//            });
//            return baseObjs;
            var baseObjects = new List<BaseObject>();
            getDataFromBTDB()[0].Value.ForEach(o =>
            {
                baseObjects.Add(new BaseObject {Name = o.Name, Type = o.Type});
            });
            return baseObjects;
        }

        private ModelObject getModelObject(string name, string type)
        {
            var correctObj = new ModelObject();
            foreach (var baseObj in getDataFromBTDB()[0].Value.Where(obj => obj.Name == name && obj.Type == type))
            {
                correctObj = baseObj;
            }

            return correctObj;
        }

        private List<ModelObject> getRelKeys(ModelObject model)
        {
           var listOfRelKeys = new List<ModelObject>();

           foreach (var keys in model.Value)
           {
               if (keys.Type.Equals("relKey"))
               {
                    listOfRelKeys.Add(keys.Value[0]);
               }
           }

           return listOfRelKeys;
        }

        private List<ModelObject> getRelValues(ModelObject model)
        {
            var listOfRelValues = new List<ModelObject>();

            foreach (var value in model.Value)
            {
                if (value.Type.Equals("relValue"))
                {
//                    value.Value.ForEach(v => v.Value);
//                    listOfRelValues.Add();
                }
            }

            return listOfRelValues;
        }
    }
}