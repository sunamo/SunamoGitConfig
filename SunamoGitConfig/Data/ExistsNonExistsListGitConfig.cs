namespace SunamoGitConfig.Data;

/// <summary>
/// Represents the exists non exists list git config type.
/// </summary>
public class ExistsNonExistsListGitConfig
{
    /// <summary>
    /// Gets or sets the exists.
    /// </summary>
    public List<GitConfigSectionData> Exists { get; set; } = [];

    /// <summary>
    /// Gets or sets the non exists.
    /// </summary>
    public List<GitConfigSectionData> NonExists { get; set; } = [];

    /// <summary>
    /// Gets or sets the unknown headers.
    /// </summary>
    public List<string> UnknownHeaders { get; set; } = [];

    /// <summary>
    /// Get value.
    /// </summary>
    /// <param name="blockSection">The block section.</param>
    /// <param name="key">The key.</param>
    /// <returns>The get value result.</returns>
    public string? GetValue(GitConfigSection blockSection, string key)
    {
        var block = Exists.FirstOrDefault(section => section.Section == blockSection);

        if (block == default(GitConfigSectionData))
        {
            return null;
        }

        var pair = block.Settings.Where(setting => setting.Key == key);

        if (!pair.Any())
        {
            return null;
        }

        return pair.First().Value;
    }

    /// <summary>
    /// Initializes a new instance of the set value class.
    /// </summary>
    /// <param name="!pair.Any()">The pair any.</param>
    public void SetValue(GitConfigSection blockSection, string key, string value)
    {
        var block = Exists.FirstOrDefault(section => section.Section == blockSection);

        if (block == default(GitConfigSectionData))
        {
            block = new GitConfigSectionData(blockSection);

            Exists.Add(block);
        }

        if (!block.Settings.TryAdd(key, value))
        {
            block.Settings[key] = value;
        }
    }


}
