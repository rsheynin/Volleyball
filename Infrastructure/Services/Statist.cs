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


        public IEnumerable<MatchStatistic> GetMatchStatistic()
        {
            var matchList = _persistentService.GetObjectList<List<Match>>("match.json"); 
            var resultList = _persistentService.GetObjectList<List<MatchResult>>("result.json");

            var matchStatistic = from match in matchList 
                                 join result in resultList on match.Id equals result.MatchId 
                                 select new MatchStatistic{
                                     MatchId = match.Id,
                                     MatchPlace = match.Place,
                                     MatchName = match.Name,
                                     MatchDate = match.Date,
                                     SetResults = result.SetResults
            };

            return matchStatistic;

        }



        public IEnumerable<MatchStatistic> GetMatchStatisticTest()
        {
            var matchList = _persistentService.GetObjectList("match.json") as IList<Match>;
            var resultList = _persistentService.GetObjectList("result.json") as IList<MatchResult>;

            var matchStatistic = from match in matchList
                                 join result in resultList on match.Id equals result.MatchId
                                 select new MatchStatistic
                                 {
                                     MatchId = match.Id,
                                     MatchPlace = match.Place,
                                     MatchName = match.Name,
                                     MatchDate = match.Date,
                                     SetResults = result.SetResults
                                 };

            return matchStatistic;

        }

    }
}
