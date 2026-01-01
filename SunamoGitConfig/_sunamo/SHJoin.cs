namespace SunamoGitConfig._sunamo;

/// <summary>
/// Helper class for joining strings
/// </summary>
internal class SHJoin
{
    /// <summary>
    /// Joins a list of strings with newline separator
    /// </summary>
    /// <param name="lines">The list of strings to join</param>
    /// <returns>Joined string with newline separators</returns>
    internal static string JoinNL(List<string> lines)
    {
        return string.Join('\n', lines);
    }
}