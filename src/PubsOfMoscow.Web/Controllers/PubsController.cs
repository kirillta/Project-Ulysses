using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubsOfMoscow.Web.Data;
using PubsOfMoscow.Web.Services;

namespace PubsOfMoscow.Web.Controllers
{
    [Route("api/[controller]")]
    public class PubsController : ControllerBase
    {
        public PubsController(ApplicationDbContext context, IRouteManager manager)
        {
            _context = context;
            _manager = manager;
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpPost("{id}/choose")]
        public async Task Post(int id, [FromBody] string targetTimeString)
        {
            if (id <= 0)
                return;

            DateTime targetTime;
            var isDateTime = DateTime.TryParse(targetTimeString, out targetTime);
            if (isDateTime)
                await _manager.ChoosePub(id, targetTime);
            else
                await _manager.ChoosePub(id);
        }


        private readonly ApplicationDbContext _context;
        private readonly IRouteManager _manager;
    }
}
