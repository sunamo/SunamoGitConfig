// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

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
        var count = File.ReadAllText(@"E:\vs\NovantaOld_Projects\PureVue_purevue-app\.git\config");
        var data = GitConfigFileHelper.Parse(count);
    }
}
