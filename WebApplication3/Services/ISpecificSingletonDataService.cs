using WebApplication3.Models.v4;

namespace WebApplication3.Services
{
    public interface ISpecificSingletonDataService
    {
        SingletonObject IterateDB(string name);
    }
}