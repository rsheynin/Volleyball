using System;
using System.Collections.Generic;
using Volleyball.Models;
using System.Linq;

namespace Volleyball.Services
{
    public interface IPersistentService
    {
    }

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


        public void Saveobject<T>(T obj, string path)
        {
            var str = _remoteFile.ReadFileData(path);
            List<T> objList;
            if (str == "[]")
            {
                objList = new List<T> { obj };
            }
            else
            {
                objList = _serializer.DeSerialize<List<T>>(str);
                objList.Add(obj);
            }

            var objStr = _serializer.Serialize(objList);
            _remoteFile.WriteFileData(path, objStr);
        }
        public IModel GetobjById<T>(Guid id,string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<T>>(str);
            var modelList = objList.Where(x => x is IModel)
               .Cast<IModel>().ToList();
            var model = _linqService.GetById(id, modelList);
            return model;
        }
        public void DeleteObjfromData<T>(Guid id, string path) 
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<T>>(str);
            var modelList = objList.Where(x => x is IModel)
               .Cast<IModel>().ToList();
            _linqService.DeleteById(id, modelList);
            var objListStr = _serializer.Serialize(modelList);
            _remoteFile.WriteFileData(path, objListStr); 
        }
        public void UpdateObjinData<T>(IModel obj, string path) 
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<T>>(str);
            var modelList = objList.Where(x => x is IModel)
                .Cast<IModel>().ToList();
            _linqService.UpdateById(obj, modelList);
            var objListStr = _serializer.Serialize(modelList);
            _remoteFile.WriteFileData(path, objListStr); 
        }
    }
}
