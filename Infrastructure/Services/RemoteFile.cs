﻿using System.IO;

namespace VB.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class  RemoteFile: IRemoteFile
    {
        /// <summary>
        /// Read the string content of the file by path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadFileData(string path)
        {
            if (!File.Exists(path))
                return "[]";

            var str = File.ReadAllText(path);
            return str;
        }

        /// <summary>
        /// Override file content if file exist else create file and write the string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        public void WriteFileData(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        /// <summary>
        /// appen string to file content
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <exception cref="FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        public void AppendAllText(string path, string content)
        {
            File.AppendAllText(path, content);
        }
    }
}
