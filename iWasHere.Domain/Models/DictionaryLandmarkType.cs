using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryLandmarkType
    {
        public DictionaryLandmarkType()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int LandmarkTypeId { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string LandmarkTypeCode { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
