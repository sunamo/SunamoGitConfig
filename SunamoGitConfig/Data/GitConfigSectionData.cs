namespace SunamoGitConfig.Data;

public class GitConfigSectionData(GitConfigSection section)
{
    public GitConfigSection Section { get; set; } = section;
    public Dictionary<string, string> Settings { get; set; } = [];
    public string? Header { get; set; }
}