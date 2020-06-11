
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using c14.Controllers;
using c14.DTO.Requests;
using c14.Models;
using Microsoft.AspNetCore.Mvc;

namespace c14.DAL
{
    public class SqlServerDbService : IDbService
    {
        private readonly CukierniaDbContext _context;

        public SqlServerDbService(CukierniaDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Zamowienie> GetOrders(GetOrdersRequest request)
        {
            IEnumerable<Zamowienie> orders = _context.Zamowienies.ToList();
            if (request.ClientLastName != null)
            {
                orders = orders.Where(x => x.Klient.Nazwisko.Equals(request.ClientLastName));
            } 
            return orders;
        }
        
        public void AddOrder(int id, AddOrderRequest request)
        {
            if (!_context.Klients.Any(x => x.IdKlient == id))
            {
                throw new ArgumentException($"No client with id: {id} exists...");
            }
            
            foreach (var item in request.Wyroby)
            {
                var exists = _context.WyrobCukierniczys.Any(x => x.Nazwa.Equals(item.Nazwa));
                if (!exists)
                {
                    throw new FileNotFoundException($"There was now item named: {item.Nazwa}");
                }
            }

            int orderId = _context.Zamowienies.Max(x => x.IdZamowienia) + 1;
            _context.Zamowienies.Add(
                new Zamowienie
                {
                    IdZamowienia = orderId,
                    DataPrzyjecia = request.DataPrzyjecia,
                    Uwagi = request.Uwagi,
                    IdKlient = id
                }
            );
            
            foreach (var item in request.Wyroby)
            {
                _context.ZamowienieWyrobCukierniczies.Add(
                    new Zamowienie_WyrobCukierniczy
                    {
                        IdWyrobCukierniczy = _context.WyrobCukierniczys
                            .Single(x => x.Nazwa.Equals(item.Nazwa))
                            .IdWyrobCukierniczy,
                        IdZamowienia = orderId,
                        Ilosc = item.Ilosc,
                        Uwagi = item.Uwagi
                    }
                );
            }

            _context.SaveChanges();
        }
    }
}