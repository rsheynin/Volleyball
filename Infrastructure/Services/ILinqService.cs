using System;
using System.Collections.Generic;
using VB.Infrastructure.Models;

namespace VB.Infrastructure.Services
{
    public interface ILinqService
    {
        /// <summary>
        /// Get object by Id from collection
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objList"></param>
        /// <returns></returns>
        IModel GetById(Guid id, List<IModel> objList);

        /// <summary>
        /// delete object from list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objList"></param>
        void DeleteById(Guid id, List<IModel> objList);

        /// <summary>
        /// Update concrete object by id 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objList"></param>
        void UpdateById(IModel obj, List<IModel> objList);

        /// <summary>
        /// save object to list
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objList"></param>
        void Save(IModel obj, List<IModel> objList);

        /// <summary>
        /// Get object by name from collection
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objList"></param>
        /// <returns></returns>
        IModel GetByName(string name, List<IModel> objList);
    }
}
