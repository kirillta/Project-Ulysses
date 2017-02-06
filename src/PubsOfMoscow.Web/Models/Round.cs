using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubsOfMoscow.Web.Models
{
    public class Round
    {
        public bool IsDone { get; set; }
        public int Number { get; set; }
        public List<Pub> Pubs { get; set; }
    }
}
