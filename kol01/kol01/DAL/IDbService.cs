using kol01.DTOs.Requests;
using kol01.DTOs.Responses;

namespace kol01.DAL
{
    public interface IDbService
    {
        public GetPrescriptionsResponse GetPrescriptions(int id);

        public PostPrescriptionResponse PostPrescription(PostPrescriptionRequest request);
    }
}