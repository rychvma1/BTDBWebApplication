using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.v4
{
    public class SingletonObject
    {
        public BaseObjectV4 BaseObj { get; set; }
        public List<ModelObjectv4> ModelObjects { get; set; }

        public SingletonObject()
        {
            BaseObj = new BaseObjectV4();
            ModelObjects = new List<ModelObjectv4>();
        }
    }
}
