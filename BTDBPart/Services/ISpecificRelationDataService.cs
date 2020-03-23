using BTDBPart.Models;

namespace BTDBPart.Services
{
    public interface ISpecificRelationDataService
    {
        RelationObject IterateDB(string name);
    }
}