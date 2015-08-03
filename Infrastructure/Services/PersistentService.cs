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
        private readonly Guid _teamid = Guid.NewGuid();
        private readonly Guid _playerid = Guid.NewGuid();
        private readonly Guid _seasonid = Guid.NewGuid();


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
                objList = new List<IModel> {obj};
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
        public IModel GetobjById(Guid id, string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<IModel>>(str);
            var model = _linqService.GetById(id, objList);
            return model;
        }

        /// <summary>
        /// Select object by name from list of objects
        /// </summary>
        /// <param name="objName"></param>
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
        /// Select objectList by filename from data
        /// </summary>
        /// <param name="path"></param>
        /// <returns List="IModel"></returns>
        public T GetObjectList<T>(string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<T>(str);

            return objList;
        }

        /// <summary>
        /// Select objectList by filename from data
        /// </summary>
        /// <param name="path"></param>
        /// <returns List="IModel"></returns>
        public List<IModel> GetObjectList(string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var objList = _serializer.DeSerialize<List<IModel>>(str);

            return objList;
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

        public void Saveobject(TeamPlayers obj, string path)
        {

            var str = _remoteFile.ReadFileData(path);
            List<TeamPlayers> teamPlayersList;
            if (str == "[]")
            {
                teamPlayersList = new List<TeamPlayers> {obj};
            }
            else
            {
                teamPlayersList = _serializer.DeSerialize<List<TeamPlayers>>(str);
                teamPlayersList.Add(obj);
            }

            var objStr = _serializer.Serialize(teamPlayersList);
            _remoteFile.WriteFileData(path, objStr);
        }

        public TeamPlayers GetTeamPlayersByTeamId(Guid teamId, string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var teamPlayersList = _serializer.DeSerialize<List<TeamPlayers>>(str);
            var teamPlayers = teamPlayersList.Find(x => x.TeamId == _teamid);
            return teamPlayers;
        } 
        
        public TeamPlayers GetTeamPlayersByPlayerId(Guid playerId, string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var teamPlayersList = _serializer.DeSerialize<List<TeamPlayers>>(str);
            var teamPlayers = teamPlayersList.Find(x => x.PlayerId == _playerid);
            return teamPlayers;
        } 
        
        public TeamPlayers GetTeamPlayersBySeasonId(Guid seasonId, string path)
        {
            var str = _remoteFile.ReadFileData(path);
            var teamPlayersList = _serializer.DeSerialize<List<TeamPlayers>>(str);
            var teamPlayers = teamPlayersList.Find(x => x.SeasonId == _seasonid);
            return teamPlayers;
        }
    }
}
