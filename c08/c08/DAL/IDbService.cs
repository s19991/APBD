using c08.DTOs.Responses;

namespace c08.DAL
{
    public interface IDbService
    {
        public GetAnimalsResponse GetAnimals(string orderBy);
    }
}