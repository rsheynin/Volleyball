using System;
using System.Collections.Generic;
using Volleyball.Models;

namespace Volleyball.Services
{
    public interface ILinqService
    {
        /// <summary>
        /// Get object by Id from colection
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
        /// Update concrete objec by id 
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
    }
}
