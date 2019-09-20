using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCurrency
    {
        public DictionaryCurrency()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int CurrencyId { get; set; }
        
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal? CurrencyExchange { get; set; }

        
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
