using Ingoport.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ingoport.Services
{
    public class CheckFileService : ICheckFile
    {
        public Dictionary<string, string> GetFile()
        {
            string path = System.IO.Path.GetFullPath(".\\test.json");
            using (StreamReader sr = new StreamReader(path))
            {
                string str = sr.ReadToEnd();
                string[] arr = str.Replace("}", string.Empty).Replace("{", string.Empty).Replace("\"", string.Empty).Split(",",
                    StringSplitOptions.RemoveEmptyEntries);

                var dict = arr
                    .Select(x => x.Split(':'))
                    .ToDictionary(b => b[0], b => b[1]);

                return dict;
            }
        }
    }
}
