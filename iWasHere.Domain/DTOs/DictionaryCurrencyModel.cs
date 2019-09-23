using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCurrencyModel
    {
        public int CurrencyId { get; set; }

       
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyExchange { get; set; }

   

    }
}
