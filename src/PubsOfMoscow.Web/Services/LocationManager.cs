using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PubsOfMoscow.Web.Models.Here;

namespace PubsOfMoscow.Web.Services
{
    public class LocationManager : ILocationManager
    {
        public LocationManager(HttpClient client, IOptions<HereAppOptions> options)
        {
            _client = client;
            _options = options;
        }


        public async Task<TimeSpan> GetTravelTime(Dictionary<decimal, decimal> coordinates)
        {
            var url = ConstructRequestUri(coordinates);
            using (var httpResponse = await _client.GetAsync(url))
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                var response = GetResponse(content);
                return TimeSpan.FromSeconds(response.Response.Route.First().Summary.TravelTime);
            }
        }


        private Uri ConstructRequestUri(Dictionary<decimal, decimal> coordinates)
        {
            var baseUrl = "https://route.api.here.com";
            var apiVersion = "/routing/7.2/";
            var resource = "calculateroute";
            var format = "json";
            var appId = _options.Value.Id;
            var appCode = _options.Value.Code;
            var mode = "shortest;pedestrian";
            var representation = "overview";
            var walkSpeed = 1.5.ToString(CultureInfo.InvariantCulture);

            var waypoints = string.Empty;
            var i = 0;
            foreach (var pair in coordinates)
            {
                waypoints += GetWaypoint(i, pair.Key, pair.Value);
                i++;
            }

            var template = $"{baseUrl}{apiVersion}{resource}.{format}?app_id={appId}&app_code={appCode}&mode={mode}&representation={representation}&walkSpeed={walkSpeed}{waypoints}";

            return new Uri(template, UriKind.Absolute);
        }


        private HereResponse GetResponse(string responseContent) 
            => JsonConvert.DeserializeObject<HereResponse>(responseContent);


        private string GetWaypoint(int index, decimal latitude, decimal longitude) 
            => $"&waypoint{index}=geo!{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}";


        private readonly HttpClient _client;
        private readonly IOptions<HereAppOptions> _options;
    }
}
