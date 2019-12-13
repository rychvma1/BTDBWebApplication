using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.v4
{
    public class BtdbObject
    {
        public BaseObjectV4 BaseObj { get; set; }
        public List<RelationObject> RelationObjects { get; set; }
        public List<SingletonObject> SingletonObjects { get; set; }
    }
}
