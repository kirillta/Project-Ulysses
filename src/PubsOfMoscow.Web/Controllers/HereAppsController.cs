using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PubsOfMoscow.Web.Models.Here;

namespace PubsOfMoscow.Web.Controllers
{
    [Route("api/[controller]")]
    public class HereAppsController : ControllerBase
    {
        public HereAppsController(IOptions<HereAppOptions> options)
        {
            _options = options;
        }


        [DisableCors]
        [HttpGet]
        public string[] Get()
        {
            var path = HttpContext.Request.PathBase;
            return new[] {_options.Value.Id, _options.Value.Code};
        }


        private readonly IOptions<HereAppOptions> _options;
    }
}
