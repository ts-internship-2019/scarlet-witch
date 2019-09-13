using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Images
    {
        public int ImageId { get; set; }
        public string Path { get; set; }
        public int? LandmarkId { get; set; }

        public virtual Landmark Landmark { get; set; }
    }
}
