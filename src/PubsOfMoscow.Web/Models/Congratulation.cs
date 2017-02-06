using System;

namespace PubsOfMoscow.Web.Models
{
    public class Congratulation
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
    }
}
