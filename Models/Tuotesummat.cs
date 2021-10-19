using System;
using System.Collections.Generic;

#nullable disable

namespace RESTAPI_Northwind.Models
{
    public partial class Tuotesummat
    {
        public int ProductId { get; set; }
        public string TuotteenNimi { get; set; }
        public decimal? TuotteidenYhteenlaskettuArvo { get; set; }
    }
}
