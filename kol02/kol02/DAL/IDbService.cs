using kol02.DTO.Requests;
using kol02.DTO.Responses;

namespace kol02.DAL
{
    public interface IDbService
    {
        public GetTeamsResponse GetTeams(int id);

        public void AddPlayerToTeam(int id, AddPlayerRequest request);
    }
}