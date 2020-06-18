
namespace kol02.Models
{
    public class PlayerTeam
    {
        public int IdPlayer { get; set; }
        public int IdTeam { get; set; }
        public int NumOnShirt { get; set; }
        public string Comment { get; set; }
        public Player Player { get; set; }
        public Team Team { get; set; }
    }
}
