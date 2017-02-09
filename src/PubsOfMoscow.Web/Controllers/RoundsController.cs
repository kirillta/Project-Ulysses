using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PubsOfMoscow.Web.Data;
using PubsOfMoscow.Web.Models;

namespace PubsOfMoscow.Web.Controllers
{
    [Route("api/[controller]")]
    public class RoundsController : ControllerBase
    {
        public RoundsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("close")]
        public void Close([FromBody] int roundNumber)
        {

        }


        [HttpGet]
        public IEnumerable<Round> Get()
            => _context.Rounds.Include("Pubs").ToList();

        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost("open")]
        public void Open([FromBody] int roundId, int pubId)
        {
            if (roundId < 1 || pubId < 1)
                return;


        }


        private readonly ApplicationDbContext _context;
    }
}
