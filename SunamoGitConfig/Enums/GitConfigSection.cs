namespace SunamoGitConfig.Enums;

/// <summary>
/// Represents different sections in Git configuration file
/// </summary>
public enum GitConfigSection
{
    /// <summary>
    /// Core Git configuration section
    /// </summary>
    core,

    /// <summary>
    /// Remote repository configuration section
    /// </summary>
    remote,

    /// <summary>
    /// Branch configuration section
    /// </summary>
    branch,

    /// <summary>
    /// Merge configuration section
    /// </summary>
    merge,

    /// <summary>
    /// Merge tool configuration section
    /// </summary>
    mergetool,

    /// <summary>
    /// Submodule configuration section
    /// </summary>
    submodule
}