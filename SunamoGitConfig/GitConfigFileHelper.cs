namespace SunamoGitConfig;

public class GitConfigFileHelper : BlockNames
{
    public static string Format(string content)
    {
        var list = SHGetLines.GetLines(content);
        for (var i = 0; i < list.Count; i++)
        {
            var line = list[i];
            if (line.StartsWith('[')) continue;

            if (!line.StartsWith('\t')) line = '\t' + line;

            list[i] = line;
        }

        return SHJoin.JoinNL(list).Trim();
    }

    public static void Save(string path, ExistsNonExistsListGitConfig config)
    {
        var stringBuilder = new StringBuilder();

        foreach (var sectionData in config.Exists) AppendBlock(stringBuilder, sectionData);

        var text = stringBuilder.ToString();
        File.WriteAllText(path, text);
    }

    private static void AppendBlock(StringBuilder stringBuilder, GitConfigSectionData data)
    {
        if (data.Settings.Count == 0) return;
        stringBuilder.AppendLine("[" + data.Section + PostfixForBlock(data.Section) + "]");
        foreach (var setting in data.Settings) stringBuilder.AppendLine("\t" + setting.Key + "=" + setting.Value);
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

    public static ExistsNonExistsListGitConfig Parse(string content)
    {
        var result = new ExistsNonExistsListGitConfig();
        var lines = SHGetLines.GetLines(content);

        var parser = new GitConfigSectionParser();

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (line.StartsWith('['))
            {
                if (line.StartsWith(CoreStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.core, line);
                }
                else if (line.StartsWith(RemoteStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.remote, line);
                }
                else if (line.StartsWith(BranchStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.branch, line);
                }
                else if (line == MergeStart || line == MergetoolStart)
                {
                }
                else if (line.StartsWith(SubmoduleStart))
                {
                    parser.AddHeaderBlock(GitConfigSection.submodule, line);
                }
                else
                {
                    // Unknown header - add to the list instead of throwing exception
                    // This allows partial parsing without losing valid sections
                    result.UnknownHeaders ??= [];
                    result.UnknownHeaders.Add(line);
                }
            }
            else
            {
                parser.AddSettingsPair(line);
            }
        }

        result.Exists = parser.Values;

        var keys = parser.Values.Select(value => value.Section);
        var values = ((GitConfigSection[])Enum.GetValues(typeof(GitConfigSection))).ToList();
        foreach (var section in values)
            if (!keys.Contains(section))
                result.NonExists.Add(new GitConfigSectionData(section));

        return result;
    }

    public static List<string> ParseBlocks(string text)
    {
        var result = new List<string>();

        var list = SHGetLines.GetLines(text);
        for (var i = 0; i < list.Count; i++)
        {
            var line = list[i];
            if (line.StartsWith('[')) result.Add(line);
        }

        return result;
    }
}
