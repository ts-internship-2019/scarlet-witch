using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class CountryXlanguage
    {
        public int CountryXlanguageId { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }

        public virtual DictionaryCountry Country { get; set; }
        public virtual DictionaryLanguage Language { get; set; }
    }
}
