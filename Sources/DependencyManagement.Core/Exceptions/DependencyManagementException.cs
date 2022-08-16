namespace DependencyManagement.Core.Exceptions;

/// <summary>
///     An core exception class for the dependency management libraries.
/// </summary>
/// <remarks>
///     If you want to catch all exceptions related to dependency management libraries only,
///     use this exception class type in error catching.
/// </remarks>
public class DependencyManagementException : Exception
{
    /// <param name="message">The message with the information about the exception.</param>
    public DependencyManagementException(string message) : base(message)
    {
    }

    /// <param name="message">The message with the information about the exception.</param>
    /// <param name="inner">The inner exception.</param>
    public DependencyManagementException(string message, Exception inner) : base(message, inner)
    {
    }
}