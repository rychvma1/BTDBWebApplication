using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface ISpecificSingletonDataService
    {
        SingletonObject IterateDB(string name);
    }
}