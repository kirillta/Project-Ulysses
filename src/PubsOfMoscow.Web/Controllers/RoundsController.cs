using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubsOfMoscow.Web.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PubsOfMoscow.Web.Controllers
{
    [Route("api/[controller]")]
    public class RoundsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Round> Get()
        {
            return new List<Round>
            {
                new Round
                {
                    Number = 1,
                    Pubs = new List<Pub>
                    {
                        new Pub
                        {
                            Address = "Сущёвская улица, 9",
                            Title = "Tipsy Pub"
                        },
                        new Pub
                        {
                            Address = "Новослободская улица, 18",
                            Title = "Grace O’Malley"
                        }
                    }
                }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
