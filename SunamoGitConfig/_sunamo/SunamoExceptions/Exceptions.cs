namespace SunamoGitConfig._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    #region Other

    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(bool shouldFillFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var currentLineIndex = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; currentLineIndex < lines.Count; currentLineIndex++)
        {
            var line = lines[currentLineIndex];
            if (shouldFillFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    shouldFillFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var afterAt = stackTraceLine.Split("at ")[1].Trim();
        var methodFullName = afterAt.Split("(")[0];
        var parts = methodFullName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    internal static string CallingMethod(int frameDepth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameDepth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name could not be retrieved";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region OnlyReturnString

    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
    #endregion

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
