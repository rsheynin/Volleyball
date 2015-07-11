using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Models
{
    [ExcludeFromCodeCoverage]
    public class ClientManager: IModel
    {

        private readonly ISerializer _serializer;
        private readonly IRemoteFile _remoteFile;

       public ClientManager (ISerializer serializer, ILinqService linqService, IRemoteFile remoteFile)
        {
           _serializer = serializer;
           _remoteFile = remoteFile;
            Id = Guid.NewGuid();
            Persistent = new PersistentService(serializer, linqService, remoteFile);
        }
        public Guid Id { get; set; } 
        public PersistentService Persistent;

        /// <exception cref="IOException">An I/O error occurred. </exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
        /// <exception cref="OverflowException"><paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
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


        /// <exception cref="IOException">An I/O error occurred. </exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        /// <exception cref="FormatException"><paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
        /// <exception cref="OverflowException"><paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
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
