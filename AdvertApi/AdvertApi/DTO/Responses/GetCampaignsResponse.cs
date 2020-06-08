using System.Collections.Generic;
using AdvertApi.Models;

namespace AdvertApi.DTO.Responses
{
    public class GetCampaignsResponse
    {
        public List<Campaign> Campaigns { get; set; }
    }
}