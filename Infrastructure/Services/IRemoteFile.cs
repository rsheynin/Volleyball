namespace VB.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRemoteFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadFileData(string path);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        void WriteFileData(string path, string content);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        void AppendAllText(string path, string content);


    }
}
