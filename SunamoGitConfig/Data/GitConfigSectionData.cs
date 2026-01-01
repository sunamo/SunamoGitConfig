namespace SunamoGitConfig.Data;

/// <summary>
/// Represents a single section in Git configuration file with its settings
/// </summary>
/// <param name="section">The type of Git configuration section</param>
public class GitConfigSectionData(GitConfigSection section)
{
    /// <summary>
    /// The type of this configuration section
    /// </summary>
    public GitConfigSection Section { get; set; } = section;

    /// <summary>
    /// Dictionary of key-value pairs representing configuration settings in this section
    /// </summary>
    public Dictionary<string, string> Settings { get; set; } = [];

    /// <summary>
    /// The original header line from the config file (e.g., "[remote \"origin\"]")
    /// </summary>
    public string? Header { get; set; }
}