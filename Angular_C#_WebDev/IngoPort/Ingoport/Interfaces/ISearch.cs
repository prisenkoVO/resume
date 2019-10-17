using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingoport.Interfaces
{
    interface ISearch
    {
        IQueryable GetUsers(string str);

        IQueryable GetNews(string str);

        IQueryable GetQA(string str);
    }
}
