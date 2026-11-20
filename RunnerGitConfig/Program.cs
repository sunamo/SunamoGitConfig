
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
