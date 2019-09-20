using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class LandmarkModel
    {
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
        public DictionaryCity cityModel { get; set; }
    }
}
