using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class TopLandmark
    {
        public int LandmarkId { get; set; }
        public string LandmarkDescription { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Images> Path { get; set; }
    }
}
