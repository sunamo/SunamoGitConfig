namespace SunamoGitConfig._sunamo.SunamoExceptions;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
internal partial class ThrowEx
{


    internal static bool Custom(string message, bool reallyThrow = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? str = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(str, reallyThrow);
    }

    internal static bool NotImplementedCase(object notImplementedName)
    { return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName); }

    #region Other
    /// <summary>
    /// Gets the full name (type.method) of the currently executed code
    /// </summary>
    /// <returns>Full name in format "TypeName.MethodName"</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfExc = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfExc.Item1, placeOfExc.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Gets the full name (type.method) from type and method name
    /// </summary>
    /// <param name="type">The type object (can be Type, MethodBase, string, or any object)</param>
    /// <param name="methodName">The method name, or null to extract from stack trace</param>
    /// <param name="isFromThrowEx">Whether this is called from ThrowEx methods</param>
    /// <returns>Full name in format "TypeName.MethodName"</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type type2)
        {
            typeFullName = type2.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type typeInfo = type.GetType();
            typeFullName = typeInfo.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the message is not null
    /// </summary>
    /// <param name="exception">The exception message, or null if no exception should be thrown</param>
    /// <param name="reallyThrow">Whether to actually throw the exception (true) or just return true (false)</param>
    /// <returns>True if exception message is not null, false otherwise</returns>
    internal static bool ThrowIsNotNull(string? exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (reallyThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode

    /// <summary>
    /// Creates and throws an exception using a factory function
    /// </summary>
    /// <typeparam name="A">The type of the exception parameter</typeparam>
    /// <param name="exceptionFactory">Factory function that creates the exception message</param>
    /// <param name="exceptionParameter">Parameter to pass to the factory function</param>
    /// <returns>True if exception was thrown or would be thrown, false otherwise</returns>
    internal static bool ThrowIsNotNull<A>(Func<string, A, string?> exceptionFactory, A exceptionParameter)
    {
        string? exc = exceptionFactory(FullNameOfExecutedCode(), exceptionParameter);
        return ThrowIsNotNull(exc);
    }

    #endregion
    #endregion
}