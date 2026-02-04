namespace SunamoGitConfig;

/// <summary>
/// Helper class for working with Git configuration files
/// </summary>
public class GitConfigFileHelper : BlockNames
{
    /// <summary>
    /// Formats Git configuration content by ensuring proper indentation
    /// </summary>
    /// <param name="content">The Git configuration content to format</param>
    /// <returns>Formatted Git configuration content</returns>
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

    /// <summary>
    /// Saves Git configuration data to a file
    /// </summary>
    /// <param name="path">The file path where to save the configuration</param>
    /// <param name="content">The Git configuration data to save</param>
    public static void Save(string path, ExistsNonExistsListGitConfig content)
    {
        var stringBuilder = new StringBuilder();

        foreach (var sectionData in content.Exists) AppendBlock(stringBuilder, sectionData);

        var text = stringBuilder.ToString();
        File.WriteAllText(path, text);
    }

    /// <summary>
    /// Appends a configuration section block to the StringBuilder
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder to append to</param>
    /// <param name="data">The configuration section data to append</param>
    private static void AppendBlock(StringBuilder stringBuilder, GitConfigSectionData data)
    {
        if (data.Settings.Count == 0) return;
        stringBuilder.AppendLine("[" + data.Section + PostfixForBlock(data.Section) + "]");
        foreach (var setting in data.Settings) stringBuilder.AppendLine("\t" + setting.Key + "=" + setting.Value);
    }

    /// <summary>
    /// Gets the postfix string for a configuration section header (e.g., ' "origin"' for remote section)
    /// </summary>
    /// <param name="section">The Git configuration section</param>
    /// <returns>The postfix string for the section header</returns>
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

    /// <summary>
    /// Loads and parses a Git configuration file
    /// </summary>
    /// <param name="path">The path to the Git configuration file</param>
    /// <returns>Parsed Git configuration data</returns>
    public static ExistsNonExistsListGitConfig Load(string path)
    {
        return Parse(File.ReadAllText(path));
    }

    /// <summary>
    /// Parses Git configuration file content
    /// </summary>
    /// <param name="gitConfigFileContent">The content of the Git configuration file</param>
    /// <returns>Parsed Git configuration data with existing and non-existing sections</returns>
    public static ExistsNonExistsListGitConfig Parse(string gitConfigFileContent)
    {
        var result = new ExistsNonExistsListGitConfig();
        var lines = SHGetLines.GetLines(gitConfigFileContent);

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
        var values = Enum.GetValues<GitConfigSection>().ToList();
        foreach (var section in values)
            if (!keys.Contains(section))
                result.NonExists.Add(new GitConfigSectionData(section));

        return result;
    }

    /// <summary>
    /// Parses and returns only the block headers from Git configuration content
    /// </summary>
    /// <param name="text">The Git configuration file content</param>
    /// <returns>List of block header lines (lines starting with '[')</returns>
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