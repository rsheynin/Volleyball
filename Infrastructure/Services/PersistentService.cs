using System;
using System.Collections.Generic;
using System.Linq;
using VB.Infrastructure.Models;

namespace VB.Infrastructure.Services
{
    public class PersistentService : IPersistentService
    {
        private readonly ISerializer _serializer;
        private readonly ILinqService _linqService;
        private readonly IRemoteFile _remoteFile;

        public PersistentService(ISerializer serializer,
            ILinqService linqService, IRemoteFile remoteFile)
        {
            _serializer = serializer;
            _linqService = linqService;
            _remoteFile = remoteFile;
        }

        /// <summary>
        /// Add obj in existing objList or create new objList and Add obj
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public void Saveobject(IModel obj, string path)
        {
            
            var str = _remoteFile.ReadFileData(path);
            List<IModel> objList;
            if (str == "[]")
            {
                objList = new List<IModel> { obj };
            }
            else
            {
                objList = _serializer.DeSerialize<List<IModel>>(str);
                objList.Add(obj);
            }

            var objStr = _serializer.Serialize(objList);
            _remoteFile.WriteFileData(path, objStr);
        }

        /// <summary>
        ///Select object by Id from list of objects  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public IModel GetobjById(Guid id,string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<IModel>>(str);
            var model = _linqService.GetById(id, objList);
            return model;
        }

        /// <summary>
        /// Select object by name from list of objects
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public IModel GetobjByName(string objName, string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<IModel>>(str);
            var model = _linqService.GetByName(objName, objList);
            return model;
        }






        /// <summary>
        /// Read all file, get object from it by Id and Delete obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        public void DeleteObjfromData(Guid id, string path) 
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<IModel>>(str);
            _linqService.DeleteById(id, objList);
            var objListStr = _serializer.Serialize(objList);
            _remoteFile.WriteFileData(path, objListStr); 
        }

        /// <summary>
        /// Read all file and update object in it by Id 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public void UpdateObjinData(IModel obj, string path) 
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<IModel>>(str);
            _linqService.UpdateById(obj, objList);
            var objListStr = _serializer.Serialize(objList);
            _remoteFile.WriteFileData(path, objListStr);
        }
    }
}
