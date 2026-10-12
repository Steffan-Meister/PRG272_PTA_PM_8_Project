using System;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
  
    /// Raised when a data-file operation fails. The message is safe to show
    /// directly to the user (no stack trace).
    
    public class DataFileException : Exception
    {
        
        /// Creates the exception with a user-friendly message.
        
        public DataFileException(string message)
            : base(message)
        {
        }

        
        /// Creates the exception with a user-friendly message and the original cause.
       
        public DataFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
