using System;
using System.IO;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using BTDBPart.Models;
using Newtonsoft.Json;
using WebApplication3.Services.Visitors;

namespace BTDBPart.Services
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
            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Wrong path to .trl files!");
                throw;
            }
            

            return btdbObject;
        }
    }
}