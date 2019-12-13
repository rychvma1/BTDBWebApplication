using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.v4
{
    public class RelationObject
    {
        public BaseObjectV4 BaseObj { get; set; }
        public List<ModelObjectv4> ModelObjects { get; set; }

        public RelationObject()
        {
            BaseObj = new BaseObjectV4();
            ModelObjects = new List<ModelObjectv4>();
        }
    }
}
