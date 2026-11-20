namespace SunamoGitConfig.Enums;

/// <summary>
/// Represents the git config section type.
/// </summary>
public enum GitConfigSection
{
    /// <summary>
    /// The core value.
    /// </summary>
    core,

    /// <summary>
    /// The remote value.
    /// </summary>
    remote,

    /// <summary>
    /// The branch value.
    /// </summary>
    branch,

    /// <summary>
    /// The merge value.
    /// </summary>
    merge,

    /// <summary>
    /// The mergetool value.
    /// </summary>
    mergetool,

    /// <summary>
    /// The submodule value.
    /// </summary>
    submodule
}
