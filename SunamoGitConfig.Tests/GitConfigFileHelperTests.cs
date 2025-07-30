using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGitConfig.Tests;
public class GitConfigFileHelperTests
{
    public void ParseTest()
    {
        var c = File.ReadAllText(@"E:\vs\NovantaOld_Projects\PureVue_purevue-app\.git\config");
        var d = GitConfigFileHelper.Parse(c);
    }
}
