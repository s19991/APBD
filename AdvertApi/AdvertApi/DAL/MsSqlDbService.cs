using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AdvertApi.DTO.Requests;
using AdvertApi.DTO.Responses;
using AdvertApi.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdvertApi.DAL
{
    public class MsSqlDbService : IDbService
    {
        private readonly s19991Context _context;

        public MsSqlDbService(s19991Context context)
        {
            _context = context;
        }
         
        public void RegisterUser(RegisterUserRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void LoginUser(LoginUserRequest request)
        {
            throw new System.NotImplementedException();
        }

        public GetCampaignsResponse GetCampaigns()
        {
            var response = new GetCampaignsResponse();
            try
            {
                response.Campaigns = _context.Campaign.ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn\'t get campaigns due to: {e.StackTrace} {e.Message}");
            }

            return response;
        }

        public void CreateCampaign(CreateCampaignRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}