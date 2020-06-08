using AdvertApi.DTO.Requests;

namespace AdvertApi.DAL
{
    public interface IDbService
    {
        public void RegisterUser(RegisterUserRequest request);

        public void LoginUser(LoginUserRequest request);

        public void GetCampaigns();

        public void CreateCampaign(CreateCampaignRequest request);
    }
}