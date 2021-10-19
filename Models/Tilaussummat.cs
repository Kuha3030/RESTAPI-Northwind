using System;
using System.Collections.Generic;

#nullable disable

namespace RESTAPI_Northwind.Models
{
    public partial class Tilaussummat
    {
        public int OrderId { get; set; }
        public decimal? TilauksenYhteelaskettuSumma { get; set; }
    }
}
