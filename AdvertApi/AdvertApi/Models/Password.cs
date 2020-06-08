using System;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public partial class Password
    {
        public int IdClient { get; set; }
        public string Password1 { get; set; }

        public virtual Client IdClientNavigation { get; set; }
    }
}
