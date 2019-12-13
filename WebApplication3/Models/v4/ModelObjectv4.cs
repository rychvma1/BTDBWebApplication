using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.v4
{
    public enum InlineObjectType
    {
        Dictionary,
        List,
        Null
    }

    public class ModelObjectv4 : BaseObjectV4
    {
        public string Value { get; set; }
        public string InlineObjectName { get; set; }
        public InlineObjectType InlineObjectType { get; set; }
        public string Info { get; set; }
    }
}