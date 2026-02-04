namespace SunamoGitConfig._sunamo;

/// <summary>
/// Helper class for splitting text into lines
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// Splits text into lines handling various newline formats (Windows, Unix, Mac)
    /// </summary>
    /// <param name="text">The text to split into lines</param>
    /// <returns>List of lines</returns>
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    /// <summary>
    /// Splits lines by Unix-style newlines (\r and \n)
    /// </summary>
    /// <param name="lines">The list of lines to process</param>
    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

    /// <summary>
    /// Splits lines by a specific delimiter, handling already-split patterns
    /// </summary>
    /// <param name="lines">The list of lines to process</param>
    /// <param name="delimiter">The delimiter to split by</param>
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

    /// <summary>
    /// Inserts items into a list at a specific index, replacing the original item
    /// </summary>
    /// <param name="list">The list to modify</param>
    /// <param name="itemsToInsert">The items to insert</param>
    /// <param name="index">The index where to insert</param>
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