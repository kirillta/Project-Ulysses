namespace PubsOfMoscow.Web.Models
{
    public class Pub
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string EstimateStartTime { get; set; }
        public bool IsChosen { get; set; }
        public string Title { get; set; }

        public Round Round { get; set; }
        public int RoundId { get; set; }
    }
}
