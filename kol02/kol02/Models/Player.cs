using System;
using System.Collections.Generic;

namespace kol02.Models
{
    public class Player
    {
        public int IdPlayer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<PlayerTeam> PlayerTeams { get; set; }
    }
}
