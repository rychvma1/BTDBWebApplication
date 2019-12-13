using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface ICustomDataServices
    {
        List<ModelObject> IterateDb();
    }
}
