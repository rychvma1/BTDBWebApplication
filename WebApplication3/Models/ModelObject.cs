using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class ModelObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string LastValue { get; set; }
        public List<ModelObject> Value { get; set; }

        public ModelObject() { }

        public ModelObject(string name)
        {
            Name = name;
            Type = null;
            Value = new List<ModelObject>();
            LastValue = null;
        }

        public ModelObject(string type, string name)
        {
            Name = name;
            Type = type;
            Value = new List<ModelObject>();
            LastValue = null;
        }
    }
}
