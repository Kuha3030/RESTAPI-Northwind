using System;
using System.Collections.Generic;

#nullable disable

namespace RESTAPI_Northwind.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int AccesslevelId { get; set; }
    }
}
