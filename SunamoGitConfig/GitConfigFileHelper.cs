using SunamoGitConfig;
using SunamoGitConfig.Data;

public class GitConfigFileHelper
{
    public const string coreStart = "[core]";
    public const string remoteStart = "[remote ";
    public const string branchStart = "[branch ";
    public const string mergeStart = "[merge]";
    public const string mergetoolStart = "[mergetool]";

    public static string Format(string actual)
    {
        var l = SH.GetLines(actual);
        for (int i = 0; i < l.Count; i++)
        {
            var line = l[i];
            if (line.StartsWith("["))
            {
                continue;
            }

            if (line.StartsWith("\t"))
            {
                line = "\t" + line;
            }
        }

        return SH.JoinNL(l).Trim();
    }

    public static ExistsNonExistsListGitConfig<GitConfigSection> ExistsBlocks(string s)
    {
        var r = new ExistsNonExistsListGitConfig<GitConfigSection>();
        var result = r.Exists;
        var l = ParseBlocks(s);
        foreach (var item in l)
        {
            if (item.StartsWith(coreStart))
            {
                result.Add(GitConfigSection.core);
            }
            else if (item.StartsWith(remoteStart))
            {
                result.Add(GitConfigSection.remote);
            }
            else if (item.StartsWith(branchStart))
            {
                result.Add(GitConfigSection.branch);
            }
            else if(item == mergeStart || item == mergetoolStart)
            {

            }
            else
            {
                ThrowExGitConfig.NotImplementedCase(item);
            }
        }

        var values = Enum.GetValues <GitConfigSection>().ToList();
        foreach (var item in values)
        {
            if (!result.Contains(item))
            {
                r.NonExists.Add(item);
            }
        }

        return r;
    }

    public static List<string> ParseBlocks(string s)
    {
        List<string> result = new List<string>();

        var l = SH.GetLines(s);
        for (int i = 0; i < l.Count; i++)
        {
            var line = l[i];
            if (line.StartsWith("["))
            {
                result.Add(line);
            }
        }

        return result;
    }
}
