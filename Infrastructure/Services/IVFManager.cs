
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VB.Infrastructure.Models;

namespace VB.Infrastructure.Services
{
    public class IVFManager : IIVFManager
    {
        private readonly IPersistentService _persistentService;

        public IVFManager(IPersistentService persistentService)
        {
            _persistentService = persistentService;
        }

        public Player DeclarePlayer(string name, int age, int number, int height, string amplua, string phonenumber, string mail)
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = age,
                Number = number,
                Height = height,
                Amplua = amplua,
                PhoneNumber = phonenumber,
                Mail = mail,
                Date = DateTime.Now
            };

            _persistentService.Saveobject(player, "player.json");

            return player;

        }

        public Team DeclareTeam(string name, int league)
        {

            var team = new Team()
            {
                Id = Guid.NewGuid(),
                Name = name,
                League = league,
                Date = DateTime.Now
            };

            _persistentService.Saveobject(team, "team.json");

            return team;
        }

        public Match DeclareMatch(string name, string place)
        {
            var match = new Match()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Place = place,
                Date = DateTime.Now
            };

            _persistentService.Saveobject(match, "match.json");

            return match;
        }

        public MatchResult DeclareResult(string name, Guid matchId, List<int> setResults)
        {
            var result = new MatchResult()
            {
                Id = Guid.NewGuid(),
                Name = name,
                MatchId = matchId,
                SetResults = setResults,

            };


            _persistentService.Saveobject(result, "matchResult.json");

            return result;
        }

        public Coach DeclareCoach(string name, Guid teamId,  string phonenumber, string mail)
        {
            var coach = new Coach()
            {
                Id = Guid.NewGuid(),
                Name = name,
                TeamId = Guid.NewGuid(),
                PhoneNumber = phonenumber,
                Mail = mail,
                Date = DateTime.Now

            };

            _persistentService.Saveobject(coach, "coach.json");

            return coach;
        }


    }

    public interface IIVFManager
    {
        Player DeclarePlayer(string name, int age, int number, int height, string amplua, string phonenumber,
            string mail);


        Team DeclareTeam(string name, int league);

        Match DeclareMatch(string name, string place);

        MatchResult DeclareResult(string name, Guid matchId, List<int> setResults);

        Coach DeclareCoach(string name, Guid teamId, string phonenumber, string mail);


    }
}