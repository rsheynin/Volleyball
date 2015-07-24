using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VB.Infrastructure.Services;

namespace VB.Infrastructure.Tests
{
    [TestClass]
    public class RemoteFileTest
    {

        private RemoteFile _target;
        private string _path;
        private string _expectedString;

        [TestInitialize]
        public void InIt()
        {
            _target = new RemoteFile();

        }

        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> is a directory.-or- <paramref name="path" /> specified a read-only file. </exception>
        [TestCleanup]
        public void Cleanup()
        {

            File.Delete("player.json");
        }

        /// <summary>
        /// 1. ReadFileData_NotExistFile_ReturnNull
        /// 2. ReadFileData_ExistFile_ReturnEmptyString
        /// 3. ReadFileData_ExistFile_ReturnString

        /// </summary>




        [TestMethod]
        public void ReadFileData_NotExistFile_ReturnNull()
        {
            _expectedString = "[]";
            _path = "player.json";
            var actual = _target.ReadFileData(_path);
            Assert.AreEqual(_expectedString, actual);
        }

        [TestMethod]
        public void ReadFileData_FileExist_ReturnEmptyString()
        {
            string content = "";
            _path = "player.json";

            _target.WriteFileData(_path, content);
            _expectedString = "";


            var actual = _target.ReadFileData(_path);
            Assert.AreEqual(_expectedString, actual);
        }

        [TestMethod]
        public void ReadFileData_FileExist_ReturnString()
        {
            string content = "player";
            _path = "player.json";

            _target.WriteFileData(_path, content);
            _expectedString = "player";


            var actual = _target.ReadFileData(_path);
            Assert.AreEqual(_expectedString, actual);
        }

        /// <summary>
        /// 1. WriteFileData_ExistFile_ReturnOverwritedFile()
        //
        /// </summary>
       
        
        [TestMethod]
        public void WriteFileData_FileExist_ReturnOverwritedFile()
        {string content = "player****";
            _path = "player.json";

            _target.WriteFileData(_path, content);
            _expectedString = "player****";


            var actual = _target.ReadFileData(_path);
            Assert.AreEqual(_expectedString, actual);
           
        }

        // if file exist
        


        [TestMethod]
        public void AppendAllText_FileExist_ReturnAppendedFile()
        { 
            _path = "player.json";
            string firstcontent = "player****";
            _target.WriteFileData(_path, firstcontent);         
            
            string content = "volley";
           
            _target.AppendAllText(_path, content);
            _expectedString ="player****volley";
              
            
            var actual = _target.ReadFileData(_path);
            Assert.AreEqual(_expectedString,actual);

        }
     
    }

     
}
