// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoGitConfig.Data;

public class GitConfigSectionData(GitConfigSection section)
{
    public GitConfigSection Section { get; set; } = section;
    public Dictionary<string, string> Settings { get; set; } = [];
    public string? Header { get; set; }
}
