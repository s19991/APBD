using System;

namespace kol02.Exceptions
{
    public class ChampionshipDoesntExistException: Exception
    {
        public ChampionshipDoesntExistException()
        {
        }
        
        public ChampionshipDoesntExistException(string message) : base(message)
        {
        }
        
        public ChampionshipDoesntExistException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}