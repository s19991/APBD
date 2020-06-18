using System;

namespace kol02.Exceptions
{
    public class PlayerIsToOldException: Exception
    {
        public PlayerIsToOldException()
        {
        }
        
        public PlayerIsToOldException(string message) : base(message)
        {
        }
        
        public PlayerIsToOldException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}