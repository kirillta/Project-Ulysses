using System;

namespace PubsOfMoscow.Web.Models
{
    public class Pub
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime EstimateStartTime { get; set; }
        public string IconUrl { get; set; }
        public bool IsChosen { get; set; }
        public decimal Latitude { get; set; }
        public string LogoUrl { get; set; }
        public decimal Longitude { get; set; }
        public string Title { get; set; }

        public Round Round { get; set; }
        public int RoundId { get; set; }
    }
}
