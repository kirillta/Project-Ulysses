using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PubsOfMoscow.Web.Models;

namespace PubsOfMoscow.Web.Services
{
    public interface ILocationManager
    {
        Task<TimeSpan> GetTravelTime(Dictionary<decimal, decimal> coordinates);
    }
}