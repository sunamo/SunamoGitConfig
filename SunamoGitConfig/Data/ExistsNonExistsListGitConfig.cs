namespace SunamoGitConfig.Data;

/// <summary>
/// Represents parsed Git configuration with sections that exist and don't exist in the config file
/// </summary>
public class ExistsNonExistsListGitConfig
{
    /// <summary>
    /// List of configuration sections that exist in the Git config file
    /// </summary>
    public List<GitConfigSectionData> Exists { get; set; } = [];

    /// <summary>
    /// List of configuration sections that don't exist in the Git config file
    /// </summary>
    public List<GitConfigSectionData> NonExists { get; set; } = [];

    /// <summary>
    /// List of header lines that couldn't be parsed
    /// </summary>
    public List<string> UnknownHeaders { get; set; } = [];

    /// <summary>
    /// Gets the value for a specific key in a configuration section
    /// </summary>
    /// <param name="blockSection">The Git configuration section to search in</param>
    /// <param name="key">The configuration key to find</param>
    /// <returns>The value if found, null otherwise</returns>
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
    /// Sets the value for a specific key in a configuration section
    /// </summary>
    /// <param name="blockSection">The Git configuration section to modify</param>
    /// <param name="key">The configuration key to set</param>
    /// <param name="value">The value to assign to the key</param>
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