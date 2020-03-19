using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class ModelObject : BaseObject
    {
        public string Value { get; set; }
        public bool? IsRelKey { get; set; }
        public List<ModelObject> Values { get; set; }

        public ModelObject()
        {
        }

        public ModelObject(string name)
        {
            Name = name;
            Type = null;
            Values = new List<ModelObject>();
            Value = null;
        }
        
        public ModelObject(string name, string type)
        {
            Name = name;
            Type = type;
            Values = new List<ModelObject>();
            Value = null;
        }
        
        public ModelObject(string name, string type,  bool isRelKey)
        {
            Name = name;
            Type = type;
            Values = new List<ModelObject>();
            Value = null;
            IsRelKey = isRelKey;
        }
    }

    public class BuilderModelObjectv4
    {
        public List<ModelObject> Objs = new List<ModelObject>();

        public BuilderModelObjectv4(ModelObject value)
        {
            WriteValue(value);
        }

        private void WriteValue(ModelObject value)
        {
            Objs.Add(value);
        }

        public void Down(ModelObject value)
        {
            Objs.Add(value);
        }

        public void Up()
        {
            var temp = Objs[Objs.Count - 1];
            Objs.RemoveAt(Objs.Count - 1);
            Objs[Objs.Count - 1].Values.Add(temp);
        }
        
        public void WriteName(string name)
        {
            Objs[Objs.Count - 1].Name = name;
        }

        public void WriteLastValue(string val)
        {
            Objs[Objs.Count - 1].Value = val;
        }


        public void WriteType(string type)
        {
            Objs[Objs.Count - 1].Type = type;
        }
    }
}