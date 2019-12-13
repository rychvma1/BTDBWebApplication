using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class BaseObject
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public BaseObject()
        {
        }

        public BaseObject(string type, string name)
        {
            Name = name;
            Type = type;
        }
    }
}