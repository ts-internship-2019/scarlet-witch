using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public int? LandmarkId { get; set; }
        public string Review1 { get; set; }
        public string Title { get; set; }
        public decimal? Grade { get; set; }
        public string UserId { get; set; }
    }
}
