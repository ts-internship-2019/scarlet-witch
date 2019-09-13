using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryInterval
    {
        public DictionaryInterval()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int VisitIntervalId { get; set; }
        public string VisitIntervalName { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
