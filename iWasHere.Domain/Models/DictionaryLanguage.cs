using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryLanguage
    {
        public DictionaryLanguage()
        {
            CountryXlanguage = new HashSet<CountryXlanguage>();
        }

        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        public virtual ICollection<CountryXlanguage> CountryXlanguage { get; set; }
    }
}
