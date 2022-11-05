using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer
{
    public class Shared
    {
        public static AppKeyObject AppKeyObject;
        public static string SearchDefinition;
    }

    public enum dbType
    {
        SqlServer=1
    }
}
