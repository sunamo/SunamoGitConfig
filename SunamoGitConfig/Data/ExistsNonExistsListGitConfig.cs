namespace SunamoGitConfig.Data;

public class ExistsNonExistsListGitConfig
{
    public List<GitConfigSectionData> Exists { get; set; } = [];
    public List<GitConfigSectionData> NonExists { get; set; } = [];
    public List<string> UnknownHeaders { get; set; } = [];

    public string? GetValue(GitConfigSection blockSection, string key)
    {
        var block = Exists.FirstOrDefault(d => d.Section == blockSection);

        if (block == default(GitConfigSectionData))
        {
            return null;
        }

        var pair = block.Settings.Where(d => d.Key == key);

        if (!pair.Any())
        {
            return null;
        }

        return pair.First().Value;
    }


    public void SetValue(GitConfigSection blockSection, string key, string value)
    {
        var block = Exists.FirstOrDefault(d => d.Section == blockSection);

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