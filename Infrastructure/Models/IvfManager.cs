using System;
using Volleyball.Models;
using Volleyball.Services;

namespace Volleyball
{
    public class IvfManager: IModel
    {
        public Guid Id { get; set; }
        public static PersistentService Persistent;

        public static Player DeclarePlayer(string name, int age, int number, int height, string amplua, int phonenumber, string mail)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = age,
                Number = number,
                Height = height,
                Amplua = amplua,
                PhoneNumber= phonenumber,
                Mail= mail,
                Date = DateTime.Now
            };

            //var str = _serializer.Serialize(player);
            //Console.WriteLine(str);

            Persistent.Saveobject(player, "player.json");

            return player;
        }
        public static Team DeclareTeam(string name, int league)
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = name,
                League = league,
                Date = DateTime.Now
            };

            Persistent.Saveobject(team, "team.json");

            return team;
        }

    }
}
