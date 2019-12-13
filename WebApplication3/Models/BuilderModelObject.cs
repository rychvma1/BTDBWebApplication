using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class BuilderModelObject
    {
        // public Stack<ModelObj> ModelObjStack = new Stack<ModelObj>();
        public List<ModelObject> Objs = new List<ModelObject>();
        // private string type, lastValue;

        public BuilderModelObject(ModelObject value)
        {
            //ModelObjStack.Push(value);
            WriteVal(value);
        }

        public void WriteVal(ModelObject val)
        {
            // ModelObjStack.Push(val);
            Objs.Add(val);
        }

        public void Down(ModelObject val)
        {
            //ModelObjStack.Push(val);
            Objs.Add(val);
            //type = val.Type;
        }

        public void Up()
        {
            // ModelObj temp = ModelObjStack.Pop();

            //ModelObjStack.Peek() = temp;
            ModelObject temp = Objs[Objs.Count - 1];

            Objs.RemoveAt(Objs.Count - 1);

            //if (Objs.Count >= 1)
            //{
            //    Objs[Objs.Count - 1].Value.Add(temp);
            //}

            Objs[Objs.Count - 1].Value.Add(temp);
            //type = "";
        }

        public void WriteName(string name)
        {
            Objs[Objs.Count - 1].Name = name;
        }

        public void WriteLastValue(string val)
        {
            Objs[Objs.Count - 1].LastValue = val;
        }


        public void WriteType(string type)
        {
            Objs[Objs.Count - 1].Type = type;
        }

    }
}
