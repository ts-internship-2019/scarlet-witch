using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int? LandmarkId { get; set; }
        public string Review1 { get; set; }
        public string Title { get; set; }
        public decimal? Grade { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }

        public virtual Landmark Landmark { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
