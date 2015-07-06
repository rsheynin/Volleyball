namespace Volleyball.Services
{
    public interface IRemoteFile
    {
        string ReadFileData(string path);

        void WriteFileData(string path, string content);

        void AppendAllText(string path, string content);


    }
}
