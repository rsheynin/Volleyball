﻿using System.Collections.Generic;
using Newtonsoft.Json;
using VB.Infrastructure.Models;

namespace VB.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Serializer : ISerializer
    {
        /// <summary>
        /// Convert string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objStr"></param>
        /// <returns></returns>
        public T DeSerialize<T>(string objStr)
        {
            var obj = JsonConvert.DeserializeObject<T>(objStr);
            return obj;
        }

        /// <summary>
        /// Convert Object to String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            var objStr = JsonConvert.SerializeObject(obj);
            return objStr;
        }

    }
}
