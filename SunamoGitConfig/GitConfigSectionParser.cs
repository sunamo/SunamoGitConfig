namespace SunamoGitConfig;

/// <summary>
/// Parser for Git configuration sections
/// </summary>
public class GitConfigSectionParser
{
    /// <summary>
    /// Can be null because AddHeaderBlock can be called multiple times, so it cannot be set in constructor
    /// </summary>
    private GitConfigSectionData? last;

    /// <summary>
    /// List of parsed configuration sections
    /// </summary>
    public List<GitConfigSectionData> Values = [];

    /// <summary>
    /// Adds a new configuration section header
    /// </summary>
    /// <param name="section">The type of Git configuration section</param>
    /// <param name="line">The header line from the config file</param>
    public void AddHeaderBlock(GitConfigSection section, string line)
    {
        last = new GitConfigSectionData(section)
        {
            Header = line
        };

        Values.Add(last);
    }

    /// <summary>
    /// Adds a key-value pair to the current configuration section
    /// </summary>
    /// <param name="line">The settings line in format "key=value"</param>
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