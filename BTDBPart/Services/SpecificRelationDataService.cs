using System.IO;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using BTDBPart.Models;
using BTDBPart.Services.Visitors;
using Newtonsoft.Json;

namespace BTDBPart.Services
{
    public class SpecificRelationDataService : ISpecificRelationDataService
    {
        private IObjectDB TempDb { get; }


        public SpecificRelationDataService(IObjectDB tempDb)
        {
            TempDb = tempDb;
        }

        public RelationObject IterateDB(string name)
        {
            IKeyValueDB kvDb;
            RelationObject relationObject = new RelationObject();

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
                            relationObject.BaseObj.Name = o.Name;
                            relationObject.BaseObj.Type = o.Type;
                            relationObject.ModelObjects = o.Values;
                        }
                    });
                }
            }


            return relationObject;
        }
    }
}