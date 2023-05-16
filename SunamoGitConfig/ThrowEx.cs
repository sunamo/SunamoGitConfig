using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGitConfig
{
    internal class ThrowEx
    {
        public static void NotImplementedCase(object _case)
        {
            throw new Exception("Not implemented case: " + _case);
        }

        internal static void Custom(string v)
        {
            throw new Exception(v);
        }
    }
}
