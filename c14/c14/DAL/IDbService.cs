using System.Collections;
using System.Collections.Generic;
using c14.DTO.Requests;
using c14.Models;

namespace c14.DAL
{
    public interface IDbService
    {
        public IEnumerable<Zamowienie> GetOrders(GetOrdersRequest request);
        
        public void AddOrder(int id, AddOrderRequest request);
    }
}