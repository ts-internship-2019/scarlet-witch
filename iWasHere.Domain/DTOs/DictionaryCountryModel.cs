using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCountryModel
    {
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public string CountryName { get; set; }
        public string LanguageName { get; set; }
    }
}
