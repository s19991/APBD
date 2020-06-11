using System;
using System.Collections.Generic;
using System.IO;
using c14.DAL;
using c14.DTO.Requests;
using c14.Models;
using Microsoft.AspNetCore.Mvc;

namespace c14.Controllers
{
    [ApiController]
    public class CukierniaController : ControllerBase
    {
        private readonly IDbService _service;

        public CukierniaController(IDbService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("api/orders")]
        public IActionResult GetOrders(GetOrdersRequest request)
        {
            IActionResult response;
            try
            {
                IEnumerable<Zamowienie> orders = _service.GetOrders(request);
                response = Ok(orders);
            }
            catch (Exception e)
            {
                response = BadRequest($"Could not get orders due to:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }

        [HttpPost]
        [Route("api/clients/{id}/orders")]
        public IActionResult AddOrder(int id, AddOrderRequest request)
        {
            IActionResult response;
            try
            {
                _service.AddOrder(id, request);
                response = Ok();
            }
            catch (ArgumentException e)
            {
                response = BadRequest($"No such client:\n{e.StackTrace}\n{e.Message}");
            }
            catch (FileNotFoundException e)
            {
                response = NotFound($"Some of the passed items do not exist:\n{e.StackTrace}\n{e.Message}");
            }
            catch (Exception e)
            {
                response = BadRequest($"Some other error occurred due to:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }
    }
}