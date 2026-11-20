namespace SunamoGitConfig;

/// <summary>
/// Represents the git config section parser type.
/// </summary>
public class GitConfigSectionParser
{
    // Can be null because AddHeaderBlock can be called multiple times, so it cannot be set in constructor
    private GitConfigSectionData? currentSection;

    /// <summary>
    /// Gets or sets the values.
    /// </summary>
    public List<GitConfigSectionData> Values { get; set; } = [];

    /// <summary>
    /// Add header block.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="line">The line.</param>
    /// <returns>The add header block result.</returns>
    public void AddHeaderBlock(GitConfigSection section, string line)
    {
        currentSection = new GitConfigSectionData(section)
        {
            Header = line
        };

        Values.Add(currentSection);
    }

    /// <summary>
    /// Initializes a new instance of the add settings pair class.
    /// </summary>
    /// <param name="currentSection">The current section.</param>
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
