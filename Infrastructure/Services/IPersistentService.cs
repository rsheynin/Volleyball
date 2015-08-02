using System;
using VB.Infrastructure.Models;

namespace VB.Infrastructure.Services
{
    /// <summary>
    ///     Interface for PersistentServicefunc
    /// </summary>
    public interface IPersistentService
    {
        /// <summary>
        ///     Add obj in existing objList or create new objList and Add obj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        void Saveobject(IModel obj, string path);

        /// <summary>
        ///     Read all file and get object from it by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        IModel GetobjById(Guid id, string path);

        /// <summary>
        ///     Read all file, get object from it by Id and Delete obj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="path"></param>
        void DeleteObjfromData(Guid id, string path);

        /// <summary>
        ///     Read all file and update object in it by Id
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        void UpdateObjinData(IModel obj, string path);

        /// <summary>
        ///     Select object by name from list of objects
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        IModel GetobjByName(string objName, string path);

        /// <summary>
        ///     Select objectList by filename from data
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        T GetObjectList<T>(string path);
    }
}