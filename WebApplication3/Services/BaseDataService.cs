using System.IO;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using Newtonsoft.Json;
using WebApplication3.Models;
using WebApplication3.Services.Visitors;

namespace WebApplication3.Services
{
    public class BaseDataService : IBaseDataService
    {
        private IObjectDB TempDb { get; }

        public BaseDataService(IObjectDB tempDb)
        {
            TempDb = tempDb;
        }

        public BtdbObject IterateDB()
        {
            IKeyValueDB kvDb;
            BtdbObject btdbObject;

            var json = File.ReadAllText(@"App_Data\settings.json");
            var settings = JsonConvert.DeserializeObject<Settings>(json);

            using (var d = new OnDiskFileCollection(settings.DirPath))
            {
                kvDb = new KeyValueDB(d);
               TempDb.Open(kvDb, false);

                using (var tr = TempDb.StartReadOnlyTransaction())
                {
                    var visitor = new BaseDataVisitor();
                    var iterator = new ODBIterator(tr, visitor);
                    iterator.Iterate();
                    btdbObject = visitor.BtdbObject;
                }
            }

            return btdbObject;
        }
    }
}