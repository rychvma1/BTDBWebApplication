using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.v4;

namespace WebApplication3.Services
{
    public interface ICustomVisitor
    {
        BtdbObject IterateDB();
    }
}
