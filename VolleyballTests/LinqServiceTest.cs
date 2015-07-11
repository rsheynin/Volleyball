using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VB.Infrastructure.Models;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Tests
{
    [TestClass]
    public class LinqServiceTest
    {
        private LinqService _target;
        private Guid _id;

        [TestInitialize]
        public void InIt()
        { 
            _target = new LinqService();
            _id = Guid.NewGuid();
        }
         
        [TestMethod]
        public void GetById_IdAndEmptyCollection_ReturnNull()
        {
           var players = new List<IModel>();
           //var playersCount = players.Count;
           IModel expectedobj = null;
           var actuale = _target.GetById(_id, players);
           Assert.AreEqual(expectedobj,actuale);
        }

        public void GetBuId_IdAndCollectionWithoutSearchedId_ReturnNull()
        {
            var players = new List<IModel>();
            players.Add(new Player());
            players.Add(new Player());
            players.Add(new Player());

            IModel expectedobj = null;
            var actuale = _target.GetById(_id, players);
            Assert.AreEqual(expectedobj, actuale);
        }

        public void GetBuId_IdAndCollectionWithSearchedId_ReturnSearchedObj()
        {
            IModel expectedobj = new Player() { Id = _id };

            var players = new List<IModel>();
            players.Add(expectedobj);
            players.Add(new Player());
            players.Add(new Player());

            var actuale = _target.GetById(_id, players);
            Assert.AreEqual(expectedobj, actuale);
        }

        [TestMethod]
        public void UpdateById_IdAndCollectionWithObjToUpdate_ObjectUpdated()
        {
            var expectedObjName = "object updated";

            var objForUpdate = new Player {Id = _id,Name = "first obj"};
            List<IModel> objList = new List<IModel>();
            objList.Add(objForUpdate);
            objList.Add(new Player());
            objList.Add(new Player());

            var objToUpdate = new Player{Id = _id,Name = expectedObjName};

            _target.UpdateById(objToUpdate, objList);

            var actual = (_target.GetById(_id, objList) as Player).Name;

            Assert.AreEqual(expectedObjName,actual);
        }

        [TestMethod]
        public void UpdateById_IdAndEmptyCollection_ReturnNull()
        {
            List<IModel> objList = new List<IModel>();

            var objToUpdate = new Player { Id = _id };

            IModel expectedobj = null;

            _target.UpdateById(objToUpdate, objList);

            var actual = _target.GetById(_id, objList);

            Assert.AreEqual(expectedobj, actual);  
        }
        
        [TestMethod]
        public void UpdateById_CollectionWitoutSearchedId_ReturnNull()
        {
            List<IModel> objList = new List<IModel>();
            objList.Add(new Player());
            objList.Add(new Player());
            objList.Add(new Player());

            var objToUpdate = new Player { Id = _id };

            IModel expectedobj = null;

            _target.UpdateById(objToUpdate, objList);

            var actual = _target.GetById(_id, objList);

            Assert.AreEqual(expectedobj, actual);  
        }

        [TestMethod]
        public void DeleteById_IdAndCollectionWithObjToDelete_ObjectDeleted()
        {
            var objForDelete = new Player{ Id = _id};

            List<IModel> objList = new List<IModel>();
            objList.Add(objForDelete);
            objList.Add(new Player());
            objList.Add(new Player());

            IModel expected = null;
          
            _target.DeleteById(_id, objList);

            var actual = _target.GetById(_id, objList);

            Assert.AreEqual(expected, actual);
        } 
        
        [TestMethod]
        public void DeleteById_IdAndEmptyCollection_ReturnNull()
        {
            List<IModel> objList = new List<IModel>();

            IModel expected = null;
          
            _target.DeleteById(_id, objList);

            var actual = _target.GetById(_id, objList);

            Assert.AreEqual(expected, actual);
        }  
        
        [TestMethod]
        public void DeleteById_IdAndCollectionWithoutSearchedId_ReturnNull()
        {
            List<IModel> objList = new List<IModel>();
            objList.Add(new Player());
            objList.Add(new Player());
            objList.Add(new Player());

            IModel expected = null;
          
            _target.DeleteById(_id, objList);

            var actual = _target.GetById(_id, objList);

            Assert.AreEqual(expected, actual);
        }  

        [TestMethod]
        public void Save_ObjToSaveAndCollection_ObjectSaved()
        {
            var objForSave = new Player { Id = _id };

            List<IModel> objList = new List<IModel>();
            objList.Add(new Player());
            objList.Add(new Player());
            
            IModel expected = objForSave;

            _target.Save(objForSave,objList);

            var actual = _target.GetById(_id, objList);

            Assert.AreEqual(expected, actual);

        }
    }
}
 