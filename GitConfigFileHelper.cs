namespace SunamoGitConfig;

public class GitConfigFileHelper : BlockNames
{
    public static string Format(string actual)
    {
        var l = SHGetLines.GetLines(actual);
        for (var i = 0; i < l.Count; i++)
        {
            var line = l[i];
            if (line.StartsWith('[')) continue;

            if (line.StartsWith('\t')) line = '\t' + line;

            l[i] = line;
        }

        return SHJoin.JoinNL(l).Trim();
    }

    public static void Save(string path, ExistsNonExistsListGitConfig content)
    {
        var sb = new StringBuilder();

        foreach (var item in content.Exists) AppendBlock(sb, item);

        var ts = sb.ToString();
        File.WriteAllText(path, ts);
    }

    private static void AppendBlock(StringBuilder sb, GitConfigSectionData data)
    {
        if (data.Settings.Count == 0) return;
        sb.AppendLine("[" + data.Section + PostfixForBlock(data.Section) + "]");
        foreach (var item in data.Settings) sb.AppendLine("\t" + item.Key + "=" + item.Value);
    }

    private static string PostfixForBlock(GitConfigSection section)
    {
        switch (section)
        {
            case GitConfigSection.remote:
                return " \"origin\"";
            case GitConfigSection.branch:
                return " \"master\"";

            case GitConfigSection.core:
            case GitConfigSection.merge:
            case GitConfigSection.mergetool:
                break;
            default:
                ThrowEx.NotImplementedCase(section);
                break;
        }

        return "";
    }

    public static ExistsNonExistsListGitConfig Load(string path)
    {
        return Parse(File.ReadAllText(path));
    }

    public static ExistsNonExistsListGitConfig Parse(string gitConfigFileContent)
    {
        var result = new ExistsNonExistsListGitConfig();
        var lines = SHGetLines.GetLines(gitConfigFileContent);

        var parser = new GitConfigSectionParser();

        foreach (var item2 in lines)
        {
            var item = item2.Trim();
            if (item.StartsWith('['))
            {
                if (item.StartsWith(coreStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.core, item);
                }
                else if (item.StartsWith(remoteStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.remote, item);
                }
                else if (item.StartsWith(branchStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.branch, item);
                }
                else if (item == mergeStart || item == mergetoolStart)
                {
                }
                else if (item.StartsWith(submoduleStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.submodule, item);
                }
                else
                {
                    // todo asi nen� nejlep�� n�pad vyhodit v�jimku. T�m zahod�m i ty spr�vn� co tam jsou
                    // todo m�sto exc vr�tit seznam "nezn�m�ch"
                    //ThrowEx.NotImplementedCase(item);

                    result.UnknownHeaders ??= [];
                    result.UnknownHeaders.Add(item);
                }
            }
            else
            {
                parser.AddSettingsPair(item);
            }
        }

        result.Exists = parser.Values;

        var keys = parser.Values.Select(d => d.Section);
        var values = Enum.GetValues<GitConfigSection>().ToList();
        foreach (var item in values)
            if (!keys.Contains(item))
                result.NonExists.Add(new GitConfigSectionData(item));

        return result;
    }

    public static List<string> ParseBlocks(string s)
    {
        var result = new List<string>();

        var l = SHGetLines.GetLines(s);
        for (var i = 0; i < l.Count; i++)
        {
            var line = l[i];
            if (line.StartsWith('[')) result.Add(line);
        }

        return result;
    }
}