namespace DependencyManagement.Core.Utils;

using Disposables;

/// <summary>
/// Utility class for working with exceptions for the dependency management.
/// </summary>
public static class Thrower
{
    /// <summary>
    /// Thrower an exception if the <paramref name="condition" /> is true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to throw.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the <paramref name="condition" /> is true.</exception>
    public static void ThrowIfObjectDisposed(bool condition, string? message = null, Exception? innerException = null)
    {
        if (condition)
        {
            throw new ObjectDisposedException(message, innerException);
        }
    }

    /// <summary>
    /// Thrower an exception if the <paramref name="condition" /> is true.
    /// </summary>
    /// <param name="object">The object to check.</param>
    /// <param name="message">The message to throw.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the <paramref name="object.IsDisposed" /> is true.</exception>
    public static void ThrowIfObjectDisposed(IDisposableObject @object, string? message = null,
        Exception? innerException = null)
    {
        ThrowIfObjectDisposed(@object.IsDisposed);
    }

    /// <summary>
    /// Thrower an exception if the <paramref name="object" /> is null.
    /// </summary>
    /// <param name="object">The object to check.</param>
    /// <param name="message">The message to throw.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="object" /> is null.</exception>
    public static void ThrowIfArgumentNull(object? @object, string? message = null, Exception? innerException = null)
    {
        if (@object is null)
        {
            throw new ArgumentNullException(message, innerException);
        }
    }

    /// <summary>
    /// Thrower an exception if the <paramref name="object" /> is null.
    /// </summary>
    /// <param name="object">The object to check.</param>
    /// <param name="message">The message to throw.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <exception cref="NullReferenceException">Thrown if the <paramref name="object" /> is null.</exception>
    public static void ThrowIfObjectNull(object? @object, string? message = null, Exception? innerException = null)
    {
        if (@object is null)
        {
            throw new NullReferenceException(message, innerException);
        }
    }
}