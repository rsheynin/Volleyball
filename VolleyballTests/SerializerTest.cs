using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Volleyball.Models;
using Volleyball.Services;

namespace VolleyballTests
{
    [TestClass]
    public class SerializerTest
    {
        private Serializer _target;

        private DateTime _date;
        private Guid _id;


        [TestInitialize]
        public void InIt()
        {
            _target = new Serializer();  
            _date = DateTime.Now;
            _id = Guid.NewGuid();
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void DeSerilize_StringToString_Throw()
        {
            _target.DeSerialize<string>("test");
        }

        [TestMethod]
        public void DeSerilize_PlayerStringToPlayerObj()
        {
            var expectedPlayer = CreatePlayer();
            var playerStr = CreatePlayerStr();
            var actual = _target.DeSerialize<Player>(playerStr);
            Assert.AreEqual(expectedPlayer.Id, actual.Id);
            Assert.AreEqual(expectedPlayer.Age, actual.Age);
        }

        [TestMethod]
        public void Serilize_PlayerObjToPlayerString()
        {
            var playerObj = CreatePlayer();
            var expectedPlayerStr = CreatePlayerStr();
            var actual = _target.Serialize(playerObj);
            Assert.AreEqual(expectedPlayerStr,actual);

        }

        private string CreatePlayerStr()
        {
            var playerStr = @"{
                ""Name"": ""Elena Krop"",
                ""Age"": 24,
                ""Number"": 5,
                ""Height"": 187,
                ""Amplua"": ""Attack"",
                ""PhoneNumber"": 542189500,
                ""Mail"": ""yelena.krop@gmail.com"",
                ""Date"": " + JsonConvert.SerializeObject(_date) + @",
                ""Id"":" + JsonConvert.SerializeObject(_id) + @"
            }";
            
            playerStr = playerStr.Replace(System.Environment.NewLine, "");
            playerStr = playerStr.Replace(" ", "");
            playerStr = playerStr.Replace("ElenaKrop", "Elena Krop");
     
            return playerStr;
        }
        
        private Player CreatePlayer()
        {
            var player = new Player()
            {
                Age = 24,
                Amplua = "Attack",
                Date = _date,
                Height = 187,
                Id = _id,
                Name = "Elena Krop",
                Number = 5,
                PhoneNumber = 542189500,
                Mail = "yelena.krop@gmail.com"
            };
            return player;
        }
    }
}
