// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

using SunamoGitConfig.Tests;

namespace RunnerGitConfig;

internal class Program
{
    static void Main()
    {
        var temp = new GitConfigFileHelperTests();
        temp.ParseTest();
    }
}
