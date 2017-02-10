using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubsOfMoscow.Web.Data;
using PubsOfMoscow.Web.Models;

namespace PubsOfMoscow.Web.Controllers
{
    [Route("api/[controller]")]
    public class CongratulationsController : ControllerBase
    {
        public CongratulationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<string> Get() 
            => _context.Congratulations
                .Where(c => c.IsApproved)
                .Select(c => c.Content)
                .ToList();


        [HttpPost]
        public async Task Post([FromBody] Congratulation congratulation)
        {
            if (string.IsNullOrWhiteSpace(congratulation.Content))
                return;

            congratulation.Date = DateTime.Now;
            congratulation.IsApproved = false;

            _context.Congratulations.Add(congratulation);
            await _context.SaveChangesAsync();
        }


        private readonly ApplicationDbContext _context;
    }
}
