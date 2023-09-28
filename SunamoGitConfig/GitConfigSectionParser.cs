namespace SunamoGitConfig
{
    internal class GitConfigSectionParser
    {
        Dictionary<GitConfigSection, GitConfigSectionData> result2 = new Dictionary<GitConfigSection, GitConfigSectionData>();
        GitConfigSectionData last = null;

        public void AddHeaderBlock(GitConfigSection section)
        {
            last = new GitConfigSectionData(section);
            result2.Add(section, last);
        }

        public void AddSettingsPair(string line)
        {
            if (line.Trim() == string.Empty)
            {
                return;
            }

            var parts = line.Split(AllStrings.swes).ToList();
            if (parts.Count > 2)
            {
                ThrowEx.Custom("More than 2 parts");
            }
            else if (parts.Count == 1)
            {
                ThrowEx.Custom("Line is without " + AllStrings.swes);
            }

            last.Settings.Add(parts[0], parts[1]);
        }

        public List<GitConfigSection> Keys()
        {
            return result2.Keys.ToList();
        }

        public List<GitConfigSectionData> Values()
        {
            return result2.Values.ToList();
        }

    }
}
;
