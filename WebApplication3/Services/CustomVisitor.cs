using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using WebApplication3.Models;
using WebApplication3.Models.v4;

namespace WebApplication3.Services
{
    public class CustomVisitor : ICustomVisitor
    {
        private IObjectDB TempDb { get; }


        public CustomVisitor(IObjectDB tempDb)
        {
            this.TempDb = tempDb;
        }

        public BtdbObject IterateDB()
        {
            BtdbObject btdbObject = new BtdbObject();
            using (var tr = TempDb.StartTransaction())
            {
                var visitor = new ToDTOVisitor();
                var iterator = new ODBIterator(tr, visitor);
                iterator.Iterate();
                btdbObject = visitor.BtdbObject;
            }

            return btdbObject;
        }

        internal class ToDTOVisitor : IODBVisitor
        {
            private static RelationObject _tempRelationObject = new RelationObject();
            private static SingletonObject _tempSingletonObject = new SingletonObject();
            private static ModelObjectv4 _tempModelObject = new ModelObjectv4();
            private static string _tempName = "";

            public BtdbObject BtdbObject = new BtdbObject
            {
                BaseObj = new BaseObjectV4 {Name = "BaseObject", Type = "BaseObject"},
                RelationObjects = new List<RelationObject>(),
                SingletonObjects = new List<SingletonObject>()
            };

            public bool VisitSingleton(uint tableId, string tableName, ulong oid)
            {
                _tempModelObject = new ModelObjectv4();
                _tempSingletonObject = new SingletonObject();
                _tempSingletonObject.BaseObj.Name = tableName;
                _tempSingletonObject.BaseObj.Type = "singleton";
                return true;
            }

            public bool StartObject(ulong oid, uint tableId, string tableName, uint version)
            {
                return true;
            }

            public bool StartField(string name)
            {
                _tempName = name;
                _tempModelObject.Name = name;
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
                    _tempModelObject.Type = string.Format(CultureInfo.InvariantCulture, "{0}", content.GetType());
                }
            }

            public bool NeedScalarAsText()
            {
                return true;
            }

            public void ScalarAsText(string content)
            {
                _tempModelObject.Value = content;
                _tempSingletonObject.ModelObjects.Add(_tempModelObject);
                _tempRelationObject.ModelObjects.Add(_tempModelObject);
            }

            public void OidReference(ulong oid)
            {
            }

            public bool StartInlineObject(uint tableId, string tableName, uint version)
            {
                _tempModelObject.Type = "InlineObject";
                _tempModelObject.Name = tableName;
                _tempSingletonObject.ModelObjects.Add(_tempModelObject);
                _tempModelObject = new ModelObjectv4();
                return true;
            }

            public void EndInlineObject()
            {
            }

            public bool StartList()
            {
                _tempModelObject.Type = "List";
                _tempModelObject.Name = _tempName;
                _tempSingletonObject.ModelObjects.Add(_tempModelObject);
                _tempModelObject = new ModelObjectv4();
                return true;
            }

            public bool StartItem()
            {
                _tempModelObject.Name = "ListItem";
                return true;
            }

            public void EndItem()
            {
                _tempModelObject = new ModelObjectv4();
            }

            public void EndList()
            {
            }

            public bool StartDictionary()
            {
                _tempModelObject.Type = "Dictionary";
                _tempModelObject.Name = _tempName;
                _tempSingletonObject.ModelObjects.Add(_tempModelObject);
                _tempModelObject = new ModelObjectv4();
                return true;
            }

            public bool StartDictKey()
            {
                _tempModelObject.Name = "DictionaryKey";
                return true;
            }

            public void EndDictKey()
            {
                _tempModelObject = new ModelObjectv4();
            }

            public bool StartDictValue()
            {
                _tempModelObject.Name = "DictionaryValue";
                return true;
            }

            public void EndDictValue()
            {
                _tempModelObject = new ModelObjectv4();
            }

            public void EndDictionary()
            {
            }

            public void EndField()
            {
                _tempModelObject = new ModelObjectv4();
            }

            public void EndObject()
            {
                BtdbObject.SingletonObjects.Add(_tempSingletonObject);
                _tempSingletonObject = new SingletonObject();
            }

            public bool StartRelation(string relationName)
            {
                _tempModelObject = new ModelObjectv4();
                _tempRelationObject = new RelationObject();
                _tempRelationObject.BaseObj.Name = relationName;
                _tempRelationObject.BaseObj.Type = "relation";
                return true;
            }

            public bool StartRelationKey()
            {
                _tempModelObject.RelKey = true;
                return true;
            }

            public void EndRelationKey()
            {
            }

            public bool StartRelationValue()
            {
                return true;
            }

            public void EndRelationValue()
            {
            }

            public void EndRelation()
            {
                BtdbObject.RelationObjects.Add(_tempRelationObject);
                _tempRelationObject = new RelationObject();
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
}