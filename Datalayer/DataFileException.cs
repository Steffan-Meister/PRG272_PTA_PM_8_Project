using System;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
    /// <summary>
    /// Raised when a data-file operation fails. The message is safe to show
    /// directly to the user (no stack trace).
    /// </summary>
    public class DataFileException : Exception
    {
        /// <summary>
        /// Creates the exception with a user-friendly message.
        /// </summary>
        public DataFileException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates the exception with a user-friendly message and the original cause.
        /// </summary>
        public DataFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
