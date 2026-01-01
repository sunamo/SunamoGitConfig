namespace SunamoGitConfig._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.

/// <summary>
/// Helper class for formatting exception messages
/// </summary>
internal sealed partial class Exceptions
{
    #region Other

    /// <summary>
    /// Checks and formats the prefix string for exception messages
    /// </summary>
    /// <param name="before">The prefix string to check</param>
    /// <returns>Formatted prefix string with colon and space, or empty string if input is null/whitespace</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Gets the place (type, method, stack trace) where an exception occurred
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to fill the first two return values (type and method name)</param>
    /// <returns>Tuple containing type name, method name, and formatted stack trace</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var item = lines[i];
            if (isFillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts type name and method name from a stack trace line
    /// </summary>
    /// <param name="stackTraceLine">The stack trace line to parse</param>
    /// <param name="type">Output parameter for the type name</param>
    /// <param name="methodName">Output parameter for the method name</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var afterAt = stackTraceLine.Split("at ")[1].Trim();
        var text = afterAt.Split("(")[0];
        var parts = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method from the stack trace
    /// </summary>
    /// <param name="frameDepth">The depth in the stack trace to retrieve (default is 1)</param>
    /// <returns>The name of the calling method, or error message if not available</returns>
    internal static string CallingMethod(int frameDepth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameDepth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region OnlyReturnString

    /// <summary>
    /// Creates a custom exception message with optional prefix
    /// </summary>
    /// <param name="before">The prefix to add before the message</param>
    /// <param name="message">The exception message</param>
    /// <returns>Formatted exception message</returns>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
    #endregion

    /// <summary>
    /// Creates a "not implemented case" exception message
    /// </summary>
    /// <param name="before">The prefix to add before the message</param>
    /// <param name="notImplementedName">The name or type of the not implemented case</param>
    /// <returns>Formatted "not implemented case" exception message</returns>
    internal static string? NotImplementedCase(string before, object notImplementedName)
    {
        var suffix = string.Empty;
        if (notImplementedName != null)
        {
            suffix = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                suffix += ((Type)notImplementedName).FullName;
            else
                suffix += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + suffix + " . internal program error. Please contact developer" +
        ".";
    }
}