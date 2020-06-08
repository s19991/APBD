using System;
using System.Collections.Generic;
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
            if (!_context.Client.Any(c => c.IdClient == request.IdClient))
            {
                throw new KeyNotFoundException($"Couldn't find client: {request.IdClient}");
            }
            
            // todo sprawdzanie czy podane budynki sa na tej samej ulicy -> else 400
            // todo obliczanie kosztu reklamy
            // todo szukanie dobrego rozmiaru banerow
            // todo dodawanie danych do DB
            // todo zwracanie stworzonej kampanii 201
            throw new System.NotImplementedException();
        }
    }
}