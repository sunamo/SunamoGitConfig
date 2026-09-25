namespace SunamoGitConfig;

/// <summary>
/// Constants for Git configuration block header names
/// </summary>
public class BlockNames
{
    /// <summary>
    /// Start of [core] block
    /// </summary>
    public const string CoreStart = "[core]";

    /// <summary>
    /// Start of [remote] block
    /// </summary>
    public const string RemoteStart = "[remote ";

    /// <summary>
    /// Start of [branch] block
    /// </summary>
    public const string BranchStart = "[branch ";

    /// <summary>
    /// Start of [merge] block
    /// </summary>
    public const string MergeStart = "[merge]";

    /// <summary>
    /// Start of [mergetool] block
    /// </summary>
    public const string MergetoolStart = "[mergetool]";

    /// <summary>
    /// Start of [submodule] block
    /// </summary>
    public const string SubmoduleStart = "[submodule ";
}