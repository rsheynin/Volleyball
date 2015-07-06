
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Volleyball.Models;
using Volleyball.Services;

namespace VolleyballTests
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
