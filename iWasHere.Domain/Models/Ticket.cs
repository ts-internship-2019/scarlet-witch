using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int TicketId { get; set; }
        public int? TicketTypeId { get; set; }
        public string TicketName { get; set; }
        public decimal? TicketCost { get; set; }
        public int? CurrencyId { get; set; }

        public virtual DictionaryCurrency Currency { get; set; }
        public virtual DictionaryTicketType TicketType { get; set; }
        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
