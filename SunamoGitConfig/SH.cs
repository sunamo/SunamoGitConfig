using static System.Net.Mime.MediaTypeNames;

namespace SunamoGitConfig
{
    public class SH
    {
        public static List<string> GetLines(string text)
        {
            List<string> result = new List<string>();

            using (StringReader sr = new StringReader(text))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public static string JoinNL(List<string> lines)
        {
            return string.Join(Environment.NewLine, lines);
        }
    }
}
