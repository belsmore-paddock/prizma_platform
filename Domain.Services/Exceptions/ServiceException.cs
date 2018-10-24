namespace Prizma.Domain.Services.Exceptions
{
    using System;

    /// <summary>
    /// The service exception is the top level exception for service errors.
    /// </summary>
    public class ServiceException : Exception
    {
        public ServiceException(string message)
            : base(message)
        {
        }
    }
}