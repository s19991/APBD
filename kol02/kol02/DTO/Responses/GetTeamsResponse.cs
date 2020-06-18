using System.Collections.Generic;

namespace kol02.DTO.Responses
{
    public class GetTeamsResponse
    {
        public int IdChampionship { get; set; }

        public Dictionary<string, float> TeamScore { get; set; }
    }
}