namespace DependencyManagement.Core.Utils;

/// <summary>
/// Utility class for working with exceptions for the dependency management.
/// </summary>
public static class ThrowUtils
{
    /// <summary>
    /// Throws an exception if the <paramref name="condition" /> is true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to throw.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the <paramref name="condition" /> is true.</exception>
    public static void ThrowIfDisposed(bool condition, string? message = null, Exception? innerException = null)
    {
        if (condition)
        {
            throw new ObjectDisposedException(message, innerException);
        }
    }

    /// <summary>
    /// Throws an exception if the <paramref name="object" /> is null.
    /// </summary>
    /// <param name="object">The object to check.</param>
    /// <param name="message">The message to throw.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="object" /> is null.</exception>
    public static void ThrowIfNull(object? @object, string? message = null, Exception? innerException = null)
    {
        if (@object is null)
        {
            throw new ArgumentNullException(message, innerException);
        }
    }
}