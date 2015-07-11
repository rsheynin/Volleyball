using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using VB.Infrastructure.Models;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Tests
{
    [TestClass]
    public class PersistentServiceTest
    {
        private PersistentService _target;
        private ISerializer _stubSerializer;
        private ILinqService _stubLinqService;
        private IRemoteFile _stubRemoteFile;
        private Guid _id;
        private const string StringToDesirialize = "_stringToDesirialize";
        private const string SerializedString = "_serializedString";

        private IModel _stubModel;
        private List<IModel> _modelList = new List<IModel>();
        private const string Path = "File Path";
        private const string FileEmptyListString = "[]";

        [TestInitialize]
        public void InIt()
        {
            _id = Guid.NewGuid();
            _stubModel = MockRepository.GenerateStub<IModel>();
            _stubSerializer = MockRepository.GenerateStub<ISerializer>();
            _stubLinqService = MockRepository.GenerateStub<ILinqService>();
            _stubRemoteFile = MockRepository.GenerateStub<IRemoteFile>();
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
                .Return(FileEmptyListString);

            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(FileEmptyListString))
                .Return(new List<IModel>());

            var actual = _target.GetobjById<IModel>(_id,Path);

            IModel expected = null;
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

            var actual = _target.GetobjById<IModel>(_id, Path);

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
            _stubSerializer.Stub(x => x
                .DeSerialize<List<IModel>>(StringToDesirialize))
                .Return(modelList);

               _stubLinqService.Expect(x => x
                   .DeleteById(_id, modelList));

               _target.DeleteObjfromData<IModel>(_id, Path);

               _stubLinqService.VerifyAllExpectations();
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

               _target.DeleteObjfromData<IModel>(_id, Path);

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

               _target.DeleteObjfromData<IModel>(_id, Path);

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

            _target.UpdateObjinData<IModel>(_stubModel, Path);

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

            _target.UpdateObjinData<IModel>(_stubModel, Path);

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

            _target.UpdateObjinData<IModel>(_stubModel, Path);

            _stubSerializer.VerifyAllExpectations();
        } 
        
        [TestMethod]
        public void Saveobject_FileIsEmptySaveInNewObj()
        {
            _stubRemoteFile.Expect(x => x
                .ReadFileData(Path))
                .Return(FileEmptyListString);

            var modelList = new List<IModel> { _stubModel };

            _stubSerializer    .Stub(x => x
                .DeSerialize<List<IModel>>(FileEmptyListString))
                .Return(modelList);

            _stubRemoteFile.WriteFileData(Path, FileEmptyListString);
            _target.Saveobject(modelList, Path);

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
                .Return(SerializedString);

            //_stubRemoteFile.Stub(x => x
            //    .WriteFileData(Path, SerializedString));

            _target.Saveobject(_stubModel, Path);

            _stubRemoteFile.AssertWasCalled(x => x
                .WriteFileData(Path, SerializedString));

        }


        // ReadFileData
        // Serialize
        // WriteFileData


        // ReadFileData
        // DeSerialize
        // Serialize
        // WriteFileData


//        [TestMethod]
//        public void Saveobject_FileExist()
//        {
//            string str = _stringToDesirialize;
//
//            _stubRemoteFile.Stub(x => x
//                           .ReadFileData(Path))
//                           .Return(str);
//
//            _stubSerializer.Stub(x => x
//                .DeSerialize<>(str))
//                .Return(objList);
//
//
//            _stubSerializer.Stub(x => x
//               .Serialize(objList))
//               .Return(objString);
//
//            _target.Saveobject(_stubModel,Path);
//
//            _stubRemoteFile.AssertWasCalled(x => x
//              .WriteFileData(Path, "SerializedObj"));
//        }


    }
}
