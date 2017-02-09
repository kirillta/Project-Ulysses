using System.Collections.Generic;

namespace PubsOfMoscow.Web.Models
{
    public class Round
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public int Number { get; set; }

        public ICollection<Pub> Pubs { get; set; }
    }
}
