using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using VB.Infrastructure.Models;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Tests
{
    [TestClass]
    public class SerializerTest
    {
        private Serializer _target;

        private DateTime _date;
        private Guid _id;
        private List<Coach> _expectedCoachList;
        private Coach _expectedCoach;
        private Guid _teamId;
        private Guid _coachId;

       


        // MatchResult DeSerilize 
        // Coatch DeSerilize 

        [TestInitialize]
        public void InIt()
        {
            _target = new Serializer();  
            _date = DateTime.Now;
            _id = Guid.NewGuid();

            _teamId = Guid.NewGuid();
            _coachId = Guid.NewGuid();
        }


        [TestMethod]
        public void DeSerialize_StringCoachList_ReturnCoachList()
        {


            var coachesString = CreateCoachesStr();
            var expectedCoach = CreateCoaches();


            var actual = _target.DeSerialize<List<Coach>>(coachesString);
            Assert.AreEqual(expectedCoach.TeamId,actual.ElementAt(0).TeamId);
            Assert.AreEqual(expectedCoach.Name,actual.ElementAt(0).Name);
            Assert.AreEqual(expectedCoach.Date,actual.ElementAt(0).Date);
            Assert.AreEqual(expectedCoach.Mail,actual.ElementAt(0).Mail);
            Assert.AreEqual(expectedCoach.PhoneNumber,actual.ElementAt(0).PhoneNumber);

        }



        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void DeSerilize_StringToString_Throw()
        {
            _target.DeSerialize<string>("test");
        }
//
//        [TestMethod]
//        public void DeSerilize_PlayerStringToPlayerObj()
//        {
//            var expectedPlayer = CreatePlayer();
//            var playerStr = CreatePlayerStr();
//            var actual = _target.DeSerialize<Player>(playerStr);
//            Assert.AreEqual(expectedPlayer.Id, actual.Id);
//            Assert.AreEqual(expectedPlayer.Age, actual.Age);
//        } 
        
        [TestMethod]
        public void DeSerilize_MatchStringToMatchObj()
        {
            var expectedMatch = CreateMatch();
            var matchStr = CreateMatchStr();
            var actual = _target.DeSerialize<Match>(matchStr);
            Assert.AreEqual(expectedMatch.Place, actual.Place);
            Assert.AreEqual(expectedMatch.Name, actual.Name);
        }

        public string CreateMatchStr()
        {
            var matchStr =  @"{
            ""Place"" : ""h"",
            ""Date"": "+ JsonConvert.SerializeObject(_date) + @",
            ""Id"": "+ JsonConvert.SerializeObject(_id) + @",
            ""Name"" : ""ha"" }";

         return matchStr;
    }

        private Match CreateMatch()
        {
            var match= new Match()
            { Place = "h",
             Date = _date,
             Id = _id,
             Name = "ha"};

            return match;
        }

        public string CreateCoachesStr()
        {
             var coaches = 
                @"[
                    {
	                    ""TeamId"":" + JsonConvert.SerializeObject(_teamId) +@",
	                    ""PhoneNumber"":""052122221"",
	                    ""Mail"":""gfjhfkjghfkgh"",
	                    ""Date"": " + JsonConvert.SerializeObject(_date) + @",
	                    ""Id"":" + JsonConvert.SerializeObject(_coachId) +@",
	                    ""Name"":""Haim Kessel""
                    }
                ]";

             return coaches;
        }

        private Coach CreateCoaches()
        {
            var coaches = new Coach
            {
                TeamId = _teamId,
                PhoneNumber = "052122221",
                Mail = "gfjhfkjghfkgh",
                Date = _date,
                Id = _coachId,
                Name = "Haim Kessel"
            };
            return coaches;
        }




//        [TestMethod]
//        public void Serilize_PlayerObjToPlayerString()
//        {
//            var playerObj = CreatePlayer();
//            var expectedPlayerStr = CreatePlayerStr();
//            var actual = _target.Serialize(playerObj);
//            Assert.AreEqual(expectedPlayerStr,actual);
//
//        }
//
//        public string CreatePlayerStr()
//        {
//            var playerStr = @"{
//                ""Age"": 24,
//                ""Number"": 5,
//                ""Height"": 187,
//                ""Amplua"": ""Attack"",
//                ""PhoneNumber"": ""0542189500"",
//                ""Mail"": ""yelena.krop@gmail.com"",
//                ""Date"": " + JsonConvert.SerializeObject(_date) + @",
//                ""Id"":" + JsonConvert.SerializeObject(_id) + @",
//                ""Name"": ""Elena Krop""
//            }";
//            
//            playerStr = playerStr.Replace(Environment.NewLine, "");
//            playerStr = playerStr.Replace(" ", "");
//            playerStr = playerStr.Replace("ElenaKrop", "Elena Krop");
//     
//            return playerStr;
//        }
//        
//        private Player CreatePlayer()
//        {
//            var player = new Player()
//            {
//                Name = "Elena Krop",
//                Age = 24,
//                Number = 5,
//                Height = 187,
//                Amplua = "Attack",
//                PhoneNumber = "0542189500",
//                Mail = "yelena.krop@gmail.com",
//                Date = _date,
//                Id = _id,
//            };
//            return player;
//        }
    }
}
