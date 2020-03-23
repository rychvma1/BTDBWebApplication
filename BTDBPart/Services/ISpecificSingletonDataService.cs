using BTDBPart.Models;

namespace BTDBPart.Services
{
    public interface ISpecificSingletonDataService
    {
        SingletonObject IterateDB(string name);
    }
}