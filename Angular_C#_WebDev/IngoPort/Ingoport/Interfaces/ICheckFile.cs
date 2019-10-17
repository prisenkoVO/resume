using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingoport.Interfaces
{
    public interface ICheckFile
    {
        Dictionary<string, string> GetFile();
    }
}
