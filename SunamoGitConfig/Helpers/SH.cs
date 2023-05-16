using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoGitConfig.Helpers
{
    internal class SH
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
    }
}
