using System;
using Microsoft.AspNetCore.Mvc;

namespace PubsOfMoscow.Web.Controllers
{
    [Route("api/[controller]")]
    public class TimeController : Controller
    {
        [HttpGet]
        public DateTime Get() 
            => DateTime.Now;
    }
}
