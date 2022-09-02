namespace DependencyManagement.Composition.Exceptions;

using Core.Exceptions;

public class EmptySequenceDependencyManagementException : DependencyManagementException
{
    public EmptySequenceDependencyManagementException() : base("The source sequence is empty.",
        new InvalidOperationException("The source sequence is empty.")) { }

    public EmptySequenceDependencyManagementException(string message)
        : base(message, new InvalidOperationException(message)) { }

    public EmptySequenceDependencyManagementException(string message, Exception inner) : base(message, inner) { }
}