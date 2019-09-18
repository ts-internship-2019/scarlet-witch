using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
   public partial class DictionaryLandmarkTypeModel
    {
        public int LandmarkTypeId { get; set; }
        public string LandmarkTypeCode { get; set; }
        public string Description { get; set; }
    }
}
