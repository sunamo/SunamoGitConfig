namespace SunamoGitConfig._sunamo;

internal class SHGetLines
{
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

    private static void SplitBy(List<string> lines, string delimiter)
    {
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var windowsNewlineParts = lines[i].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var macNewlineParts = lines[i].Split(new string[] { "\n\r" }, StringSplitOptions.None);

                if (windowsNewlineParts.Length > 1)
                {
                    ThrowEx.Custom("cannot contain any \\r\\n, pass already split by this pattern");
                }
                else if (macNewlineParts.Length > 1)
                {
                    ThrowEx.Custom("cannot contain any \\n\\r, pass already split by this pattern");
                }
            }

            var parts = lines[i].Split(new string[] { delimiter }, StringSplitOptions.None);

            if (parts.Length > 1)
            {
                InsertOnIndex(lines, parts.ToList(), i);
            }
        }
    }

    private static void InsertOnIndex(List<string> list, List<string> itemsToInsert, int index)
    {
        itemsToInsert.Reverse();

        list.RemoveAt(index);

        foreach (var line in itemsToInsert)
        {
            list.Insert(index, line);
        }
    }
}
