using System;
using System.Collections.Generic;
using c14.Models;

namespace c14.DTO.Requests
{
    public class AddOrderRequest
    {
        public DateTime DataPrzyjecia { get; set; }

        public string Uwagi { get; set; }

        public List<AddOrderWyrobCukierniczy> Wyroby { get; set; }
    }
}