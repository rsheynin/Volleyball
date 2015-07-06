using System;
using System.Collections.Generic;
using System.Linq;
using Volleyball.Models;

namespace Volleyball.Services
{
    public class LinqService: ILinqService
    {
        /// <summary>
        /// Get object by Id from colection
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objList"></param>
        /// <returns></returns>
        public IModel GetById(Guid id, List<IModel> objList)
        {
            var obj = objList.FirstOrDefault(x => x.Id == id);
            return obj;
        }

        /// <summary>
        /// delete object from list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objList"></param>
        public void DeleteById(Guid id, List<IModel> objList)
        {
            var itemToDelete = objList.FirstOrDefault(x => x.Id == id);
            if (itemToDelete != null)
                objList.Remove(itemToDelete);
        }

        /// <summary>
        /// Update concrete objec by id 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objList"></param>
        public void UpdateById(IModel obj, List<IModel> objList)
        {
            var itemToUpdate = objList.FirstOrDefault(x => x.Id == obj.Id);
            if (itemToUpdate != null)
            {
                objList.Remove(itemToUpdate);
                objList.Add(obj);
            }
        }

        /// <summary>
        /// save object to list
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objList"></param>
        public void Save(IModel obj, List<IModel> objList)
        {
            objList.Add(obj);
        }
    }
}
