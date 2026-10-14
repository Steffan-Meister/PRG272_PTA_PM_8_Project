using System;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
    
    public class DataFileException : Exception
    {
        public DataFileException()
        {
        }

        public DataFileException(string message)
            : base(message)
        {
        }

        public DataFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
