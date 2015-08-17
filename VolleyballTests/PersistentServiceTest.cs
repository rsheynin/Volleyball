using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using VB.Infrastructure.DTO;
using VB.Infrastructure.Models;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Tests
{
    [TestClass]
    public class PersistentServiceTest
    {
        private PersistentService _target;
        private IPersistentService _stubPersistentService;
        private ISerializer _stubSerializer;
        private ILinqService _stubLinqService;
        private IRemoteFile _stubRemoteFile;
        private Guid _id;
        private DateTime _date;
        private const string StringToDesirialize = "_stringToDesirialize";
        private const string SerializedstringPlayerList = SERIALIZEDSTRING_PLAYER_LIST;
        private const string SERIALIZED_STRING = "_serializedString";
        private const string objName = "";
        
        private IModel _stubModel;
        private readonly List<IModel> _modelList = new List<IModel>();
        private readonly List<TeamPlayers> _teamPlayersList = new List<TeamPlayers>();
        private readonly List<PlayerDTO> _playerDtoList = new List<PlayerDTO>();
        private readonly List<Player> _playerList = new List<Player>();
        private TeamPlayers _stubTeamPlayers;
        private Player _stubPlayer;
        private PlayerDTO _stubDtoPlayer;
        private const string Path = "File Path";
        private const string FILE_EMPTY_LIST_STRING = "[]";
        private const string SERIALIZEDSTRING_PLAYER_LIST = "serializedstringPlayerList";
        private const string PLAYER_JSON_FILE_P_ATH = "player.json";

        [TestInitialize]
        public void InIt()
        {
            _id = Guid.NewGuid();
            _stubModel = MockRepository.GenerateStub<IModel>();
            _stubTeamPlayers = MockRepository.GenerateStub<TeamPlayers>();
            _stubDtoPlayer = MockRepository.GenerateStub<PlayerDTO>();
            _stubPlayer = MockRepository.GenerateStub<Player>();

            _stubSerializer = MockRepository.GenerateStub<ISerializer>();
            _stubLinqService = MockRepository.GenerateStub<ILinqService>();
            _stubRemoteFile = MockRepository.GenerateStub<IRemoteFile>();
            _stubPersistentService = MockRepository.GenerateStub<IPersistentService>();
            _target = new PersistentService(_stubSerializer,
                _stubLinqService,_stubRemoteFile);
        }

        /// <summary>
        /// Id not exist
        /// Id exist
        /// </summary>


        [TestMethod]
        public void GetobjById_IDNotExistInPersistent_Null()
        {
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(FILE_EMPTY_LIST_STRING);

            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(FILE_EMPTY_LIST_STRING))
                .Return(new List<IModel>());

            var actual = _target.GetobjById(_id,Path);

            IModel expected = null;
            Assert.AreEqual(expected,actual);
        }  
        
        [TestMethod]
        public void GetobjByName_NameExistInPersistent_ReturnModel()
        {
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(StringToDesirialize);

            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(_modelList);

            _stubLinqService.Stub(x => x
                .GetByName(objName,_modelList))
                .Return(_stubModel);

            var actual = _target.GetobjByName(objName,Path);

            IModel expected = _stubModel;
            Assert.AreEqual(expected,actual);
        }
         
        [TestMethod]
        public void GetobjList_ListExistInPersistent_ReturnModelList()
        {
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(StringToDesirialize);

            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(_modelList);

            var actual = _target.GetObjectList<List<IModel>>(Path);

            var expected = _modelList;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void GetobjById_IDExistInPersistent_ReturnModel()
        {
           var stringToDesirialize = "[{\"Id\":"+_id+"}]";
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(stringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(stringToDesirialize))
                .Return(modelList);

            _stubLinqService.Stub(x => x
                .GetById(_id, modelList))
                .Return(_stubModel);

            var actual = _target.GetobjById(_id, Path);

            IModel expected = _stubModel;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DeleteObjfromData_LinkService()
        {
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(StringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Expect(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

               //_stubLinqService.Expect(x => x
               //    .DeleteById(_id, modelList));

               _target.DeleteObjfromData(_id, Path);

               //_stubLinqService.VerifyAllExpectations();
            _stubSerializer.VerifyAllExpectations();

            _stubLinqService.AssertWasCalled(x => x
                .DeleteById(_id,modelList));

        }  
        
        [TestMethod]
        public void DeleteObjfromData_RemoteFile()
        {
            _stubRemoteFile.Expect(x => x
                .ReadFileData(Path))
                .Return(StringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

               _stubLinqService.Stub(x => x
                   .DeleteById(_id, modelList));

               _target.DeleteObjfromData(_id, Path);

               _stubRemoteFile.VerifyAllExpectations();
        }  
        
        [TestMethod]
        public void DeleteObjfromData_Serializer()
        {
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(StringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Expect(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

               _stubLinqService.Stub(x => x
                   .DeleteById(_id, modelList));

               _target.DeleteObjfromData(_id, Path);

               _stubSerializer.VerifyAllExpectations();
        } 
        
        [TestMethod]
        public void UpdateObjData_LinqService()
        {
            _stubRemoteFile.Stub(x => x
               .ReadFileData(Path))
               .Return(StringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

            _stubLinqService.Expect(x => x
                .UpdateById(_stubModel, modelList));

            _target.UpdateObjinData(_stubModel, Path);

            _stubLinqService.VerifyAllExpectations();
        }

        [TestMethod]
        public void UpdateObjData_RemoteFile()
         {
            _stubRemoteFile.Expect(x => x
               .ReadFileData(Path))
               .Return(StringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

            _stubLinqService.Stub(x => x
                .UpdateById(_stubModel, modelList));

            _target.UpdateObjinData(_stubModel, Path);

            _stubRemoteFile.VerifyAllExpectations();
        }
        
        [TestMethod]
        public void UpdateObjData_Serializer()
        {
            _stubRemoteFile.Stub(x => x
               .ReadFileData(Path))
               .Return(StringToDesirialize);

            var modelList = new List<IModel> { _stubModel };
            _stubSerializer.Expect(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

            _stubLinqService.Stub(x => x
                .UpdateById(_stubModel, modelList));

            _target.UpdateObjinData(_stubModel, Path);

            _stubSerializer.VerifyAllExpectations();
        } 
        
        [TestMethod]
        public void Saveobject_FileIsEmptySaveInNewObj()
        {
            _stubRemoteFile.Expect(x => x
                .ReadFileData(Path))
                .Return(FILE_EMPTY_LIST_STRING);

            var modelList = new List<IModel> { _stubModel };

            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(FILE_EMPTY_LIST_STRING))
                .Return(modelList);

            _stubRemoteFile.WriteFileData(Path, FILE_EMPTY_LIST_STRING);
            _target.Saveobject(_stubModel, Path);

            _stubRemoteFile.VerifyAllExpectations();
        } 
        
        [TestMethod]
        public void Saveobject_FileExist_AddToFile()
        {
            
            _stubRemoteFile.Stub(x => x
                .ReadFileData(Path))
                .Return(StringToDesirialize);


            _stubSerializer.Expect(x => x
               .DeSerialize<List<IModel>>(StringToDesirialize))
               .Return(_modelList);

            _modelList.Add(_stubModel);

            _stubSerializer.Stub(x => x
                .Serialize(_modelList))
                .Return(SERIALIZED_STRING);

            //_stubRemoteFile.Stub(x => x
            //    .WriteFileData(Path, SerializedString));

            _target.Saveobject(_stubModel, Path);

            _stubRemoteFile.AssertWasCalled(x => x
                .WriteFileData(Path, SERIALIZED_STRING));

        }

        [TestMethod]
        public void SaveTeamPlayers_FileIsEmptySaveInNew()
        {
            _stubRemoteFile.Expect(x => x
               .ReadFileData(Path))
               .Return(FILE_EMPTY_LIST_STRING);

            var teamPlayersList = new List<TeamPlayers> { _stubTeamPlayers };

            _stubSerializer.Stub(x => x
                .DeSerialize<List<TeamPlayers>>(FILE_EMPTY_LIST_STRING))
                .Return(teamPlayersList);

            _stubRemoteFile.WriteFileData(Path, FILE_EMPTY_LIST_STRING);
            _target.Saveobject(_stubTeamPlayers, Path);

            _stubRemoteFile.VerifyAllExpectations();
        }

         [TestMethod]
        public void SaveTeamPlayers_FileExist_AddToFile()
        {
            _stubRemoteFile.Expect(x => x
               .ReadFileData(Path))
               .Return(FILE_EMPTY_LIST_STRING);

            _stubSerializer.Expect(x => x
               .DeSerialize<List<TeamPlayers>>(StringToDesirialize))
               .Return(_teamPlayersList);

            _teamPlayersList.Add(_stubTeamPlayers);

            _stubSerializer.Stub(x => x
                .Serialize(_teamPlayersList))
                .Return(SERIALIZED_STRING);

            _target.Saveobject(_stubTeamPlayers, Path);

            _stubRemoteFile.AssertWasCalled(x => x
                .WriteFileData(Path, SERIALIZED_STRING));
        }

         [TestMethod]
         public void SaveDTOobject_FileIsEmptySaveInNew()
         {
             _stubRemoteFile.Stub(x => x
              .ReadFileData(Path))
              .Return(FILE_EMPTY_LIST_STRING);

           
             _stubDtoPlayer = new PlayerDTO()
             {
                 Name = "",
                 Age = 0,
                 Amplua = "",
                 Height = 0,
                 Number = 0,
                 Mail = "",
                 PhoneNumber = ""
             };
             var expectedPlayersList = new List<Player>();
             _stubPlayer = new Player()
             {
                 Name = _stubDtoPlayer.Name,
                 Age = _stubDtoPlayer.Age,
                 Amplua = _stubDtoPlayer.Amplua,
                 Height = _stubDtoPlayer.Height,
                 Number = _stubDtoPlayer.Number,
                 Mail = _stubDtoPlayer.Mail,
                 PhoneNumber = _stubDtoPlayer.PhoneNumber
             };             
              expectedPlayersList.Add(_stubPlayer);

             _stubSerializer.Stub(x => x
                 .Serialize(Arg<List<Player>>.Matches(actualPlayers => 
                     CheckPlayers(actualPlayers, expectedPlayersList))))
               .Return(SERIALIZEDSTRING_PLAYER_LIST);

              _target.SaveDTOobject(_stubDtoPlayer,Path);


             //In this case is same
              _stubRemoteFile.AssertWasCalled(x => x
                  .WriteFileData(Path, SERIALIZEDSTRING_PLAYER_LIST));
             //_stubRemoteFile.AssertWasCalled(x => x
             //     .WriteFileData(Arg<string>.Is.Equal(PLAYER_JSON_FILE_P_ATH),
             //     Arg<string>.Is.Equal(SERIALIZEDSTRING_PLAYER_LIST)));

             
         }

        private bool CheckPlayers(List<Player> actualPlayers, List<Player> expectedPlayersList)
        {
            Assert.AreEqual(expectedPlayersList.Count,actualPlayers.Count);
            for (int i = 0; i < actualPlayers.Count; i++)
            {
                Assert.AreEqual(expectedPlayersList.ElementAt(i).Name, actualPlayers.ElementAt(i).Name);
                Assert.AreEqual(expectedPlayersList.ElementAt(i).Age, actualPlayers.ElementAt(i).Age);
                Assert.AreEqual(expectedPlayersList.ElementAt(i).Amplua, actualPlayers.ElementAt(i).Amplua);
                Assert.AreEqual(expectedPlayersList.ElementAt(i).Height, actualPlayers.ElementAt(i).Height);
                Assert.AreEqual(expectedPlayersList.ElementAt(i).Number, actualPlayers.ElementAt(i).Number);
                Assert.AreEqual(expectedPlayersList.ElementAt(i).Mail, actualPlayers.ElementAt(i).Mail);
                Assert.AreEqual(expectedPlayersList.ElementAt(i).PhoneNumber, actualPlayers.ElementAt(i).PhoneNumber);
                
              
            }
            return true;
        }

   

         [TestMethod]
         public void SaveDTOobject_FileExist_AddToFile()
         {
             _stubRemoteFile.Stub(x => x
              .ReadFileData(Path))
              .Return(StringToDesirialize);

             _stubDtoPlayer = new PlayerDTO()
             {
                 Name = "",
                 Age = 0,
                 Amplua = "",
                 Height = 0,
                 Number = 0,
                 Mail = "",
                 PhoneNumber = ""
             };
           
             _stubPlayer = new Player()
             {
                 Name = _stubDtoPlayer.Name,
                 Age = _stubDtoPlayer.Age,
                 Amplua = _stubDtoPlayer.Amplua,
                 Height = _stubDtoPlayer.Height,
                 Number = _stubDtoPlayer.Number,
                 Mail = _stubDtoPlayer.Mail,
                 PhoneNumber = _stubDtoPlayer.PhoneNumber
             };
            
             _stubSerializer.Stub(x => x
                .DeSerialize<List<Player>>(StringToDesirialize))
                .Return(_playerList);

             _playerList.Add(_stubPlayer);

             _stubSerializer.Stub(x => x
                   .Serialize(Arg<List<Player>>.Matches(actualPlayers =>
                       CheckPlayers(actualPlayers, _playerList))))
                 .Return(SERIALIZEDSTRING_PLAYER_LIST);

             _target.SaveDTOobject(_stubDtoPlayer, Path);

            
             _stubRemoteFile.AssertWasCalled(x => x
                 .WriteFileData(Path, SERIALIZEDSTRING_PLAYER_LIST));
            
         }

         private bool CheckPlayers_fileExist(List<Player> actualPlayers, List<Player> __playerList)
         {
             Assert.AreEqual(_playerList.Count, actualPlayers.Count);
             for (int i = 0; i < actualPlayers.Count; i++)
             {
                 Assert.AreEqual(_playerList.ElementAt(i).Name, actualPlayers.ElementAt(i).Name);
                 Assert.AreEqual(_playerList.ElementAt(i).Age, actualPlayers.ElementAt(i).Age);
                 Assert.AreEqual(_playerList.ElementAt(i).Amplua, actualPlayers.ElementAt(i).Amplua);
                 Assert.AreEqual(_playerList.ElementAt(i).Height, actualPlayers.ElementAt(i).Height);
                 Assert.AreEqual(_playerList.ElementAt(i).Number, actualPlayers.ElementAt(i).Number);
                 Assert.AreEqual(_playerList.ElementAt(i).Mail, actualPlayers.ElementAt(i).Mail);
                 Assert.AreEqual(_playerList.ElementAt(i).PhoneNumber, actualPlayers.ElementAt(i).PhoneNumber);


             }
             return true;
         }

    }
}
