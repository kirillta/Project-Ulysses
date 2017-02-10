using System;
using System.Threading.Tasks;

namespace PubsOfMoscow.Web.Services
{
    public interface IRouteManager
    {
        Task ChoosePub(int id, DateTime? targetTime = null);
    }
}