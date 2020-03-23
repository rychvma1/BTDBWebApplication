using System.Collections.Generic;

namespace BTDBPart.Models
{
    public class BtdbObject
    {
        public BaseObject BaseObj { get; set; }
        public List<RelationObject> RelationObjects { get; set; }
        public List<SingletonObject> SingletonObjects { get; set; }
    }
}
