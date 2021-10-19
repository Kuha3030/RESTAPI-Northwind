using System;
using System.Collections.Generic;

#nullable disable

namespace RESTAPI_Northwind.Models
{
    public partial class Documentation
    {
        public int DocumentationId { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string Keycode { get; set; }
    }
}
