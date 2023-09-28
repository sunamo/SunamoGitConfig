namespace SunamoGitConfig.Data
{
    public class GitConfigSectionData
    {
        public GitConfigSection Section;
        public Dictionary<string, string> Settings = new Dictionary<string, string>();

        public GitConfigSectionData(GitConfigSection section)
        {
            Section = section;
        }
    }
}
