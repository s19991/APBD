using System;

namespace kol02.Exceptions
{
    public class PlayerAlreadyInTheTeamException: Exception
    {
        public PlayerAlreadyInTheTeamException()
        {
        }
        
        public PlayerAlreadyInTheTeamException(string message) : base(message)
        {
        }
        
        public PlayerAlreadyInTheTeamException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}