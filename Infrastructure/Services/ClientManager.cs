using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using VB.Infrastructure.Models;

namespace VB.Infrastructure.Services
{
    [ExcludeFromCodeCoverage]
    public class ClientManager
    {

        private readonly IIVFManager _ivfManager;

        public ClientManager(IIVFManager ivfManager)
        {
            _ivfManager = ivfManager;

        }

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

            var player = _ivfManager.DeclarePlayer(name, Convert.ToInt32(age), Convert.ToInt32(number), Convert.ToInt32(height), amplua, phonenumber, mail);

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


            var team = _ivfManager.DeclareTeam(name, Convert.ToInt32(league));


            return team;
        }
        /// <summary>
        /// Create Match By Client Data
        /// </summary>
        /// <returns>a <see cref="Match"/> details</returns>
        public Match CreateMatchByClientData()
        {
            Console.WriteLine("Enter match place, please");
            var place = Console.ReadLine();

            Console.WriteLine("Enter 2 teams name, please");
            var name = Console.ReadLine();

            var match = _ivfManager.DeclareMatch(place, name);

            CreateResultByClientData(match.Id);
            CreateResultByClientData(match.Id);
            
            return match;
        }

        public MatchResult CreateResultByClientData(Guid matchId)
        {
            Console.WriteLine("Enter Team Name, please");
            var name = Console.ReadLine();

            var setResults = new List<int>();
            int x = 1;

            do
            {
                Console.WriteLine("Enter Team point in " + x + "set or enter 'end', please");
                var pointsString = Console.ReadLine();

                if (String.Equals(pointsString, "end", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                setResults.Add(Convert.ToInt32(pointsString));

                x++;
            }
            while (x < 6);


            var result = _ivfManager.DeclareResult(name, matchId, setResults);

            return result;
        }

        /// <exception cref="IOException">An I/O error occurred. </exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        public Coach CreateCoachByClientData()
        {
            Console.WriteLine("Enter Full Coach Name, please");
            var name = Console.ReadLine();

            Console.WriteLine("Enter Coach Team, please");
            var teamname = Console.ReadLine();

            Console.WriteLine("Enter Coach e-mail address  , please");
            var email = Console.ReadLine();

            Console.WriteLine("Enter Coach phonenumber, please");
            var phonenumber = Console.ReadLine();

            var coach= _ivfManager.DeclareCoach (name, teamname, email, phonenumber);

            return coach;

        }
    }
}
 