using System.Globalization;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using WebApplication3.Models;

namespace WebApplication3.Services.Visitors
{
    public class Visitor : IODBVisitor
    {
        public BuilderModelObjectv4 Builder =
            new BuilderModelObjectv4(new ModelObject {Name = "base", Type = "base"});

        private static string _tempName = "";
        private int a, b, c, d, e = 0;

        public bool VisitSingleton(uint tableId, string tableName, ulong oid)
        {
            Builder.Down(new ModelObject(tableName,"singleton"));
            return true;
        }

        public bool StartObject(ulong oid, uint tableId, string tableName, uint version)
        {
            return true;
        }

        public bool StartField(string name)
        {
            _tempName = name;
            return true;
        }

        public bool NeedScalarAsObject()
        {
            return true;
        }

        public void ScalarAsObject(object content)
        {
            if (content != null)
            {
                switch (Builder.Objs[Builder.Objs.Count - 1].Type)
                {
                    case null:
                        Builder.WriteName(_tempName);
                        Builder.WriteType(string.Format(CultureInfo.InvariantCulture, "{0}", content.GetType()));
                        break;
                    case "InlineObject":
                        Builder.Down(new ModelObject(_tempName));
                        Builder.WriteType(string.Format(CultureInfo.InvariantCulture, "{0}", content.GetType()));
                        break;
                    case "singleton":
                        Builder.Down(new ModelObject(_tempName));
                        Builder.WriteType(string.Format(CultureInfo.InvariantCulture, "{0}", content.GetType()));
                        break;
                    case "relValue":
                        Builder.Down(new ModelObject(_tempName));
                        Builder.WriteType(string.Format(CultureInfo.InvariantCulture, "{0}", content.GetType()));
                        break;
                    default:
                    {
                        Builder.WriteName(_tempName);
                        Builder.WriteType(string.Format(CultureInfo.InvariantCulture, "{0}", content.GetType()));
                        break;
                    }
                }
            }
            else
            {
                Builder.Down(new ModelObject(_tempName));
            }
        }

        public bool NeedScalarAsText()
        {
            return true;
        }

        public void ScalarAsText(string content)
        {
            Builder.WriteLastValue(content);
            Builder.Up();
        }

        public void OidReference(ulong oid)
        {
        }

        public bool StartInlineObject(uint tableId, string tableName, uint version)
        {
            // Builder.Down(new ModelObjectv4("InlineObject", tableName));
            Builder.WriteName(tableName + " " + e++);
            Builder.WriteType("InlineObject");
            return true;
        }

        public void EndInlineObject()
        {
            
            // Builder.Up();
            Builder.Up();
        }

        public bool StartList()
        {
            Builder.Down(new ModelObject(_tempName,"List"));
            return true;
        }

        public bool StartItem()
        {
            _tempName = "ListItem " + a++;
            Builder.Down(new ModelObject(_tempName, "ListItem"));
            return true;
        }

        public void EndItem()
        {
        }

        public void EndList()
        {
            Builder.Up();
        }

        public bool StartDictionary()
        {
            Builder.Down(new ModelObject(_tempName, "Dictionary"));
            return true;
        }

        public bool StartDictKey()
        {
            _tempName = "DictKey " + b++;
            Builder.Down(new ModelObject(_tempName, "DictKey"));
            return true;
        }

        public void EndDictKey()
        {
        }

        public bool StartDictValue()
        {
            _tempName = "DictValue " + c++;
            Builder.Down(new ModelObject(_tempName, "DictValue"));
            return true;
        }

        public void EndDictValue()
        {
        }

        public void EndDictionary()
        {
            Builder.Up();
        }

        public void EndField()
        {
        }

        public void EndObject()
        {
            a = 0;
            b = 0;
            c = 0;
            e = 0;
        }

        public bool StartRelation(string relationName)
        {
            Builder.Down(new ModelObject(relationName, "relation"));
            return true;
        }

        public bool StartRelationKey()
        {
            Builder.Down(new ModelObject("relKey", "relKey", true));
            return true;
        }

        public void EndRelationKey()
        {
        }

        public bool StartRelationValue()
        {
            Builder.Down(new ModelObject("relValue " + d++, "relValue"));
            return true;
        }

        public void EndRelationValue()
        {
            Builder.Up();
        }

        public void EndRelation()
        {
            d = 0;
        }

        public void InlineBackRef(int iid)
        {
        }

        public void InlineRef(int iid)
        {
        }

        public void MarkCurrentKeyAsUsed(IKeyValueDBTransaction tr)
        {
        }
    }
}