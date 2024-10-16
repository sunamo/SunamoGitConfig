namespace SunamoGitConfig;

public class GitConfigSectionParser
{
    private GitConfigSectionData last;
    public List<GitConfigSectionData> Values = new();

    public void AddHeaderBlock(GitConfigSection section, string line)
    {
        last = new GitConfigSectionData(section);
        last.Header = line;

        Values.Add(last);
    }

    public void AddSettingsPair(string line)
    {
        if (line.Trim() == string.Empty) return;

        var parts = line.Split("=").ToList();
        if (parts.Count > 2)
            ThrowEx.Custom("More than 2 parts");
        else if (parts.Count == 1) ThrowEx.Custom("Line is without " + "=");

        last.Settings.Add(parts[0], parts[1]);
    }
}