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


        [HttpGet]
        public IEnumerable<Round> Get()
            => _context.Rounds.Include("Pubs").ToList();


        private readonly ApplicationDbContext _context;
    }
}
