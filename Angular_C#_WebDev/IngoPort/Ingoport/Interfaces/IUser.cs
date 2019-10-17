using System.Collections.Generic;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> AllUsers { get; }
    }
}
