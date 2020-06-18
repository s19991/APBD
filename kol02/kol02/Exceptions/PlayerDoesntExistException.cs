using System;

namespace kol02.Exceptions
{
    public class PlayerDoesntExistException: Exception
    {
        public PlayerDoesntExistException()
        {
        }
        
        public PlayerDoesntExistException(string message) : base(message)
        {
        }
        
        public PlayerDoesntExistException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}