﻿using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class LandmarkModel
    {

        public DictionaryCity City { get; set; }

        public DictionaryTicketType TicketType { get; set; }

        public DictionaryInterval VisitInterval { get; set; }

        public DictionaryLandmarkType LandmarkType { get; set; }

        public Images Image { get; set; }

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

        public LandmarkModel()
        {
            this.City = new DictionaryCity();
            this.TicketType = new DictionaryTicketType();
            this.VisitInterval = new DictionaryInterval();
            this.LandmarkType = new DictionaryLandmarkType();
            this.Image = new Images();
        }
    }
}