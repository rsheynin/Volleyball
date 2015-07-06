using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volleyball.Models;
using Volleyball.Services;

namespace Volleyball
{
    [ExcludeFromCodeCoverage]
    public class ClientManager: IModel
    {

        private readonly ISerializer _serializer;
        private readonly ILinqService _linqService;
        private readonly IRemoteFile _remoteFile;

       public ClientManager (ISerializer serializer, ILinqService linqService, IRemoteFile remoteFile)
        {
            _serializer = serializer;
            _linqService = linqService;
            _remoteFile = remoteFile;
            Id = Guid.NewGuid();
            Persistent = new PersistentService(serializer, _linqService, remoteFile);
        }
        public Guid Id { get; set; } 
        public PersistentService Persistent;

        public Player CreatePlayerByClientData()
        {
            Console.WriteLine("Enter full player Name, please");
            var name = Console.ReadLine();

            Console.WriteLine("Enter  player Age, please");
            var age = Console.ReadLine();

            Console.WriteLine("Enter player Number, please");
            var number = Console.ReadLine();

            Console.WriteLine("Enter player Height, please");
            var height = Console.ReadLine();

            Console.WriteLine("Enter  player Amplua, please");
            var amplua = Console.ReadLine();

            Console.WriteLine("Enter  player PhoneNumber, please");
            var phonenumber = Console.ReadLine();

            Console.WriteLine("Enter  player E-mail, please");
            var mail = Console.ReadLine();

            var player = IvfManager.DeclarePlayer(name, Convert.ToInt32(age), Convert.ToInt32(number), Convert.ToInt32(height), amplua, Convert.ToInt32(phonenumber), mail);

            return player;
        }


        public Team CreateTeamByClientData()
        {
            Console.WriteLine("Enter full team Name, please");
            var name = Console.ReadLine();

            Console.WriteLine("Enter  teamLeague, please");
            var league = Console.ReadLine();


            var team = IvfManager.DeclareTeam(name, Convert.ToInt32(league));


            return team;
        }
    }
}
