using AdvertApi.DTO.Requests;
using AdvertApi.DTO.Responses;

namespace AdvertApi.DAL
{
    public interface IDbService
    {
        public void RegisterUser(RegisterUserRequest request);

        public void LoginUser(LoginUserRequest request);

        public GetCampaignsResponse GetCampaigns();

        public void CreateCampaign(CreateCampaignRequest request);
    }
}