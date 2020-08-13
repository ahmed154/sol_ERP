using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_API.ViewModels
{
    public class UserToken
    {       
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
