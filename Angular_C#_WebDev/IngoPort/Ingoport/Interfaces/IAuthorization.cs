using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public interface IAuthorization
    {

        string CheckForAvailability(User user);

        string GetToken(List<Claim> claimss);

        string DecodeToken(string jwt);
    }
}
