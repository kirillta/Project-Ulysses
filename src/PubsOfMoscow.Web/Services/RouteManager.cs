using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PubsOfMoscow.Web.Data;
using PubsOfMoscow.Web.Models;

namespace PubsOfMoscow.Web.Services
{
    public class RouteManager : IRouteManager
    {
        public RouteManager(ApplicationDbContext context, ILocationManager manager)
        {
            _context = context;
            _manager = manager;
        }


        public async Task ChoosePub(int id, DateTime? targetTime = null)
        {
            var pub = await GetPub(id);
            if (pub == null)
                return;

            pub.IsChosen = true;
            pub.EstimateStartTime = targetTime ?? DateTime.Now;

            var round = await GetRound(pub.RoundId);
            if (round == null)
                return;

            foreach (var roundPub in round.Pubs)
            {
                if (!roundPub.IsChosen)
                    roundPub.EstimateStartTime = DateTime.MinValue;
            }

            await _context.SaveChangesAsync();
            await UpdateFurtherEstimateTimes(pub, round.Number);
        }


        private Dictionary<decimal, decimal> GetCoordinates(Pub current, Pub nextOne)
        {
            var coordinates = new Dictionary<decimal, decimal>
            {
                {current.Latitude, current.Longitude},
                {nextOne.Latitude, nextOne.Longitude}
            };

            return coordinates;
        }


        private async Task<Pub> GetPub(int id)
            => await _context.Pubs
                .FirstOrDefaultAsync(p => p.Id == id);


        private async Task<Round> GetRound(int id)
            => await GetRounds()
                .FirstOrDefaultAsync(r => r.Id == id);


        private IQueryable<Round> GetRounds()
            => _context.Rounds.Include("Pubs");


        private async Task UpdateFurtherEstimateTimes(Pub currentPub, int roundNumber)
        {
            var rounds = GetRounds()
                .Where(r => r.Number > roundNumber)
                .ToList();

            var counter = roundNumber + 1;
            while (true)
            {
                var round = rounds.FirstOrDefault(r => r.Id == counter);
                if (round == null)
                    break;

                foreach (var pub in round.Pubs)
                {
                    var coordinates = GetCoordinates(currentPub, pub);
                    var travelTime = await _manager.GetTravelTime(coordinates);
                    pub.EstimateStartTime = GetUpdatedTime(currentPub.EstimateStartTime, travelTime);
                }

                var closestPub = new Pub { EstimateStartTime = currentPub.EstimateStartTime.AddHours(24)};
                foreach (var pub in round.Pubs)
                {
                    if (pub.EstimateStartTime < closestPub.EstimateStartTime)
                        closestPub = pub;
                }

                currentPub = closestPub;
                counter++;
            }

            await _context.SaveChangesAsync();
        }


        private DateTime GetUpdatedTime(DateTime current, TimeSpan travelTime) 
            => current.AddHours(1)
                .AddMinutes(travelTime.Minutes);


        private readonly ApplicationDbContext _context;
        private readonly ILocationManager _manager;
    }
}
