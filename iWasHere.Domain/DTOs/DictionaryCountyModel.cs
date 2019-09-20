using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCountyModel
    {
        public int? CountryId { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string CountryName { get; set; }
    }
}
