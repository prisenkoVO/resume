using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public class IRole
    {
        IEnumerable<Role> Roles { get;}
    }
}
