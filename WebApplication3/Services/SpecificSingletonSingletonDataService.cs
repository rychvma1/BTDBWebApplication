using System.IO;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using Newtonsoft.Json;
using WebApplication3.Models;
using WebApplication3.Services.Visitors;

namespace WebApplication3.Services
{
    public class SpecificSingletonSingletonDataService : ISpecificSingletonDataService
    {
        private IObjectDB TempDb { get; }


        public SpecificSingletonSingletonDataService(IObjectDB tempDb)
        {
            TempDb = tempDb;
        }

        public SingletonObject IterateDB(string name)
        {
            IKeyValueDB kvDb;
            SingletonObject singletonObject = new SingletonObject();

            var json = File.ReadAllText(@"App_Data\settings.json");
            var settings = JsonConvert.DeserializeObject<Settings>(json);

            using (var d = new OnDiskFileCollection(settings.DirPath))
            {
                kvDb = new KeyValueDB(d);
                TempDb.Open(kvDb, false);

                using (var tr = TempDb.StartReadOnlyTransaction())
                {
                    var visitor = new Visitor();
                    var iterator = new ODBIterator(tr, visitor);
                    iterator.Iterate();
                    visitor.Builder.Objs.ForEach(o =>
                    {
                        if (o.Name.Equals(name))
                        {
                            singletonObject.BaseObj.Name = o.Name;
                            singletonObject.BaseObj.Type = o.Type;
                            singletonObject.ModelObjects = o.Values;
                        }
                    });
                }
            }

            return singletonObject;
        }

    }
}