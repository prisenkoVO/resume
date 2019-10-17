using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public interface IRegistration
    {
        string RegisterUser(User user);
    }
}
