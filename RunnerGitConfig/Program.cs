// variables names: ok
// Variable names have been checked and replaced with self-descriptive names

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
