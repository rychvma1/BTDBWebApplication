using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class BtdbObject
    {
        public BaseObject BaseObj { get; set; }
        public List<RelationObject> RelationObjects { get; set; }
        public List<SingletonObject> SingletonObjects { get; set; }
    }
}
