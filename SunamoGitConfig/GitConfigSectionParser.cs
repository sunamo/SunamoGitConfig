namespace SunamoGitConfig;

public class GitConfigSectionParser
{
    // Can be null because AddHeaderBlock can be called multiple times, so it cannot be set in constructor
    private GitConfigSectionData? currentSection;

    public List<GitConfigSectionData> Values { get; set; } = [];

    public void AddHeaderBlock(GitConfigSection section, string line)
    {
        currentSection = new GitConfigSectionData(section)
        {
            Header = line
        };

        Values.Add(currentSection);
    }

    public void AddSettingsPair(string line)
    {
        if (line.Trim() == string.Empty) return;

        var parts = line.Split("=").ToList();
        if (parts.Count > 2)
            ThrowEx.Custom("More than 2 parts");
        else if (parts.Count == 1) ThrowEx.Custom("Line is without " + "=");

        if (currentSection == null)
        {
            throw new Exception($"Call {nameof(AddHeaderBlock)} firstly!");
        }

        currentSection.Settings.Add(parts[0], parts[1]);
    }
}
