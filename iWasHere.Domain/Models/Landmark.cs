using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Landmark
    {
        public Landmark()
        {
            Images = new HashSet<Images>();
            Review = new HashSet<Review>();
        }

        public int LandmarkId { get; set; }
        public int? LandmarkTypeId { get; set; }
        public bool? HasEntryTicket { get; set; }
        public int? VisitIntervalId { get; set; }
        public string LandmarkDescription { get; set; }
        public int? TicketId { get; set; }
        public string StreetName { get; set; }
        public int? StreetNumber { get; set; }
        public int? CityId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual DictionaryCity City { get; set; }
        public virtual DictionaryLandmarkType LandmarkType { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual DictionaryInterval VisitInterval { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
