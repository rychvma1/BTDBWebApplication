using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class SingletonObject
    {
        public BaseObject BaseObj { get; set; }
        public List<ModelObject> ModelObjects { get; set; }

        public SingletonObject()
        {
            BaseObj = new BaseObject();
            ModelObjects = new List<ModelObject>();
        }
    }
}
