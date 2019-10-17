using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public interface IRandomCoffee
    {
        Dictionary<string, string> Enroll(long id);

        string AddFeedback(RandomCoffeeFeedback feedback);

        string FindNewPair(long userId);

        int GetUserStatus(long userId);

        Dictionary<string, object> ServiceUsage();

        string CloseMeeting(long id);

        IQueryable MyRCHistory(long id);

        IQueryable GetUserInfo(long userId);
    }
}
