﻿using System;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public partial class Client
    {
        public Client()
        {
            Campaign = new HashSet<Campaign>();
        }

        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
