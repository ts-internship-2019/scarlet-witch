using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCountry
    {
        public DictionaryCountry()
        {
            CountryXlanguage = new HashSet<CountryXlanguage>();
            DictionaryCounty = new HashSet<DictionaryCounty>();
            
        }

        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<CountryXlanguage> CountryXlanguage { get; set; }
        public virtual ICollection<DictionaryCounty> DictionaryCounty { get; set; }
        
    }
}
