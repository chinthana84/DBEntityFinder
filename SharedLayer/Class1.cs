using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer
{
    public class Class1
    {
    }

    public enum dbType
    {
        SqlServer=1
    }

    public class AppKeyObject
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public string dbType { get; set; }

    }
}
