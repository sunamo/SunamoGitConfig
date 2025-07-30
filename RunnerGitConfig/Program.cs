using SunamoGitConfig.Tests;

namespace RunnerGitConfig;

internal class Program
{
    static void Main()
    {
        var t = new GitConfigFileHelperTests();
        t.ParseTest();
    }
}
