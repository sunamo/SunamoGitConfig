using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGitConfig
{
    internal class ThrowExGitConfig
    {
        public static void NotImplementedCase(string _case)
        {
            throw new Exception("Not implemented case: " + _case);
        }
    }
}
