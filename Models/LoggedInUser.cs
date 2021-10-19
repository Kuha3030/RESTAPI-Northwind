using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI_Northwind.Models
{
    public class LoggedInUser
    {
        public string Username { get; set; }
        public int AccesslevelId { get; set; }
        public string Token { get; set; }

    }
}
