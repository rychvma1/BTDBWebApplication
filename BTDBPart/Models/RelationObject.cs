using System.Collections.Generic;

namespace BTDBPart.Models
{
    public class RelationObject
    {
        public BaseObject BaseObj { get; set; }
        public List<ModelObject> ModelObjects { get; set; }

        public RelationObject()
        {
            BaseObj = new BaseObject();
            ModelObjects = new List<ModelObject>();
        }
    }
}
