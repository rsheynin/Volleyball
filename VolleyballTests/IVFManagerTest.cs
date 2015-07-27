using System;
using System.IO;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using VB.Infrastructure.Models;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Tests
{

    /// <summary>
    /// Testing IVFManager Declareobject functions
    /// </summary>
    [TestClass]
    public class IVFManagerTest
    {

        private IVFManager _target;
        private IPersistentService _stubPersistentService;


        private Match _expectedMatch;
        private MatchResult _expectedresult;
        private Coach _expectedCoach;

        private readonly Guid _teamid = Guid.NewGuid();
        private IModel _stubModel;
        private Team _team;


        [TestInitialize]
        public void InIt()
        {
            _stubModel = MockRepository.GenerateStub<IModel>();
            _stubModel.Id = _teamid;

        }

        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> is a directory.-or- <paramref name="path" /> specified a read-only file. </exception>
        public void Cleanup()
        {
            File.Delete("player.json");
            File.Delete("teeam.json");
            File.Delete("match.json");
            File.Delete("matchResult.json");
            File.Delete("coach.json");

        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        /// <summary>
        ///  DeclarePlayer CheckArguments
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void DeclarePlayer_CheckArguments_Player()
        {

            //  ################ First Way
            var expectedPlayer = new Player()
            {
                Name = "",
                Age = 0,
                Number = 0,
                Height = 0,
                Amplua = "",
                PhoneNumber = "",
                Mail = ""
            };

            var actual = _target.DeclarePlayer(expectedPlayer.Name, expectedPlayer.Age, expectedPlayer.Number,
                expectedPlayer.Height,
                expectedPlayer.Amplua, expectedPlayer.PhoneNumber, expectedPlayer.Mail);

            Assert.AreEqual(expectedPlayer.Name, actual.Name);
            Assert.AreEqual(expectedPlayer.Age, actual.Age);
            Assert.AreEqual(expectedPlayer.Number, actual.Number);
            Assert.AreEqual(expectedPlayer.Height, actual.Height);
            Assert.AreEqual(expectedPlayer.Amplua, actual.Amplua);
            Assert.AreEqual(expectedPlayer.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expectedPlayer.Mail, actual.Mail);

        }

        [TestMethod]
        public void DeclarePlayer_Saveobject_Player()

        {
            var expectedPlayer = new Player()
            {
                Name = "",
                Age = 0,
                Number = 0,
                Height = 0,
                Amplua = "",
                PhoneNumber = "",
                Mail = ""
            };

            _target.DeclarePlayer(expectedPlayer.Name, expectedPlayer.Age, expectedPlayer.Number,
                expectedPlayer.Height,
                expectedPlayer.Amplua, expectedPlayer.PhoneNumber, expectedPlayer.Mail);

            _stubPersistentService.AssertWasCalled(x => x
                .Saveobject(Arg<Player>.Matches(actualPlayer => CheckPlayer(expectedPlayer, actualPlayer)),
                    Arg<string>.Is.Anything //Equal("player.json")
                ));

            //_stubPersistentService.AssertWasCalled(x => x
            //    .Saveobject(
            //        Arg<Player>.Matches(actualPlayer => actualPlayer.Age == 0 ),
            //        Arg<string>.Is.Same("player.json")
            //    ));}
        }

        private bool CheckPlayer(Player expected, Player actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Age, actual.Age);
            Assert.AreEqual(expected.Number, actual.Number);
            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Amplua, actual.Amplua);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.Mail, actual.Mail);

            return true;
        }

        /// <summary>
        ///DeclareTeam_CheckArguments_Saveobject()
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void DeclareTeam_CheckArguments_Team()
        {
            var expectedTeam = new Team
            {
                Name = "",
                League = 0,

            };
            var actual = _target.DeclareTeam("", 0);

            Assert.AreEqual(expectedTeam.Name, actual.Name);
            Assert.AreEqual(expectedTeam.League, actual.League);

        }

        [TestMethod]
        public void DeclareTeam_Saveobject_Team()
        {
            var expectedTeam = new Team
            {
                Name = "",
                League = 0,

            };
            _target.DeclareTeam("", 0);

            _stubPersistentService.AssertWasCalled(x => x
                .Saveobject(Arg<Team>.Matches(actualTeam => CheckTeam(expectedTeam, actualTeam)),
                    Arg<string>.Is.Same("team.json")));

        }

        private bool CheckTeam(Team expected, Team actual)
        {

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.League, actual.League);

            return true;
        }

        [TestMethod]
        public void DeclareMatch_CheckArguments_Match()
        {
            _expectedMatch = new Match()
            {
                Place = "",
                Name = "",
            };

            var actual = _target.DeclareMatch(
                _expectedMatch.Place, _expectedMatch.Name);

            Assert.AreEqual(_expectedMatch.Place, actual.Place);
            Assert.AreEqual(_expectedMatch.Name, actual.Name);

        }

        [TestMethod]
        public void DeclareMatch_Saveobject_Match()
        {
            _expectedMatch = new Match()
            {
                Place = "",
                Name = ""
            };

            _target.DeclareMatch(_expectedMatch.Place, _expectedMatch.Name);

            _stubPersistentService.AssertWasCalled(x => x
                .Saveobject(Arg<Match>.Matches(actualMatch => CheckMatch(_expectedMatch, actualMatch)),
                    Arg<string>.Is.Same("match.json")));
        }

        private bool CheckMatch(Match expectedMatch, Match actualMatch)
        {
            Assert.AreEqual(expectedMatch.Place, actualMatch.Place);
            Assert.AreEqual(expectedMatch.Name, actualMatch.Name);

            return true;
        }
        
         [TestMethod]
        public void DeclareResult_CheckArguments_Result()
        {
            _expectedresult = new MatchResult()
            {
                Name = "",
                MatchId = Guid.NewGuid(),
                SetResults = null
            };

            var actual = _target.DeclareResult("", new Guid(), null);
            Assert.AreEqual(_expectedresult.Name, actual.Name);
            Assert.AreEqual(_expectedresult.MatchId, actual.MatchId);
            Assert.AreEqual(_expectedresult.SetResults, actual.SetResults);


        }

        [TestMethod]
        public void DeclareResult_CallSavefunction_Result()
        {
            _expectedresult = new MatchResult()
            {
                Name = "",
                MatchId = Guid.NewGuid(),
                SetResults = null
            };

            _target.DeclareResult("", new Guid(), null);

            _stubPersistentService.AssertWasCalled(x => x.
                Saveobject(Arg<MatchResult>.Matches(actualresult => CheckResult(actualresult, _expectedresult)),
                Arg<string>.Is.Same("matchResult.json")));

        }

        private bool CheckResult(MatchResult actualresult, MatchResult expectedresult)
        {
             Assert.AreEqual(expectedresult.Name, actualresult.Name);
            Assert.AreEqual(expectedresult.MatchId, actualresult.MatchId);
            Assert.AreEqual(expectedresult.SetResults, actualresult.SetResults);
            

            return true;
        }

        [TestMethod]
        public void DeclareCoach_CheckArguments_Coach()
        {
            _expectedCoach = new Coach()
            {
               Name = "",
               TeamId = _teamid,
               PhoneNumber= "",
               Mail = ""
            };

            var actual = _target.DeclareCoach("", _teamid, "", "");
            Assert.AreEqual(_expectedCoach.Name, actual.Name);
            Assert.AreEqual(_expectedCoach.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(_expectedCoach.Mail, actual.Mail);
        }



        [TestMethod]
        public void DeclareCoach_Saveobject_Coach()
        {
            _expectedCoach = new Coach()
            {
                Name = "",
                TeamId = _teamid,
                PhoneNumber = "",
                Mail = ""
            };

           

             _target.DeclareCoach("", _teamid, "", "");
          
            _stubPersistentService.AssertWasCalled(x=>x.
                Saveobject(Arg<Coach>.Matches(actualcoach=>CheckCoach(actualcoach,_expectedCoach)), Arg<string>.Is.Same("coach.json")));
        }

        private bool CheckCoach(Coach actualcoach, Coach expectedCoach)
        {
            Assert.AreEqual(actualcoach.Name, expectedCoach.Name);
            Assert.AreEqual(actualcoach.PhoneNumber, expectedCoach.PhoneNumber);
            Assert.AreEqual(actualcoach.Mail, expectedCoach.Mail);

            return true;
        }
    }

    }


