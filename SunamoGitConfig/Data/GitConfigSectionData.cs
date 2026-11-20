namespace SunamoGitConfig.Data;

/// <summary>
/// Git config section data.
/// </summary>
/// <param name="section">The section.</param>
/// <returns>The git config section data result.</returns>
public class GitConfigSectionData(GitConfigSection section)
{
    /// <summary>
    /// Gets or sets the section.
    /// </summary>
    public GitConfigSection Section { get; set; } = section;

    /// <summary>
    /// Gets or sets the settings.
    /// </summary>
    public Dictionary<string, string> Settings { get; set; } = [];

    /// <summary>
    /// Gets or sets the header.
    /// </summary>
    public string? Header { get; set; }
}
