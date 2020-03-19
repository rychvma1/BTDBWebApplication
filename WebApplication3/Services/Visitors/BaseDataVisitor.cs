using System.Collections.Generic;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using WebApplication3.Models;

namespace WebApplication3.Services.Visitors
{
    public class BaseDataVisitor : IODBVisitor
        {
            private static RelationObject _tempRelationObject = new RelationObject();
            private static SingletonObject _tempSingletonObject = new SingletonObject();
            private static string _tempName = "";

            public BtdbObject BtdbObject = new BtdbObject
            {
                BaseObj = new BaseObject {Name = "BaseObject", Type = "BaseObject"},
                RelationObjects = new List<RelationObject>(),
                SingletonObjects = new List<SingletonObject>()
            };

            public bool VisitSingleton(uint tableId, string tableName, ulong oid)
            {
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
                return true;
            }

            public bool NeedScalarAsObject()
            {
                return true;
            }

            public void ScalarAsObject(object content)
            {
            }

            public bool NeedScalarAsText()
            {
                return true;
            }

            public void ScalarAsText(string content)
            {
            }

            public void OidReference(ulong oid)
            {
            }

            public bool StartInlineObject(uint tableId, string tableName, uint version)
            {
                return true;
            }

            public void EndInlineObject()
            {
            }

            public bool StartList()
            {
                return true;
            }

            public bool StartItem()
            {
                return true;
            }

            public void EndItem()
            {
            }

            public void EndList()
            {
            }

            public bool StartDictionary()
            {
                return true;
            }

            public bool StartDictKey()
            {
                return true;
            }

            public void EndDictKey()
            {
            }

            public bool StartDictValue()
            {
                return true;
            }

            public void EndDictValue()
            {
            }

            public void EndDictionary()
            {
            }

            public void EndField()
            {
            }

            public void EndObject()
            {
                BtdbObject.SingletonObjects.Add(_tempSingletonObject);
                _tempSingletonObject = new SingletonObject();
            }

            public bool StartRelation(string relationName)
            {
                _tempRelationObject = new RelationObject();
                _tempRelationObject.BaseObj.Name = relationName;
                _tempRelationObject.BaseObj.Type = "relation";
                return true;
            }

            public bool StartRelationKey()
            {
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