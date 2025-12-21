namespace SunamoGitConfig;

public class GitConfigSectionParser
{
    /// <summary>
    /// Může být null protože AddHeaderBlock se může volat vícekrát => nemůže být ctor
    /// 
    /// </summary>
    private GitConfigSectionData? last;
    public List<GitConfigSectionData> Values = [];

    public void AddHeaderBlock(GitConfigSection section, string line)
    {
        last = new GitConfigSectionData(section)
        {
            Header = line
        };

        Values.Add(last);
    }

    public void AddSettingsPair(string line)
    {
        if (line.Trim() == string.Empty) return;

        var parts = line.Split("=").ToList();
        if (parts.Count > 2)
            ThrowEx.Custom("More than 2 parts");
        else if (parts.Count == 1) ThrowEx.Custom("Line is without " + "=");

        if (last == null)
        {
            throw new Exception($"Call {nameof(AddHeaderBlock)} firstly!");
        }

        last.Settings.Add(parts[0], parts[1]);
    }
}