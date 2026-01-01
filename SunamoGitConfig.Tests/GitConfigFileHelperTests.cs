using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGitConfig.Tests;

/// <summary>
/// Tests for GitConfigFileHelper class
/// </summary>
public class GitConfigFileHelperTests
{
    /// <summary>
    /// Tests the Parse method for Git configuration files
    /// </summary>
    [Fact]
    public void ParseTest()
    {
        var gitConfigContent = File.ReadAllText(@"E:\vs\NovantaOld_Projects\PureVue_purevue-app\.git\config");
        var data = GitConfigFileHelper.Parse(gitConfigContent);

        // Verify that parsing completed successfully
        Assert.NotNull(data);
        Assert.NotNull(data.Exists);
        Assert.NotNull(data.NonExists);
    }
}
