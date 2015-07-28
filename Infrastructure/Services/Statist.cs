using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VB.Infrastructure.Models;
using VB.Infrastructure.Statistic;

namespace VB.Infrastructure.Services
{
    public class Statist 
    {
        private readonly IPersistentService _persistentService;

        public Statist(IPersistentService persistentService)
        {
            _persistentService = persistentService;
        }


//        public List<MatchStatistic> GetMatchStatistic()
//        {
//            var matchList = _persistentService.GetObjectList("match.json");
//
//            var matchResultList = _persistentService.GetObjectList("matchResult.json");
//
////            var matchStatistic = from a in matchList
////                join b in matchResultList on a.Id equals b.Id
////                select new {a.Id, a.Name, a.Place, a.Date, b.SetResults};  
////
////            return matchStatistic;
////            
//        }

    }
}
